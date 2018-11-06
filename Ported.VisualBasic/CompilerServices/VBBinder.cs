// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.VBBinder
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
//using System.Runtime.Remoting;
using System.Security.Permissions;

namespace Ported.VisualBasic.CompilerServices
{
  internal sealed class VBBinder : Binder
  {
    private const int PARAMARRAY_NONE = -1;
    private const int ARG_MISSING = -1;
    internal string m_BindToName;
    internal Type m_objType;
    private VBBinder.VBBinderState m_state;
    private MemberInfo m_CachedMember;
    private bool[] m_ByRefFlags;

    private void ThrowInvalidCast(Type ArgType, Type ParmType, int ParmIndex)
    {
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromToArg4", this.CalledMethodName(), Conversions.ToString(checked (ParmIndex + 1)), Utils.VBFriendlyName(ArgType), Utils.VBFriendlyName(ParmType)));
    }

    public VBBinder(bool[] CopyBack)
    {
      this.m_ByRefFlags = CopyBack;
    }

    public override void ReorderArgumentArray(ref object[] args, object objState)
    {
      VBBinder.VBBinderState vbBinderState = (VBBinder.VBBinderState) objState;
      if (args != null && vbBinderState != null)
      {
        if (vbBinderState.m_OriginalParamOrder != null)
        {
          if (this.m_ByRefFlags != null)
          {
            if (vbBinderState.m_ByRefFlags == null)
            {
              int num = 0;
              int upperBound = this.m_ByRefFlags.GetUpperBound(0);
              int index = num;
              while (index <= upperBound)
              {
                this.m_ByRefFlags[index] = false;
                checked { ++index; }
              }
            }
            else
            {
              int num = 0;
              int upperBound = vbBinderState.m_OriginalParamOrder.GetUpperBound(0);
              int index1 = num;
              while (index1 <= upperBound)
              {
                int index2 = vbBinderState.m_OriginalParamOrder[index1];
                if (index2 >= 0 && index2 <= args.GetUpperBound(0))
                {
                  this.m_ByRefFlags[index2] = vbBinderState.m_ByRefFlags[index2];
                  vbBinderState.m_OriginalArgs[index2] = args[index1];
                }
                checked { ++index1; }
              }
            }
          }
        }
        else if (this.m_ByRefFlags != null)
        {
          if (vbBinderState.m_ByRefFlags == null)
          {
            int num = 0;
            int upperBound = this.m_ByRefFlags.GetUpperBound(0);
            int index = num;
            while (index <= upperBound)
            {
              this.m_ByRefFlags[index] = false;
              checked { ++index; }
            }
          }
          else
          {
            int num = 0;
            int upperBound = this.m_ByRefFlags.GetUpperBound(0);
            int index = num;
            while (index <= upperBound)
            {
              if (this.m_ByRefFlags[index])
              {
                bool byRefFlag = vbBinderState.m_ByRefFlags[index];
                this.m_ByRefFlags[index] = byRefFlag;
                if (byRefFlag)
                  vbBinderState.m_OriginalArgs[index] = args[index];
              }
              checked { ++index; }
            }
          }
        }
      }
      if (vbBinderState == null)
        return;
      vbBinderState.m_OriginalParamOrder = (int[]) null;
      vbBinderState.m_ByRefFlags = (bool[]) null;
    }

