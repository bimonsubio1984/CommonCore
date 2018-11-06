// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.NewLateBinding
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class provides helpers that the Visual Basic compiler uses for late binding calls; it is not meant to be called directly from your code.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class NewLateBinding
  {
    private NewLateBinding()
    {
    }

    /// <summary>Indicates whether a call requires late-bound evaluation. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="instance">An instance of the call object exposing the property or method.</param>
    /// <param name="type">The type of the call object.</param>
    /// <param name="memberName">The name of the property or method on the call object.</param>
    /// <param name="arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="allowFunctionEvaluation">A <see langword="Boolean" /> value that specifies whether to allow function evaluation.</param>
    /// <param name="allowPropertyEvaluation">A <see langword="Boolean" /> value that specifies whether to allow property evaluation.</param>
    /// <returns>A <see langword="Boolean" /> value that indicates whether the expression requires late-bound evaluation.</returns>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public static bool LateCanEvaluate(object instance, Type type, string memberName, object[] arguments, bool allowFunctionEvaluation, bool allowPropertyEvaluation)
    {
      Symbols.Container container = type == null ? new Symbols.Container(instance) : new Symbols.Container(type);
      MemberInfo[] members = container.GetMembers(ref memberName, false);
      if (members.Length == 0)
        return true;
      if (members[0].MemberType == MemberTypes.Field)
      {
        if (arguments.Length == 0 || new Symbols.Container(container.GetFieldValue((FieldInfo) members[0])).IsArray)
          return true;
        return allowPropertyEvaluation;
      }
      if (members[0].MemberType == MemberTypes.Method)
        return allowFunctionEvaluation;
      if (members[0].MemberType == MemberTypes.Property)
        return allowPropertyEvaluation;
      return true;
    }

    /// <summary>Executes a late-bound method or function call. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Instance">An instance of the call object exposing the property or method.</param>
    /// <param name="Type">The type of the call object.</param>
    /// <param name="MemberName">The name of the property or method on the call object.</param>
    /// <param name="Arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="ArgumentNames">An array of argument names.</param>
    /// <param name="TypeArguments">An array of argument types; used only for generic calls to pass argument types.</param>
    /// <param name="CopyBack">An array of <see langword="Boolean" /> values that the late binder uses to communicate back to the call site which arguments match <see langword="ByRef" /> parameters. Each <see langword="True" /> value indicates that the arguments matched and should be copied out after the call to <see langword="LateCall" /> is complete.</param>
    /// <param name="IgnoreReturn">A <see langword="Boolean" /> value indicating whether or not the return value can be ignored.</param>
    /// <returns>An instance of the call object.</returns>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static object LateCall(object Instance, Type Type, string MemberName, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, bool[] CopyBack, bool IgnoreReturn)
    {
      if (Arguments == null)
        Arguments = Symbols.NoArguments;
      if (ArgumentNames == null)
        ArgumentNames = Symbols.NoArgumentNames;
      if (TypeArguments == null)
        TypeArguments = Symbols.NoTypeArguments;
      Symbols.Container BaseReference = Type == null ? new Symbols.Container(Instance) : new Symbols.Container(Type);
      if (BaseReference.IsCOMObject)
        return LateBinding.InternalLateCall(Instance, Type, MemberName, Arguments, ArgumentNames, CopyBack, IgnoreReturn);
      BindingFlags InvocationFlags = BindingFlags.InvokeMethod | BindingFlags.GetProperty;
      if (IgnoreReturn)
        InvocationFlags |= BindingFlags.IgnoreReturn;
      OverloadResolution.ResolutionFailure Failure= OverloadResolution.ResolutionFailure.None;
      return NewLateBinding.CallMethod(BaseReference, MemberName, Arguments, ArgumentNames, TypeArguments, CopyBack, InvocationFlags, true, ref Failure);
    }

    /// <summary>Executes a late-bound property get or field access call. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Instance">An instance of the call object exposing the property or method.</param>
    /// <param name="Arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="ArgumentNames">An array of argument names.</param>
    /// <returns>An instance of the call object.</returns>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static object LateIndexGet(object Instance, object[] Arguments, string[] ArgumentNames)
    {
      OverloadResolution.ResolutionFailure Failure= OverloadResolution.ResolutionFailure.None;
      return NewLateBinding.InternalLateIndexGet(Instance, Arguments, ArgumentNames, true, ref Failure);
    }

    internal static object InternalLateIndexGet(object Instance, object[] Arguments, string[] ArgumentNames, bool ReportErrors, ref OverloadResolution.ResolutionFailure Failure)
    {
      Failure = OverloadResolution.ResolutionFailure.None;
      if (Arguments == null)
        Arguments = Symbols.NoArguments;
      if (ArgumentNames == null)
        ArgumentNames = Symbols.NoArgumentNames;
      Symbols.Container BaseReference = new Symbols.Container(Instance);
      if (BaseReference.IsCOMObject)
        return LateBinding.LateIndexGet(Instance, Arguments, ArgumentNames);
      if (!BaseReference.IsArray)
        return NewLateBinding.CallMethod(BaseReference, "", Arguments, ArgumentNames, Symbols.NoTypeArguments, (bool[]) null, BindingFlags.InvokeMethod | BindingFlags.GetProperty, ReportErrors, ref Failure);
      if (ArgumentNames.Length <= 0)
        return BaseReference.GetArrayValue(Arguments);
      Failure = OverloadResolution.ResolutionFailure.InvalidArgument;
      if (ReportErrors)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNamedArgs"));
      return (object) null;
    }

    /// <summary>Executes a late-bound property get or field access call. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Instance">An instance of the call object exposing the property or method.</param>
    /// <param name="Type">The type of the call object.</param>
    /// <param name="MemberName">The name of the property or method on the call object.</param>
    /// <param name="Arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="ArgumentNames">An array of argument names.</param>
    /// <param name="TypeArguments">An array of argument types; used only for generic calls to pass argument types.</param>
    /// <param name="CopyBack">An array of <see langword="Boolean" /> values that the late binder uses to communicate back to the call site which arguments match <see langword="ByRef" /> parameters. Each <see langword="True" /> value indicates that the arguments matched and should be copied out after the call to <see langword="LateCall" /> is complete.</param>
    /// <returns>An instance of the call object.</returns>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static object LateGet(object Instance, Type Type, string MemberName, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, bool[] CopyBack)
    {
      if (Arguments == null)
        Arguments = Symbols.NoArguments;
      if (ArgumentNames == null)
        ArgumentNames = Symbols.NoArgumentNames;
      if (TypeArguments == null)
        TypeArguments = Symbols.NoTypeArguments;
      Symbols.Container BaseReference = Type == null ? new Symbols.Container(Instance) : new Symbols.Container(Type);
      BindingFlags bindingFlags = BindingFlags.InvokeMethod | BindingFlags.GetProperty;
      if (BaseReference.IsCOMObject)
        return LateBinding.LateGet(Instance, Type, MemberName, Arguments, ArgumentNames, CopyBack);
      MemberInfo[] members = BaseReference.GetMembers(ref MemberName, true);
      if (members[0].MemberType == MemberTypes.Field)
      {
        if (TypeArguments.Length > 0)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
        object fieldValue = BaseReference.GetFieldValue((FieldInfo) members[0]);
        if (Arguments.Length == 0)
          return fieldValue;
        return NewLateBinding.LateIndexGet(fieldValue, Arguments, ArgumentNames);
      }
      if (ArgumentNames.Length > Arguments.Length || CopyBack != null && CopyBack.Length != Arguments.Length)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
      OverloadResolution.ResolutionFailure Failure= OverloadResolution.ResolutionFailure.None;
      Symbols.Method TargetProcedure1 = NewLateBinding.ResolveCall(BaseReference, MemberName, members, Arguments, ArgumentNames, TypeArguments, bindingFlags, false, ref Failure);
      if (Failure == OverloadResolution.ResolutionFailure.None)
        return BaseReference.InvokeMethod(TargetProcedure1, Arguments, CopyBack, bindingFlags);
      if (Arguments.Length > 0)
      {
        Symbols.Method TargetProcedure2 = NewLateBinding.ResolveCall(BaseReference, MemberName, members, Symbols.NoArguments, Symbols.NoArgumentNames, TypeArguments, bindingFlags, false, ref Failure);
        if (Failure == OverloadResolution.ResolutionFailure.None)
        {
          object Instance1 = BaseReference.InvokeMethod(TargetProcedure2, Symbols.NoArguments, (bool[]) null, bindingFlags);
          if (Instance1 == null)
            throw new MissingMemberException(Utils.GetResourceString("IntermediateLateBoundNothingResult1", TargetProcedure2.ToString(), BaseReference.VBFriendlyName));
          object obj = NewLateBinding.InternalLateIndexGet(Instance1, Arguments, ArgumentNames, false, ref Failure);
          if (Failure == OverloadResolution.ResolutionFailure.None)
            return obj;
        }
      }
      NewLateBinding.ResolveCall(BaseReference, MemberName, members, Arguments, ArgumentNames, TypeArguments, bindingFlags, true, ref Failure);
      throw new InternalErrorException();
    }

    /// <summary>Executes a late-bound property set or field write call. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Instance">An instance of the call object exposing the property or method.</param>
    /// <param name="Arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="ArgumentNames">An array of argument names.</param>
    /// <param name="OptimisticSet">A <see langword="Boolean" /> value used to determine whether the set operation will work. Set to <see langword="True" /> when you believe that an intermediate value has been set in the property or field; otherwise <see langword="False" />.</param>
    /// <param name="RValueBase">A <see langword="Boolean" /> value that specifies when the base reference of the late reference is an <see langword="RValue" />. Set to <see langword="True" /> when the base reference of the late reference is an <see langword="RValue" />; this allows you to generate a run-time exception for late assignments to fields of <see langword="RValues" /> of value types. Otherwise, set to <see langword="False" />.</param>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static void LateIndexSetComplex(object Instance, object[] Arguments, string[] ArgumentNames, bool OptimisticSet, bool RValueBase)
    {
      if (Arguments == null)
        Arguments = Symbols.NoArguments;
      if (ArgumentNames == null)
        ArgumentNames = Symbols.NoArgumentNames;
      Symbols.Container BaseReference = new Symbols.Container(Instance);
      if (BaseReference.IsArray)
      {
        if (ArgumentNames.Length > 0)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNamedArgs"));
        BaseReference.SetArrayValue(Arguments);
      }
      else
      {
        if (ArgumentNames.Length > Arguments.Length)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
        if (Arguments.Length < 1)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
        string MemberName = "";
        if (BaseReference.IsCOMObject)
        {
          LateBinding.LateIndexSetComplex(Instance, Arguments, ArgumentNames, OptimisticSet, RValueBase);
        }
        else
        {
          BindingFlags bindingFlags = BindingFlags.SetProperty;
          MemberInfo[] members = BaseReference.GetMembers(ref MemberName, true);
          OverloadResolution.ResolutionFailure Failure= OverloadResolution.ResolutionFailure.None;
          Symbols.Method TargetProcedure = NewLateBinding.ResolveCall(BaseReference, MemberName, members, Arguments, ArgumentNames, Symbols.NoTypeArguments, bindingFlags, false, ref Failure);
          if (Failure == OverloadResolution.ResolutionFailure.None)
          {
            if (RValueBase && BaseReference.IsValueType)
              throw new Exception(Utils.GetResourceString("RValueBaseForValueType", BaseReference.VBFriendlyName, BaseReference.VBFriendlyName));
            BaseReference.InvokeMethod(TargetProcedure, Arguments, (bool[]) null, bindingFlags);
          }
          else if (!OptimisticSet)
          {
            NewLateBinding.ResolveCall(BaseReference, MemberName, members, Arguments, ArgumentNames, Symbols.NoTypeArguments, bindingFlags, true, ref Failure);
            throw new InternalErrorException();
          }
        }
      }
    }

    /// <summary>Executes a late-bound property set or field write call. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Instance">An instance of the call object exposing the property or method.</param>
    /// <param name="Arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="ArgumentNames">An array of argument names.</param>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public static void LateIndexSet(object Instance, object[] Arguments, string[] ArgumentNames)
    {
      NewLateBinding.LateIndexSetComplex(Instance, Arguments, ArgumentNames, false, false);
    }

    /// <summary>Executes a late-bound property set or field write call. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Instance">An instance of the call object exposing the property or method.</param>
    /// <param name="Type">The type of the call object.</param>
    /// <param name="MemberName">The name of the property or method on the call object.</param>
    /// <param name="Arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="ArgumentNames">An array of argument names.</param>
    /// <param name="TypeArguments">An array of argument types; used only for generic calls to pass argument types.</param>
    /// <param name="OptimisticSet">A <see langword="Boolean" /> value used to determine whether the set operation will work. Set to <see langword="True" /> when you believe that an intermediate value has been set in the property or field; otherwise <see langword="False" />.</param>
    /// <param name="RValueBase">A <see langword="Boolean" /> value that specifies when the base reference of the late reference is an <see langword="RValue" />. Set to <see langword="True" /> when the base reference of the late reference is an <see langword="RValue" />; this allows you to generate a run-time exception for late assignments to fields of <see langword="RValues" /> of value types. Otherwise, set to <see langword="False" />.</param>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static void LateSetComplex(object Instance, Type Type, string MemberName, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, bool OptimisticSet, bool RValueBase)
    {
      NewLateBinding.LateSet(Instance, Type, MemberName, Arguments, ArgumentNames, TypeArguments, OptimisticSet, RValueBase, (CallType) 0);
    }

    /// <summary>Executes a late-bound property set or field write call. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Instance">An instance of the call object exposing the property or method.</param>
    /// <param name="Type">The type of the call object.</param>
    /// <param name="MemberName">The name of the property or method on the call object.</param>
    /// <param name="Arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="ArgumentNames">An array of argument names.</param>
    /// <param name="TypeArguments">An array of argument types; used only for generic calls to pass argument types.</param>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static void LateSet(object Instance, Type Type, string MemberName, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments)
    {
      NewLateBinding.LateSet(Instance, Type, MemberName, Arguments, ArgumentNames, TypeArguments, false, false, (CallType) 0);
    }

    /// <summary>Executes a late-bound property set or field write call. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Instance">An instance of the call object exposing the property or method.</param>
    /// <param name="Type">The type of the call object.</param>
    /// <param name="MemberName">The name of the property or method on the call object.</param>
    /// <param name="Arguments">An array containing the arguments to be passed to the property or method being called.</param>
    /// <param name="ArgumentNames">An array of argument names.</param>
    /// <param name="TypeArguments">An array of argument types; used only for generic calls to pass argument types.</param>
    /// <param name="OptimisticSet">A <see langword="Boolean" /> value used to determine whether the set operation will work. Set to <see langword="True" /> when you believe that an intermediate value has been set in the property or field; otherwise <see langword="False" />.</param>
    /// <param name="RValueBase">A <see langword="Boolean" /> value that specifies when the base reference of the late reference is an <see langword="RValue" />. Set to <see langword="True" /> when the base reference of the late reference is an <see langword="RValue" />; this allows you to generate a run-time exception for late assignments to fields of <see langword="RValues" /> of value types. Otherwise, set to <see langword="False" />.</param>
    /// <param name="CallType">An enumeration member of type <see cref="T:Ported.VisualBasic.CallType" /> representing the type of procedure being called. The value of CallType can be <see langword="Method" />, <see langword="Get" />, or <see langword="Set" />. Only <see langword="Set" /> is used.</param>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public static void LateSet(object Instance, Type Type, string MemberName, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, bool OptimisticSet, bool RValueBase, CallType CallType)
    {
      if (Arguments == null)
        Arguments = Symbols.NoArguments;
      if (ArgumentNames == null)
        ArgumentNames = Symbols.NoArgumentNames;
      if (TypeArguments == null)
        TypeArguments = Symbols.NoTypeArguments;
      Symbols.Container BaseReference = Type == null ? new Symbols.Container(Instance) : new Symbols.Container(Type);
      if (BaseReference.IsCOMObject)
      {
        try
        {
          LateBinding.InternalLateSet(Instance, ref Type, MemberName, Arguments, ArgumentNames, OptimisticSet, CallType);
          if (RValueBase && Type.IsValueType)
            throw new Exception(Utils.GetResourceString("RValueBaseForValueType", Utils.VBFriendlyName(Type, Instance), Utils.VBFriendlyName(Type, Instance)));
        }
        catch (MissingMemberException ex) when (OptimisticSet)
        {
        }
      }
      else
      {
        MemberInfo[] members = BaseReference.GetMembers(ref MemberName, true);
        if (members[0].MemberType == MemberTypes.Field)
        {
          if (TypeArguments.Length > 0)
            throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
          if (Arguments.Length == 1)
          {
            if (RValueBase && BaseReference.IsValueType)
              throw new Exception(Utils.GetResourceString("RValueBaseForValueType", BaseReference.VBFriendlyName, BaseReference.VBFriendlyName));
            BaseReference.SetFieldValue((FieldInfo) members[0], Arguments[0]);
          }
          else
            NewLateBinding.LateIndexSetComplex(BaseReference.GetFieldValue((FieldInfo) members[0]), Arguments, ArgumentNames, OptimisticSet, true);
        }
        else
        {
          BindingFlags bindingFlags1 = BindingFlags.SetProperty;
          if (ArgumentNames.Length > Arguments.Length)
            throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
          OverloadResolution.ResolutionFailure Failure= OverloadResolution.ResolutionFailure.None;
          if (TypeArguments.Length == 0)
          {
            Symbols.Method TargetProcedure = NewLateBinding.ResolveCall(BaseReference, MemberName, members, Arguments, ArgumentNames, Symbols.NoTypeArguments, bindingFlags1, false, ref Failure);
            if (Failure == OverloadResolution.ResolutionFailure.None)
            {
              if (RValueBase && BaseReference.IsValueType)
                throw new Exception(Utils.GetResourceString("RValueBaseForValueType", BaseReference.VBFriendlyName, BaseReference.VBFriendlyName));
              BaseReference.InvokeMethod(TargetProcedure, Arguments, (bool[]) null, bindingFlags1);
              return;
            }
          }
          BindingFlags bindingFlags2 = BindingFlags.InvokeMethod | BindingFlags.GetProperty;
          if (Failure == OverloadResolution.ResolutionFailure.None || Failure == OverloadResolution.ResolutionFailure.MissingMember)
          {
            Symbols.Method TargetProcedure = NewLateBinding.ResolveCall(BaseReference, MemberName, members, Symbols.NoArguments, Symbols.NoArgumentNames, TypeArguments, bindingFlags2, false, ref Failure);
            if (Failure == OverloadResolution.ResolutionFailure.None)
            {
              object Instance1 = BaseReference.InvokeMethod(TargetProcedure, Symbols.NoArguments, (bool[]) null, bindingFlags2);
              if (Instance1 == null)
                throw new MissingMemberException(Utils.GetResourceString("IntermediateLateBoundNothingResult1", TargetProcedure.ToString(), BaseReference.VBFriendlyName));
              NewLateBinding.LateIndexSetComplex(Instance1, Arguments, ArgumentNames, OptimisticSet, true);
              return;
            }
          }
          if (!OptimisticSet)
          {
            if (TypeArguments.Length == 0)
              NewLateBinding.ResolveCall(BaseReference, MemberName, members, Arguments, ArgumentNames, TypeArguments, bindingFlags1, true, ref Failure);
            else
              NewLateBinding.ResolveCall(BaseReference, MemberName, members, Symbols.NoArguments, Symbols.NoArgumentNames, TypeArguments, bindingFlags2, true, ref Failure);
            throw new InternalErrorException();
          }
        }
      }
    }

    private static object CallMethod(Symbols.Container BaseReference, string MethodName, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, bool[] CopyBack, BindingFlags InvocationFlags, bool ReportErrors, ref OverloadResolution.ResolutionFailure Failure)
    {
      Failure = OverloadResolution.ResolutionFailure.None;
      if (ArgumentNames.Length > Arguments.Length || CopyBack != null && CopyBack.Length != Arguments.Length)
      {
        Failure = OverloadResolution.ResolutionFailure.InvalidArgument;
        if (ReportErrors)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
        return (object) null;
      }
      if (Symbols.HasFlag(InvocationFlags, BindingFlags.SetProperty) && Arguments.Length < 1)
      {
        Failure = OverloadResolution.ResolutionFailure.InvalidArgument;
        if (ReportErrors)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
        return (object) null;
      }
      MemberInfo[] members = BaseReference.GetMembers(ref MethodName, ReportErrors);
      if (members == null || members.Length == 0)
      {
        Failure = OverloadResolution.ResolutionFailure.MissingMember;
        if (ReportErrors)
          BaseReference.GetMembers(ref MethodName, true);
        return (object) null;
      }
      Symbols.Method TargetProcedure = NewLateBinding.ResolveCall(BaseReference, MethodName, members, Arguments, ArgumentNames, TypeArguments, InvocationFlags, ReportErrors, ref Failure);
      if (Failure == OverloadResolution.ResolutionFailure.None)
        return BaseReference.InvokeMethod(TargetProcedure, Arguments, CopyBack, InvocationFlags);
      return (object) null;
    }

    internal static MethodInfo MatchesPropertyRequirements(Symbols.Method TargetProcedure, BindingFlags Flags)
    {
      if (Symbols.HasFlag(Flags, BindingFlags.SetProperty))
        return TargetProcedure.AsProperty().GetSetMethod();
      return TargetProcedure.AsProperty().GetGetMethod();
    }

    internal static Exception ReportPropertyMismatch(Symbols.Method TargetProcedure, BindingFlags Flags)
    {
      if (Symbols.HasFlag(Flags, BindingFlags.SetProperty))
        return (Exception) new MissingMemberException(Utils.GetResourceString("NoSetProperty1", new string[1]
        {
          TargetProcedure.AsProperty().Name
        }));
      return (Exception) new MissingMemberException(Utils.GetResourceString("NoGetProperty1", new string[1]
      {
        TargetProcedure.AsProperty().Name
      }));
    }

    internal static Symbols.Method ResolveCall(Symbols.Container BaseReference, string MethodName, MemberInfo[] Members, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, BindingFlags LookupFlags, bool ReportErrors, ref OverloadResolution.ResolutionFailure Failure)
    {
      Failure = OverloadResolution.ResolutionFailure.None;
      if (Members[0].MemberType != MemberTypes.Method && Members[0].MemberType != MemberTypes.Property)
      {
        Failure = OverloadResolution.ResolutionFailure.InvalidTarget;
        if (ReportErrors)
          throw new ArgumentException(Utils.GetResourceString("ExpressionNotProcedure", MethodName, BaseReference.VBFriendlyName));
        return (Symbols.Method) null;
      }
      int length = Arguments.Length;
      object obj1 = (object) null;
      if (Symbols.HasFlag(LookupFlags, BindingFlags.SetProperty))
      {
        if (Arguments.Length == 0)
        {
          Failure = OverloadResolution.ResolutionFailure.InvalidArgument;
          if (ReportErrors)
            throw new InvalidCastException(Utils.GetResourceString("PropertySetMissingArgument1", new string[1]
            {
              MethodName
            }));
          return (Symbols.Method) null;
        }
        object[] objArray = Arguments;
        Arguments = new object[checked (length - 2 + 1)];
        Array.Copy((Array) objArray, (Array) Arguments, Arguments.Length);
        obj1 = objArray[checked (length - 1)];
      }
      Symbols.Method TargetProcedure1 = OverloadResolution.ResolveOverloadedCall(MethodName, Members, Arguments, ArgumentNames, TypeArguments, LookupFlags, ReportErrors, ref Failure);
      if (Failure != OverloadResolution.ResolutionFailure.None)
        return (Symbols.Method) null;
      if (!TargetProcedure1.ArgumentsValidated && !OverloadResolution.CanMatchArguments(TargetProcedure1, Arguments, ArgumentNames, TypeArguments, false, (List<string>) null))
      {
        Failure = OverloadResolution.ResolutionFailure.InvalidArgument;
        if (ReportErrors)
        {
          string str = "";
          List<string> Errors = new List<string>();
          OverloadResolution.CanMatchArguments(TargetProcedure1, Arguments, ArgumentNames, TypeArguments, false, Errors);
          List<string>.Enumerator enumerator = Ported.VisualBasic.CompilerServices.OverloadResolution.InitEnumerator<string>(); 
          try
          {
            enumerator = Errors.GetEnumerator();
            while (enumerator.MoveNext())
            {
              string current = enumerator.Current;
              str = str + "\r\n    " + current;
            }
          }
          finally
          {
            enumerator.Dispose();
          }
          throw new InvalidCastException(Utils.GetResourceString("MatchArgumentFailure2", TargetProcedure1.ToString(), str));
        }
        return (Symbols.Method) null;
      }
      if (TargetProcedure1.IsProperty)
      {
        if (NewLateBinding.MatchesPropertyRequirements(TargetProcedure1, LookupFlags) == null)
        {
          Failure = OverloadResolution.ResolutionFailure.InvalidTarget;
          if (ReportErrors)
            throw NewLateBinding.ReportPropertyMismatch(TargetProcedure1, LookupFlags);
          return (Symbols.Method) null;
        }
      }
      else if (Symbols.HasFlag(LookupFlags, BindingFlags.SetProperty))
      {
        Failure = OverloadResolution.ResolutionFailure.InvalidTarget;
        if (ReportErrors)
          throw new MissingMemberException(Utils.GetResourceString("MethodAssignment1", new string[1]
          {
            TargetProcedure1.AsMethod().Name
          }));
        return (Symbols.Method) null;
      }
      if (Symbols.HasFlag(LookupFlags, BindingFlags.SetProperty))
      {
        ParameterInfo[] parameters = NewLateBinding.GetCallTarget(TargetProcedure1, LookupFlags).GetParameters();
        ParameterInfo parameterInfo = parameters[checked (parameters.Length - 1)];
        Symbols.Method TargetProcedure2 = TargetProcedure1;
        object obj2 = obj1;
        ParameterInfo Parameter1 = parameterInfo;
        int num1 = 0;
        int num2 = 0;
        // ISSUE: variable of the null type
        object local1 = null;
        bool flag1 = false;
        ref bool local2 = ref flag1;
        bool flag2 = false;
        ref bool local3 = ref flag2;
        if (!OverloadResolution.CanPassToParameter(TargetProcedure2, obj2, Parameter1, num1 != 0, num2 != 0, (List<string>) local1, ref local2, ref local3))
        {
          Failure = OverloadResolution.ResolutionFailure.InvalidArgument;
          if (ReportErrors)
          {
            string str = "";
            List<string> stringList = new List<string>();
            Symbols.Method TargetProcedure3 = TargetProcedure1;
            object obj3 = obj1;
            ParameterInfo Parameter2 = parameterInfo;
            int num3 = 0;
            int num4 = 0;
            List<string> Errors = stringList;
            flag2 = false;
            ref bool local4 = ref flag2;
            bool flag3 = false;
            ref bool local5 = ref flag3;
            OverloadResolution.CanPassToParameter(TargetProcedure3, obj3, Parameter2, num3 != 0, num4 != 0, Errors, ref local4, ref local5);
            List<string>.Enumerator enumerator= Ported.VisualBasic.CompilerServices.OverloadResolution.InitEnumerator<string>();
            try
            {
              enumerator = stringList.GetEnumerator();
              while (enumerator.MoveNext())
              {
                string current = enumerator.Current;
                str = str + "\r\n    " + current;
              }
            }
            finally
            {
              enumerator.Dispose();
            }
            throw new InvalidCastException(Utils.GetResourceString("MatchArgumentFailure2", TargetProcedure1.ToString(), str));
          }
          return (Symbols.Method) null;
        }
      }
      return TargetProcedure1;
    }

    internal static MethodBase GetCallTarget(Symbols.Method TargetProcedure, BindingFlags Flags)
    {
      if (TargetProcedure.IsMethod)
        return TargetProcedure.AsMethod();
      if (TargetProcedure.IsProperty)
        return (MethodBase) NewLateBinding.MatchesPropertyRequirements(TargetProcedure, Flags);
      return (MethodBase) null;
    }

    internal static object[] ConstructCallArguments(Symbols.Method TargetProcedure, object[] Arguments, BindingFlags LookupFlags)
    {
      ParameterInfo[] parameters = NewLateBinding.GetCallTarget(TargetProcedure, LookupFlags).GetParameters();
      object[] MatchedArguments = new object[checked (parameters.Length - 1 + 1)];
      int length = Arguments.Length;
      object obj = (object) null;
      if (Symbols.HasFlag(LookupFlags, BindingFlags.SetProperty))
      {
        object[] objArray = Arguments;
        Arguments = new object[checked (length - 2 + 1)];
        Array.Copy((Array) objArray, (Array) Arguments, Arguments.Length);
        obj = objArray[checked (length - 1)];
      }
      OverloadResolution.MatchArguments(TargetProcedure, Arguments, MatchedArguments);
      if (Symbols.HasFlag(LookupFlags, BindingFlags.SetProperty))
      {
        ParameterInfo Parameter = parameters[checked (parameters.Length - 1)];
        MatchedArguments[checked (parameters.Length - 1)] = OverloadResolution.PassToParameter(obj, Parameter, Parameter.ParameterType);
      }
      return MatchedArguments;
    }
  }
}
