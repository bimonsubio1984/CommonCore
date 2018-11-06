// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.OperatorCaches
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll



using System;

namespace Ported.VisualBasic.CompilerServices
{
  internal class OperatorCaches
  {
    internal static readonly OperatorCaches.FixedList ConversionCache = new OperatorCaches.FixedList();
    internal static readonly OperatorCaches.FixedExistanceList UnconvertibleTypeCache = new OperatorCaches.FixedExistanceList();

    private OperatorCaches()
    {
    }

    internal sealed class FixedList
    {
      private readonly OperatorCaches.FixedList.Entry[] m_List;
      private readonly int m_Size;
      private int m_First;
      private int m_Last;
      private int m_Count;
      private const int DefaultSize = 50;

      internal FixedList()
        : this(50)
      {
      }

      internal FixedList(int Size)
      {
        this.m_Size = Size;
        this.m_List = new OperatorCaches.FixedList.Entry[checked (this.m_Size - 1 + 1)];
        int num1 = 0;
        int num2 = checked (this.m_Size - 2);
        int index1 = num1;
        while (index1 <= num2)
        {
          this.m_List[index1].Next = checked (index1 + 1);
          checked { ++index1; }
        }
        int index2 = checked (this.m_Size - 1);
        while (index2 >= 1)
        {
          this.m_List[index2].Previous = checked (index2 - 1);
          checked { index2 += -1; }
        }
        this.m_List[0].Previous = checked (this.m_Size - 1);
        this.m_Last = checked (this.m_Size - 1);
      }

      private void MoveToFront(int Item)
      {
        if (Item == this.m_First)
          return;
        int next = this.m_List[Item].Next;
        int previous = this.m_List[Item].Previous;
        this.m_List[previous].Next = next;
        this.m_List[next].Previous = previous;
        this.m_List[this.m_First].Previous = Item;
        this.m_List[this.m_Last].Next = Item;
        this.m_List[Item].Next = this.m_First;
        this.m_List[Item].Previous = this.m_Last;
        this.m_First = Item;
      }

      internal void Insert(Type TargetType, Type SourceType, ConversionResolution.ConversionClass Classification, Symbols.Method OperatorMethod)
      {
        if (this.m_Count < this.m_Size)
          checked { ++this.m_Count; }
        int last = this.m_Last;
        this.m_First = last;
        this.m_Last = this.m_List[this.m_Last].Previous;
        this.m_List[last].TargetType = TargetType;
        this.m_List[last].SourceType = SourceType;
        this.m_List[last].Classification = Classification;
        this.m_List[last].OperatorMethod = OperatorMethod;
      }

      internal bool Lookup(Type TargetType, Type SourceType, ref ConversionResolution.ConversionClass Classification, ref Symbols.Method OperatorMethod)
      {
        int index = this.m_First;
        int num = 0;
        while (num < this.m_Count)
        {
          if (TargetType == this.m_List[index].TargetType && SourceType == this.m_List[index].SourceType)
          {
            Classification = this.m_List[index].Classification;
            OperatorMethod = this.m_List[index].OperatorMethod;
            this.MoveToFront(index);
            return true;
          }
          index = this.m_List[index].Next;
          checked { ++num; }
        }
        Classification = ConversionResolution.ConversionClass.Bad;
        OperatorMethod = (Symbols.Method) null;
        return false;
      }

      private struct Entry
      {
        internal Type TargetType;
        internal Type SourceType;
        internal ConversionResolution.ConversionClass Classification;
        internal Symbols.Method OperatorMethod;
        internal int Next;
        internal int Previous;
      }
    }

    internal sealed class FixedExistanceList
    {
      private readonly OperatorCaches.FixedExistanceList.Entry[] m_List;
      private readonly int m_Size;
      private int m_First;
      private int m_Last;
      private int m_Count;
      private const int DefaultSize = 50;

      internal FixedExistanceList()
        : this(50)
      {
      }

      internal FixedExistanceList(int Size)
      {
        this.m_Size = Size;
        this.m_List = new OperatorCaches.FixedExistanceList.Entry[checked (this.m_Size - 1 + 1)];
        int num1 = 0;
        int num2 = checked (this.m_Size - 2);
        int index1 = num1;
        while (index1 <= num2)
        {
          this.m_List[index1].Next = checked (index1 + 1);
          checked { ++index1; }
        }
        int index2 = checked (this.m_Size - 1);
        while (index2 >= 1)
        {
          this.m_List[index2].Previous = checked (index2 - 1);
          checked { index2 += -1; }
        }
        this.m_List[0].Previous = checked (this.m_Size - 1);
        this.m_Last = checked (this.m_Size - 1);
      }

      private void MoveToFront(int Item)
      {
        if (Item == this.m_First)
          return;
        int next = this.m_List[Item].Next;
        int previous = this.m_List[Item].Previous;
        this.m_List[previous].Next = next;
        this.m_List[next].Previous = previous;
        this.m_List[this.m_First].Previous = Item;
        this.m_List[this.m_Last].Next = Item;
        this.m_List[Item].Next = this.m_First;
        this.m_List[Item].Previous = this.m_Last;
        this.m_First = Item;
      }

      internal void Insert(Type Type)
      {
        if (this.m_Count < this.m_Size)
          checked { ++this.m_Count; }
        int last = this.m_Last;
        this.m_First = last;
        this.m_Last = this.m_List[this.m_Last].Previous;
        this.m_List[last].Type = Type;
      }

      internal bool Lookup(Type Type)
      {
        int index = this.m_First;
        int num = 0;
        while (num < this.m_Count)
        {
          if (Type == this.m_List[index].Type)
          {
            this.MoveToFront(index);
            return true;
          }
          index = this.m_List[index].Next;
          checked { ++num; }
        }
        return false;
      }

      private struct Entry
      {
        internal Type Type;
        internal int Next;
        internal int Previous;
      }
    }
  }
}

