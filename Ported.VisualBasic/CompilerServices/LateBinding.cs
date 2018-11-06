// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.LateBinding
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll



using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
//using System.Runtime.Remoting;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated since Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class LateBinding
  {
    private const CallType DefaultCallType = (CallType) 0;
    private const BindingFlags VBLateBindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding;

    private LateBinding()
    {
    }

    private static MemberInfo GetMostDerivedMemberInfo(IReflect objIReflect, string name, BindingFlags flags)
    {
      MemberInfo[] nonGenericMembers = LateBinding.GetNonGenericMembers(objIReflect.GetMember(name, flags));
      if (nonGenericMembers == null || nonGenericMembers.Length == 0)
        return (MemberInfo) null;
      MemberInfo memberInfo = nonGenericMembers[0];
      int num = 1;
      int upperBound = nonGenericMembers.GetUpperBound(0);
      int index = num;
      while (index <= upperBound)
      {
        if (nonGenericMembers[index].DeclaringType.IsSubclassOf(memberInfo.DeclaringType))
          memberInfo = nonGenericMembers[index];
        checked { ++index; }
      }
      return memberInfo;
    }

    /// <summary>Returns a late-bound value from an object.</summary>
    /// <param name="o">The object to return the value from.</param>
    /// <param name="objType">The type of the object.</param>
    /// <param name="name">The member name of <paramref name="o" /> to retrieve a value from.</param>
    /// <param name="args">An array of one or more index values that specify the location in <paramref name="o" /> if <paramref name="o" /> is a one-dimensional or multi-dimensional array, or argument values to pass to the object <paramref name="o" />.</param>
    /// <param name="paramnames">An array that contains the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
    /// <param name="CopyBack">An array of <see langword="Boolean" /> values, where <see langword="True" /> indicates that the associated parameter is passed <see langword="ByRef" />.</param>
    /// <returns>If <paramref name="o" /> is an array, the value from <paramref name="o" /> at the location specified by <paramref name="args" />. If <paramref name="o" /> is an object, the return value of <paramref name="o" /> invoked by using the named parameters in <paramref name="paramnames" /> and their associated values in <paramref name="args" />.</returns>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static object LateGet(object o, Type objType, string name, object[] args, string[] paramnames, bool[] CopyBack)
    {
      BindingFlags invokeAttr1 = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.OptionalParamBinding;
      if (objType == null)
      {
        if (o == null)
          throw ExceptionUtils.VbMakeException(91);
        objType = o.GetType();
      }
      IReflect correctIreflect = LateBinding.GetCorrectIReflect(o, objType);
      if (name == null)
        name = "";
      if (objType.IsCOMObject)
      {
        LateBinding.CheckForClassExtendingCOMClass(objType);
      }
      else
      {
        MemberInfo derivedMemberInfo = LateBinding.GetMostDerivedMemberInfo(correctIreflect, name, invokeAttr1 | BindingFlags.GetField);
        if (derivedMemberInfo != null && derivedMemberInfo.MemberType == MemberTypes.Field)
        {
          VBBinder.SecurityCheckForLateboundCalls(derivedMemberInfo, objType, correctIreflect);
          object o1;
          if (objType == correctIreflect || ((FieldInfo) derivedMemberInfo).IsStatic || LateBinding.DoesTargetObjectMatch(o, derivedMemberInfo))
          {
            LateBinding.VerifyObjRefPresentForInstanceCall(o, derivedMemberInfo);
            o1 = ((FieldInfo) derivedMemberInfo).GetValue(o);
          }
          else
            o1 = LateBinding.InvokeMemberOnIReflect(correctIreflect, derivedMemberInfo, BindingFlags.GetField, o, (object[]) null);
          if (args == null || args.Length == 0)
            return o1;
          return LateBinding.LateIndexGet(o1, args, paramnames);
        }
      }
      VBBinder vbBinder = new VBBinder(CopyBack);
      try
      {
        return vbBinder.InvokeMember(name, invokeAttr1, objType, correctIreflect, o, args, paramnames);
      }
      catch (Exception ex1) when (LateBinding.IsMissingMemberException(ex1))
      {
        if (objType.IsCOMObject || args != null && args.Length > 0)
        {
          BindingFlags invokeAttr2 = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.OptionalParamBinding;
          if (!objType.IsCOMObject)
            invokeAttr2 |= BindingFlags.GetField;
          object o1;
          try
          {
            o1 = vbBinder.InvokeMember(name, invokeAttr2, objType, correctIreflect, o, (object[]) null, (string[]) null);
          }
          catch (AccessViolationException ex2)
          {
            throw ex2;
          }
          catch (StackOverflowException ex2)
          {
            throw ex2;
          }
          catch (OutOfMemoryException ex2)
          {
            throw ex2;
          }
          catch (ThreadAbortException ex2)
          {
            throw ex2;
          }
          catch (Exception ex2)
          {
            o1 = (object) null;
          }
          if (o1 == null)
            throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
          try
          {
            return LateBinding.LateIndexGet(o1, args, paramnames);
          }
          catch (Exception ex2) when (LateBinding.IsMissingMemberException(ex2) && ex1 is MissingMemberException && 1U > 0U)
          {
            throw ex1;
          }
        }
        else
          throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    private static bool IsMissingMemberException(Exception ex)
    {
      if (ex is MissingMemberException || ex is MemberAccessException)
        return true;
      COMException comException = ex as COMException;
      if (comException != null)
      {
        if (comException.ErrorCode == -2147352570 || comException.ErrorCode == -2146827850)
          return true;
      }
      else if (ex is TargetInvocationException && ex.InnerException is COMException && ((ExternalException) ex.InnerException).ErrorCode == -2147352559)
        return true;
      return false;
    }

    /// <summary>Sets a late-bound value of a member of an object, using the specified parameters.</summary>
    /// <param name="o">The object to set the member value for.</param>
    /// <param name="objType">The type of the object.</param>
    /// <param name="name">The member name to set.</param>
    /// <param name="args">An array of one or more parameter values to pass to the member of <paramref name="o" />.</param>
    /// <param name="paramnames">An array that contains the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
    /// <param name="OptimisticSet">
    /// <see langword="True" /> to suppress the exception thrown when the set member is not found.</param>
    /// <param name="RValueBase">
    /// <see langword="True" /> to identify <paramref name="o" /> as the result of a late-bound expression.</param>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static void LateSetComplex(object o, Type objType, string name, object[] args, string[] paramnames, bool OptimisticSet, bool RValueBase)
    {
      try
      {
        LateBinding.InternalLateSet(o, ref objType, name, args, paramnames, OptimisticSet, (CallType) 0);
        if (RValueBase && objType.IsValueType)
          throw new Exception(Utils.GetResourceString("RValueBaseForValueType", Utils.VBFriendlyName(objType, o), Utils.VBFriendlyName(objType, o)));
      }
      catch (MissingMemberException ex) when (OptimisticSet)
      {
      }
    }

    /// <summary>Sets a late-bound value of a member of an object, using the specified parameters.</summary>
    /// <param name="o">The object to set the member value for.</param>
    /// <param name="objType">The type of the object.</param>
    /// <param name="name">The member name to set.</param>
    /// <param name="args">An array of one or parameter values to pass to the member of <paramref name="o" />.</param>
    /// <param name="paramnames">An array that contains the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public static void LateSet(object o, Type objType, string name, object[] args, string[] paramnames)
    {
      LateBinding.InternalLateSet(o, ref objType, name, args, paramnames, false, (CallType) 0);
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    internal static void InternalLateSet(object o, ref Type objType, string name, object[] args, string[] paramnames, bool OptimisticSet, CallType UseCallType)
    {
      BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding;
      if (objType == null)
      {
        if (o == null)
          throw ExceptionUtils.VbMakeException(91);
        objType = o.GetType();
      }
      IReflect correctIreflect = LateBinding.GetCorrectIReflect(o, objType);
      if (name == null)
        name = "";
      BindingFlags invokeAttr1;
      if (objType.IsCOMObject)
      {
        LateBinding.CheckForClassExtendingCOMClass(objType);
        switch (UseCallType)
        {
          case CallType.Let:
            invokeAttr1 = bindingFlags | BindingFlags.PutDispProperty;
            break;
          case CallType.Set:
            invokeAttr1 = bindingFlags | BindingFlags.PutRefDispProperty;
            if (args[args.GetUpperBound(0)] == null)
            {
              args[args.GetUpperBound(0)] = (object) new DispatchWrapper((object) null);
              break;
            }
            break;
          default:
            invokeAttr1 = bindingFlags | LateBinding.GetPropertyPutFlags(args[args.GetUpperBound(0)]);
            break;
        }
      }
      else
      {
        invokeAttr1 = bindingFlags | BindingFlags.SetProperty;
        MemberInfo derivedMemberInfo = LateBinding.GetMostDerivedMemberInfo(correctIreflect, name, invokeAttr1 | BindingFlags.SetField);
        if (derivedMemberInfo != null && derivedMemberInfo.MemberType == MemberTypes.Field)
        {
          FieldInfo fieldInfo = (FieldInfo) derivedMemberInfo;
          if (fieldInfo.IsInitOnly)
            throw new MissingMemberException(Utils.GetResourceString("MissingMember_ReadOnlyField2", name, Utils.VBFriendlyName(objType, o)));
          if (args == null || args.Length == 0)
            throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
          if (args.Length == 1)
          {
            object obj1 = args[0];
            VBBinder.SecurityCheckForLateboundCalls((MemberInfo) fieldInfo, objType, correctIreflect);
            object obj2 = obj1 != null ? ObjectType.CTypeHelper(args[0], fieldInfo.FieldType) : (object) null;
            if (objType == correctIreflect || fieldInfo.IsStatic || LateBinding.DoesTargetObjectMatch(o, (MemberInfo) fieldInfo))
            {
              LateBinding.VerifyObjRefPresentForInstanceCall(o, (MemberInfo) fieldInfo);
              fieldInfo.SetValue(o, obj2);
              return;
            }
            LateBinding.InvokeMemberOnIReflect(correctIreflect, (MemberInfo) fieldInfo, BindingFlags.SetField, o, new object[1]
            {
              obj2
            });
            return;
          }
          if (args.Length > 1)
          {
            VBBinder.SecurityCheckForLateboundCalls(derivedMemberInfo, objType, correctIreflect);
            object obj = (object) null;
            object o1;
            if (objType == correctIreflect || ((FieldInfo) derivedMemberInfo).IsStatic || LateBinding.DoesTargetObjectMatch(o, derivedMemberInfo))
            {
              LateBinding.VerifyObjRefPresentForInstanceCall(o, derivedMemberInfo);
              o1 = ((FieldInfo) derivedMemberInfo).GetValue(o);
            }
            else
              o1 = LateBinding.InvokeMemberOnIReflect(correctIreflect, derivedMemberInfo, BindingFlags.GetField, o, new object[1]
              {
                obj
              });
            LateBinding.LateIndexSet(o1, args, paramnames);
            return;
          }
        }
      }
      VBBinder vbBinder = new VBBinder((bool[]) null);
      if (OptimisticSet)
      {
        if (args.GetUpperBound(0) > 0)
        {
          BindingFlags bindingAttr = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty | BindingFlags.OptionalParamBinding;
          Type[] types = new Type[checked (args.GetUpperBound(0) - 1 + 1)];
          int num = 0;
          int upperBound = types.GetUpperBound(0);
          int index = num;
          while (index <= upperBound)
          {
            object obj = args[index];
            types[index] = obj != null ? obj.GetType() : (Type) null;
            checked { ++index; }
          }
          try
          {
            PropertyInfo property = correctIreflect.GetProperty(name, bindingAttr, (Binder) vbBinder, typeof (int), types, (ParameterModifier[]) null);
            if (property == null)
              return;
            if (!property.CanWrite)
              return;
          }
          catch (MissingMemberException ex)
          {
            return;
          }
        }
      }
      try
      {
        vbBinder.InvokeMember(name, invokeAttr1, objType, correctIreflect, o, args, paramnames);
      }
      catch (Exception ex1) when (LateBinding.IsMissingMemberException(ex1))
      {
        if (args != null && args.Length > 1)
        {
          BindingFlags invokeAttr2 = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty | BindingFlags.OptionalParamBinding;
          if (!objType.IsCOMObject)
            invokeAttr2 |= BindingFlags.GetField;
          object o1;
          try
          {
            o1 = vbBinder.InvokeMember(name, invokeAttr2, objType, correctIreflect, o, (object[]) null, (string[]) null);
          }
          catch (Exception ex2) when (LateBinding.IsMissingMemberException(ex2) && ex1 is MissingMemberException && 1U > 0U)
          {
            throw ex1;
          }
          catch (AccessViolationException ex2)
          {
            throw ex2;
          }
          catch (StackOverflowException ex2)
          {
            throw ex2;
          }
          catch (OutOfMemoryException ex2)
          {
            throw ex2;
          }
          catch (ThreadAbortException ex2)
          {
            throw ex2;
          }
          catch (Exception ex2)
          {
            o1 = (object) null;
          }
          if (o1 == null)
            throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
          try
          {
            LateBinding.LateIndexSet(o1, args, paramnames);
          }
          catch (Exception ex2) when (LateBinding.IsMissingMemberException(ex2) && ex1 is MissingMemberException && 1U > 0U)
          {
            throw ex1;
          }
        }
        else
          throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
      }
      catch (TargetInvocationException ex)
      {
        if (ex.InnerException == null)
          throw ex;
        if (!(ex.InnerException is TargetParameterCountException))
          throw ex.InnerException;
        if ((invokeAttr1 & BindingFlags.PutRefDispProperty) != BindingFlags.Default)
          throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberSetNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
        throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberLetNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
      }
    }

    private static void CheckForClassExtendingCOMClass(Type objType)
    {
      if (objType.IsCOMObject && Operators.CompareString(objType.FullName, "System.__ComObject", false) != 0 && Operators.CompareString(objType.BaseType.FullName, "System.__ComObject", false) != 0)
        throw new InvalidOperationException(Utils.GetResourceString("LateboundCallToInheritedComClass"));
    }

    /// <summary>Returns a late-bound value from an object, using the specified index or parameters.</summary>
    /// <param name="o">The object to return the value from.</param>
    /// <param name="args">An array of one or more index values that specify the location in <paramref name="o" /> if <paramref name="o" /> is a one-dimensional or multi-dimensional array, or argument values to pass to the object <paramref name="o" />.</param>
    /// <param name="paramnames">An array that contains the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
    /// <returns>If <paramref name="o" /> is an array, the value from <paramref name="o" /> at the location specified by <paramref name="args" />. If <paramref name="o" /> is an object, the return value of <paramref name="o" /> invoked by using the named parameters in <paramref name="paramnames" /> and their associated values in <paramref name="args" />.</returns>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public static object LateIndexGet(object o, object[] args, string[] paramnames)
    {
      string DefaultName = (string) null;
      if (o == null)
        throw ExceptionUtils.VbMakeException(91);
      Type type = o.GetType();
      IReflect correctIreflect = LateBinding.GetCorrectIReflect(o, type);
      if (type.IsArray)
      {
        if (paramnames != null && paramnames.Length != 0)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNamedArgs"));
        Array array = (Array) o;
        int length = args.Length;
        if (length != array.Rank)
          throw new RankException();
        if (length == 1)
          return array.GetValue(Conversions.ToInteger(args[0]));
        if (length == 2)
          return array.GetValue(Conversions.ToInteger(args[0]), Conversions.ToInteger(args[1]));
        int[] numArray = new int[checked (length - 1 + 1)];
        int num1 = 0;
        int num2 = checked (length - 1);
        int index = num1;
        while (index <= num2)
        {
          numArray[index] = Conversions.ToInteger(args[index]);
          checked { ++index; }
        }
        return array.GetValue(numArray);
      }
      MethodBase[] match = (MethodBase[]) null;
      BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.OptionalParamBinding;
      if (!type.IsCOMObject)
      {
        if (args == null || args.Length == 0)
          bindingFlags |= BindingFlags.GetField;
        MemberInfo[] defaultMembers = LateBinding.GetDefaultMembers(type, correctIreflect, ref DefaultName);
        int index1=0;
        if (defaultMembers != null)
        {
          int num = 0;
          int upperBound = defaultMembers.GetUpperBound(0);
          int index2 = num;
          while (index2 <= upperBound)
          {
            MemberInfo getMethod = defaultMembers[index2];
            if (getMethod.MemberType == MemberTypes.Property)
              getMethod = (MemberInfo) ((PropertyInfo) getMethod).GetGetMethod();
            if (getMethod != null && getMethod.MemberType != MemberTypes.Field)
            {
              defaultMembers[index1] = getMethod;
              checked { ++index1; }
            }
            checked { ++index2; }
          }
        }
        if (defaultMembers == null | index1 == 0)
          throw new MissingMemberException(Utils.GetResourceString("MissingMember_NoDefaultMemberFound1", new string[1]
          {
            Utils.VBFriendlyName(type, o)
          }));
        match = new MethodBase[checked (index1 - 1 + 1)];
        int num1 = 0;
        int num2 = checked (index1 - 1);
        int index3 = num1;
        while (index3 <= num2)
        {
          try
          {
            match[index3] = (MethodBase) defaultMembers[index3];
          }
          catch (StackOverflowException ex)
          {
            throw ex;
          }
          catch (OutOfMemoryException ex)
          {
            throw ex;
          }
          catch (ThreadAbortException ex)
          {
            throw ex;
          }
          catch (Exception ex)
          {
          }
          checked { ++index3; }
        }
      }
      else
        LateBinding.CheckForClassExtendingCOMClass(type);
      VBBinder vbBinder = new VBBinder((bool[]) null);
      try
      {
        if (type.IsCOMObject)
          return vbBinder.InvokeMember("", bindingFlags, type, correctIreflect, o, args, paramnames);
        object ObjState = (object) null;
        vbBinder.m_BindToName = DefaultName;
        vbBinder.m_objType = type;
        MethodBase method = vbBinder.BindToMethod(bindingFlags, match, ref args, (ParameterModifier[]) null, (CultureInfo) null, paramnames, out ObjState);
        VBBinder.SecurityCheckForLateboundCalls((MemberInfo) method, type, correctIreflect);
        object obj;
        if (type == correctIreflect || method.IsStatic || LateBinding.DoesTargetObjectMatch(o, (MemberInfo) method))
        {
          LateBinding.VerifyObjRefPresentForInstanceCall(o, (MemberInfo) method);
          obj = method.Invoke(o, args);
        }
        else
          obj = LateBinding.InvokeMemberOnIReflect(correctIreflect, (MemberInfo) method, BindingFlags.InvokeMethod, o, args);
        vbBinder.ReorderArgumentArray(ref args, ObjState);
        return obj;
      }
      catch (Exception ex) when (LateBinding.IsMissingMemberException(ex))
      {
        throw new MissingMemberException(Utils.GetResourceString("MissingMember_NoDefaultMemberFound1", new string[1]
        {
          Utils.VBFriendlyName(type, o)
        }));
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    private static MemberInfo[] GetDefaultMembers(Type typ, IReflect objIReflect, ref string DefaultName)
    {
      if (typ == objIReflect)
      {
        do
        {
          object[] customAttributes = typ.GetCustomAttributes(typeof (DefaultMemberAttribute), false);
          if (customAttributes != null && customAttributes.Length != 0)
          {
            DefaultName = ((DefaultMemberAttribute) customAttributes[0]).MemberName;
            MemberInfo[] nonGenericMembers = LateBinding.GetNonGenericMembers(typ.GetMember(DefaultName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
            if (nonGenericMembers != null && nonGenericMembers.Length != 0)
              return nonGenericMembers;
            DefaultName = "";
            return (MemberInfo[]) null;
          }
          typ = typ.BaseType;
        }
        while (typ != null);
        DefaultName = "";
        return (MemberInfo[]) null;
      }
      MemberInfo[] nonGenericMembers1 = LateBinding.GetNonGenericMembers(objIReflect.GetMember("", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
      if (nonGenericMembers1 == null || nonGenericMembers1.Length == 0)
      {
        DefaultName = "";
        return (MemberInfo[]) null;
      }
      DefaultName = nonGenericMembers1[0].Name;
      return nonGenericMembers1;
    }

    /// <summary>Sets a late-bound value of an object, using the specified parameters.</summary>
    /// <param name="o">The object to set the value for.</param>
    /// <param name="args">An array of one or more parameter values to pass to the object <paramref name="o" />.</param>
    /// <param name="paramnames">An array that contains the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
    /// <param name="OptimisticSet">
    /// <see langword="True" /> to suppress the exception thrown when the set member is not found.</param>
    /// <param name="RValueBase">
    /// <see langword="True" /> to identify <paramref name="o" /> as the result of a late-bound expression.</param>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public static void LateIndexSetComplex(object o, object[] args, string[] paramnames, bool OptimisticSet, bool RValueBase)
    {
      try
      {
        LateBinding.LateIndexSet(o, args, paramnames);
        if (RValueBase && o.GetType().IsValueType)
          throw new Exception(Utils.GetResourceString("RValueBaseForValueType", o.GetType().Name, o.GetType().Name));
      }
      catch (MissingMemberException ex) when (OptimisticSet)
      {
      }
    }

    /// <summary>Sets a late-bound value of an object, using the specified parameters.</summary>
    /// <param name="o">The object to set the value for.</param>
    /// <param name="args">An array of one or more index values that specify the location in <paramref name="o" /> if <paramref name="o" /> is a one-dimensional or multi-dimensional array, or argument values to pass to the object <paramref name="o" />.</param>
    /// <param name="paramnames">An array that contains the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static void LateIndexSet(object o, object[] args, string[] paramnames)
    {
      string DefaultName = (string) null;
      if (o == null)
        throw ExceptionUtils.VbMakeException(91);
      Type type = o.GetType();
      IReflect correctIreflect = LateBinding.GetCorrectIReflect(o, type);
      if (type.IsArray)
      {
        if (paramnames != null && paramnames.Length != 0)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNamedArgs"));
        Array array = (Array) o;
        int index1 = checked (args.Length - 1);
        object obj = args[index1];
        if (obj != null)
        {
          Type elementType = type.GetElementType();
          if (obj.GetType() != elementType)
            obj = ObjectType.CTypeHelper(obj, elementType);
        }
        if (index1 != array.Rank)
          throw new RankException();
        if (index1 == 1)
          array.SetValue(obj, Conversions.ToInteger(args[0]));
        else if (index1 == 2)
        {
          array.SetValue(obj, Conversions.ToInteger(args[0]), Conversions.ToInteger(args[1]));
        }
        else
        {
          int[] numArray = new int[checked (index1 - 1 + 1)];
          int num1 = 0;
          int num2 = checked (index1 - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            numArray[index2] = Conversions.ToInteger(args[index2]);
            checked { ++index2; }
          }
          array.SetValue(obj, numArray);
        }
      }
      else
      {
        MethodBase[] match = (MethodBase[]) null;
        BindingFlags bindingFlags1 = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding;
        BindingFlags bindingFlags2;
        if (type.IsCOMObject)
        {
          LateBinding.CheckForClassExtendingCOMClass(type);
          bindingFlags2 = bindingFlags1 | LateBinding.GetPropertyPutFlags(args[args.GetUpperBound(0)]);
        }
        else
        {
          bindingFlags2 = bindingFlags1 | BindingFlags.SetProperty;
          if (args.Length == 1)
            bindingFlags2 |= BindingFlags.SetField;
          MemberInfo[] defaultMembers = LateBinding.GetDefaultMembers(type, correctIreflect, ref DefaultName);
          int index1=0;
          if (defaultMembers != null)
          {
            int num = 0;
            int upperBound = defaultMembers.GetUpperBound(0);
            int index2 = num;
            while (index2 <= upperBound)
            {
              MemberInfo setMethod = defaultMembers[index2];
              if (setMethod.MemberType == MemberTypes.Property)
                setMethod = (MemberInfo) ((PropertyInfo) setMethod).GetSetMethod();
              if (setMethod != null && setMethod.MemberType != MemberTypes.Field)
              {
                defaultMembers[index1] = setMethod;
                checked { ++index1; }
              }
              checked { ++index2; }
            }
          }
          if (defaultMembers == null | index1 == 0)
            throw new MissingMemberException(Utils.GetResourceString("MissingMember_NoDefaultMemberFound1", new string[1]
            {
              Utils.VBFriendlyName(type, o)
            }));
          match = new MethodBase[checked (index1 - 1 + 1)];
          int num1 = 0;
          int num2 = checked (index1 - 1);
          int index3 = num1;
          while (index3 <= num2)
          {
            try
            {
              match[index3] = (MethodBase) defaultMembers[index3];
            }
            catch (StackOverflowException ex)
            {
              throw ex;
            }
            catch (OutOfMemoryException ex)
            {
              throw ex;
            }
            catch (ThreadAbortException ex)
            {
              throw ex;
            }
            catch (Exception ex)
            {
            }
            checked { ++index3; }
          }
        }
        VBBinder vbBinder = new VBBinder((bool[]) null);
        try
        {
          if (type.IsCOMObject)
          {
            vbBinder.InvokeMember("", bindingFlags2, type, correctIreflect, o, args, paramnames);
          }
          else
          {
            object ObjState = (object) null;
            vbBinder.m_BindToName = DefaultName;
            vbBinder.m_objType = type;
            MethodBase method = vbBinder.BindToMethod(bindingFlags2, match, ref args, (ParameterModifier[]) null, (CultureInfo) null, paramnames, out ObjState);
            VBBinder.SecurityCheckForLateboundCalls((MemberInfo) method, type, correctIreflect);
            if (type == correctIreflect || method.IsStatic || LateBinding.DoesTargetObjectMatch(o, (MemberInfo) method))
            {
              LateBinding.VerifyObjRefPresentForInstanceCall(o, (MemberInfo) method);
              method.Invoke(o, args);
            }
            else
              LateBinding.InvokeMemberOnIReflect(correctIreflect, (MemberInfo) method, BindingFlags.InvokeMethod, o, args);
            vbBinder.ReorderArgumentArray(ref args, ObjState);
          }
        }
        catch (Exception ex) when (LateBinding.IsMissingMemberException(ex))
        {
          throw new MissingMemberException(Utils.GetResourceString("MissingMember_NoDefaultMemberFound1", new string[1]
          {
            Utils.VBFriendlyName(type, o)
          }));
        }
        catch (TargetInvocationException ex)
        {
          throw ex.InnerException;
        }
      }
    }

    private static BindingFlags GetPropertyPutFlags(object NewValue)
    {
      if (NewValue == null)
        return BindingFlags.SetProperty;
      return NewValue is ValueType || NewValue is string || (NewValue is DBNull || NewValue is Missing) || (NewValue is Array || NewValue is CurrencyWrapper) ? BindingFlags.PutDispProperty : BindingFlags.PutRefDispProperty;
    }

    /// <summary>Performs a late-bound call to an object method.</summary>
    /// <param name="o">The object to call a method on.</param>
    /// <param name="objType">The type of the object.</param>
    /// <param name="name">The method name of <paramref name="o" /> to call.</param>
    /// <param name="args">An array of parameter values to pass to the method.</param>
    /// <param name="paramnames">An array that contains the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
    /// <param name="CopyBack">An array of <see langword="Boolean" /> values, where <see langword="True" /> indicates that the associated parameter is passed <see langword="ByRef" />.</param>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public static void LateCall(object o, Type objType, string name, object[] args, string[] paramnames, bool[] CopyBack)
    {
      LateBinding.InternalLateCall(o, objType, name, args, paramnames, CopyBack, true);
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    internal static object InternalLateCall(object o, Type objType, string name, object[] args, string[] paramnames, bool[] CopyBack, bool IgnoreReturn)
    {
      BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod | BindingFlags.OptionalParamBinding;
      if (IgnoreReturn)
        bindingFlags |= BindingFlags.IgnoreReturn;
      if (objType == null)
      {
        if (o == null)
          throw ExceptionUtils.VbMakeException(91);
        objType = o.GetType();
      }
      IReflect correctIreflect = LateBinding.GetCorrectIReflect(o, objType);
      if (objType.IsCOMObject)
        LateBinding.CheckForClassExtendingCOMClass(objType);
      if (name == null)
        name = "";
      VBBinder vbBinder = new VBBinder(CopyBack);
      if (!objType.IsCOMObject)
      {
        MemberInfo[] membersByName = LateBinding.GetMembersByName(correctIreflect, name, bindingFlags);
        if (membersByName == null || membersByName.Length == 0)
          throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
        if (LateBinding.MemberIsField(membersByName))
          throw new ArgumentException(Utils.GetResourceString("ExpressionNotProcedure", name, Utils.VBFriendlyName(objType, o)));
        if (membersByName.Length == 1)
        {
          if (paramnames != null)
          {
            if (paramnames.Length != 0)
              goto label_31;
          }
          MemberInfo getMethod = membersByName[0];
          if (getMethod.MemberType == MemberTypes.Property)
          {
            getMethod = (MemberInfo) ((PropertyInfo) getMethod).GetGetMethod();
            if (getMethod == null)
              throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
          }
          MethodBase method = (MethodBase) getMethod;
          ParameterInfo[] parameters = method.GetParameters();
          int length1 = args.Length;
          int length2 = parameters.Length;
          if (length2 == length1)
          {
            if (length2 == 0)
              return LateBinding.FastCall(o, method, parameters, args, objType, correctIreflect);
            if (CopyBack == null)
            {
              if (LateBinding.NoByrefs(parameters))
              {
                ParameterInfo parameterInfo = parameters[checked (length2 - 1)];
                if (!parameterInfo.ParameterType.IsArray)
                  return LateBinding.FastCall(o, method, parameters, args, objType, correctIreflect);
                object[] customAttributes = parameterInfo.GetCustomAttributes(typeof (ParamArrayAttribute), false);
                if (customAttributes != null)
                {
                  if (customAttributes.Length != 0)
                    goto label_31;
                }
                return LateBinding.FastCall(o, method, parameters, args, objType, correctIreflect);
              }
            }
          }
        }
      }
label_31:
      try
      {
        return vbBinder.InvokeMember(name, bindingFlags, objType, correctIreflect, o, args, paramnames);
      }
      catch (MissingMemberException ex)
      {
        throw;
      }
      catch (Exception ex) when (LateBinding.IsMissingMemberException(ex))
      {
        throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType, o)));
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    private static bool NoByrefs(ParameterInfo[] parameters)
    {
      int num1 = 0;
      int num2 = checked (parameters.Length - 1);
      int index = num1;
      while (index <= num2)
      {
        if (parameters[index].ParameterType.IsByRef)
          return false;
        checked { ++index; }
      }
      return true;
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    private static object FastCall(object o, MethodBase method, ParameterInfo[] Parameters, object[] args, Type objType, IReflect objIReflect)
    {
      int num = 0;
      int upperBound = args.GetUpperBound(0);
      int index = num;
      while (index <= upperBound)
      {
        ParameterInfo parameter = Parameters[index];
        object defaultValue = args[index];
        if (defaultValue is Missing && parameter.IsOptional)
          defaultValue = parameter.DefaultValue;
        args[index] = ObjectType.CTypeHelper(defaultValue, parameter.ParameterType);
        checked { ++index; }
      }
      VBBinder.SecurityCheckForLateboundCalls((MemberInfo) method, objType, objIReflect);
      if (objType != objIReflect && !method.IsStatic && !LateBinding.DoesTargetObjectMatch(o, (MemberInfo) method))
        return LateBinding.InvokeMemberOnIReflect(objIReflect, (MemberInfo) method, BindingFlags.InvokeMethod, o, args);
      LateBinding.VerifyObjRefPresentForInstanceCall(o, (MemberInfo) method);
      return method.Invoke(o, args);
    }

    private static MemberInfo[] GetMembersByName(IReflect objIReflect, string name, BindingFlags flags)
    {
      MemberInfo[] nonGenericMembers = LateBinding.GetNonGenericMembers(objIReflect.GetMember(name, flags));
      if (nonGenericMembers != null && nonGenericMembers.Length == 0)
        return (MemberInfo[]) null;
      return nonGenericMembers;
    }

    private static bool MemberIsField(MemberInfo[] mi)
    {
      int num1 = 0;
      int upperBound1 = mi.GetUpperBound(0);
      int index1 = num1;
      while (index1 <= upperBound1)
      {
        MemberInfo memberInfo = mi[index1];
        if (memberInfo != null && memberInfo.MemberType == MemberTypes.Field)
        {
          int num2 = 0;
          int upperBound2 = mi.GetUpperBound(0);
          int index2 = num2;
          while (index2 <= upperBound2)
          {
            if (index1 != index2 && mi[index2] != null && memberInfo.DeclaringType.IsSubclassOf(mi[index2].DeclaringType))
              mi[index2] = (MemberInfo) null;
            checked { ++index2; }
          }
        }
        checked { ++index1; }
      }
      MemberInfo[] memberInfoArray = mi;
      int index3 = 0;
      while (index3 < memberInfoArray.Length)
      {
        MemberInfo memberInfo = memberInfoArray[index3];
        if (memberInfo != null && memberInfo.MemberType != MemberTypes.Field)
          return false;
        checked { ++index3; }
      }
      return true;
    }

    internal static bool DoesTargetObjectMatch(object Value, MemberInfo Member)
    {
      return Value == null || Member.DeclaringType.IsAssignableFrom(Value.GetType());
    }

    internal static object InvokeMemberOnIReflect(IReflect objIReflect, MemberInfo member, BindingFlags flags, object target, object[] args)
    {
      VBBinder vbBinder = new VBBinder((bool[]) null);
      vbBinder.CacheMember(member);
      return objIReflect.InvokeMember(member.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding | flags, (Binder) vbBinder, target, args, (ParameterModifier[]) null, (CultureInfo) null, (string[]) null);
    }

    
    private static IReflect GetCorrectIReflect(object o, Type objType)
    {
      if (o != null && !objType.IsCOMObject && (/*!RemotingServices.IsTransparentProxy(o) && */  !(o is Type)))
      {
        IReflect reflect = o as IReflect;
        if (reflect != null)
          return reflect;
      }
      return (IReflect) objType;
    }

    internal static void VerifyObjRefPresentForInstanceCall(object Value, MemberInfo Member)
    {
      if (Value != null)
        return;
      bool flag = true;
      switch (Member.MemberType)
      {
        case MemberTypes.Constructor:
          flag = ((MethodBase) Member).IsStatic;
          break;
        case MemberTypes.Field:
          flag = ((FieldInfo) Member).IsStatic;
          break;
        case MemberTypes.Method:
          flag = ((MethodBase) Member).IsStatic;
          break;
      }
      if (!flag)
        throw new NullReferenceException(Utils.GetResourceString("NullReference_InstanceReqToAccessMember1", new string[1]
        {
          Utils.MemberToString(Member)
        }));
    }

    internal static MemberInfo[] GetNonGenericMembers(MemberInfo[] Members)
    {
      if (Members != null && Members.Length > 0)
      {
        int num1 = 0;
        int num2 = 0;
        int upperBound1 = Members.GetUpperBound(0);
        int index1 = num2;
        while (index1 <= upperBound1)
        {
          if (LateBinding.LegacyIsGeneric(Members[index1]))
            Members[index1] = (MemberInfo) null;
          else
            checked { ++num1; }
          checked { ++index1; }
        }
        if (num1 == checked (Members.GetUpperBound(0) + 1))
          return Members;
        if (num1 > 0)
        {
          MemberInfo[] memberInfoArray = new MemberInfo[checked (num1 - 1 + 1)];
          int index2 = 0;
          int num3 = 0;
          int upperBound2 = Members.GetUpperBound(0);
          int index3 = num3;
          while (index3 <= upperBound2)
          {
            if (Members[index3] != null)
            {
              memberInfoArray[index2] = Members[index3];
              checked { ++index2; }
            }
            checked { ++index3; }
          }
          return memberInfoArray;
        }
      }
      return (MemberInfo[]) null;
    }

    internal static bool LegacyIsGeneric(MemberInfo Member)
    {
      MethodBase methodBase = Member as MethodBase;
      if (methodBase == null)
        return false;
      return methodBase.IsGenericMethod;
    }
  }
}

