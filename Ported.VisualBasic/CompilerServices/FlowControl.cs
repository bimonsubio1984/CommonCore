// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.FlowControl
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>Provides services to the Visual Basic compiler for compiling <see langword="For...Next" /> and <see langword="For Each" /> loops.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class FlowControl
  {
    private FlowControl()
    {
    }

    /// <summary>Checks for valid values for the loop counter, <see langword="Step" />, and <see langword="To" /> values.</summary>
    /// <param name="count">Required. A <see langword="Single" /> value that represents the initial value passed for the loop counter variable.</param>
    /// <param name="limit">Required. A <see langword="Single" /> value that represents the value passed by using the <see langword="To" /> keyword.</param>
    /// <param name="StepValue">Required. A <see langword="Single" /> value that represents the value passed by using the <see langword="Step" /> keyword.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="StepValue" /> is greater than zero and <paramref name="count" /> is less than or equal to <paramref name="limit" />, or if <paramref name="StepValue" /> is less than or equal to zero and <paramref name="count" /> is greater than or equal to <paramref name="limit" />; otherwise, <see langword="False" />.</returns>
    public static bool ForNextCheckR4(float count, float limit, float StepValue)
    {
      if ((double) StepValue > 0.0)
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
      if (StepValue > 0.0)
        return count <= limit;
      return count >= limit;
    }

    /// <summary>Checks for valid values for the loop counter, <see langword="Step" />, and <see langword="To" /> values.</summary>
    /// <param name="count">Required. A <see langword="Decimal" /> value that represents the initial value passed for the loop counter variable.</param>
    /// <param name="limit">Required. A <see langword="Decimal" /> value that represents the value passed by using the <see langword="To" /> keyword.</param>
    /// <param name="StepValue">Required. A <see langword="Decimal" /> value that represents the value passed by using the <see langword="Step" /> keyword.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="StepValue" /> is greater than zero and <paramref name="count" /> is less than or equal to <paramref name="limit" />, or if <paramref name="StepValue" /> is less than or equal to zero and <paramref name="count" /> is greater than or equal to <paramref name="limit" />; otherwise, <see langword="False" />.</returns>
    public static bool ForNextCheckDec(Decimal count, Decimal limit, Decimal StepValue)
    {
      if (StepValue < Decimal.Zero)
        return count >= limit;
      return count <= limit;
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
      TypeCode widestType = ObjectType.GetWidestType(Start, Limit, false);
      TypeCode typeCode1 = ObjectType.GetWidestType(StepValue, widestType);
      if (typeCode1 == TypeCode.String)
        typeCode1 = TypeCode.Double;
      if (typeCode1 == TypeCode.Object)
        throw new ArgumentException(Utils.GetResourceString("ForLoop_CommonType3", Utils.VBFriendlyName(type1), Utils.VBFriendlyName(type2), Utils.VBFriendlyName(StepValue)));
      FlowControl.ObjectFor LoopFor = new FlowControl.ObjectFor();
      TypeCode typeCode2 = Type.GetTypeCode(type1);
      TypeCode typeCode3 = Type.GetTypeCode(type2);
      TypeCode typeCode4 = Type.GetTypeCode(type3);
      Type type4 = (Type) null;
      if (typeCode2 == typeCode1 && type1.IsEnum)
        type4 = type1;
      if (typeCode3 == typeCode1 && type2.IsEnum)
      {
        if (type4 != null && type4 != type2)
        {
          type4 = (Type) null;
          goto label_18;
        }
        else
          type4 = type2;
      }
      if (typeCode4 == typeCode1 && type3.IsEnum)
        type4 = type4 == null || type4 == type3 ? type3 : (Type) null;
label_18:
      LoopFor.EnumType = type4;
      try
      {
        LoopFor.Counter = ObjectType.CTypeHelper(Start, typeCode1);
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
        throw new ArgumentException(Utils.GetResourceString("ForLoop_ConvertToType3", nameof (Start), Utils.VBFriendlyName(type1), Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(typeCode1))));
      }
      try
      {
        LoopFor.Limit = ObjectType.CTypeHelper(Limit, typeCode1);
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
        throw new ArgumentException(Utils.GetResourceString("ForLoop_ConvertToType3", nameof (Limit), Utils.VBFriendlyName(type2), Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(typeCode1))));
      }
      try
      {
        LoopFor.StepValue = ObjectType.CTypeHelper(StepValue, typeCode1);
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
        throw new ArgumentException(Utils.GetResourceString("ForLoop_ConvertToType3", "Step", Utils.VBFriendlyName(type3), Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(typeCode1))));
      }
      object obj = ObjectType.CTypeHelper((object) 0, typeCode1);
      LoopFor.PositiveStep = ((IComparable) LoopFor.StepValue).CompareTo(obj) >= 0;
      LoopForResult = (object) LoopFor;
      CounterResult = LoopFor.EnumType == null ? LoopFor.Counter : Enum.ToObject(LoopFor.EnumType, LoopFor.Counter);
      return FlowControl.CheckContinueLoop(LoopFor);
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
      FlowControl.ObjectFor LoopFor = (FlowControl.ObjectFor) LoopObj;
      TypeCode typeCode1 = ((IConvertible) Counter).GetTypeCode();
      TypeCode typeCode2 = ((IConvertible) LoopFor.StepValue).GetTypeCode();
      TypeCode typeCode3;
      if (typeCode1 == typeCode2 && typeCode1 != TypeCode.String)
      {
        typeCode3 = typeCode1;
      }
      else
      {
        typeCode3 = ObjectType.GetWidestType((object) typeCode1, typeCode2);
        if (typeCode3 == TypeCode.String)
          typeCode3 = TypeCode.Double;
        TypeCode typeCode4= TypeCode.Empty;
        if (typeCode4 == TypeCode.Object)
          throw new ArgumentException(Utils.GetResourceString("ForLoop_CommonType2", Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(typeCode1)), Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(typeCode2))));
        try
        {
          Counter = ObjectType.CTypeHelper(Counter, typeCode3);
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
          throw new ArgumentException(Utils.GetResourceString("ForLoop_ConvertToType3", "Start", Utils.VBFriendlyName(Counter.GetType()), Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(typeCode3))));
        }
        try
        {
          LoopFor.Limit = ObjectType.CTypeHelper(LoopFor.Limit, typeCode3);
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
          throw new ArgumentException(Utils.GetResourceString("ForLoop_ConvertToType3", "Limit", Utils.VBFriendlyName(LoopFor.Limit.GetType()), Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(typeCode3))));
        }
        try
        {
          LoopFor.StepValue = ObjectType.CTypeHelper(LoopFor.StepValue, typeCode3);
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
          throw new ArgumentException(Utils.GetResourceString("ForLoop_ConvertToType3", "Step", Utils.VBFriendlyName(LoopFor.StepValue.GetType()), Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(typeCode3))));
        }
      }
      LoopFor.Counter = ObjectType.AddObj(Counter, LoopFor.StepValue);
      TypeCode typeCode5 = ((IConvertible) LoopFor.Counter).GetTypeCode();
      CounterResult = LoopFor.EnumType == null ? LoopFor.Counter : Enum.ToObject(LoopFor.EnumType, LoopFor.Counter);
      if (typeCode5 == typeCode3)
        return FlowControl.CheckContinueLoop(LoopFor);
      LoopFor.Limit = ObjectType.CTypeHelper(LoopFor.Limit, typeCode5);
      LoopFor.StepValue = ObjectType.CTypeHelper(LoopFor.StepValue, typeCode5);
      return false;
    }

    /// <summary>Gets the enumerator for an array being iterated over in a <see langword="For Each" /> loop.</summary>
    /// <param name="ary">An array being iterated over in a <see langword="For Each" /> loop.</param>
    /// <returns>The enumerator for <paramref name="ary" />.</returns>
    public static IEnumerator ForEachInArr(Array ary)
    {
      IEnumerator enumerator = ary.GetEnumerator();
      if (enumerator == null)
        throw ExceptionUtils.VbMakeException(92);
      return enumerator;
    }

    /// <summary>Gets the enumerator for an object being iterated over in a <see langword="For Each" /> loop.</summary>
    /// <param name="obj">An object being iterated over in a <see langword="For Each" /> loop.</param>
    /// <returns>The enumerator for <paramref name="obj" />.</returns>
    public static IEnumerator ForEachInObj(object obj)
    {
      if (obj == null)
        throw ExceptionUtils.VbMakeException(91);
      IEnumerable enumerable;
      try
      {
        enumerable = (IEnumerable) obj;
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
        throw ExceptionUtils.MakeException1(100, obj.GetType().ToString());
      }
      IEnumerator enumerator = enumerable.GetEnumerator();
      if (enumerator == null)
        throw ExceptionUtils.MakeException1(100, obj.GetType().ToString());
      return enumerator;
    }

    /// <summary>Sets a reference to the next object in a <see langword="For Each" /> loop.</summary>
    /// <param name="obj">The range variable of the <see langword="For Each" /> loop.</param>
    /// <param name="enumerator">The <see cref="T:System.Collections.IEnumerator" /> of the object being iterated over in the <see langword="For Each" /> loop.</param>
    /// <returns>
    /// <see langword="True" /> if <paramref name="obj" /> refers to the next object; <see langword="False" /> if there are no more objects and <paramref name="obj" /> is <see langword="Nothing" />.</returns>
    public static bool ForEachNextObj(ref object obj, IEnumerator enumerator)
    {
      if (enumerator.MoveNext())
      {
        obj = enumerator.Current;
        return true;
      }
      obj = (object) null;
      return false;
    }

    private static bool CheckContinueLoop(FlowControl.ObjectFor LoopFor)
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

    /// <summary>Checks for a synchronization lock on the specified type.</summary>
    /// <param name="obj">The data type for which to check for synchronization lock.</param>
    public static void CheckForSyncLockOnValueType(object obj)
    {
      if (obj != null && obj.GetType().IsValueType)
        throw new ArgumentException(Utils.GetResourceString("SyncLockRequiresReferenceType1", new string[1]
        {
          Utils.VBFriendlyName(obj.GetType())
        }));
    }

    private sealed class ObjectFor
    {
      public object Counter;
      public object Limit;
      public object StepValue;
      public bool PositiveStep;
      public Type EnumType;
    }
  }
}
