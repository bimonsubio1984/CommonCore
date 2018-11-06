// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.AssemblyData
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System.Collections;
using System.ComponentModel;
using System.IO;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal sealed class AssemblyData
  {
    public ArrayList m_Files;
    internal FileSystemInfo[] m_DirFiles;
    internal int m_DirNextFileIndex;
    internal FileAttributes m_DirAttributes;

    public AssemblyData()
    {
      ArrayList arrayList = new ArrayList(256);
      object obj = (object) null;
      int num = 0;
      do
      {
        arrayList.Add(obj);
        checked { ++num; }
      }
      while (num <= (int) byte.MaxValue);
      this.m_Files = arrayList;
    }

    internal VB6File GetChannelObj(int lChannel)
    {
      return lChannel >= this.m_Files.Count ? (VB6File) (object) null : (VB6File) this.m_Files[lChannel];
    }

    internal void SetChannelObj(int lChannel, VB6File oFile)
    {
      if (this.m_Files == null)
        this.m_Files = new ArrayList(256);
      if (oFile == null)
      {
        ((VB6File) this.m_Files[lChannel])?.CloseFile();
        this.m_Files[lChannel] = (object) null;
      }
      else
      {
        object obj = (object) oFile;
        this.m_Files[lChannel] = obj;
      }
    }
  }
}