    public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object ObjState)
    {
      ObjState = null;
      Type type1 = (Type) null;
      Type type2 = (Type) null;
      Type elementType1 = (Type) null;
      if (match == null || match.Length == 0)
        throw ExceptionUtils.VbMakeException(438);
      if (this.m_CachedMember != null && this.m_CachedMember.MemberType == MemberTypes.Method && (match[0] != null && Operators.CompareString(match[0].Name, this.m_CachedMember.Name, false) == 0))
        return (MethodBase) this.m_CachedMember;
      bool flag1 = (bindingAttr & BindingFlags.SetProperty) != BindingFlags.Default;
      if (names != null && names.Length == 0)
        names = (string[]) null;
      int length1 = match.Length;
      if (length1 > 1)
      {
        int num1 = 0;
        int upperBound1 = match.GetUpperBound(0);
        int index1 = num1;
        while (index1 <= upperBound1)
        {
          MethodBase methodBase = match[index1];
          if (methodBase != null && !methodBase.IsHideBySig)
          {
            if (methodBase.IsVirtual)
            {
              if ((methodBase.Attributes & MethodAttributes.VtableLayoutMask) != MethodAttributes.PrivateScope)
              {
                int num2 = 0;
                int upperBound2 = match.GetUpperBound(0);
                int index2 = num2;
                while (index2 <= upperBound2)
                {
                  if (index1 != index2 && match[index2] != null && methodBase.DeclaringType.IsSubclassOf(match[index2].DeclaringType))
                  {
                    match[index2] = (MethodBase) null;
                    checked { --length1; }
                  }
                  checked { ++index2; }
                }
              }
            }
            else
            {
              int num2 = 0;
              int upperBound2 = match.GetUpperBound(0);
              int index2 = num2;
              while (index2 <= upperBound2)
              {
                if (index1 != index2 && match[index2] != null && methodBase.DeclaringType.IsSubclassOf(match[index2].DeclaringType))
                {
                  match[index2] = (MethodBase) null;
                  checked { --length1; }
                }
                checked { ++index2; }
              }
            }
          }
          checked { ++index1; }
        }
      }
      int num3 = length1;
      int ParmIndex1=0;
      if (names != null)
      {
        int num1 = 0;
        int upperBound1 = match.GetUpperBound(0);
        int index1 = num1;
        while (index1 <= upperBound1)
        {
          MethodBase methodBase = match[index1];
          if (methodBase != null)
          {
            ParameterInfo[] parameters = methodBase.GetParameters();
            int upperBound2 = parameters.GetUpperBound(0);
            if (flag1)
              checked { --upperBound2; }
            int num2=0;
            if (upperBound2 >= 0)
            {
              ParameterInfo parameterInfo = parameters[upperBound2];
              num2 = -1;
              if (parameterInfo.ParameterType.IsArray)
              {
                object[] customAttributes = parameterInfo.GetCustomAttributes(typeof (ParamArrayAttribute), false);
                num2 = customAttributes == null || customAttributes.Length <= 0 ? -1 : upperBound2;
              }
            }
            int num4 = 0;
            int upperBound3 = names.GetUpperBound(0);
            int index2 = num4;
            while (index2 <= upperBound3)
            {
              int num5 = 0;
              int num6 = upperBound2;
              ParmIndex1 = num5;
              while (ParmIndex1 <= num6)
              {
                if (Strings.StrComp(names[index2], parameters[ParmIndex1].Name, CompareMethod.Text) == 0)
                {
                  if (ParmIndex1 == num2 && length1 == 1)
                    throw ExceptionUtils.VbMakeExceptionEx(446, Utils.GetResourceString("NamedArgumentOnParamArray"));
                  if (ParmIndex1 == num2)
                  {
                    ParmIndex1 = checked (upperBound2 + 1);
                    break;
                  }
                  break;
                }
                checked { ++ParmIndex1; }
              }
              if (ParmIndex1 > upperBound2)
              {
                if (length1 == 1)
                  throw new MissingMemberException(Utils.GetResourceString("Argument_InvalidNamedArg2", names[index2], this.CalledMethodName()));
                match[index1] = (MethodBase) null;
                checked { --length1; }
                break;
              }
              checked { ++index2; }
            }
          }
          checked { ++index1; }
        }
      }
      int[] numArray1 = new int[checked (match.Length - 1 + 1)];
      int num7 = 0;
      int upperBound4 = match.GetUpperBound(0);
      int index3 = num7;
      while (index3 <= upperBound4)
      {
        MethodBase methodBase = match[index3];
        if (methodBase != null)
        {
          int num1 = -1;
          ParameterInfo[] parameters = methodBase.GetParameters();
          int upperBound1 = parameters.GetUpperBound(0);
          if (flag1)
            checked { --upperBound1; }
          if (upperBound1 >= 0)
          {
            ParameterInfo parameterInfo = parameters[upperBound1];
            if (parameterInfo.ParameterType.IsArray)
            {
              object[] customAttributes = parameterInfo.GetCustomAttributes(typeof (ParamArrayAttribute), false);
              if (customAttributes != null && customAttributes.Length > 0)
                num1 = upperBound1;
            }
          }
          numArray1[index3] = num1;
          if (num1 == -1 && args.Length > parameters.Length)
          {
            if (length1 == 1)
              throw new MissingMemberException(Utils.GetResourceString("NoMethodTakingXArguments2", this.CalledMethodName(), Conversions.ToString(this.GetPropArgCount(args, flag1))));
            match[index3] = (MethodBase) null;
            checked { --length1; }
          }
          int num2 = upperBound1;
          if (num1 != -1)
            checked { --num2; }
          if (args.Length < num2)
          {
            int length2 = args.Length;
            int num4 = checked (num2 - 1);
            int index1 = length2;
            while (index1 <= num4 && parameters[index1].DefaultValue != DBNull.Value)
              checked { ++index1; }
            if (index1 != num2)
            {
              if (length1 == 1)
                throw new MissingMemberException(Utils.GetResourceString("NoMethodTakingXArguments2", this.CalledMethodName(), Conversions.ToString(this.GetPropArgCount(args, flag1))));
              match[index3] = (MethodBase) null;
              checked { --length1; }
            }
          }
        }
        checked { ++index3; }
      }
      object[] ParamOrder = new object[checked (match.Length - 1 + 1)];
      int num8 = 0;
      int upperBound5 = match.GetUpperBound(0);
      int index4 = num8;
      while (index4 <= upperBound5)
      {
        MethodBase methodBase = match[index4];
        if (methodBase != null)
        {
          ParameterInfo[] parameters = methodBase.GetParameters();
          int[] paramOrder1 = args.Length <= parameters.Length ? new int[checked (parameters.Length - 1 + 1)] : new int[checked (args.Length - 1 + 1)];
          ParamOrder[index4] = (object) paramOrder1;
          if (names == null)
          {
            int upperBound1 = args.GetUpperBound(0);
            if (flag1)
              checked { --upperBound1; }
            int num1 = 0;
            int num2 = upperBound1;
            int index1 = num1;
            while (index1 <= num2)
            {
              paramOrder1[index1] = !(args[index1] is Missing) || index1 <= parameters.GetUpperBound(0) && !parameters[index1].IsOptional ? index1 : -1;
              checked { ++index1; }
            }
            int upperBound2 = paramOrder1.GetUpperBound(0);
            int num4 = index1;
            int num5 = upperBound2;
            int index2 = num4;
            while (index2 <= num5)
            {
              paramOrder1[index2] = -1;
              checked { ++index2; }
            }
            if (flag1)
              paramOrder1[upperBound2] = args.GetUpperBound(0);
          }
          else
          {
            Exception paramOrder2 = this.CreateParamOrder(flag1, paramOrder1, methodBase.GetParameters(), args, names);
            if (paramOrder2 != null)
            {
              if (length1 == 1)
                throw paramOrder2;
              match[index4] = (MethodBase) null;
              checked { --length1; }
            }
          }
        }
        checked { ++index4; }
      }
      Type[] ArgTypes = new Type[checked (args.Length - 1 + 1)];
      int num9 = 0;
      int upperBound6 = args.GetUpperBound(0);
      int index5 = num9;
      while (index5 <= upperBound6)
      {
        if (args[index5] != null)
          ArgTypes[index5] = args[index5].GetType();
        checked { ++index5; }
      }
      int num10 = 0;
      int upperBound7 = match.GetUpperBound(0);
      int index6 = num10;
      while (index6 <= upperBound7)
      {
        MethodBase methodBase = match[index6];
        if (methodBase != null)
        {
          ParameterInfo[] parameters = methodBase.GetParameters();
          int[] numArray2 = (int[]) ParamOrder[index6];
          int upperBound1 = numArray2.GetUpperBound(0);
          if (flag1)
            checked { --upperBound1; }
          int index1 = numArray1[index6];
          if (index1 != -1)
            elementType1 = parameters[index1].ParameterType.GetElementType();
          else if (numArray2.Length > parameters.Length)
            goto label_130;
          int num1 = 0;
          int num2 = upperBound1;
          ParmIndex1 = num1;
          while (ParmIndex1 <= num2)
          {
            int index2 = numArray2[ParmIndex1];
            if (index2 == -1)
            {
              if (!parameters[ParmIndex1].IsOptional && ParmIndex1 != numArray1[index6])
              {
                if (length1 == 1)
                  throw new MissingMemberException(Utils.GetResourceString("NoMethodTakingXArguments2", this.CalledMethodName(), Conversions.ToString(this.GetPropArgCount(args, flag1))));
                goto label_130;
              }
            }
            else
            {
              type1 = ArgTypes[index2];
              if (type1 != null)
              {
                if (index1 != -1 && ParmIndex1 > index1)
                {
                  type2 = parameters[index1].ParameterType.GetElementType();
                }
                else
                {
                  type2 = parameters[ParmIndex1].ParameterType;
                  if (type2.IsByRef)
                    type2 = type2.GetElementType();
                  if (ParmIndex1 == index1)
                  {
                    if (!type2.IsInstanceOfType(args[index2]) || ParmIndex1 != upperBound1)
                      type2 = elementType1;
                    else
                      goto label_128;
                  }
                }
                if (type2 != type1 && (type1 != Type.Missing || !parameters[ParmIndex1].IsOptional))
                {
                  if (args[index2] != Missing.Value)
                  {
                    if (type2 != typeof (object) && !type2.IsInstanceOfType(args[index2]))
                    {
                      TypeCode typeCode1 = Type.GetTypeCode(type2);
                      TypeCode typeCode2 = type1 != null ? Type.GetTypeCode(type1) : TypeCode.Empty;
                      switch (typeCode1)
                      {
                        case TypeCode.Boolean:
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                          switch (typeCode2)
                          {
                            case TypeCode.Boolean:
                            case TypeCode.Byte:
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                            case TypeCode.String:
                              break;
                            default:
                              goto label_130;
                          }

                          break;
                        case TypeCode.Char:
                          if (typeCode2 != TypeCode.String)
                            goto label_130;
                          else
                            break;
                        case TypeCode.DateTime:
                          if (typeCode2 != TypeCode.String)
                            goto label_130;
                          else
                            break;
                        case TypeCode.String:
                          switch (typeCode2)
                          {
                            case TypeCode.Empty:
                            case TypeCode.Boolean:
                            case TypeCode.Char:
                            case TypeCode.Byte:
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                            case TypeCode.String:
                              break;
                            default:
                              if (type1 != typeof (char[]))
                                goto label_130;
                              else
                                break;
                          }

                          break;
                        default:
                          if (type2 == typeof (char[]))
                          {
                            switch (typeCode2)
                            {
                              case TypeCode.Object:
                                if (type1 == typeof (char[]))
                                  break;
                                goto label_130;
                              case TypeCode.String:
                                break;
                              default:
                                goto label_130;
                            }
                          }
                          else
                            goto label_130;

                          break;
                      }
                    }
                  }
                  else
                    goto label_130;
                }
              }
            }
label_128:
            checked { ++ParmIndex1; }
          }
          goto label_135;
label_130:
          if (length1 == 1)
          {
            if (num3 == 1)
              this.ThrowInvalidCast(type1, type2, ParmIndex1);
            else
              throw new AmbiguousMatchException(Utils.GetResourceString("AmbiguousMatch_NarrowingConversion1", new string[1]
              {
                this.CalledMethodName()
              }));
          }
          match[index6] = (MethodBase) null;
          checked { --length1; }
        }
label_135:
        checked { ++index6; }
      }
      int index7 = 0;
      int num11 = 0;
      int upperBound8 = match.GetUpperBound(0);
      int index8 = num11;
      while (index8 <= upperBound8)
      {
        MethodBase methodBase = match[index8];
        if (methodBase != null)
        {
          int[] numArray2 = (int[]) ParamOrder[index8];
          ParameterInfo[] parameters = methodBase.GetParameters();
          bool flag2 = false;
          int upperBound1 = parameters.GetUpperBound(0);
          if (flag1)
            checked { --upperBound1; }
          int upperBound2 = args.GetUpperBound(0);
          if (flag1)
          {
            int num1 = checked (upperBound2 - 1);
          }
          int num2 = numArray1[index8];
          if (num2 != -1)
            elementType1 = parameters[upperBound1].ParameterType.GetElementType();
          int num4 = 0;
          int num5 = upperBound1;
          int ParmIndex2 = num4;
          while (ParmIndex2 <= num5)
          {
            Type type3 = ParmIndex2 != num2 ? parameters[ParmIndex2].ParameterType : elementType1;
            if (type3.IsByRef)
            {
              flag2 = true;
              type3 = type3.GetElementType();
            }
            int index1 = numArray2[ParmIndex2];
            if ((index1 != -1 || !parameters[ParmIndex2].IsOptional) && ParmIndex2 != numArray1[index8])
            {
              Type type4 = ArgTypes[index1];
              if (type4 != null && (type4 != Type.Missing || !parameters[ParmIndex2].IsOptional) && (type3 != type4 && type3 != typeof (object)))
              {
                TypeCode typeCode1 = Type.GetTypeCode(type3);
                TypeCode typeCode2 = type4 != null ? Type.GetTypeCode(type4) : TypeCode.Empty;
                switch (typeCode1)
                {
                  case TypeCode.Boolean:
                  case TypeCode.Byte:
                  case TypeCode.Int16:
                  case TypeCode.Int32:
                  case TypeCode.Int64:
                  case TypeCode.Single:
                  case TypeCode.Double:
                  case TypeCode.Decimal:
                    switch (typeCode2)
                    {
                      case TypeCode.Boolean:
                      case TypeCode.Byte:
                      case TypeCode.Int16:
                      case TypeCode.Int32:
                      case TypeCode.Int64:
                      case TypeCode.Single:
                      case TypeCode.Double:
                      case TypeCode.Decimal:
                      case TypeCode.String:
                        break;
                      default:
                        if (index7 == 0)
                        {
                          this.ThrowInvalidCast(type4, type3, ParmIndex2);
                          break;
                        }
                        break;
                    }
                    break;
                }
              }
            }
            checked { ++ParmIndex2; }
          }
          if (ParmIndex2 > upperBound1)
          {
            if (index8 != index7)
            {
              match[index7] = match[index8];
              ParamOrder[index7] = ParamOrder[index8];
              numArray1[index7] = numArray1[index8];
              match[index8] = (MethodBase) null;
            }
            checked { ++index7; }
            if (flag2)
              ;
          }
          else
            match[index8] = (MethodBase) null;
        }
        checked { ++index8; }
      }
      if (index7 == 0)
        throw new MissingMemberException(Utils.GetResourceString("NoMethodTakingXArguments2", this.CalledMethodName(), Conversions.ToString(this.GetPropArgCount(args, flag1))));
      VBBinder.VBBinderState state = new VBBinder.VBBinderState();
      this.m_state = state;
      ObjState = (object) state;
      state.m_OriginalArgs = args;
      int index9;
      if (index7 == 1)
      {
        index9 = 0;
      }
      else
      {
        index9 = 0;
        VBBinder.BindScore bindScore1 = VBBinder.BindScore.Unknown;
        int index1 = 0;
        int num1 = 0;
        int num2 = checked (index7 - 1);
        int index2 = num1;
        while (index2 <= num2)
        {
          MethodBase ThisMethod = match[index2];
          if (ThisMethod != null)
          {
            int[] numArray2 = (int[]) ParamOrder[index2];
            VBBinder.BindScore bindScore2 = this.BindingScore(ThisMethod.GetParameters(), numArray2, ArgTypes, flag1, numArray1[index2]);
            if (bindScore2 < bindScore1)
            {
              if (index2 != 0)
              {
                match[0] = match[index2];
                ParamOrder[0] = ParamOrder[index2];
                numArray1[0] = numArray1[index2];
                match[index2] = (MethodBase) null;
              }
              index1 = 1;
              bindScore1 = bindScore2;
            }
            else if (bindScore2 == bindScore1)
            {
              if (bindScore2 == VBBinder.BindScore.Exact || bindScore2 == VBBinder.BindScore.Widening1)
              {
                switch (this.GetMostSpecific(match[0], ThisMethod, numArray2, ParamOrder, flag1, numArray1[0], numArray1[index2], args))
                {
                  case -1:
                    if (index1 != index2)
                    {
                      match[index1] = match[index2];
                      ParamOrder[index1] = ParamOrder[index2];
                      numArray1[index1] = numArray1[index2];
                      match[index2] = (MethodBase) null;
                    }
                    checked { ++index1; }
                    break;
                  case 0:
                    break;
                  default:
                    bool flag2 = true;
                    int num4 = 1;
                    int num5 = checked (index1 - 1);
                    int index10 = num4;
                    while (index10 <= num5)
                    {
                      if (this.GetMostSpecific(match[index10], ThisMethod, numArray2, ParamOrder, flag1, numArray1[index10], numArray1[index2], args) != 1)
                      {
                        flag2 = false;
                        break;
                      }
                      checked { ++index10; }
                    }
                    if (flag2)
                      index1 = 0;
                    if (index2 != index1)
                    {
                      match[index1] = match[index2];
                      ParamOrder[index1] = ParamOrder[index2];
                      numArray1[index1] = numArray1[index2];
                      match[index2] = (MethodBase) null;
                    }
                    checked { ++index1; }
                    break;
                }
              }
              else
              {
                if (index1 != index2)
                {
                  match[index1] = match[index2];
                  ParamOrder[index1] = ParamOrder[index2];
                  numArray1[index1] = numArray1[index2];
                  match[index2] = (MethodBase) null;
                }
                checked { ++index1; }
              }
            }
            else
              match[index2] = (MethodBase) null;
          }
          checked { ++index2; }
        }
        if (index1 > 1)
        {
          int num4 = 0;
          int upperBound1 = match.GetUpperBound(0);
          int index10 = num4;
          while (index10 <= upperBound1)
          {
            MethodBase match1 = match[index10];
            if (match1 != null)
            {
              int num5 = 0;
              int upperBound2 = match.GetUpperBound(0);
              int index11 = num5;
              while (index11 <= upperBound2)
              {
                if (index10 != index11 && match[index11] != null && (match1 == match[index11] || match1.DeclaringType.IsSubclassOf(match[index11].DeclaringType) && this.MethodsDifferOnlyByReturnType(match1, match[index11])))
                {
                  match[index11] = (MethodBase) null;
                  checked { --index1; }
                }
                checked { ++index11; }
              }
            }
            checked { ++index10; }
          }
          int num6 = 0;
          int upperBound3 = match.GetUpperBound(0);
          int index12 = num6;
          while (index12 <= upperBound3)
          {
            if (match[index12] == null)
            {
              int num5 = checked (index12 + 1);
              int upperBound2 = match.GetUpperBound(0);
              int index11 = num5;
              while (index11 <= upperBound2)
              {
                MethodBase methodBase = match[index11];
                if (methodBase != null)
                {
                  match[index12] = methodBase;
                  ParamOrder[index12] = ParamOrder[index11];
                  numArray1[index12] = numArray1[index11];
                  match[index11] = (MethodBase) null;
                }
                checked { ++index11; }
              }
            }
            checked { ++index12; }
          }
        }
        if (index1 > 1)
        {
          string str = "\r\n    " + Utils.MethodToString(match[0]);
          int num4 = 1;
          int num5 = checked (index1 - 1);
          int index10 = num4;
          while (index10 <= num5)
          {
            str = str + "\r\n    " + Utils.MethodToString(match[index10]);
            checked { ++index10; }
          }
          switch (bindScore1)
          {
            case VBBinder.BindScore.Exact:
              throw new AmbiguousMatchException(Utils.GetResourceString("AmbiguousCall_ExactMatch2", this.CalledMethodName(), str));
            case VBBinder.BindScore.Widening0:
            case VBBinder.BindScore.Widening1:
              throw new AmbiguousMatchException(Utils.GetResourceString("AmbiguousCall_WideningConversion2", this.CalledMethodName(), str));
            default:
              throw new AmbiguousMatchException(Utils.GetResourceString("AmbiguousCall2", this.CalledMethodName(), str));
          }
        }
      }
      MethodBase methodBase1 = match[index9];
      int[] paramOrder = (int[]) ParamOrder[index9];
      if (names != null)
        this.ReorderParams(paramOrder, args, state);
      ParameterInfo[] parameters1 = methodBase1.GetParameters();
      if (args.Length > 0)
      {
        state.m_ByRefFlags = new bool[checked (args.GetUpperBound(0) + 1)];
        bool flag2 = false;
        int num1 = 0;
        int upperBound1 = parameters1.GetUpperBound(0);
        int index1 = num1;
        while (index1 <= upperBound1)
        {
          if (parameters1[index1].ParameterType.IsByRef)
          {
            if (state.m_OriginalParamOrder == null)
            {
              if (index1 < state.m_ByRefFlags.Length)
                state.m_ByRefFlags[index1] = true;
            }
            else if (index1 < state.m_OriginalParamOrder.Length)
            {
              int index2 = state.m_OriginalParamOrder[index1];
              if (index2 >= 0)
                state.m_ByRefFlags[index2] = true;
            }
            flag2 = true;
          }
          checked { ++index1; }
        }
        if (!flag2)
          state.m_ByRefFlags = (bool[]) null;
      }
      else
        state.m_ByRefFlags = (bool[]) null;
      int val2 = numArray1[index9];
      if (val2 != -1)
      {
        int upperBound1 = parameters1.GetUpperBound(0);
        if (flag1)
          checked { --upperBound1; }
        int upperBound2 = args.GetUpperBound(0);
        if (flag1)
          checked { --upperBound2; }
        object[] objArray1 = new object[checked (parameters1.Length - 1 + 1)];
        int num1 = 0;
        int num2 = checked (Math.Min(upperBound2, val2) - 1);
        int index1 = num1;
        while (index1 <= num2)
        {
          objArray1[index1] = ObjectType.CTypeHelper(args[index1], parameters1[index1].ParameterType);
          checked { ++index1; }
        }
        if (upperBound2 < val2)
        {
          int num4 = checked (upperBound2 + 1);
          int num5 = checked (val2 - 1);
          int index2 = num4;
          while (index2 <= num5)
          {
            objArray1[index2] = ObjectType.CTypeHelper(parameters1[index2].DefaultValue, parameters1[index2].ParameterType);
            checked { ++index2; }
          }
        }
        if (flag1)
        {
          int upperBound3 = objArray1.GetUpperBound(0);
          objArray1[upperBound3] = ObjectType.CTypeHelper(args[args.GetUpperBound(0)], parameters1[upperBound3].ParameterType);
        }
        if (upperBound2 == -1)
        {
          objArray1[val2] = (object) Array.CreateInstance(elementType1, 0);
        }
        else
        {
          Type elementType2 = parameters1[upperBound1].ParameterType.GetElementType();
          int length2 = checked (args.Length - parameters1.Length + 1);
          Type parameterType = parameters1[upperBound1].ParameterType;
          if (length2 == 1 && parameterType.IsArray && (args[val2] == null || parameterType.IsInstanceOfType(args[val2])))
            objArray1[val2] = args[val2];
          else if (elementType2 == typeof (object))
          {
            object[] objArray2 = new object[checked (length2 - 1 + 1)];
            int num4 = 0;
            int num5 = checked (length2 - 1);
            int index2 = num4;
            while (index2 <= num5)
            {
              objArray2[index2] = ObjectType.CTypeHelper(args[checked (index2 + val2)], elementType2);
              checked { ++index2; }
            }
            objArray1[val2] = (object) objArray2;
          }
          else
          {
            Array instance = Array.CreateInstance(elementType2, length2);
            int num4 = 0;
            int num5 = checked (length2 - 1);
            int index2 = num4;
            while (index2 <= num5)
            {
              instance.SetValue(ObjectType.CTypeHelper(args[checked (index2 + val2)], elementType2), index2);
              checked { ++index2; }
            }
            objArray1[val2] = (object) instance;
          }
        }
        args = objArray1;
      }
      else
      {
        object[] objArray = new object[checked (parameters1.Length - 1 + 1)];
        int num1 = 0;
        int upperBound1 = objArray.GetUpperBound(0);
        int index1 = num1;
        while (index1 <= upperBound1)
        {
          int index2 = paramOrder[index1];
          objArray[index1] = index2 < 0 || index2 > args.GetUpperBound(0) ? ObjectType.CTypeHelper(parameters1[index1].DefaultValue, parameters1[index1].ParameterType) : ObjectType.CTypeHelper(args[index2], parameters1[index1].ParameterType);
          checked { ++index1; }
        }
        int num2 = index1;
        int upperBound2 = parameters1.GetUpperBound(0);
        int index10 = num2;
        while (index10 <= upperBound2)
        {
          objArray[index10] = ObjectType.CTypeHelper(parameters1[index10].DefaultValue, parameters1[index10].ParameterType);
          checked { ++index10; }
        }
        args = objArray;
      }
      if (methodBase1 == null)
        throw new MissingMemberException(Utils.GetResourceString("NoMethodTakingXArguments2", this.CalledMethodName(), Conversions.ToString(this.GetPropArgCount(args, flag1))));
      return methodBase1;
    }

