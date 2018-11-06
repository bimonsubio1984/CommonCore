// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.LikeOperator
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class provides helpers that the Visual Basic compiler uses to do the work for the Like Operator (Visual Basic). It is not meant to be called directly from your code.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class LikeOperator
  {
    private static string[] LigatureExpansions = new string[9]
    {
      "",
      "ss",
      "sz",
      "AE",
      "ae",
      "TH",
      "th",
      "OE",
      "oe"
    };
    private static byte[] LigatureMap = new byte[142];

    private LikeOperator()
    {
    }

    static LikeOperator()
    {
      LikeOperator.LigatureMap[25] = (byte) 1;
      LikeOperator.LigatureMap[25] = (byte) 2;
      LikeOperator.LigatureMap[0] = (byte) 3;
      LikeOperator.LigatureMap[32] = (byte) 4;
      LikeOperator.LigatureMap[24] = (byte) 5;
      LikeOperator.LigatureMap[56] = (byte) 6;
      LikeOperator.LigatureMap[140] = (byte) 7;
      LikeOperator.LigatureMap[141] = (byte) 8;
    }

    private static byte LigatureIndex(char ch)
    {
      if (Strings.Asc(ch) < 198 || Strings.Asc(ch) > 339)
        return 0;
      return LikeOperator.LigatureMap[checked (Strings.Asc(ch) - 198)];
    }

    private static int CanCharExpand(char ch, byte[] LocaleSpecificLigatureTable, CompareInfo Comparer, CompareOptions Options)
    {
      byte num1 = LikeOperator.LigatureIndex(ch);
      if (num1 == (byte) 0)
        return 0;
      if (LocaleSpecificLigatureTable[(int) num1] == (byte) 0)
        LocaleSpecificLigatureTable[(int) num1] = Comparer.Compare(Conversions.ToString(ch), LikeOperator.LigatureExpansions[(int) num1]) != 0 ? (byte) 2 : (byte) 1;
      if (LocaleSpecificLigatureTable[(int) num1] == (byte) 1)
        return (int) num1;
      int num2=0;
      return num2;
    }

    private static string GetCharExpansion(char ch, byte[] LocaleSpecificLigatureTable, CompareInfo Comparer, CompareOptions Options)
    {
      int index = LikeOperator.CanCharExpand(ch, LocaleSpecificLigatureTable, Comparer, Options);
      if (index == 0)
        return Conversions.ToString(ch);
      return LikeOperator.LigatureExpansions[index];
    }

    private static void ExpandString(ref string Input, ref int Length, ref LikeOperator.LigatureInfo[] InputLigatureInfo, byte[] LocaleSpecificLigatureTable, CompareInfo Comparer, CompareOptions Options, ref bool WidthChanged, bool UseFullWidth)
    {
      WidthChanged = false;
      if (Length == 0)
        return;
      CultureInfo cultureInfo = Utils.GetCultureInfo();
      Encoding encoding = Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage);
      int dwMapFlags = 256;
      bool flag = false;
      if (!encoding.IsSingleByte)
      {
        dwMapFlags = 4194560;
        if (Strings.IsValidCodePage(932))
        {
          dwMapFlags = !UseFullWidth ? 6291712 : 10486016;
          Input = Strings.vbLCMapString(cultureInfo, dwMapFlags, Input);
          flag = true;
          if (Input.Length != Length)
          {
            Length = Input.Length;
            WidthChanged = true;
          }
        }
      }
      if (!flag)
        Input = Strings.vbLCMapString(cultureInfo, dwMapFlags, Input);
      int num1 = 0;
      int num2 = checked (Length - 1);
      int index1 = num1;
      int num3=0;
      while (index1 <= num2)
      {
        if (LikeOperator.CanCharExpand(Input[index1], LocaleSpecificLigatureTable, Comparer, Options) != 0)
          checked { ++num3; }
        checked { ++index1; }
      }
      if (num3 <= 0)
        return;
      InputLigatureInfo = new LikeOperator.LigatureInfo[checked (Length + num3 - 1 + 1)];
      StringBuilder stringBuilder = new StringBuilder(checked (Length + num3 - 1));
      int index2 = 0;
      int num4 = 0;
      int num5 = checked (Length - 1);
      int index3 = num4;
      while (index3 <= num5)
      {
        char ch = Input[index3];
        if (LikeOperator.CanCharExpand(ch, LocaleSpecificLigatureTable, Comparer, Options) != 0)
        {
          string charExpansion = LikeOperator.GetCharExpansion(ch, LocaleSpecificLigatureTable, Comparer, Options);
          stringBuilder.Append(charExpansion);
          InputLigatureInfo[index2].Kind = LikeOperator.CharKind.ExpandedChar1;
          InputLigatureInfo[index2].CharBeforeExpansion = ch;
          checked { ++index2; }
          InputLigatureInfo[index2].Kind = LikeOperator.CharKind.ExpandedChar2;
          InputLigatureInfo[index2].CharBeforeExpansion = ch;
        }
        else
          stringBuilder.Append(ch);
        checked { ++index2; }
        checked { ++index3; }
      }
      Input = stringBuilder.ToString();
      Length = stringBuilder.Length;
    }

    /// <summary>Performs binary or text string comparison given two objects. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Source">The string to compare.</param>
    /// <param name="Pattern">The string against which <paramref name="Source" /> is being compared.</param>
    /// <param name="CompareOption">A <see cref="T:Ported.VisualBasic.CompareMethod" /> enumeration specifying whether or not to use text comparison. If <see cref="F:Ported.VisualBasic.CompareMethod.Text" />, this method uses text comparison; if <see cref="F:Ported.VisualBasic.CompareMethod.Binary" />, this method uses binary comparison.</param>
    /// <returns>A <see langword="Boolean" /> value indicating whether the strings match. Returns <see langword="True" /> if the strings match; otherwise <see langword="False" />.</returns>
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
        return (object) LikeOperator.LikeString(Conversions.ToString(Source), Conversions.ToString(Pattern), CompareOption);
      return Operators.InvokeUserDefinedOperator(Symbols.UserDefinedOperator.Like, new object[2]
      {
        Source,
        Pattern
      });
    }

    /// <summary>Performs binary or text string comparison given two strings. This helper method is not meant to be called directly from your code.</summary>
    /// <param name="Source">The string to compare.</param>
    /// <param name="Pattern">The string against which <paramref name="Source" /> is being compared.</param>
    /// <param name="CompareOption">A <see cref="T:Ported.VisualBasic.CompareMethod" /> enumeration specifying whether or not to use text comparison. If <see cref="F:Ported.VisualBasic.CompareMethod.Text" />, this method uses text comparison; if <see cref="F:Ported.VisualBasic.CompareMethod.Binary" />, this method uses binary comparison.</param>
    /// <returns>A <see langword="Boolean" /> value indicating whether the strings match. Returns <see langword="True" /> if the strings match; otherwise <see langword="False" />.</returns>
    public static bool LikeString(string Source, string Pattern, CompareMethod CompareOption)
    {
      LikeOperator.LigatureInfo[] ligatureInfoArray1 = (LikeOperator.LigatureInfo[]) null;
      LikeOperator.LigatureInfo[] ligatureInfoArray2 = (LikeOperator.LigatureInfo[]) null;
      int num1 = Pattern != null ? Pattern.Length : 0;
      int num2 = Source != null ? Source.Length : 0;
      CompareOptions Options;
      CompareInfo Comparer1;
      bool flag1;
      if (CompareOption == CompareMethod.Binary)
      {
        Options = CompareOptions.Ordinal;
        Comparer1 = (CompareInfo) null;
      }
      else
      {
        Comparer1 = Utils.GetCultureInfo().CompareInfo;
        Options = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
        byte[] numArray = new byte[checked (LikeOperator.LigatureExpansions.Length - 1 + 1)];
        ref string local1 = ref Source;
        ref int local2 = ref num2;
        ref LikeOperator.LigatureInfo[] local3 = ref ligatureInfoArray1;
        byte[] LocaleSpecificLigatureTable1 = numArray;
        CompareInfo Comparer2 = Comparer1;
        int num3 = (int) Options;
        flag1 = false;
        ref bool local4 = ref flag1;
        int num4 = 0;
        LikeOperator.ExpandString(ref local1, ref local2, ref local3, LocaleSpecificLigatureTable1, Comparer2, (CompareOptions) num3, ref local4, num4 != 0);
        ref string local5 = ref Pattern;
        ref int local6 = ref num1;
        ref LikeOperator.LigatureInfo[] local7 = ref ligatureInfoArray2;
        byte[] LocaleSpecificLigatureTable2 = numArray;
        CompareInfo Comparer3 = Comparer1;
        int num5 = (int) Options;
        flag1 = false;
        ref bool local8 = ref flag1;
        int num6 = 0;
        LikeOperator.ExpandString(ref local5, ref local6, ref local7, LocaleSpecificLigatureTable2, Comparer3, (CompareOptions) num5, ref local8, num6 != 0);
      }
      int RightEnd=0;
      int index=0;
      while (RightEnd < num1 && index < num2)
      {
        switch (Pattern[RightEnd])
        {
          case '#':
          case '＃':
            if (!char.IsDigit(Source[index]))
              return false;
            break;
          case '*':
          case '＊':
            bool Mismatch=false;
            bool PatternError=false;
            LikeOperator.MatchAsterisk(Source, num2, index, ligatureInfoArray1, Pattern, num1, RightEnd, ligatureInfoArray2, ref Mismatch, ref PatternError, Comparer1, Options);
            if (PatternError)
              throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
              {
                nameof (Pattern)
              }));
            return !Mismatch;
          case '?':
          case '？':
            LikeOperator.SkipToEndOfExpandedChar(ligatureInfoArray1, num2, ref index);
            break;
          case '[':
          case '［':
            string Source1 = Source;
            int SourceLength = num2;
            ref int local1 = ref index;
            LikeOperator.LigatureInfo[] SourceLigatureInfo = ligatureInfoArray1;
            string Pattern1 = Pattern;
            int PatternLength = num1;
            ref int local2 = ref RightEnd;
            LikeOperator.LigatureInfo[] PatternLigatureInfo = ligatureInfoArray2;
            bool flag2=false;
            ref bool local3 = ref flag2;
            bool flag3=false;
            ref bool local4 = ref flag3;
            bool flag4=false;
            ref bool local5 = ref flag4;
            CompareInfo Comparer2 = Comparer1;
            int num3 = (int) Options;
            flag1 = false;
            ref bool local6 = ref flag1;
            // ISSUE: variable of the null type
            object local7 = null;
            int num4 = 0;
            LikeOperator.MatchRange(Source1, SourceLength, ref local1, SourceLigatureInfo, Pattern1, PatternLength, ref local2, PatternLigatureInfo, ref local3, ref local4, ref local5, Comparer2, (CompareOptions) num3, ref local6, (List<LikeOperator.Range>) local7, num4 != 0);
            if (flag4)
              throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
              {
                nameof (Pattern)
              }));
            if (flag3)
              return false;
            if (flag2)
            {
              checked { ++RightEnd; }
              continue;
            }
            break;
          default:
            if (LikeOperator.CompareChars(Source, num2, index, ref index, ligatureInfoArray1, Pattern, num1, RightEnd, ref RightEnd, ligatureInfoArray2, Comparer1, Options, false, false) != 0)
              return false;
            break;
        }
        checked { ++RightEnd; }
        checked { ++index; }
      }
      while (RightEnd < num1)
      {
        char ch = Pattern[RightEnd];
        switch (ch)
        {
          case '*':
          case '＊':
            checked { ++RightEnd; }
            continue;
          default:
            if (checked (RightEnd + 1) < num1 && (ch == '[' && Pattern[checked (RightEnd + 1)] == ']' || ch == '［' && Pattern[checked (RightEnd + 1)] == '］'))
            {
              checked { RightEnd += 2; }
              continue;
            }
            goto label_25;
        }
      }
