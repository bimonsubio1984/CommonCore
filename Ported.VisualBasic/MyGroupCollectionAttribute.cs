// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.MyGroupCollectionAttribute
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;

namespace Ported.VisualBasic
{
  /// <summary>This attribute supports <see langword="My.Forms" /> and <see langword="My.WebServices" /> in Visual Basic.</summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public sealed class MyGroupCollectionAttribute : Attribute
  {
    private string m_NameOfBaseTypeToCollect;
    private string m_NameOfCreateMethod;
    private string m_NameOfDisposeMethod;
    private string m_DefaultInstanceAlias;

    /// <summary>This attribute supports <see langword="My.Forms" /> and <see langword="My.WebServices" /> in Visual Basic.</summary>
    /// <param name="typeToCollect">
    /// <see langword="String" />. Initializes the <see cref="P:Ported.VisualBasic.MyGroupCollectionAttribute.MyGroupName" /> property. The compiler generates accessor properties for classes that derive from this type.</param>
    /// <param name="createInstanceMethodName">
    /// <see langword="String" />. Initializes the <see cref="P:Ported.VisualBasic.MyGroupCollectionAttribute.CreateMethod" /> property. Specifies the method in the class that creates the type's instances.</param>
    /// <param name="disposeInstanceMethodName">
    /// <see langword="String" />. Initializes the <see cref="P:Ported.VisualBasic.MyGroupCollectionAttribute.DisposeMethod" /> property. Specifies the method in the class that disposes of the type's instances.</param>
    /// <param name="defaultInstanceAlias">
    /// <see langword="String" />. Initializes the <see cref="P:Ported.VisualBasic.MyGroupCollectionAttribute.DefaultInstanceAlias" /> property. Specifies the name of the property that returns the default instance of the class.</param>
    public MyGroupCollectionAttribute(string typeToCollect, string createInstanceMethodName, string disposeInstanceMethodName, string defaultInstanceAlias)
    {
      this.m_NameOfBaseTypeToCollect = typeToCollect;
      this.m_NameOfCreateMethod = createInstanceMethodName;
      this.m_NameOfDisposeMethod = disposeInstanceMethodName;
      this.m_DefaultInstanceAlias = defaultInstanceAlias;
    }

    /// <summary>This property supports <see langword="My" /> in Visual Basic.</summary>
    /// <returns>Specifies the name of the type for which the compiler generates accessor properties.</returns>
    public string MyGroupName
    {
      get
      {
        return this.m_NameOfBaseTypeToCollect;
      }
    }

    /// <summary>This property supports <see langword="My" /> in Visual Basic.</summary>
    /// <returns>Specifies the method in the class that creates the type's instances.</returns>
    public string CreateMethod
    {
      get
      {
        return this.m_NameOfCreateMethod;
      }
    }

    /// <summary>This property supports <see langword="My" /> in Visual Basic.</summary>
    /// <returns>Specifies the method in the class that disposes of the type's instances.</returns>
    public string DisposeMethod
    {
      get
      {
        return this.m_NameOfDisposeMethod;
      }
    }

    /// <summary>This property supports <see langword="My" /> in Visual Basic.</summary>
    /// <returns>Specifies the name of the property that returns the default instance of the class.</returns>
    public string DefaultInstanceAlias
    {
      get
      {
        return this.m_DefaultInstanceAlias;
      }
    }
  }
}