    private int GetPropArgCount(object[] args, bool IsPropertySet)
    {
      if (IsPropertySet)
        return checked (args.Length - 1);
      return args.Length;
    }

    private int GetMostSpecific(MethodBase match0, MethodBase ThisMethod, int[] ArgIndexes, object[] ParamOrder, bool IsPropertySet, int ParamArrayIndex0, int ParamArrayIndex1, object[] args)
    {
      Type type1 = (Type) null;
      Type type2 = (Type) null;
      int upperBound1 = args.GetUpperBound(0);
      ParameterInfo[] parameters1 = ThisMethod.GetParameters();
      ParameterInfo[] parameters2 = match0.GetParameters();
      int[] numArray = (int[]) ParamOrder[0];
      int num1 = -1;
      int upperBound2 = args.GetUpperBound(0);
      int upperBound3 = parameters2.GetUpperBound(0);
      int upperBound4 = parameters1.GetUpperBound(0);
      if (IsPropertySet)
      {
        checked { --upperBound3; }
        checked { --upperBound4; }
        checked { --upperBound2; }
        checked { --upperBound1; }
      }
      bool flag1;
      if (ParamArrayIndex0 == -1)
      {
        flag1 = false;
      }
      else
      {
        type1 = parameters2[ParamArrayIndex0].ParameterType.GetElementType();
        flag1 = true;
        if (upperBound2 != -1 && upperBound2 == upperBound3)
        {
          object o = args[upperBound2];
          if (o == null || parameters2[upperBound3].ParameterType.IsInstanceOfType(o))
            flag1 = false;
        }
      }
      bool flag2;
      if (ParamArrayIndex1 == -1)
      {
        flag2 = false;
      }
      else
      {
        type2 = parameters1[ParamArrayIndex1].ParameterType.GetElementType();
        flag2 = true;
        if (upperBound2 != -1 && upperBound2 == upperBound4)
        {
          object o = args[upperBound2];
          if (o == null || parameters1[upperBound4].ParameterType.IsInstanceOfType(o))
            flag2 = false;
        }
      }
      int num2 = 0;
      int num3 = Math.Min(upperBound1, Math.Max(upperBound3, upperBound4));
      int index1 = num2;
      while (index1 <= num3)
      {
        int index2 = index1 > upperBound3 ? -1 : numArray[index1];
        int index3 = index1 > upperBound4 ? -1 : ArgIndexes[index1];
        if (index2 != -1 || index3 != -1)
        {
          if (flag2 && ParamArrayIndex1 != -1 && index1 >= ParamArrayIndex1)
          {
            Type type3;
            if (flag1 && ParamArrayIndex0 != -1 && index1 >= ParamArrayIndex0)
            {
              type3 = type1;
            }
            else
            {
              type3 = parameters2[index2].ParameterType;
              if (type3.IsByRef)
                type3 = type3.GetElementType();
            }
            if (type2 == type3)
            {
              if (num1 == -1 && ParamArrayIndex0 == -1 && (index1 == upperBound3 && args[upperBound3] != null))
                num1 = 0;
            }
            else if (ObjectType.IsWideningConversion(type3, type2))
            {
              if (num1 != 1)
              {
                num1 = 0;
              }
              else
              {
                num1 = -1;
                break;
              }
            }
            else if (ObjectType.IsWideningConversion(type2, type3))
            {
              if (num1 != 0)
              {
                num1 = 1;
              }
              else
              {
                num1 = -1;
                break;
              }
            }
          }
          else if (flag1 && ParamArrayIndex0 != -1 && index1 >= ParamArrayIndex0)
          {
            Type type3;
            if (flag2 && ParamArrayIndex1 != -1 && index1 >= ParamArrayIndex1)
            {
              type3 = type2;
            }
            else
            {
              type3 = parameters1[index3].ParameterType;
              if (type3.IsByRef)
                type3 = type3.GetElementType();
            }
            if (type1 == type3)
            {
              if (num1 == -1 && ParamArrayIndex1 == -1 && (index1 == upperBound4 && args[upperBound4] != null))
                num1 = 1;
            }
            else if (ObjectType.IsWideningConversion(type1, type3))
            {
              if (num1 != 1)
              {
                num1 = 0;
              }
              else
              {
                num1 = -1;
                break;
              }
            }
            else if (ObjectType.IsWideningConversion(type3, type1))
            {
              if (num1 != 0)
              {
                num1 = 1;
              }
              else
              {
                num1 = -1;
                break;
              }
            }
          }
          else
          {
            Type parameterType1 = parameters2[numArray[index1]].ParameterType;
            Type parameterType2 = parameters1[ArgIndexes[index1]].ParameterType;
            if (parameterType1 != parameterType2)
            {
              if (ObjectType.IsWideningConversion(parameterType1, parameterType2))
              {
                if (num1 != 1)
                {
                  num1 = 0;
                }
                else
                {
                  num1 = -1;
                  break;
                }
              }
              else if (ObjectType.IsWideningConversion(parameterType2, parameterType1))
              {
                if (num1 != 0)
                {
                  num1 = 1;
                }
                else
                {
                  num1 = -1;
                  break;
                }
              }
              else if (ObjectType.IsWiderNumeric(parameterType1, parameterType2))
              {
                if (num1 != 0)
                {
                  num1 = 1;
                }
                else
                {
                  num1 = -1;
                  break;
                }
              }
              else if (ObjectType.IsWiderNumeric(parameterType2, parameterType1))
              {
                if (num1 != 1)
                {
                  num1 = 0;
                }
                else
                {
                  num1 = -1;
                  break;
                }
              }
              else
                num1 = -1;
            }
          }
        }
        checked { ++index1; }
      }
      if (num1 == -1)
      {
        if ((ParamArrayIndex0 == -1 || !flag1) && ParamArrayIndex1 != -1)
        {
          if (flag2 && this.MatchesParamArraySignature(parameters2, parameters1, ParamArrayIndex1, IsPropertySet, upperBound1))
            num1 = 0;
        }
        else if ((ParamArrayIndex1 == -1 || !flag2) && (ParamArrayIndex0 != -1 && flag1) && this.MatchesParamArraySignature(parameters1, parameters2, ParamArrayIndex0, IsPropertySet, upperBound1))
          num1 = 1;
      }
      return num1;
    }

