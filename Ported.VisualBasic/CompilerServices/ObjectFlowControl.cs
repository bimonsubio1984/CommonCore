// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.ObjectFlowControl
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>The Visual Basic compiler uses this class for object flow control; it is not meant to be called directly from your code.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class ObjectFlowControl
  {
    private ObjectFlowControl()
    {
    }

    /// <summary>Checks for a synchronization lock on the specified type.</summary>
    /// <param name="Expression">The data type for which to check for synchronization lock.</param>
    public static void CheckForSyncLockOnValueType(object Expression)
    {
      if (Expression != null && Expression.GetType().IsValueType)
        throw new ArgumentException(Utils.GetResourceString("SyncLockRequiresReferenceType1", new string[1]
        {
          Utils.VBFriendlyName(Expression.GetType())
        }));
    }

    /// <summary>Provides services to the Visual Basic compiler for compiling <see langword="For...Next" /> loops.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ForLoopControl
    {
      private object Counter;
      private object Limit;
      private object StepValue;
      private bool PositiveStep;
      private Type EnumType;
      private Type WidestType;
      private TypeCode WidestTypeCode;
      private bool UseUserDefinedOperators;
      private Symbols.Method OperatorPlus;
      private Symbols.Method OperatorGreaterEqual;
      private Symbols.Method OperatorLessEqual;

      private ForLoopControl()
      {
      }

      private static Type GetWidestType(Type Type1, Type Type2)
      {
        if (Type1 == null || Type2 == null)
          return (Type) null;
        if (!Type1.IsEnum && !Type2.IsEnum)
        {
          TypeCode typeCode1 = Symbols.GetTypeCode(Type1);
          TypeCode typeCode2 = Symbols.GetTypeCode(Type2);
          if (Symbols.IsNumericType(typeCode1) && Symbols.IsNumericType(typeCode2))
            return Symbols.MapTypeCodeToType(ConversionResolution.ForLoopWidestTypeCode[(int) typeCode1][(int) typeCode2]);
        }
        Type TargetType1 = Type2;
        Type SourceType1 = Type1;
        Symbols.Method method1 = (Symbols.Method) null;
        ref Symbols.Method local1 = ref method1;
        switch (ConversionResolution.ClassifyConversion(TargetType1, SourceType1, ref local1))
        {
          case ConversionResolution.ConversionClass.Identity:
          case ConversionResolution.ConversionClass.Widening:
            return Type2;
          default:
            Type TargetType2 = Type1;
            Type SourceType2 = Type2;
            Symbols.Method method2 = (Symbols.Method) null;
            ref Symbols.Method local2 = ref method2;
            if (ConversionResolution.ClassifyConversion(TargetType2, SourceType2, ref local2) == ConversionResolution.ConversionClass.Widening)
              return Type1;
            return (Type) null;
        }
      }

      private static Type GetWidestType(Type Type1, Type Type2, Type Type3)
      {
        return ObjectFlowControl.ForLoopControl.GetWidestType(Type1, ObjectFlowControl.ForLoopControl.GetWidestType(Type2, Type3));
      }

      private static object ConvertLoopElement(string ElementName, object Value, Type SourceType, Type TargetType)
      {
        try
        {
          return Conversions.ChangeType(Value, TargetType);
        }
        catch (AccessViolationException ex)
        {
          throw ex;
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
          throw new ArgumentException(Utils.GetResourceString("ForLoop_ConvertToType3", ElementName, Utils.VBFriendlyName(SourceType), Utils.VBFriendlyName(TargetType)));
        }
      }

      private static Symbols.Method VerifyForLoopOperator(Symbols.UserDefinedOperator Op, object ForLoopArgument, Type ForLoopArgumentType)
      {
        Symbols.Method userDefinedOperator = Operators.GetCallableUserDefinedOperator(Op, ForLoopArgument, ForLoopArgument);
        if ((object) userDefinedOperator == null)
          throw new ArgumentException(Utils.GetResourceString("ForLoop_OperatorRequired2", Utils.VBFriendlyNameOfType(ForLoopArgumentType, true), Symbols.OperatorNames[(int) Op]));
        MethodInfo methodInfo = userDefinedOperator.AsMethod() as MethodInfo;
        ParameterInfo[] parameters = methodInfo.GetParameters();
        switch (Op)
        {
          case Symbols.UserDefinedOperator.Plus:
          case Symbols.UserDefinedOperator.Minus:
            if (parameters.Length != 2 || parameters[0].ParameterType != ForLoopArgumentType || (parameters[1].ParameterType != ForLoopArgumentType || methodInfo.ReturnType != ForLoopArgumentType))
              throw new ArgumentException(Utils.GetResourceString("ForLoop_UnacceptableOperator2", userDefinedOperator.ToString(), Utils.VBFriendlyNameOfType(ForLoopArgumentType, true)));
            break;
          case Symbols.UserDefinedOperator.LessEqual:
          case Symbols.UserDefinedOperator.GreaterEqual:
            if (parameters.Length != 2 || parameters[0].ParameterType != ForLoopArgumentType || parameters[1].ParameterType != ForLoopArgumentType)
              throw new ArgumentException(Utils.GetResourceString("ForLoop_UnacceptableRelOperator2", userDefinedOperator.ToString(), Utils.VBFriendlyNameOfType(ForLoopArgumentType, true)));
            break;
        }
        return userDefinedOperator;
      }

      /// <summary>Initializes a <see langword="For...Next" /> loop.</summary>
      /// <param name="Counter">The loop counter variable.</param>
      /// <param name="Start">The initial value of the loop counter.</param>
      /// <param name="Limit">The value of the <see langword="To" /> option.</param>
      /// <param name="StepValue">The value of the <see langword="Step" /> option.</param>
      /// <param name="LoopForResult">An object that contains verified values for loop values.</param>
      /// <param name="CounterResult">The counter value for the next loop iteration.</param>
      /// <returns>
      /// <see langword="False" /> if the loop has terminated; otherwise, <see langword="True" />.</returns>
      public static bool ForLoopInitObj(object Counter, object Start, object Limit, object StepValue, ref object LoopForResult, ref object CounterResult)
      {
        if (Start == null)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
          {
            nameof (Start)
          }));
        if (Limit == null)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
          {
            nameof (Limit)
          }));
        if (StepValue == null)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
          {
            "Step"
          }));
        Type type1 = Start.GetType();
        Type type2 = Limit.GetType();
        Type type3 = StepValue.GetType();
        Type widestType = ObjectFlowControl.ForLoopControl.GetWidestType(type3, type1, type2);
        if (widestType == null)
          throw new ArgumentException(Utils.GetResourceString("ForLoop_CommonType3", Utils.VBFriendlyName(type1), Utils.VBFriendlyName(type2), Utils.VBFriendlyName(StepValue)));
        ObjectFlowControl.ForLoopControl LoopFor = new ObjectFlowControl.ForLoopControl();
        TypeCode TypeCode = Symbols.GetTypeCode(widestType);
        if (TypeCode == TypeCode.Object)
          LoopFor.UseUserDefinedOperators = true;
        if (TypeCode == TypeCode.String)
          TypeCode = TypeCode.Double;
        TypeCode typeCode1 = Type.GetTypeCode(type1);
        TypeCode typeCode2 = Type.GetTypeCode(type2);
        TypeCode typeCode3 = Type.GetTypeCode(type3);
        Type type4 = (Type) null;
        if (typeCode1 == TypeCode && type1.IsEnum)
          type4 = type1;
        if (typeCode2 == TypeCode && type2.IsEnum)
        {
          if (type4 != null && type4 != type2)
          {
            type4 = (Type) null;
            goto label_20;
          }
          else
            type4 = type2;
        }
        if (typeCode3 == TypeCode && type3.IsEnum)
          type4 = type4 == null || type4 == type3 ? type3 : (Type) null;
