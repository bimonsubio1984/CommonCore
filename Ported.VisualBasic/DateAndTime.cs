// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.DateAndTime
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="DateAndTime" /> module contains the procedures and properties used in date and time operations. </summary>
  [StandardModule]
  public sealed class DateAndTime
  {
    private static string[] AcceptedDateFormatsDBCS = new string[4]
    {
      "yyyy-M-d",
      "y-M-d",
      "yyyy/M/d",
      "y/M/d"
    };
    private static string[] AcceptedDateFormatsSBCS = new string[4]
    {
      "M-d-yyyy",
      "M-d-y",
      "M/d/yyyy",
      "M/d/y"
    };

    /// <summary>Returns or sets a <see langword="Date" /> value containing the current date according to your system.</summary>
    /// <returns>Returns or sets a <see langword="Date" /> value containing the current date according to your system.</returns>
    public static DateTime Today
    {
      get
      {
        return DateTime.Today;
      }
      //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)] 
      set
      {
        Utils.SetDate(value);
      }
    }

    /// <summary>Returns a <see langword="Date" /> value containing the current date and time according to your system.</summary>
    /// <returns>Returns a <see langword="Date" /> value containing the current date and time according to your system.</returns>
    public static DateTime Now
    {
      get
      {
        return DateTime.Now;
      }
    }

    /// <summary>Returns or sets a <see langword="Date" /> value containing the current time of day according to your system.</summary>
    /// <returns>Returns or sets a <see langword="Date" /> value containing the current time of day according to your system.</returns>
    public static DateTime TimeOfDay
    {
      get
      {
        DateTime dateTime = DateTime.Now;
        long ticks = dateTime.TimeOfDay.Ticks;
        dateTime = new DateTime(checked (ticks - unchecked (ticks % 10000000L)));
        return dateTime;
      }
      //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)] 
      set
      {
        Utils.SetTime(value);
      }
    }

    /// <summary>Returns or sets a <see langword="String" /> value representing the current time of day according to your system.</summary>
    /// <returns>Returns or sets a <see langword="String" /> value representing the current time of day according to your system.</returns>
    /// <exception cref="T:System.InvalidCastException">Invalid format used to set the value of <see langword="TimeString" />.</exception>
    public static string TimeString
    {
      get
      {
        return new DateTime(DateTime.Now.TimeOfDay.Ticks).ToString("HH:mm:ss", (IFormatProvider) Utils.GetInvariantCultureInfo());
      }
      //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)] 
      set
      {
        DateTime dtTime;
        try
        {
          dtTime = DateType.FromString(value, Utils.GetInvariantCultureInfo());
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
          throw ExceptionUtils.VbMakeException((Exception) new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(value, 32), "Date")), 5);
        }
        Utils.SetTime(dtTime);
      }
    }

    private static bool IsDBCSCulture()
    {
      return Marshal.SystemMaxDBCSCharSize != 1;
    }

    /// <summary>Returns or sets a <see langword="String" /> value representing the current date according to your system.</summary>
    /// <returns>Returns or sets a <see langword="String" /> value representing the current date according to your system.</returns>
    /// <exception cref="T:System.InvalidCastException">Invalid format used to set the value of <see langword="DateString" />.</exception>
    public static string DateString
    {
      get
      {
        if (DateAndTime.IsDBCSCulture())
          return DateTime.Today.ToString("yyyy\\-MM\\-dd", (IFormatProvider) Utils.GetInvariantCultureInfo());
        return DateTime.Today.ToString("MM\\-dd\\-yyyy", (IFormatProvider) Utils.GetInvariantCultureInfo());
      }
      //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)] 
      set
      {
        DateTime vDate;
        try
        {
          string halfwidthNumbers = Utils.ToHalfwidthNumbers(value, Utils.GetCultureInfo());
          vDate = !DateAndTime.IsDBCSCulture() ? DateTime.ParseExact(halfwidthNumbers, DateAndTime.AcceptedDateFormatsSBCS, (IFormatProvider) Utils.GetInvariantCultureInfo(), DateTimeStyles.AllowWhiteSpaces) : DateTime.ParseExact(halfwidthNumbers, DateAndTime.AcceptedDateFormatsDBCS, (IFormatProvider) Utils.GetInvariantCultureInfo(), DateTimeStyles.AllowWhiteSpaces);
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
          throw ExceptionUtils.VbMakeException((Exception) new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(value, 32), "Date")), 5);
        }
        Utils.SetDate(vDate);
      }
    }

    /// <summary>Returns a <see langword="Double" /> value representing the number of seconds elapsed since midnight.</summary>
    /// <returns>Returns a <see langword="Double" /> value representing the number of seconds elapsed since midnight.</returns>
    public static double Timer
    {
      get
      {
        return (double) (DateTime.Now.Ticks % 864000000000L) / 10000000.0;
      }
    }

    private static Calendar CurrentCalendar
    {
      get
      {
        return Thread.CurrentThread.CurrentCulture.Calendar;
      }
    }

    /// <summary>Returns a <see langword="Date" /> value containing a date and time value to which a specified time interval has been added.</summary>
    /// <param name="Interval">Required. <see langword="DateInterval" /> enumeration value or <see langword="String" /> expression representing the time interval you want to add.</param>
    /// <param name="Number">Required. <see langword="Double" />. Floating-point expression representing the number of intervals you want to add. <paramref name="Number" /> can be positive (to get date/time values in the future) or negative (to get date/time values in the past). It can contain a fractional part when <paramref name="Interval" /> specifies hours, minutes, or seconds. For other values of <paramref name="Interval" />, any fractional part of <paramref name="Number" /> is ignored.</param>
    /// <param name="DateValue">Required. <see langword="Date" />. An expression representing the date and time to which the interval is to be added. <paramref name="DateValue" /> itself is not changed in the calling program.</param>
    /// <returns>Returns a <see langword="Date" /> value containing a date and time value to which a specified time interval has been added.</returns>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="DateValue" /> is not coercible to <see langword="Date" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Interval" /> is not valid.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Calculated date is before 00:00:00 on January 1 of the year 1, or later than 23:59:59 on December 31, 9999.</exception>
    public static DateTime DateAdd(DateInterval Interval, double Number, DateTime DateValue)
    {
      int num = checked ((int) Math.Round(Conversion.Fix(Number)));
      switch (Interval)
      {
        case DateInterval.Year:
          return DateAndTime.CurrentCalendar.AddYears(DateValue, num);
        case DateInterval.Quarter:
          return DateValue.AddMonths(checked (num * 3));
        case DateInterval.Month:
          return DateAndTime.CurrentCalendar.AddMonths(DateValue, num);
        case DateInterval.DayOfYear:
        case DateInterval.Day:
        case DateInterval.Weekday:
          return DateValue.AddDays((double) num);
        case DateInterval.WeekOfYear:
          return DateValue.AddDays((double) num * 7.0);
        case DateInterval.Hour:
          return DateValue.AddHours((double) num);
        case DateInterval.Minute:
          return DateValue.AddMinutes((double) num);
        case DateInterval.Second:
          return DateValue.AddSeconds((double) num);
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            nameof (Interval)
          }));
      }
    }

    /// <summary>Returns a <see langword="Long" /> value specifying the number of time intervals between two <see langword="Date" /> values.</summary>
    /// <param name="Interval">Required. <see langword="DateInterval" /> enumeration value or <see langword="String" /> expression representing the time interval you want to use as the unit of difference between <paramref name="Date1" /> and <paramref name="Date2" />.</param>
    /// <param name="Date1">Required. <see langword="Date" />. The first date/time value you want to use in the calculation. </param>
    /// <param name="Date2">Required. <see langword="Date" />. The second date/time value you want to use in the calculation.</param>
    /// <param name="DayOfWeek">Optional. A value chosen from the <see langword="FirstDayOfWeek" /> enumeration that specifies the first day of the week. If not specified, <see langword="FirstDayOfWeek.Sunday" /> is used.</param>
    /// <param name="WeekOfYear">Optional. A value chosen from the <see langword="FirstWeekOfYear" /> enumeration that specifies the first week of the year. If not specified, <see langword="FirstWeekOfYear.Jan1" /> is used.</param>
    /// <returns>Returns a <see langword="Long" /> value specifying the number of time intervals between two <see langword="Date" /> values.</returns>
    /// <exception cref="T:System.ArgumentException">Invalid <paramref name="Interval" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Date1" />, <paramref name="Date2" />, or <paramref name="DayofWeek" /> is out of range.</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="Date1" /> or <paramref name="Date2" /> is of an invalid type.</exception>
    public static long DateDiff(DateInterval Interval, DateTime Date1, DateTime Date2, FirstDayOfWeek DayOfWeek = FirstDayOfWeek.Sunday, FirstWeekOfYear WeekOfYear = FirstWeekOfYear.Jan1)
    {
      TimeSpan timeSpan = Date2.Subtract(Date1);
      switch (Interval)
      {
        case DateInterval.Year:
          Calendar currentCalendar1 = DateAndTime.CurrentCalendar;
          return (long) checked (currentCalendar1.GetYear(Date2) - currentCalendar1.GetYear(Date1));
        case DateInterval.Quarter:
          Calendar currentCalendar2 = DateAndTime.CurrentCalendar;
          return (long) checked ((currentCalendar2.GetYear(Date2) - currentCalendar2.GetYear(Date1)) * 4 + unchecked (checked (currentCalendar2.GetMonth(Date2) - 1) / 3) - unchecked (checked (currentCalendar2.GetMonth(Date1) - 1) / 3));
        case DateInterval.Month:
          Calendar currentCalendar3 = DateAndTime.CurrentCalendar;
          return (long) checked ((currentCalendar3.GetYear(Date2) - currentCalendar3.GetYear(Date1)) * 12 + currentCalendar3.GetMonth(Date2) - currentCalendar3.GetMonth(Date1));
        case DateInterval.DayOfYear:
        case DateInterval.Day:
          return checked ((long) Math.Round(Conversion.Fix(timeSpan.TotalDays)));
        case DateInterval.WeekOfYear:
          Date1 = Date1.AddDays((double) checked (-DateAndTime.GetDayOfWeek(Date1, DayOfWeek)));
          Date2 = Date2.AddDays((double) checked (-DateAndTime.GetDayOfWeek(Date2, DayOfWeek)));
          return checked ((long) Math.Round(Conversion.Fix(Date2.Subtract(Date1).TotalDays))) / 7L;
        case DateInterval.Weekday:
          return checked ((long) Math.Round(Conversion.Fix(timeSpan.TotalDays))) / 7L;
        case DateInterval.Hour:
          return checked ((long) Math.Round(Conversion.Fix(timeSpan.TotalHours)));
        case DateInterval.Minute:
          return checked ((long) Math.Round(Conversion.Fix(timeSpan.TotalMinutes)));
        case DateInterval.Second:
          return checked ((long) Math.Round(Conversion.Fix(timeSpan.TotalSeconds)));
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            nameof (Interval)
          }));
      }
    }

    private static int GetDayOfWeek(DateTime dt, FirstDayOfWeek weekdayFirst)
    {
      if (weekdayFirst < FirstDayOfWeek.System || weekdayFirst > FirstDayOfWeek.Saturday)
        throw ExceptionUtils.VbMakeException(5);
      if (weekdayFirst == FirstDayOfWeek.System)
        weekdayFirst = (FirstDayOfWeek) (Utils.GetDateTimeFormatInfo().FirstDayOfWeek + 1);
      return checked (unchecked (checked (unchecked ((int) dt.DayOfWeek) - unchecked ((int) weekdayFirst) + 8) % 7) + 1);
    }

    /// <summary>Returns an <see langword="Integer" /> value containing the specified component of a given <see langword="Date" /> value.</summary>
    /// <param name="Interval">Required. <see langword="DateInterval" /> enumeration value or <see langword="String" /> expression representing the part of the date/time value you want to return.</param>
    /// <param name="DateValue">Required. <see langword="Date" /> value that you want to evaluate.</param>
    /// <param name="FirstDayOfWeekValue">Optional. A value chosen from the <see langword="FirstDayOfWeek" /> enumeration that specifies the first day of the week. If not specified, <see langword="FirstDayOfWeek.Sunday" /> is used.</param>
    /// <param name="FirstWeekOfYearValue">Optional. A value chosen from the <see langword="FirstWeekOfYear" /> enumeration that specifies the first week of the year. If not specified, <see langword="FirstWeekOfYear.Jan1" /> is used.</param>
    /// <returns>Returns an <see langword="Integer" /> value containing the specified component of a given <see langword="Date" /> value.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Interval" /> is not valid. </exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="DateValue" /> is not coercible to <see langword="Date" />.</exception>
    public static int DatePart(DateInterval Interval, DateTime DateValue, FirstDayOfWeek FirstDayOfWeekValue = FirstDayOfWeek.Sunday, FirstWeekOfYear FirstWeekOfYearValue = FirstWeekOfYear.Jan1)
    {
      switch (Interval)
      {
        case DateInterval.Year:
          return DateAndTime.CurrentCalendar.GetYear(DateValue);
        case DateInterval.Quarter:
          return checked (unchecked (checked (DateValue.Month - 1) / 3) + 1);
        case DateInterval.Month:
          return DateAndTime.CurrentCalendar.GetMonth(DateValue);
        case DateInterval.DayOfYear:
          return DateAndTime.CurrentCalendar.GetDayOfYear(DateValue);
        case DateInterval.Day:
          return DateAndTime.CurrentCalendar.GetDayOfMonth(DateValue);
        case DateInterval.WeekOfYear:
          DayOfWeek firstDayOfWeek = FirstDayOfWeekValue != FirstDayOfWeek.System ? (DayOfWeek) (FirstDayOfWeekValue - 1) : Utils.GetCultureInfo().DateTimeFormat.FirstDayOfWeek;
          CalendarWeekRule rule= CalendarWeekRule.FirstDay;
          switch (FirstWeekOfYearValue)
          {
            case FirstWeekOfYear.System:
              rule = Utils.GetCultureInfo().DateTimeFormat.CalendarWeekRule;
              break;
            case FirstWeekOfYear.Jan1:
              rule = CalendarWeekRule.FirstDay;
              break;
            case FirstWeekOfYear.FirstFourDays:
              rule = CalendarWeekRule.FirstFourDayWeek;
              break;
            case FirstWeekOfYear.FirstFullWeek:
              rule = CalendarWeekRule.FirstFullWeek;
              break;
          }
          return DateAndTime.CurrentCalendar.GetWeekOfYear(DateValue, rule, firstDayOfWeek);
        case DateInterval.Weekday:
          return DateAndTime.Weekday(DateValue, FirstDayOfWeekValue);
        case DateInterval.Hour:
          return DateAndTime.CurrentCalendar.GetHour(DateValue);
        case DateInterval.Minute:
          return DateAndTime.CurrentCalendar.GetMinute(DateValue);
        case DateInterval.Second:
          return DateAndTime.CurrentCalendar.GetSecond(DateValue);
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            nameof (Interval)
          }));
      }
    }

    /// <summary>Returns a <see langword="Date" /> value containing a date and time value to which a specified time interval has been added.</summary>
    /// <param name="Interval">Required. <see langword="DateInterval" /> enumeration value or <see langword="String" /> expression representing the time interval you want to add.</param>
    /// <param name="Number">Required. <see langword="Double" />. Floating-point expression representing the number of intervals you want to add. <paramref name="Number" /> can be positive (to get date/time values in the future) or negative (to get date/time values in the past). It can contain a fractional part when <paramref name="Interval" /> specifies hours, minutes, or seconds. For other values of <paramref name="Interval" />, any fractional part of <paramref name="Number" /> is ignored.</param>
    /// <param name="DateValue">Required. <see langword="Date" />. An expression representing the date and time to which the interval is to be added. <paramref name="DateValue" /> itself is not changed in the calling program.</param>
    /// <returns>Returns a <see langword="Date" /> value containing a date and time value to which a specified time interval has been added.</returns>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="DateValue" /> is not coercible to <see langword="Date" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Interval" /> is not valid.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Calculated date is before 00:00:00 on January 1 of the year 1, or later than 23:59:59 on December 31, 9999.</exception>
    public static DateTime DateAdd(string Interval, double Number, object DateValue)
    {
      DateTime date;
      try
      {
        date = Conversions.ToDate(DateValue);
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
        throw new InvalidCastException(Utils.GetResourceString("Argument_InvalidDateValue1", new string[1]
        {
          nameof (DateValue)
        }));
      }
      return DateAndTime.DateAdd(DateAndTime.DateIntervalFromString(Interval), Number, date);
    }

    /// <summary>Returns a <see langword="Long" /> value specifying the number of time intervals between two <see langword="Date" /> values.</summary>
    /// <param name="Interval">Required. <see langword="DateInterval" /> enumeration value or <see langword="String" /> expression representing the time interval you want to use as the unit of difference between <paramref name="Date1" /> and <paramref name="Date2" />.</param>
    /// <param name="Date1">Required. <see langword="Date" />. The first date/time value you want to use in the calculation. </param>
    /// <param name="Date2">Required. <see langword="Date" />. The second date/time value you want to use in the calculation.</param>
    /// <param name="DayOfWeek">Optional. A value chosen from the <see langword="FirstDayOfWeek" /> enumeration that specifies the first day of the week. If not specified, <see langword="FirstDayOfWeek.Sunday" /> is used.</param>
    /// <param name="WeekOfYear">Optional. A value chosen from the <see langword="FirstWeekOfYear" /> enumeration that specifies the first week of the year. If not specified, <see langword="FirstWeekOfYear.Jan1" /> is used.</param>
    /// <returns>Returns a <see langword="Long" /> value specifying the number of time intervals between two <see langword="Date" /> values.</returns>
    /// <exception cref="T:System.ArgumentException">Invalid <paramref name="Interval" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Date1" />, <paramref name="Date2" />, or <paramref name="DayofWeek" /> is out of range.</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="Date1" /> or <paramref name="Date2" /> is of an invalid type.</exception>
    public static long DateDiff(string Interval, object Date1, object Date2, FirstDayOfWeek DayOfWeek = FirstDayOfWeek.Sunday, FirstWeekOfYear WeekOfYear = FirstWeekOfYear.Jan1)
    {
      DateTime date1;
      try
      {
        date1 = Conversions.ToDate(Date1);
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
        throw new InvalidCastException(Utils.GetResourceString("Argument_InvalidDateValue1", new string[1]
        {
          nameof (Date1)
        }));
      }
      DateTime date2;
      try
      {
        date2 = Conversions.ToDate(Date2);
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
        throw new InvalidCastException(Utils.GetResourceString("Argument_InvalidDateValue1", new string[1]
        {
          nameof (Date2)
        }));
      }
      return DateAndTime.DateDiff(DateAndTime.DateIntervalFromString(Interval), date1, date2, DayOfWeek, WeekOfYear);
    }

    /// <summary>Returns an <see langword="Integer" /> value containing the specified component of a given <see langword="Date" /> value.</summary>
    /// <param name="Interval">Required. <see langword="DateInterval" /> enumeration value or <see langword="String" /> expression representing the part of the date/time value you want to return.</param>
    /// <param name="DateValue">Required. <see langword="Date" /> value that you want to evaluate.</param>
    /// <param name="DayOfWeek">Optional. A value chosen from the <see langword="FirstDayOfWeek" /> enumeration that specifies the first day of the week. If not specified, <see langword="FirstDayOfWeek.Sunday" /> is used.</param>
    /// <param name="WeekOfYear">Optional. A value chosen from the <see langword="FirstWeekOfYear" /> enumeration that specifies the first week of the year. If not specified, <see langword="FirstWeekOfYear.Jan1" /> is used.</param>
    /// <returns>Returns an <see langword="Integer" /> value containing the specified component of a given <see langword="Date" /> value.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Interval" /> is invalid. </exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="DateValue" /> is not coercible to <see langword="Date" />.</exception>
    public static int DatePart(string Interval, object DateValue, FirstDayOfWeek DayOfWeek = FirstDayOfWeek.Sunday, FirstWeekOfYear WeekOfYear = FirstWeekOfYear.Jan1)
    {
      DateTime date;
      try
      {
        date = Conversions.ToDate(DateValue);
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
        throw new InvalidCastException(Utils.GetResourceString("Argument_InvalidDateValue1", new string[1]
        {
          nameof (DateValue)
        }));
      }
      return DateAndTime.DatePart(DateAndTime.DateIntervalFromString(Interval), date, DayOfWeek, WeekOfYear);
    }

    private static DateInterval DateIntervalFromString(string Interval)
    {
      if (Interval != null)
        Interval = Interval.ToUpperInvariant();
      string Left = Interval;
      if (Operators.CompareString(Left, "YYYY", false) == 0)
        return DateInterval.Year;
      if (Operators.CompareString(Left, "Y", false) == 0)
        return DateInterval.DayOfYear;
      if (Operators.CompareString(Left, "M", false) == 0)
        return DateInterval.Month;
      if (Operators.CompareString(Left, "D", false) == 0)
        return DateInterval.Day;
      if (Operators.CompareString(Left, "H", false) == 0)
        return DateInterval.Hour;
      if (Operators.CompareString(Left, "N", false) == 0)
        return DateInterval.Minute;
      if (Operators.CompareString(Left, "S", false) == 0)
        return DateInterval.Second;
      if (Operators.CompareString(Left, "WW", false) == 0)
        return DateInterval.WeekOfYear;
      if (Operators.CompareString(Left, "W", false) == 0)
        return DateInterval.Weekday;
      if (Operators.CompareString(Left, "Q", false) == 0)
        return DateInterval.Quarter;
      throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
      {
        nameof (Interval)
      }));
    }

    /// <summary>Returns a <see langword="Date" /> value representing a specified year, month, and day, with the time information set to midnight (00:00:00).</summary>
    /// <param name="Year">Required. <see langword="Integer" /> expression from 1 through 9999. However, values below this range are also accepted. If <paramref name="Year" /> is 0 through 99, it is interpreted as being between 1930 and 2029, as explained in the "Remarks" section below. If <paramref name="Year" /> is less than 1, it is subtracted from the current year.</param>
    /// <param name="Month">Required. <see langword="Integer" /> expression from 1 through 12. However, values outside this range are also accepted. The value of <paramref name="Month" /> is offset by 1 and applied to January of the calculated year. In other words, (<paramref name="Month" /> - 1) is added to January. The year is recalculated if necessary. The following results illustrate this effect:If <paramref name="Month" /> is 1, the result is January of the calculated year.If <paramref name="Month" /> is 0, the result is December of the previous year.If <paramref name="Month" /> is -1, the result is November of the previous year.If <paramref name="Month" /> is 13, the result is January of the following year.</param>
    /// <param name="Day">Required. <see langword="Integer" /> expression from 1 through 31. However, values outside this range are also accepted. The value of <paramref name="Day" /> is offset by 1 and applied to the first day of the calculated month. In other words, (<paramref name="Day" /> - 1) is added to the first of the month. The month and year are recalculated if necessary. The following results illustrate this effect:If <paramref name="Day" /> is 1, the result is the first day of the calculated month.If <paramref name="Day" /> is 0, the result is the last day of the previous month.If <paramref name="Day" /> is -1, the result is the penultimate day of the previous month.If <paramref name="Day" /> is past the end of the current month, the result is the appropriate day of the following month. For example, if <paramref name="Month" /> is 4 and <paramref name="Day" /> is 31, the result is May 1.</param>
    /// <returns>Returns a <see langword="Date" /> value representing a specified year, month, and day, with the time information set to midnight (00:00:00).</returns>
    public static DateTime DateSerial(int Year, int Month, int Day)
    {
      Calendar currentCalendar = DateAndTime.CurrentCalendar;
      if (Year < 0)
        Year = checked (currentCalendar.GetYear(DateTime.Today) + Year);
      else if (Year < 100)
        Year = currentCalendar.ToFourDigitYear(Year);
      if (currentCalendar is GregorianCalendar)
      {
        if (Month >= 1)
        {
          if (Month <= 12)
          {
            if (Day >= 1)
            {
              if (Day <= 28)
                return new DateTime(Year, Month, Day);
            }
          }
        }
      }
      DateTime dateTime;
      try
      {
        dateTime = currentCalendar.ToDateTime(Year, 1, 1, 0, 0, 0, 0);
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
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Year)
        })), 5);
      }
      DateTime time;
      try
      {
        time = currentCalendar.AddMonths(dateTime, checked (Month - 1));
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
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Month)
        })), 5);
      }
      try
      {
        return currentCalendar.AddDays(time, checked (Day - 1));
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
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Day)
        })), 5);
      }
    }

    /// <summary>Returns a <see langword="Date" /> value representing a specified hour, minute, and second, with the date information set relative to January 1 of the year 1.</summary>
    /// <param name="Hour">Required. <see langword="Integer" /> expression from 0 through 23. However, values outside this range are also accepted.</param>
    /// <param name="Minute">Required. <see langword="Integer" /> expression from 0 through 59. However, values outside this range are also accepted. The value of <paramref name="Minute" /> is added to the calculated hour, so a negative value specifies minutes before that hour.</param>
    /// <param name="Second">Required. <see langword="Integer" /> expression from 0 through 59. However, values outside this range are also accepted. The value of <paramref name="Second" /> is added to the calculated minute, so a negative value specifies seconds before that minute.</param>
    /// <returns>Returns a <see langword="Date" /> value representing a specified hour, minute, and second, with the date information set relative to January 1 of the year 1.</returns>
    /// <exception cref="T:System.ArgumentException">An argument is outside the range -2,147,483,648 through 2,147,483,647</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Calculated time is less than negative 24 hours.</exception>
    public static DateTime TimeSerial(int Hour, int Minute, int Second)
    {
      int num = checked (Hour * 60 * 60 + Minute * 60 + Second);
      if (num < 0)
        checked { num += 86400; }
      return new DateTime(checked ((long) num * 10000000L));
    }

    /// <summary>Returns a <see langword="Date" /> value containing the date information represented by a string, with the time information set to midnight (00:00:00).</summary>
    /// <param name="StringDate">Required. <see langword="String" /> expression representing a date/time value from 00:00:00 on January 1 of the year 1 through 23:59:59 on December 31, 9999.</param>
    /// <returns>
    /// <see langword="Date" /> value containing the date information represented by a string, with the time information set to midnight (00:00:00).</returns>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="StringDate" /> includes invalid time information.</exception>
    public static DateTime DateValue(string StringDate)
    {
      return Conversions.ToDate(StringDate).Date;
    }

    /// <summary>Returns a <see langword="Date" /> value containing the time information represented by a string, with the date information set to January 1 of the year 1.</summary>
    /// <param name="StringTime">Required. <see langword="String" /> expression representing a date/time value from 00:00:00 on January 1 of the year 1 through 23:59:59 on December 31, 9999.</param>
    /// <returns>Returns a <see langword="Date" /> value containing the time information represented by a string, with the date information set to January 1 of the year 1.</returns>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="StringTime" /> includes invalid date information.</exception>
    public static DateTime TimeValue(string StringTime)
    {
      return new DateTime(Conversions.ToDate(StringTime).Ticks % 864000000000L);
    }

    /// <summary>Returns an <see langword="Integer" /> value from 1 through 9999 representing the year.</summary>
    /// <param name="DateValue">Required. <see langword="Date" /> value from which you want to extract the year.</param>
    /// <returns>Returns an <see langword="Integer" /> value from 1 through 9999 representing the year.</returns>
    public static int Year(DateTime DateValue)
    {
      return DateAndTime.CurrentCalendar.GetYear(DateValue);
    }

    /// <summary>Returns an <see langword="Integer" /> value from 1 through 12 representing the month of the year.</summary>
    /// <param name="DateValue">Required. <see langword="Date" /> value from which you want to extract the month.</param>
    /// <returns>Returns an <see langword="Integer" /> value from 1 through 12 representing the month of the year.</returns>
    public static int Month(DateTime DateValue)
    {
      return DateAndTime.CurrentCalendar.GetMonth(DateValue);
    }

    /// <summary>Returns an <see langword="Integer" /> value from 1 through 31 representing the day of the month.</summary>
    /// <param name="DateValue">Required. <see langword="Date" /> value from which you want to extract the day.</param>
    /// <returns>Returns an <see langword="Integer" /> value from 1 through 31 representing the day of the month.</returns>
    public static int Day(DateTime DateValue)
    {
      return DateAndTime.CurrentCalendar.GetDayOfMonth(DateValue);
    }

    /// <summary>Returns an <see langword="Integer" /> value from 0 through 23 representing the hour of the day.</summary>
    /// <param name="TimeValue">Required. <see langword="Date" /> value from which you want to extract the hour.</param>
    /// <returns>Returns an <see langword="Integer" /> value from 0 through 23 representing the hour of the day.</returns>
    public static int Hour(DateTime TimeValue)
    {
      return DateAndTime.CurrentCalendar.GetHour(TimeValue);
    }

    /// <summary>Returns an <see langword="Integer" /> value from 0 through 59 representing the minute of the hour.</summary>
    /// <param name="TimeValue">Required. <see langword="Date" /> value from which you want to extract the minute.</param>
    /// <returns>Returns an <see langword="Integer" /> value from 0 through 59 representing the minute of the hour.</returns>
    public static int Minute(DateTime TimeValue)
    {
      return DateAndTime.CurrentCalendar.GetMinute(TimeValue);
    }

    /// <summary>Returns an <see langword="Integer" /> value from 0 through 59 representing the second of the minute.</summary>
    /// <param name="TimeValue">Required. <see langword="Date" /> value from which you want to extract the second.</param>
    /// <returns>Returns an <see langword="Integer" /> value from 0 through 59 representing the second of the minute.</returns>
    public static int Second(DateTime TimeValue)
    {
      return DateAndTime.CurrentCalendar.GetSecond(TimeValue);
    }

    /// <summary>Returns an <see langword="Integer" /> value containing a number representing the day of the week.</summary>
    /// <param name="DateValue">Required. <see langword="Date" /> value for which you want to determine the day of the week.</param>
    /// <param name="DayOfWeek">Optional. A value chosen from the <see langword="FirstDayOfWeek" /> enumeration that specifies the first day of the week. If not specified, <see langword="FirstDayOfWeek.Sunday" /> is used.</param>
    /// <returns>Returns an <see langword="Integer" /> value containing a number representing the day of the week.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="DayOfWeek" /> is less than 0 or more than 7.</exception>
    public static int Weekday(DateTime DateValue, FirstDayOfWeek DayOfWeek = FirstDayOfWeek.Sunday)
    {
      if (DayOfWeek == FirstDayOfWeek.System)
        DayOfWeek = (FirstDayOfWeek) (DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek + 1);
      else if (DayOfWeek < FirstDayOfWeek.Sunday || DayOfWeek > FirstDayOfWeek.Saturday)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (DayOfWeek)
        }));
      return checked (unchecked (checked ((int) unchecked (DateAndTime.CurrentCalendar.GetDayOfWeek(DateValue) + 1) - unchecked ((int) DayOfWeek) + 7) % 7) + 1);
    }

    /// <summary>Returns a <see langword="String" /> value containing the name of the specified month.</summary>
    /// <param name="Month">Required. <see langword="Integer" />. The numeric designation of the month, from 1 through 13; 1 indicates January and 12 indicates December. You can use the value 13 with a 13-month calendar. If your system is using a 12-month calendar and <paramref name="Month" /> is 13, <see langword="MonthName" /> returns an empty string.</param>
    /// <param name="Abbreviate">Optional. <see langword="Boolean" /> value that indicates if the month name is to be abbreviated. If omitted, the default is <see langword="False" />, which means the month name is not abbreviated.</param>
    /// <returns>Returns a <see langword="String" /> value containing the name of the specified month.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Month" /> is less than 1 or greater than 13.</exception>
    public static string MonthName(int Month, bool Abbreviate = false)
    {
      if (Month < 1 || Month > 13)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Month)
        }));
      string str = !Abbreviate ? Utils.GetDateTimeFormatInfo().GetMonthName(Month) : Utils.GetDateTimeFormatInfo().GetAbbreviatedMonthName(Month);
      if (str.Length == 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Month)
        }));
      return str;
    }

    /// <summary>Returns a <see langword="String" /> value containing the name of the specified weekday.</summary>
    /// <param name="Weekday">Required. <see langword="Integer" />. The numeric designation for the weekday, from 1 through 7; 1 indicates the first day of the week and 7 indicates the last day of the week. The identities of the first and last days depend on the setting of <paramref name="FirstDayOfWeekValue" />.</param>
    /// <param name="Abbreviate">Optional. <see langword="Boolean" /> value that indicates if the weekday name is to be abbreviated. If omitted, the default is <see langword="False" />, which means the weekday name is not abbreviated.</param>
    /// <param name="FirstDayOfWeekValue">Optional. A value chosen from the <see langword="FirstDayOfWeek" /> enumeration that specifies the first day of the week. If not specified, <see langword="FirstDayOfWeek.System" /> is used.</param>
    /// <returns>Returns a <see langword="String" /> value containing the name of the specified weekday.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Weekday" /> is less than 1 or greater than 7, or <paramref name="FirstDayOfWeekValue" /> is less than 0 or greater than 7.</exception>
    public static string WeekdayName(int Weekday, bool Abbreviate = false, FirstDayOfWeek FirstDayOfWeekValue = FirstDayOfWeek.System)
    {
      if (Weekday < 1 || Weekday > 7)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Weekday)
        }));
      switch (FirstDayOfWeekValue)
      {
        case FirstDayOfWeek.System:
        case FirstDayOfWeek.Sunday:
        case FirstDayOfWeek.Monday:
        case FirstDayOfWeek.Tuesday:
        case FirstDayOfWeek.Wednesday:
        case FirstDayOfWeek.Thursday:
        case FirstDayOfWeek.Friday:
        case FirstDayOfWeek.Saturday:
          DateTimeFormatInfo format = (DateTimeFormatInfo) Utils.GetCultureInfo().GetFormat(typeof (DateTimeFormatInfo));
          if (FirstDayOfWeekValue == FirstDayOfWeek.System)
            FirstDayOfWeekValue = (FirstDayOfWeek) (format.FirstDayOfWeek + 1);
          string str;
          try
          {
            str = !Abbreviate ? format.GetDayName((DayOfWeek) ((int) (Weekday + FirstDayOfWeekValue - 2) % 7)) : format.GetAbbreviatedDayName((DayOfWeek) ((int) (Weekday + FirstDayOfWeekValue - 2) % 7));
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
              nameof (Weekday)
            }));
          }
          if (str.Length == 0)
            throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
            {
              nameof (Weekday)
            }));
          return str;
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            nameof (FirstDayOfWeekValue)
          }));
      }
    }
  }
}