    private bool MatchesParamArraySignature(ParameterInfo[] param0, ParameterInfo[] param1, int ParamArrayIndex1, bool IsPropertySet, int ArgCountUpperBound)
    {
      int upperBound = param0.GetUpperBound(0);
      if (IsPropertySet)
        checked { --upperBound; }
      int num1 = Math.Min(upperBound, ArgCountUpperBound);
      int num2 = 0;
      int num3 = num1;
      int index = num2;
      while (index <= num3)
      {
        Type type1 = param0[index].ParameterType;
        if (type1.IsByRef)
          type1 = type1.GetElementType();
        Type type2;
        if (index >= ParamArrayIndex1)
        {
          type2 = param1[ParamArrayIndex1].ParameterType.GetElementType();
        }
        else
        {
          type2 = param1[index].ParameterType;
          if (type2.IsByRef)
            type2 = type2.GetElementType();
        }
        if (type1 != type2)
          return false;
        checked { ++index; }
      }
      return true;
    }

    private bool MethodsDifferOnlyByReturnType(MethodBase match1, MethodBase match2)
    {
      if (match1 != match2)
        ;
      ParameterInfo[] parameters1 = match1.GetParameters();
      ParameterInfo[] parameters2 = match2.GetParameters();
      int num1 = Math.Min(parameters1.GetUpperBound(0), parameters2.GetUpperBound(0));
      int num2 = 0;
      int num3 = num1;
      int index1 = num2;
      while (index1 <= num3)
      {
        Type type1 = parameters1[index1].ParameterType;
        if (type1.IsByRef)
          type1 = type1.GetElementType();
        Type type2 = parameters2[index1].ParameterType;
        if (type2.IsByRef)
          type2 = type2.GetElementType();
        if (type1 != type2)
          return false;
        checked { ++index1; }
      }
      if (parameters1.Length > parameters2.Length)
      {
        int num4 = checked (num1 + 1);
        int upperBound = parameters2.GetUpperBound(0);
        int index2 = num4;
        while (index2 <= upperBound)
        {
          if (!parameters1[index2].IsOptional)
            return false;
          checked { ++index2; }
        }
      }
      else if (parameters2.Length > parameters1.Length)
      {
        int num4 = checked (num1 + 1);
        int upperBound = parameters1.GetUpperBound(0);
        int index2 = num4;
        while (index2 <= upperBound)
        {
          if (!parameters2[index2].IsOptional)
            return false;
          checked { ++index2; }
        }
      }
      return true;
    }

