// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ForEachEnum
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.Collections;
using System.ComponentModel;

namespace Ported.VisualBasic
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal sealed class ForEachEnum : IEnumerator, IDisposable
  {
    private bool mDisposed;
    private Collection mCollectionObject;
    private Collection.Node mCurrent;
    private Collection.Node mNext;
    private bool mAtBeginning;
    internal WeakReference WeakRef;

    void IDisposable.Dispose()
    {
      if (!this.mDisposed)
      {
        this.mCollectionObject.RemoveIterator(this.WeakRef);
        this.mDisposed = true;
      }
      this.mCurrent = (Collection.Node) null;
      this.mNext = (Collection.Node) null;
    }

    public ForEachEnum(Collection coll)
    {
      this.mDisposed = false;
      this.mCollectionObject = coll;
      this.Reset();
    }

    public bool MoveNext()
    {
      if (this.mDisposed)
        return false;
      if (this.mAtBeginning)
      {
        this.mAtBeginning = false;
        this.mNext = this.mCollectionObject.GetFirstListNode();
      }
      if (this.mNext == null)
      {
        ((IDisposable)this).Dispose();
        return false;
      }
      this.mCurrent = this.mNext;
      if (this.mCurrent != null)
      {
        this.mNext = this.mCurrent.m_Next;
        return true;
      }
      ((IDisposable)this).Dispose();
      return false;
    }

    public void Reset()
    {
      if (this.mDisposed)
      {
        this.mCollectionObject.AddIterator(this.WeakRef);
        this.mDisposed = false;
      }
      this.mCurrent = (Collection.Node) null;
      this.mNext = (Collection.Node) null;
      this.mAtBeginning = true;
    }

    public object Current
    {
      get
      {
        if (this.mCurrent == null)
          return (object) null;
        return this.mCurrent.m_Value;
      }
    }

    public void Adjust(Collection.Node Node, ForEachEnum.AdjustIndexType Type)
    {
      if (Node == null || this.mDisposed)
        return;
      switch (Type)
      {
        case ForEachEnum.AdjustIndexType.Insert:
          if (this.mCurrent == null || Node != this.mCurrent.m_Next)
            break;
          this.mNext = Node;
          break;
        case ForEachEnum.AdjustIndexType.Remove:
          if (Node == this.mCurrent || Node != this.mNext)
            break;
          this.mNext = this.mNext.m_Next;
          break;
      }
    }

    internal void AdjustOnListCleared()
    {
      this.mNext = (Collection.Node) null;
    }

    internal enum AdjustIndexType
    {
      Insert,
      Remove,
    }
  }
}
