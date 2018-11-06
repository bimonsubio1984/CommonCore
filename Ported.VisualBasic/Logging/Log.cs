// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Logging.Log
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Ported.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Permissions;
using System.Text;

namespace Ported.VisualBasic.Logging
{
  /// <summary>Provides a property and methods for writing event and exception information to the application's log listeners.</summary>
  //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class Log
  {
    private static Dictionary<TraceEventType, int> m_IdHash = Log.InitializeIDHash();
    private Log.DefaultTraceSource m_TraceSource;
    private const string WINAPP_SOURCE_NAME = "DefaultSource";
    private const string DEFAULT_FILE_LOG_TRACE_LISTENER_NAME = "FileLog";

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.Logging.Log" /> class. </summary>
    public Log()
    {
      this.m_TraceSource = new Log.DefaultTraceSource("DefaultSource");
      if (!this.m_TraceSource.HasBeenConfigured)
        this.InitializeWithDefaultsSinceNoConfigExists();
      AppDomain.CurrentDomain.ProcessExit += new EventHandler(this.CloseOnProcessExit);
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.Logging.Log" /> class. </summary>
    /// <param name="name">
    /// <see cref="T:System.String" />. The name to give to the <see cref="P:Ported.VisualBasic.Logging.Log.TraceSource" /> property object.</param>
    public Log(string name)
    {
      this.m_TraceSource = new Log.DefaultTraceSource(name);
      if (this.m_TraceSource.HasBeenConfigured)
        return;
      this.InitializeWithDefaultsSinceNoConfigExists();
    }

    /// <summary>Writes a message to the application's log listeners.</summary>
    /// <param name="message">Required. The message to log. If <paramref name="message" /> is <see langword="Nothing" />, an empty string is used.</param>
    /// <exception cref="T:System.Security.SecurityException">Code with partial trust calls the method, but writes to an event log listener that requires full trust.</exception>
    public void WriteEntry(string message)
    {
      this.WriteEntry(message, TraceEventType.Information, this.TraceEventTypeToId(TraceEventType.Information));
    }

    /// <summary>Writes a message to the application's log listeners.</summary>
    /// <param name="message">Required. The message to log. If <paramref name="message" /> is <see langword="Nothing" />, an empty string is used.</param>
    /// <param name="severity">The type of message. By default, <see langword="TraceEventType.Information" />.</param>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The message type is not one of the <see cref="T:System.Diagnostics.TraceEventType" /> enumeration values.</exception>
    /// <exception cref="T:System.Security.SecurityException">Code with partial trust calls the method, but writes to an event log listener that requires full trust.</exception>
    public void WriteEntry(string message, TraceEventType severity)
    {
      this.WriteEntry(message, severity, this.TraceEventTypeToId(severity));
    }

    /// <summary>Writes a message to the application's log listeners.</summary>
    /// <param name="message">Required. The message to log. If <paramref name="message" /> is <see langword="Nothing" />, an empty string is used.</param>
    /// <param name="severity">The type of message. By default, <see langword="TraceEventType.Information" />.</param>
    /// <param name="id">Message identifier, typically used for correlation. By default, related to <paramref name="entryType" /> as described in the table.</param>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The message type is not one of the <see cref="T:System.Diagnostics.TraceEventType" /> enumeration values.</exception>
    /// <exception cref="T:System.Security.SecurityException">Code with partial trust calls the method, but writes to an event log listener that requires full trust.</exception>
    public void WriteEntry(string message, TraceEventType severity, int id)
    {
      if (message == null)
        message = "";
      this.m_TraceSource.TraceEvent(severity, id, message);
    }

    /// <summary>Writes exception information to the application's log listeners.</summary>
    /// <param name="ex">Required. Exception to log.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ex" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">Code with partial trust calls the method, but writes to an event log listener that requires full trust.</exception>
    public void WriteException(Exception ex)
    {
      this.WriteException(ex, TraceEventType.Error, "", this.TraceEventTypeToId(TraceEventType.Error));
    }

    /// <summary>Writes exception information to the application's log listeners.</summary>
    /// <param name="ex">Required. Exception to log.</param>
    /// <param name="severity">The type of message. By default, <see cref="F:System.Diagnostics.TraceEventType.Error" />.</param>
    /// <param name="additionalInfo">String to append to the message. By default, this is an empty string.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ex" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The message type is not one of the <see cref="T:System.Diagnostics.TraceEventType" /> enumeration values.</exception>
    /// <exception cref="T:System.Security.SecurityException">Code with partial trust calls the method, but writes to an event log listener that requires full trust.</exception>
    public void WriteException(Exception ex, TraceEventType severity, string additionalInfo)
    {
      this.WriteException(ex, severity, additionalInfo, this.TraceEventTypeToId(severity));
    }

    /// <summary>Writes exception information to the application's log listeners.</summary>
    /// <param name="ex">Required. Exception to log.</param>
    /// <param name="severity">The type of message. By default, <see cref="F:System.Diagnostics.TraceEventType.Error" />.</param>
    /// <param name="additionalInfo">String to append to the message. By default, this is an empty string.</param>
    /// <param name="id">Message identifier, typically used for correlation. By default, related to <paramref name="entryType" /> as described in the table in the Remarks section.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ex" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The message type is not one of the <see cref="T:System.Diagnostics.TraceEventType" /> enumeration values.</exception>
    /// <exception cref="T:System.Security.SecurityException">Code with partial trust calls the method, but writes to an event log listener that requires full trust.</exception>
    public void WriteException(Exception ex, TraceEventType severity, string additionalInfo, int id)
    {
      if (ex == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (ex));
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
      if (Operators.CompareString(additionalInfo, "", false) != 0)
      {
        stringBuilder.Append(" ");
        stringBuilder.Append(additionalInfo);
      }
      this.m_TraceSource.TraceEvent(severity, id, stringBuilder.ToString());
    }

    /// <summary>Gets to the <see cref="T:System.Diagnostics.TraceSource" /> object that underlies the <see langword="Log" /> object.</summary>
    /// <returns>Returns the <see cref="T:System.Diagnostics.TraceSource" /> object that underlies the <see langword="Log" /> object.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public TraceSource TraceSource
    {
      get
      {
        return (TraceSource) this.m_TraceSource;
      }
    }

  
    /// <summary>Gets the file the <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> object that underlies the <see langword="Log" /> object. </summary>
    /// <returns>Returns the <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> object that underlies the <see langword="Log" /> object.</returns>
    public FileLogTraceListener DefaultFileLogWriter
    {
      get
      {
        return (FileLogTraceListener) this.TraceSource.Listeners["FileLog"];
      }
    }
  
    /// <summary>Creates a new <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> object and adds it to the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection.</summary>
    protected internal virtual void InitializeWithDefaultsSinceNoConfigExists()
    {
      this.m_TraceSource.Listeners.Add((TraceListener) new FileLogTraceListener("FileLog"));
      this.m_TraceSource.Switch.Level = SourceLevels.Information;
    }
  

    private void CloseOnProcessExit(object sender, EventArgs e)
    {
      AppDomain.CurrentDomain.ProcessExit -= new EventHandler(this.CloseOnProcessExit);
      this.TraceSource.Close();
    }

    private static Dictionary<TraceEventType, int> InitializeIDHash()
    {
      Dictionary<TraceEventType, int> dictionary1 = new Dictionary<TraceEventType, int>(10);
      Dictionary<TraceEventType, int> dictionary2 = dictionary1;
      dictionary2.Add(TraceEventType.Information, 0);
      dictionary2.Add(TraceEventType.Warning, 1);
      dictionary2.Add(TraceEventType.Error, 2);
      dictionary2.Add(TraceEventType.Critical, 3);
      dictionary2.Add(TraceEventType.Start, 4);
      dictionary2.Add(TraceEventType.Stop, 5);
      dictionary2.Add(TraceEventType.Suspend, 6);
      dictionary2.Add(TraceEventType.Resume, 7);
      dictionary2.Add(TraceEventType.Verbose, 8);
      dictionary2.Add(TraceEventType.Transfer, 9);
      return dictionary1;
    }

    private int TraceEventTypeToId(TraceEventType traceEventValue)
    {
      if (Log.m_IdHash.ContainsKey(traceEventValue))
        return Log.m_IdHash[traceEventValue];
      return 0;
    }

    internal sealed class DefaultTraceSource : TraceSource
    {
      private bool m_HasBeenInitializedFromConfigFile;
      private StringDictionary listenerAttributes;

      public DefaultTraceSource(string name)
        : base(name)
      {
      }

      public bool HasBeenConfigured
      {
        get
        {
          if (this.listenerAttributes == null)
            this.listenerAttributes = this.Attributes;
          return this.m_HasBeenInitializedFromConfigFile;
        }
      }

      protected override string[] GetSupportedAttributes()
      {
        this.m_HasBeenInitializedFromConfigFile = true;
        return base.GetSupportedAttributes();
      }
    }
  }
}
*/