    public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture)
    {
      if (this.m_CachedMember != null && this.m_CachedMember.MemberType == MemberTypes.Field && (match[0] != null && Operators.CompareString(match[0].Name, this.m_CachedMember.Name, false) == 0))
        return (FieldInfo) this.m_CachedMember;
      FieldInfo fieldInfo = match[0];
      int num = 1;
      int upperBound = match.GetUpperBound(0);
      int index = num;
      while (index <= upperBound)
      {
        if (match[index].DeclaringType.IsSubclassOf(fieldInfo.DeclaringType))
          fieldInfo = match[index];
        checked { ++index; }
      }
      return fieldInfo;
    }

    public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
    {
      VBBinder.BindScore bindScore1 = VBBinder.BindScore.Unknown;
      int index1 = 0;
      int num1 = 0;
      int upperBound1 = match.GetUpperBound(0);
      int index2 = num1;
      while (index2 <= upperBound1)
      {
        PropertyInfo propertyInfo = match[index2];
        if (propertyInfo != null)
        {
          VBBinder.BindScore bindScore2 = this.BindingScore(propertyInfo.GetIndexParameters(), (int[]) null, indexes, false, -1);
          if (bindScore2 < bindScore1)
          {
            if (index2 != 0)
            {
              match[0] = match[index2];
              match[index2] = (PropertyInfo) null;
            }
            index1 = 1;
            bindScore1 = bindScore2;
          }
          else if (bindScore2 == bindScore1)
          {
            switch (bindScore2)
            {
              case VBBinder.BindScore.Exact:
                if (propertyInfo.DeclaringType.IsSubclassOf(match[0].DeclaringType))
                {
                  if (index2 != 0)
                  {
                    match[0] = match[index2];
                    match[index2] = (PropertyInfo) null;
                  }
                  index1 = 1;
                  break;
                }
                if (!match[0].DeclaringType.IsSubclassOf(propertyInfo.DeclaringType))
                {
                  if (index1 != index2)
                  {
                    match[index1] = match[index2];
                    match[index2] = (PropertyInfo) null;
                  }
                  checked { ++index1; }
                  break;
                }
                break;
                                
              case VBBinder.BindScore.Widening1:
                ParameterInfo[] indexParameters1 = propertyInfo.GetIndexParameters();
                ParameterInfo[] indexParameters2 = match[0].GetIndexParameters();
                int num2 = -1;
                int num3 = 0;
                int upperBound2 = indexParameters1.GetUpperBound(0);
                int num4 = num3;
                while (num4 <= upperBound2)
                {
                  int index3 = num4;
                  int index4 = num4;
                  if (index3 != -1 && index4 != -1)
                  {
                    Type parameterType1 = indexParameters2[index3].ParameterType;
                    Type parameterType2 = indexParameters1[index4].ParameterType;
                    if (ObjectType.IsWideningConversion(parameterType1, parameterType2))
                    {
                      if (num2 != 1)
                      {
                        num2 = 0;
                      }
                      else
                      {
                        num2 = -1;
                        break;
                      }
                    }
                    else if (ObjectType.IsWideningConversion(parameterType2, parameterType1))
                    {
                      if (num2 != 0)
                      {
                        num2 = 1;
                      }
                      else
                      {
                        num2 = -1;
                        break;
                      }
                    }
                  }
                  checked { ++num4; }
                }
                switch (num2)
                {
                  case -1:
                    if (index1 != index2)
                    {
                      match[index1] = match[index2];
                      match[index2] = (PropertyInfo) null;
                    }
                    checked { ++index1; }
                    break;
                  case 0:
                    index1 = 1;
                    break;
                  default:
                    if (index2 != 0)
                    {
                      match[0] = match[index2];
                      match[index2] = (PropertyInfo) null;
                    }
                    index1 = 1;
                    break;
                }
                break;
              default:
                if (index1 != index2)
                {
                  match[index1] = match[index2];
                  match[index2] = (PropertyInfo) null;
                }
                checked { ++index1; }
                break;
            }
          }
          else
            match[index2] = (PropertyInfo) null;
        }
        checked { ++index2; }
      }
      if (index1 == 1)
        return match[0];
      return (PropertyInfo) null;
    }

    public override object ChangeType(object value, Type typ, CultureInfo culture)
    {
      try
      {
        if (typ == typeof (object) || typ.IsByRef && typ.GetElementType() == typeof (object))
          return value;
        return ObjectType.CTypeHelper(value, typ);
      }
      catch (Exception ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(value), Utils.VBFriendlyName(typ)));
      }
    }

    private VBBinder.BindScore BindingScore(ParameterInfo[] Parameters, int[] paramOrder, Type[] ArgTypes, bool IsPropertySet, int ParamArrayIndex)
    {
      VBBinder.BindScore bindScore = VBBinder.BindScore.Exact;
      int upperBound1 = ArgTypes.GetUpperBound(0);
      int upperBound2 = Parameters.GetUpperBound(0);
      if (IsPropertySet)
      {
        checked { --upperBound2; }
        checked { --upperBound1; }
      }
      int num1 = 0;
      int num2 = Math.Max(upperBound1, upperBound2);
      int index1 = num1;
      while (index1 <= num2)
      {
        int index2 = paramOrder != null ? paramOrder[index1] : index1;
        Type FromType = index2 != -1 ? ArgTypes[index2] : (Type) null;
        if (FromType != null)
        {
          Type ToType = index1 <= upperBound2 ? Parameters[index1].ParameterType : Parameters[ParamArrayIndex].ParameterType;
          if (index1 != ParamArrayIndex || !FromType.IsArray || ToType != FromType)
          {
            if (index1 == ParamArrayIndex && FromType.IsArray && (this.m_state.m_OriginalArgs == null || this.m_state.m_OriginalArgs[index2] == null || ToType.IsInstanceOfType(this.m_state.m_OriginalArgs[index2])))
            {
              if (bindScore < VBBinder.BindScore.Widening1)
                bindScore = VBBinder.BindScore.Widening1;
            }
            else
            {
              if (ParamArrayIndex != -1 && index1 >= ParamArrayIndex || ToType.IsByRef)
                ToType = ToType.GetElementType();
              if (FromType != ToType)
              {
                if (ObjectType.IsWideningConversion(FromType, ToType))
                {
                  if (bindScore < VBBinder.BindScore.Widening1)
                    bindScore = VBBinder.BindScore.Widening1;
                }
                else if (FromType.IsArray && (this.m_state.m_OriginalArgs == null || this.m_state.m_OriginalArgs[index2] == null || ToType.IsInstanceOfType(this.m_state.m_OriginalArgs[index2])))
                {
                  if (bindScore < VBBinder.BindScore.Widening1)
                    bindScore = VBBinder.BindScore.Widening1;
                }
                else
                  bindScore = VBBinder.BindScore.Narrowing;
              }
            }
          }
        }
        checked { ++index1; }
      }
      return bindScore;
    }

    private void ReorderParams(int[] paramOrder, object[] vars, VBBinder.VBBinderState state)
    {
      int num1 = Math.Max(vars.GetUpperBound(0), paramOrder.GetUpperBound(0));
      state.m_OriginalParamOrder = new int[checked (num1 + 1)];
      int num2 = 0;
      int num3 = num1;
      int index = num2;
      while (index <= num3)
      {
        state.m_OriginalParamOrder[index] = paramOrder[index];
        checked { ++index; }
      }
    }

    private Exception CreateParamOrder(bool SetProp, int[] paramOrder, ParameterInfo[] pars, object[] args, string[] names)
    {
      bool[] flagArray = new bool[checked (pars.Length - 1 + 1)];
      int num1 = checked (args.Length - names.Length - 1);
      int upperBound1 = pars.GetUpperBound(0);
      int num2 = 0;
      int upperBound2 = pars.GetUpperBound(0);
      int index1 = num2;
      while (index1 <= upperBound2)
      {
        paramOrder[index1] = -1;
        checked { ++index1; }
      }
      if (SetProp)
      {
        paramOrder[pars.GetUpperBound(0)] = args.GetUpperBound(0);
        checked { --num1; }
        checked { --upperBound1; }
      }
      int num3 = 0;
      int num4 = num1;
      int index2 = num3;
      while (index2 <= num4)
      {
        paramOrder[index2] = checked (names.Length + index2);
        checked { ++index2; }
      }
      int num5 = 0;
      int upperBound3 = names.GetUpperBound(0);
      int index3 = num5;
      while (index3 <= upperBound3)
      {
        int num6 = 0;
        int num7 = upperBound1;
        int index4 = num6;
        while (index4 <= num7)
        {
          if (Strings.StrComp(names[index3], pars[index4].Name, CompareMethod.Text) == 0)
          {
            if (paramOrder[index4] != -1)
              return (Exception) new ArgumentException(Utils.GetResourceString("NamedArgumentAlreadyUsed1", new string[1]
              {
                pars[index4].Name
              }));
            paramOrder[index4] = index3;
            flagArray[index3] = true;
            break;
          }
          checked { ++index4; }
        }
        if (index4 > upperBound1)
          return (Exception) new MissingMemberException(Utils.GetResourceString("Argument_InvalidNamedArg2", names[index3], this.CalledMethodName()));
        checked { ++index3; }
      }
      return (Exception) null;
    }

    [DebuggerHidden]
    [DebuggerStepThrough]
    internal object InvokeMember(string name, BindingFlags invokeAttr, Type objType, IReflect objIReflect, object target, object[] args, string[] namedParameters)
    {
      if (objType.IsCOMObject)
      {
        ParameterModifier[] modifiers = (ParameterModifier[]) null;
        if (this.m_ByRefFlags != null)
        {
          if (target != null)
          {
            if (true /*!RemotingServices.IsTransparentProxy(target)*/)
            {
              ParameterModifier parameterModifier = new ParameterModifier(args.Length);
              modifiers = new ParameterModifier[1]
              {
                parameterModifier
              };
              object obj = (object) Missing.Value;
              int num = 0;
              int upperBound = args.GetUpperBound(0);
              int index = num;
              while (index <= upperBound)
              {
                if (args[index] != obj)
                  parameterModifier[index] = this.m_ByRefFlags[index];
                checked { ++index; }
              }
            }
          }
        }
        try
        {
          //new SecurityPermission(PermissionState.Unrestricted).Demand();
          return objIReflect.InvokeMember(name, invokeAttr, (Binder) null, target, args, modifiers, (CultureInfo) null, namedParameters);
        }
        catch (MissingMemberException ex)
        {
          throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", name, Utils.VBFriendlyName(objType)));
        }
      }
      else
      {
        this.m_BindToName = name;
        this.m_objType = objType;
        if (name.Length == 0)
        {
          if (objType == objIReflect)
          {
            name = this.GetDefaultMemberName(objType);
            if (name == null)
              throw new MissingMemberException(Utils.GetResourceString("MissingMember_NoDefaultMemberFound1", new string[1]
              {
                Utils.VBFriendlyName(objType)
              }));
          }
          else
            name = "";
        }
        MethodBase[] methodsByName = this.GetMethodsByName(objType, objIReflect, name, invokeAttr);
        if (args == null)
          args = new object[0];
        object ObjState = (object) null;
        MethodBase method = this.BindToMethod(invokeAttr, methodsByName, ref args, (ParameterModifier[]) null, (CultureInfo) null, namedParameters, out ObjState);
        if (method == null)
          throw new MissingMemberException(Utils.GetResourceString("NoMethodTakingXArguments2", this.CalledMethodName(), Conversions.ToString(this.GetPropArgCount(args, (invokeAttr & BindingFlags.SetProperty) != BindingFlags.Default))));
        VBBinder.SecurityCheckForLateboundCalls((MemberInfo) method, objType, objIReflect);
        MethodInfo methodInfo = (MethodInfo) method;
        object obj;
        if (objType == objIReflect || methodInfo.IsStatic || LateBinding.DoesTargetObjectMatch(target, (MemberInfo) methodInfo))
        {
          LateBinding.VerifyObjRefPresentForInstanceCall(target, (MemberInfo) methodInfo);
          obj = methodInfo.Invoke(target, args);
        }
        else
          obj = LateBinding.InvokeMemberOnIReflect(objIReflect, (MemberInfo) methodInfo, BindingFlags.InvokeMethod, target, args);
        if (ObjState != null)
          this.ReorderArgumentArray(ref args, ObjState);
        return obj;
      }
    }

    private string GetDefaultMemberName(Type typ)
    {
      do
      {
        object[] customAttributes = typ.GetCustomAttributes(typeof (DefaultMemberAttribute), false);
        if (customAttributes != null && customAttributes.Length != 0)
          return ((DefaultMemberAttribute) customAttributes[0]).MemberName;
        typ = typ.BaseType;
      }
      while (typ != null);
      return (string) null;
    }

    private MethodBase[] GetMethodsByName(Type objType, IReflect objIReflect, string name, BindingFlags invokeAttr)
    {
      MemberInfo[] nonGenericMembers = LateBinding.GetNonGenericMembers(objIReflect.GetMember(name, invokeAttr));
      if (nonGenericMembers == null)
        return (MethodBase[]) null;
      int num1 = 0;
      int upperBound1 = nonGenericMembers.GetUpperBound(0);
      int index1 = num1;
      int num2=0;
      while (index1 <= upperBound1)
      {
        MemberInfo memberInfo = nonGenericMembers[index1];
        if (memberInfo != null)
        {
          if (memberInfo.MemberType == MemberTypes.Field)
          {
            Type declaringType = memberInfo.DeclaringType;
            int num3 = 0;
            int upperBound2 = nonGenericMembers.GetUpperBound(0);
            int index2 = num3;
            while (index2 <= upperBound2)
            {
              if (index1 != index2 && nonGenericMembers[index2] != null && declaringType.IsSubclassOf(nonGenericMembers[index2].DeclaringType))
              {
                nonGenericMembers[index2] = (MemberInfo) null;
                checked { ++num2; }
              }
              checked { ++index2; }
            }
          }
          else if (memberInfo.MemberType == MemberTypes.Method)
          {
            MethodInfo methodInfo = (MethodInfo) memberInfo;
            if (!methodInfo.IsHideBySig && (!methodInfo.IsVirtual || methodInfo.IsVirtual && (methodInfo.Attributes & MethodAttributes.VtableLayoutMask) != MethodAttributes.PrivateScope || methodInfo.IsVirtual && (methodInfo.GetBaseDefinition().Attributes & MethodAttributes.VtableLayoutMask) != MethodAttributes.PrivateScope))
            {
              Type declaringType = memberInfo.DeclaringType;
              int num3 = 0;
              int upperBound2 = nonGenericMembers.GetUpperBound(0);
              int index2 = num3;
              while (index2 <= upperBound2)
              {
                if (index1 != index2 && nonGenericMembers[index2] != null && declaringType.IsSubclassOf(nonGenericMembers[index2].DeclaringType))
                {
                  nonGenericMembers[index2] = (MemberInfo) null;
                  checked { ++num2; }
                }
                checked { ++index2; }
              }
            }
          }
          else if (memberInfo.MemberType == MemberTypes.Property)
          {
            PropertyInfo propertyInfo = (PropertyInfo) memberInfo;
            int num3 = 1;
            do
            {
              MethodInfo methodInfo = num3 != 1 ? propertyInfo.GetSetMethod() : propertyInfo.GetGetMethod();
              if (methodInfo != null && !methodInfo.IsHideBySig && (!methodInfo.IsVirtual || methodInfo.IsVirtual && (methodInfo.Attributes & MethodAttributes.VtableLayoutMask) != MethodAttributes.PrivateScope))
              {
                Type declaringType = memberInfo.DeclaringType;
                int num4 = 0;
                int upperBound2 = nonGenericMembers.GetUpperBound(0);
                int index2 = num4;
                while (index2 <= upperBound2)
                {
                  if (index1 != index2 && nonGenericMembers[index2] != null && declaringType.IsSubclassOf(nonGenericMembers[index2].DeclaringType))
                  {
                    nonGenericMembers[index2] = (MemberInfo) null;
                    checked { ++num2; }
                  }
                  checked { ++index2; }
                }
              }
              checked { ++num3; }
            }
            while (num3 <= 2);
            MethodInfo methodInfo1 = (invokeAttr & BindingFlags.GetProperty) == BindingFlags.Default ? ((invokeAttr & BindingFlags.SetProperty) == BindingFlags.Default ? (MethodInfo) null : propertyInfo.GetSetMethod()) : propertyInfo.GetGetMethod();
            if (methodInfo1 == null)
              checked { ++num2; }
            nonGenericMembers[index1] = (MemberInfo) methodInfo1;
          }
          else if (memberInfo.MemberType == MemberTypes.NestedType)
          {
            Type declaringType = memberInfo.DeclaringType;
            int num3 = 0;
            int upperBound2 = nonGenericMembers.GetUpperBound(0);
            int index2 = num3;
            while (index2 <= upperBound2)
            {
              if (index1 != index2 && nonGenericMembers[index2] != null && declaringType.IsSubclassOf(nonGenericMembers[index2].DeclaringType))
              {
                nonGenericMembers[index2] = (MemberInfo) null;
                checked { ++num2; }
              }
              checked { ++index2; }
            }
            if (num2 == checked (nonGenericMembers.Length - 1))
              throw new ArgumentException(Utils.GetResourceString("Argument_IllegalNestedType2", name, Utils.VBFriendlyName(objType)));
            nonGenericMembers[index1] = (MemberInfo) null;
            checked { ++num2; }
          }
        }
        checked { ++index1; }
      }
      MethodBase[] methodBaseArray = new MethodBase[checked (nonGenericMembers.Length - num2 - 1 + 1)];
      int index3 = 0;
      int num5 = 0;
      int num6 = checked (nonGenericMembers.Length - 1);
      int index4 = num5;
      while (index4 <= num6)
      {
        if (nonGenericMembers[index4] != null)
        {
          methodBaseArray[index3] = (MethodBase) nonGenericMembers[index4];
          checked { ++index3; }
        }
        checked { ++index4; }
      }
      return methodBaseArray;
    }

    internal string CalledMethodName()
    {
      return this.m_objType.Name + "." + this.m_BindToName;
    }

    internal static void SecurityCheckForLateboundCalls(MemberInfo member, Type objType, IReflect objIReflect)
    {
      if (objType != objIReflect && !VBBinder.IsMemberPublic(member))
        throw new MissingMethodException();
      Type declaringType = member.DeclaringType;
      if (!declaringType.IsPublic && declaringType.Assembly == Utils.VBRuntimeAssembly)
        throw new MissingMethodException();
    }

    private static bool IsMemberPublic(MemberInfo Member)
    {
      switch (Member.MemberType)
      {
        case MemberTypes.Constructor:
          return ((MethodBase) Member).IsPublic;
        case MemberTypes.Field:
          return ((FieldInfo) Member).IsPublic;
        case MemberTypes.Method:
          return ((MethodBase) Member).IsPublic;
        case MemberTypes.Property:
          return false;
        default:
          return false;
      }
    }

    internal void CacheMember(MemberInfo member)
    {
      this.m_CachedMember = member;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class VBBinderState
    {
      internal object[] m_OriginalArgs;
      internal bool[] m_ByRefFlags;
      internal bool[] m_OriginalByRefFlags;
      internal int[] m_OriginalParamOrder;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public enum BindScore
    {
      Exact,
      Widening0,
      Widening1,
      Narrowing,
      Unknown,
    }
  }
}
