// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ComClassAttribute
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="ComClassAttribute" /> attribute instructs the compiler to add metadata that allows a class to be exposed as a COM object.</summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public sealed class ComClassAttribute : Attribute
  {
    private string m_ClassID;
    private string m_InterfaceID;
    private string m_EventID;
    private bool m_InterfaceShadows;

    /// <summary>Initializes a new instance of the <see langword="ComClassAttribute" /> class.</summary>
    public ComClassAttribute()
    {
      this.m_InterfaceShadows = false;
    }

    /// <summary>Initializes a new instance of the <see langword="ComClassAttribute" /> class.</summary>
    /// <param name="_ClassID">Initializes the value of the <see langword="ClassID" /> property that is used to uniquely identify a class.</param>
    public ComClassAttribute(string _ClassID)
    {
      this.m_InterfaceShadows = false;
      this.m_ClassID = _ClassID;
    }

    /// <summary>Initializes a new instance of the <see langword="ComClassAttribute" /> class.</summary>
    /// <param name="_ClassID">Initializes the value of the <see langword="ClassID" /> property that is used to uniquely identify a class.</param>
    /// <param name="_InterfaceID">Initializes the value of the <see langword="InterfaceID" /> property that is used to uniquely identify an interface.</param>
    public ComClassAttribute(string _ClassID, string _InterfaceID)
    {
      this.m_InterfaceShadows = false;
      this.m_ClassID = _ClassID;
      this.m_InterfaceID = _InterfaceID;
    }

    /// <summary>Initializes a new instance of the <see langword="ComClassAttribute" /> class.</summary>
    /// <param name="_ClassID">Initializes the value of the <see langword="ClassID" /> property that is used to uniquely identify a class.</param>
    /// <param name="_InterfaceID">Initializes the value of the <see langword="InterfaceID" /> property that is used to uniquely identify an interface.</param>
    /// <param name="_EventId">Initializes the value of the <see langword="EventID" /> property that is used to uniquely identify an event.</param>
    public ComClassAttribute(string _ClassID, string _InterfaceID, string _EventId)
    {
      this.m_InterfaceShadows = false;
      this.m_ClassID = _ClassID;
      this.m_InterfaceID = _InterfaceID;
      this.m_EventID = _EventId;
    }

    /// <summary>Gets a class ID used to uniquely identify a class.</summary>
    /// <returns>Read-only. A string that can be used by the compiler to uniquely identify the class when a COM object is created.</returns>
    public string ClassID
    {
      get
      {
        return this.m_ClassID;
      }
    }

    /// <summary>Gets an interface ID used to uniquely identify an interface.</summary>
    /// <returns>Read-only. A string that can be used by the compiler to uniquely identify an interface for the class when a COM object is created.</returns>
    public string InterfaceID
    {
      get
      {
        return this.m_InterfaceID;
      }
    }

    /// <summary>Gets an event ID used to uniquely identify an event.</summary>
    /// <returns>Read only. A string that can be used by the compiler to uniquely identify an event for the class when a COM object is created.</returns>
    public string EventID
    {
      get
      {
        return this.m_EventID;
      }
    }

    /// <summary>Indicates that the COM interface name shadows another member of the class or base class.</summary>
    /// <returns>A <see langword="Boolean" /> value that indicates that the COM interface name shadows another member of the class or base class.</returns>
    public bool InterfaceShadows
    {
      get
      {
        return this.m_InterfaceShadows;
      }
      set
      {
        this.m_InterfaceShadows = value;
      }
    }
  }
}
