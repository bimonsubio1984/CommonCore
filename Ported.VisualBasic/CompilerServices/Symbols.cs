// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.Symbols
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll



using System;
using System.Collections;
using System.Reflection;
//using System.Runtime.Remoting;

namespace Ported.VisualBasic.CompilerServices
{
  internal class Symbols
  {
    internal static readonly object[] NoArguments = new object[0];
    internal static readonly string[] NoArgumentNames = new string[0];
    internal static readonly Type[] NoTypeArguments = new Type[0];
    internal static readonly Type[] NoTypeParameters = new Type[0];
    internal static readonly string[] OperatorCLSNames = new string[28];
    internal static readonly string[] OperatorNames;

    private Symbols()
    {
    }

    static Symbols()
    {
      Symbols.OperatorCLSNames[1] = "op_Explicit";
      Symbols.OperatorCLSNames[2] = "op_Implicit";
      Symbols.OperatorCLSNames[3] = "op_True";
      Symbols.OperatorCLSNames[4] = "op_False";
      Symbols.OperatorCLSNames[5] = "op_UnaryNegation";
      Symbols.OperatorCLSNames[6] = "op_OnesComplement";
      Symbols.OperatorCLSNames[7] = "op_UnaryPlus";
      Symbols.OperatorCLSNames[8] = "op_Addition";
      Symbols.OperatorCLSNames[9] = "op_Subtraction";
      Symbols.OperatorCLSNames[10] = "op_Multiply";
      Symbols.OperatorCLSNames[11] = "op_Division";
      Symbols.OperatorCLSNames[12] = "op_Exponent";
      Symbols.OperatorCLSNames[13] = "op_IntegerDivision";
      Symbols.OperatorCLSNames[14] = "op_Concatenate";
      Symbols.OperatorCLSNames[15] = "op_LeftShift";
      Symbols.OperatorCLSNames[16] = "op_RightShift";
      Symbols.OperatorCLSNames[17] = "op_Modulus";
      Symbols.OperatorCLSNames[18] = "op_BitwiseOr";
      Symbols.OperatorCLSNames[19] = "op_ExclusiveOr";
      Symbols.OperatorCLSNames[20] = "op_BitwiseAnd";
      Symbols.OperatorCLSNames[21] = "op_Like";
      Symbols.OperatorCLSNames[22] = "op_Equality";
      Symbols.OperatorCLSNames[23] = "op_Inequality";
      Symbols.OperatorCLSNames[24] = "op_LessThan";
      Symbols.OperatorCLSNames[25] = "op_LessThanOrEqual";
      Symbols.OperatorCLSNames[26] = "op_GreaterThanOrEqual";
      Symbols.OperatorCLSNames[27] = "op_GreaterThan";
      Symbols.OperatorNames = new string[28];
      Symbols.OperatorNames[1] = "CType";
      Symbols.OperatorNames[2] = "CType";
      Symbols.OperatorNames[3] = "IsTrue";
      Symbols.OperatorNames[4] = "IsFalse";
      Symbols.OperatorNames[5] = "-";
      Symbols.OperatorNames[6] = "Not";
      Symbols.OperatorNames[7] = "+";
      Symbols.OperatorNames[8] = "+";
      Symbols.OperatorNames[9] = "-";
      Symbols.OperatorNames[10] = "*";
      Symbols.OperatorNames[11] = "/";
      Symbols.OperatorNames[12] = "^";
      Symbols.OperatorNames[13] = "\\";
      Symbols.OperatorNames[14] = "&";
      Symbols.OperatorNames[15] = "<<";
      Symbols.OperatorNames[16] = ">>";
      Symbols.OperatorNames[17] = "Mod";
      Symbols.OperatorNames[18] = "Or";
      Symbols.OperatorNames[19] = "Xor";
      Symbols.OperatorNames[20] = "And";
      Symbols.OperatorNames[21] = "Like";
      Symbols.OperatorNames[22] = "=";
      Symbols.OperatorNames[23] = "<>";
      Symbols.OperatorNames[24] = "<";
      Symbols.OperatorNames[25] = "<=";
      Symbols.OperatorNames[26] = ">=";
      Symbols.OperatorNames[27] = ">";
    }

