// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.ConversionResolution
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Ported.VisualBasic.CompilerServices
{
  internal class ConversionResolution
  {
    private static readonly ConversionResolution.ConversionClass[][] ConversionTable = new ConversionResolution.ConversionClass[19][]
    {
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Widening
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.None,
        ConversionResolution.ConversionClass.Identity,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Bad
      },
      new ConversionResolution.ConversionClass[19]
      {
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Widening,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Narrowing,
        ConversionResolution.ConversionClass.Bad,
        ConversionResolution.ConversionClass.Identity
      }
    };
    internal static readonly int[] NumericSpecificityRank = new int[19];
    internal static readonly TypeCode[][] ForLoopWidestTypeCode;

    private ConversionResolution()
    {
    }

    static ConversionResolution()
    {
      ConversionResolution.NumericSpecificityRank[6] = 1;
      ConversionResolution.NumericSpecificityRank[5] = 2;
      ConversionResolution.NumericSpecificityRank[7] = 3;
      ConversionResolution.NumericSpecificityRank[8] = 4;
      ConversionResolution.NumericSpecificityRank[9] = 5;
      ConversionResolution.NumericSpecificityRank[10] = 6;
      ConversionResolution.NumericSpecificityRank[11] = 7;
      ConversionResolution.NumericSpecificityRank[12] = 8;
      ConversionResolution.NumericSpecificityRank[15] = 9;
      ConversionResolution.NumericSpecificityRank[13] = 10;
      ConversionResolution.NumericSpecificityRank[14] = 11;
      ConversionResolution.ForLoopWidestTypeCode = new TypeCode[19][]
      {
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Int16,
          TypeCode.Empty,
          TypeCode.SByte,
          TypeCode.Int16,
          TypeCode.Int16,
          TypeCode.Int32,
          TypeCode.Int32,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Decimal,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.SByte,
          TypeCode.Empty,
          TypeCode.SByte,
          TypeCode.Int16,
          TypeCode.Int16,
          TypeCode.Int32,
          TypeCode.Int32,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Decimal,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Int16,
          TypeCode.Empty,
          TypeCode.Int16,
          TypeCode.Byte,
          TypeCode.Int16,
          TypeCode.UInt16,
          TypeCode.Int32,
          TypeCode.UInt32,
          TypeCode.Int64,
          TypeCode.UInt64,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Int16,
          TypeCode.Empty,
          TypeCode.Int16,
          TypeCode.Int16,
          TypeCode.Int16,
          TypeCode.Int32,
          TypeCode.Int32,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Decimal,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Int32,
          TypeCode.Empty,
          TypeCode.Int32,
          TypeCode.UInt16,
          TypeCode.Int32,
          TypeCode.UInt16,
          TypeCode.Int32,
          TypeCode.UInt32,
          TypeCode.Int64,
          TypeCode.UInt64,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Int32,
          TypeCode.Empty,
          TypeCode.Int32,
          TypeCode.Int32,
          TypeCode.Int32,
          TypeCode.Int32,
          TypeCode.Int32,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Decimal,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Int64,
          TypeCode.Empty,
          TypeCode.Int64,
          TypeCode.UInt32,
          TypeCode.Int64,
          TypeCode.UInt32,
          TypeCode.Int64,
          TypeCode.UInt32,
          TypeCode.Int64,
          TypeCode.UInt64,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Int64,
          TypeCode.Empty,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Int64,
          TypeCode.Decimal,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Decimal,
          TypeCode.UInt64,
          TypeCode.Decimal,
          TypeCode.UInt64,
          TypeCode.Decimal,
          TypeCode.UInt64,
          TypeCode.Decimal,
          TypeCode.UInt64,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Single,
          TypeCode.Empty,
          TypeCode.Single,
          TypeCode.Single,
          TypeCode.Single,
          TypeCode.Single,
          TypeCode.Single,
          TypeCode.Single,
          TypeCode.Single,
          TypeCode.Single,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Single,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Double,
          TypeCode.Empty,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Double,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Decimal,
          TypeCode.Decimal,
          TypeCode.Decimal,
          TypeCode.Decimal,
          TypeCode.Decimal,
          TypeCode.Decimal,
          TypeCode.Decimal,
          TypeCode.Decimal,
          TypeCode.Single,
          TypeCode.Double,
          TypeCode.Decimal,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        },
        new TypeCode[19]
        {
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty,
          TypeCode.Empty
        }
      };
    }

    [Conditional("DEBUG")]
    private static void VerifyTypeCodeEnum()
    {
    }

    internal static ConversionResolution.ConversionClass ClassifyConversion(Type TargetType, Type SourceType, ref Symbols.Method OperatorMethod)
    {
      ConversionResolution.ConversionClass conversionClass = ConversionResolution.ClassifyPredefinedConversion(TargetType, SourceType);
      if (conversionClass == ConversionResolution.ConversionClass.None && !Symbols.IsInterface(SourceType) && !Symbols.IsInterface(TargetType) && ((Symbols.IsClassOrValueType(SourceType) || Symbols.IsClassOrValueType(TargetType)) && (!Symbols.IsIntrinsicType(SourceType) || !Symbols.IsIntrinsicType(TargetType))))
        conversionClass = ConversionResolution.ClassifyUserDefinedConversion(TargetType, SourceType, ref OperatorMethod);
      return conversionClass;
    }

    
    
    internal static ConversionResolution.ConversionClass ClassifyIntrinsicConversion(TypeCode TargetTypeCode, TypeCode SourceTypeCode)
    {
      return ConversionResolution.ConversionTable[(int) TargetTypeCode][(int) SourceTypeCode];
    }

    
    
    internal static ConversionResolution.ConversionClass ClassifyPredefinedCLRConversion(Type TargetType, Type SourceType)
    {
      if (TargetType == SourceType)
        return ConversionResolution.ConversionClass.Identity;
      if (Symbols.IsRootObjectType(TargetType) || Symbols.IsOrInheritsFrom(SourceType, TargetType))
        return ConversionResolution.ConversionClass.Widening;
      if (Symbols.IsRootObjectType(SourceType) || Symbols.IsOrInheritsFrom(TargetType, SourceType))
        return ConversionResolution.ConversionClass.Narrowing;
      if (Symbols.IsInterface(SourceType))
        return Symbols.IsClass(TargetType) || Symbols.IsArrayType(TargetType) || (Symbols.IsGenericParameter(TargetType) || Symbols.IsInterface(TargetType)) || (!Symbols.IsValueType(TargetType) || Symbols.Implements(TargetType, SourceType)) ? ConversionResolution.ConversionClass.Narrowing : ConversionResolution.ConversionClass.None;
      if (Symbols.IsInterface(TargetType))
      {
        if (Symbols.IsArrayType(SourceType))
          return ConversionResolution.ClassifyCLRArrayToInterfaceConversion(TargetType, SourceType);
        if (Symbols.IsValueType(SourceType))
          return Symbols.Implements(SourceType, TargetType) ? ConversionResolution.ConversionClass.Widening : ConversionResolution.ConversionClass.None;
        if (Symbols.IsClass(SourceType))
          return Symbols.Implements(SourceType, TargetType) ? ConversionResolution.ConversionClass.Widening : ConversionResolution.ConversionClass.Narrowing;
      }
      if (Symbols.IsEnum(SourceType) || Symbols.IsEnum(TargetType))
      {
        if (Symbols.GetTypeCode(SourceType) != Symbols.GetTypeCode(TargetType))
          return ConversionResolution.ConversionClass.None;
        return Symbols.IsEnum(TargetType) ? ConversionResolution.ConversionClass.Narrowing : ConversionResolution.ConversionClass.Widening;
      }
      if (Symbols.IsGenericParameter(SourceType))
      {
        if (!Symbols.IsClassOrInterface(TargetType))
          return ConversionResolution.ConversionClass.None;
        Type[] interfaceConstraints = Symbols.GetInterfaceConstraints(SourceType);
        int index = 0;
        while (index < interfaceConstraints.Length)
        {
          Type SourceType1 = interfaceConstraints[index];
          switch (ConversionResolution.ClassifyPredefinedConversion(TargetType, SourceType1))
          {
            case ConversionResolution.ConversionClass.Identity:
            case ConversionResolution.ConversionClass.Widening:
              return ConversionResolution.ConversionClass.Widening;
            default:
              checked { ++index; }
              continue;
          }
        }
        Type classConstraint = Symbols.GetClassConstraint(SourceType);
        if (classConstraint != null)
        {
          switch (ConversionResolution.ClassifyPredefinedConversion(TargetType, classConstraint))
          {
            case ConversionResolution.ConversionClass.Identity:
            case ConversionResolution.ConversionClass.Widening:
              return ConversionResolution.ConversionClass.Widening;
          }
        }

        //return Interaction.IIf<ConversionResolution.ConversionClass>(Symbols.IsInterface(TargetType), ConversionResolution.ConversionClass.Narrowing, ConversionResolution.ConversionClass.None);
        if (Symbols.IsInterface(TargetType))
        {
          return ConversionResolution.ConversionClass.Narrowing;
        }
        else
        {
          return ConversionResolution.ConversionClass.None;
        }
        
      }
      if (Symbols.IsGenericParameter(TargetType))
      {
        Type classConstraint = Symbols.GetClassConstraint(TargetType);
        return classConstraint != null && Symbols.IsOrInheritsFrom(classConstraint, SourceType) ? ConversionResolution.ConversionClass.Narrowing : ConversionResolution.ConversionClass.None;
      }
      if (Symbols.IsArrayType(SourceType) && Symbols.IsArrayType(TargetType) && SourceType.GetArrayRank() == TargetType.GetArrayRank())
        return ConversionResolution.ClassifyCLRConversionForArrayElementTypes(TargetType.GetElementType(), SourceType.GetElementType());
      return ConversionResolution.ConversionClass.None;
    }

    
    
    private static ConversionResolution.ConversionClass ClassifyCLRArrayToInterfaceConversion(Type TargetInterface, Type SourceArrayType)
    {
      if (Symbols.Implements(SourceArrayType, TargetInterface))
        return ConversionResolution.ConversionClass.Widening;
      if (SourceArrayType.GetArrayRank() > 1)
        return ConversionResolution.ConversionClass.Narrowing;
      Type elementType = SourceArrayType.GetElementType();
      ConversionResolution.ConversionClass conversionClass = ConversionResolution.ConversionClass.None;
      if (TargetInterface.IsGenericType && !TargetInterface.IsGenericTypeDefinition)
      {
        Type genericTypeDefinition = TargetInterface.GetGenericTypeDefinition();
        if (genericTypeDefinition == typeof (IList<>) || genericTypeDefinition == typeof (ICollection<>) || genericTypeDefinition == typeof (IEnumerable<>))
          conversionClass = ConversionResolution.ClassifyCLRConversionForArrayElementTypes(TargetInterface.GetGenericArguments()[0], elementType);
      }
      else
        conversionClass = ConversionResolution.ClassifyPredefinedCLRConversion(TargetInterface, typeof (IList<>).MakeGenericType(elementType));
      return conversionClass == ConversionResolution.ConversionClass.Identity || conversionClass == ConversionResolution.ConversionClass.Widening ? ConversionResolution.ConversionClass.Widening : ConversionResolution.ConversionClass.Narrowing;
    }

    private static ConversionResolution.ConversionClass ClassifyCLRConversionForArrayElementTypes(Type TargetElementType, Type SourceElementType)
    {
      if (Symbols.IsReferenceType(SourceElementType) && Symbols.IsReferenceType(TargetElementType) || Symbols.IsValueType(SourceElementType) && Symbols.IsValueType(TargetElementType))
        return ConversionResolution.ClassifyPredefinedCLRConversion(TargetElementType, SourceElementType);
      if (Symbols.IsGenericParameter(SourceElementType) && Symbols.IsGenericParameter(TargetElementType))
      {
        if (SourceElementType == TargetElementType)
          return ConversionResolution.ConversionClass.Identity;
        if (Symbols.IsReferenceType(SourceElementType) && Symbols.IsOrInheritsFrom(SourceElementType, TargetElementType))
          return ConversionResolution.ConversionClass.Widening;
        if (Symbols.IsReferenceType(TargetElementType) && Symbols.IsOrInheritsFrom(TargetElementType, SourceElementType))
          return ConversionResolution.ConversionClass.Narrowing;
      }
      return ConversionResolution.ConversionClass.None;
    }

    internal static ConversionResolution.ConversionClass ClassifyPredefinedConversion(Type TargetType, Type SourceType)
    {
      if (TargetType == SourceType)
        return ConversionResolution.ConversionClass.Identity;
      TypeCode typeCode1 = Symbols.GetTypeCode(SourceType);
      TypeCode typeCode2 = Symbols.GetTypeCode(TargetType);
      if (Symbols.IsIntrinsicType(typeCode1) && Symbols.IsIntrinsicType(typeCode2))
      {
        if (Symbols.IsEnum(TargetType) && Symbols.IsIntegralType(typeCode1) && Symbols.IsIntegralType(typeCode2))
          return ConversionResolution.ConversionClass.Narrowing;
        if (typeCode1 == typeCode2 && Symbols.IsEnum(SourceType))
          return ConversionResolution.ConversionClass.Widening;
        return ConversionResolution.ClassifyIntrinsicConversion(typeCode2, typeCode1);
      }
      if (Symbols.IsCharArrayRankOne(SourceType) && Symbols.IsStringType(TargetType))
        return ConversionResolution.ConversionClass.Widening;
      if (Symbols.IsCharArrayRankOne(TargetType) && Symbols.IsStringType(SourceType))
        return ConversionResolution.ConversionClass.Narrowing;
      return ConversionResolution.ClassifyPredefinedCLRConversion(TargetType, SourceType);
    }

    private static List<Symbols.Method> CollectConversionOperators(Type TargetType, Type SourceType, ref bool FoundTargetTypeOperators, ref bool FoundSourceTypeOperators)
    {
      if (Symbols.IsIntrinsicType(TargetType))
        TargetType = typeof (object);
      if (Symbols.IsIntrinsicType(SourceType))
        SourceType = typeof (object);
      List<Symbols.Method> methodList1 = Operators.CollectOperators(Symbols.UserDefinedOperator.Widen, TargetType, SourceType, ref FoundTargetTypeOperators, ref FoundSourceTypeOperators);
      List<Symbols.Method> methodList2 = Operators.CollectOperators(Symbols.UserDefinedOperator.Narrow, TargetType, SourceType, ref FoundTargetTypeOperators, ref FoundSourceTypeOperators);
      methodList1.AddRange((IEnumerable<Symbols.Method>) methodList2);
      return methodList1;
    }

    private static bool Encompasses(Type Larger, Type Smaller)
    {
      switch (ConversionResolution.ClassifyPredefinedConversion(Larger, Smaller))
      {
        case ConversionResolution.ConversionClass.Identity:
        case ConversionResolution.ConversionClass.Widening:
          return true;
        default:
          return false;
      }
    }

    private static bool NotEncompasses(Type Larger, Type Smaller)
    {
      switch (ConversionResolution.ClassifyPredefinedConversion(Larger, Smaller))
      {
        case ConversionResolution.ConversionClass.Identity:
        case ConversionResolution.ConversionClass.Narrowing:
          return true;
        default:
          return false;
      }
    }

    private static Type MostEncompassing(List<Type> Types)
    {
      Type type1 = Types[0];
      int num1 = 1;
      int num2 = checked (Types.Count - 1);
      int index = num1;
      while (index <= num2)
      {
        Type type2 = Types[index];
        if (ConversionResolution.Encompasses(type2, type1))
          type1 = type2;
        else if (!ConversionResolution.Encompasses(type1, type2))
          return (Type) null;
        checked { ++index; }
      }
      return type1;
    }

    private static Type MostEncompassed(List<Type> Types)
    {
      Type type1 = Types[0];
      int num1 = 1;
      int num2 = checked (Types.Count - 1);
      int index = num1;
      while (index <= num2)
      {
        Type type2 = Types[index];
        if (ConversionResolution.Encompasses(type1, type2))
          type1 = type2;
        else if (!ConversionResolution.Encompasses(type2, type1))
          return (Type) null;
        checked { ++index; }
      }
      return type1;
    }

    private static void FindBestMatch(Type TargetType, Type SourceType, List<Symbols.Method> SearchList, List<Symbols.Method> ResultList, ref bool GenericMembersExistInList)
    {
      List<Symbols.Method>.Enumerator enumerator = Ported.VisualBasic.CompilerServices.OverloadResolution.InitEnumerator<Symbols.Method>(); ;
      try
      {
        enumerator = SearchList.GetEnumerator();
        while (enumerator.MoveNext())
        {
          Symbols.Method current = enumerator.Current;
          MethodBase methodBase = current.AsMethod();
          Type parameterType = methodBase.GetParameters()[0].ParameterType;
          Type returnType = ((MethodInfo) methodBase).ReturnType;
          if (parameterType == SourceType && returnType == TargetType)
            ConversionResolution.InsertInOperatorListIfLessGenericThanExisting(current, ResultList, ref GenericMembersExistInList);
        }
      }
      finally
      {
        enumerator.Dispose();
      }
    }

    private static void InsertInOperatorListIfLessGenericThanExisting(Symbols.Method OperatorToInsert, List<Symbols.Method> OperatorList, ref bool GenericMembersExistInList)
    {
      if (Symbols.IsGeneric(OperatorToInsert.DeclaringType))
        GenericMembersExistInList = true;
      if (GenericMembersExistInList)
      {
        int index = checked (OperatorList.Count - 1);
        while (index >= 0)
        {
          Symbols.Method Left = OperatorList[index];
          Symbols.Method method = OverloadResolution.LeastGenericProcedure(Left, OperatorToInsert);
          if ((object) method == (object) Left)
            return;
          if ((object) method != null)
            OperatorList.Remove(Left);
          checked { index += -1; }
        }
      }
      OperatorList.Add(OperatorToInsert);
    }

    private static List<Symbols.Method> ResolveConversion(Type TargetType, Type SourceType, List<Symbols.Method> OperatorSet, bool WideningOnly, ref bool ResolutionIsAmbiguous)
    {
      ResolutionIsAmbiguous = false;
      Type SourceType1 = (Type) null;
      Type TargetType1 = (Type) null;
      bool GenericMembersExistInList = false;
      List<Symbols.Method> methodList = new List<Symbols.Method>(OperatorSet.Count);
      List<Symbols.Method> SearchList = new List<Symbols.Method>(OperatorSet.Count);
      List<Type> Types1 = new List<Type>(OperatorSet.Count);
      List<Type> Types2 = new List<Type>(OperatorSet.Count);
      List<Type> Types3 = (List<Type>) null;
      List<Type> Types4 = (List<Type>) null;
      if (!WideningOnly)
      {
        Types3 = new List<Type>(OperatorSet.Count);
        Types4 = new List<Type>(OperatorSet.Count);
      }
      List<Symbols.Method>.Enumerator enumerator=Ported.VisualBasic.CompilerServices.OverloadResolution.InitEnumerator<Symbols.Method>();
      try
      {
        enumerator = OperatorSet.GetEnumerator();
        while (enumerator.MoveNext())
        {
          Symbols.Method current = enumerator.Current;
          MethodBase Method = current.AsMethod();
          if (WideningOnly)
          {
            if (Symbols.IsNarrowingConversionOperator(Method))
              break;
          }
          Type parameterType = Method.GetParameters()[0].ParameterType;
          Type returnType = ((MethodInfo) Method).ReturnType;
          if (!Symbols.IsGeneric(Method) && !Symbols.IsGeneric(Method.DeclaringType) || ConversionResolution.ClassifyPredefinedConversion(returnType, parameterType) == ConversionResolution.ConversionClass.None)
          {
            if (parameterType == SourceType && returnType == TargetType)
              ConversionResolution.InsertInOperatorListIfLessGenericThanExisting(current, methodList, ref GenericMembersExistInList);
            else if (methodList.Count == 0)
            {
              if (ConversionResolution.Encompasses(parameterType, SourceType) && ConversionResolution.Encompasses(TargetType, returnType))
              {
                SearchList.Add(current);
                if (parameterType == SourceType)
                  SourceType1 = parameterType;
                else
                  Types1.Add(parameterType);
                if (returnType == TargetType)
                  TargetType1 = returnType;
                else
                  Types2.Add(returnType);
              }
              else if (!WideningOnly && ConversionResolution.Encompasses(parameterType, SourceType) && ConversionResolution.NotEncompasses(TargetType, returnType))
              {
                SearchList.Add(current);
                if (parameterType == SourceType)
                  SourceType1 = parameterType;
                else
                  Types1.Add(parameterType);
                if (returnType == TargetType)
                  TargetType1 = returnType;
                else
                  Types4.Add(returnType);
              }
              else if (!WideningOnly && ConversionResolution.NotEncompasses(parameterType, SourceType) && ConversionResolution.NotEncompasses(TargetType, returnType))
              {
                SearchList.Add(current);
                if (parameterType == SourceType)
                  SourceType1 = parameterType;
                else
                  Types3.Add(parameterType);
                if (returnType == TargetType)
                  TargetType1 = returnType;
                else
                  Types4.Add(returnType);
              }
            }
          }
        }
      }
      finally
      {
        enumerator.Dispose();
      }
      if (methodList.Count == 0 && SearchList.Count > 0)
      {
        if (SourceType1 == null)
          SourceType1 = Types1.Count <= 0 ? ConversionResolution.MostEncompassing(Types3) : ConversionResolution.MostEncompassed(Types1);
        if (TargetType1 == null)
          TargetType1 = Types2.Count <= 0 ? ConversionResolution.MostEncompassed(Types4) : ConversionResolution.MostEncompassing(Types2);
        if (SourceType1 == null || TargetType1 == null)
        {
          ResolutionIsAmbiguous = true;
          return new List<Symbols.Method>();
        }
        ConversionResolution.FindBestMatch(TargetType1, SourceType1, SearchList, methodList, ref GenericMembersExistInList);
      }
      if (methodList.Count > 1)
        ResolutionIsAmbiguous = true;
      return methodList;
    }

    internal static ConversionResolution.ConversionClass ClassifyUserDefinedConversion(Type TargetType, Type SourceType, ref Symbols.Method OperatorMethod)
    {
      lock (OperatorCaches.ConversionCache)
      {
        if (OperatorCaches.UnconvertibleTypeCache.Lookup(TargetType) && OperatorCaches.UnconvertibleTypeCache.Lookup(SourceType))
          return ConversionResolution.ConversionClass.None;
        ConversionResolution.ConversionClass Classification= ConversionResolution.ConversionClass.None;
        if (OperatorCaches.ConversionCache.Lookup(TargetType, SourceType, ref Classification, ref OperatorMethod))
          return Classification;
      }
      bool FoundTargetTypeOperators = false;
      bool FoundSourceTypeOperators = false;
      ConversionResolution.ConversionClass Classification1 = ConversionResolution.DoClassifyUserDefinedConversion(TargetType, SourceType, ref OperatorMethod, ref FoundTargetTypeOperators, ref FoundSourceTypeOperators);
      lock (OperatorCaches.ConversionCache)
      {
        if (!FoundTargetTypeOperators)
          OperatorCaches.UnconvertibleTypeCache.Insert(TargetType);
        if (!FoundSourceTypeOperators)
          OperatorCaches.UnconvertibleTypeCache.Insert(SourceType);
        if (!FoundTargetTypeOperators)
        {
          if (!FoundSourceTypeOperators)
            goto label_15;
        }
        OperatorCaches.ConversionCache.Insert(TargetType, SourceType, Classification1, OperatorMethod);
      }
label_15:
      return Classification1;
    }

    private static ConversionResolution.ConversionClass DoClassifyUserDefinedConversion(Type TargetType, Type SourceType, ref Symbols.Method OperatorMethod, ref bool FoundTargetTypeOperators, ref bool FoundSourceTypeOperators)
    {
      OperatorMethod = (Symbols.Method) null;
      List<Symbols.Method> OperatorSet = ConversionResolution.CollectConversionOperators(TargetType, SourceType, ref FoundTargetTypeOperators, ref FoundSourceTypeOperators);
      if (OperatorSet.Count == 0)
        return ConversionResolution.ConversionClass.None;
      bool ResolutionIsAmbiguous = false;
      List<Symbols.Method> methodList1 = ConversionResolution.ResolveConversion(TargetType, SourceType, OperatorSet, true, ref ResolutionIsAmbiguous);
      if (methodList1.Count == 1)
      {
        OperatorMethod = methodList1[0];
        OperatorMethod.ArgumentsValidated = true;
        return ConversionResolution.ConversionClass.Widening;
      }
      if (methodList1.Count == 0 && !ResolutionIsAmbiguous)
      {
        List<Symbols.Method> methodList2 = ConversionResolution.ResolveConversion(TargetType, SourceType, OperatorSet, false, ref ResolutionIsAmbiguous);
        if (methodList2.Count == 1)
        {
          OperatorMethod = methodList2[0];
          OperatorMethod.ArgumentsValidated = true;
          return ConversionResolution.ConversionClass.Narrowing;
        }
        if (methodList2.Count == 0)
          return ConversionResolution.ConversionClass.None;
      }
      return ConversionResolution.ConversionClass.Ambiguous;
    }

    internal enum ConversionClass : sbyte
    {
      Bad,
      Identity,
      Widening,
      Narrowing,
      None,
      Ambiguous,
    }
  }
}

