// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Financial
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Threading;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="Financial" /> module contains procedures used to perform financial operations. </summary>
  [StandardModule]
  public sealed class Financial
  {
    private const double cnL_IT_STEP = 1E-05;
    private const double cnL_IT_EPSILON = 1E-07;

    /// <summary>Returns a <see langword="Double" /> specifying the depreciation of an asset for a specific time period using the double-declining balance method or some other method you specify.</summary>
    /// <param name="Cost">Required. <see langword="Double" /> specifying initial cost of the asset.</param>
    /// <param name="Salvage">Required. <see langword="Double" /> specifying value of the asset at the end of its useful life.</param>
    /// <param name="Life">Required. <see langword="Double" /> specifying length of useful life of the asset.</param>
    /// <param name="Period">Required. <see langword="Double" /> specifying period for which asset depreciation is calculated.</param>
    /// <param name="Factor">Optional. <see langword="Double" /> specifying rate at which the balance declines. If omitted, 2 (double-declining method) is assumed.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the depreciation of an asset for a specific time period using the double-declining balance method or some other method you specify.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Factor" /> &lt;= 0, <paramref name="Salvage" /> &lt; 0, <paramref name="Period" /> &lt;= 0, or <paramref name="Period" /> &gt; <paramref name="Life." /></exception>
    public static double DDB(double Cost, double Salvage, double Life, double Period, double Factor = 2.0)
    {
      if (Factor <= 0.0 || Salvage < 0.0 || (Period <= 0.0 || Period > Life))
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Factor)
        }));
      if (Cost <= 0.0)
        return 0.0;
      if (Life < 2.0)
        return Cost - Salvage;
      if (Life == 2.0 && Period > 1.0)
        return 0.0;
      if (Life == 2.0 && Period <= 1.0)
        return Cost - Salvage;
      if (Period <= 1.0)
      {
        double num1 = Cost * Factor / Life;
        double num2 = Cost - Salvage;
        if (num1 > num2)
          return num2;
        return num1;
      }
      double x = (Life - Factor) / Life;
      double y = Period - 1.0;
      double num3 = Factor * Cost / Life * Math.Pow(x, y);
      double num4 = Cost * (1.0 - Math.Pow(x, Period)) - Cost + Salvage;
      if (num4 > 0.0)
        num3 -= num4;
      return num3 < 0.0 ? 0.0 : num3;
    }

    /// <summary>Returns a <see langword="Double" /> specifying the future value of an annuity based on periodic, fixed payments and a fixed interest rate.</summary>
    /// <param name="Rate">Required. <see langword="Double" /> specifying interest rate per period. For example, if you get a car loan at an annual percentage rate (APR) of 10 percent and make monthly payments, the rate per period is 0.1/12, or 0.0083.</param>
    /// <param name="NPer">Required. <see langword="Double" /> specifying total number of payment periods in the annuity. For example, if you make monthly payments on a four-year car loan, your loan has a total of 4 x 12 (or 48) payment periods.</param>
    /// <param name="Pmt">Required. <see langword="Double" /> specifying payment to be made each period. Payments usually contain principal and interest that doesn't change over the life of the annuity.</param>
    /// <param name="PV">Optional. <see langword="Double" /> specifying present value (or lump sum) of a series of future payments. For example, when you borrow money to buy a car, the loan amount is the present value to the lender of the monthly car payments you will make. If omitted, 0 is assumed.</param>
    /// <param name="Due">Optional. Object of type <see cref="T:Ported.VisualBasic.DueDate" /> that specifies when payments are due. This argument must be either <see langword="DueDate.EndOfPeriod" /> if payments are due at the end of the payment period, or <see langword="DueDate.BegOfPeriod" /> if payments are due at the beginning of the period. If omitted, <see langword="DueDate.EndOfPeriod" /> is assumed.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the future value of an annuity based on periodic, fixed payments and a fixed interest rate.</returns>
    public static double FV(double Rate, double NPer, double Pmt, double PV = 0.0, DueDate Due = DueDate.EndOfPeriod)
    {
      return Financial.FV_Internal(Rate, NPer, Pmt, PV, Due);
    }

    private static double FV_Internal(double Rate, double NPer, double Pmt, double PV = 0.0, DueDate Due = DueDate.EndOfPeriod)
    {
      if (Rate == 0.0)
        return -PV - Pmt * NPer;
      double num1 = Due == DueDate.EndOfPeriod ? 1.0 : 1.0 + Rate;
      double num2 = Math.Pow(1.0 + Rate, NPer);
      return -PV * num2 - Pmt / Rate * num1 * (num2 - 1.0);
    }

    /// <summary>Returns a <see langword="Double" /> specifying the interest payment for a given period of an annuity based on periodic, fixed payments and a fixed interest rate.</summary>
    /// <param name="Rate">Required. <see langword="Double" /> specifying interest rate per period. For example, if you get a car loan at an annual percentage rate (APR) of 10 percent and make monthly payments, the rate per period is 0.1/12, or 0.0083.</param>
    /// <param name="Per">Required. <see langword="Double" /> specifying payment period in the range 1 through <paramref name="NPer" />.</param>
    /// <param name="NPer">Required. <see langword="Double" /> specifying total number of payment periods in the annuity. For example, if you make monthly payments on a four-year car loan, your loan has a total of 4 x 12 (or 48) payment periods.</param>
    /// <param name="PV">Required. <see langword="Double" /> specifying present value, or value today, of a series of future payments or receipts. For example, when you borrow money to buy a car, the loan amount is the present value to the lender of the monthly car payments you will make.</param>
    /// <param name="FV">Optional. <see langword="Double" /> specifying future value or cash balance you want after you've made the final payment. For example, the future value of a loan is $0 because that's its value after the final payment. However, if you want to save $50,000 over 18 years for your child's education, then $50,000 is the future value. If omitted, 0 is assumed.</param>
    /// <param name="Due">Optional. Object of type <see cref="T:Ported.VisualBasic.DueDate" /> that specifies when payments are due. This argument must be either DueDate.EndOfPeriod if payments are due at the end of the payment period, or DueDate.BegOfPeriod if payments are due at the beginning of the period. If omitted, DueDate.EndOfPeriod is assumed.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the interest payment for a given period of an annuity based on periodic, fixed payments and a fixed interest rate.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Per" /> &lt;= 0 or <paramref name="Per" /> &gt; <paramref name="NPer" /></exception>
    public static double IPmt(double Rate, double Per, double NPer, double PV, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod)
    {
      double num = Due == DueDate.EndOfPeriod ? 1.0 : 2.0;
      if (Per <= 0.0 || Per >= NPer + 1.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Per)
        }));
      if (Due != DueDate.EndOfPeriod && Per == 1.0)
        return 0.0;
      double Pmt = Financial.PMT_Internal(Rate, NPer, PV, FV, Due);
      if (Due != DueDate.EndOfPeriod)
        PV += Pmt;
      return Financial.FV_Internal(Rate, Per - num, Pmt, PV, DueDate.EndOfPeriod) * Rate;
    }

    /// <summary>Returns a <see langword="Double" /> specifying the internal rate of return for a series of periodic cash flows (payments and receipts).</summary>
    /// <param name="ValueArray">Required. Array of <see langword="Double" /> specifying cash flow values. The array must contain at least one negative value (a payment) and one positive value (a receipt).</param>
    /// <param name="Guess">Optional. Object specifying value you estimate will be returned by <see langword="IRR" />. If omitted, <paramref name="Guess" /> is 0.1 (10 percent).</param>
    /// <returns>Returns a <see langword="Double" /> specifying the internal rate of return for a series of periodic cash flows (payments and receipts).</returns>
    /// <exception cref="T:System.ArgumentException">Array argument values are invalid or <paramref name="Guess" /> &lt;= -1.</exception>
    public static double IRR(ref double[] ValueArray, double Guess = 0.1)
    {
      int upperBound;
      try
      {
        upperBound = ValueArray.GetUpperBound(0);
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
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (ValueArray)
        }));
      }
      int num1 = checked (upperBound + 1);
      if (Guess <= -1.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Guess)
        }));
      if (num1 <= 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (ValueArray)
        }));
      double num2 = ValueArray[0] <= 0.0 ? -ValueArray[0] : ValueArray[0];
      int num3 = 0;
      int num4 = upperBound;
      int index = num3;
      while (index <= num4)
      {
        if (ValueArray[index] > num2)
          num2 = ValueArray[index];
        else if (-ValueArray[index] > num2)
          num2 = -ValueArray[index];
        checked { ++index; }
      }
      double num5 = num2 * 1E-07 * 0.01;
      double Guess1 = Guess;
      double num6 = Financial.OptPV2(ref ValueArray, Guess1);
      double Guess2 = num6 <= 0.0 ? Guess1 - 1E-05 : Guess1 + 1E-05;
      if (Guess2 <= -1.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Rate"
        }));
      double num7 = Financial.OptPV2(ref ValueArray, Guess2);
      int num8 = 0;
      do
      {
        if (num7 == num6)
        {
          if (Guess2 > Guess1)
            Guess1 -= 1E-05;
          else
            Guess1 += 1E-05;
          num6 = Financial.OptPV2(ref ValueArray, Guess1);
          if (num7 == num6)
            throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
        }
        double Guess3 = Guess2 - (Guess2 - Guess1) * num7 / (num7 - num6);
        if (Guess3 <= -1.0)
          Guess3 = (Guess2 - 1.0) * 0.5;
        double num9 = Financial.OptPV2(ref ValueArray, Guess3);
        double num10 = Guess3 <= Guess2 ? Guess2 - Guess3 : Guess3 - Guess2;
        if ((num9 <= 0.0 ? -num9 : num9) < num5 && num10 < 1E-07)
          return Guess3;
        double num11 = num9;
        num6 = num7;
        num7 = num11;
        double num12 = Guess3;
        Guess1 = Guess2;
        Guess2 = num12;
        checked { ++num8; }
      }
      while (num8 <= 39);
      throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
    }

    /// <summary>Returns a <see langword="Double" /> specifying the modified internal rate of return for a series of periodic cash flows (payments and receipts).</summary>
    /// <param name="ValueArray">Required. Array of <see langword="Double" /> specifying cash-flow values. The array must contain at least one negative value (a payment) and one positive value (a receipt).</param>
    /// <param name="FinanceRate">Required. <see langword="Double" /> specifying interest rate paid as the cost of financing.</param>
    /// <param name="ReinvestRate">Required. <see langword="Double" /> specifying interest rate received on gains from cash reinvestment.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the modified internal rate of return for a series of periodic cash flows (payments and receipts).</returns>
    /// <exception cref="T:System.ArgumentException">Rank of <paramref name="ValueArray" /> does not equal 1, <paramref name="FinanceRate" /> = -1, or <paramref name="ReinvestRate" /> = -1</exception>
    /// <exception cref="T:System.DivideByZeroException">Division by zero has occurred.</exception>
    public static double MIRR(ref double[] ValueArray, double FinanceRate, double ReinvestRate)
    {
      if (ValueArray.Rank != 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_RankEQOne1", new string[1]
        {
          nameof (ValueArray)
        }));
      int num1 = 0;
      int num2 = checked (ValueArray.GetUpperBound(0) - num1 + 1);
      if (FinanceRate == -1.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (FinanceRate)
        }));
      if (ReinvestRate == -1.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (ReinvestRate)
        }));
      if (num2 <= 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (ValueArray)
        }));
      double num3 = Financial.LDoNPV(FinanceRate, ref ValueArray, -1);
      if (num3 == 0.0)
        throw new DivideByZeroException(Utils.GetResourceString("Financial_CalcDivByZero"));
      double x = -Financial.LDoNPV(ReinvestRate, ref ValueArray, 1) * Math.Pow(ReinvestRate + 1.0, (double) num2) / (num3 * (FinanceRate + 1.0));
      if (x < 0.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
      double y = 1.0 / ((double) num2 - 1.0);
      return Math.Pow(x, y) - 1.0;
    }

    /// <summary>Returns a <see langword="Double" /> specifying the number of periods for an annuity based on periodic fixed payments and a fixed interest rate.</summary>
    /// <param name="Rate">Required. <see langword="Double" /> specifying interest rate per period. For example, if you get a car loan at an annual percentage rate (APR) of 10 percent and make monthly payments, the rate per period is 0.1/12, or 0.0083.</param>
    /// <param name="Pmt">Required. <see langword="Double" /> specifying payment to be made each period. Payments usually contain principal and interest that does not change over the life of the annuity.</param>
    /// <param name="PV">Required. <see langword="Double" /> specifying present value, or value today, of a series of future payments or receipts. For example, when you borrow money to buy a car, the loan amount is the present value to the lender of the monthly car payments you will make.</param>
    /// <param name="FV">Optional. <see langword="Double" /> specifying future value or cash balance you want after you have made the final payment. For example, the future value of a loan is $0 because that is its value after the final payment. However, if you want to save $50,000 over 18 years for your child's education, then $50,000 is the future value. If omitted, 0 is assumed.</param>
    /// <param name="Due">Optional. Object of type <see cref="T:Ported.VisualBasic.DueDate" /> that specifies when payments are due. This argument must be either DueDate.EndOfPeriod if payments are due at the end of the payment period, or DueDate.BegOfPeriod if payments are due at the beginning of the period. If omitted, DueDate.EndOfPeriod is assumed.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the number of periods for an annuity based on periodic fixed payments and a fixed interest rate.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Rate" /> &lt;= -1.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Rate" /> = 0 and <paramref name="Pmt" /> = 0</exception>
    public static double NPer(double Rate, double Pmt, double PV, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod)
    {
      if (Rate <= -1.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Rate)
        }));
      if (Rate == 0.0)
      {
        if (Pmt == 0.0)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            nameof (Pmt)
          }));
        return -(PV + FV) / Pmt;
      }
      double num = Due == DueDate.EndOfPeriod ? Pmt / Rate : Pmt * (1.0 + Rate) / Rate;
      double d1 = -FV + num;
      double d2 = PV + num;
      if (d1 < 0.0 && d2 < 0.0)
      {
        d1 = -1.0 * d1;
        d2 = -1.0 * d2;
      }
      else if (d1 <= 0.0 || d2 <= 0.0)
        throw new ArgumentException(Utils.GetResourceString("Financial_CannotCalculateNPer"));
      double d3 = Rate + 1.0;
      return (Math.Log(d1) - Math.Log(d2)) / Math.Log(d3);
    }

    /// <summary>Returns a <see langword="Double" /> specifying the net present value of an investment based on a series of periodic cash flows (payments and receipts) and a discount rate.</summary>
    /// <param name="Rate">Required. <see langword="Double" /> specifying discount rate over the length of the period, expressed as a decimal.</param>
    /// <param name="ValueArray">Required. Array of <see langword="Double" /> specifying cash flow values. The array must contain at least one negative value (a payment) and one positive value (a receipt).</param>
    /// <returns>Returns a <see langword="Double" /> specifying the net present value of an investment based on a series of periodic cash flows (payments and receipts) and a discount rate.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ValueArray" /> is <see langword="Nothing" />, rank of <paramref name="ValueArray" /> &lt;&gt; 1, or <paramref name="Rate" /> = -1 </exception>
    public static double NPV(double Rate, ref double[] ValueArray)
    {
      if (ValueArray == null)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (ValueArray)
        }));
      if (ValueArray.Rank != 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_RankEQOne1", new string[1]
        {
          nameof (ValueArray)
        }));
      int num1 = 0;
      int num2 = checked (ValueArray.GetUpperBound(0) - num1 + 1);
      if (Rate == -1.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Rate)
        }));
      if (num2 < 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (ValueArray)
        }));
      return Financial.LDoNPV(Rate, ref ValueArray, 0);
    }

    /// <summary>Returns a <see langword="Double" /> specifying the payment for an annuity based on periodic, fixed payments and a fixed interest rate.</summary>
    /// <param name="Rate">Required. <see langword="Double" /> specifies the interest rate per period. For example, if you get a car loan at an annual percentage rate (APR) of 10 percent and make monthly payments, the rate per period is 0.1/12, or 0.0083.</param>
    /// <param name="NPer">Required. <see langword="Double" /> specifies the total number of payment periods in the annuity. For example, if you make monthly payments on a four-year car loan, your loan has a total of 4 × 12 (or 48) payment periods.</param>
    /// <param name="PV">Required. <see langword="Double" /> specifies the present value (or lump sum) that a series of payments to be paid in the future is worth now. For example, when you borrow money to buy a car, the loan amount is the present value to the lender of the monthly car payments you will make.</param>
    /// <param name="FV">Optional. <see langword="Double" /> specifying future value or cash balance you want after you have made the final payment. For example, the future value of a loan is $0 because that is its value after the final payment. However, if you want to save $50,000 during 18 years for your child's education, then $50,000 is the future value. If omitted, 0 is assumed.</param>
    /// <param name="Due">Optional. Object of type <see cref="T:Ported.VisualBasic.DueDate" /> that specifies when payments are due. This argument must be either DueDate.EndOfPeriod if payments are due at the end of the payment period, or DueDate.BegOfPeriod if payments are due at the beginning of the period. If omitted, DueDate.EndOfPeriod is assumed.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the payment for an annuity based on periodic, fixed payments and a fixed interest rate.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="NPer" /> = 0.</exception>
    public static double Pmt(double Rate, double NPer, double PV, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod)
    {
      return Financial.PMT_Internal(Rate, NPer, PV, FV, Due);
    }

    private static double PMT_Internal(double Rate, double NPer, double PV, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod)
    {
      if (NPer == 0.0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (NPer)
        }));
      if (Rate == 0.0)
        return (-FV - PV) / NPer;
      double num1 = Due == DueDate.EndOfPeriod ? 1.0 : 1.0 + Rate;
      double num2 = Math.Pow(Rate + 1.0, NPer);
      return (-FV - PV * num2) / (num1 * (num2 - 1.0)) * Rate;
    }

    /// <summary>Returns a <see langword="Double" /> specifying the principal payment for a given period of an annuity based on periodic fixed payments and a fixed interest rate.</summary>
    /// <param name="Rate">Required. <see langword="Double" /> specifies the interest rate per period. For example, if you get a car loan at an annual percentage rate (APR) of 10 percent and make monthly payments, the rate per period is 0.1/12, or 0.0083.</param>
    /// <param name="Per">Required. <see langword="Double" /> specifies the payment period in the range 1 through <paramref name="NPer" />.</param>
    /// <param name="NPer">Required. <see langword="Double" /> specifies the total number of payment periods in the annuity. For example, if you make monthly payments on a four-year car loan, your loan has a total of 4 x 12 (or 48) payment periods.</param>
    /// <param name="PV">Required. <see langword="Double" /> specifies the current value of a series of future payments or receipts. For example, when you borrow money to buy a car, the loan amount is the present value to the lender of the monthly car payments you will make.</param>
    /// <param name="FV">Optional. <see langword="Double" /> specifying future value or cash balance you want after you have made the final payment. For example, the future value of a loan is $0 because that is its value after the final payment. However, if you want to save $50,000 over 18 years for your child's education, then $50,000 is the future value. If omitted, 0 is assumed.</param>
    /// <param name="Due">Optional. Object of type <see cref="T:Ported.VisualBasic.DueDate" /> that specifies when payments are due. This argument must be either DueDate.EndOfPeriod if payments are due at the end of the payment period, or DueDate.BegOfPeriod if payments are due at the beginning of the period. If omitted, DueDate.EndOfPeriod is assumed.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the principal payment for a given period of an annuity based on periodic fixed payments and a fixed interest rate.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Per" /> &lt;=0 or <paramref name="Per" /> &gt; <paramref name="NPer" />.</exception>
    public static double PPmt(double Rate, double Per, double NPer, double PV, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod)
    {
      if (Per <= 0.0 || Per >= NPer + 1.0)
        throw new ArgumentException(Utils.GetResourceString("PPMT_PerGT0AndLTNPer", new string[1]
        {
          nameof (Per)
        }));
      return Financial.PMT_Internal(Rate, NPer, PV, FV, Due) - Financial.IPmt(Rate, Per, NPer, PV, FV, Due);
    }

    /// <summary>Returns a <see langword="Double" /> specifying the present value of an annuity based on periodic, fixed payments to be paid in the future and a fixed interest rate.</summary>
    /// <param name="Rate">Required. <see langword="Double" /> specifies the interest rate per period. For example, if you get a car loan at an annual percentage rate (APR) of 10 percent and make monthly payments, the rate per period is 0.1/12, or 0.0083.</param>
    /// <param name="NPer">Required. <see langword="Double" /> specifies the total number of payment periods in the annuity. For example, if you make monthly payments on a four-year car loan, your loan has 4 x 12 (or 48) payment periods.</param>
    /// <param name="Pmt">Required. <see langword="Double" /> specifies the payment to be made each period. Payments usually contain principal and interest that does not change during the life of the annuity.</param>
    /// <param name="FV">Optional. <see langword="Double" /> specifies the future value or cash balance you want after you make the final payment. For example, the future value of a loan is $0 because that is its value after the final payment. However, if you want to save $50,000 over 18 years for your child's education, then $50,000 is the future value. If omitted, 0 is assumed.</param>
    /// <param name="Due">Optional. Object of type <see cref="T:Ported.VisualBasic.DueDate" /> that specifies when payments are due. This argument must be either DueDate.EndOfPeriod if payments are due at the end of the payment period, or DueDate.BegOfPeriod if payments are due at the beginning of the period. If omitted, DueDate.EndOfPeriod is assumed.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the present value of an annuity based on periodic, fixed payments to be paid in the future and a fixed interest rate.</returns>
    public static double PV(double Rate, double NPer, double Pmt, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod)
    {
      if (Rate == 0.0)
        return -FV - Pmt * NPer;
      double num1 = Due == DueDate.EndOfPeriod ? 1.0 : 1.0 + Rate;
      double num2 = Math.Pow(1.0 + Rate, NPer);
      return -(FV + Pmt * num1 * ((num2 - 1.0) / Rate)) / num2;
    }

    /// <summary>Returns a <see langword="Double" /> specifying the interest rate per period for an annuity.</summary>
    /// <param name="NPer">Required. <see langword="Double" /> specifies the total number of payment periods in the annuity. For example, if you make monthly payments on a four-year car loan, your loan has a total of 4 * 12 (or 48) payment periods.</param>
    /// <param name="Pmt">Required. <see langword="Double" /> specifies the payment to be made each period. Payments usually contain principal and interest that doesn't change over the life of the annuity.</param>
    /// <param name="PV">Required. <see langword="Double" /> specifies the present value, or value today, of a series of future payments or receipts. For example, when you borrow money to buy a car, the loan amount is the present value to the lender of the monthly car payments you will make.</param>
    /// <param name="FV">Optional. <see langword="Double" /> specifies the future value or cash balance you want after you make the final payment. For example, the future value of a loan is $0 because that is its value after the final payment. However, if you want to save $50,000 over 18 years for your child's education, then $50,000 is the future value. If omitted, 0 is assumed.</param>
    /// <param name="Due">Optional. Object of type <see cref="T:Ported.VisualBasic.DueDate" /> that specifies when payments are due. This argument must be either DueDate.EndOfPeriod if payments are due at the end of the payment period, or DueDate.BegOfPeriod if payments are due at the beginning of the period. If omitted, DueDate.EndOfPeriod is assumed.</param>
    /// <param name="Guess">Optional. <see langword="Double" /> specifying value you estimate is returned by <see langword="Rate" />. If omitted, <paramref name="Guess" /> is 0.1 (10 percent).</param>
    /// <returns>Returns a <see langword="Double" /> specifying the interest rate per period for an annuity.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="NPer" /> &lt;= 0.</exception>
    public static double Rate(double NPer, double Pmt, double PV, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod, double Guess = 0.1)
    {
      if (NPer <= 0.0)
        throw new ArgumentException(Utils.GetResourceString("Rate_NPerMustBeGTZero"));
      double Rate1 = Guess;
      double num1 = Financial.LEvalRate(Rate1, NPer, Pmt, PV, FV, Due);
      double Rate2 = num1 <= 0.0 ? Rate1 * 2.0 : Rate1 / 2.0;
      double num2 = Financial.LEvalRate(Rate2, NPer, Pmt, PV, FV, Due);
      int num3 = 0;
      do
      {
        if (num2 == num1)
        {
          if (Rate2 > Rate1)
            Rate1 -= 1E-05;
          else
            Rate1 -= -1E-05;
          num1 = Financial.LEvalRate(Rate1, NPer, Pmt, PV, FV, Due);
          if (num2 == num1)
            throw new ArgumentException(Utils.GetResourceString("Financial_CalcDivByZero"));
        }
        double Rate3 = Rate2 - (Rate2 - Rate1) * num2 / (num2 - num1);
        double num4 = Financial.LEvalRate(Rate3, NPer, Pmt, PV, FV, Due);
        if (Math.Abs(num4) < 1E-07)
          return Rate3;
        double num5 = num4;
        num1 = num2;
        num2 = num5;
        double num6 = Rate3;
        Rate1 = Rate2;
        Rate2 = num6;
        checked { ++num3; }
      }
      while (num3 <= 39);
      throw new ArgumentException(Utils.GetResourceString("Financial_CannotCalculateRate"));
    }

    /// <summary>Returns a <see langword="Double" /> specifying the straight-line depreciation of an asset for a single period.</summary>
    /// <param name="Cost">Required. <see langword="Double" /> specifying initial cost of the asset.</param>
    /// <param name="Salvage">Required. <see langword="Double" /> specifying value of the asset at the end of its useful life.</param>
    /// <param name="Life">Required. <see langword="Double" /> specifying length of the useful life of the asset.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the straight-line depreciation of an asset for a single period.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Life" /> = 0.</exception>
    public static double SLN(double Cost, double Salvage, double Life)
    {
      if (Life == 0.0)
        throw new ArgumentException(Utils.GetResourceString("Financial_LifeNEZero"));
      return (Cost - Salvage) / Life;
    }

    /// <summary>Returns a <see langword="Double" /> specifying the sum-of-years digits depreciation of an asset for a specified period.</summary>
    /// <param name="Cost">Required. <see langword="Double" /> specifying the initial cost of the asset.</param>
    /// <param name="Salvage">Required. <see langword="Double" /> specifying the value of the asset at the end of its useful life.</param>
    /// <param name="Life">Required. <see langword="Double" /> specifying the length of the useful life of the asset.</param>
    /// <param name="Period">Required. <see langword="Double" /> specifying the period for which asset depreciation is calculated.</param>
    /// <returns>Returns a <see langword="Double" /> specifying the sum-of-years digits depreciation of an asset for a specified period.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Salvage" /> &lt; 0, <paramref name="Period" /> &gt; <paramref name="Life" />, or <paramref name="Period" /> &lt;=0.</exception>
    public static double SYD(double Cost, double Salvage, double Life, double Period)
    {
      if (Salvage < 0.0)
        throw new ArgumentException(Utils.GetResourceString("Financial_ArgGEZero1", new string[1]
        {
          nameof (Salvage)
        }));
      if (Period > Life)
        throw new ArgumentException(Utils.GetResourceString("Financial_PeriodLELife"));
      if (Period <= 0.0)
        throw new ArgumentException(Utils.GetResourceString("Financial_ArgGTZero1", new string[1]
        {
          nameof (Period)
        }));
      return (Cost - Salvage) / (Life * (Life + 1.0)) * (Life + 1.0 - Period) * 2.0;
    }

    private static double LEvalRate(double Rate, double NPer, double Pmt, double PV, double dFv, DueDate Due)
    {
      if (Rate == 0.0)
        return PV + Pmt * NPer + dFv;
      double num1 = Math.Pow(Rate + 1.0, NPer);
      double num2 = Due == DueDate.EndOfPeriod ? 1.0 : 1.0 + Rate;
      return PV * num1 + Pmt * num2 * (num1 - 1.0) / Rate + dFv;
    }

    private static double LDoNPV(double Rate, ref double[] ValueArray, int iWNType)
    {
      bool flag1 = iWNType < 0;
      bool flag2 = iWNType > 0;
      double num1 = 1.0;
      double num2 = 0.0;
      int num3 = 0;
      int upperBound = ValueArray.GetUpperBound(0);
      int num4 = num3;
      int num5 = upperBound;
      int index = num4;
      while (index <= num5)
      {
        double num6 = ValueArray[index];
        num1 += num1 * Rate;
        if ((!flag1 || num6 <= 0.0) && (!flag2 || num6 >= 0.0))
          num2 += num6 / num1;
        checked { ++index; }
      }
      return num2;
    }

    private static double OptPV2(ref double[] ValueArray, double Guess = 0.1)
    {
      int index1 = 0;
      int upperBound = ValueArray.GetUpperBound(0);
      double num1 = 0.0;
      double num2 = 1.0 + Guess;
      while (index1 <= upperBound && ValueArray[index1] == 0.0)
        checked { ++index1; }
      int num3 = upperBound;
      int num4 = index1;
      int index2 = num3;
      while (index2 >= num4)
      {
        num1 = num1 / num2 + ValueArray[index2];
        checked { index2 += -1; }
      }
      return num1;
    }
  }
}