label_25:
      return RightEnd >= num1 && index >= num2;
    }

    private static void SkipToEndOfExpandedChar(LikeOperator.LigatureInfo[] InputLigatureInfo, int Length, ref int Current)
    {
      if (InputLigatureInfo == null || Current >= Length || InputLigatureInfo[Current].Kind != LikeOperator.CharKind.ExpandedChar1)
        return;
      checked { ++Current; }
    }

    private static int CompareChars(string Left, int LeftLength, int LeftStart, ref int LeftEnd, LikeOperator.LigatureInfo[] LeftLigatureInfo, string Right, int RightLength, int RightStart, ref int RightEnd, LikeOperator.LigatureInfo[] RightLigatureInfo, CompareInfo Comparer, CompareOptions Options, bool MatchBothCharsOfExpandedCharInRight = false, bool UseUnexpandedCharForRight = false)
    {
      LeftEnd = LeftStart;
      RightEnd = RightStart;
      if (Options == CompareOptions.Ordinal)
        return checked ((int) Left[LeftStart] - (int) Right[RightStart]);
      if (UseUnexpandedCharForRight)
      {
        if (RightLigatureInfo != null && RightLigatureInfo[RightEnd].Kind == LikeOperator.CharKind.ExpandedChar1)
        {
          Right = Right.Substring(RightStart, checked (RightEnd - RightStart));
          Right += Conversions.ToString(RightLigatureInfo[RightEnd].CharBeforeExpansion);
          checked { ++RightEnd; }
          return LikeOperator.CompareChars(Left.Substring(LeftStart, checked (LeftEnd - LeftStart + 1)), Right, Comparer, Options);
        }
      }
      else if (MatchBothCharsOfExpandedCharInRight)
      {
        int num1 = RightEnd;
        LikeOperator.SkipToEndOfExpandedChar(RightLigatureInfo, RightLength, ref RightEnd);
        if (num1 < RightEnd)
        {
          int num2 = 0;
          if (checked (LeftEnd + 1) < LeftLength)
            num2 = 1;
          int num3 = LikeOperator.CompareChars(Left.Substring(LeftStart, checked (LeftEnd - LeftStart + 1 + num2)), Right.Substring(RightStart, checked (RightEnd - RightStart + 1)), Comparer, Options);
          if (num3 == 0)
            checked { LeftEnd += num2; }
          return num3;
        }
      }
      if (LeftEnd == LeftStart && RightEnd == RightStart)
        return Comparer.Compare(Conversions.ToString(Left[LeftStart]), Conversions.ToString(Right[RightStart]), Options);
      return LikeOperator.CompareChars(Left.Substring(LeftStart, checked (LeftEnd - LeftStart + 1)), Right.Substring(RightStart, checked (RightEnd - RightStart + 1)), Comparer, Options);
    }

    private static int CompareChars(string Left, string Right, CompareInfo Comparer, CompareOptions Options)
    {
      if (Options == CompareOptions.Ordinal)
        return checked ((int) Left[0] - (int) Right[0]);
      return Comparer.Compare(Left, Right, Options);
    }

    private static int CompareChars(char Left, char Right, CompareInfo Comparer, CompareOptions Options)
    {
      if (Options == CompareOptions.Ordinal)
        return checked ((int) Left - (int) Right);
      return Comparer.Compare(Conversions.ToString(Left), Conversions.ToString(Right), Options);
    }

    private static void MatchRange(string Source, int SourceLength, ref int SourceIndex, LikeOperator.LigatureInfo[] SourceLigatureInfo, string Pattern, int PatternLength, ref int PatternIndex, LikeOperator.LigatureInfo[] PatternLigatureInfo, ref bool RangePatternEmpty, ref bool Mismatch, ref bool PatternError, CompareInfo Comparer, CompareOptions Options, ref bool SeenNot , List<LikeOperator.Range> RangeList = null, bool ValidatePatternWithoutMatching = false)
    {
      RangePatternEmpty = false;
      Mismatch = false;
      PatternError = false;
      SeenNot = false;
      checked { ++PatternIndex; }
      if (PatternIndex >= PatternLength)
      {
        PatternError = true;
      }
      else
      {
        char ch = Pattern[PatternIndex];
        if (ch == '!' || ch == '！')
        {
          SeenNot = true;
          checked { ++PatternIndex; }
          if (PatternIndex >= PatternLength)
          {
            Mismatch = true;
            return;
          }
          ch = Pattern[PatternIndex];
        }
        if (ch == ']' || ch == '］')
        {
          if (SeenNot)
          {
            SeenNot = false;
            if (!ValidatePatternWithoutMatching)
              Mismatch = LikeOperator.CompareChars(Source[SourceIndex], '!', Comparer, Options) != 0;
            if (RangeList == null)
              return;
            LikeOperator.Range range;
            range.Start = checked (PatternIndex - 1);
            range.StartLength = 1;
            range.End = -1;
            range.EndLength = 0;
            RangeList.Add(range);
          }
          else
            RangePatternEmpty = true;
        }
        else
        {
          int LeftEnd=0;
          int num1 = 0;
          while (true)
          {
            if (ch != ']' && ch != '］')
            {
              if (!ValidatePatternWithoutMatching && PatternLigatureInfo != null && PatternLigatureInfo[PatternIndex].Kind == LikeOperator.CharKind.ExpandedChar1)
              {
                if (LikeOperator.CompareChars(Source, SourceLength, SourceIndex, ref LeftEnd, SourceLigatureInfo, Pattern, PatternLength, PatternIndex, ref num1, PatternLigatureInfo, Comparer, Options, true, false) == 0)
                  goto label_17;
              }
              else
              {
                num1 = PatternIndex;
                LikeOperator.SkipToEndOfExpandedChar(PatternLigatureInfo, PatternLength, ref num1);
              }
              LikeOperator.Range range;
              range.Start = PatternIndex;
              range.StartLength = checked (num1 - PatternIndex + 1);
              string Left1;
              if (Options == CompareOptions.Ordinal)
                Left1 = Conversions.ToString(Pattern[PatternIndex]);
              else if (PatternLigatureInfo != null && PatternLigatureInfo[PatternIndex].Kind == LikeOperator.CharKind.ExpandedChar1)
              {
                Left1 = Conversions.ToString(PatternLigatureInfo[PatternIndex].CharBeforeExpansion);
                PatternIndex = num1;
              }
              else
              {
                Left1 = Pattern.Substring(PatternIndex, checked (num1 - PatternIndex + 1));
                PatternIndex = num1;
              }
              if (checked (num1 + 2) < PatternLength && (Pattern[checked (num1 + 1)] == '-' || Pattern[checked (num1 + 1)] == '－') && (Pattern[checked (num1 + 2)] != ']' && Pattern[checked (num1 + 2)] != '］'))
              {
                checked { PatternIndex += 2; }
                if (!ValidatePatternWithoutMatching && PatternLigatureInfo != null && PatternLigatureInfo[PatternIndex].Kind == LikeOperator.CharKind.ExpandedChar1)
                {
                  if (LikeOperator.CompareChars(Source, SourceLength, SourceIndex, ref LeftEnd, SourceLigatureInfo, Pattern, PatternLength, PatternIndex, ref num1, PatternLigatureInfo, Comparer, Options, true, false) == 0)
                    goto label_27;
                }
                else
                {
                  num1 = PatternIndex;
                  LikeOperator.SkipToEndOfExpandedChar(PatternLigatureInfo, PatternLength, ref num1);
                }
                range.End = PatternIndex;
                range.EndLength = checked (num1 - PatternIndex + 1);
                string Right1;
                if (Options == CompareOptions.Ordinal)
                  Right1 = Conversions.ToString(Pattern[PatternIndex]);
                else if (PatternLigatureInfo != null && PatternLigatureInfo[PatternIndex].Kind == LikeOperator.CharKind.ExpandedChar1)
                {
                  Right1 = Conversions.ToString(PatternLigatureInfo[PatternIndex].CharBeforeExpansion);
                  PatternIndex = num1;
                }
                else
                {
                  Right1 = Pattern.Substring(PatternIndex, checked (num1 - PatternIndex + 1));
                  PatternIndex = num1;
                }
                if (LikeOperator.CompareChars(Left1, Right1, Comparer, Options) <= 0)
                {
                  if (!ValidatePatternWithoutMatching)
                  {
                    string Left2 = Source;
                    int LeftLength1 = SourceLength;
                    int LeftStart1 = SourceIndex;
                    ref int local1 = ref LeftEnd;
                    LikeOperator.LigatureInfo[] LeftLigatureInfo1 = SourceLigatureInfo;
                    string Right2 = Pattern;
                    int RightLength1 = checked (range.Start + range.StartLength);
                    int start = range.Start;
                    int num2 = 0;
                    ref int local2 = ref num2;
                    LikeOperator.LigatureInfo[] RightLigatureInfo1 = PatternLigatureInfo;
                    CompareInfo Comparer1 = Comparer;
                    int num3 = (int) Options;
                    int num4 = 0;
                    int num5 = 1;
                    if (LikeOperator.CompareChars(Left2, LeftLength1, LeftStart1, ref local1, LeftLigatureInfo1, Right2, RightLength1, start, ref local2, RightLigatureInfo1, Comparer1, (CompareOptions) num3, num4 != 0, num5 != 0) >= 0)
                    {
                      string Left3 = Source;
                      int LeftLength2 = SourceLength;
                      int LeftStart2 = SourceIndex;
                      ref int local3 = ref LeftEnd;
                      LikeOperator.LigatureInfo[] LeftLigatureInfo2 = SourceLigatureInfo;
                      string Right3 = Pattern;
                      int RightLength2 = checked (range.End + range.EndLength);
                      int end = range.End;
                      int num6 = 0;
                      ref int local4 = ref num6;
                      LikeOperator.LigatureInfo[] RightLigatureInfo2 = PatternLigatureInfo;
                      CompareInfo Comparer2 = Comparer;
                      int num7 = (int) Options;
                      int num8 = 0;
                      int num9 = 1;
                      if (LikeOperator.CompareChars(Left3, LeftLength2, LeftStart2, ref local3, LeftLigatureInfo2, Right3, RightLength2, end, ref local4, RightLigatureInfo2, Comparer2, (CompareOptions) num7, num8 != 0, num9 != 0) <= 0)
                        goto label_39;
                    }
                  }
                }
                else
                  goto label_35;
              }
              else
              {
                if (!ValidatePatternWithoutMatching)
                {
                  string Left2 = Source;
                  int LeftLength = SourceLength;
                  int LeftStart = SourceIndex;
                  ref int local1 = ref LeftEnd;
                  LikeOperator.LigatureInfo[] LeftLigatureInfo = SourceLigatureInfo;
                  string Right = Pattern;
                  int RightLength = checked (range.Start + range.StartLength);
                  int start = range.Start;
                  int num2 = 0;
                  ref int local2 = ref num2;
                  LikeOperator.LigatureInfo[] RightLigatureInfo = PatternLigatureInfo;
                  CompareInfo Comparer1 = Comparer;
                  int num3 = (int) Options;
                  int num4 = 0;
                  int num5 = 1;
                  if (LikeOperator.CompareChars(Left2, LeftLength, LeftStart, ref local1, LeftLigatureInfo, Right, RightLength, start, ref local2, RightLigatureInfo, Comparer1, (CompareOptions) num3, num4 != 0, num5 != 0) == 0)
                    goto label_39;
                }
                range.End = -1;
                range.EndLength = 0;
              }
              RangeList?.Add(range);
              checked { ++PatternIndex; }
              if (PatternIndex < PatternLength)
                ch = Pattern[PatternIndex];
              else
                goto label_51;
            }
            else
              break;
          }
          Mismatch = !SeenNot;
          return;
label_17:
          SourceIndex = LeftEnd;
          PatternIndex = num1;
          goto label_39;
label_27:
          PatternIndex = num1;
          goto label_39;
label_35:
          PatternError = true;
          return;
label_39:
          if (SeenNot)
          {
            Mismatch = true;
            return;
          }
          do
          {
            checked { ++PatternIndex; }
            if (PatternIndex >= PatternLength)
            {
              PatternError = true;
              return;
            }
          }
          while (Pattern[PatternIndex] != ']' && Pattern[PatternIndex] != '］');
          SourceIndex = LeftEnd;
          return;
label_51:
          PatternError = true;
        }
      }
    }

    private static bool ValidateRangePattern(string Pattern, int PatternLength, ref int PatternIndex, LikeOperator.LigatureInfo[] PatternLigatureInfo, CompareInfo Comparer, CompareOptions Options, ref bool SeenNot, ref List<LikeOperator.Range> RangeList)
    {
      object local1 = null;
      int SourceLength = -1;
      int num1 = -1;
      ref int local2 = ref num1;
      object local3 = null;
      string Pattern1 = Pattern;
      int PatternLength1 = PatternLength;
      ref int local4 = ref PatternIndex;
      LikeOperator.LigatureInfo[] PatternLigatureInfo1 = PatternLigatureInfo;
      bool flag1 = false;
      ref bool local5 = ref flag1;
      bool flag2 = false;
      ref bool local6 = ref flag2;
      bool flag3=false;
      ref bool local7 = ref flag3;
      CompareInfo Comparer1 = Comparer;
      int num2 = (int) Options;
      ref bool local8 = ref SeenNot;
      List<LikeOperator.Range> RangeList1 = RangeList;
      int num3 = 1;
      LikeOperator.MatchRange((string) local1, SourceLength, ref local2, (LikeOperator.LigatureInfo[]) local3, Pattern1, PatternLength1, ref local4, PatternLigatureInfo1, ref local5, ref local6, ref local7, Comparer1, (CompareOptions) num2, ref local8, RangeList1, num3 != 0);
      return !flag3;
    }

    private static void BuildPatternGroups(string Source, int SourceLength, ref int SourceIndex, LikeOperator.LigatureInfo[] SourceLigatureInfo, string Pattern, int PatternLength, ref int PatternIndex, LikeOperator.LigatureInfo[] PatternLigatureInfo, ref bool PatternError, ref int PGIndexForLastAsterisk, CompareInfo Comparer, CompareOptions Options, ref LikeOperator.PatternGroup[] PatternGroups)
    {
      PatternError = false;
      PGIndexForLastAsterisk = 0;
      PatternGroups = new LikeOperator.PatternGroup[16];
      int num1 = 15;
      LikeOperator.PatternType patternType = LikeOperator.PatternType.NONE;
      int index1 = 0;
      do
      {
        if (index1 >= num1)
        {
          LikeOperator.PatternGroup[] patternGroupArray = new LikeOperator.PatternGroup[checked (num1 + 16 + 1)];
          PatternGroups.CopyTo((Array) patternGroupArray, 0);
          PatternGroups = patternGroupArray;
          checked { num1 += 16; }
        }
        switch (Pattern[PatternIndex])
        {
          case '#':
          case '＃':
            if (patternType == LikeOperator.PatternType.DIGIT)
            {
              LikeOperator.PatternGroup[] patternGroupArray = PatternGroups;
              int index2 = checked (index1 - 1);
              int index3 = index2;
              patternGroupArray[index3].CharCount = checked (PatternGroups[index2].CharCount + 1);
              break;
            }
            PatternGroups[index1].PatType = LikeOperator.PatternType.DIGIT;
            PatternGroups[index1].CharCount = 1;
            checked { ++index1; }
            patternType = LikeOperator.PatternType.DIGIT;
            break;
          case '*':
          case '＊':
            if (patternType != LikeOperator.PatternType.STAR)
            {
              patternType = LikeOperator.PatternType.STAR;
              PatternGroups[index1].PatType = LikeOperator.PatternType.STAR;
              PGIndexForLastAsterisk = index1;
              checked { ++index1; }
              break;
            }
            break;
          case '?':
          case '？':
            if (patternType == LikeOperator.PatternType.ANYCHAR)
            {
              LikeOperator.PatternGroup[] patternGroupArray = PatternGroups;
              int index2 = checked (index1 - 1);
              int index3 = index2;
              patternGroupArray[index3].CharCount = checked (PatternGroups[index2].CharCount + 1);
              break;
            }
            PatternGroups[index1].PatType = LikeOperator.PatternType.ANYCHAR;
            PatternGroups[index1].CharCount = 1;
            checked { ++index1; }
            patternType = LikeOperator.PatternType.ANYCHAR;
            break;
          case '[':
          case '［':
            bool SeenNot = false;
            List<LikeOperator.Range> RangeList = new List<LikeOperator.Range>();
            if (!LikeOperator.ValidateRangePattern(Pattern, PatternLength, ref PatternIndex, PatternLigatureInfo, Comparer, Options, ref SeenNot, ref RangeList))
            {
              PatternError = true;
              return;
            }
            if (RangeList.Count != 0)
            {
              patternType = !SeenNot ? LikeOperator.PatternType.INCLIST : LikeOperator.PatternType.EXCLIST;
              PatternGroups[index1].PatType = patternType;
              PatternGroups[index1].CharCount = 1;
              PatternGroups[index1].RangeList = RangeList;
              checked { ++index1; }
              break;
            }
            break;
          default:
            int num2 = PatternIndex;
            int num3 = PatternIndex;
            if (num3 >= PatternLength)
              num3 = checked (PatternLength - 1);
            if (patternType == LikeOperator.PatternType.STRING)
            {
              LikeOperator.PatternGroup[] patternGroupArray = PatternGroups;
              int index2 = checked (index1 - 1);
              int index3 = index2;
              patternGroupArray[index3].CharCount = checked (PatternGroups[index2].CharCount + 1);
              PatternGroups[checked (index1 - 1)].StringPatternEnd = num3;
              break;
            }
            PatternGroups[index1].PatType = LikeOperator.PatternType.STRING;
            PatternGroups[index1].CharCount = 1;
            PatternGroups[index1].StringPatternStart = num2;
            PatternGroups[index1].StringPatternEnd = num3;
            checked { ++index1; }
            patternType = LikeOperator.PatternType.STRING;
            break;
        }
        checked { ++PatternIndex; }
      }
      while (PatternIndex < PatternLength);
      PatternGroups[index1].PatType = LikeOperator.PatternType.NONE;
      PatternGroups[index1].MinSourceIndex = SourceLength;
      int num4 = SourceLength;
      while (index1 > 0)
      {
        switch (PatternGroups[index1].PatType)
        {
          case LikeOperator.PatternType.STRING:
            checked { num4 -= PatternGroups[index1].CharCount; }
            break;
          case LikeOperator.PatternType.EXCLIST:
          case LikeOperator.PatternType.INCLIST:
            checked { --num4; }
            break;
          case LikeOperator.PatternType.DIGIT:
          case LikeOperator.PatternType.ANYCHAR:
            checked { num4 -= PatternGroups[index1].CharCount; }
            break;
        }
        PatternGroups[index1].MaxSourceIndex = num4;
        checked { --index1; }
      }
    }

    private static void MatchAsterisk(string Source, int SourceLength, int SourceIndex, LikeOperator.LigatureInfo[] SourceLigatureInfo, string Pattern, int PatternLength, int PatternIndex, LikeOperator.LigatureInfo[] PattternLigatureInfo, ref bool Mismatch, ref bool PatternError, CompareInfo Comparer, CompareOptions Options)
    {
      Mismatch = false;
      PatternError = false;
      if (PatternIndex >= PatternLength)
        return;
      LikeOperator.PatternGroup[] PatternGroups = (LikeOperator.PatternGroup[]) null;
      int PGIndexForLastAsterisk=0;
      LikeOperator.BuildPatternGroups(Source, SourceLength, ref SourceIndex, SourceLigatureInfo, Pattern, PatternLength, ref PatternIndex, PattternLigatureInfo, ref PatternError, ref PGIndexForLastAsterisk, Comparer, Options, ref PatternGroups);
      if (PatternError)
        return;
      if (PatternGroups[checked (PGIndexForLastAsterisk + 1)].PatType != LikeOperator.PatternType.NONE)
      {
        int num1 = SourceIndex;
        int index1 = checked (PGIndexForLastAsterisk + 1);
        int CharsToSubtract=0;
        do
        {
          checked { CharsToSubtract += PatternGroups[index1].CharCount; }
          checked { ++index1; }
        }
        while (PatternGroups[index1].PatType != LikeOperator.PatternType.NONE);
        SourceIndex = SourceLength;
        LikeOperator.SubtractChars(Source, SourceLength, ref SourceIndex, CharsToSubtract, SourceLigatureInfo, Options);
        LikeOperator.MatchAsterisk(Source, SourceLength, SourceIndex, SourceLigatureInfo, Pattern, PattternLigatureInfo, PatternGroups, PGIndexForLastAsterisk, ref Mismatch, ref PatternError, Comparer, Options);
        if (PatternError || Mismatch)
          return;
        SourceLength = PatternGroups[checked (PGIndexForLastAsterisk + 1)].StartIndexOfPossibleMatch;
        if (SourceLength <= 0)
          return;
        PatternGroups[index1].MaxSourceIndex = SourceLength;
        PatternGroups[index1].MinSourceIndex = SourceLength;
        PatternGroups[index1].StartIndexOfPossibleMatch = 0;
        PatternGroups[checked (PGIndexForLastAsterisk + 1)] = PatternGroups[index1];
        PatternGroups[PGIndexForLastAsterisk].MinSourceIndex = 0;
        PatternGroups[PGIndexForLastAsterisk].StartIndexOfPossibleMatch = 0;
        int index2 = checked (PGIndexForLastAsterisk + 1);
        int num2 = SourceLength;
        while (index2 > 0)
        {
          switch (PatternGroups[index2].PatType)
          {
            case LikeOperator.PatternType.STRING:
              checked { num2 -= PatternGroups[index2].CharCount; }
              break;
            case LikeOperator.PatternType.EXCLIST:
            case LikeOperator.PatternType.INCLIST:
              checked { --num2; }
              break;
            case LikeOperator.PatternType.DIGIT:
            case LikeOperator.PatternType.ANYCHAR:
              checked { num2 -= PatternGroups[index2].CharCount; }
              break;
          }
          PatternGroups[index2].MaxSourceIndex = num2;
          checked { --index2; }
        }
        SourceIndex = num1;
      }
      LikeOperator.MatchAsterisk(Source, SourceLength, SourceIndex, SourceLigatureInfo, Pattern, PattternLigatureInfo, PatternGroups, 0, ref Mismatch, ref PatternError, Comparer, Options);
    }

    private static void MatchAsterisk(string Source, int SourceLength, int SourceIndex, LikeOperator.LigatureInfo[] SourceLigatureInfo, string Pattern, LikeOperator.LigatureInfo[] PatternLigatureInfo, LikeOperator.PatternGroup[] PatternGroups, int PGIndex, ref bool Mismatch, ref bool PatternError, CompareInfo Comparer, CompareOptions Options)
    {
      int index1 = PGIndex;
      int num1 = SourceIndex;
      int index2 = -1;
      int index3 = -1;
      PatternGroups[PGIndex].MinSourceIndex = SourceIndex;
      PatternGroups[PGIndex].StartIndexOfPossibleMatch = SourceIndex;
      checked { ++PGIndex; }
      while (true)
      {
        LikeOperator.PatternGroup patternGroup = PatternGroups[PGIndex];
        switch (patternGroup.PatType)
        {
          case LikeOperator.PatternType.STRING:
            if (SourceIndex <= patternGroup.MaxSourceIndex)
            {
              PatternGroups[PGIndex].StartIndexOfPossibleMatch = SourceIndex;
              int stringPatternStart = patternGroup.StringPatternStart;
              int num2 = 0;
              int LeftEnd = SourceIndex;
              bool flag = true;
              do
              {
                int num3 = LikeOperator.CompareChars(Source, SourceLength, LeftEnd, ref LeftEnd, SourceLigatureInfo, Pattern, checked (patternGroup.StringPatternEnd + 1), stringPatternStart, ref stringPatternStart, PatternLigatureInfo, Comparer, Options, false, false);
                if (flag)
                {
                  flag = false;
                  num2 = checked (LeftEnd + 1);
                }
                if (num3 != 0)
                {
                  SourceIndex = num2;
                  index1 = checked (PGIndex - 1);
                  num1 = SourceIndex;
                  goto case LikeOperator.PatternType.STRING;
                }
                else
                {
                  checked { ++stringPatternStart; }
                  checked { ++LeftEnd; }
                  if (stringPatternStart > patternGroup.StringPatternEnd)
                  {
                    SourceIndex = LeftEnd;
                    goto default;
                  }
                }
              }
              while (LeftEnd < SourceLength);
              goto label_12;
            }
            else
              goto label_3;
          case LikeOperator.PatternType.EXCLIST:
          case LikeOperator.PatternType.INCLIST:
            while (SourceIndex <= patternGroup.MaxSourceIndex)
            {
              PatternGroups[PGIndex].StartIndexOfPossibleMatch = SourceIndex;
              if (!LikeOperator.MatchRangeAfterAsterisk(Source, SourceLength, ref SourceIndex, SourceLigatureInfo, Pattern, PatternLigatureInfo, patternGroup, Comparer, Options))
              {
                index1 = checked (PGIndex - 1);
                num1 = SourceIndex;
              }
              else
                goto default;
            }
            goto label_21;
          case LikeOperator.PatternType.DIGIT:
            if (SourceIndex <= patternGroup.MaxSourceIndex)
            {
              PatternGroups[PGIndex].StartIndexOfPossibleMatch = SourceIndex;
              int num2 = 1;
              int charCount = patternGroup.CharCount;
              int num3 = num2;
              while (num3 <= charCount)
              {
                char c = Source[SourceIndex];
                checked { ++SourceIndex; }
                if (!char.IsDigit(c))
                {
                  index1 = checked (PGIndex - 1);
                  num1 = SourceIndex;
                  goto case LikeOperator.PatternType.DIGIT;
                }
                else
                  checked { ++num3; }
              }
              goto default;
            }
            else
              goto label_14;
          case LikeOperator.PatternType.ANYCHAR:
            if (SourceIndex <= patternGroup.MaxSourceIndex)
            {
              PatternGroups[PGIndex].StartIndexOfPossibleMatch = SourceIndex;
              int num2 = 1;
              int charCount = patternGroup.CharCount;
              int num3 = num2;
              while (num3 <= charCount)
              {
                if (SourceIndex >= SourceLength)
                {
                  Mismatch = true;
                  return;
                }
                LikeOperator.SkipToEndOfExpandedChar(SourceLigatureInfo, SourceLength, ref SourceIndex);
                checked { ++SourceIndex; }
                checked { ++num3; }
              }
              goto default;
            }
            else
              goto label_25;
          case LikeOperator.PatternType.STAR:
            PatternGroups[PGIndex].StartIndexOfPossibleMatch = SourceIndex;
            patternGroup.MinSourceIndex = SourceIndex;
            if (PatternGroups[index1].PatType != LikeOperator.PatternType.STAR)
            {
              if (SourceIndex <= patternGroup.MaxSourceIndex)
                break;
              goto label_36;
            }
            else
              goto label_40;
          case LikeOperator.PatternType.NONE:
            PatternGroups[PGIndex].StartIndexOfPossibleMatch = patternGroup.MaxSourceIndex;
            if (SourceIndex < patternGroup.MaxSourceIndex)
            {
              index1 = checked (PGIndex - 1);
              num1 = patternGroup.MaxSourceIndex;
            }
            if (PatternGroups[index1].PatType == LikeOperator.PatternType.STAR || PatternGroups[index1].PatType == LikeOperator.PatternType.NONE)
              goto label_48;
            else
              break;
          default:
            if (PGIndex == index1)
            {
              if (SourceIndex == num1)
              {
                SourceIndex = PatternGroups[index2].MinSourceIndex;
                PGIndex = index2;
                index1 = index2;
                continue;
              }
              if (SourceIndex < num1)
              {
                LikeOperator.PatternGroup[] patternGroupArray = PatternGroups;
                int index4 = index3;
                int index5 = index4;
                patternGroupArray[index5].MinSourceIndex = checked (PatternGroups[index4].MinSourceIndex + 1);
                SourceIndex = PatternGroups[index3].MinSourceIndex;
                PGIndex = checked (index3 + 1);
                continue;
              }
              checked { ++PGIndex; }
              index1 = index3;
              continue;
            }
            checked { ++PGIndex; }
            continue;
        }
        index2 = PGIndex;
        SourceIndex = num1;
        PGIndex = index1;
        do
        {
          LikeOperator.SubtractChars(Source, SourceLength, ref SourceIndex, PatternGroups[PGIndex].CharCount, SourceLigatureInfo, Options);
          checked { --PGIndex; }
        }
        while (PatternGroups[PGIndex].PatType != LikeOperator.PatternType.STAR);
        SourceIndex = Math.Max(SourceIndex, checked (PatternGroups[PGIndex].MinSourceIndex + 1));
        PatternGroups[PGIndex].MinSourceIndex = SourceIndex;
        index3 = PGIndex;
label_40:
        checked { ++PGIndex; }
      }
label_3:
      Mismatch = true;
      return;
label_12:
      Mismatch = true;
      return;
label_14:
      Mismatch = true;
      return;
label_21:
      Mismatch = true;
      return;
label_25:
      Mismatch = true;
      return;
label_48:
      return;
label_36:
      Mismatch = true;
    }

        private static List<LikeOperator.Range>.Enumerator InitEnumerator()
        {
            List<LikeOperator.Range> Result = new List<LikeOperator.Range>();
            return Result.GetEnumerator();
        }

        private static bool MatchRangeAfterAsterisk(string Source, int SourceLength, ref int SourceIndex, LikeOperator.LigatureInfo[] SourceLigatureInfo, string Pattern, LikeOperator.LigatureInfo[] PatternLigatureInfo, LikeOperator.PatternGroup PG, CompareInfo Comparer, CompareOptions Options)
    {
      List<LikeOperator.Range> rangeList = PG.RangeList;
      int num1 = SourceIndex;
      bool flag = false;
            List<LikeOperator.Range>.Enumerator enumerator = Ported.VisualBasic.CompilerServices.OverloadResolution.InitEnumerator<LikeOperator.Range>();
      try
      {
        enumerator = rangeList.GetEnumerator();
        while (enumerator.MoveNext())
        {
          LikeOperator.Range current = enumerator.Current;
          int num2 = 1;
          int num3;
          if (PatternLigatureInfo != null && PatternLigatureInfo[current.Start].Kind == LikeOperator.CharKind.ExpandedChar1)
          {
            string Left = Source;
            int LeftLength = SourceLength;
            int LeftStart = SourceIndex;
            ref int local1 = ref num1;
            LikeOperator.LigatureInfo[] LeftLigatureInfo = SourceLigatureInfo;
            string Right = Pattern;
            int RightLength = checked (current.Start + current.StartLength);
            int start = current.Start;
            num3 = 0;
            ref int local2 = ref num3;
            LikeOperator.LigatureInfo[] RightLigatureInfo = PatternLigatureInfo;
            CompareInfo Comparer1 = Comparer;
            int num4 = (int) Options;
            int num5 = 1;
            int num6 = 0;
            if (LikeOperator.CompareChars(Left, LeftLength, LeftStart, ref local1, LeftLigatureInfo, Right, RightLength, start, ref local2, RightLigatureInfo, Comparer1, (CompareOptions) num4, num5 != 0, num6 != 0) == 0)
            {
              flag = true;
              break;
            }
          }
          string Left1 = Source;
          int LeftLength1 = SourceLength;
          int LeftStart1 = SourceIndex;
          ref int local3 = ref num1;
          LikeOperator.LigatureInfo[] LeftLigatureInfo1 = SourceLigatureInfo;
          string Right1 = Pattern;
          int RightLength1 = checked (current.Start + current.StartLength);
          int start1 = current.Start;
          num3 = 0;
          ref int local4 = ref num3;
          LikeOperator.LigatureInfo[] RightLigatureInfo1 = PatternLigatureInfo;
          CompareInfo Comparer2 = Comparer;
          int num7 = (int) Options;
          int num8 = 0;
          int num9 = 1;
          int num10 = LikeOperator.CompareChars(Left1, LeftLength1, LeftStart1, ref local3, LeftLigatureInfo1, Right1, RightLength1, start1, ref local4, RightLigatureInfo1, Comparer2, (CompareOptions) num7, num8 != 0, num9 != 0);
          if (num10 > 0 && current.End >= 0)
          {
            string Left2 = Source;
            int LeftLength2 = SourceLength;
            int LeftStart2 = SourceIndex;
            ref int local1 = ref num1;
            LikeOperator.LigatureInfo[] LeftLigatureInfo2 = SourceLigatureInfo;
            string Right2 = Pattern;
            int RightLength2 = checked (current.End + current.EndLength);
            int end = current.End;
            num3 = 0;
            ref int local2 = ref num3;
            LikeOperator.LigatureInfo[] RightLigatureInfo2 = PatternLigatureInfo;
            CompareInfo Comparer1 = Comparer;
            int num4 = (int) Options;
            int num5 = 0;
            int num6 = 1;
            num2 = LikeOperator.CompareChars(Left2, LeftLength2, LeftStart2, ref local1, LeftLigatureInfo2, Right2, RightLength2, end, ref local2, RightLigatureInfo2, Comparer1, (CompareOptions) num4, num5 != 0, num6 != 0);
          }
          if (num10 == 0 || num10 > 0 && num2 <= 0)
          {
            flag = true;
            break;
          }
        }
      }
      finally
      {
        enumerator.Dispose();
      }
      if (PG.PatType == LikeOperator.PatternType.EXCLIST)
        flag = !flag;
      SourceIndex = checked (num1 + 1);
      return flag;
    }

    private static void SubtractChars(string Input, int InputLength, ref int Current, int CharsToSubtract, LikeOperator.LigatureInfo[] InputLigatureInfo, CompareOptions Options)
    {
      if (Options == CompareOptions.Ordinal)
      {
        checked { Current -= CharsToSubtract; }
        if (Current >= 0)
          return;
        Current = 0;
      }
      else
      {
        int num1 = 1;
        int num2 = CharsToSubtract;
        int num3 = num1;
        while (num3 <= num2)
        {
          LikeOperator.SubtractOneCharInTextCompareMode(Input, InputLength, ref Current, InputLigatureInfo, Options);
          if (Current < 0)
          {
            Current = 0;
            break;
          }
          checked { ++num3; }
        }
      }
    }

    private static void SubtractOneCharInTextCompareMode(string Input, int InputLength, ref int Current, LikeOperator.LigatureInfo[] InputLigatureInfo, CompareOptions Options)
    {
      if (Current >= InputLength)
        checked { --Current; }
      else if (InputLigatureInfo != null && InputLigatureInfo[Current].Kind == LikeOperator.CharKind.ExpandedChar2)
        checked { Current -= 2; }
      else
        checked { --Current; }
    }

    private enum Ligatures
    {
      Invalid = 0,
      Min = 198, // 0x000000C6
      aeUpper = 198, // 0x000000C6
      thUpper = 222, // 0x000000DE
      ssBeta = 223, // 0x000000DF
      szBeta = 223, // 0x000000DF
      ae = 230, // 0x000000E6
      th = 254, // 0x000000FE
      oeUpper = 338, // 0x00000152
      Max = 339, // 0x00000153
      oe = 339, // 0x00000153
    }

    private enum CharKind
    {
      None,
      ExpandedChar1,
      ExpandedChar2,
    }

    private struct LigatureInfo
    {
      internal LikeOperator.CharKind Kind;
      internal char CharBeforeExpansion;
    }

    private enum PatternType
    {
      STRING,
      EXCLIST,
      INCLIST,
      DIGIT,
      ANYCHAR,
      STAR,
      NONE,
    }

    private struct PatternGroup
    {
      internal LikeOperator.PatternType PatType;
      internal int MaxSourceIndex;
      internal int CharCount;
      internal int StringPatternStart;
      internal int StringPatternEnd;
      internal int MinSourceIndex;
      internal List<LikeOperator.Range> RangeList;
      public int StartIndexOfPossibleMatch;
    }

    private struct Range
    {
      internal int Start;
      internal int StartLength;
      internal int End;
      internal int EndLength;
    }
  }
}

*/