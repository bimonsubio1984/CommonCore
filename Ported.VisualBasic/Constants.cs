// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Constants
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="Constants" /> module contains miscellaneous constants. These constants can be used anywhere in your code.</summary>
  [StandardModule]
  public sealed class Constants
  {
    /// <summary>Represents the object error number. User-defined error numbers should be greater than this value.</summary>
    public const int vbObjectError = -2147221504;
    /// <summary>Represents a carriage-return character combined with a linefeed character for print and display functions.</summary>
    public const string vbCrLf = "\r\n";
    /// <summary>Represents a newline character for print and display functions.</summary>
    public const string vbNewLine = "\r\n";
    /// <summary>Represents a carriage-return character for print and display functions.</summary>
    public const string vbCr = "\r";
    /// <summary>Represents a linefeed character for print and display functions.</summary>
    public const string vbLf = "\n";
    /// <summary>Represents a backspace character for print and display functions.</summary>
    public const string vbBack = "\b";
    /// <summary>Represents a form-feed character for print functions.</summary>
    public const string vbFormFeed = "\f";
    /// <summary>Represents a tab character for print and display functions.</summary>
    public const string vbTab = "\t";
    /// <summary>Represents a carriage-return character for print functions.</summary>
    public const string vbVerticalTab = "\v";
    /// <summary>Represents a null character for print and display functions.</summary>
    public const string vbNullChar = "\0";
    /// <summary>Represents a zero-length string for print and display functions, and for calling external procedures.</summary>
    public const string vbNullString = null;
    /// <summary>Indicates that the window style is hidden for the invoked program when the <see langword="Shell" /> function is called.</summary>
    public const AppWinStyle vbHide = AppWinStyle.Hide;
    /// <summary>Indicates that the window style is normal with focus for the invoked program when the <see langword="Shell" /> function is called. </summary>
    public const AppWinStyle vbNormalFocus = AppWinStyle.NormalFocus;
    /// <summary>Indicates that the window style is minimized with focus for the invoked program when the <see langword="Shell" /> function is called.</summary>
    public const AppWinStyle vbMinimizedFocus = AppWinStyle.MinimizedFocus;
    /// <summary>Indicates that the window style is maximized with focus for the invoked program when the <see langword="Shell" /> function is called.</summary>
    public const AppWinStyle vbMaximizedFocus = AppWinStyle.MaximizedFocus;
    /// <summary>Indicates that the window style is normal without focus for the invoked program when the <see langword="Shell" /> function is called.</summary>
    public const AppWinStyle vbNormalNoFocus = AppWinStyle.NormalNoFocus;
    /// <summary>Indicates that the window style is minimized without focus for the invoked program when the <see langword="Shell" /> function is called.</summary>
    public const AppWinStyle vbMinimizedNoFocus = AppWinStyle.MinimizedNoFocus;
    /// <summary>Specifies that a method should be called when the <see langword="CallByName" /> function is called.</summary>
    public const CallType vbMethod = CallType.Method;
    /// <summary>Specifies that a property value should be retrieved when the <see langword="CallByName" /> function is called.</summary>
    public const CallType vbGet = CallType.Get;
    /// <summary>Indicates that a property value should be set to an object instance when the <see langword="CallByName" /> function is called.</summary>
    public const CallType vbLet = CallType.Let;
    /// <summary>Indicates that a property value should be set when the <see langword="CallByName" /> function is called.</summary>
    public const CallType vbSet = CallType.Set;
    /// <summary>Specifies that a binary comparison should be performed when comparison functions are called.</summary>
    public const CompareMethod vbBinaryCompare = CompareMethod.Binary;
    /// <summary>Indicates that a text comparison should be performed when comparison functions are called.</summary>
    public const CompareMethod vbTextCompare = CompareMethod.Text;
    /// <summary>Indicates that the general date format for the current culture should be used when the <see langword="FormatDateTime" /> function is called.</summary>
    public const DateFormat vbGeneralDate = DateFormat.GeneralDate;
    /// <summary>Indicates that the long date format for the current culture should be used when the <see langword="FormatDateTime" /> function is called.</summary>
    public const DateFormat vbLongDate = DateFormat.LongDate;
    /// <summary>Indicates that the short-date format for the current culture should be used when the <see langword="FormatDateTime" /> function is called.</summary>
    public const DateFormat vbShortDate = DateFormat.ShortDate;
    /// <summary>Indicates that the long time format for the current culture should be used when the <see langword="FormatDateTime" /> function is called.</summary>
    public const DateFormat vbLongTime = DateFormat.LongTime;
    /// <summary>Indicates that the short-time format for the current culture should be used when the <see langword="FormatDateTime" /> function is called.</summary>
    public const DateFormat vbShortTime = DateFormat.ShortTime;
    /// <summary>Indicates that the day specified by your system as the first day of the week should be used when date-related functions are called.</summary>
    public const FirstDayOfWeek vbUseSystemDayOfWeek = FirstDayOfWeek.System;
    /// <summary>Specifies that Sunday should be used as the first day of the week when date-related functions are called.</summary>
    public const FirstDayOfWeek vbSunday = FirstDayOfWeek.Sunday;
    /// <summary>Specifies that Monday should be used as the first day of the week when date-related functions are called.</summary>
    public const FirstDayOfWeek vbMonday = FirstDayOfWeek.Monday;
    /// <summary>Specifies that Tuesday should be used as the first day of the week when date-related functions are called.</summary>
    public const FirstDayOfWeek vbTuesday = FirstDayOfWeek.Tuesday;
    /// <summary>Specifies that Wednesday should be used as the first day of the week when date-related functions are called.</summary>
    public const FirstDayOfWeek vbWednesday = FirstDayOfWeek.Wednesday;
    /// <summary>Specifies that Thursday should be used as the first day of the week when date-related functions are called.</summary>
    public const FirstDayOfWeek vbThursday = FirstDayOfWeek.Thursday;
    /// <summary>Specifies that Friday should be used as the first day of the week when date-related functions are called.</summary>
    public const FirstDayOfWeek vbFriday = FirstDayOfWeek.Friday;
    /// <summary>Specifies that Saturday should be used as the first day of the week when date-related functions are called.</summary>
    public const FirstDayOfWeek vbSaturday = FirstDayOfWeek.Saturday;
    /// <summary>Indicates that the file is a normal file for file-access functions.</summary>
    public const FileAttribute vbNormal = FileAttribute.Normal;
    /// <summary>Indicates that the file is a read-only file for file-access functions.</summary>
    public const FileAttribute vbReadOnly = FileAttribute.ReadOnly;
    /// <summary>Indicates that the file is a hidden file for file-access functions.</summary>
    public const FileAttribute vbHidden = FileAttribute.Hidden;
    /// <summary>Indicates that the file is a system file for file-access functions.</summary>
    public const FileAttribute vbSystem = FileAttribute.System;
    /// <summary>Indicates the volume label file attribute for file-access functions.</summary>
    public const FileAttribute vbVolume = FileAttribute.Volume;
    /// <summary>Indicates that the file is a directory or folder for file-access functions.</summary>
    public const FileAttribute vbDirectory = FileAttribute.Directory;
    /// <summary>Indicates that the file has changed since the last backup operation for file-access functions.</summary>
    public const FileAttribute vbArchive = FileAttribute.Archive;
    /// <summary>Indicates that the week specified by your system as the first week of the year should be used when date-related functions are called.</summary>
    public const FirstWeekOfYear vbUseSystem = FirstWeekOfYear.System;
    /// <summary>Indicates that the week of the year in which January 1 occurs should be used when date-related functions are called.</summary>
    public const FirstWeekOfYear vbFirstJan1 = FirstWeekOfYear.Jan1;
    /// <summary>Indicates that the first week of the year that has at least four days should be used when date-related functions are called.</summary>
    public const FirstWeekOfYear vbFirstFourDays = FirstWeekOfYear.FirstFourDays;
    /// <summary>Indicates that the first full week of the year should be used when date-related functions are called.</summary>
    public const FirstWeekOfYear vbFirstFullWeek = FirstWeekOfYear.FirstFullWeek;
    /// <summary>Indicates that characters should be converted to uppercase when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbUpperCase = VbStrConv.Uppercase;
    /// <summary>Indicates that characters should be converted to lowercase when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbLowerCase = VbStrConv.Lowercase;
    /// <summary>Indicates that the first letter of every word in a string should be converted to uppercase and the remaining characters to lowercase when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbProperCase = VbStrConv.ProperCase;
    /// <summary>Indicates that narrow (single-byte) characters should be converted to wide (double-byte) characters when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbWide = VbStrConv.Wide;
    /// <summary>Indicates that wide (double-byte) characters should be converted to narrow (single-byte) characters when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbNarrow = VbStrConv.Narrow;
    /// <summary>Indicates that Katakana characters should be converted to Hiragana characters when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbKatakana = VbStrConv.Katakana;
    /// <summary>Indicates Hiragana characters should be converted to Katakana characters when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbHiragana = VbStrConv.Hiragana;
    /// <summary>Indicates that characters should be converted to Simplified Chinese when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbSimplifiedChinese = VbStrConv.SimplifiedChinese;
    /// <summary>Indicates that characters should be converted to Traditional Chinese when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbTraditionalChinese = VbStrConv.TraditionalChinese;
    /// <summary>Indicates that characters should be converted to use linguistic rules for casing instead of file system rules for casing to when the <see langword="StrConv" /> function is called.</summary>
    public const VbStrConv vbLinguisticCasing = VbStrConv.LinguisticCasing;
    /// <summary>Indicates that the default <see langword="Boolean" /> value should be used when number-formatting functions are called.</summary>
    public const TriState vbUseDefault = TriState.UseDefault;
    /// <summary>Indicates that a <see langword="Boolean" /> value of <see langword="True" /> should be used when number-formatting functions are called.</summary>
    public const TriState vbTrue = TriState.True;
    /// <summary>Indicates that a <see langword="Boolean" /> value of <see langword="False" /> should be used when number-formatting functions are called.</summary>
    public const TriState vbFalse = TriState.False;
    /// <summary>Indicates that the type of a variant object is <see langword="Empty" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbEmpty = VariantType.Empty;
    /// <summary>Indicates that the type of a variant object is <see langword="Nothing" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbNull = VariantType.Null;
    /// <summary>Indicates that the type of a variant object is <see langword="Integer" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbInteger = VariantType.Integer;
    /// <summary>Indicates the type of a variant object is <see langword="Long" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbLong = VariantType.Long;
    /// <summary>Indicates that the type of a variant object is <see langword="Single" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbSingle = VariantType.Single;
    /// <summary>Indicates that the type of a variant object is <see langword="Double" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbDouble = VariantType.Double;
    /// <summary>Indicates that the type of a variant object is <see langword="Currency" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbCurrency = VariantType.Currency;
    /// <summary>Indicates that the type of a variant object is <see langword="Date" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbDate = VariantType.Date;
    /// <summary>Indicates that the type of a variant object is <see langword="String" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbString = VariantType.String;
    /// <summary>Indicates that the type of a variant object is <see langword="Object" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbObject = VariantType.Object;
    /// <summary>Indicates that the type of a variant object is <see langword="Boolean" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbBoolean = VariantType.Boolean;
    /// <summary>Indicates that the type of a variant object is <see langword="Variant" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbVariant = VariantType.Variant;
    /// <summary>Indicates that the type of a variant object is <see langword="Decimal" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbDecimal = VariantType.Decimal;
    /// <summary>Indicates that the type of a variant object is <see langword="Byte" />. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbByte = VariantType.Byte;
    /// <summary>Indicates that the type of a variant object is a user-defined type. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbUserDefinedType = VariantType.UserDefinedType;
    /// <summary>Indicates that the type of a variant object is an array. Returned by the <see langword="VarType" /> function.</summary>
    public const VariantType vbArray = VariantType.Array;
    /// <summary>Indicates that the <see langword="OK" /> button was clicked in a message box. Returned by the <see langword="MsgBox" /> function.</summary>
    public const MsgBoxResult vbOK = MsgBoxResult.Ok;
    /// <summary>Indicates that the <see langword="Cancel" /> button was clicked in a message box. Returned by the <see langword="MsgBox" /> function.</summary>
    public const MsgBoxResult vbCancel = MsgBoxResult.Cancel;
    /// <summary>Indicates that the <see langword="Abort" /> button was clicked in a message box. Returned by the <see langword="MsgBox" /> function.</summary>
    public const MsgBoxResult vbAbort = MsgBoxResult.Abort;
    /// <summary>Indicates that the <see langword="Retry" /> button was clicked in a message box. Returned by the <see langword="MsgBox" /> function.</summary>
    public const MsgBoxResult vbRetry = MsgBoxResult.Retry;
    /// <summary>Indicates that the <see langword="Ignore" /> button was clicked in a message box. Returned by the <see langword="MsgBox" /> function.</summary>
    public const MsgBoxResult vbIgnore = MsgBoxResult.Ignore;
    /// <summary>Indicates that the <see langword="Yes" /> button was clicked in a message box. Returned by the <see langword="MsgBox" /> function.</summary>
    public const MsgBoxResult vbYes = MsgBoxResult.Yes;
    /// <summary>Indicates that the <see langword="No" /> button was clicked in a message box. Returned by the <see langword="MsgBox" /> function.</summary>
    public const MsgBoxResult vbNo = MsgBoxResult.No;
    /// <summary>Indicates that only the <see langword="OK" /> button will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbOKOnly = MsgBoxStyle.OkOnly;
    /// <summary>Indicates that the <see langword="OK" /> and <see langword="Cancel" /> buttons will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbOKCancel = MsgBoxStyle.OkCancel;
    /// <summary>Indicates that the <see langword="Abort" />, <see langword="Retry" />, and <see langword="Ignore" /> buttons will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbAbortRetryIgnore = MsgBoxStyle.AbortRetryIgnore;
    /// <summary>Indicates that the <see langword="Yes" />, <see langword="No" />, and <see langword="Cancel" /> buttons will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbYesNoCancel = MsgBoxStyle.YesNoCancel;
    /// <summary>Indicates that the <see langword="Yes" /> and <see langword="No" /> buttons will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbYesNo = MsgBoxStyle.YesNo;
    /// <summary>Indicates that the <see langword="Retry" /> and <see langword="Cancel" /> buttons will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbRetryCancel = MsgBoxStyle.RetryCancel;
    /// <summary>Indicates that the critical message icon will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbCritical = MsgBoxStyle.Critical;
    /// <summary>Indicates that the question icon will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbQuestion = MsgBoxStyle.Question;
    /// <summary>Indicates that the exclamation icon will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbExclamation = MsgBoxStyle.Exclamation;
    /// <summary>Indicates that the information icon will display when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbInformation = MsgBoxStyle.Information;
    /// <summary>Indicates that the leftmost button is selected as the default button when the message box appears.</summary>
    public const MsgBoxStyle vbDefaultButton1 = MsgBoxStyle.OkOnly;
    /// <summary>Indicates that the second button from the left is selected as the default button when the message box appears.</summary>
    public const MsgBoxStyle vbDefaultButton2 = MsgBoxStyle.DefaultButton2;
    /// <summary>Indicates that the third button from the left is selected as the default button when the message box appears.</summary>
    public const MsgBoxStyle vbDefaultButton3 = MsgBoxStyle.DefaultButton3;
    /// <summary>Indicates that the message box will be displayed as a modal dialog box when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbApplicationModal = MsgBoxStyle.OkOnly;
    /// <summary>Indicates that the message box will be displayed as a modal dialog box when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbSystemModal = MsgBoxStyle.SystemModal;
    /// <summary>Indicates that the <see langword="Help" /> button will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbMsgBoxHelp = MsgBoxStyle.MsgBoxHelp;
    /// <summary>Indicates that text will be right-aligned when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbMsgBoxRight = MsgBoxStyle.MsgBoxRight;
    /// <summary>Indicates that right-to-left reading text (Hebrew and Arabic systems) will be displayed when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbMsgBoxRtlReading = MsgBoxStyle.MsgBoxRtlReading;
    /// <summary>Indicates that the message box will display in the foreground when the <see langword="MsgBox" /> function is called.</summary>
    public const MsgBoxStyle vbMsgBoxSetForeground = MsgBoxStyle.MsgBoxSetForeground;
  }
}
