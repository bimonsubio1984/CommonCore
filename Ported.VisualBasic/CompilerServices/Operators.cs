// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.Operators
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>Provides late-bound math operators, such as <see cref="M:Ported.VisualBasic.CompilerServices.Operators.AddObject(System.Object,System.Object)" /> and <see cref="M:Ported.VisualBasic.CompilerServices.Operators.CompareObject(System.Object,System.Object,System.Boolean)" />, which the Visual Basic compiler uses internally. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class Operators
  {
    internal static readonly object Boxed_ZeroDouble = (object) 0.0;
    internal static readonly object Boxed_ZeroSinge = (object) 0.0f;
    internal static readonly object Boxed_ZeroDecimal = (object) Decimal.Zero;
    internal static readonly object Boxed_ZeroLong = (object) 0L;
    internal static readonly object Boxed_ZeroInteger = (object) 0;
    internal static readonly object Boxed_ZeroShort = (object) (short) 0;
    internal static readonly object Boxed_ZeroULong = (object) 0UL;
    internal static readonly object Boxed_ZeroUInteger = (object) 0U;
    internal static readonly object Boxed_ZeroUShort = (object) (ushort) 0;
    internal static readonly object Boxed_ZeroSByte = (object) (sbyte) 0;
    internal static readonly object Boxed_ZeroByte = (object) (byte) 0;
    private const int TCMAX = 19;

    internal static List<Symbols.Method> CollectOperators(Symbols.UserDefinedOperator Op, Type Type1, Type Type2, ref bool FoundType1Operators, ref bool FoundType2Operators)
    {
      bool flag = Type2 != null;
      int num1;
      List<Symbols.Method> methodList1;
      if (!Symbols.IsRootObjectType(Type1) && Symbols.IsClassOrValueType(Type1))
      {
        MemberInfo[] Members = new Symbols.Container(Type1).LookupNamedMembers(Symbols.OperatorCLSNames[(int) Op]);
        // ISSUE: variable of the null type
        object local1 = null;
        
        //int ArgumentCount = Interaction.IIf<int>(Symbols.IsUnaryOperator(Op), 1, 2);
        int ArgumentCount;
        if (Symbols.IsUnaryOperator(Op)) ArgumentCount=1; else ArgumentCount=2;
       
        // ISSUE: variable of the null type
        object local2 = null;
                // ISSUE: variable of the null type
        object local3 = null;
        int num2 = 1;
                // ISSUE: variable of the null type
        object local4 = null;
        num1 = 0;
        ref int local5 = ref num1;
        int num3 = 0;
        ref int local6 = ref num3;
        methodList1 = OverloadResolution.CollectOverloadCandidates(Members, (object[]) local1, ArgumentCount, (string[]) local2, (Type[]) local3, num2 != 0, (Type) local4, ref local5, ref local6);
        if (methodList1.Count > 0)
          FoundType1Operators = true;
      }
      else
        methodList1 = new List<Symbols.Method>();
      if (flag && !Symbols.IsRootObjectType(Type2) && Symbols.IsClassOrValueType(Type2))
      {
        Type Base = Type1;
        while (Base != null && !Symbols.IsOrInheritsFrom(Type2, Base))
          Base = Base.BaseType;
        MemberInfo[] Members = new Symbols.Container(Type2).LookupNamedMembers(Symbols.OperatorCLSNames[(int) Op]);
                // ISSUE: variable of the null type
        object local1 = null;
        
        //int ArgumentCount = Interaction.IIf<int>(Symbols.IsUnaryOperator(Op), 1, 2);
        int ArgumentCount;
        if (Symbols.IsUnaryOperator(Op)) ArgumentCount = 1;
        else ArgumentCount = 2; 
        
                // ISSUE: variable of the null type
        object local2 = null;
                // ISSUE: variable of the null type
        object local3 = null;
        int num2 = 1;
        Type TerminatingScope = Base;
        int num3 = 0;
        ref int local4 = ref num3;
        num1 = 0;
        ref int local5 = ref num1;
        List<Symbols.Method> methodList2 = OverloadResolution.CollectOverloadCandidates(Members, (object[]) local1, ArgumentCount, (string[]) local2, (Type[]) local3, num2 != 0, TerminatingScope, ref local4, ref local5);
        if (methodList2.Count > 0)
          FoundType2Operators = true;
        methodList1.AddRange((IEnumerable<Symbols.Method>) methodList2);
      }
      return methodList1;
    }

    internal static Symbols.Method ResolveUserDefinedOperator(Symbols.UserDefinedOperator Op, object[] Arguments, bool ReportErrors)
    {
      Arguments = (object[]) Arguments.Clone();
      Type type1 = (Type) null;
      Type type2;
      if (Arguments[0] == null)
      {
        type1 = Arguments[1].GetType();
        type2 = type1;
        Arguments[0] = (object) new Symbols.TypedNothing(type2);
      }
      else
      {
        type2 = Arguments[0].GetType();
        if (Arguments.Length > 1)
        {
          if (Arguments[1] != null)
          {
            type1 = Arguments[1].GetType();
          }
          else
          {
            type1 = type2;
            Arguments[1] = (object) new Symbols.TypedNothing(type1);
          }
        }
      }
      bool FoundType1Operators = false;
      bool FoundType2Operators = false;
      List<Symbols.Method> Candidates = Operators.CollectOperators(Op, type2, type1, ref FoundType1Operators, ref FoundType2Operators);
      if (Candidates.Count > 0)
      {
        OverloadResolution.ResolutionFailure Failure= OverloadResolution.ResolutionFailure.None;
        return OverloadResolution.ResolveOverloadedCall(Symbols.OperatorNames[(int) Op], Candidates, Arguments, Symbols.NoArgumentNames, Symbols.NoTypeArguments, BindingFlags.InvokeMethod, ReportErrors, ref Failure);
      }
      return (Symbols.Method) null;
    }

    internal static object InvokeUserDefinedOperator(Symbols.Method OperatorMethod, bool ForceArgumentValidation, params object[] Arguments)
    {
      if ((!OperatorMethod.ArgumentsValidated || ForceArgumentValidation) && !OverloadResolution.CanMatchArguments(OperatorMethod, Arguments, Symbols.NoArgumentNames, Symbols.NoTypeArguments, false, (List<string>) null))
      {
        string str = "";
        List<string> Errors = new List<string>();
        OverloadResolution.CanMatchArguments(OperatorMethod, Arguments, Symbols.NoArgumentNames, Symbols.NoTypeArguments, false, Errors);
        List<string>.Enumerator enumerator=Ported.VisualBasic.CompilerServices.OverloadResolution.InitEnumerator<string>();
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
        throw new InvalidCastException(Utils.GetResourceString("MatchArgumentFailure2", OperatorMethod.ToString(), str));
      }
      return new Symbols.Container(OperatorMethod.DeclaringType).InvokeMethod(OperatorMethod, Arguments, (bool[]) null, BindingFlags.InvokeMethod);
    }

    internal static object InvokeUserDefinedOperator(Symbols.UserDefinedOperator Op, params object[] Arguments)
    {
      Symbols.Method OperatorMethod = Operators.ResolveUserDefinedOperator(Op, Arguments, true);
      if ((object) OperatorMethod != null)
        return Operators.InvokeUserDefinedOperator(OperatorMethod, false, Arguments);
      if (Arguments.Length > 1)
        throw Operators.GetNoValidOperatorException(Op, Arguments[0], Arguments[1]);
      throw Operators.GetNoValidOperatorException(Op, Arguments[0]);
    }

    internal static Symbols.Method GetCallableUserDefinedOperator(Symbols.UserDefinedOperator Op, params object[] Arguments)
    {
      Symbols.Method TargetProcedure = Operators.ResolveUserDefinedOperator(Op, Arguments, false);
      if ((object) TargetProcedure != null && !TargetProcedure.ArgumentsValidated && !OverloadResolution.CanMatchArguments(TargetProcedure, Arguments, Symbols.NoArgumentNames, Symbols.NoTypeArguments, false, (List<string>) null))
        return (Symbols.Method) null;
      return TargetProcedure;
    }

    private Operators()
    {
    }

    private static sbyte ToVBBool(IConvertible conv)
    {
      return (sbyte) -(conv.ToBoolean((IFormatProvider) null) ? 1 : 0);
    }

    private static IConvertible ToVBBoolConv(IConvertible conv)
    {
      return (IConvertible) (sbyte) -(conv.ToBoolean((IFormatProvider) null) ? 1 : 0);
    }

    private static Type GetEnumResult(object Left, object Right)
    {
      if (Left != null)
      {
        if (Left is Enum)
        {
          if (Right == null)
            return Left.GetType();
          if (Right is Enum)
          {
            Type type = Left.GetType();
            if (type == Right.GetType())
              return type;
          }
        }
      }
      else if (Right is Enum)
        return Right.GetType();
      return (Type) null;
    }

    private static Exception GetNoValidOperatorException(Symbols.UserDefinedOperator Op, object Operand)
    {
      return (Exception) new InvalidCastException(Utils.GetResourceString("UnaryOperand2", Symbols.OperatorNames[(int) Op], Utils.VBFriendlyName(Operand)));
    }

    private static Exception GetNoValidOperatorException(Symbols.UserDefinedOperator Op, object Left, object Right)
    {
      string str1;
      if (Left == null)
      {
        str1 = "'Nothing'";
      }
      else
      {
        string str2 = Left as string;
        if (str2 != null)
          str1 = Utils.GetResourceString("NoValidOperator_StringType1", new string[1]
          {
            Strings.Left(str2, 32)
          });
        else
          str1 = Utils.GetResourceString("NoValidOperator_NonStringType1", new string[1]
          {
            Utils.VBFriendlyName(Left)
          });
      }
      string str3;
      if (Right == null)
      {
        str3 = "'Nothing'";
      }
      else
      {
        string str2 = Right as string;
        if (str2 != null)
          str3 = Utils.GetResourceString("NoValidOperator_StringType1", new string[1]
          {
            Strings.Left(str2, 32)
          });
        else
          str3 = Utils.GetResourceString("NoValidOperator_NonStringType1", new string[1]
          {
            Utils.VBFriendlyName(Right)
          });
      }
      return (Exception) new InvalidCastException(Utils.GetResourceString("BinaryOperands3", Symbols.OperatorNames[(int) Op], str1, str3));
    }

    /// <summary>Represents the Visual Basic equal (=) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="Left" /> and <paramref name="Right" /> are equal; otherwise, <see langword="False" />.</returns>
    public static object CompareObjectEqual(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return (object) false;
        case Operators.CompareClass.UserDefined:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Equal, new object[2]
          {
            Left,
            Right
          });
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Equal, Left, Right);
        default:
          return (object) (compareClass == Operators.CompareClass.Equal);
      }
    }

    /// <summary>Represents the overloaded Visual Basic equals (=) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>The result of the overloaded equals operator. <see langword="False" /> if operator overloading is not supported.</returns>
    public static bool ConditionalCompareObjectEqual(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return false;
        case Operators.CompareClass.UserDefined:
          return Conversions.ToBoolean(Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Equal, new object[2]
          {
            Left,
            Right
          }));
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Equal, Left, Right);
        default:
          return compareClass == Operators.CompareClass.Equal;
      }
    }

    /// <summary>Represents the Visual Basic not-equal (&lt;&gt;) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="Left" /> is not equal to <paramref name="Right" />; otherwise, <see langword="False" />.</returns>
    public static object CompareObjectNotEqual(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return (object) true;
        case Operators.CompareClass.UserDefined:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.NotEqual, new object[2]
          {
            Left,
            Right
          });
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.NotEqual, Left, Right);
        default:
          return (object) (compareClass != Operators.CompareClass.Equal);
      }
    }

    /// <summary>Represents the overloaded Visual Basic not-equal (&lt;&gt;) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>The result of the overloaded not-equal operator. <see langword="False" /> if operator overloading is not supported.</returns>
    public static bool ConditionalCompareObjectNotEqual(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return true;
        case Operators.CompareClass.UserDefined:
          return Conversions.ToBoolean(Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.NotEqual, new object[2]
          {
            Left,
            Right
          }));
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.NotEqual, Left, Right);
        default:
          return compareClass != Operators.CompareClass.Equal;
      }
    }

    /// <summary>Represents the Visual Basic less-than (&lt;) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="Left" /> is less than <paramref name="Right" />; otherwise, <see langword="False" />.</returns>
    public static object CompareObjectLess(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return (object) false;
        case Operators.CompareClass.UserDefined:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Less, new object[2]
          {
            Left,
            Right
          });
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Less, Left, Right);
        default:
          return (object) (compareClass < Operators.CompareClass.Equal);
      }
    }

    /// <summary>Represents the overloaded Visual Basic less-than (&lt;) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>The result of the overloaded less-than operator. <see langword="False" /> if operator overloading is not supported.</returns>
    public static bool ConditionalCompareObjectLess(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return false;
        case Operators.CompareClass.UserDefined:
          return Conversions.ToBoolean(Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Less, new object[2]
          {
            Left,
            Right
          }));
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Less, Left, Right);
        default:
          return compareClass < Operators.CompareClass.Equal;
      }
    }

    /// <summary>Represents the Visual Basic less-than or equal-to (&lt;=) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="Left" /> is less than or equal to <paramref name="Right" />; otherwise, <see langword="False" />.</returns>
    public static object CompareObjectLessEqual(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return (object) false;
        case Operators.CompareClass.UserDefined:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.LessEqual, new object[2]
          {
            Left,
            Right
          });
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.LessEqual, Left, Right);
        default:
          return (object) (compareClass <= Operators.CompareClass.Equal);
      }
    }

    /// <summary>Represents the overloaded Visual Basic less-than or equal-to (&lt;=) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>The result of the overloaded less-than or equal-to operator. <see langword="False" /> if operator overloading is not supported.</returns>
    public static bool ConditionalCompareObjectLessEqual(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return false;
        case Operators.CompareClass.UserDefined:
          return Conversions.ToBoolean(Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.LessEqual, new object[2]
          {
            Left,
            Right
          }));
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.LessEqual, Left, Right);
        default:
          return compareClass <= Operators.CompareClass.Equal;
      }
    }

    /// <summary>Represents the Visual Basic greater-than or equal-to (&gt;=) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="Left" /> is greater than or equal to <paramref name="Right" />; otherwise, <see langword="False" />.</returns>
    public static object CompareObjectGreaterEqual(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return (object) false;
        case Operators.CompareClass.UserDefined:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.GreaterEqual, new object[2]
          {
            Left,
            Right
          });
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.GreaterEqual, Left, Right);
        default:
          return (object) (compareClass >= Operators.CompareClass.Equal);
      }
    }

    /// <summary>Represents the overloaded Visual Basic greater-than or equal-to (&gt;=) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>The result of the overloaded greater-than or equal-to operator. <see langword="False" /> if operator overloading is not supported.</returns>
    public static bool ConditionalCompareObjectGreaterEqual(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return false;
        case Operators.CompareClass.UserDefined:
          return Conversions.ToBoolean(Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.GreaterEqual, new object[2]
          {
            Left,
            Right
          }));
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.GreaterEqual, Left, Right);
        default:
          return compareClass >= Operators.CompareClass.Equal;
      }
    }

    /// <summary>Represents the Visual Basic greater-than (&gt;) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="Left" /> is greater than <paramref name="Right" />; otherwise, <see langword="False" />.</returns>
    public static object CompareObjectGreater(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return (object) false;
        case Operators.CompareClass.UserDefined:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Greater, new object[2]
          {
            Left,
            Right
          });
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Greater, Left, Right);
        default:
          return (object) (compareClass > Operators.CompareClass.Equal);
      }
    }

    /// <summary>Represents the overloaded Visual Basic greater-than (&gt;) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>The result of the overloaded greater-than operator. <see langword="False" /> if operator overloading is not supported.</returns>
    public static bool ConditionalCompareObjectGreater(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return false;
        case Operators.CompareClass.UserDefined:
          return Conversions.ToBoolean(Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Greater, new object[2]
          {
            Left,
            Right
          }));
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Greater, Left, Right);
        default:
          return compareClass > Operators.CompareClass.Equal;
      }
    }

    /// <summary>Represents Visual Basic comparison operators.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>Value Condition -1
    /// <paramref name="Left" /> is less than <paramref name="Right" />.0
    /// <paramref name="Left" /> and <paramref name="Right" /> are equal.1
    /// <paramref name="Left" /> is greater than <paramref name="Right" />.</returns>
    public static int CompareObject(object Left, object Right, bool TextCompare)
    {
      Operators.CompareClass compareClass = Operators.CompareObject2(Left, Right, TextCompare);
      switch (compareClass)
      {
        case Operators.CompareClass.Unordered:
          return 0;
        case Operators.CompareClass.UserDefined:
        case Operators.CompareClass.Undefined:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.IsTrue, Left, Right);
        default:
          return (int) compareClass;
      }
    }

    private static Operators.CompareClass CompareObject2(object Left, object Right, bool TextCompare)
    {
      IConvertible convertible1 = Left as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Right as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      if (typeCode1 == TypeCode.Object)
      {
        char[] chArray = Left as char[];
        if (chArray != null && (typeCode2 == TypeCode.String || typeCode2 == TypeCode.Empty || typeCode2 == TypeCode.Object && Right is char[]))
        {
          Left = (object) new string(chArray);
          convertible1 = (IConvertible) Left;
          typeCode1 = TypeCode.String;
        }
      }
      if (typeCode2 == TypeCode.Object)
      {
        char[] chArray = Right as char[];
        if (chArray != null && (typeCode1 == TypeCode.String || typeCode1 == TypeCode.Empty))
        {
          Right = (object) new string(chArray);
          convertible2 = (IConvertible) Right;
          typeCode2 = TypeCode.String;
        }
      }
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.CompareClass.Equal;
        case TypeCode.Boolean:
          return Operators.CompareBoolean(false, convertible2.ToBoolean((IFormatProvider) null));
        case TypeCode.Char:
          return Operators.CompareChar(char.MinValue, convertible2.ToChar((IFormatProvider) null));
        case TypeCode.SByte:
          return Operators.CompareInt32(0, (int) convertible2.ToSByte((IFormatProvider) null));
        case TypeCode.Byte:
          return Operators.CompareInt32(0, (int) convertible2.ToByte((IFormatProvider) null));
        case TypeCode.Int16:
          return Operators.CompareInt32(0, (int) convertible2.ToInt16((IFormatProvider) null));
        case TypeCode.UInt16:
          return Operators.CompareInt32(0, (int) convertible2.ToUInt16((IFormatProvider) null));
        case TypeCode.Int32:
          return Operators.CompareInt32(0, convertible2.ToInt32((IFormatProvider) null));
        case TypeCode.UInt32:
          return Operators.CompareUInt32(0U, convertible2.ToUInt32((IFormatProvider) null));
        case TypeCode.Int64:
          return Operators.CompareInt64(0L, convertible2.ToInt64((IFormatProvider) null));
        case TypeCode.UInt64:
          return Operators.CompareUInt64(0UL, convertible2.ToUInt64((IFormatProvider) null));
        case TypeCode.Single:
          return Operators.CompareSingle(0.0f, convertible2.ToSingle((IFormatProvider) null));
        case TypeCode.Double:
          return Operators.CompareDouble(0.0, convertible2.ToDouble((IFormatProvider) null));
        case TypeCode.Decimal:
          return Operators.CompareDecimal((IConvertible) Decimal.Zero, convertible2);
        case TypeCode.DateTime:
          return Operators.CompareDate(DateTime.MinValue, convertible2.ToDateTime((IFormatProvider) null));
        case TypeCode.String:
          return (Operators.CompareClass) Operators.CompareString((string) null, convertible2.ToString((IFormatProvider) null), TextCompare);
        case (TypeCode) 57:
          return Operators.CompareBoolean(convertible1.ToBoolean((IFormatProvider) null), false);
        case (TypeCode) 60:
          return Operators.CompareBoolean(convertible1.ToBoolean((IFormatProvider) null), convertible2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 62:
          return Operators.CompareInt32((int) Operators.ToVBBool(convertible1), (int) convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.CompareInt32((int) Operators.ToVBBool(convertible1), (int) convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.CompareInt32((int) Operators.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 67:
        case (TypeCode) 68:
          return Operators.CompareInt64((long) Operators.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 69:
        case (TypeCode) 72:
          return Operators.CompareDecimal(Operators.ToVBBoolConv(convertible1), convertible2);
        case (TypeCode) 70:
          return Operators.CompareSingle((float) Operators.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return Operators.CompareDouble((double) Operators.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 75:
          return Operators.CompareBoolean(convertible1.ToBoolean((IFormatProvider) null), Conversions.ToBoolean(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 76:
          return Operators.CompareChar(convertible1.ToChar((IFormatProvider) null), char.MinValue);
        case (TypeCode) 80:
          return Operators.CompareChar(convertible1.ToChar((IFormatProvider) null), convertible2.ToChar((IFormatProvider) null));
        case (TypeCode) 94:
        case (TypeCode) 346:
        case (TypeCode) 360:
          return (Operators.CompareClass) Operators.CompareString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null), TextCompare);
        case (TypeCode) 95:
          return Operators.CompareInt32((int) convertible1.ToSByte((IFormatProvider) null), 0);
        case (TypeCode) 98:
          return Operators.CompareInt32((int) convertible1.ToSByte((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 100:
          return Operators.CompareInt32((int) convertible1.ToSByte((IFormatProvider) null), (int) convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return Operators.CompareInt32((int) convertible1.ToInt16((IFormatProvider) null), (int) convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
        case (TypeCode) 180:
          return Operators.CompareInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 125:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 163:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 220:
          return Operators.CompareInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 107:
        case (TypeCode) 110:
        case (TypeCode) 129:
        case (TypeCode) 145:
        case (TypeCode) 148:
        case (TypeCode) 167:
        case (TypeCode) 183:
        case (TypeCode) 186:
        case (TypeCode) 205:
        case (TypeCode) 221:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 243:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 300:
          return Operators.CompareDecimal(convertible1, convertible2);
        case (TypeCode) 108:
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 165:
        case (TypeCode) 184:
        case (TypeCode) 203:
        case (TypeCode) 222:
        case (TypeCode) 241:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return Operators.CompareSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 109:
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 166:
        case (TypeCode) 185:
        case (TypeCode) 204:
        case (TypeCode) 223:
        case (TypeCode) 242:
        case (TypeCode) 261:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return Operators.CompareDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.CompareDouble(convertible1.ToDouble((IFormatProvider) null), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 114:
          return Operators.CompareInt32((int) convertible1.ToByte((IFormatProvider) null), 0);
        case (TypeCode) 117:
          return Operators.CompareInt32((int) convertible1.ToInt16((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 120:
          return Operators.CompareInt32((int) convertible1.ToByte((IFormatProvider) null), (int) convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 122:
        case (TypeCode) 158:
        case (TypeCode) 160:
          return Operators.CompareInt32((int) convertible1.ToUInt16((IFormatProvider) null), (int) convertible2.ToUInt16((IFormatProvider) null));
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
        case (TypeCode) 200:
          return Operators.CompareUInt32(convertible1.ToUInt32((IFormatProvider) null), convertible2.ToUInt32((IFormatProvider) null));
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
        case (TypeCode) 240:
          return Operators.CompareUInt64(convertible1.ToUInt64((IFormatProvider) null), convertible2.ToUInt64((IFormatProvider) null));
        case (TypeCode) 133:
          return Operators.CompareInt32((int) convertible1.ToInt16((IFormatProvider) null), 0);
        case (TypeCode) 136:
          return Operators.CompareInt32((int) convertible1.ToInt16((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 152:
          return Operators.CompareInt32((int) convertible1.ToUInt16((IFormatProvider) null), 0);
        case (TypeCode) 155:
          return Operators.CompareInt32(convertible1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 171:
          return Operators.CompareInt32(convertible1.ToInt32((IFormatProvider) null), 0);
        case (TypeCode) 174:
          return Operators.CompareInt32(convertible1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 190:
          return Operators.CompareUInt32(convertible1.ToUInt32((IFormatProvider) null), 0U);
        case (TypeCode) 193:
          return Operators.CompareInt64(convertible1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(convertible2));
        case (TypeCode) 209:
          return Operators.CompareInt64(convertible1.ToInt64((IFormatProvider) null), 0L);
        case (TypeCode) 212:
          return Operators.CompareInt64(convertible1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(convertible2));
        case (TypeCode) 228:
          return Operators.CompareUInt64(convertible1.ToUInt64((IFormatProvider) null), 0UL);
        case (TypeCode) 231:
          return Operators.CompareDecimal(convertible1, Operators.ToVBBoolConv(convertible2));
        case (TypeCode) 247:
          return Operators.CompareSingle(convertible1.ToSingle((IFormatProvider) null), 0.0f);
        case (TypeCode) 250:
          return Operators.CompareSingle(convertible1.ToSingle((IFormatProvider) null), (float) Operators.ToVBBool(convertible2));
        case (TypeCode) 266:
          return Operators.CompareDouble(convertible1.ToDouble((IFormatProvider) null), 0.0);
        case (TypeCode) 269:
          return Operators.CompareDouble(convertible1.ToDouble((IFormatProvider) null), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 285:
          return Operators.CompareDecimal(convertible1, (IConvertible) Decimal.Zero);
        case (TypeCode) 288:
          return Operators.CompareDecimal(convertible1, Operators.ToVBBoolConv(convertible2));
        case (TypeCode) 304:
          return Operators.CompareDate(convertible1.ToDateTime((IFormatProvider) null), DateTime.MinValue);
        case (TypeCode) 320:
          return Operators.CompareDate(convertible1.ToDateTime((IFormatProvider) null), convertible2.ToDateTime((IFormatProvider) null));
        case (TypeCode) 322:
          return Operators.CompareDate(convertible1.ToDateTime((IFormatProvider) null), Conversions.ToDate(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 342:
          return (Operators.CompareClass) Operators.CompareString(convertible1.ToString((IFormatProvider) null), (string) null, TextCompare);
        case (TypeCode) 345:
          return Operators.CompareBoolean(Conversions.ToBoolean(convertible1.ToString((IFormatProvider) null)), convertible2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.CompareDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 358:
          return Operators.CompareDate(Conversions.ToDate(convertible1.ToString((IFormatProvider) null)), convertible2.ToDateTime((IFormatProvider) null));
        default:
          return typeCode1 == TypeCode.Object || typeCode2 == TypeCode.Object ? Operators.CompareClass.UserDefined : Operators.CompareClass.Undefined;
      }
    }

    private static Operators.CompareClass CompareBoolean(bool Left, bool Right)
    {
      if (Left == Right)
        return Operators.CompareClass.Equal;
      return (Left ? 1 : 0) < (Right ? 1 : 0) ? Operators.CompareClass.Greater : Operators.CompareClass.Less;
    }

    private static Operators.CompareClass CompareInt32(int Left, int Right)
    {
      if (Left == Right)
        return Operators.CompareClass.Equal;
      return Left > Right ? Operators.CompareClass.Greater : Operators.CompareClass.Less;
    }

    private static Operators.CompareClass CompareUInt32(uint Left, uint Right)
    {
      if ((int) Left == (int) Right)
        return Operators.CompareClass.Equal;
      return Left > Right ? Operators.CompareClass.Greater : Operators.CompareClass.Less;
    }

    private static Operators.CompareClass CompareInt64(long Left, long Right)
    {
      if (Left == Right)
        return Operators.CompareClass.Equal;
      return Left > Right ? Operators.CompareClass.Greater : Operators.CompareClass.Less;
    }

    private static Operators.CompareClass CompareUInt64(ulong Left, ulong Right)
    {
      if ((long) Left == (long) Right)
        return Operators.CompareClass.Equal;
      return Left > Right ? Operators.CompareClass.Greater : Operators.CompareClass.Less;
    }

    private static Operators.CompareClass CompareDecimal(IConvertible Left, IConvertible Right)
    {
      int num = Decimal.Compare(Left.ToDecimal((IFormatProvider) null), Right.ToDecimal((IFormatProvider) null));
      if (num == 0)
        return Operators.CompareClass.Equal;
      return num > 0 ? Operators.CompareClass.Greater : Operators.CompareClass.Less;
    }

    private static Operators.CompareClass CompareSingle(float Left, float Right)
    {
      if ((double) Left == (double) Right)
        return Operators.CompareClass.Equal;
      if ((double) Left < (double) Right)
        return Operators.CompareClass.Less;
      return (double) Left > (double) Right ? Operators.CompareClass.Greater : Operators.CompareClass.Unordered;
    }

    private static Operators.CompareClass CompareDouble(double Left, double Right)
    {
      if (Left == Right)
        return Operators.CompareClass.Equal;
      if (Left < Right)
        return Operators.CompareClass.Less;
      return Left > Right ? Operators.CompareClass.Greater : Operators.CompareClass.Unordered;
    }

    private static Operators.CompareClass CompareDate(DateTime Left, DateTime Right)
    {
      int num = DateTime.Compare(Left, Right);
      if (num == 0)
        return Operators.CompareClass.Equal;
      return num > 0 ? Operators.CompareClass.Greater : Operators.CompareClass.Less;
    }

    private static Operators.CompareClass CompareChar(char Left, char Right)
    {
      if ((int) Left == (int) Right)
        return Operators.CompareClass.Equal;
      return (int) Left > (int) Right ? Operators.CompareClass.Greater : Operators.CompareClass.Less;
    }

    /// <summary>Performs binary or text string comparison when given two strings.</summary>
    /// <param name="Left">Required. Any <see langword="String" /> expression.</param>
    /// <param name="Right">Required. Any <see langword="String" /> expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>Value Condition -1
    /// <paramref name="Left" /> is less than <paramref name="Right" />. 0
    /// <paramref name="Left" /> is equal to <paramref name="Right" />. 1
    /// <paramref name="Left" /> is greater than <paramref name="Right" />. </returns>
    public static int CompareString(string Left, string Right, bool TextCompare)
    {
      if ((object) Left == (object) Right)
        return 0;
      if (Left == null)
        return Right.Length == 0 ? 0 : -1;
      if (Right == null)
        return Left.Length == 0 ? 0 : 1;
      int num = !TextCompare ? string.CompareOrdinal(Left, Right) : Utils.GetCultureInfo().CompareInfo.Compare(Left, Right, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
      if (num == 0)
        return 0;
      return num > 0 ? 1 : -1;
    }

    /// <summary>Represents the Visual Basic unary plus (+) operator.</summary>
    /// <param name="Operand">Required. Any numeric expression.</param>
    /// <returns>The value of <paramref name="Operand" />. (The sign of the <paramref name="Operand" /> is unchanged.)</returns>
    public static object PlusObject(object Operand)
    {
      if (Operand == null)
        return Operators.Boxed_ZeroInteger;
      IConvertible convertible = Operand as IConvertible;
      switch (convertible != null ? (int) convertible.GetTypeCode() : (Operand != null ? 1 : 0))
      {
        case 0:
          return Operators.Boxed_ZeroInteger;
        case 1:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.UnaryPlus, Operand);
        case 3:
          return (object) (short) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
        case 5:
          return (object) convertible.ToSByte((IFormatProvider) null);
        case 6:
          return (object) convertible.ToByte((IFormatProvider) null);
        case 7:
          return (object) convertible.ToInt16((IFormatProvider) null);
        case 8:
          return (object) convertible.ToUInt16((IFormatProvider) null);
        case 9:
          return (object) convertible.ToInt32((IFormatProvider) null);
        case 10:
          return (object) convertible.ToUInt32((IFormatProvider) null);
        case 11:
          return (object) convertible.ToInt64((IFormatProvider) null);
        case 12:
          return (object) convertible.ToUInt64((IFormatProvider) null);
        case 13:
        case 14:
        case 15:
          return Operand;
        case 18:
          return (object) Conversions.ToDouble(convertible.ToString((IFormatProvider) null));
        default:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.UnaryPlus, Operand);
      }
    }

    /// <summary>Represents the Visual Basic unary minus (–) operator.</summary>
    /// <param name="Operand">Required. Any numeric expression.</param>
    /// <returns>The negative value of <paramref name="Operand" />.</returns>
    public static object NegateObject(object Operand)
    {
      IConvertible convertible = Operand as IConvertible;
      switch (convertible != null ? (int) convertible.GetTypeCode() : (Operand != null ? 1 : 0))
      {
        case 0:
          return Operators.Boxed_ZeroInteger;
        case 1:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Negate, Operand);
        case 3:
          if (Operand is bool)
            return Operators.NegateBoolean((bool) Operand);
          return Operators.NegateBoolean(convertible.ToBoolean((IFormatProvider) null));
        case 5:
          if (Operand is sbyte)
            return Operators.NegateSByte((sbyte) Operand);
          return Operators.NegateSByte(convertible.ToSByte((IFormatProvider) null));
        case 6:
          if (Operand is byte)
            return Operators.NegateByte((byte) Operand);
          return Operators.NegateByte(convertible.ToByte((IFormatProvider) null));
        case 7:
          if (Operand is short)
            return Operators.NegateInt16((short) Operand);
          return Operators.NegateInt16(convertible.ToInt16((IFormatProvider) null));
        case 8:
          if (Operand is ushort)
            return Operators.NegateUInt16((ushort) Operand);
          return Operators.NegateUInt16(convertible.ToUInt16((IFormatProvider) null));
        case 9:
          if (Operand is int)
            return Operators.NegateInt32((int) Operand);
          return Operators.NegateInt32(convertible.ToInt32((IFormatProvider) null));
        case 10:
          if (Operand is uint)
            return Operators.NegateUInt32((uint) Operand);
          return Operators.NegateUInt32(convertible.ToUInt32((IFormatProvider) null));
        case 11:
          if (Operand is long)
            return Operators.NegateInt64((long) Operand);
          return Operators.NegateInt64(convertible.ToInt64((IFormatProvider) null));
        case 12:
          if (Operand is ulong)
            return Operators.NegateUInt64((ulong) Operand);
          return Operators.NegateUInt64(convertible.ToUInt64((IFormatProvider) null));
        case 13:
          if (Operand is float)
            return Operators.NegateSingle((float) Operand);
          return Operators.NegateSingle(convertible.ToSingle((IFormatProvider) null));
        case 14:
          if (Operand is double)
            return Operators.NegateDouble((double) Operand);
          return Operators.NegateDouble(convertible.ToDouble((IFormatProvider) null));
        case 15:
          if (Operand is Decimal)
            return Operators.NegateDecimal((Decimal) Operand);
          return Operators.NegateDecimal(convertible.ToDecimal((IFormatProvider) null));
        case 18:
          string Operand1 = Operand as string;
          if (Operand1 != null)
            return Operators.NegateString(Operand1);
          return Operators.NegateString(convertible.ToString((IFormatProvider) null));
        default:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Negate, Operand);
      }
    }

    private static object NegateBoolean(bool Operand)
    {
      return (object) -(short) -(Operand ? 1 : 0);
    }

    private static object NegateSByte(sbyte Operand)
    {
      if (Operand == sbyte.MinValue)
        return (object) (short) 128;
      return (object) -Operand;
    }

    private static object NegateByte(byte Operand)
    {
      return (object) (short) -Operand;
    }

    private static object NegateInt16(short Operand)
    {
      if (Operand == short.MinValue)
        return (object) 32768;
      return (object) -Operand;
    }

    private static object NegateUInt16(ushort Operand)
    {
      return (object) (int) checked (-Operand);
    }

    private static object NegateInt32(int Operand)
    {
      if (Operand == int.MinValue)
        return (object) 2147483648L;
      return (object) checked (-Operand);
    }

    private static object NegateUInt32(uint Operand)
    {
      return (object) (long) checked (-Operand);
    }

    private static object NegateInt64(long Operand)
    {
      if (Operand == long.MinValue)
        return (object) new Decimal(0, int.MinValue, 0, false, (byte) 0);
      return (object) checked (-Operand);
    }

    private static object NegateUInt64(ulong Operand)
    {
      return (object) Decimal.Negate(new Decimal(Operand));
    }

    private static object NegateDecimal(Decimal Operand)
    {
      try
      {
        return (object) Decimal.Negate(Operand);
      }
      catch (OverflowException ex)
      {
        return (object) -Convert.ToDouble(Operand);
      }
    }

    private static object NegateSingle(float Operand)
    {
      return (object) (float) -(double) Operand;
    }

    private static object NegateDouble(double Operand)
    {
      return (object) -Operand;
    }

    private static object NegateString(string Operand)
    {
      return (object) -Conversions.ToDouble(Operand);
    }

    /// <summary>Represents the Visual Basic <see langword="Not" /> operator.</summary>
    /// <param name="Operand">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>For <see langword="Boolean" /> operations, <see langword="False" /> if <paramref name="Operand" /> is <see langword="True" />; otherwise, <see langword="True" />. For bitwise operations, 1 if <paramref name="Operand" /> is 0; otherwise, 0.</returns>
    public static object NotObject(object Operand)
    {
      IConvertible convertible = Operand as IConvertible;
      switch (convertible != null ? (int) convertible.GetTypeCode() : (Operand != null ? 1 : 0))
      {
        case 0:
          return (object) -1;
        case 1:
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Not, Operand);
        case 3:
          return Operators.NotBoolean(convertible.ToBoolean((IFormatProvider) null));
        case 5:
          return Operators.NotSByte(convertible.ToSByte((IFormatProvider) null), Operand.GetType());
        case 6:
          return Operators.NotByte(convertible.ToByte((IFormatProvider) null), Operand.GetType());
        case 7:
          return Operators.NotInt16(convertible.ToInt16((IFormatProvider) null), Operand.GetType());
        case 8:
          return Operators.NotUInt16(convertible.ToUInt16((IFormatProvider) null), Operand.GetType());
        case 9:
          return Operators.NotInt32(convertible.ToInt32((IFormatProvider) null), Operand.GetType());
        case 10:
          return Operators.NotUInt32(convertible.ToUInt32((IFormatProvider) null), Operand.GetType());
        case 11:
          return Operators.NotInt64(convertible.ToInt64((IFormatProvider) null), Operand.GetType());
        case 12:
          return Operators.NotUInt64(convertible.ToUInt64((IFormatProvider) null), Operand.GetType());
        case 13:
        case 14:
        case 15:
          return Operators.NotInt64(convertible.ToInt64((IFormatProvider) null));
        case 18:
          return Operators.NotInt64(Conversions.ToLong(convertible.ToString((IFormatProvider) null)));
        default:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Not, Operand);
      }
    }

    private static object NotBoolean(bool Operand)
    {
      return (object) !Operand;
    }

    private static object NotSByte(sbyte Operand, Type OperandType)
    {
      sbyte num = (sbyte)~Operand;
      if (OperandType.IsEnum)
        return Enum.ToObject(OperandType, num);
      return (object) num;
    }

    private static object NotByte(byte Operand, Type OperandType)
    {
      byte num = (byte)~Operand;
      if (OperandType.IsEnum)
        return Enum.ToObject(OperandType, num);
      return (object) num;
    }

    private static object NotInt16(short Operand, Type OperandType)
    {
      short num = (short)~Operand;
      if (OperandType.IsEnum)
        return Enum.ToObject(OperandType, num);
      return (object) num;
    }

    private static object NotUInt16(ushort Operand, Type OperandType)
    {
      ushort num = (ushort)~Operand;
      if (OperandType.IsEnum)
        return Enum.ToObject(OperandType, num);
      return (object) num;
    }

    private static object NotInt32(int Operand, Type OperandType)
    {
      int num = ~Operand;
      if (OperandType.IsEnum)
        return Enum.ToObject(OperandType, num);
      return (object) num;
    }

    private static object NotUInt32(uint Operand, Type OperandType)
    {
      uint num = ~Operand;
      if (OperandType.IsEnum)
        return Enum.ToObject(OperandType, num);
      return (object) num;
    }

    private static object NotInt64(long Operand)
    {
      return (object) ~Operand;
    }

    private static object NotInt64(long Operand, Type OperandType)
    {
      long num = ~Operand;
      if (OperandType.IsEnum)
        return Enum.ToObject(OperandType, num);
      return (object) num;
    }

    private static object NotUInt64(ulong Operand, Type OperandType)
    {
      ulong num = ~Operand;
      if (OperandType.IsEnum)
        return Enum.ToObject(OperandType, num);
      return (object) num;
    }

    /// <summary>Represents the Visual Basic <see langword="And" /> operator.</summary>
    /// <param name="Left">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <param name="Right">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>For <see langword="Boolean" /> operations, <see langword="True" /> if both <paramref name="Left" /> and <paramref name="Right" /> evaluate to <see langword="True" />; otherwise, <see langword="False" />. For bitwise operations, 1 if both <paramref name="Left" /> and <paramref name="Right" /> evaluate to 1; otherwise, 0.</returns>
    public static object AndObject(object Left, object Right)
    {
      IConvertible conv1 = Left as IConvertible;
      TypeCode typeCode1 = conv1 != null ? conv1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible conv2 = Right as IConvertible;
      TypeCode typeCode2 = conv2 != null ? conv2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.Boxed_ZeroInteger;
        case TypeCode.Boolean:
        case (TypeCode) 57:
          return (object) false;
        case TypeCode.SByte:
        case (TypeCode) 95:
          return Operators.AndSByte((sbyte) 0, (sbyte) 0, Operators.GetEnumResult(Left, Right));
        case TypeCode.Byte:
        case (TypeCode) 114:
          return Operators.AndByte((byte) 0, (byte) 0, Operators.GetEnumResult(Left, Right));
        case TypeCode.Int16:
        case (TypeCode) 133:
          return Operators.AndInt16((short) 0, (short) 0, Operators.GetEnumResult(Left, Right));
        case TypeCode.UInt16:
        case (TypeCode) 152:
          return Operators.AndUInt16((ushort) 0, (ushort) 0, Operators.GetEnumResult(Left, Right));
        case TypeCode.Int32:
        case (TypeCode) 171:
          return Operators.AndInt32(0, 0, Operators.GetEnumResult(Left, Right));
        case TypeCode.UInt32:
        case (TypeCode) 190:
          return Operators.AndUInt32(0U, 0U, Operators.GetEnumResult(Left, Right));
        case TypeCode.Int64:
        case (TypeCode) 209:
          return Operators.AndInt64(0L, 0L, Operators.GetEnumResult(Left, Right));
        case TypeCode.UInt64:
        case (TypeCode) 228:
          return Operators.AndUInt64(0UL, 0UL, Operators.GetEnumResult(Left, Right));
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return Operators.AndInt64(0L, conv2.ToInt64((IFormatProvider) null), (Type) null);
        case TypeCode.String:
          return Operators.AndInt64(0L, Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        case (TypeCode) 60:
          return Operators.AndBoolean(conv1.ToBoolean((IFormatProvider) null), conv2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 62:
          return Operators.AndSByte(Operators.ToVBBool(conv1), conv2.ToSByte((IFormatProvider) null), (Type) null);
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.AndInt16((short) Operators.ToVBBool(conv1), conv2.ToInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.AndInt32((int) Operators.ToVBBool(conv1), conv2.ToInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 67:
        case (TypeCode) 68:
        case (TypeCode) 69:
        case (TypeCode) 70:
        case (TypeCode) 71:
        case (TypeCode) 72:
          return Operators.AndInt64((long) Operators.ToVBBool(conv1), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 75:
          return Operators.AndBoolean(conv1.ToBoolean((IFormatProvider) null), Conversions.ToBoolean(conv2.ToString((IFormatProvider) null)));
        case (TypeCode) 98:
          return Operators.AndSByte(conv1.ToSByte((IFormatProvider) null), Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 100:
          return Operators.AndSByte(conv1.ToSByte((IFormatProvider) null), conv2.ToSByte((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
          return Operators.AndInt16(conv1.ToInt16((IFormatProvider) null), conv2.ToInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
          return Operators.AndInt32(conv1.ToInt32((IFormatProvider) null), conv2.ToInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 107:
        case (TypeCode) 108:
        case (TypeCode) 109:
        case (TypeCode) 110:
        case (TypeCode) 125:
        case (TypeCode) 127:
        case (TypeCode) 128:
        case (TypeCode) 129:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 145:
        case (TypeCode) 146:
        case (TypeCode) 147:
        case (TypeCode) 148:
        case (TypeCode) 163:
        case (TypeCode) 165:
        case (TypeCode) 166:
        case (TypeCode) 167:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 183:
        case (TypeCode) 184:
        case (TypeCode) 185:
        case (TypeCode) 186:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 203:
        case (TypeCode) 204:
        case (TypeCode) 205:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 221:
        case (TypeCode) 222:
        case (TypeCode) 223:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 241:
        case (TypeCode) 242:
        case (TypeCode) 243:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 261:
        case (TypeCode) 262:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 298:
        case (TypeCode) 299:
        case (TypeCode) 300:
          return Operators.AndInt64(conv1.ToInt64((IFormatProvider) null), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.AndInt64(conv1.ToInt64((IFormatProvider) null), Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return Operators.AndInt16(conv1.ToInt16((IFormatProvider) null), (short) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 120:
          return Operators.AndByte(conv1.ToByte((IFormatProvider) null), conv2.ToByte((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 122:
        case (TypeCode) 158:
          return Operators.AndUInt16(conv1.ToUInt16((IFormatProvider) null), conv2.ToUInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
          return Operators.AndUInt32(conv1.ToUInt32((IFormatProvider) null), conv2.ToUInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
          return Operators.AndUInt64(conv1.ToUInt64((IFormatProvider) null), conv2.ToUInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 140:
          return Operators.AndInt16(conv1.ToInt16((IFormatProvider) null), conv2.ToInt16((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 155:
        case (TypeCode) 174:
          return Operators.AndInt32(conv1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 160:
          return Operators.AndUInt16(conv1.ToUInt16((IFormatProvider) null), conv2.ToUInt16((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 180:
          return Operators.AndInt32(conv1.ToInt32((IFormatProvider) null), conv2.ToInt32((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 193:
        case (TypeCode) 212:
        case (TypeCode) 231:
        case (TypeCode) 250:
        case (TypeCode) 269:
        case (TypeCode) 288:
          return Operators.AndInt64(conv1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 200:
          return Operators.AndUInt32(conv1.ToUInt32((IFormatProvider) null), conv2.ToUInt32((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 220:
          return Operators.AndInt64(conv1.ToInt64((IFormatProvider) null), conv2.ToInt64((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 240:
          return Operators.AndUInt64(conv1.ToUInt64((IFormatProvider) null), conv2.ToUInt64((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return Operators.AndInt64(conv1.ToInt64((IFormatProvider) null), 0L, (Type) null);
        case (TypeCode) 342:
          return Operators.AndInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), 0L, (Type) null);
        case (TypeCode) 345:
          return Operators.AndBoolean(Conversions.ToBoolean(conv1.ToString((IFormatProvider) null)), conv2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.AndInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 360:
          return Operators.AndInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.And, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.And, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object AndBoolean(bool Left, bool Right)
    {
      return (object) (Left & Right);
    }

    private static object AndSByte(sbyte Left, sbyte Right, Type EnumType = null)
    {
      sbyte num = (sbyte) ((int) Left & (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object AndByte(byte Left, byte Right, Type EnumType = null)
    {
      byte num = (byte) ((int) Left & (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object AndInt16(short Left, short Right, Type EnumType = null)
    {
      short num = (short) ((int) Left & (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object AndUInt16(ushort Left, ushort Right, Type EnumType = null)
    {
      ushort num = (ushort) ((int) Left & (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object AndInt32(int Left, int Right, Type EnumType = null)
    {
      int num = Left & Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object AndUInt32(uint Left, uint Right, Type EnumType = null)
    {
      uint num = Left & Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object AndInt64(long Left, long Right, Type EnumType = null)
    {
      long num = Left & Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object AndUInt64(ulong Left, ulong Right, Type EnumType = null)
    {
      ulong num = Left & Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    /// <summary>Represents the Visual Basic <see langword="Or" /> operator.</summary>
    /// <param name="Left">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <param name="Right">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>For <see langword="Boolean" /> operations, <see langword="False" /> if both <paramref name="Left" /> and <paramref name="Right" /> evaluate to <see langword="False" />; otherwise, <see langword="True" />. For bitwise operations, 0 if both <paramref name="Left" /> and <paramref name="Right" /> evaluate to 0; otherwise, 1.</returns>
    public static object OrObject(object Left, object Right)
    {
      IConvertible conv1 = Left as IConvertible;
      TypeCode typeCode1 = conv1 != null ? conv1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible conv2 = Right as IConvertible;
      TypeCode typeCode2 = conv2 != null ? conv2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.Boxed_ZeroInteger;
        case TypeCode.Boolean:
          return Operators.OrBoolean(false, conv2.ToBoolean((IFormatProvider) null));
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
          return Right;
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return Operators.OrInt64(0L, conv2.ToInt64((IFormatProvider) null), (Type) null);
        case TypeCode.String:
          return Operators.OrInt64(0L, Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        case (TypeCode) 57:
          return Operators.OrBoolean(conv1.ToBoolean((IFormatProvider) null), false);
        case (TypeCode) 60:
          return Operators.OrBoolean(conv1.ToBoolean((IFormatProvider) null), conv2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 62:
          return Operators.OrSByte(Operators.ToVBBool(conv1), conv2.ToSByte((IFormatProvider) null), (Type) null);
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.OrInt16((short) Operators.ToVBBool(conv1), conv2.ToInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.OrInt32((int) Operators.ToVBBool(conv1), conv2.ToInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 67:
        case (TypeCode) 68:
        case (TypeCode) 69:
        case (TypeCode) 70:
        case (TypeCode) 71:
        case (TypeCode) 72:
          return Operators.OrInt64((long) Operators.ToVBBool(conv1), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 75:
          return Operators.OrBoolean(conv1.ToBoolean((IFormatProvider) null), Conversions.ToBoolean(conv2.ToString((IFormatProvider) null)));
        case (TypeCode) 95:
        case (TypeCode) 114:
        case (TypeCode) 133:
        case (TypeCode) 152:
        case (TypeCode) 171:
        case (TypeCode) 190:
        case (TypeCode) 209:
        case (TypeCode) 228:
          return Left;
        case (TypeCode) 98:
          return Operators.OrSByte(conv1.ToSByte((IFormatProvider) null), Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 100:
          return Operators.OrSByte(conv1.ToSByte((IFormatProvider) null), conv2.ToSByte((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
          return Operators.OrInt16(conv1.ToInt16((IFormatProvider) null), conv2.ToInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
          return Operators.OrInt32(conv1.ToInt32((IFormatProvider) null), conv2.ToInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 107:
        case (TypeCode) 108:
        case (TypeCode) 109:
        case (TypeCode) 110:
        case (TypeCode) 125:
        case (TypeCode) 127:
        case (TypeCode) 128:
        case (TypeCode) 129:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 145:
        case (TypeCode) 146:
        case (TypeCode) 147:
        case (TypeCode) 148:
        case (TypeCode) 163:
        case (TypeCode) 165:
        case (TypeCode) 166:
        case (TypeCode) 167:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 183:
        case (TypeCode) 184:
        case (TypeCode) 185:
        case (TypeCode) 186:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 203:
        case (TypeCode) 204:
        case (TypeCode) 205:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 221:
        case (TypeCode) 222:
        case (TypeCode) 223:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 241:
        case (TypeCode) 242:
        case (TypeCode) 243:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 261:
        case (TypeCode) 262:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 298:
        case (TypeCode) 299:
        case (TypeCode) 300:
          return Operators.OrInt64(conv1.ToInt64((IFormatProvider) null), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.OrInt64(conv1.ToInt64((IFormatProvider) null), Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return Operators.OrInt16(conv1.ToInt16((IFormatProvider) null), (short) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 120:
          return Operators.OrByte(conv1.ToByte((IFormatProvider) null), conv2.ToByte((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 122:
        case (TypeCode) 158:
          return Operators.OrUInt16(conv1.ToUInt16((IFormatProvider) null), conv2.ToUInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
          return Operators.OrUInt32(conv1.ToUInt32((IFormatProvider) null), conv2.ToUInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
          return Operators.OrUInt64(conv1.ToUInt64((IFormatProvider) null), conv2.ToUInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 140:
          return Operators.OrInt16(conv1.ToInt16((IFormatProvider) null), conv2.ToInt16((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 155:
        case (TypeCode) 174:
          return Operators.OrInt32(conv1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 160:
          return Operators.OrUInt16(conv1.ToUInt16((IFormatProvider) null), conv2.ToUInt16((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 180:
          return Operators.OrInt32(conv1.ToInt32((IFormatProvider) null), conv2.ToInt32((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 193:
        case (TypeCode) 212:
        case (TypeCode) 231:
        case (TypeCode) 250:
        case (TypeCode) 269:
        case (TypeCode) 288:
          return Operators.OrInt64(conv1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 200:
          return Operators.OrUInt32(conv1.ToUInt32((IFormatProvider) null), conv2.ToUInt32((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 220:
          return Operators.OrInt64(conv1.ToInt64((IFormatProvider) null), conv2.ToInt64((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 240:
          return Operators.OrUInt64(conv1.ToUInt64((IFormatProvider) null), conv2.ToUInt64((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return Operators.OrInt64(conv1.ToInt64((IFormatProvider) null), 0L, (Type) null);
        case (TypeCode) 342:
          return Operators.OrInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), 0L, (Type) null);
        case (TypeCode) 345:
          return Operators.OrBoolean(Conversions.ToBoolean(conv1.ToString((IFormatProvider) null)), conv2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.OrInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 360:
          return Operators.OrInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Or, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Or, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object OrBoolean(bool Left, bool Right)
    {
      return (object) (Left | Right);
    }

    private static object OrSByte(sbyte Left, sbyte Right, Type EnumType = null)
    {
      sbyte num = (sbyte) ((int) Left | (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object OrByte(byte Left, byte Right, Type EnumType = null)
    {
      byte num = (byte) ((int) Left | (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object OrInt16(short Left, short Right, Type EnumType = null)
    {
      short num = (short) ((int) Left | (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object OrUInt16(ushort Left, ushort Right, Type EnumType = null)
    {
      ushort num = (ushort) ((int) Left | (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object OrInt32(int Left, int Right, Type EnumType = null)
    {
      int num = Left | Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object OrUInt32(uint Left, uint Right, Type EnumType = null)
    {
      uint num = Left | Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object OrInt64(long Left, long Right, Type EnumType = null)
    {
      long num = Left | Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object OrUInt64(ulong Left, ulong Right, Type EnumType = null)
    {
      ulong num = Left | Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    /// <summary>Represents the Visual Basic <see langword="Xor" /> operator.</summary>
    /// <param name="Left">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <param name="Right">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>A <see langword="Boolean" /> or numeric value. For a <see langword="Boolean" /> comparison, the return value is the logical exclusion (exclusive logical disjunction) of two <see langword="Boolean" /> values. For bitwise (numeric) operations, the return value is a numeric value that represents the bitwise exclusion (exclusive bitwise disjunction) of two numeric bit patterns. For more information, see Xor Operator (Visual Basic).</returns>
    public static object XorObject(object Left, object Right)
    {
      IConvertible conv1 = Left as IConvertible;
      TypeCode typeCode1 = conv1 != null ? conv1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible conv2 = Right as IConvertible;
      TypeCode typeCode2 = conv2 != null ? conv2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.Boxed_ZeroInteger;
        case TypeCode.Boolean:
          return Operators.XorBoolean(false, conv2.ToBoolean((IFormatProvider) null));
        case TypeCode.SByte:
          return Operators.XorSByte((sbyte) 0, conv2.ToSByte((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case TypeCode.Byte:
          return Operators.XorByte((byte) 0, conv2.ToByte((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case TypeCode.Int16:
          return Operators.XorInt16((short) 0, conv2.ToInt16((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case TypeCode.UInt16:
          return Operators.XorUInt16((ushort) 0, conv2.ToUInt16((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case TypeCode.Int32:
          return Operators.XorInt32(0, conv2.ToInt32((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case TypeCode.UInt32:
          return Operators.XorUInt32(0U, conv2.ToUInt32((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case TypeCode.Int64:
          return Operators.XorInt64(0L, conv2.ToInt64((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case TypeCode.UInt64:
          return Operators.XorUInt64(0UL, conv2.ToUInt64((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return Operators.XorInt64(0L, conv2.ToInt64((IFormatProvider) null), (Type) null);
        case TypeCode.String:
          return Operators.XorInt64(0L, Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        case (TypeCode) 57:
          return Operators.XorBoolean(conv1.ToBoolean((IFormatProvider) null), false);
        case (TypeCode) 60:
          return Operators.XorBoolean(conv1.ToBoolean((IFormatProvider) null), conv2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 62:
          return Operators.XorSByte(Operators.ToVBBool(conv1), conv2.ToSByte((IFormatProvider) null), (Type) null);
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.XorInt16((short) Operators.ToVBBool(conv1), conv2.ToInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.XorInt32((int) Operators.ToVBBool(conv1), conv2.ToInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 67:
        case (TypeCode) 68:
        case (TypeCode) 69:
        case (TypeCode) 70:
        case (TypeCode) 71:
        case (TypeCode) 72:
          return Operators.XorInt64((long) Operators.ToVBBool(conv1), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 75:
          return Operators.XorBoolean(conv1.ToBoolean((IFormatProvider) null), Conversions.ToBoolean(conv2.ToString((IFormatProvider) null)));
        case (TypeCode) 95:
          return Operators.XorSByte(conv1.ToSByte((IFormatProvider) null), (sbyte) 0, Operators.GetEnumResult(Left, Right));
        case (TypeCode) 98:
          return Operators.XorSByte(conv1.ToSByte((IFormatProvider) null), Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 100:
          return Operators.XorSByte(conv1.ToSByte((IFormatProvider) null), conv2.ToSByte((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
          return Operators.XorInt16(conv1.ToInt16((IFormatProvider) null), conv2.ToInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
          return Operators.XorInt32(conv1.ToInt32((IFormatProvider) null), conv2.ToInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 107:
        case (TypeCode) 108:
        case (TypeCode) 109:
        case (TypeCode) 110:
        case (TypeCode) 125:
        case (TypeCode) 127:
        case (TypeCode) 128:
        case (TypeCode) 129:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 145:
        case (TypeCode) 146:
        case (TypeCode) 147:
        case (TypeCode) 148:
        case (TypeCode) 163:
        case (TypeCode) 165:
        case (TypeCode) 166:
        case (TypeCode) 167:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 183:
        case (TypeCode) 184:
        case (TypeCode) 185:
        case (TypeCode) 186:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 203:
        case (TypeCode) 204:
        case (TypeCode) 205:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 221:
        case (TypeCode) 222:
        case (TypeCode) 223:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 241:
        case (TypeCode) 242:
        case (TypeCode) 243:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 261:
        case (TypeCode) 262:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 298:
        case (TypeCode) 299:
        case (TypeCode) 300:
          return Operators.XorInt64(conv1.ToInt64((IFormatProvider) null), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.XorInt64(conv1.ToInt64((IFormatProvider) null), Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        case (TypeCode) 114:
          return Operators.XorByte(conv1.ToByte((IFormatProvider) null), (byte) 0, Operators.GetEnumResult(Left, Right));
        case (TypeCode) 117:
        case (TypeCode) 136:
          return Operators.XorInt16(conv1.ToInt16((IFormatProvider) null), (short) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 120:
          return Operators.XorByte(conv1.ToByte((IFormatProvider) null), conv2.ToByte((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 122:
        case (TypeCode) 158:
          return Operators.XorUInt16(conv1.ToUInt16((IFormatProvider) null), conv2.ToUInt16((IFormatProvider) null), (Type) null);
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
          return Operators.XorUInt32(conv1.ToUInt32((IFormatProvider) null), conv2.ToUInt32((IFormatProvider) null), (Type) null);
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
          return Operators.XorUInt64(conv1.ToUInt64((IFormatProvider) null), conv2.ToUInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 133:
          return Operators.XorInt16(conv1.ToInt16((IFormatProvider) null), (short) 0, Operators.GetEnumResult(Left, Right));
        case (TypeCode) 140:
          return Operators.XorInt16(conv1.ToInt16((IFormatProvider) null), conv2.ToInt16((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 152:
          return Operators.XorUInt16(conv1.ToUInt16((IFormatProvider) null), (ushort) 0, Operators.GetEnumResult(Left, Right));
        case (TypeCode) 155:
        case (TypeCode) 174:
          return Operators.XorInt32(conv1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 160:
          return Operators.XorUInt16(conv1.ToUInt16((IFormatProvider) null), conv2.ToUInt16((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 171:
          return Operators.XorInt32(conv1.ToInt32((IFormatProvider) null), 0, Operators.GetEnumResult(Left, Right));
        case (TypeCode) 180:
          return Operators.XorInt32(conv1.ToInt32((IFormatProvider) null), conv2.ToInt32((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 190:
          return Operators.XorUInt32(conv1.ToUInt32((IFormatProvider) null), 0U, Operators.GetEnumResult(Left, Right));
        case (TypeCode) 193:
        case (TypeCode) 212:
        case (TypeCode) 231:
        case (TypeCode) 250:
        case (TypeCode) 269:
        case (TypeCode) 288:
          return Operators.XorInt64(conv1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(conv2), (Type) null);
        case (TypeCode) 200:
          return Operators.XorUInt32(conv1.ToUInt32((IFormatProvider) null), conv2.ToUInt32((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 209:
          return Operators.XorInt64(conv1.ToInt64((IFormatProvider) null), 0L, Operators.GetEnumResult(Left, Right));
        case (TypeCode) 220:
          return Operators.XorInt64(conv1.ToInt64((IFormatProvider) null), conv2.ToInt64((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 228:
          return Operators.XorUInt64(conv1.ToUInt64((IFormatProvider) null), 0UL, Operators.GetEnumResult(Left, Right));
        case (TypeCode) 240:
          return Operators.XorUInt64(conv1.ToUInt64((IFormatProvider) null), conv2.ToUInt64((IFormatProvider) null), Operators.GetEnumResult(Left, Right));
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return Operators.XorInt64(conv1.ToInt64((IFormatProvider) null), 0L, (Type) null);
        case (TypeCode) 342:
          return Operators.XorInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), 0L, (Type) null);
        case (TypeCode) 345:
          return Operators.XorBoolean(Conversions.ToBoolean(conv1.ToString((IFormatProvider) null)), conv2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.XorInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), conv2.ToInt64((IFormatProvider) null), (Type) null);
        case (TypeCode) 360:
          return Operators.XorInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), Conversions.ToLong(conv2.ToString((IFormatProvider) null)), (Type) null);
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Xor, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Xor, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object XorBoolean(bool Left, bool Right)
    {
      return (object) (Left ^ Right);
    }

    private static object XorSByte(sbyte Left, sbyte Right, Type EnumType = null)
    {
      sbyte num = (sbyte) ((int) Left ^ (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object XorByte(byte Left, byte Right, Type EnumType = null)
    {
      byte num = (byte) ((int) Left ^ (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object XorInt16(short Left, short Right, Type EnumType = null)
    {
      short num = (short) ((int) Left ^ (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object XorUInt16(ushort Left, ushort Right, Type EnumType = null)
    {
      ushort num = (ushort) ((int) Left ^ (int) Right);
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object XorInt32(int Left, int Right, Type EnumType = null)
    {
      int num = Left ^ Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object XorUInt32(uint Left, uint Right, Type EnumType = null)
    {
      uint num = Left ^ Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object XorInt64(long Left, long Right, Type EnumType = null)
    {
      long num = Left ^ Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    private static object XorUInt64(ulong Left, ulong Right, Type EnumType = null)
    {
      ulong num = Left ^ Right;
      if (EnumType != null)
        return Enum.ToObject(EnumType, num);
      return (object) num;
    }

    /// <summary>Represents the Visual Basic addition (+) operator.</summary>
    /// <param name="Left">Required. Any numeric expression.</param>
    /// <param name="Right">Required. Any numeric expression.</param>
    /// <returns>The sum of <paramref name="Left" /> and <paramref name="Right" />.</returns>
    public static object AddObject(object Left, object Right)
    {
      IConvertible convertible1 = Left as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Right as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      if (typeCode1 == TypeCode.Object)
      {
        char[] chArray = Left as char[];
        if (chArray != null && (typeCode2 == TypeCode.String || typeCode2 == TypeCode.Empty || typeCode2 == TypeCode.Object && Right is char[]))
        {
          Left = (object) new string(chArray);
          convertible1 = (IConvertible) Left;
          typeCode1 = TypeCode.String;
        }
      }
      if (typeCode2 == TypeCode.Object)
      {
        char[] chArray = Right as char[];
        if (chArray != null && (typeCode1 == TypeCode.String || typeCode1 == TypeCode.Empty))
        {
          Right = (object) new string(chArray);
          convertible2 = (IConvertible) Right;
          typeCode2 = TypeCode.String;
        }
      }
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.Boxed_ZeroInteger;
        case TypeCode.Boolean:
          return Operators.AddInt16((short) 0, (short) Operators.ToVBBool(convertible2));
        case TypeCode.Char:
          return Operators.AddString("\0", convertible2.ToString((IFormatProvider) null));
        case TypeCode.SByte:
          return (object) convertible2.ToSByte((IFormatProvider) null);
        case TypeCode.Byte:
          return (object) convertible2.ToByte((IFormatProvider) null);
        case TypeCode.Int16:
          return (object) convertible2.ToInt16((IFormatProvider) null);
        case TypeCode.UInt16:
          return (object) convertible2.ToUInt16((IFormatProvider) null);
        case TypeCode.Int32:
          return (object) convertible2.ToInt32((IFormatProvider) null);
        case TypeCode.UInt32:
          return (object) convertible2.ToUInt32((IFormatProvider) null);
        case TypeCode.Int64:
          return (object) convertible2.ToInt64((IFormatProvider) null);
        case TypeCode.UInt64:
          return (object) convertible2.ToUInt64((IFormatProvider) null);
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.String:
        case (TypeCode) 56:
          return Right;
        case TypeCode.DateTime:
          return Operators.AddString(Conversions.ToString(DateTime.MinValue), Conversions.ToString(convertible2.ToDateTime((IFormatProvider) null)));
        case (TypeCode) 57:
          return Operators.AddInt16((short) Operators.ToVBBool(convertible1), (short) 0);
        case (TypeCode) 60:
          return Operators.AddInt16((short) Operators.ToVBBool(convertible1), (short) Operators.ToVBBool(convertible2));
        case (TypeCode) 62:
          return Operators.AddSByte(Operators.ToVBBool(convertible1), convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.AddInt16((short) Operators.ToVBBool(convertible1), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.AddInt32((int) Operators.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 67:
        case (TypeCode) 68:
          return Operators.AddInt64((long) Operators.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 69:
        case (TypeCode) 72:
          return Operators.AddDecimal(Operators.ToVBBoolConv(convertible1), (IConvertible) convertible2.ToDecimal((IFormatProvider) null));
        case (TypeCode) 70:
          return Operators.AddSingle((float) Operators.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return Operators.AddDouble((double) Operators.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 75:
          return Operators.AddDouble((double) Operators.ToVBBool(convertible1), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 76:
          return Operators.AddString(convertible1.ToString((IFormatProvider) null), "\0");
        case (TypeCode) 80:
        case (TypeCode) 94:
        case (TypeCode) 346:
          return Operators.AddString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null));
        case (TypeCode) 95:
          return (object) convertible1.ToSByte((IFormatProvider) null);
        case (TypeCode) 98:
          return Operators.AddSByte(convertible1.ToSByte((IFormatProvider) null), Operators.ToVBBool(convertible2));
        case (TypeCode) 100:
          return Operators.AddSByte(convertible1.ToSByte((IFormatProvider) null), convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return Operators.AddInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
        case (TypeCode) 180:
          return Operators.AddInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 125:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 163:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 220:
          return Operators.AddInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 107:
        case (TypeCode) 110:
        case (TypeCode) 129:
        case (TypeCode) 145:
        case (TypeCode) 148:
        case (TypeCode) 167:
        case (TypeCode) 183:
        case (TypeCode) 186:
        case (TypeCode) 205:
        case (TypeCode) 221:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 243:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 300:
          return Operators.AddDecimal(convertible1, convertible2);
        case (TypeCode) 108:
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 165:
        case (TypeCode) 184:
        case (TypeCode) 203:
        case (TypeCode) 222:
        case (TypeCode) 241:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return Operators.AddSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 109:
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 166:
        case (TypeCode) 185:
        case (TypeCode) 204:
        case (TypeCode) 223:
        case (TypeCode) 242:
        case (TypeCode) 261:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return Operators.AddDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.AddDouble(convertible1.ToDouble((IFormatProvider) null), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 114:
          return (object) convertible1.ToByte((IFormatProvider) null);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return Operators.AddInt16(convertible1.ToInt16((IFormatProvider) null), (short) Operators.ToVBBool(convertible2));
        case (TypeCode) 120:
          return Operators.AddByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 122:
        case (TypeCode) 158:
        case (TypeCode) 160:
          return Operators.AddUInt16(convertible1.ToUInt16((IFormatProvider) null), convertible2.ToUInt16((IFormatProvider) null));
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
        case (TypeCode) 200:
          return Operators.AddUInt32(convertible1.ToUInt32((IFormatProvider) null), convertible2.ToUInt32((IFormatProvider) null));
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
        case (TypeCode) 240:
          return Operators.AddUInt64(convertible1.ToUInt64((IFormatProvider) null), convertible2.ToUInt64((IFormatProvider) null));
        case (TypeCode) 133:
          return (object) convertible1.ToInt16((IFormatProvider) null);
        case (TypeCode) 152:
          return (object) convertible1.ToUInt16((IFormatProvider) null);
        case (TypeCode) 155:
        case (TypeCode) 174:
          return Operators.AddInt32(convertible1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 171:
          return (object) convertible1.ToInt32((IFormatProvider) null);
        case (TypeCode) 190:
          return (object) convertible1.ToUInt32((IFormatProvider) null);
        case (TypeCode) 193:
        case (TypeCode) 212:
          return Operators.AddInt64(convertible1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(convertible2));
        case (TypeCode) 209:
          return (object) convertible1.ToInt64((IFormatProvider) null);
        case (TypeCode) 228:
          return (object) convertible1.ToUInt64((IFormatProvider) null);
        case (TypeCode) 231:
        case (TypeCode) 288:
          return Operators.AddDecimal(convertible1, Operators.ToVBBoolConv(convertible2));
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
        case (TypeCode) 342:
        case (TypeCode) 344:
          return Left;
        case (TypeCode) 250:
          return Operators.AddSingle(convertible1.ToSingle((IFormatProvider) null), (float) Operators.ToVBBool(convertible2));
        case (TypeCode) 269:
          return Operators.AddDouble(convertible1.ToDouble((IFormatProvider) null), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 304:
          return Operators.AddString(Conversions.ToString(convertible1.ToDateTime((IFormatProvider) null)), Conversions.ToString(DateTime.MinValue));
        case (TypeCode) 320:
          return Operators.AddString(Conversions.ToString(convertible1.ToDateTime((IFormatProvider) null)), Conversions.ToString(convertible2.ToDateTime((IFormatProvider) null)));
        case (TypeCode) 322:
          return Operators.AddString(Conversions.ToString(convertible1.ToDateTime((IFormatProvider) null)), convertible2.ToString((IFormatProvider) null));
        case (TypeCode) 345:
          return Operators.AddDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.AddDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 358:
          return Operators.AddString(convertible1.ToString((IFormatProvider) null), Conversions.ToString(convertible2.ToDateTime((IFormatProvider) null)));
        case (TypeCode) 360:
          return Operators.AddString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null));
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Plus, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Plus, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object AddByte(byte Left, byte Right)
    {
      short num = checked ((short) unchecked ((int) Left + (int) Right));
      if (num > (short) byte.MaxValue)
        return (object) num;
      return (object) checked ((byte) num);
    }

    private static object AddSByte(sbyte Left, sbyte Right)
    {
      short num = checked ((short) unchecked ((int) Left + (int) Right));
      if (num > (short) sbyte.MaxValue || num < (short) sbyte.MinValue)
        return (object) num;
      return (object) checked ((sbyte) num);
    }

    private static object AddInt16(short Left, short Right)
    {
      int num = checked ((int) Left + (int) Right);
      if (num > (int) short.MaxValue || num < (int) short.MinValue)
        return (object) num;
      return (object) checked ((short) num);
    }

    private static object AddUInt16(ushort Left, ushort Right)
    {
      int num = checked ((int) Left + (int) Right);
      if (num > (int) ushort.MaxValue)
        return (object) num;
      return (object) checked ((ushort) num);
    }

    private static object AddInt32(int Left, int Right)
    {
      long num = checked ((long) Left + (long) Right);
      if (num > (long) int.MaxValue || num < (long) int.MinValue)
        return (object) num;
      return (object) checked ((int) num);
    }

    private static object AddUInt32(uint Left, uint Right)
    {
      long num = checked ((long) Left + (long) Right);
      if (num > (long) uint.MaxValue)
        return (object) num;
      return (object) checked ((uint) num);
    }

    private static object AddInt64(long Left, long Right)
    {
      try
      {
        return (object) checked (Left + Right);
      }
      catch (OverflowException ex)
      {
        return (object) Decimal.Add(new Decimal(Left), new Decimal(Right));
      }
    }

    private static object AddUInt64(ulong Left, ulong Right)
    {
      try
      {
        return (object) checked (Left + Right);
      }
      catch (OverflowException ex)
      {
        return (object) Decimal.Add(new Decimal(Left), new Decimal(Right));
      }
    }

    private static object AddDecimal(IConvertible Left, IConvertible Right)
    {
      Decimal d1 = Left.ToDecimal((IFormatProvider) null);
      Decimal d2 = Right.ToDecimal((IFormatProvider) null);
      try
      {
        return (object) Decimal.Add(d1, d2);
      }
      catch (OverflowException ex)
      {
        return (object) (Convert.ToDouble(d1) + Convert.ToDouble(d2));
      }
    }

    private static object AddSingle(float Left, float Right)
    {
      double d = (double) Left + (double) Right;
      if (d <= 3.40282346638529E+38 && d >= -3.40282346638529E+38)
        return (object) (float) d;
      if (double.IsInfinity(d) && (float.IsInfinity(Left) || float.IsInfinity(Right)))
        return (object) (float) d;
      return (object) d;
    }

    private static object AddDouble(double Left, double Right)
    {
      return (object) (Left + Right);
    }

    private static object AddString(string Left, string Right)
    {
      return (object) (Left + Right);
    }

    /// <summary>Represents the Visual Basic subtraction (–) operator.</summary>
    /// <param name="Left">Required. Any numeric expression. </param>
    /// <param name="Right">Required. Any numeric expression.</param>
    /// <returns>The difference between <paramref name="Left" /> and <paramref name="Right" />.</returns>
    public static object SubtractObject(object Left, object Right)
    {
      IConvertible convertible1 = Left as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Right as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.Boxed_ZeroInteger;
        case TypeCode.Boolean:
          return Operators.SubtractInt16((short) 0, (short) Operators.ToVBBool(convertible2));
        case TypeCode.SByte:
          return Operators.SubtractSByte((sbyte) 0, convertible2.ToSByte((IFormatProvider) null));
        case TypeCode.Byte:
          return Operators.SubtractByte((byte) 0, convertible2.ToByte((IFormatProvider) null));
        case TypeCode.Int16:
          return Operators.SubtractInt16((short) 0, convertible2.ToInt16((IFormatProvider) null));
        case TypeCode.UInt16:
          return Operators.SubtractUInt16((ushort) 0, convertible2.ToUInt16((IFormatProvider) null));
        case TypeCode.Int32:
          return Operators.SubtractInt32(0, convertible2.ToInt32((IFormatProvider) null));
        case TypeCode.UInt32:
          return Operators.SubtractUInt32(0U, convertible2.ToUInt32((IFormatProvider) null));
        case TypeCode.Int64:
          return Operators.SubtractInt64(0L, convertible2.ToInt64((IFormatProvider) null));
        case TypeCode.UInt64:
          return Operators.SubtractUInt64(0UL, convertible2.ToUInt64((IFormatProvider) null));
        case TypeCode.Single:
          return Operators.SubtractSingle(0.0f, convertible2.ToSingle((IFormatProvider) null));
        case TypeCode.Double:
          return Operators.SubtractDouble(0.0, convertible2.ToDouble((IFormatProvider) null));
        case TypeCode.Decimal:
          return Operators.SubtractDecimal((IConvertible) Decimal.Zero, convertible2);
        case TypeCode.String:
          return Operators.SubtractDouble(0.0, Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 57:
          return Operators.SubtractInt16((short) Operators.ToVBBool(convertible1), (short) 0);
        case (TypeCode) 60:
          return Operators.SubtractInt16((short) Operators.ToVBBool(convertible1), (short) Operators.ToVBBool(convertible2));
        case (TypeCode) 62:
          return Operators.SubtractSByte(Operators.ToVBBool(convertible1), convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.SubtractInt16((short) Operators.ToVBBool(convertible1), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.SubtractInt32((int) Operators.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 67:
        case (TypeCode) 68:
          return Operators.SubtractInt64((long) Operators.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 69:
        case (TypeCode) 72:
          return Operators.SubtractDecimal(Operators.ToVBBoolConv(convertible1), (IConvertible) convertible2.ToDecimal((IFormatProvider) null));
        case (TypeCode) 70:
          return Operators.SubtractSingle((float) Operators.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return Operators.SubtractDouble((double) Operators.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 75:
          return Operators.SubtractDouble((double) Operators.ToVBBool(convertible1), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 95:
          return (object) convertible1.ToSByte((IFormatProvider) null);
        case (TypeCode) 98:
          return Operators.SubtractSByte(convertible1.ToSByte((IFormatProvider) null), Operators.ToVBBool(convertible2));
        case (TypeCode) 100:
          return Operators.SubtractSByte(convertible1.ToSByte((IFormatProvider) null), convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return Operators.SubtractInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
        case (TypeCode) 180:
          return Operators.SubtractInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 125:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 163:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 220:
          return Operators.SubtractInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 107:
        case (TypeCode) 110:
        case (TypeCode) 129:
        case (TypeCode) 145:
        case (TypeCode) 148:
        case (TypeCode) 167:
        case (TypeCode) 183:
        case (TypeCode) 186:
        case (TypeCode) 205:
        case (TypeCode) 221:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 243:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 300:
          return Operators.SubtractDecimal(convertible1, convertible2);
        case (TypeCode) 108:
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 165:
        case (TypeCode) 184:
        case (TypeCode) 203:
        case (TypeCode) 222:
        case (TypeCode) 241:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return Operators.SubtractSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 109:
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 166:
        case (TypeCode) 185:
        case (TypeCode) 204:
        case (TypeCode) 223:
        case (TypeCode) 242:
        case (TypeCode) 261:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return Operators.SubtractDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.SubtractDouble(convertible1.ToDouble((IFormatProvider) null), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 114:
          return (object) convertible1.ToByte((IFormatProvider) null);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return Operators.SubtractInt16(convertible1.ToInt16((IFormatProvider) null), (short) Operators.ToVBBool(convertible2));
        case (TypeCode) 120:
          return Operators.SubtractByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 122:
        case (TypeCode) 158:
        case (TypeCode) 160:
          return Operators.SubtractUInt16(convertible1.ToUInt16((IFormatProvider) null), convertible2.ToUInt16((IFormatProvider) null));
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
        case (TypeCode) 200:
          return Operators.SubtractUInt32(convertible1.ToUInt32((IFormatProvider) null), convertible2.ToUInt32((IFormatProvider) null));
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
        case (TypeCode) 240:
          return Operators.SubtractUInt64(convertible1.ToUInt64((IFormatProvider) null), convertible2.ToUInt64((IFormatProvider) null));
        case (TypeCode) 133:
          return (object) convertible1.ToInt16((IFormatProvider) null);
        case (TypeCode) 152:
          return (object) convertible1.ToUInt16((IFormatProvider) null);
        case (TypeCode) 155:
        case (TypeCode) 174:
          return Operators.SubtractInt32(convertible1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 171:
          return (object) convertible1.ToInt32((IFormatProvider) null);
        case (TypeCode) 190:
          return (object) convertible1.ToUInt32((IFormatProvider) null);
        case (TypeCode) 193:
        case (TypeCode) 212:
          return Operators.SubtractInt64(convertible1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(convertible2));
        case (TypeCode) 209:
          return (object) convertible1.ToInt64((IFormatProvider) null);
        case (TypeCode) 228:
          return (object) convertible1.ToUInt64((IFormatProvider) null);
        case (TypeCode) 231:
        case (TypeCode) 288:
          return Operators.SubtractDecimal(convertible1, Operators.ToVBBoolConv(convertible2));
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return Left;
        case (TypeCode) 250:
          return Operators.SubtractSingle(convertible1.ToSingle((IFormatProvider) null), (float) Operators.ToVBBool(convertible2));
        case (TypeCode) 269:
          return Operators.SubtractDouble(convertible1.ToDouble((IFormatProvider) null), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 342:
          return (object) Conversions.ToDouble(convertible1.ToString((IFormatProvider) null));
        case (TypeCode) 345:
          return Operators.SubtractDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.SubtractDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 360:
          return Operators.SubtractDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object && (typeCode1 != TypeCode.DateTime || typeCode2 != TypeCode.DateTime) && ((typeCode1 != TypeCode.DateTime || typeCode2 != TypeCode.Empty) && (typeCode1 != TypeCode.Empty || typeCode2 != TypeCode.DateTime)))
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Minus, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Minus, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object SubtractByte(byte Left, byte Right)
    {
      short num = checked ((short) unchecked ((int) Left - (int) Right));
      if (num < (short) 0)
        return (object) num;
      return (object) checked ((byte) num);
    }

    private static object SubtractSByte(sbyte Left, sbyte Right)
    {
      short num = checked ((short) unchecked ((int) Left - (int) Right));
      if (num < (short) sbyte.MinValue || num > (short) sbyte.MaxValue)
        return (object) num;
      return (object) checked ((sbyte) num);
    }

    private static object SubtractInt16(short Left, short Right)
    {
      int num = checked ((int) Left - (int) Right);
      if (num < (int) short.MinValue || num > (int) short.MaxValue)
        return (object) num;
      return (object) checked ((short) num);
    }

    private static object SubtractUInt16(ushort Left, ushort Right)
    {
      int num = checked ((int) Left - (int) Right);
      if (num < 0)
        return (object) num;
      return (object) checked ((ushort) num);
    }

    private static object SubtractInt32(int Left, int Right)
    {
      long num = checked ((long) Left - (long) Right);
      if (num < (long) int.MinValue || num > (long) int.MaxValue)
        return (object) num;
      return (object) checked ((int) num);
    }

    private static object SubtractUInt32(uint Left, uint Right)
    {
      long num = checked ((long) Left - (long) Right);
      if (num < 0L)
        return (object) num;
      return (object) checked ((uint) num);
    }

    private static object SubtractInt64(long Left, long Right)
    {
      try
      {
        return (object) checked (Left - Right);
      }
      catch (OverflowException ex)
      {
        return (object) Decimal.Subtract(new Decimal(Left), new Decimal(Right));
      }
    }

    private static object SubtractUInt64(ulong Left, ulong Right)
    {
      try
      {
        return (object) checked (Left - Right);
      }
      catch (OverflowException ex)
      {
        return (object) Decimal.Subtract(new Decimal(Left), new Decimal(Right));
      }
    }

    private static object SubtractDecimal(IConvertible Left, IConvertible Right)
    {
      Decimal d1 = Left.ToDecimal((IFormatProvider) null);
      Decimal d2 = Right.ToDecimal((IFormatProvider) null);
      try
      {
        return (object) Decimal.Subtract(d1, d2);
      }
      catch (OverflowException ex)
      {
        return (object) (Convert.ToDouble(d1) - Convert.ToDouble(d2));
      }
    }

    private static object SubtractSingle(float Left, float Right)
    {
      double d = (double) Left - (double) Right;
      if (d <= 3.40282346638529E+38 && d >= -3.40282346638529E+38)
        return (object) (float) d;
      if (double.IsInfinity(d) && (float.IsInfinity(Left) || float.IsInfinity(Right)))
        return (object) (float) d;
      return (object) d;
    }

    private static object SubtractDouble(double Left, double Right)
    {
      return (object) (Left - Right);
    }

    /// <summary>Represents the Visual Basic multiply (*) operator.</summary>
    /// <param name="Left">Required. Any numeric expression.</param>
    /// <param name="Right">Required. Any numeric expression.</param>
    /// <returns>The product of <paramref name="Left" /> and <paramref name="Right" />.</returns>
    public static object MultiplyObject(object Left, object Right)
    {
      IConvertible convertible1 = Left as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Right as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
        case TypeCode.Int32:
        case (TypeCode) 171:
          return Operators.Boxed_ZeroInteger;
        case TypeCode.Boolean:
        case TypeCode.Int16:
        case (TypeCode) 57:
        case (TypeCode) 133:
          return Operators.Boxed_ZeroShort;
        case TypeCode.SByte:
        case (TypeCode) 95:
          return Operators.Boxed_ZeroSByte;
        case TypeCode.Byte:
        case (TypeCode) 114:
          return Operators.Boxed_ZeroByte;
        case TypeCode.UInt16:
        case (TypeCode) 152:
          return Operators.Boxed_ZeroUShort;
        case TypeCode.UInt32:
        case (TypeCode) 190:
          return Operators.Boxed_ZeroUInteger;
        case TypeCode.Int64:
        case (TypeCode) 209:
          return Operators.Boxed_ZeroLong;
        case TypeCode.UInt64:
        case (TypeCode) 228:
          return Operators.Boxed_ZeroULong;
        case TypeCode.Single:
        case (TypeCode) 247:
          return Operators.Boxed_ZeroSinge;
        case TypeCode.Double:
        case (TypeCode) 266:
          return Operators.Boxed_ZeroDouble;
        case TypeCode.Decimal:
        case (TypeCode) 285:
          return Operators.Boxed_ZeroDecimal;
        case TypeCode.String:
          return Operators.MultiplyDouble(0.0, Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 60:
          return Operators.MultiplyInt16((short) Operators.ToVBBool(convertible1), (short) Operators.ToVBBool(convertible2));
        case (TypeCode) 62:
          return Operators.MultiplySByte(Operators.ToVBBool(convertible1), convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.MultiplyInt16((short) Operators.ToVBBool(convertible1), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.MultiplyInt32((int) Operators.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 67:
        case (TypeCode) 68:
          return Operators.MultiplyInt64((long) Operators.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 69:
        case (TypeCode) 72:
          return Operators.MultiplyDecimal(Operators.ToVBBoolConv(convertible1), (IConvertible) convertible2.ToDecimal((IFormatProvider) null));
        case (TypeCode) 70:
          return Operators.MultiplySingle((float) Operators.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return Operators.MultiplyDouble((double) Operators.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 75:
          return Operators.MultiplyDouble((double) Operators.ToVBBool(convertible1), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 98:
          return Operators.MultiplySByte(convertible1.ToSByte((IFormatProvider) null), Operators.ToVBBool(convertible2));
        case (TypeCode) 100:
          return Operators.MultiplySByte(convertible1.ToSByte((IFormatProvider) null), convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return Operators.MultiplyInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
        case (TypeCode) 180:
          return Operators.MultiplyInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 125:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 163:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 220:
          return Operators.MultiplyInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 107:
        case (TypeCode) 110:
        case (TypeCode) 129:
        case (TypeCode) 145:
        case (TypeCode) 148:
        case (TypeCode) 167:
        case (TypeCode) 183:
        case (TypeCode) 186:
        case (TypeCode) 205:
        case (TypeCode) 221:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 243:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 300:
          return Operators.MultiplyDecimal(convertible1, convertible2);
        case (TypeCode) 108:
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 165:
        case (TypeCode) 184:
        case (TypeCode) 203:
        case (TypeCode) 222:
        case (TypeCode) 241:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return Operators.MultiplySingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 109:
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 166:
        case (TypeCode) 185:
        case (TypeCode) 204:
        case (TypeCode) 223:
        case (TypeCode) 242:
        case (TypeCode) 261:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return Operators.MultiplyDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.MultiplyDouble(convertible1.ToDouble((IFormatProvider) null), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 117:
        case (TypeCode) 136:
          return Operators.MultiplyInt16(convertible1.ToInt16((IFormatProvider) null), (short) Operators.ToVBBool(convertible2));
        case (TypeCode) 120:
          return Operators.MultiplyByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 122:
        case (TypeCode) 158:
        case (TypeCode) 160:
          return Operators.MultiplyUInt16(convertible1.ToUInt16((IFormatProvider) null), convertible2.ToUInt16((IFormatProvider) null));
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
        case (TypeCode) 200:
          return Operators.MultiplyUInt32(convertible1.ToUInt32((IFormatProvider) null), convertible2.ToUInt32((IFormatProvider) null));
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
        case (TypeCode) 240:
          return Operators.MultiplyUInt64(convertible1.ToUInt64((IFormatProvider) null), convertible2.ToUInt64((IFormatProvider) null));
        case (TypeCode) 155:
        case (TypeCode) 174:
          return Operators.MultiplyInt32(convertible1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 193:
        case (TypeCode) 212:
          return Operators.MultiplyInt64(convertible1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(convertible2));
        case (TypeCode) 231:
        case (TypeCode) 288:
          return Operators.MultiplyDecimal(convertible1, Operators.ToVBBoolConv(convertible2));
        case (TypeCode) 250:
          return Operators.MultiplySingle(convertible1.ToSingle((IFormatProvider) null), (float) Operators.ToVBBool(convertible2));
        case (TypeCode) 269:
          return Operators.MultiplyDouble(convertible1.ToDouble((IFormatProvider) null), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 342:
          return Operators.MultiplyDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), 0.0);
        case (TypeCode) 345:
          return Operators.MultiplyDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.MultiplyDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 360:
          return Operators.MultiplyDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Multiply, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Multiply, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object MultiplyByte(byte Left, byte Right)
    {
      int num = checked ((int) Left * (int) Right);
      if (num <= (int) byte.MaxValue)
        return (object) checked ((byte) num);
      if (num > (int) short.MaxValue)
        return (object) num;
      return (object) checked ((short) num);
    }

    private static object MultiplySByte(sbyte Left, sbyte Right)
    {
      short num = checked ((short) unchecked ((int) Left * (int) Right));
      if (num > (short) sbyte.MaxValue || num < (short) sbyte.MinValue)
        return (object) num;
      return (object) checked ((sbyte) num);
    }

    private static object MultiplyInt16(short Left, short Right)
    {
      int num = checked ((int) Left * (int) Right);
      if (num > (int) short.MaxValue || num < (int) short.MinValue)
        return (object) num;
      return (object) checked ((short) num);
    }

    private static object MultiplyUInt16(ushort Left, ushort Right)
    {
      long num = checked ((long) Left * (long) Right);
      if (num <= (long) ushort.MaxValue)
        return (object) checked ((ushort) num);
      if (num > (long) int.MaxValue)
        return (object) num;
      return (object) checked ((int) num);
    }

    private static object MultiplyInt32(int Left, int Right)
    {
      long num = checked ((long) Left * (long) Right);
      if (num > (long) int.MaxValue || num < (long) int.MinValue)
        return (object) num;
      return (object) checked ((int) num);
    }

    private static object MultiplyUInt32(uint Left, uint Right)
    {
      ulong num = checked ((ulong) Left * (ulong) Right);
      if (num <= (ulong) uint.MaxValue)
        return (object) checked ((uint) num);
      if (Decimal.Compare(new Decimal(num), new Decimal(-1, int.MaxValue, 0, false, (byte) 0)) > 0)
        return (object) new Decimal(num);
      return (object) checked ((long) num);
    }

    private static object MultiplyInt64(long Left, long Right)
    {
      try
      {
        return (object) checked (Left * Right);
      }
      catch (OverflowException ex)
      {
      }
      try
      {
        return (object) Decimal.Multiply(new Decimal(Left), new Decimal(Right));
      }
      catch (OverflowException ex)
      {
        return (object) ((double) Left * (double) Right);
      }
    }

    private static object MultiplyUInt64(ulong Left, ulong Right)
    {
      try
      {
        return (object) checked (Left * Right);
      }
      catch (OverflowException ex)
      {
      }
      try
      {
        return (object) Decimal.Multiply(new Decimal(Left), new Decimal(Right));
      }
      catch (OverflowException ex)
      {
        return (object) ((double) Left * (double) Right);
      }
    }

    private static object MultiplyDecimal(IConvertible Left, IConvertible Right)
    {
      Decimal d1 = Left.ToDecimal((IFormatProvider) null);
      Decimal d2 = Right.ToDecimal((IFormatProvider) null);
      try
      {
        return (object) Decimal.Multiply(d1, d2);
      }
      catch (OverflowException ex)
      {
        return (object) (Convert.ToDouble(d1) * Convert.ToDouble(d2));
      }
    }

    private static object MultiplySingle(float Left, float Right)
    {
      double d = (double) Left * (double) Right;
      if (d <= 3.40282346638529E+38 && d >= -3.40282346638529E+38)
        return (object) (float) d;
      if (double.IsInfinity(d) && (float.IsInfinity(Left) || float.IsInfinity(Right)))
        return (object) (float) d;
      return (object) d;
    }

    private static object MultiplyDouble(double Left, double Right)
    {
      return (object) (Left * Right);
    }

    /// <summary>Represents the Visual Basic division (/) operator.</summary>
    /// <param name="Left">Required. Any numeric expression.</param>
    /// <param name="Right">Required. Any numeric expression.</param>
    /// <returns>The full quotient of <paramref name="Left" /> divided by <paramref name="Right" />, including any remainder.</returns>
    public static object DivideObject(object Left, object Right)
    {
      IConvertible convertible1 = Left as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Right as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.DivideDouble(0.0, 0.0);
        case TypeCode.Boolean:
          return Operators.DivideDouble(0.0, (double) Operators.ToVBBool(convertible2));
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Double:
          return Operators.DivideDouble(0.0, convertible2.ToDouble((IFormatProvider) null));
        case TypeCode.Single:
          return Operators.DivideSingle(0.0f, convertible2.ToSingle((IFormatProvider) null));
        case TypeCode.Decimal:
          return Operators.DivideDecimal((IConvertible) Decimal.Zero, convertible2);
        case TypeCode.String:
          return Operators.DivideDouble(0.0, Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 57:
          return Operators.DivideDouble((double) Operators.ToVBBool(convertible1), 0.0);
        case (TypeCode) 60:
          return Operators.DivideDouble((double) Operators.ToVBBool(convertible1), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 62:
        case (TypeCode) 63:
        case (TypeCode) 64:
        case (TypeCode) 65:
        case (TypeCode) 66:
        case (TypeCode) 67:
        case (TypeCode) 68:
        case (TypeCode) 69:
        case (TypeCode) 71:
          return Operators.DivideDouble((double) Operators.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 70:
          return Operators.DivideSingle((float) Operators.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 72:
          return Operators.DivideDecimal(Operators.ToVBBoolConv(convertible1), convertible2);
        case (TypeCode) 75:
          return Operators.DivideDouble((double) Operators.ToVBBool(convertible1), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 95:
        case (TypeCode) 114:
        case (TypeCode) 133:
        case (TypeCode) 152:
        case (TypeCode) 171:
        case (TypeCode) 190:
        case (TypeCode) 209:
        case (TypeCode) 228:
        case (TypeCode) 266:
          return Operators.DivideDouble(convertible1.ToDouble((IFormatProvider) null), 0.0);
        case (TypeCode) 98:
        case (TypeCode) 117:
        case (TypeCode) 136:
        case (TypeCode) 155:
        case (TypeCode) 174:
        case (TypeCode) 193:
        case (TypeCode) 212:
        case (TypeCode) 231:
        case (TypeCode) 269:
          return Operators.DivideDouble(convertible1.ToDouble((IFormatProvider) null), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 100:
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 107:
        case (TypeCode) 109:
        case (TypeCode) 119:
        case (TypeCode) 120:
        case (TypeCode) 121:
        case (TypeCode) 122:
        case (TypeCode) 123:
        case (TypeCode) 124:
        case (TypeCode) 125:
        case (TypeCode) 126:
        case (TypeCode) 128:
        case (TypeCode) 138:
        case (TypeCode) 139:
        case (TypeCode) 140:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 145:
        case (TypeCode) 147:
        case (TypeCode) 157:
        case (TypeCode) 158:
        case (TypeCode) 159:
        case (TypeCode) 160:
        case (TypeCode) 161:
        case (TypeCode) 162:
        case (TypeCode) 163:
        case (TypeCode) 164:
        case (TypeCode) 166:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
        case (TypeCode) 180:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 183:
        case (TypeCode) 185:
        case (TypeCode) 195:
        case (TypeCode) 196:
        case (TypeCode) 197:
        case (TypeCode) 198:
        case (TypeCode) 199:
        case (TypeCode) 200:
        case (TypeCode) 201:
        case (TypeCode) 202:
        case (TypeCode) 204:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 220:
        case (TypeCode) 221:
        case (TypeCode) 223:
        case (TypeCode) 233:
        case (TypeCode) 234:
        case (TypeCode) 235:
        case (TypeCode) 236:
        case (TypeCode) 237:
        case (TypeCode) 238:
        case (TypeCode) 239:
        case (TypeCode) 240:
        case (TypeCode) 242:
        case (TypeCode) 261:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return Operators.DivideDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 108:
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 165:
        case (TypeCode) 184:
        case (TypeCode) 203:
        case (TypeCode) 222:
        case (TypeCode) 241:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return Operators.DivideSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 110:
        case (TypeCode) 129:
        case (TypeCode) 148:
        case (TypeCode) 167:
        case (TypeCode) 186:
        case (TypeCode) 205:
        case (TypeCode) 224:
        case (TypeCode) 243:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 300:
          return Operators.DivideDecimal(convertible1, convertible2);
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.DivideDouble(convertible1.ToDouble((IFormatProvider) null), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 247:
          return Operators.DivideSingle(convertible1.ToSingle((IFormatProvider) null), 0.0f);
        case (TypeCode) 250:
          return Operators.DivideSingle(convertible1.ToSingle((IFormatProvider) null), (float) Operators.ToVBBool(convertible2));
        case (TypeCode) 285:
          return Operators.DivideDecimal(convertible1, (IConvertible) Decimal.Zero);
        case (TypeCode) 288:
          return Operators.DivideDecimal(convertible1, Operators.ToVBBoolConv(convertible2));
        case (TypeCode) 342:
          return Operators.DivideDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), 0.0);
        case (TypeCode) 345:
          return Operators.DivideDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.DivideDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 360:
          return Operators.DivideDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Divide, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Divide, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object DivideDecimal(IConvertible Left, IConvertible Right)
    {
      Decimal d1 = Left.ToDecimal((IFormatProvider) null);
      Decimal d2 = Right.ToDecimal((IFormatProvider) null);
      try
      {
        return (object) Decimal.Divide(d1, d2);
      }
      catch (OverflowException ex)
      {
        return (object) (float) ((double) Convert.ToSingle(d1) / (double) Convert.ToSingle(d2));
      }
    }

    private static object DivideSingle(float Left, float Right)
    {
      float f = Left / Right;
      if (!float.IsInfinity(f))
        return (object) f;
      if (float.IsInfinity(Left) || float.IsInfinity(Right))
        return (object) f;
      return (object) ((double) Left / (double) Right);
    }

    private static object DivideDouble(double Left, double Right)
    {
      return (object) (Left / Right);
    }

    /// <summary>Represents the Visual Basic exponent (^) operator.</summary>
    /// <param name="Left">Required. Any numeric expression.</param>
    /// <param name="Right">Required. Any numeric expression.</param>
    /// <returns>The result of <paramref name="Left" /> raised to the power of <paramref name="Right" />.</returns>
    public static object ExponentObject(object Left, object Right)
    {
      IConvertible conv1 = Left as IConvertible;
      TypeCode typeCode1 = conv1 != null ? conv1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible conv2 = Right as IConvertible;
      TypeCode typeCode2 = conv2 != null ? conv2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      double x;
      switch (typeCode1)
      {
        case TypeCode.Empty:
          x = 0.0;
          break;
        case TypeCode.Boolean:
          x = (double) Operators.ToVBBool(conv1);
          break;
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
          x = conv1.ToDouble((IFormatProvider) null);
          break;
        case TypeCode.String:
          x = Conversions.ToDouble(conv1.ToString((IFormatProvider) null));
          break;
        default:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Power, Left, Right);
      }
      double y;
      switch (typeCode2)
      {
        case TypeCode.Empty:
          y = 0.0;
          break;
        case TypeCode.Boolean:
          y = (double) Operators.ToVBBool(conv2);
          break;
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
          y = conv2.ToDouble((IFormatProvider) null);
          break;
        case TypeCode.String:
          y = Conversions.ToDouble(conv2.ToString((IFormatProvider) null));
          break;
        default:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Power, Left, Right);
      }
      return (object) Math.Pow(x, y);
    }

    /// <summary>Represents the Visual Basic <see langword="Mod" /> operator.</summary>
    /// <param name="Left">Required. Any numeric expression.</param>
    /// <param name="Right">Required. Any numeric expression.</param>
    /// <returns>The remainder after <paramref name="Left" /> is divided by <paramref name="Right" />. </returns>
    public static object ModObject(object Left, object Right)
    {
      IConvertible convertible1 = Left as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Right as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.ModInt32(0, 0);
        case TypeCode.Boolean:
          return Operators.ModInt16((short) 0, (short) Operators.ToVBBool(convertible2));
        case TypeCode.SByte:
          return Operators.ModSByte((sbyte) 0, convertible2.ToSByte((IFormatProvider) null));
        case TypeCode.Byte:
          return Operators.ModByte((byte) 0, convertible2.ToByte((IFormatProvider) null));
        case TypeCode.Int16:
          return Operators.ModInt16((short) 0, convertible2.ToInt16((IFormatProvider) null));
        case TypeCode.UInt16:
          return Operators.ModUInt16((ushort) 0, convertible2.ToUInt16((IFormatProvider) null));
        case TypeCode.Int32:
          return Operators.ModInt32(0, convertible2.ToInt32((IFormatProvider) null));
        case TypeCode.UInt32:
          return Operators.ModUInt32(0U, convertible2.ToUInt32((IFormatProvider) null));
        case TypeCode.Int64:
          return Operators.ModInt64(0L, convertible2.ToInt64((IFormatProvider) null));
        case TypeCode.UInt64:
          return Operators.ModUInt64(0UL, convertible2.ToUInt64((IFormatProvider) null));
        case TypeCode.Single:
          return Operators.ModSingle(0.0f, convertible2.ToSingle((IFormatProvider) null));
        case TypeCode.Double:
          return Operators.ModDouble(0.0, convertible2.ToDouble((IFormatProvider) null));
        case TypeCode.Decimal:
          return Operators.ModDecimal((IConvertible) Decimal.Zero, (IConvertible) convertible2.ToDecimal((IFormatProvider) null));
        case TypeCode.String:
          return Operators.ModDouble(0.0, Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 57:
          return Operators.ModInt16((short) Operators.ToVBBool(convertible1), (short) 0);
        case (TypeCode) 60:
          return Operators.ModInt16((short) Operators.ToVBBool(convertible1), (short) Operators.ToVBBool(convertible2));
        case (TypeCode) 62:
          return Operators.ModSByte(Operators.ToVBBool(convertible1), convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.ModInt16((short) Operators.ToVBBool(convertible1), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.ModInt32((int) Operators.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 67:
        case (TypeCode) 68:
          return Operators.ModInt64((long) Operators.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 69:
        case (TypeCode) 72:
          return Operators.ModDecimal(Operators.ToVBBoolConv(convertible1), (IConvertible) convertible2.ToDecimal((IFormatProvider) null));
        case (TypeCode) 70:
          return Operators.ModSingle((float) Operators.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return Operators.ModDouble((double) Operators.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 75:
          return Operators.ModDouble((double) Operators.ToVBBool(convertible1), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 95:
          return Operators.ModSByte(convertible1.ToSByte((IFormatProvider) null), (sbyte) 0);
        case (TypeCode) 98:
          return Operators.ModSByte(convertible1.ToSByte((IFormatProvider) null), Operators.ToVBBool(convertible2));
        case (TypeCode) 100:
          return Operators.ModSByte(convertible1.ToSByte((IFormatProvider) null), convertible2.ToSByte((IFormatProvider) null));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return Operators.ModInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
        case (TypeCode) 180:
          return Operators.ModInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 125:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 163:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 220:
          return Operators.ModInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 107:
        case (TypeCode) 110:
        case (TypeCode) 129:
        case (TypeCode) 145:
        case (TypeCode) 148:
        case (TypeCode) 167:
        case (TypeCode) 183:
        case (TypeCode) 186:
        case (TypeCode) 205:
        case (TypeCode) 221:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 243:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 300:
          return Operators.ModDecimal(convertible1, convertible2);
        case (TypeCode) 108:
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 165:
        case (TypeCode) 184:
        case (TypeCode) 203:
        case (TypeCode) 222:
        case (TypeCode) 241:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return Operators.ModSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 109:
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 166:
        case (TypeCode) 185:
        case (TypeCode) 204:
        case (TypeCode) 223:
        case (TypeCode) 242:
        case (TypeCode) 261:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return Operators.ModDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.ModDouble(convertible1.ToDouble((IFormatProvider) null), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 114:
          return Operators.ModByte(convertible1.ToByte((IFormatProvider) null), (byte) 0);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return Operators.ModInt16(convertible1.ToInt16((IFormatProvider) null), (short) Operators.ToVBBool(convertible2));
        case (TypeCode) 120:
          return Operators.ModByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 122:
        case (TypeCode) 158:
        case (TypeCode) 160:
          return Operators.ModUInt16(convertible1.ToUInt16((IFormatProvider) null), convertible2.ToUInt16((IFormatProvider) null));
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
        case (TypeCode) 200:
          return Operators.ModUInt32(convertible1.ToUInt32((IFormatProvider) null), convertible2.ToUInt32((IFormatProvider) null));
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
        case (TypeCode) 240:
          return Operators.ModUInt64(convertible1.ToUInt64((IFormatProvider) null), convertible2.ToUInt64((IFormatProvider) null));
        case (TypeCode) 133:
          return Operators.ModInt16(convertible1.ToInt16((IFormatProvider) null), (short) 0);
        case (TypeCode) 152:
          return Operators.ModUInt16(convertible1.ToUInt16((IFormatProvider) null), (ushort) 0);
        case (TypeCode) 155:
        case (TypeCode) 174:
          return Operators.ModInt32(convertible1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(convertible2));
        case (TypeCode) 171:
          return Operators.ModInt32(convertible1.ToInt32((IFormatProvider) null), 0);
        case (TypeCode) 190:
          return Operators.ModUInt32(convertible1.ToUInt32((IFormatProvider) null), 0U);
        case (TypeCode) 193:
        case (TypeCode) 212:
          return Operators.ModInt64(convertible1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(convertible2));
        case (TypeCode) 209:
          return Operators.ModInt64(convertible1.ToInt64((IFormatProvider) null), 0L);
        case (TypeCode) 228:
          return Operators.ModUInt64(convertible1.ToUInt64((IFormatProvider) null), 0UL);
        case (TypeCode) 231:
        case (TypeCode) 288:
          return Operators.ModDecimal(convertible1, Operators.ToVBBoolConv(convertible2));
        case (TypeCode) 247:
          return Operators.ModSingle(convertible1.ToSingle((IFormatProvider) null), 0.0f);
        case (TypeCode) 250:
          return Operators.ModSingle(convertible1.ToSingle((IFormatProvider) null), (float) Operators.ToVBBool(convertible2));
        case (TypeCode) 266:
          return Operators.ModDouble(convertible1.ToDouble((IFormatProvider) null), 0.0);
        case (TypeCode) 269:
          return Operators.ModDouble(convertible1.ToDouble((IFormatProvider) null), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 285:
          return Operators.ModDecimal(convertible1, (IConvertible) Decimal.Zero);
        case (TypeCode) 342:
          return Operators.ModDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), 0.0);
        case (TypeCode) 345:
          return Operators.ModDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), (double) Operators.ToVBBool(convertible2));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.ModDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 360:
          return Operators.ModDouble(Conversions.ToDouble(convertible1.ToString((IFormatProvider) null)), Conversions.ToDouble(convertible2.ToString((IFormatProvider) null)));
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.Modulus, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Modulus, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object ModSByte(sbyte Left, sbyte Right)
    {
      return (object) checked ((sbyte) unchecked ((int) Left % (int) Right));
    }

    private static object ModByte(byte Left, byte Right)
    {
      return (object) checked ((byte) unchecked ((uint) Left % (uint) Right));
    }

    private static object ModInt16(short Left, short Right)
    {
      int num = (int) Left % (int) Right;
      if (num < (int) short.MinValue || num > (int) short.MaxValue)
        return (object) num;
      return (object) checked ((short) num);
    }

    private static object ModUInt16(ushort Left, ushort Right)
    {
      return (object) checked ((ushort) unchecked ((uint) Left % (uint) Right));
    }

    private static object ModInt32(int Left, int Right)
    {
      long num = (long) Left % (long) Right;
      if (num < (long) int.MinValue || num > (long) int.MaxValue)
        return (object) num;
      return (object) checked ((int) num);
    }

    private static object ModUInt32(uint Left, uint Right)
    {
      return (object) (Left % Right);
    }

    private static object ModInt64(long Left, long Right)
    {
      if (Left == long.MinValue && Right == -1L)
        return (object) 0L;
      return (object) (Left % Right);
    }

    private static object ModUInt64(ulong Left, ulong Right)
    {
      return (object) (Left % Right);
    }

    private static object ModDecimal(IConvertible Left, IConvertible Right)
    {
      return (object) Decimal.Remainder(Left.ToDecimal((IFormatProvider) null), Right.ToDecimal((IFormatProvider) null));
    }

    private static object ModSingle(float Left, float Right)
    {
      return (object) (float) ((double) Left % (double) Right);
    }

    private static object ModDouble(double Left, double Right)
    {
      return (object) (Left % Right);
    }

    /// <summary>Represents the Visual Basic integer division (\) operator.</summary>
    /// <param name="Left">Required. Any numeric expression.</param>
    /// <param name="Right">Required. Any numeric expression.</param>
    /// <returns>The integer quotient of <paramref name="Left" /> divided by <paramref name="Right" />, which discards any remainder and retains only the integer portion.</returns>
    public static object IntDivideObject(object Left, object Right)
    {
      IConvertible conv1 = Left as IConvertible;
      TypeCode typeCode1 = conv1 != null ? conv1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible conv2 = Right as IConvertible;
      TypeCode typeCode2 = conv2 != null ? conv2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) typeCode1) * 19) + typeCode2)
      {
        case TypeCode.Empty:
          return Operators.IntDivideInt32(0, 0);
        case TypeCode.Boolean:
          return Operators.IntDivideInt16((short) 0, (short) Operators.ToVBBool(conv2));
        case TypeCode.SByte:
          return Operators.IntDivideSByte((sbyte) 0, conv2.ToSByte((IFormatProvider) null));
        case TypeCode.Byte:
          return Operators.IntDivideByte((byte) 0, conv2.ToByte((IFormatProvider) null));
        case TypeCode.Int16:
          return Operators.IntDivideInt16((short) 0, conv2.ToInt16((IFormatProvider) null));
        case TypeCode.UInt16:
          return Operators.IntDivideUInt16((ushort) 0, conv2.ToUInt16((IFormatProvider) null));
        case TypeCode.Int32:
          return Operators.IntDivideInt32(0, conv2.ToInt32((IFormatProvider) null));
        case TypeCode.UInt32:
          return Operators.IntDivideUInt32(0U, conv2.ToUInt32((IFormatProvider) null));
        case TypeCode.Int64:
          return Operators.IntDivideInt64(0L, conv2.ToInt64((IFormatProvider) null));
        case TypeCode.UInt64:
          return Operators.IntDivideUInt64(0UL, conv2.ToUInt64((IFormatProvider) null));
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return Operators.IntDivideInt64(0L, conv2.ToInt64((IFormatProvider) null));
        case TypeCode.String:
          return Operators.IntDivideInt64(0L, Conversions.ToLong(conv2.ToString((IFormatProvider) null)));
        case (TypeCode) 57:
          return Operators.IntDivideInt16((short) Operators.ToVBBool(conv1), (short) 0);
        case (TypeCode) 60:
          return Operators.IntDivideInt16((short) Operators.ToVBBool(conv1), (short) Operators.ToVBBool(conv2));
        case (TypeCode) 62:
          return Operators.IntDivideSByte(Operators.ToVBBool(conv1), conv2.ToSByte((IFormatProvider) null));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return Operators.IntDivideInt16((short) Operators.ToVBBool(conv1), conv2.ToInt16((IFormatProvider) null));
        case (TypeCode) 65:
        case (TypeCode) 66:
          return Operators.IntDivideInt32((int) Operators.ToVBBool(conv1), conv2.ToInt32((IFormatProvider) null));
        case (TypeCode) 67:
        case (TypeCode) 68:
        case (TypeCode) 69:
        case (TypeCode) 70:
        case (TypeCode) 71:
        case (TypeCode) 72:
          return Operators.IntDivideInt64((long) Operators.ToVBBool(conv1), conv2.ToInt64((IFormatProvider) null));
        case (TypeCode) 75:
          return Operators.IntDivideInt64((long) Operators.ToVBBool(conv1), Conversions.ToLong(conv2.ToString((IFormatProvider) null)));
        case (TypeCode) 95:
          return Operators.IntDivideSByte(conv1.ToSByte((IFormatProvider) null), (sbyte) 0);
        case (TypeCode) 98:
          return Operators.IntDivideSByte(conv1.ToSByte((IFormatProvider) null), Operators.ToVBBool(conv2));
        case (TypeCode) 100:
          return Operators.IntDivideSByte(conv1.ToSByte((IFormatProvider) null), conv2.ToSByte((IFormatProvider) null));
        case (TypeCode) 101:
        case (TypeCode) 102:
        case (TypeCode) 119:
        case (TypeCode) 121:
        case (TypeCode) 138:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return Operators.IntDivideInt16(conv1.ToInt16((IFormatProvider) null), conv2.ToInt16((IFormatProvider) null));
        case (TypeCode) 103:
        case (TypeCode) 104:
        case (TypeCode) 123:
        case (TypeCode) 141:
        case (TypeCode) 142:
        case (TypeCode) 157:
        case (TypeCode) 159:
        case (TypeCode) 161:
        case (TypeCode) 176:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 179:
        case (TypeCode) 180:
          return Operators.IntDivideInt32(conv1.ToInt32((IFormatProvider) null), conv2.ToInt32((IFormatProvider) null));
        case (TypeCode) 105:
        case (TypeCode) 106:
        case (TypeCode) 107:
        case (TypeCode) 108:
        case (TypeCode) 109:
        case (TypeCode) 110:
        case (TypeCode) 125:
        case (TypeCode) 127:
        case (TypeCode) 128:
        case (TypeCode) 129:
        case (TypeCode) 143:
        case (TypeCode) 144:
        case (TypeCode) 145:
        case (TypeCode) 146:
        case (TypeCode) 147:
        case (TypeCode) 148:
        case (TypeCode) 163:
        case (TypeCode) 165:
        case (TypeCode) 166:
        case (TypeCode) 167:
        case (TypeCode) 181:
        case (TypeCode) 182:
        case (TypeCode) 183:
        case (TypeCode) 184:
        case (TypeCode) 185:
        case (TypeCode) 186:
        case (TypeCode) 195:
        case (TypeCode) 197:
        case (TypeCode) 199:
        case (TypeCode) 201:
        case (TypeCode) 203:
        case (TypeCode) 204:
        case (TypeCode) 205:
        case (TypeCode) 214:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 217:
        case (TypeCode) 218:
        case (TypeCode) 219:
        case (TypeCode) 220:
        case (TypeCode) 221:
        case (TypeCode) 222:
        case (TypeCode) 223:
        case (TypeCode) 224:
        case (TypeCode) 233:
        case (TypeCode) 235:
        case (TypeCode) 237:
        case (TypeCode) 239:
        case (TypeCode) 241:
        case (TypeCode) 242:
        case (TypeCode) 243:
        case (TypeCode) 252:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 255:
        case (TypeCode) 256:
        case (TypeCode) 257:
        case (TypeCode) 258:
        case (TypeCode) 259:
        case (TypeCode) 260:
        case (TypeCode) 261:
        case (TypeCode) 262:
        case (TypeCode) 271:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 274:
        case (TypeCode) 275:
        case (TypeCode) 276:
        case (TypeCode) 277:
        case (TypeCode) 278:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 290:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 293:
        case (TypeCode) 294:
        case (TypeCode) 295:
        case (TypeCode) 296:
        case (TypeCode) 297:
        case (TypeCode) 298:
        case (TypeCode) 299:
        case (TypeCode) 300:
          return Operators.IntDivideInt64(conv1.ToInt64((IFormatProvider) null), conv2.ToInt64((IFormatProvider) null));
        case (TypeCode) 113:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 170:
        case (TypeCode) 189:
        case (TypeCode) 208:
        case (TypeCode) 227:
        case (TypeCode) 246:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return Operators.IntDivideInt64(conv1.ToInt64((IFormatProvider) null), Conversions.ToLong(conv2.ToString((IFormatProvider) null)));
        case (TypeCode) 114:
          return Operators.IntDivideByte(conv1.ToByte((IFormatProvider) null), (byte) 0);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return Operators.IntDivideInt16(conv1.ToInt16((IFormatProvider) null), (short) Operators.ToVBBool(conv2));
        case (TypeCode) 120:
          return Operators.IntDivideByte(conv1.ToByte((IFormatProvider) null), conv2.ToByte((IFormatProvider) null));
        case (TypeCode) 122:
        case (TypeCode) 158:
        case (TypeCode) 160:
          return Operators.IntDivideUInt16(conv1.ToUInt16((IFormatProvider) null), conv2.ToUInt16((IFormatProvider) null));
        case (TypeCode) 124:
        case (TypeCode) 162:
        case (TypeCode) 196:
        case (TypeCode) 198:
        case (TypeCode) 200:
          return Operators.IntDivideUInt32(conv1.ToUInt32((IFormatProvider) null), conv2.ToUInt32((IFormatProvider) null));
        case (TypeCode) 126:
        case (TypeCode) 164:
        case (TypeCode) 202:
        case (TypeCode) 234:
        case (TypeCode) 236:
        case (TypeCode) 238:
        case (TypeCode) 240:
          return Operators.IntDivideUInt64(conv1.ToUInt64((IFormatProvider) null), conv2.ToUInt64((IFormatProvider) null));
        case (TypeCode) 133:
          return Operators.IntDivideInt16(conv1.ToInt16((IFormatProvider) null), (short) 0);
        case (TypeCode) 152:
          return Operators.IntDivideUInt16(conv1.ToUInt16((IFormatProvider) null), (ushort) 0);
        case (TypeCode) 155:
        case (TypeCode) 174:
          return Operators.IntDivideInt32(conv1.ToInt32((IFormatProvider) null), (int) Operators.ToVBBool(conv2));
        case (TypeCode) 171:
          return Operators.IntDivideInt32(conv1.ToInt32((IFormatProvider) null), 0);
        case (TypeCode) 190:
          return Operators.IntDivideUInt32(conv1.ToUInt32((IFormatProvider) null), 0U);
        case (TypeCode) 193:
        case (TypeCode) 212:
        case (TypeCode) 231:
        case (TypeCode) 250:
        case (TypeCode) 269:
        case (TypeCode) 288:
          return Operators.IntDivideInt64(conv1.ToInt64((IFormatProvider) null), (long) Operators.ToVBBool(conv2));
        case (TypeCode) 209:
          return Operators.IntDivideInt64(conv1.ToInt64((IFormatProvider) null), 0L);
        case (TypeCode) 228:
          return Operators.IntDivideUInt64(conv1.ToUInt64((IFormatProvider) null), 0UL);
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return Operators.IntDivideInt64(conv1.ToInt64((IFormatProvider) null), 0L);
        case (TypeCode) 342:
          return Operators.IntDivideInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), 0L);
        case (TypeCode) 345:
          return Operators.IntDivideInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), (long) Operators.ToVBBool(conv2));
        case (TypeCode) 347:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 350:
        case (TypeCode) 351:
        case (TypeCode) 352:
        case (TypeCode) 353:
        case (TypeCode) 354:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return Operators.IntDivideInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), conv2.ToInt64((IFormatProvider) null));
        case (TypeCode) 360:
          return Operators.IntDivideInt64(Conversions.ToLong(conv1.ToString((IFormatProvider) null)), Conversions.ToLong(conv2.ToString((IFormatProvider) null)));
        default:
          if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
            throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.IntegralDivide, Left, Right);
          return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.IntegralDivide, new object[2]
          {
            Left,
            Right
          });
      }
    }

    private static object IntDivideSByte(sbyte Left, sbyte Right)
    {
      if (Left == sbyte.MinValue && Right == (sbyte) -1)
        return (object) (short) 128;
      return (object) checked ((sbyte) unchecked ((int) Left / (int) Right));
    }

    private static object IntDivideByte(byte Left, byte Right)
    {
      return (object) checked ((byte) unchecked ((uint) Left / (uint) Right));
    }

    private static object IntDivideInt16(short Left, short Right)
    {
      if (Left == short.MinValue && Right == (short) -1)
        return (object) 32768;
      return (object) checked ((short) unchecked ((int) Left / (int) Right));
    }

    private static object IntDivideUInt16(ushort Left, ushort Right)
    {
      return (object) checked ((ushort) unchecked ((uint) Left / (uint) Right));
    }

    private static object IntDivideInt32(int Left, int Right)
    {
      if (Left == int.MinValue && Right == -1)
        return (object) 2147483648L;
      return (object) (Left / Right);
    }

    private static object IntDivideUInt32(uint Left, uint Right)
    {
      return (object) (Left / Right);
    }

    private static object IntDivideInt64(long Left, long Right)
    {
      return (object) (Left / Right);
    }

    private static object IntDivideUInt64(ulong Left, ulong Right)
    {
      return (object) (Left / Right);
    }

    /// <summary>Represents the Visual Basic arithmetic left shift (&lt;&lt;) operator.</summary>
    /// <param name="Operand">Required. Integral numeric expression. The bit pattern to be shifted. The data type must be an integral type (<see langword="SByte" />, <see langword="Byte" />, <see langword="Short" />, <see langword="UShort" />, <see langword="Integer" />, <see langword="UInteger" />, <see langword="Long" />, or <see langword="ULong" />).</param>
    /// <param name="Amount">Required. Numeric expression. The number of bits to shift the bit pattern. The data type must be <see langword="Integer" /> or widen to <see langword="Integer" />.</param>
    /// <returns>An integral numeric value. The result of shifting the bit pattern. The data type is the same as that of <paramref name="Operand" />.</returns>
    public static object LeftShiftObject(object Operand, object Amount)
    {
      IConvertible convertible1 = Operand as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Operand != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Amount as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Amount != null ? TypeCode.Object : TypeCode.Empty);
      if (typeCode1 == TypeCode.Object || typeCode2 == TypeCode.Object)
        return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.ShiftLeft, new object[2]
        {
          Operand,
          Amount
        });
      switch (typeCode1)
      {
        case TypeCode.Empty:
          return (object) (0 << Conversions.ToInteger(Amount));
        case TypeCode.Boolean:
          return (object) (short) ((int) (short) -(convertible1.ToBoolean((IFormatProvider) null) ? 1 : 0) << (Conversions.ToInteger(Amount) & 15));
        case TypeCode.SByte:
          return (object) (sbyte) ((int) convertible1.ToSByte((IFormatProvider) null) << (Conversions.ToInteger(Amount) & 7));
        case TypeCode.Byte:
          return (object) (byte) ((uint) convertible1.ToByte((IFormatProvider) null) << (Conversions.ToInteger(Amount) & 7));
        case TypeCode.Int16:
          return (object) (short) ((int) convertible1.ToInt16((IFormatProvider) null) << (Conversions.ToInteger(Amount) & 15));
        case TypeCode.UInt16:
          return (object) (ushort) ((uint) convertible1.ToUInt16((IFormatProvider) null) << (Conversions.ToInteger(Amount) & 15));
        case TypeCode.Int32:
          return (object) (convertible1.ToInt32((IFormatProvider) null) << Conversions.ToInteger(Amount));
        case TypeCode.UInt32:
          return (object) (uint) ((int) convertible1.ToUInt32((IFormatProvider) null) << Conversions.ToInteger(Amount));
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return (object) (convertible1.ToInt64((IFormatProvider) null) << Conversions.ToInteger(Amount));
        case TypeCode.UInt64:
          return (object) (ulong) ((long) convertible1.ToUInt64((IFormatProvider) null) << Conversions.ToInteger(Amount));
        case TypeCode.String:
          return (object) (Conversions.ToLong(convertible1.ToString((IFormatProvider) null)) << Conversions.ToInteger(Amount));
        default:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.ShiftLeft, Operand);
      }
    }

    /// <summary>Represents the Visual Basic arithmetic right shift (&gt;&gt;) operator.</summary>
    /// <param name="Operand">Required. Integral numeric expression. The bit pattern to be shifted. The data type must be an integral type (<see langword="SByte" />, <see langword="Byte" />, <see langword="Short" />, <see langword="UShort" />, <see langword="Integer" />, <see langword="UInteger" />, <see langword="Long" />, or <see langword="ULong" />).</param>
    /// <param name="Amount">Required. Numeric expression. The number of bits to shift the bit pattern. The data type must be <see langword="Integer" /> or widen to <see langword="Integer" />.</param>
    /// <returns>An integral numeric value. The result of shifting the bit pattern. The data type is the same as that of <paramref name="Operand" />.</returns>
    public static object RightShiftObject(object Operand, object Amount)
    {
      IConvertible convertible1 = Operand as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Operand != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Amount as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Amount != null ? TypeCode.Object : TypeCode.Empty);
      if (typeCode1 == TypeCode.Object || typeCode2 == TypeCode.Object)
        return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.ShiftRight, new object[2]
        {
          Operand,
          Amount
        });
      switch (typeCode1)
      {
        case TypeCode.Empty:
          return (object) (0 >> Conversions.ToInteger(Amount));
        case TypeCode.Boolean:
          return (object) (short) ((int) (short) -(convertible1.ToBoolean((IFormatProvider) null) ? 1 : 0) >> (Conversions.ToInteger(Amount) & 15));
        case TypeCode.SByte:
          return (object) (sbyte) ((int) convertible1.ToSByte((IFormatProvider) null) >> (Conversions.ToInteger(Amount) & 7));
        case TypeCode.Byte:
          return (object) (byte) ((uint) convertible1.ToByte((IFormatProvider) null) >> (Conversions.ToInteger(Amount) & 7));
        case TypeCode.Int16:
          return (object) (short) ((int) convertible1.ToInt16((IFormatProvider) null) >> (Conversions.ToInteger(Amount) & 15));
        case TypeCode.UInt16:
          return (object) (ushort) ((uint) convertible1.ToUInt16((IFormatProvider) null) >> (Conversions.ToInteger(Amount) & 15));
        case TypeCode.Int32:
          return (object) (convertible1.ToInt32((IFormatProvider) null) >> Conversions.ToInteger(Amount));
        case TypeCode.UInt32:
          return (object) (convertible1.ToUInt32((IFormatProvider) null) >> Conversions.ToInteger(Amount));
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return (object) (convertible1.ToInt64((IFormatProvider) null) >> Conversions.ToInteger(Amount));
        case TypeCode.UInt64:
          return (object) (convertible1.ToUInt64((IFormatProvider) null) >> Conversions.ToInteger(Amount));
        case TypeCode.String:
          return (object) (Conversions.ToLong(convertible1.ToString((IFormatProvider) null)) >> Conversions.ToInteger(Amount));
        default:
          throw Operators.GetNoValidOperatorException(Symbols.UserDefinedOperator.ShiftRight, Operand);
      }
    }

    /// <summary>Represents the Visual Basic <see langword="Like" /> operator.</summary>
    /// <param name="Source">Required. Any expression.</param>
    /// <param name="Pattern">Required. Any string expression conforming to the pattern-matching conventions described in Like Operator.</param>
    /// <param name="CompareOption">Required. A <see cref="T:Ported.VisualBasic.CompareMethod" /> value that specifies that the operation use either text or binary comparison.</param>
    /// <returns>
    /// <see langword="True" /> if the string representation of the value in <paramref name="Source" /> satisfies the pattern that is contained in <paramref name="Pattern" />; otherwise, <see langword="False" />. <see langword="True" /> if both <paramref name="Source" /> and <paramref name="Pattern" /> are <see langword="Nothing" />.</returns>
    public static object LikeObject(object Source, object Pattern, CompareMethod CompareOption)
    {
      IConvertible convertible1 = Source as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Source != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Pattern as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Pattern != null ? TypeCode.Object : TypeCode.Empty);
      if (typeCode1 == TypeCode.Object && Source is char[])
        typeCode1 = TypeCode.String;
      if (typeCode2 == TypeCode.Object && Pattern is char[])
        typeCode2 = TypeCode.String;
      if (typeCode1 != TypeCode.Object && typeCode2 != TypeCode.Object)
        return (object) Operators.LikeString(Conversions.ToString(Source), Conversions.ToString(Pattern), CompareOption);
      return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Like, new object[2]
      {
        Source,
        Pattern
      });
    }

    /// <summary>Represents the Visual Basic <see langword="Like" /> operator.</summary>
    /// <param name="Source">Required. Any <see langword="String" /> expression. </param>
    /// <param name="Pattern">Required. Any <see langword="String" /> expression conforming to the pattern-matching conventions described in Like Operator.</param>
    /// <param name="CompareOption">Required. A <see cref="T:Ported.VisualBasic.CompareMethod" /> value that specifies that the operation use either text or binary comparison.</param>
    /// <returns>
    /// <see langword="True" /> if the value in <paramref name="Source" /> satisfies the pattern that is contained in <paramref name="Pattern" />; otherwise, <see langword="False" />. <see langword="True" /> if both <paramref name="Source" /> and <paramref name="Pattern" /> are empty.</returns>
    public static bool LikeString(string Source, string Pattern, CompareMethod CompareOption)
    {
      if (CompareOption == CompareMethod.Binary)
        return Operators.LikeStringBinary(Source, Pattern);
      return Operators.LikeStringText(Source, Pattern);
    }

    private static bool LikeStringBinary(string Source, string Pattern)
    {
      bool flag1 = false;
      int num1 = Pattern != null ? Pattern.Length : 0;
      int num2 = Source != null ? Source.Length : 0;
      int index1=0;
      char ch1=char.MinValue;
      if (index1 < num2)
        ch1 = Source[index1];
      int index2=0;
      bool flag2=false;
      int startIndex=0;
      while (index2 < num1)
      {
        char p = Pattern[index2];
        bool Match=false;
        char ch2=char.MinValue;
        if (p == '*' && !flag2)
        {
          int num3 = Operators.AsteriskSkip(Pattern.Substring(checked (index2 + 1)), Source.Substring(startIndex), checked (num2 - startIndex), CompareMethod.Binary, Strings.m_InvariantCompareInfo);
          if (num3 < 0)
            return false;
          if (num3 > 0)
          {
            checked { startIndex += num3; }
            if (startIndex < num2)
              ch1 = Source[startIndex];
          }
        }
        else if (p == '?' && !flag2)
        {
          checked { ++startIndex; }
          if (startIndex < num2)
            ch1 = Source[startIndex];
        }
        else if (p == '#' && !flag2)
        {
          if (char.IsDigit(ch1))
          {
            checked { ++startIndex; }
            if (startIndex < num2)
              ch1 = Source[startIndex];
          }
          else
            break;
        }
        else
        {
          bool flag3=false;
          bool flag4 = false;
          if (p == '-' && flag2 && (flag3 && !flag1) && (!flag4 && (checked (index2 + 1) >= num1 || Pattern[checked (index2 + 1)] != ']')))
          {
            flag4 = true;
          }
          else
          {
            bool SeenNot=false;
            if (p == '!' && flag2 && !SeenNot)
            {
              SeenNot = true;
              Match = true;
            }
            else if (p == '[' && !flag2)
            {
              flag2 = true;
              ch2 = char.MinValue;
              flag3 = false;
            }
            else if (p == ']' && flag2)
            {
              flag2 = false;
              if (flag3)
              {
                if (Match)
                {
                  checked { ++startIndex; }
                  if (startIndex < num2)
                    ch1 = Source[startIndex];
                }
                else
                  break;
              }
              else if (flag4)
              {
                if (!Match)
                  break;
              }
              else if (SeenNot)
              {
                if ('!' == ch1)
                {
                  checked { ++startIndex; }
                  if (startIndex < num2)
                    ch1 = Source[startIndex];
                }
                else
                  break;
              }
              Match = false;
              flag3 = false;
              SeenNot = false;
              flag4 = false;
            }
            else
            {
              flag3 = true;
              flag1 = false;
              if (flag2)
              {
                if (flag4)
                {
                  flag4 = false;
                  flag1 = true;
                  char ch3 = p;
                  if ((int) ch2 > (int) ch3)
                    throw ExceptionUtils.VbMakeException(93);
                  if (SeenNot && Match || !SeenNot && !Match)
                  {
                    Match = (int) ch1 > (int) ch2 && (int) ch1 <= (int) ch3;
                    if (SeenNot)
                      Match = !Match;
                  }
                }
                else
                {
                  ch2 = p;
                  Match = Operators.LikeStringCompareBinary(SeenNot, Match, p, ch1);
                }
              }
              else if ((int) p == (int) ch1 || SeenNot)
              {
                SeenNot = false;
                checked { ++startIndex; }
                if (startIndex < num2)
                  ch1 = Source[startIndex];
                else if (startIndex > num2)
                  return false;
              }
              else
                break;
            }
          }
        }
        checked { ++index2; }
      }
      if (flag2)
      {
        if (num2 == 0)
          return false;
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Pattern)
        }));
      }
      return index2 == num1 && startIndex == num2;
    }

    private static bool LikeStringText(string Source, string Pattern)
    {
      bool flag1 = false;
      int num1 = Pattern != null ? Pattern.Length : 0;
      int num2 = Source != null ? Source.Length : 0;
      int index1=0;
      char ch1=char.MinValue;
      if (index1 < num2)
        ch1 = Source[index1];
      CompareInfo compareInfo = Utils.GetCultureInfo().CompareInfo;
      CompareOptions compareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
      int index2=0;
      bool flag2=false;
      int startIndex=0;
      while (index2 < num1)
      {
        char p = Pattern[index2];
        bool Match = false;
        char ch2=char.MinValue;
        if (p == '*' && !flag2)
        {
          int num3 = Operators.AsteriskSkip(Pattern.Substring(checked (index2 + 1)), Source.Substring(startIndex), checked (num2 - startIndex), CompareMethod.Text, compareInfo);
          if (num3 < 0)
            return false;
          if (num3 > 0)
          {
            checked { startIndex += num3; }
            if (startIndex < num2)
              ch1 = Source[startIndex];
          }
        }
        else if (p == '?' && !flag2)
        {
          checked { ++startIndex; }
          if (startIndex < num2)
            ch1 = Source[startIndex];
        }
        else if (p == '#' && !flag2)
        {
          if (char.IsDigit(ch1))
          {
            checked { ++startIndex; }
            if (startIndex < num2)
              ch1 = Source[startIndex];
          }
          else
            break;
        }
        else
        {
          bool flag3 = false;
          bool flag4 = false;
          if (p == '-' && flag2 && (flag3 && !flag1) && (!flag4 && (checked (index2 + 1) >= num1 || Pattern[checked (index2 + 1)] != ']')))
          {
            flag4 = true;
          }
          else
          {
            bool SeenNot=false;
            if (p == '!' && flag2 && !SeenNot)
            {
              SeenNot = true;
              Match = true;
            }
            else if (p == '[' && !flag2)
            {
              flag2 = true;
              ch2 = char.MinValue;
              flag3 = false;
            }
            else if (p == ']' && flag2)
            {
              flag2 = false;
              if (flag3)
              {
                if (Match)
                {
                  checked { ++startIndex; }
                  if (startIndex < num2)
                    ch1 = Source[startIndex];
                }
                else
                  break;
              }
              else if (flag4)
              {
                if (!Match)
                  break;
              }
              else if (SeenNot)
              {
                if (compareInfo.Compare("!", Conversions.ToString(ch1)) == 0)
                {
                  checked { ++startIndex; }
                  if (startIndex < num2)
                    ch1 = Source[startIndex];
                }
                else
                  break;
              }
              Match = false;
              flag3 = false;
              SeenNot = false;
              flag4 = false;
            }
            else
            {
              flag3 = true;
              flag1 = false;
              if (flag2)
              {
                if (flag4)
                {
                  flag4 = false;
                  flag1 = true;
                  char ch3 = p;
                  if ((int) ch2 > (int) ch3)
                    throw ExceptionUtils.VbMakeException(93);
                  if (SeenNot && Match || !SeenNot && !Match)
                  {
                    Match = compareOptions != CompareOptions.Ordinal ? compareInfo.Compare(Conversions.ToString(ch2), Conversions.ToString(ch1), compareOptions) < 0 && compareInfo.Compare(Conversions.ToString(ch3), Conversions.ToString(ch1), compareOptions) >= 0 : (int) ch1 > (int) ch2 && (int) ch1 <= (int) ch3;
                    if (SeenNot)
                      Match = !Match;
                  }
                }
                else
                {
                  ch2 = p;
                  Match = Operators.LikeStringCompare(compareInfo, SeenNot, Match, p, ch1, compareOptions);
                }
              }
              else
              {
                if (compareOptions == CompareOptions.Ordinal)
                {
                  if ((int) p != (int) ch1 && !SeenNot)
                    break;
                }
                else
                {
                  string string1 = Conversions.ToString(p);
                  string string2 = Conversions.ToString(ch1);
                  while (checked (index2 + 1) < num1 && (UnicodeCategory.ModifierSymbol == char.GetUnicodeCategory(Pattern[checked (index2 + 1)]) || UnicodeCategory.NonSpacingMark == char.GetUnicodeCategory(Pattern[checked (index2 + 1)])))
                  {
                    string1 += Conversions.ToString(Pattern[checked (index2 + 1)]);
                    checked { ++index2; }
                  }
                  while (checked (startIndex + 1) < num2 && (UnicodeCategory.ModifierSymbol == char.GetUnicodeCategory(Source[checked (startIndex + 1)]) || UnicodeCategory.NonSpacingMark == char.GetUnicodeCategory(Source[checked (startIndex + 1)])))
                  {
                    string2 += Conversions.ToString(Source[checked (startIndex + 1)]);
                    checked { ++startIndex; }
                  }
                  if (compareInfo.Compare(string1, string2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) != 0 && !SeenNot)
                    break;
                }
                SeenNot = false;
                checked { ++startIndex; }
                if (startIndex < num2)
                  ch1 = Source[startIndex];
                else if (startIndex > num2)
                  return false;
              }
            }
          }
        }
        checked { ++index2; }
      }
      if (flag2)
      {
        if (num2 == 0)
          return false;
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Pattern)
        }));
      }
      return index2 == num1 && startIndex == num2;
    }

    private static bool LikeStringCompareBinary(bool SeenNot, bool Match, char p, char s)
    {
      if (SeenNot && Match)
        return (int) p != (int) s;
      if (!SeenNot && !Match)
        return (int) p == (int) s;
      return Match;
    }

    private static bool LikeStringCompare(CompareInfo ci, bool SeenNot, bool Match, char p, char s, CompareOptions Options)
    {
      if (SeenNot && Match)
      {
        if (Options == CompareOptions.Ordinal)
          return (int) p != (int) s;
        return ci.Compare(Conversions.ToString(p), Conversions.ToString(s), Options) != 0;
      }
      if (SeenNot || Match)
        return Match;
      if (Options == CompareOptions.Ordinal)
        return (int) p == (int) s;
      return ci.Compare(Conversions.ToString(p), Conversions.ToString(s), Options) == 0;
    }

    private static int AsteriskSkip(string Pattern, string Source, int SourceEndIndex, CompareMethod CompareOption, CompareInfo ci)
    {
      int num1 = Strings.Len(Pattern);
      int length=0;
      int Count=0;
      while (length < num1)
      {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        switch (Pattern[length])
        {
          case '!':
            if (Pattern[checked (length + 1)] == ']')
            {
              flag2 = true;
              break;
            }
            flag1 = true;
            break;
          case '#':
          case '?':
            if (flag3)
            {
              flag2 = true;
              break;
            }
            checked { ++Count; }
            flag1 = true;
            break;
          case '*':
            if (Count > 0)
            {
              if (flag1)
              {
                int num2 = Operators.MultipleAsteriskSkip(Pattern, Source, Count, CompareOption);
                return checked (SourceEndIndex - num2);
              }
              string str = Pattern.Substring(0, length);
              CompareOptions options = CompareOption != CompareMethod.Binary ? CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth : CompareOptions.Ordinal;
              return ci.LastIndexOf(Source, str, options);
            }
            break;
          case '-':
            if (Pattern[checked (length + 1)] == ']')
            {
              flag2 = true;
              break;
            }
            break;
          case '[':
            if (flag3)
            {
              flag2 = true;
              break;
            }
            flag3 = true;
            break;
          case ']':
            if (flag2 || !flag3)
            {
              checked { ++Count; }
              flag1 = true;
            }
            flag2 = false;
            flag3 = false;
            break;
          default:
            if (flag3)
            {
              flag2 = true;
              break;
            }
            checked { ++Count; }
            break;
        }
        checked { ++length; }
      }
      return checked (SourceEndIndex - Count);
    }

    private static int MultipleAsteriskSkip(string Pattern, string Source, int Count, CompareMethod CompareOption)
    {
      int num = Strings.Len(Source);
      while (Count < num)
      {
        string Source1 = Source.Substring(checked (num - Count));
        bool flag;
        try
        {
          flag = Operators.LikeString(Source1, Pattern, CompareOption);
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
          flag = false;
        }
        if (!flag)
          checked { ++Count; }
        else
          break;
      }
      return Count;
    }

    /// <summary>Represents the Visual Basic concatenation (&amp;) operator.</summary>
    /// <param name="Left">Required. Any expression.</param>
    /// <param name="Right">Required. Any expression.</param>
    /// <returns>A string representing the concatenation of <paramref name="Left" /> and <paramref name="Right" />.</returns>
    public static object ConcatenateObject(object Left, object Right)
    {
      IConvertible convertible1 = Left as IConvertible;
      TypeCode typeCode1 = convertible1 != null ? convertible1.GetTypeCode() : (Left != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = Right as IConvertible;
      TypeCode typeCode2 = convertible2 != null ? convertible2.GetTypeCode() : (Right != null ? TypeCode.Object : TypeCode.Empty);
      if (typeCode1 == TypeCode.Object && Left is char[])
        typeCode1 = TypeCode.String;
      if (typeCode2 == TypeCode.Object && Right is char[])
        typeCode2 = TypeCode.String;
      if (typeCode1 == TypeCode.Object || typeCode2 == TypeCode.Object)
        return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Concatenate, new object[2]
        {
          Left,
          Right
        });
      bool flag1 = typeCode1 == TypeCode.DBNull;
      bool flag2 = typeCode2 == TypeCode.DBNull;
      if (flag1 & flag2)
        return Left;
      if (flag1 & !flag2)
        Left = (object) "";
      else if (flag2 & !flag1)
        Right = (object) "";
      return (object) (Conversions.ToString(Left) + Conversions.ToString(Right));
    }

    private enum CompareClass
    {
      Less = -1,
      Equal = 0,
      Greater = 1,
      Unordered = 2,
      UserDefined = 3,
      Undefined = 4,
    }
  }
}