    internal static bool IsUnaryOperator(Symbols.UserDefinedOperator Op)
    {
      switch (Op)
      {
        case Symbols.UserDefinedOperator.Narrow:
        case Symbols.UserDefinedOperator.Widen:
        case Symbols.UserDefinedOperator.IsTrue:
        case Symbols.UserDefinedOperator.IsFalse:
        case Symbols.UserDefinedOperator.Negate:
        case Symbols.UserDefinedOperator.Not:
        case Symbols.UserDefinedOperator.UnaryPlus:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsBinaryOperator(Symbols.UserDefinedOperator Op)
    {
      switch (Op)
      {
        case Symbols.UserDefinedOperator.Plus:
        case Symbols.UserDefinedOperator.Minus:
        case Symbols.UserDefinedOperator.Multiply:
        case Symbols.UserDefinedOperator.Divide:
        case Symbols.UserDefinedOperator.Power:
        case Symbols.UserDefinedOperator.IntegralDivide:
        case Symbols.UserDefinedOperator.Concatenate:
        case Symbols.UserDefinedOperator.ShiftLeft:
        case Symbols.UserDefinedOperator.ShiftRight:
        case Symbols.UserDefinedOperator.Modulus:
        case Symbols.UserDefinedOperator.Or:
        case Symbols.UserDefinedOperator.Xor:
        case Symbols.UserDefinedOperator.And:
        case Symbols.UserDefinedOperator.Like:
        case Symbols.UserDefinedOperator.Equal:
        case Symbols.UserDefinedOperator.NotEqual:
        case Symbols.UserDefinedOperator.Less:
        case Symbols.UserDefinedOperator.LessEqual:
        case Symbols.UserDefinedOperator.GreaterEqual:
        case Symbols.UserDefinedOperator.Greater:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsUserDefinedOperator(MethodBase Method)
    {
      return Method.IsSpecialName && Method.Name.StartsWith("op_", StringComparison.Ordinal);
    }

    internal static bool IsNarrowingConversionOperator(MethodBase Method)
    {
      return Method.IsSpecialName && Method.Name.Equals(Symbols.OperatorCLSNames[1]);
    }

    internal static Symbols.UserDefinedOperator MapToUserDefinedOperator(MethodBase Method)
    {
      int index = 1;
      do
      {
        if (Method.Name.Equals(Symbols.OperatorCLSNames[index]))
        {
          int length = Method.GetParameters().Length;
          Symbols.UserDefinedOperator Op = (Symbols.UserDefinedOperator) checked ((sbyte) index);
          if (length == 1 && Symbols.IsUnaryOperator(Op) || length == 2 && Symbols.IsBinaryOperator(Op))
            return Op;
        }
        checked { ++index; }
      }
      while (index <= 27);
      return Symbols.UserDefinedOperator.UNDEF;
    }

    internal static TypeCode GetTypeCode(Type Type)
    {
      return Type.GetTypeCode(Type);
    }

    internal static Type MapTypeCodeToType(TypeCode TypeCode)
    {
      switch (TypeCode)
      {
        case TypeCode.Object:
          return typeof (object);
        case TypeCode.DBNull:
          return typeof (DBNull);
        case TypeCode.Boolean:
          return typeof (bool);
        case TypeCode.Char:
          return typeof (char);
        case TypeCode.SByte:
          return typeof (sbyte);
        case TypeCode.Byte:
          return typeof (byte);
        case TypeCode.Int16:
          return typeof (short);
        case TypeCode.UInt16:
          return typeof (ushort);
        case TypeCode.Int32:
          return typeof (int);
        case TypeCode.UInt32:
          return typeof (uint);
        case TypeCode.Int64:
          return typeof (long);
        case TypeCode.UInt64:
          return typeof (ulong);
        case TypeCode.Single:
          return typeof (float);
        case TypeCode.Double:
          return typeof (double);
        case TypeCode.Decimal:
          return typeof (Decimal);
        case TypeCode.DateTime:
          return typeof (DateTime);
        case TypeCode.String:
          return typeof (string);
        default:
          return (Type) null;
      }
    }

    internal static bool IsRootObjectType(Type Type)
    {
      return Type == typeof (object);
    }

    internal static bool IsRootEnumType(Type Type)
    {
      return Type == typeof (Enum);
    }

    internal static bool IsValueType(Type Type)
    {
      return Type.IsValueType;
    }

    internal static bool IsEnum(Type Type)
    {
      return Type.IsEnum;
    }

    internal static bool IsArrayType(Type Type)
    {
      return Type.IsArray;
    }

    internal static bool IsStringType(Type Type)
    {
      return Type == typeof (string);
    }

    internal static bool IsCharArrayRankOne(Type Type)
    {
      return Type == typeof (char[]);
    }

    internal static bool IsIntegralType(TypeCode TypeCode)
    {
      switch (TypeCode)
      {
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsNumericType(TypeCode TypeCode)
    {
      switch (TypeCode)
      {
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsNumericType(Type Type)
    {
      return Symbols.IsNumericType(Symbols.GetTypeCode(Type));
    }

    internal static bool IsIntrinsicType(TypeCode TypeCode)
    {
      switch (TypeCode)
      {
        case TypeCode.Boolean:
        case TypeCode.Char:
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.DateTime:
        case TypeCode.String:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsIntrinsicType(Type Type)
    {
      return Symbols.IsIntrinsicType(Symbols.GetTypeCode(Type)) && !Symbols.IsEnum(Type);
    }

    internal static bool IsClass(Type Type)
    {
      return Type.IsClass || Symbols.IsRootEnumType(Type);
    }

    internal static bool IsClassOrValueType(Type Type)
    {
      return Symbols.IsValueType(Type) || Symbols.IsClass(Type);
    }

    internal static bool IsInterface(Type Type)
    {
      return Type.IsInterface;
    }

    internal static bool IsClassOrInterface(Type Type)
    {
      return Symbols.IsClass(Type) || Symbols.IsInterface(Type);
    }

    internal static bool IsReferenceType(Type Type)
    {
      return Symbols.IsClass(Type) || Symbols.IsInterface(Type);
    }

    internal static bool IsGenericParameter(Type Type)
    {
      return Type.IsGenericParameter;
    }

    internal static bool Implements(Type Implementor, Type Interface)
    {
      Type[] interfaces = Implementor.GetInterfaces();
      int index = 0;
      while (index < interfaces.Length)
      {
        if (interfaces[index] == Interface)
          return true;
        checked { ++index; }
      }
      return false;
    }

    internal static bool IsOrInheritsFrom(Type Derived, Type Base)
    {
      if (Derived == Base)
        return true;
      if (Derived.IsGenericParameter)
      {
        if (Symbols.IsClass(Base) && (uint) (Derived.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) > 0U && Symbols.IsOrInheritsFrom(typeof (ValueType), Base))
          return true;
        Type[] parameterConstraints = Derived.GetGenericParameterConstraints();
        int index = 0;
        while (index < parameterConstraints.Length)
        {
          if (Symbols.IsOrInheritsFrom(parameterConstraints[index], Base))
            return true;
          checked { ++index; }
        }
      }
      else if (Symbols.IsInterface(Derived))
      {
        if (Symbols.IsInterface(Base))
        {
          Type[] interfaces = Derived.GetInterfaces();
          int index = 0;
          while (index < interfaces.Length)
          {
            if (interfaces[index] == Base)
              return true;
            checked { ++index; }
          }
        }
      }
      else if (Symbols.IsClass(Base) && Symbols.IsClassOrValueType(Derived))
        return Derived.IsSubclassOf(Base);
      return false;
    }

    internal static bool IsGeneric(Type Type)
    {
      return Type.IsGenericType;
    }

    internal static bool IsInstantiatedGeneric(Type Type)
    {
      return Type.IsGenericType && !Type.IsGenericTypeDefinition;
    }

    internal static bool IsGeneric(MethodBase Method)
    {
      return Method.IsGenericMethod;
    }

    internal static bool IsGeneric(MemberInfo Member)
    {
      MethodBase Method = Member as MethodBase;
      if (Method == null)
        return false;
      return Symbols.IsGeneric(Method);
    }

    internal static bool IsRawGeneric(MethodBase Method)
    {
      return Method.IsGenericMethod && Method.IsGenericMethodDefinition;
    }

    internal static Type[] GetTypeParameters(MemberInfo Member)
    {
      MethodBase methodBase = Member as MethodBase;
      if (methodBase == null)
        return Symbols.NoTypeParameters;
      return methodBase.GetGenericArguments();
    }

    internal static Type[] GetTypeParameters(Type Type)
    {
      return Type.GetGenericArguments();
    }

    internal static Type[] GetTypeArguments(Type Type)
    {
      return Type.GetGenericArguments();
    }

    internal static Type[] GetInterfaceConstraints(Type GenericParameter)
    {
      return GenericParameter.GetInterfaces();
    }

    internal static Type GetClassConstraint(Type GenericParameter)
    {
      Type baseType = GenericParameter.BaseType;
      if (Symbols.IsRootObjectType(baseType))
        return (Type) null;
      return baseType;
    }

    internal static int IndexIn(Type PossibleGenericParameter, MethodBase GenericMethodDef)
    {
      if (Symbols.IsGenericParameter(PossibleGenericParameter) && PossibleGenericParameter.DeclaringMethod != null && Symbols.AreGenericMethodDefsEqual(PossibleGenericParameter.DeclaringMethod, GenericMethodDef))
        return PossibleGenericParameter.GenericParameterPosition;
      return -1;
    }

    internal static bool RefersToGenericParameter(Type ReferringType, MethodBase Method)
    {
      if (!Symbols.IsRawGeneric(Method))
        return false;
      if (ReferringType.IsByRef)
        ReferringType = Symbols.GetElementType(ReferringType);
      if (Symbols.IsGenericParameter(ReferringType))
      {
        if (Symbols.AreGenericMethodDefsEqual(ReferringType.DeclaringMethod, Method))
          return true;
      }
      else if (Symbols.IsGeneric(ReferringType))
      {
        Type[] typeArguments = Symbols.GetTypeArguments(ReferringType);
        int index = 0;
        while (index < typeArguments.Length)
        {
          if (Symbols.RefersToGenericParameter(typeArguments[index], Method))
            return true;
          checked { ++index; }
        }
      }
      else if (Symbols.IsArrayType(ReferringType))
        return Symbols.RefersToGenericParameter(ReferringType.GetElementType(), Method);
      return false;
    }

    internal static bool RefersToGenericParameterCLRSemantics(Type ReferringType, Type Typ)
    {
      if (ReferringType.IsByRef)
        ReferringType = Symbols.GetElementType(ReferringType);
      if (Symbols.IsGenericParameter(ReferringType))
      {
        if (ReferringType.DeclaringType == Typ)
          return true;
      }
      else if (Symbols.IsGeneric(ReferringType))
      {
        Type[] typeArguments = Symbols.GetTypeArguments(ReferringType);
        int index = 0;
        while (index < typeArguments.Length)
        {
          if (Symbols.RefersToGenericParameterCLRSemantics(typeArguments[index], Typ))
            return true;
          checked { ++index; }
        }
      }
      else if (Symbols.IsArrayType(ReferringType))
        return Symbols.RefersToGenericParameterCLRSemantics(ReferringType.GetElementType(), Typ);
      return false;
    }

    internal static bool AreGenericMethodDefsEqual(MethodBase Method1, MethodBase Method2)
    {
      return Method1 == Method2 || Method1.MetadataToken == Method2.MetadataToken;
    }

    internal static bool IsShadows(MethodBase Method)
    {
      return !Method.IsHideBySig && (!Method.IsVirtual || (Method.Attributes & MethodAttributes.VtableLayoutMask) != MethodAttributes.PrivateScope || (((MethodInfo) Method).GetBaseDefinition().Attributes & MethodAttributes.VtableLayoutMask) != MethodAttributes.PrivateScope);
    }

    internal static bool IsShared(MemberInfo Member)
    {
      switch (Member.MemberType)
      {
        case MemberTypes.Constructor:
          return ((MethodBase) Member).IsStatic;
        case MemberTypes.Field:
          return ((FieldInfo) Member).IsStatic;
        case MemberTypes.Method:
          return ((MethodBase) Member).IsStatic;
        case MemberTypes.Property:
          return ((PropertyInfo) Member).GetGetMethod().IsStatic;
        default:
          return false;
      }
    }

    internal static bool IsParamArray(ParameterInfo Parameter)
    {
      return Symbols.IsArrayType(Parameter.ParameterType) && Parameter.IsDefined(typeof (ParamArrayAttribute), false);
    }

    internal static Type GetElementType(Type Type)
    {
      return Type.GetElementType();
    }

    internal static bool AreParametersAndReturnTypesValid(ParameterInfo[] Parameters, Type ReturnType)
    {
      if (ReturnType != null && (ReturnType.IsPointer || ReturnType.IsByRef))
        return false;
      if (Parameters != null)
      {
        ParameterInfo[] parameterInfoArray = Parameters;
        int index = 0;
        while (index < parameterInfoArray.Length)
        {
          if (parameterInfoArray[index].ParameterType.IsPointer)
            return false;
          checked { ++index; }
        }
      }
      return true;
    }

    internal static void GetAllParameterCounts(ParameterInfo[] Parameters, ref int RequiredParameterCount, ref int MaximumParameterCount, ref int ParamArrayIndex)
    {
      MaximumParameterCount = Parameters.Length;
      int index = checked (MaximumParameterCount - 1);
      while (index >= 0)
      {
        if (!Parameters[index].IsOptional)
        {
          RequiredParameterCount = checked (index + 1);
          break;
        }
        checked { index += -1; }
      }
      if (MaximumParameterCount == 0 || !Symbols.IsParamArray(Parameters[checked (MaximumParameterCount - 1)]))
        return;
      ParamArrayIndex = checked (MaximumParameterCount - 1);
      checked { --RequiredParameterCount; }
    }

    internal static bool IsNonPublicRuntimeMember(MemberInfo Member)
    {
      Type declaringType = Member.DeclaringType;
      return !declaringType.IsPublic && declaringType.Assembly == Utils.VBRuntimeAssembly;
    }

    internal static bool HasFlag(BindingFlags Flags, BindingFlags FlagToTest)
    {
      return (uint) (Flags & FlagToTest) > 0U;
    }

    internal enum UserDefinedOperator : sbyte
    {
      UNDEF,
      Narrow,
      Widen,
      IsTrue,
      IsFalse,
      Negate,
      Not,
      UnaryPlus,
      Plus,
      Minus,
      Multiply,
      Divide,
      Power,
      IntegralDivide,
      Concatenate,
      ShiftLeft,
      ShiftRight,
      Modulus,
      Or,
      Xor,
      And,
      Like,
      Equal,
      NotEqual,
      Less,
      LessEqual,
      GreaterEqual,
      Greater,
      MAX,
    }

    internal sealed class Container
    {
      private static readonly MemberInfo[] NoMembers = new MemberInfo[0];
      private readonly object m_Instance;
      private readonly Type m_Type;
      private readonly IReflect m_IReflect;
      private readonly bool m_UseCustomReflection;
      private const BindingFlags DefaultLookupFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;

      internal Container(object Instance)
      {
        if (Instance == null)
          throw ExceptionUtils.VbMakeException(91);
        this.m_Instance = Instance;
        this.m_Type = Instance.GetType();
        this.m_UseCustomReflection = false;
        if (!this.m_Type.IsCOMObject && /*!RemotingServices.IsTransparentProxy(Instance) && */ !(Instance is Type))
        {
          this.m_IReflect = Instance as IReflect;
          if (this.m_IReflect != null)
            this.m_UseCustomReflection = true;
        }
        if (!this.m_UseCustomReflection)
          this.m_IReflect = (IReflect) this.m_Type;
        this.CheckForClassExtendingCOMClass();
      }

      internal Container(Type Type)
      {
        if (Type == null)
          throw ExceptionUtils.VbMakeException(91);
        this.m_Instance = (object) null;
        this.m_Type = Type;
        this.m_IReflect = (IReflect) Type;
        this.m_UseCustomReflection = false;
        this.CheckForClassExtendingCOMClass();
      }

      internal bool IsCOMObject
      {
        get
        {
          return this.m_Type.IsCOMObject;
        }
      }

      internal string VBFriendlyName
      {
        get
        {
          return Utils.VBFriendlyName(this.m_Type, this.m_Instance);
        }
      }

      internal bool IsArray
      {
        get
        {
          return Symbols.IsArrayType(this.m_Type) && this.m_Instance != null;
        }
      }

      internal bool IsValueType
      {
        get
        {
          return Symbols.IsValueType(this.m_Type) && this.m_Instance != null;
        }
      }

      private static MemberInfo[] FilterInvalidMembers(MemberInfo[] Members)
      {
        if (Members == null || Members.Length == 0)
          return (MemberInfo[]) null;
        int num1 = 0;
        int num2 = 0;
        int num3 = checked (Members.Length - 1);
        int index1 = num2;
        while (index1 <= num3)
        {
          ParameterInfo[] Parameters = (ParameterInfo[]) null;
          Type ReturnType = (Type) null;
          switch (Members[index1].MemberType)
          {
            case MemberTypes.Constructor:
            case MemberTypes.Method:
              MethodInfo member1 = (MethodInfo) Members[index1];
              Parameters = member1.GetParameters();
              ReturnType = member1.ReturnType;
              break;
            case MemberTypes.Field:
              ReturnType = ((FieldInfo) Members[index1]).FieldType;
              break;
            case MemberTypes.Property:
              PropertyInfo member2 = (PropertyInfo) Members[index1];
              MethodInfo getMethod = member2.GetGetMethod();
              if (getMethod != null)
              {
                Parameters = getMethod.GetParameters();
              }
              else
              {
                ParameterInfo[] parameters = member2.GetSetMethod().GetParameters();
                Parameters = new ParameterInfo[checked (parameters.Length - 2 + 1)];
                Array.Copy((Array) parameters, (Array) Parameters, Parameters.Length);
              }
              ReturnType = member2.PropertyType;
              break;
          }
          if (Symbols.AreParametersAndReturnTypesValid(Parameters, ReturnType))
            checked { ++num1; }
          else
            Members[index1] = (MemberInfo) null;
          checked { ++index1; }
        }
        if (num1 == Members.Length)
          return Members;
        if (num1 <= 0)
          return (MemberInfo[]) null;
        MemberInfo[] memberInfoArray = new MemberInfo[checked (num1 - 1 + 1)];
        int index2 = 0;
        int num4 = 0;
        int num5 = checked (Members.Length - 1);
        int index3 = num4;
        while (index3 <= num5)
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

      internal MemberInfo[] LookupNamedMembers(string MemberName)
      {
        MemberInfo[] Members;
        if (Symbols.IsGenericParameter(this.m_Type))
        {
          Type classConstraint = Symbols.GetClassConstraint(this.m_Type);
          Members = classConstraint == null ? (MemberInfo[]) null : classConstraint.GetMember(MemberName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        }
        else
          Members = this.m_IReflect.GetMember(MemberName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        MemberInfo[] memberInfoArray = Symbols.Container.FilterInvalidMembers(Members);
        if (memberInfoArray == null)
          memberInfoArray = Symbols.Container.NoMembers;
        else if (memberInfoArray.Length > 1)
          Array.Sort((Array) memberInfoArray, (IComparer) Symbols.Container.InheritanceSorter.Instance);
        return memberInfoArray;
      }

      private MemberInfo[] LookupDefaultMembers(ref string DefaultMemberName)
      {
        string name = (string) null;
        Type Type = this.m_Type;
        do
        {
          object[] customAttributes = Type.GetCustomAttributes(typeof (DefaultMemberAttribute), false);
          if (customAttributes != null && customAttributes.Length > 0)
          {
            name = ((DefaultMemberAttribute) customAttributes[0]).MemberName;
            break;
          }
          Type = Type.BaseType;
        }
        while (Type != null && !Symbols.IsRootObjectType(Type));
        if (name != null)
        {
          MemberInfo[] memberInfoArray = Symbols.Container.FilterInvalidMembers(Type.GetMember(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
          if (memberInfoArray != null)
          {
            DefaultMemberName = name;
            if (memberInfoArray.Length > 1)
              Array.Sort((Array) memberInfoArray, (IComparer) Symbols.Container.InheritanceSorter.Instance);
            return memberInfoArray;
          }
        }
        return Symbols.Container.NoMembers;
      }

      internal MemberInfo[] GetMembers(ref string MemberName, bool ReportErrors)
      {
        if (MemberName == null)
          MemberName = "";
        MemberInfo[] memberInfoArray;
        if (Operators.CompareString(MemberName, "", false) == 0)
        {
          memberInfoArray = !this.m_UseCustomReflection ? this.LookupDefaultMembers(ref MemberName) : this.LookupNamedMembers(MemberName);
          if (memberInfoArray.Length == 0)
          {
            if (ReportErrors)
              throw new MissingMemberException(Utils.GetResourceString("MissingMember_NoDefaultMemberFound1", new string[1]
              {
                this.VBFriendlyName
              }));
            return memberInfoArray;
          }
          if (this.m_UseCustomReflection)
            MemberName = memberInfoArray[0].Name;
        }
        else
        {
          memberInfoArray = this.LookupNamedMembers(MemberName);
          if (memberInfoArray.Length == 0 && ReportErrors)
            throw new MissingMemberException(Utils.GetResourceString("MissingMember_MemberNotFoundOnType2", MemberName, this.VBFriendlyName));
        }
        return memberInfoArray;
      }

      private void CheckForClassExtendingCOMClass()
      {
        if (this.IsCOMObject && Operators.CompareString(this.m_Type.FullName, "System.__ComObject", false) != 0 && Operators.CompareString(this.m_Type.BaseType.FullName, "System.__ComObject", false) != 0)
          throw new InvalidOperationException(Utils.GetResourceString("LateboundCallToInheritedComClass"));
      }

      internal object GetFieldValue(FieldInfo Field)
      {
        if (this.m_Instance == null && !Symbols.IsShared((MemberInfo) Field))
          throw new NullReferenceException(Utils.GetResourceString("NullReference_InstanceReqToAccessMember1", new string[1]
          {
            Utils.FieldToString(Field)
          }));
        if (Symbols.IsNonPublicRuntimeMember((MemberInfo) Field))
          throw new MissingMemberException();
        return Field.GetValue(this.m_Instance);
      }

      internal void SetFieldValue(FieldInfo Field, object Value)
      {
        if (Field.IsInitOnly)
          throw new MissingMemberException(Utils.GetResourceString("MissingMember_ReadOnlyField2", Field.Name, this.VBFriendlyName));
        if (this.m_Instance == null && !Symbols.IsShared((MemberInfo) Field))
          throw new NullReferenceException(Utils.GetResourceString("NullReference_InstanceReqToAccessMember1", new string[1]
          {
            Utils.FieldToString(Field)
          }));
        if (Symbols.IsNonPublicRuntimeMember((MemberInfo) Field))
          throw new MissingMemberException();
        Field.SetValue(this.m_Instance, Conversions.ChangeType(Value, Field.FieldType));
      }

      internal object GetArrayValue(object[] Indices)
      {
        Array instance = (Array) this.m_Instance;
        int rank = instance.Rank;
        if (Indices.Length != rank)
          throw new RankException();
        int num1 = (int) Conversions.ChangeType(Indices[0], typeof (int));
        if (rank == 1)
          return instance.GetValue(num1);
        int index2 = (int) Conversions.ChangeType(Indices[1], typeof (int));
        if (rank == 2)
          return instance.GetValue(num1, index2);
        int index3 = (int) Conversions.ChangeType(Indices[2], typeof (int));
        if (rank == 3)
          return instance.GetValue(num1, index2, index3);
        int[] numArray = new int[checked (rank - 1 + 1)];
        numArray[0] = num1;
        numArray[1] = index2;
        numArray[2] = index3;
        int num2 = 3;
        int num3 = checked (rank - 1);
        int index = num2;
        while (index <= num3)
        {
          numArray[index] = (int) Conversions.ChangeType(Indices[index], typeof (int));
          checked { ++index; }
        }
        return instance.GetValue(numArray);
      }

      internal void SetArrayValue(object[] Arguments)
      {
        Array instance = (Array) this.m_Instance;
        int rank = instance.Rank;
        if (checked (Arguments.Length - 1) != rank)
          throw new RankException();
        object Expression = Arguments[checked (Arguments.Length - 1)];
        Type elementType = this.m_Type.GetElementType();
        int num1 = (int) Conversions.ChangeType(Arguments[0], typeof (int));
        if (rank == 1)
        {
          instance.SetValue(Conversions.ChangeType(Expression, elementType), num1);
        }
        else
        {
          int index2 = (int) Conversions.ChangeType(Arguments[1], typeof (int));
          if (rank == 2)
          {
            instance.SetValue(Conversions.ChangeType(Expression, elementType), num1, index2);
          }
          else
          {
            int index3 = (int) Conversions.ChangeType(Arguments[2], typeof (int));
            if (rank == 3)
            {
              instance.SetValue(Conversions.ChangeType(Expression, elementType), num1, index2, index3);
            }
            else
            {
              int[] numArray = new int[checked (rank - 1 + 1)];
              numArray[0] = num1;
              numArray[1] = index2;
              numArray[2] = index3;
              int num2 = 3;
              int num3 = checked (rank - 1);
              int index = num2;
              while (index <= num3)
              {
                numArray[index] = (int) Conversions.ChangeType(Arguments[index], typeof (int));
                checked { ++index; }
              }
              instance.SetValue(Conversions.ChangeType(Expression, elementType), numArray);
            }
          }
        }
      }

      internal object InvokeMethod(Symbols.Method TargetProcedure, object[] Arguments, bool[] CopyBack, BindingFlags Flags)
      {
        MethodBase callTarget = NewLateBinding.GetCallTarget(TargetProcedure, Flags);
        object[] objArray = NewLateBinding.ConstructCallArguments(TargetProcedure, Arguments, Flags);
        if (this.m_Instance == null && !Symbols.IsShared((MemberInfo) callTarget))
          throw new NullReferenceException(Utils.GetResourceString("NullReference_InstanceReqToAccessMember1", new string[1]
          {
            TargetProcedure.ToString()
          }));
        if (Symbols.IsNonPublicRuntimeMember((MemberInfo) callTarget))
          throw new MissingMemberException();
        object obj;
        try
        {
          obj = callTarget.Invoke(this.m_Instance, objArray);
        }
        catch (TargetInvocationException ex) when (ex.InnerException != null)
        {
          throw ex.InnerException;
        }
        OverloadResolution.ReorderArgumentArray(TargetProcedure, objArray, Arguments, CopyBack, Flags);
        return obj;
      }

      private class InheritanceSorter : IComparer
      {
        internal static readonly Symbols.Container.InheritanceSorter Instance = new Symbols.Container.InheritanceSorter();

        private InheritanceSorter()
        {
        }

        int IComparer.Compare(object Left, object Right)
        {
          Type declaringType1 = ((MemberInfo) Left).DeclaringType;
          Type declaringType2 = ((MemberInfo) Right).DeclaringType;
          if (declaringType1 == declaringType2)
            return 0;
          return declaringType1.IsSubclassOf(declaringType2) ? -1 : 1;
        }
      }
    }

    internal sealed class Method
    {
      private MemberInfo m_Item;
      private MethodBase m_RawItem;
      private ParameterInfo[] m_Parameters;
      private ParameterInfo[] m_RawParameters;
      private ParameterInfo[] m_RawParametersFromType;
      private Type m_RawDeclaringType;
      internal readonly int ParamArrayIndex;
      internal readonly bool ParamArrayExpanded;
      internal bool NotCallable;
      internal bool RequiresNarrowingConversion;
      internal bool AllNarrowingIsFromObject;
      internal bool LessSpecific;
      internal bool ArgumentsValidated;
      internal int[] NamedArgumentMapping;
      internal Type[] TypeArguments;
      internal bool ArgumentMatchingDone;

      private Method(ParameterInfo[] Parameters, int ParamArrayIndex, bool ParamArrayExpanded)
      {
        this.m_Parameters = Parameters;
        this.m_RawParameters = Parameters;
        this.ParamArrayIndex = ParamArrayIndex;
        this.ParamArrayExpanded = ParamArrayExpanded;
        this.AllNarrowingIsFromObject = true;
      }

      internal Method(MethodBase Method, ParameterInfo[] Parameters, int ParamArrayIndex, bool ParamArrayExpanded)
        : this(Parameters, ParamArrayIndex, ParamArrayExpanded)
      {
        this.m_Item = (MemberInfo) Method;
        this.m_RawItem = Method;
      }

      internal Method(PropertyInfo Property, ParameterInfo[] Parameters, int ParamArrayIndex, bool ParamArrayExpanded)
        : this(Parameters, ParamArrayIndex, ParamArrayExpanded)
      {
        this.m_Item = (MemberInfo) Property;
      }

      internal ParameterInfo[] Parameters
      {
        get
        {
          return this.m_Parameters;
        }
      }

      internal ParameterInfo[] RawParameters
      {
        get
        {
          return this.m_RawParameters;
        }
      }

      internal ParameterInfo[] RawParametersFromType
      {
        get
        {
          if (this.m_RawParametersFromType == null)
            this.m_RawParametersFromType = this.IsProperty ? this.m_RawParameters : this.m_Item.DeclaringType.Module.ResolveMethod(this.m_Item.MetadataToken, (Type[]) null, (Type[]) null).GetParameters();
          return this.m_RawParametersFromType;
        }
      }

      internal Type DeclaringType
      {
        get
        {
          return this.m_Item.DeclaringType;
        }
      }

      internal Type RawDeclaringType
      {
        get
        {
          if (this.m_RawDeclaringType == null)
          {
            Type declaringType = this.m_Item.DeclaringType;
            int metadataToken = declaringType.MetadataToken;
            this.m_RawDeclaringType = declaringType.Module.ResolveType(metadataToken, (Type[]) null, (Type[]) null);
          }
          return this.m_RawDeclaringType;
        }
      }

      internal bool HasParamArray
      {
        get
        {
          return this.ParamArrayIndex > -1;
        }
      }

      internal bool HasByRefParameter
      {
        get
        {
          ParameterInfo[] parameters = this.Parameters;
          int index = 0;
          while (index < parameters.Length)
          {
            if (parameters[index].ParameterType.IsByRef)
              return true;
            checked { ++index; }
          }
          return false;
        }
      }

      internal bool IsProperty
      {
        get
        {
          return this.m_Item.MemberType == MemberTypes.Property;
        }
      }

      internal bool IsMethod
      {
        get
        {
          return this.m_Item.MemberType == MemberTypes.Method || this.m_Item.MemberType == MemberTypes.Constructor;
        }
      }

      internal bool IsGeneric
      {
        get
        {
          return Symbols.IsGeneric(this.m_Item);
        }
      }

      internal Type[] TypeParameters
      {
        get
        {
          return Symbols.GetTypeParameters(this.m_Item);
        }
      }

      internal bool BindGenericArguments()
      {
        try
        {
          this.m_Item = (MemberInfo) ((MethodInfo) this.m_RawItem).MakeGenericMethod(this.TypeArguments);
          this.m_Parameters = this.AsMethod().GetParameters();
          return true;
        }
        catch (ArgumentException ex)
        {
          return false;
        }
      }

      internal MethodBase AsMethod()
      {
        return this.m_Item as MethodBase;
      }

      internal PropertyInfo AsProperty()
      {
        return this.m_Item as PropertyInfo;
      }

      public static bool operator ==(Symbols.Method Left, Symbols.Method Right)
      {
        return Left.m_Item == Right.m_Item;
      }

      public static bool operator !=(Symbols.Method Left, Symbols.Method right)
      {
        return Left.m_Item != right.m_Item;
      }

      public static bool operator ==(MemberInfo Left, Symbols.Method Right)
      {
        return Left == Right.m_Item;
      }

      public static bool operator !=(MemberInfo Left, Symbols.Method Right)
      {
        return Left != Right.m_Item;
      }

      public override string ToString()
      {
        return Utils.MemberToString(this.m_Item);
      }
    }

    internal sealed class TypedNothing
    {
      internal readonly Type Type;

      internal TypedNothing(Type Type)
      {
        this.Type = Type;
      }
    }
  }
}
