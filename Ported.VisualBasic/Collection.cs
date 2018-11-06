// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Collection
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace Ported.VisualBasic
{
  /// <summary>A Visual Basic <see langword="Collection" /> is an ordered set of items that can be referred to as a unit.</summary>
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof (Collection.CollectionDebugView))]
  [Serializable]
  public sealed class Collection : ICollection, IList, ISerializable, IDeserializationCallback
  {
    private const string SERIALIZATIONKEY_KEYS = "Keys";
    private const string SERIALIZATIONKEY_KEYSCOUNT = "KeysCount";
    private const string SERIALIZATIONKEY_VALUES = "Values";
    private const string SERIALIZATIONKEY_CULTUREINFO = "CultureInfo";
    private SerializationInfo m_DeserializationInfo;
    private Dictionary<string, Collection.Node> m_KeyedNodesHash;
    private Collection.FastList m_ItemsList;
    private ArrayList m_Iterators;
    private CultureInfo m_CultureInfo;

    /// <summary>Creates and returns a new Visual Basic <see cref="T:Ported.VisualBasic.Collection" /> object.</summary>
    public Collection()
    {
      this.Initialize(Utils.GetCultureInfo(), 0);
    }

    /// <summary>Adds an element to a <see langword="Collection" /> object.</summary>
    /// <param name="Item">Required. An object of any type that specifies the element to add to the collection.</param>
    /// <param name="Key">Optional. A unique <see langword="String" /> expression that specifies a key string that can be used instead of a positional index to access this new element in the collection.</param>
    /// <param name="Before">Optional. An expression that specifies a relative position in the collection. The element to be added is placed in the collection before the element identified by the <paramref name="Before" /> argument. If <paramref name="Before" /> is a numeric expression, it must be a number from 1 through the value of the collection's <see cref="P:Ported.VisualBasic.Collection.Count" /> property. If <paramref name="Before" /> is a <see langword="String" /> expression, it must correspond to the key string specified when the element being referred to was added to the collection. You cannot specify both <paramref name="Before" /> and <paramref name="After" />.</param>
    /// <param name="After">Optional. An expression that specifies a relative position in the collection. The element to be added is placed in the collection after the element identified by the <paramref name="After" /> argument. If <paramref name="After" /> is a numeric expression, it must be a number from 1 through the value of the collection's <see langword="Count" /> property. If <paramref name="After" /> is a <see langword="String" /> expression, it must correspond to the key string specified when the element referred to was added to the collection. You cannot specify both <paramref name="Before" /> and <paramref name="After" />.</param>
    public void Add(object Item, string Key = null, object Before = null, object After = null)
    {
      if (Before != null && After != null)
        throw new ArgumentException(Utils.GetResourceString("Collection_BeforeAfterExclusive"));
      Collection.Node node = new Collection.Node(Key, Item);
      if (Key != null)
      {
        try
        {
          this.m_KeyedNodesHash.Add(Key, node);
        }
        catch (ArgumentException ex)
        {
          throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Collection_DuplicateKey")), 457);
        }
      }
      try
      {
        if (Before == null && After == null)
          this.m_ItemsList.Add(node);
        else if (Before != null)
        {
          string key = Before as string;
          if (key != null)
          {
            Collection.Node NodeToInsertBefore = (Collection.Node) null;
            if (!this.m_KeyedNodesHash.TryGetValue(key, out NodeToInsertBefore))
              throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
              {
                nameof (Before)
              }));
            this.m_ItemsList.InsertBefore(node, NodeToInsertBefore);
          }
          else
            this.m_ItemsList.Insert(checked (Conversions.ToInteger(Before) - 1), node);
        }
        else
        {
          string key = After as string;
          if (key != null)
          {
            Collection.Node NodeToInsertAfter = (Collection.Node) null;
            if (!this.m_KeyedNodesHash.TryGetValue(key, out NodeToInsertAfter))
              throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
              {
                nameof (After)
              }));
            this.m_ItemsList.InsertAfter(node, NodeToInsertAfter);
          }
          else
            this.m_ItemsList.Insert(Conversions.ToInteger(After), node);
        }
      }
      catch (OutOfMemoryException ex)
      {
        throw;
      }
      catch (ThreadAbortException ex)
      {
        throw;
      }
      catch (StackOverflowException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        if (Key != null)
          this.m_KeyedNodesHash.Remove(Key);
        throw;
      }
      this.AdjustEnumeratorsOnNodeInserted(node);
    }

    /// <summary>Deletes all elements of a Visual Basic <see langword="Collection" /> object.</summary>
    public void Clear()
    {
      this.m_KeyedNodesHash.Clear();
      this.m_ItemsList.Clear();
      int index = checked (this.m_Iterators.Count - 1);
      while (index >= 0)
      {
        WeakReference iterator = (WeakReference) this.m_Iterators[index];
        if (iterator.IsAlive)
          ((ForEachEnum) iterator.Target)?.AdjustOnListCleared();
        else
          this.m_Iterators.RemoveAt(index);
        checked { --index; }
      }
    }

    /// <summary>Returns a <see langword="Boolean" /> value indicating whether a Visual Basic <see langword="Collection" /> object contains an element with a specific key.</summary>
    /// <param name="Key">Required. A <see langword="String" /> expression that specifies the key for which to search the elements of the collection.</param>
    /// <returns>Returns a <see langword="Boolean" /> value indicating whether a Visual Basic <see langword="Collection" /> object contains an element with a specific key.</returns>
    public bool Contains(string Key)
    {
      if (Key == null)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Key)
        }));
      return this.m_KeyedNodesHash.ContainsKey(Key);
    }

    /// <summary>Removes an element from a <see langword="Collection" /> object.</summary>
    /// <param name="Key">A unique <see langword="String" /> expression that specifies a key string that can be used, instead of a positional index, to access an element of the collection. <paramref name="Key" /> must correspond to the <paramref name="Key" /> argument specified when the element was added to the collection.</param>
    public void Remove(string Key)
    {
      Collection.Node node = (Collection.Node) null;
      if (this.m_KeyedNodesHash.TryGetValue(Key, out node))
      {
        this.AdjustEnumeratorsOnNodeRemoved(node);
        this.m_KeyedNodesHash.Remove(Key);
        this.m_ItemsList.RemoveNode(node);
        node.m_Prev = (Collection.Node) null;
        node.m_Next = (Collection.Node) null;
      }
      else
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Key)
        }));
    }

    /// <summary>Removes an element from a <see langword="Collection" /> object.</summary>
    /// <param name="Index">A numeric expression that specifies the position of an element of the collection. <paramref name="Index" /> must be a number from 1 through the value of the collection's <see cref="P:Ported.VisualBasic.Collection.Count" /> property.</param>
    public void Remove(int Index)
    {
      this.IndexCheck(Index);
      Collection.Node RemovedNode = this.m_ItemsList.RemoveAt(checked (Index - 1));
      this.AdjustEnumeratorsOnNodeRemoved(RemovedNode);
      if (RemovedNode.m_Key != null)
        this.m_KeyedNodesHash.Remove(RemovedNode.m_Key);
      RemovedNode.m_Prev = (Collection.Node) null;
      RemovedNode.m_Next = (Collection.Node) null;
    }

    /// <summary>Returns a specific element of a <see langword="Collection" /> object either by position or by key. Read-only.</summary>
    /// <param name="Index">(A) A numeric expression that specifies the position of an element of the collection. <paramref name="Index" /> must be a number from 1 through the value of the collection's <see cref="P:Ported.VisualBasic.Collection.Count" /> property. Or (B) An <see langword="Object" /> expression that specifies the position or key string of an element of the collection.</param>
    /// <returns>Returns a specific element of a <see langword="Collection" /> object either by position or by key. Read-only.</returns>
    public object this[int Index]
    {
      get
      {
        this.IndexCheck(Index);
        return this.m_ItemsList.get_Item(checked (Index - 1)).m_Value;
      }
    }

        object IList.this[int index]
        {
            get
            {
                return this.m_ItemsList.get_Item(index).m_Value;
            }
            set
            {
                this.m_ItemsList.get_Item(index).m_Value = value;
            }
        }

        /// <summary>Returns a specific element of a <see langword="Collection" /> object either by position or by key. Read-only.</summary>
        /// <param name="Key">A unique <see langword="String" /> expression that specifies a key string that can be used, instead of a positional index, to access an element of the collection. <paramref name="Key" /> must correspond to the <paramref name="Key" /> argument specified when the element was added to the collection.</param>
        /// <returns>Returns a specific element of a <see langword="Collection" /> object either by position or by key. Read-only.</returns>
        public object this[string Key]
    {
      get
      {
        if (Key == null)
          throw new IndexOutOfRangeException(Utils.GetResourceString("Argument_CollectionIndex"));
        Collection.Node node = (Collection.Node) null;
        if (!this.m_KeyedNodesHash.TryGetValue(Key, out node))
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            "Index"
          }));
        return node.m_Value;
      }
    }

    /// <summary>Returns a specific element of a <see langword="Collection" /> object either by position or by key. Read-only.</summary>
    /// <param name="Index">(A) A numeric expression that specifies the position of an element of the collection. <paramref name="Index" /> must be a number from 1 through the value of the collection's <see cref="P:Ported.VisualBasic.Collection.Count" /> property. Or (B) An <see langword="Object" /> expression that specifies the position or key string of an element of the collection.</param>
    /// <returns>Returns a specific element of a <see langword="Collection" /> object either by position or by key. Read-only.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public object this[object Index]
    {
      get
      {
        if (!(Index is string) && !(Index is char))
        {
          if (!(Index is char[]))
          {
            int integer;
            try
            {
              integer = Conversions.ToInteger(Index);
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
                nameof (Index)
              }));
            }
            return this[integer];
          }
        }
        return this[Conversions.ToString(Index)];
      }
    }

    /// <summary>Returns an <see langword="Integer" /> containing the number of elements in a collection. Read-only.</summary>
    /// <returns>Returns an <see langword="Integer" /> containing the number of elements in a collection. Read-only.</returns>
    public int Count
    {
      get
      {
        return this.m_ItemsList.Count();
      }
    }

    /// <summary>Returns a reference to an enumerator object, which is used to iterate over a <see cref="T:Ported.VisualBasic.Collection" /> object.</summary>
    /// <returns>Returns a reference to an enumerator object, which is used to iterate over a <see cref="T:Ported.VisualBasic.Collection" /> object.</returns>
    public IEnumerator GetEnumerator()
    {
      int index = checked (this.m_Iterators.Count - 1);
      while (index >= 0)
      {
        if (!((WeakReference) this.m_Iterators[index]).IsAlive)
          this.m_Iterators.RemoveAt(index);
        checked { --index; }
      }
      ForEachEnum forEachEnum = new ForEachEnum(this);
      WeakReference weakReference = new WeakReference((object) forEachEnum);
      forEachEnum.WeakRef = weakReference;
      this.m_Iterators.Add((object) weakReference);
      return (IEnumerator) forEachEnum;
    }

    internal void RemoveIterator(WeakReference weakref)
    {
      this.m_Iterators.Remove((object) weakref);
    }

    internal void AddIterator(WeakReference weakref)
    {
      this.m_Iterators.Add((object) weakref);
    }

    internal Collection.Node GetFirstListNode()
    {
      return this.m_ItemsList.GetFirstListNode();
    }

    private void Initialize(CultureInfo CultureInfo, int StartingHashCapacity = 0)
    {
      this.m_KeyedNodesHash = StartingHashCapacity <= 0 ? new Dictionary<string, Collection.Node>((IEqualityComparer<string>) StringComparer.Create(CultureInfo, true)) : new Dictionary<string, Collection.Node>(StartingHashCapacity, (IEqualityComparer<string>) StringComparer.Create(CultureInfo, true));
      this.m_ItemsList = new Collection.FastList();
      this.m_Iterators = new ArrayList();
      this.m_CultureInfo = CultureInfo;
    }

    private void AdjustEnumeratorsOnNodeInserted(Collection.Node NewNode)
    {
      this.AdjustEnumeratorsHelper(NewNode, ForEachEnum.AdjustIndexType.Insert);
    }

    private void AdjustEnumeratorsOnNodeRemoved(Collection.Node RemovedNode)
    {
      this.AdjustEnumeratorsHelper(RemovedNode, ForEachEnum.AdjustIndexType.Remove);
    }

    private void AdjustEnumeratorsHelper(Collection.Node NewOrRemovedNode, ForEachEnum.AdjustIndexType Type)
    {
      int index = checked (this.m_Iterators.Count - 1);
      while (index >= 0)
      {
        WeakReference iterator = (WeakReference) this.m_Iterators[index];
        if (iterator.IsAlive)
          ((ForEachEnum) iterator.Target)?.Adjust(NewOrRemovedNode, Type);
        else
          this.m_Iterators.RemoveAt(index);
        checked { --index; }
      }
    }

    private void IndexCheck(int Index)
    {
      if (Index < 1 || Index > this.m_ItemsList.Count())
        throw new IndexOutOfRangeException(Utils.GetResourceString("Argument_CollectionIndex"));
    }

    private Collection.FastList InternalItemsList()
    {
      return this.m_ItemsList;
    }

    private Collection(SerializationInfo info, StreamingContext context)
    {
      this.m_DeserializationInfo = info;
    }

    /// <summary>Returns the data needed to serialize the <see cref="T:Ported.VisualBasic.Collection" /> object. Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface.</summary>
    /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:Ported.VisualBasic.Collection" /> object. </param>
    /// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:Ported.VisualBasic.Collection" /> object. </param>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      string[] strArray = new string[checked (this.Count - 1 + 1)];
      object[] objArray = new object[checked (this.Count - 1 + 1)];
      Collection.Node node = this.GetFirstListNode();
      int num = 0;
      for (; node != null; node = node.m_Next)
      {
        if (node.m_Key != null)
          checked { ++num; }
        int index=0;
        strArray[index] = node.m_Key;
        objArray[index] = node.m_Value;
        checked { ++index; }
      }
      info.AddValue("Keys", (object) strArray, typeof (string[]));
      info.AddValue("KeysCount", (object) num, typeof (int));
      info.AddValue("Values", (object) objArray, typeof (object[]));
      info.AddValue("CultureInfo", (object) this.m_CultureInfo);
    }

    /// <summary>Runs after the entire <see cref="T:Ported.VisualBasic.Collection" /> object graph has been deserialized. Implements the <see cref="T:System.Runtime.Serialization.IDeserializationCallback" /> interface.</summary>
    /// <param name="sender">The object that initiated the callback. </param>
    void IDeserializationCallback.OnDeserialization(object sender)
    {
      try
      {
        CultureInfo CultureInfo = (CultureInfo) this.m_DeserializationInfo.GetValue("CultureInfo", typeof (CultureInfo));
        if (CultureInfo == null)
          throw new SerializationException(Utils.GetResourceString("Serialization_MissingCultureInfo"));
        string[] strArray = (string[]) this.m_DeserializationInfo.GetValue("Keys", typeof (string[]));
        object[] objArray = (object[]) this.m_DeserializationInfo.GetValue("Values", typeof (object[]));
        if (strArray == null)
          throw new SerializationException(Utils.GetResourceString("Serialization_MissingKeys"));
        if (objArray == null)
          throw new SerializationException(Utils.GetResourceString("Serialization_MissingValues"));
        if (strArray.Length != objArray.Length)
          throw new SerializationException(Utils.GetResourceString("Serialization_KeyValueDifferentSizes"));
        int StartingHashCapacity = this.m_DeserializationInfo.GetInt32("KeysCount");
        if (StartingHashCapacity < 0 || StartingHashCapacity > strArray.Length)
          StartingHashCapacity = 0;
        this.Initialize(CultureInfo, StartingHashCapacity);
        int num1 = 0;
        int num2 = checked (strArray.Length - 1);
        int index = num1;
        while (index <= num2)
        {
          this.Add(objArray[index], strArray[index], (object) null, (object) null);
          checked { ++index; }
        }
        this.m_DeserializationInfo = (SerializationInfo) null;
      }
      finally
      {
        if (this.m_DeserializationInfo != null)
        {
          this.m_DeserializationInfo = (SerializationInfo) null;
          this.Initialize(Utils.GetCultureInfo(), 0);
        }
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    int ICollection.Count
    {
      get
      {
        return this.m_ItemsList.Count();
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (array)
        }));
      if (array.Rank != 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_RankEQOne1", new string[1]
        {
          nameof (array)
        }));
      if (index < 0 || checked (array.Length - index) < this.Count)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (index)
        }));
      object[] objArray = array as object[];
      if (objArray != null)
      {
        int num = 1;
        int count = this.Count;
        int index1 = num;
        while (index1 <= count)
        {
          objArray[checked (index + index1 - 1)] = this[index1];
          checked { ++index1; }
        }
      }
      else
      {
        int num = 1;
        int count = this.Count;
        int index1 = num;
        while (index1 <= count)
        {
          array.SetValue(this[index1], checked (index + index1 - 1));
          checked { ++index1; }
        }
      }
    }

    int IList.Add(object value)
    {
      this.Add(value, (string) null, (object) null, (object) null);
      return checked (this.m_ItemsList.Count() - 1);
    }

    void IList.Insert(int index, object value)
    {
      Collection.Node node = new Collection.Node((string) null, value);
      this.m_ItemsList.Insert(index, node);
      this.AdjustEnumeratorsOnNodeInserted(node);
    }

    void IList.RemoveAt(int index)
    {
      Collection.Node RemovedNode = this.m_ItemsList.RemoveAt(index);
      this.AdjustEnumeratorsOnNodeRemoved(RemovedNode);
      if (RemovedNode.m_Key != null)
        this.m_KeyedNodesHash.Remove(RemovedNode.m_Key);
      RemovedNode.m_Prev = (Collection.Node) null;
      RemovedNode.m_Next = (Collection.Node) null;
    }

    void IList.Remove(object value)
    {
      int index = ((IList)this).IndexOf(value);
      if (index == -1)
        return;
      ((IList)this).RemoveAt(index);
    }

    void IList.Clear()
    {
      this.Clear();
    }

    

    bool IList.Contains(object value)
    {
      return ((IList)this).IndexOf(value) != -1;
    }

    int IList.IndexOf(object value)
    {
      return this.m_ItemsList.IndexOfValue(value);
    }

    internal sealed class Node
    {
      internal object m_Value;
      internal string m_Key;
      internal Collection.Node m_Next;
      internal Collection.Node m_Prev;

      internal Node(string Key, object Value)
      {
        this.m_Value = Value;
        this.m_Key = Key;
      }
    }

    internal sealed class CollectionDebugView
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private Collection m_InstanceBeingWatched;

      public CollectionDebugView(Collection RealClass)
      {
        this.m_InstanceBeingWatched = RealClass;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public object[] Items
      {
        get
        {
          int count = this.m_InstanceBeingWatched.Count;
          if (count == 0)
            return (object[]) null;
          object[] objArray = new object[checked (count + 1)];
          objArray[0] = (object) Utils.GetResourceString("EmptyPlaceHolderMessage");
          int num1 = 1;
          int num2 = count;
          int index = num1;
          while (index <= num2)
          {
            Collection.Node node = this.m_InstanceBeingWatched.InternalItemsList().get_Item(checked (index - 1));
            objArray[index] = (object) new Collection.KeyValuePair((object) node.m_Key, node.m_Value);
            checked { ++index; }
          }
          return objArray;
        }
      }
    }

    private sealed class FastList
    {
      private Collection.Node m_StartOfList;
      private Collection.Node m_EndOfList;
      private int m_Count;

      internal FastList()
      {
        this.m_Count = 0;
      }

      internal void Add(Collection.Node Node)
      {
        if (this.m_StartOfList == null)
        {
          this.m_StartOfList = Node;
        }
        else
        {
          this.m_EndOfList.m_Next = Node;
          Node.m_Prev = this.m_EndOfList;
        }
        this.m_EndOfList = Node;
        checked { ++this.m_Count; }
      }

      internal int IndexOfValue(object Value)
      {
        Collection.Node node = this.m_StartOfList;
        int num = 0;
        while (node != null)
        {
          if (this.DataIsEqual(node.m_Value, Value))
            return num;
          node = node.m_Next;
          checked { ++num; }
        }
        return -1;
      }

      internal void RemoveNode(Collection.Node NodeToBeDeleted)
      {
        this.DeleteNode(NodeToBeDeleted, NodeToBeDeleted.m_Prev);
      }

      internal Collection.Node RemoveAt(int Index)
      {
        Collection.Node NodeToBeDeleted = this.m_StartOfList;
        int num = 0;
        Collection.Node PrevNode = (Collection.Node) null;
        while (num < Index && NodeToBeDeleted != null)
        {
          PrevNode = NodeToBeDeleted;
          NodeToBeDeleted = NodeToBeDeleted.m_Next;
          checked { ++num; }
        }
        if (NodeToBeDeleted == null)
          throw new ArgumentOutOfRangeException(nameof (Index));
        this.DeleteNode(NodeToBeDeleted, PrevNode);
        return NodeToBeDeleted;
      }

      internal int Count()
      {
        return this.m_Count;
      }

      internal void Clear()
      {
        this.m_StartOfList = (Collection.Node) null;
        this.m_EndOfList = (Collection.Node) null;
        this.m_Count = 0;
      }

      internal Collection.Node get_Item(int Index)
      {
        int Index1 = Index;
        Collection.Node node = (Collection.Node) null;
        ref Collection.Node local = ref node;
        Collection.Node nodeAtIndex = this.GetNodeAtIndex(Index1, ref local);
        if (nodeAtIndex == null)
          throw new ArgumentOutOfRangeException(nameof (Index));
        return nodeAtIndex;
      }

      internal void Insert(int Index, Collection.Node Node)
      {
        Collection.Node PrevNode = (Collection.Node) null;
        if (Index < 0 || Index > this.m_Count)
          throw new ArgumentOutOfRangeException(nameof (Index));
        Collection.Node nodeAtIndex = this.GetNodeAtIndex(Index, ref PrevNode);
        this.Insert(Node, PrevNode, nodeAtIndex);
      }

      internal void InsertBefore(Collection.Node Node, Collection.Node NodeToInsertBefore)
      {
        this.Insert(Node, NodeToInsertBefore.m_Prev, NodeToInsertBefore);
      }

      internal void InsertAfter(Collection.Node Node, Collection.Node NodeToInsertAfter)
      {
        this.Insert(Node, NodeToInsertAfter, NodeToInsertAfter.m_Next);
      }

      internal Collection.Node GetFirstListNode()
      {
        return this.m_StartOfList;
      }

      private bool DataIsEqual(object obj1, object obj2)
      {
        if (obj1 == obj2)
          return true;
        if (obj1.GetType() == obj2.GetType())
          return object.Equals(obj1, obj2);
        return false;
      }

      private Collection.Node GetNodeAtIndex(int Index, ref Collection.Node PrevNode )
      {
        Collection.Node node = this.m_StartOfList;
        int num = 0;
        PrevNode = (Collection.Node) null;
        while (num < Index && node != null)
        {
          PrevNode = node;
          node = node.m_Next;
          checked { ++num; }
        }
        return node;
      }

      private void Insert(Collection.Node Node, Collection.Node PrevNode, Collection.Node CurrentNode)
      {
        Node.m_Next = CurrentNode;
        if (CurrentNode != null)
          CurrentNode.m_Prev = Node;
        if (PrevNode == null)
        {
          this.m_StartOfList = Node;
        }
        else
        {
          PrevNode.m_Next = Node;
          Node.m_Prev = PrevNode;
        }
        if (Node.m_Next == null)
          this.m_EndOfList = Node;
        checked { ++this.m_Count; }
      }

      private void DeleteNode(Collection.Node NodeToBeDeleted, Collection.Node PrevNode)
      {
        if (PrevNode == null)
        {
          this.m_StartOfList = this.m_StartOfList.m_Next;
          if (this.m_StartOfList == null)
            this.m_EndOfList = (Collection.Node) null;
          else
            this.m_StartOfList.m_Prev = (Collection.Node) null;
        }
        else
        {
          PrevNode.m_Next = NodeToBeDeleted.m_Next;
          if (PrevNode.m_Next == null)
            this.m_EndOfList = PrevNode;
          else
            PrevNode.m_Next.m_Prev = PrevNode;
        }
        checked { --this.m_Count; }
      }
    }

    private struct KeyValuePair
    {
      private object m_Key;
      private object m_Value;

      internal KeyValuePair(object NewKey, object NewValue)
      {
        this = new Collection.KeyValuePair();
        this.m_Key = NewKey;
        this.m_Value = NewValue;
      }

      public object Key
      {
        get
        {
          return this.m_Key;
        }
      }

      public object Value
      {
        get
        {
          return this.m_Value;
        }
      }
    }
  }
}