label_20:
        LoopFor.EnumType = type4;
        LoopFor.WidestType = LoopFor.UseUserDefinedOperators ? widestType : Symbols.MapTypeCodeToType(TypeCode);
        LoopFor.WidestTypeCode = TypeCode;
        LoopFor.Counter = ObjectFlowControl.ForLoopControl.ConvertLoopElement(nameof (Start), Start, type1, LoopFor.WidestType);
        LoopFor.Limit = ObjectFlowControl.ForLoopControl.ConvertLoopElement(nameof (Limit), Limit, type2, LoopFor.WidestType);
        LoopFor.StepValue = ObjectFlowControl.ForLoopControl.ConvertLoopElement("Step", StepValue, type3, LoopFor.WidestType);
        if (LoopFor.UseUserDefinedOperators)
        {
          LoopFor.OperatorPlus = ObjectFlowControl.ForLoopControl.VerifyForLoopOperator(Symbols.UserDefinedOperator.Plus, LoopFor.Counter, LoopFor.WidestType);
          ObjectFlowControl.ForLoopControl.VerifyForLoopOperator(Symbols.UserDefinedOperator.Minus, LoopFor.Counter, LoopFor.WidestType);
          LoopFor.OperatorLessEqual = ObjectFlowControl.ForLoopControl.VerifyForLoopOperator(Symbols.UserDefinedOperator.LessEqual, LoopFor.Counter, LoopFor.WidestType);
          LoopFor.OperatorGreaterEqual = ObjectFlowControl.ForLoopControl.VerifyForLoopOperator(Symbols.UserDefinedOperator.GreaterEqual, LoopFor.Counter, LoopFor.WidestType);
        }
        LoopFor.PositiveStep = Operators.ConditionalCompareObjectGreaterEqual(LoopFor.StepValue, Operators.SubtractObject(LoopFor.StepValue, LoopFor.StepValue), false);
        LoopForResult = (object) LoopFor;
        CounterResult = LoopFor.EnumType == null ? LoopFor.Counter : Enum.ToObject(LoopFor.EnumType, LoopFor.Counter);
        return ObjectFlowControl.ForLoopControl.CheckContinueLoop(LoopFor);
      }

      /// <summary>Increments a <see langword="For...Next" /> loop.</summary>
      /// <param name="Counter">The loop counter variable.</param>
      /// <param name="LoopObj">An object that contains verified values for loop values.</param>
      /// <param name="CounterResult">The counter value for the next loop iteration.</param>
      /// <returns>
      /// <see langword="False" /> if the loop has terminated; otherwise, <see langword="True" />.</returns>
      public static bool ForNextCheckObj(object Counter, object LoopObj, ref object CounterResult)
      {
        if (LoopObj == null)
          throw ExceptionUtils.VbMakeException(92);
        if (Counter == null)
          throw new NullReferenceException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
          {
            nameof (Counter)
          }));
        ObjectFlowControl.ForLoopControl LoopFor = (ObjectFlowControl.ForLoopControl) LoopObj;
        bool flag = false;
        if (!LoopFor.UseUserDefinedOperators)
        {
          TypeCode typeCode = ((IConvertible) Counter).GetTypeCode();
          if (typeCode != LoopFor.WidestTypeCode || typeCode == TypeCode.String)
          {
            if (typeCode == System.TypeCode.Object)
              throw new ArgumentException(Utils.GetResourceString("ForLoop_CommonType2", Utils.VBFriendlyName(Symbols.MapTypeCodeToType(typeCode)), Utils.VBFriendlyName(LoopFor.WidestType)));
            TypeCode TypeCode = Symbols.GetTypeCode(ObjectFlowControl.ForLoopControl.GetWidestType(Symbols.MapTypeCodeToType(typeCode), LoopFor.WidestType));
            if (TypeCode == TypeCode.String)
              TypeCode = TypeCode.Double;
            LoopFor.WidestTypeCode = TypeCode;
            LoopFor.WidestType = Symbols.MapTypeCodeToType(TypeCode);
            flag = true;
          }
        }
        if (flag || LoopFor.UseUserDefinedOperators)
        {
          Counter = ObjectFlowControl.ForLoopControl.ConvertLoopElement("Start", Counter, Counter.GetType(), LoopFor.WidestType);
          if (!LoopFor.UseUserDefinedOperators)
          {
            LoopFor.Limit = ObjectFlowControl.ForLoopControl.ConvertLoopElement("Limit", LoopFor.Limit, LoopFor.Limit.GetType(), LoopFor.WidestType);
            LoopFor.StepValue = ObjectFlowControl.ForLoopControl.ConvertLoopElement("Step", LoopFor.StepValue, LoopFor.StepValue.GetType(), LoopFor.WidestType);
          }
        }
        if (!LoopFor.UseUserDefinedOperators)
        {
          LoopFor.Counter = Operators.AddObject(Counter, LoopFor.StepValue);
          TypeCode typeCode = ((IConvertible) LoopFor.Counter).GetTypeCode();
          CounterResult = LoopFor.EnumType == null ? LoopFor.Counter : Enum.ToObject(LoopFor.EnumType, LoopFor.Counter);
          if (typeCode != LoopFor.WidestTypeCode)
          {
            LoopFor.Limit = Conversions.ChangeType(LoopFor.Limit, Symbols.MapTypeCodeToType(typeCode));
            LoopFor.StepValue = Conversions.ChangeType(LoopFor.StepValue, Symbols.MapTypeCodeToType(typeCode));
            return false;
          }
        }
        else
        {
          LoopFor.Counter = Operators.InvokeUserDefinedOperator(LoopFor.OperatorPlus, true, Counter, LoopFor.StepValue);
          if (LoopFor.Counter.GetType() != LoopFor.WidestType)
            LoopFor.Counter = ObjectFlowControl.ForLoopControl.ConvertLoopElement("Start", LoopFor.Counter, LoopFor.Counter.GetType(), LoopFor.WidestType);
          CounterResult = LoopFor.Counter;
        }
        return ObjectFlowControl.ForLoopControl.CheckContinueLoop(LoopFor);
      }

      /// <summary>Checks for valid values for the loop counter, <see langword="Step" />, and <see langword="To" /> values.</summary>
      /// <param name="count">Required. A <see langword="Single" /> value that represents the initial value passed for the loop counter variable.</param>
      /// <param name="limit">Required. A <see langword="Single" /> value that represents the value passed by using the <see langword="To" /> keyword.</param>
      /// <param name="StepValue">Required. A <see langword="Single" /> value that represents the value passed by using the <see langword="Step" /> keyword.</param>
      /// <returns>
      /// <see langword="True" /> if <paramref name="StepValue" /> is greater than zero and <paramref name="count" /> is less than or equal to <paramref name="limit" />, or if <paramref name="StepValue" /> is less than or equal to zero and <paramref name="count" /> is greater than or equal to <paramref name="limit" />; otherwise, <see langword="False" />.</returns>
      public static bool ForNextCheckR4(float count, float limit, float StepValue)
      {
        if ((double) StepValue >= 0.0)
          return (double) count <= (double) limit;
        return (double) count >= (double) limit;
      }

      /// <summary>Checks for valid values for the loop counter, <see langword="Step" />, and <see langword="To" /> values.</summary>
      /// <param name="count">Required. A <see langword="Double" /> value that represents the initial value passed for the loop counter variable.</param>
      /// <param name="limit">Required. A <see langword="Double" /> value that represents the value passed by using the <see langword="To" /> keyword.</param>
      /// <param name="StepValue">Required. A <see langword="Double" /> value that represents the value passed by using the <see langword="Step" /> keyword.</param>
      /// <returns>
      /// <see langword="True" /> if <paramref name="StepValue" /> is greater than zero and <paramref name="count" /> is less than or equal to <paramref name="limit" />, or if <paramref name="StepValue" /> is less than or equal to zero and <paramref name="count" /> is greater than or equal to <paramref name="limit" />; otherwise, <see langword="False" />.</returns>
      public static bool ForNextCheckR8(double count, double limit, double StepValue)
      {
        if (StepValue >= 0.0)
          return count <= limit;
        return count >= limit;
      }

      /// <summary>Checks for valid values for the loop counter, <see langword="Step" />, and <see langword="To" /> values.</summary>
      /// <param name="count">Required. A <see langword="Decimal" /> value that represents the initial value passed for the loop counter variable.</param>
      /// <param name="limit">Required. A <see langword="Decimal" /> value that represents the value passed by using the <see langword="To" /> keyword.</param>
      /// <param name="StepValue">Required. A <see langword="Decimal" /> value that represents the value passed by using the <see langword="Step" /> keyword.</param>
      /// <returns>
      /// <see langword="True" /> if <paramref name="StepValue" /> is greater than zero and <paramref name="count" /> is less than or equal to <paramref name="limit" /> or <paramref name="StepValue" /> is less than or equal to zero and <paramref name="count" /> is greater than or equal to <paramref name="limit" />; otherwise, <see langword="False" />.</returns>
      public static bool ForNextCheckDec(Decimal count, Decimal limit, Decimal StepValue)
      {
        if (Decimal.Compare(StepValue, Decimal.Zero) >= 0)
          return Decimal.Compare(count, limit) <= 0;
        return Decimal.Compare(count, limit) >= 0;
      }

      private static bool CheckContinueLoop(ObjectFlowControl.ForLoopControl LoopFor)
      {
        if (!LoopFor.UseUserDefinedOperators)
        {
          try
          {
            int num = ((IComparable) LoopFor.Counter).CompareTo(LoopFor.Limit);
            if (LoopFor.PositiveStep)
              return num <= 0;
            return num >= 0;
          }
          catch (InvalidCastException ex)
          {
            throw new ArgumentException(Utils.GetResourceString("Argument_IComparable2", "loop control variable", Utils.VBFriendlyName(LoopFor.Counter)));
          }
        }
        else
        {
          if (LoopFor.PositiveStep)
            return Conversions.ToBoolean(Operators.InvokeUserDefinedOperator(LoopFor.OperatorLessEqual, true, LoopFor.Counter, LoopFor.Limit));
          return Conversions.ToBoolean(Operators.InvokeUserDefinedOperator(LoopFor.OperatorGreaterEqual, true, LoopFor.Counter, LoopFor.Limit));
        }
      }
    }
  }
}
