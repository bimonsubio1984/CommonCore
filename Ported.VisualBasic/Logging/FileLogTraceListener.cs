// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Logging.FileLogTraceListener
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Ported.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ported.VisualBasic.Logging
{
  /// <summary>Provides a simple listener that directs logging output to file.</summary>
  [ComVisible(false)]
  public class FileLogTraceListener : TraceListener
  {
    private static Dictionary<string, FileLogTraceListener.ReferencedStream> m_Streams = new Dictionary<string, FileLogTraceListener.ReferencedStream>();
    private LogFileLocation m_Location;
    private bool m_AutoFlush;
    private bool m_Append;
    private bool m_IncludeHostName;
    private DiskSpaceExhaustedOption m_DiskSpaceExhaustedBehavior;
    private string m_BaseFileName;
    private LogFileCreationScheduleOption m_LogFileDateStamp;
    private long m_MaxFileSize;
    private long m_ReserveDiskSpace;
    private string m_Delimiter;
    private Encoding m_Encoding;
    private string m_FullFileName;
    private string m_CustomLocation;
    private FileLogTraceListener.ReferencedStream m_Stream;
    private DateTime m_Day;
    private DateTime m_FirstDayOfWeek;
    private string m_HostName;
    private BitArray m_PropertiesSet;
    private string[] m_SupportedAttributes;
    private const int PROPERTY_COUNT = 12;
    private const int APPEND_INDEX = 0;
    private const int AUTOFLUSH_INDEX = 1;
    private const int BASEFILENAME_INDEX = 2;
    private const int CUSTOMLOCATION_INDEX = 3;
    private const int DELIMITER_INDEX = 4;
    private const int DISKSPACEEXHAUSTEDBEHAVIOR_INDEX = 5;
    private const int ENCODING_INDEX = 6;
    private const int INCLUDEHOSTNAME_INDEX = 7;
    private const int LOCATION_INDEX = 8;
    private const int LOGFILECREATIONSCHEDULE_INDEX = 9;
    private const int MAXFILESIZE_INDEX = 10;
    private const int RESERVEDISKSPACE_INDEX = 11;
    private const string DATE_FORMAT = "yyyy-MM-dd";
    private const string FILE_EXTENSION = ".log";
    private const int MAX_OPEN_ATTEMPTS = 2147483647;
    private const string DEFAULT_NAME = "FileLogTraceListener";
    private const int MIN_FILE_SIZE = 1000;
    private const string KEY_APPEND = "append";
    private const string KEY_APPEND_PASCAL = "Append";
    private const string KEY_AUTOFLUSH = "autoflush";
    private const string KEY_AUTOFLUSH_PASCAL = "AutoFlush";
    private const string KEY_AUTOFLUSH_CAMEL = "autoFlush";
    private const string KEY_BASEFILENAME = "basefilename";
    private const string KEY_BASEFILENAME_PASCAL = "BaseFilename";
    private const string KEY_BASEFILENAME_CAMEL = "baseFilename";
    private const string KEY_BASEFILENAME_PASCAL_ALT = "BaseFileName";
    private const string KEY_BASEFILENAME_CAMEL_ALT = "baseFileName";
    private const string KEY_CUSTOMLOCATION = "customlocation";
    private const string KEY_CUSTOMLOCATION_PASCAL = "CustomLocation";
    private const string KEY_CUSTOMLOCATION_CAMEL = "customLocation";
    private const string KEY_DELIMITER = "delimiter";
    private const string KEY_DELIMITER_PASCAL = "Delimiter";
    private const string KEY_DISKSPACEEXHAUSTEDBEHAVIOR = "diskspaceexhaustedbehavior";
    private const string KEY_DISKSPACEEXHAUSTEDBEHAVIOR_PASCAL = "DiskSpaceExhaustedBehavior";
    private const string KEY_DISKSPACEEXHAUSTEDBEHAVIOR_CAMEL = "diskSpaceExhaustedBehavior";
    private const string KEY_ENCODING = "encoding";
    private const string KEY_ENCODING_PASCAL = "Encoding";
    private const string KEY_INCLUDEHOSTNAME = "includehostname";
    private const string KEY_INCLUDEHOSTNAME_PASCAL = "IncludeHostName";
    private const string KEY_INCLUDEHOSTNAME_CAMEL = "includeHostName";
    private const string KEY_LOCATION = "location";
    private const string KEY_LOCATION_PASCAL = "Location";
    private const string KEY_LOGFILECREATIONSCHEDULE = "logfilecreationschedule";
    private const string KEY_LOGFILECREATIONSCHEDULE_PASCAL = "LogFileCreationSchedule";
    private const string KEY_LOGFILECREATIONSCHEDULE_CAMEL = "logFileCreationSchedule";
    private const string KEY_MAXFILESIZE = "maxfilesize";
    private const string KEY_MAXFILESIZE_PASCAL = "MaxFileSize";
    private const string KEY_MAXFILESIZE_CAMEL = "maxFileSize";
    private const string KEY_RESERVEDISKSPACE = "reservediskspace";
    private const string KEY_RESERVEDISKSPACE_PASCAL = "ReserveDiskSpace";
    private const string KEY_RESERVEDISKSPACE_CAMEL = "reserveDiskSpace";
    private const string STACK_DELIMITER = ", ";

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> class with the supplied name.</summary>
    /// <param name="name">
    /// <see langword="String" />. The name of the instance object.</param>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public FileLogTraceListener(string name)
      : base(name)
    {
      this.m_Location = LogFileLocation.LocalUserApplicationDirectory;
      this.m_AutoFlush = false;
      this.m_Append = true;
      this.m_IncludeHostName = false;
      this.m_DiskSpaceExhaustedBehavior = DiskSpaceExhaustedOption.DiscardMessages;
      this.m_BaseFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
      this.m_LogFileDateStamp = LogFileCreationScheduleOption.None;
      this.m_MaxFileSize = 5000000L;
      this.m_ReserveDiskSpace = 10000000L;
      this.m_Delimiter = "\t";
      this.m_Encoding = Encoding.UTF8;
      this.m_CustomLocation = Application.UserAppDataPath;
      this.m_Day = DateAndTime.Now.Date;
      this.m_FirstDayOfWeek = FileLogTraceListener.GetFirstDayOfWeek(DateAndTime.Now.Date);
      this.m_PropertiesSet = new BitArray(12, false);
      this.m_SupportedAttributes = new string[34]
      {
        "append",
        nameof (Append),
        "autoflush",
        nameof (AutoFlush),
        "autoFlush",
        "basefilename",
        "BaseFilename",
        "baseFilename",
        nameof (BaseFileName),
        "baseFileName",
        "customlocation",
        nameof (CustomLocation),
        "customLocation",
        "delimiter",
        nameof (Delimiter),
        "diskspaceexhaustedbehavior",
        nameof (DiskSpaceExhaustedBehavior),
        "diskSpaceExhaustedBehavior",
        "encoding",
        nameof (Encoding),
        "includehostname",
        nameof (IncludeHostName),
        "includeHostName",
        "location",
        nameof (Location),
        "logfilecreationschedule",
        nameof (LogFileCreationSchedule),
        "logFileCreationSchedule",
        "maxfilesize",
        nameof (MaxFileSize),
        "maxFileSize",
        "reservediskspace",
        nameof (ReserveDiskSpace),
        "reserveDiskSpace"
      };
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> class with the default name.</summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public FileLogTraceListener()
      : this(nameof (FileLogTraceListener))
    {
    }

    /// <summary>Gets or sets location for the log files.</summary>
    /// <returns>
    /// <see cref="T:Ported.VisualBasic.Logging.LogFileLocation" />, which is the location for the log file. The default value is <see cref="F:Ported.VisualBasic.Logging.LogFileLocation.LocalUserApplicationDirectory" />.</returns>
    public LogFileLocation Location
    {
      get
      {
        if (!this.m_PropertiesSet[8] && this.Attributes.ContainsKey("location"))
          this.Location = (LogFileLocation) TypeDescriptor.GetConverter(typeof (LogFileLocation)).ConvertFromInvariantString(this.Attributes["location"]);
        return this.m_Location;
      }
      set
      {
        this.ValidateLogFileLocationEnumValue(value, nameof (value));
        if (this.m_Location != value)
          this.CloseCurrentStream();
        this.m_Location = value;
        this.m_PropertiesSet[8] = true;
      }
    }

    /// <summary>Indicates whether or not the writing to the log file stream flushes the buffer.</summary>
    /// <returns>
    /// <see langword="Boolean" />, with <see langword="True" /> indicating that the stream is flushed after every write; otherwise the log entries are buffered and written more efficiently. The default setting for this property is <see langword="False" />.</returns>
    public bool AutoFlush
    {
      get
      {
        if (!this.m_PropertiesSet[1] && this.Attributes.ContainsKey("autoflush"))
          this.AutoFlush = Convert.ToBoolean(this.Attributes["autoflush"], (IFormatProvider) CultureInfo.InvariantCulture);
        return this.m_AutoFlush;
      }
      set
      {
        this.DemandWritePermission();
        this.m_AutoFlush = value;
        this.m_PropertiesSet[1] = true;
      }
    }

    /// <summary>Indicates whether or not the host name of the logging machine should be included in the output.</summary>
    /// <returns>
    /// <see langword="Boolean" />. Use <see langword="True" /> if the host identifier should be included; otherwise use <see langword="False" />. The default value is <see langword="False" />.</returns>
    public bool IncludeHostName
    {
      get
      {
        if (!this.m_PropertiesSet[7] && this.Attributes.ContainsKey("includehostname"))
          this.IncludeHostName = Convert.ToBoolean(this.Attributes["includehostname"], (IFormatProvider) CultureInfo.InvariantCulture);
        return this.m_IncludeHostName;
      }
      set
      {
        this.DemandWritePermission();
        this.m_IncludeHostName = value;
        this.m_PropertiesSet[7] = true;
      }
    }

    /// <summary>Determines whether to append the output to the current file or write it to a new file.</summary>
    /// <returns>
    /// <see langword="Boolean" />, with <see langword="True" /> indicating that the output is appended to the current file, and <see langword="False" /> indicating that output is written to a new file. The default setting for this property is <see langword="True" />.</returns>
    public bool Append
    {
      get
      {
        if (!this.m_PropertiesSet[0] && this.Attributes.ContainsKey("append"))
          this.Append = Convert.ToBoolean(this.Attributes["append"], (IFormatProvider) CultureInfo.InvariantCulture);
        return this.m_Append;
      }
      set
      {
        this.DemandWritePermission();
        if (value != this.m_Append)
          this.CloseCurrentStream();
        this.m_Append = value;
        this.m_PropertiesSet[0] = true;
      }
    }

    /// <summary>Determines what to do when writing to the log file and there is less free disk space available than specified by the <see cref="P:Ported.VisualBasic.Logging.FileLogTraceListener.ReserveDiskSpace" /> property.</summary>
    /// <returns>
    /// <see cref="T:Ported.VisualBasic.Logging.DiskSpaceExhaustedOption" />. Determines what to do when attempting to write to the log file and there is less free disk space available than specified by the <see cref="P:Ported.VisualBasic.Logging.FileLogTraceListener.ReserveDiskSpace" /> property, or if the log file size is greater than what the <see cref="P:Ported.VisualBasic.Logging.FileLogTraceListener.MaxFileSize" /> property allows. The default value is <see cref="F:Ported.VisualBasic.Logging.DiskSpaceExhaustedOption.DiscardMessages" />.</returns>
    public DiskSpaceExhaustedOption DiskSpaceExhaustedBehavior
    {
      get
      {
        if (!this.m_PropertiesSet[5] && this.Attributes.ContainsKey("diskspaceexhaustedbehavior"))
          this.DiskSpaceExhaustedBehavior = (DiskSpaceExhaustedOption) TypeDescriptor.GetConverter(typeof (DiskSpaceExhaustedOption)).ConvertFromInvariantString(this.Attributes["diskspaceexhaustedbehavior"]);
        return this.m_DiskSpaceExhaustedBehavior;
      }
      set
      {
        this.DemandWritePermission();
        this.ValidateDiskSpaceExhaustedOptionEnumValue(value, nameof (value));
        this.m_DiskSpaceExhaustedBehavior = value;
        this.m_PropertiesSet[5] = true;
      }
    }

    /// <summary>Gets or sets the base name for the log files, which is used to create the full log-file name.</summary>
    /// <returns>
    /// <see langword="String" />. The base name for the log files. The default is the application's product name.</returns>
    public string BaseFileName
    {
      get
      {
        if (!this.m_PropertiesSet[2] && this.Attributes.ContainsKey("basefilename"))
          this.BaseFileName = this.Attributes["basefilename"];
        return this.m_BaseFileName;
      }
      set
      {
        if (Operators.CompareString(value, "", false) == 0)
          throw ExceptionUtils.GetArgumentNullException(nameof (value), "ApplicationLogBaseNameNull");
        Path.GetFullPath(value);
        if (string.Compare(value, this.m_BaseFileName, StringComparison.OrdinalIgnoreCase) != 0)
        {
          this.CloseCurrentStream();
          this.m_BaseFileName = value;
        }
        this.m_PropertiesSet[2] = true;
      }
    }

    /// <summary>Gets the current full log-file name.</summary>
    /// <returns>
    /// <see langword="String" />, which is the current full log-file name.</returns>
    public string FullLogFileName
    {
      get
      {
        this.EnsureStreamIsOpen();
        string fullFileName = this.m_FullFileName;
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullFileName).Demand();
        return fullFileName;
      }
    }

    /// <summary>Determines which date to include in the names of the log files.</summary>
    /// <returns>
    /// <see cref="T:Ported.VisualBasic.Logging.LogFileCreationScheduleOption" />. This indicates which date to include in the log-file names. The default value is <see cref="F:Ported.VisualBasic.Logging.LogFileCreationScheduleOption.None" />.</returns>
    public LogFileCreationScheduleOption LogFileCreationSchedule
    {
      get
      {
        if (!this.m_PropertiesSet[9] && this.Attributes.ContainsKey("logfilecreationschedule"))
          this.LogFileCreationSchedule = (LogFileCreationScheduleOption) TypeDescriptor.GetConverter(typeof (LogFileCreationScheduleOption)).ConvertFromInvariantString(this.Attributes["logfilecreationschedule"]);
        return this.m_LogFileDateStamp;
      }
      set
      {
        this.ValidateLogFileCreationScheduleOptionEnumValue(value, nameof (value));
        if (value != this.m_LogFileDateStamp)
        {
          this.CloseCurrentStream();
          this.m_LogFileDateStamp = value;
        }
        this.m_PropertiesSet[9] = true;
      }
    }

    /// <summary>Gets or sets the maximum allowed size of the log file, in bytes.</summary>
    /// <returns>
    /// <see langword="Long" />. This is the maximum allowed log-file size, in bytes. The default value is 5000000.</returns>
    /// <exception cref="T:System.ArgumentException">When this property is set to a value less than 1000.</exception>
    public long MaxFileSize
    {
      get
      {
        if (!this.m_PropertiesSet[10] && this.Attributes.ContainsKey("maxfilesize"))
          this.MaxFileSize = Convert.ToInt64(this.Attributes["maxfilesize"], (IFormatProvider) CultureInfo.InvariantCulture);
        return this.m_MaxFileSize;
      }
      set
      {
        this.DemandWritePermission();
        if (value < 1000L)
          throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (value), "ApplicationLogNumberTooSmall", nameof (MaxFileSize));
        this.m_MaxFileSize = value;
        this.m_PropertiesSet[10] = true;
      }
    }

    /// <summary>Gets or sets the amount of free disk space, in bytes, necessary before messages can be written to the log file.</summary>
    /// <returns>
    /// <see langword="Long" />. This is the amount of free disk space necessary. The default value is 10000000.</returns>
    /// <exception cref="T:System.ArgumentException">When this property is set to a value less than 0.</exception>
    public long ReserveDiskSpace
    {
      get
      {
        if (!this.m_PropertiesSet[11] && this.Attributes.ContainsKey("reservediskspace"))
          this.ReserveDiskSpace = Convert.ToInt64(this.Attributes["reservediskspace"], (IFormatProvider) CultureInfo.InvariantCulture);
        return this.m_ReserveDiskSpace;
      }
      set
      {
        this.DemandWritePermission();
        if (value < 0L)
          throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (value), "ApplicationLog_NegativeNumber", nameof (ReserveDiskSpace));
        this.m_ReserveDiskSpace = value;
        this.m_PropertiesSet[11] = true;
      }
    }

    /// <summary>Gets or sets the delimiter used to delimit fields within a log message.</summary>
    /// <returns>
    /// <see langword="String" />, which is the delimiter used for fields within a log message. The default setting for this property is the TAB character.</returns>
    public string Delimiter
    {
      get
      {
        if (!this.m_PropertiesSet[4] && this.Attributes.ContainsKey("delimiter"))
          this.Delimiter = this.Attributes["delimiter"];
        return this.m_Delimiter;
      }
      set
      {
        this.m_Delimiter = value;
        this.m_PropertiesSet[4] = true;
      }
    }

    /// <summary>Gets or sets the encoding to use when creating a new log file.</summary>
    /// <returns>
    /// <see cref="T:System.Text.Encoding" />, which is the encoding to use when creating a new log file. The default value of this property is <see cref="T:System.Text.UTF8Encoding" />.</returns>
    public Encoding Encoding
    {
      get
      {
        if (!this.m_PropertiesSet[6] && this.Attributes.ContainsKey("encoding"))
          this.Encoding = Encoding.GetEncoding(this.Attributes["encoding"]);
        return this.m_Encoding;
      }
      set
      {
        if (value == null)
          throw ExceptionUtils.GetArgumentNullException(nameof (value));
        this.m_Encoding = value;
        this.m_PropertiesSet[6] = true;
      }
    }

    /// <summary>Gets or sets the log file directory when the <see cref="P:Ported.VisualBasic.Logging.FileLogTraceListener.Location" /> property is set to <see cref="F:Ported.VisualBasic.Logging.LogFileLocation.Custom" />.</summary>
    /// <returns>
    /// <see langword="String" />, which is the name of the log-file directory. The default setting for this property is the user's directory for application data.</returns>
    public string CustomLocation
    {
      get
      {
        if (!this.m_PropertiesSet[3] && this.Attributes.ContainsKey("customlocation"))
          this.CustomLocation = this.Attributes["customlocation"];
        string fullPath = Path.GetFullPath(this.m_CustomLocation);
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullPath).Demand();
        return fullPath;
      }
      set
      {
        string fullPath = Path.GetFullPath(value);
        if (!Directory.Exists(fullPath))
          Directory.CreateDirectory(fullPath);
        if (this.Location == LogFileLocation.Custom & string.Compare(fullPath, this.m_CustomLocation, StringComparison.OrdinalIgnoreCase) != 0)
          this.CloseCurrentStream();
        this.Location = LogFileLocation.Custom;
        this.m_CustomLocation = fullPath;
        this.m_PropertiesSet[3] = true;
      }
    }

    /// <summary>Writes a verbatim message to disk, without any additional context information.</summary>
    /// <param name="message">
    /// <see langword="String" />. The custom message to write.</param>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public override void Write(string message)
    {
      try
      {
        this.HandleDateChange();
        if (!this.ResourcesAvailable((long) this.Encoding.GetByteCount(message)))
          return;
        this.ListenerStream.Write(message);
        if (!this.AutoFlush)
          return;
        this.ListenerStream.Flush();
      }
      catch (Exception ex)
      {
        this.CloseCurrentStream();
        throw;
      }
    }

    /// <summary>Writes a verbatim message to disk, followed by the current line terminator, without any additional context information.</summary>
    /// <param name="message">
    /// <see langword="String" />. The custom message to write.</param>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public override void WriteLine(string message)
    {
      try
      {
        this.HandleDateChange();
        if (!this.ResourcesAvailable((long) this.Encoding.GetByteCount(message + "\r\n")))
          return;
        this.ListenerStream.WriteLine(message);
        if (!this.AutoFlush)
          return;
        this.ListenerStream.Flush();
      }
      catch (Exception ex)
      {
        this.CloseCurrentStream();
        throw;
      }
    }

    /// <summary>Writes trace information, a message and event information to the output file or stream.</summary>
    /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    /// <param name="source">A name of the trace source that invoked this method. </param>
    /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> enumeration values.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="message">A message to write.</param>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
    {
      if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message, (object[]) null, (object) null, (object[]) null))
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(source + this.Delimiter);
      stringBuilder.Append(Enum.GetName(typeof (TraceEventType), (object) eventType) + this.Delimiter);
      stringBuilder.Append(id.ToString((IFormatProvider) CultureInfo.InvariantCulture) + this.Delimiter);
      stringBuilder.Append(message);
      if ((this.TraceOutputOptions & TraceOptions.Callstack) == TraceOptions.Callstack)
        stringBuilder.Append(this.Delimiter + eventCache.Callstack);
      if ((this.TraceOutputOptions & TraceOptions.LogicalOperationStack) == TraceOptions.LogicalOperationStack)
        stringBuilder.Append(this.Delimiter + FileLogTraceListener.StackToString(eventCache.LogicalOperationStack));
      if ((this.TraceOutputOptions & TraceOptions.DateTime) == TraceOptions.DateTime)
        stringBuilder.Append(this.Delimiter + eventCache.DateTime.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture));
      if ((this.TraceOutputOptions & TraceOptions.ProcessId) == TraceOptions.ProcessId)
        stringBuilder.Append(this.Delimiter + eventCache.ProcessId.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if ((this.TraceOutputOptions & TraceOptions.ThreadId) == TraceOptions.ThreadId)
        stringBuilder.Append(this.Delimiter + eventCache.ThreadId);
      if ((this.TraceOutputOptions & TraceOptions.Timestamp) == TraceOptions.Timestamp)
        stringBuilder.Append(this.Delimiter + eventCache.Timestamp.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if (this.IncludeHostName)
        stringBuilder.Append(this.Delimiter + this.HostName);
      this.WriteLine(stringBuilder.ToString());
    }

    /// <summary>Writes trace information, a formatted array of objects, and event information to the output file or stream.</summary>
    /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    /// <param name="source">A name of the trace source that invoked this method. </param>
    /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> enumeration values.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
    /// <param name="args">An <see langword="Object" /> array containing zero or more objects to format.</param>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
    {
      string message = args == null ? format : string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, args);
      this.TraceEvent(eventCache, source, eventType, id, message);
    }

    /// <summary>Writes trace information, a data object, and event information to the output file or stream.</summary>
    /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    /// <param name="source">A name of the trace source that invoked this method. </param>
    /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> enumeration values.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">The trace data to emit.</param>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
    {
      string message = "";
      if (data != null)
        message = data.ToString();
      this.TraceEvent(eventCache, source, eventType, id, message);
    }

    /// <summary>Writes trace information, an array of data objects, and event information to the output file or stream.</summary>
    /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    /// <param name="source">The name of the trace source that invoked this method. </param>
    /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> enumeration values.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">An array of objects to emit as data.</param>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (data != null)
      {
        int num1 = checked (data.Length - 1);
        int num2 = 0;
        int num3 = num1;
        int index = num2;
        while (index <= num3)
        {
          stringBuilder.Append(data[index].ToString());
          if (index != num1)
            stringBuilder.Append(this.Delimiter);
          checked { ++index; }
        }
      }
      this.TraceEvent(eventCache, source, eventType, id, stringBuilder.ToString());
    }

    /// <summary>Flushes the underlying stream that writes to the current log file.</summary>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public override void Flush()
    {
      if (this.m_Stream == null)
        return;
      this.m_Stream.Flush();
    }

    /// <summary>Closes the underlying stream for the current log file and releases any resources associated with the current stream.</summary>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public override void Close()
    {
      this.Dispose(true);
    }

    /// <summary>Gets the custom XML configuration attributes supported by the trace listener.</summary>
    /// <returns>
    /// <see langword="String" /> array containing the XML configuration attributes recognized by this listener.</returns>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    protected override string[] GetSupportedAttributes()
    {
      return this.m_SupportedAttributes;
    }

    /// <summary>Closes the underlying stream and optionally releases the managed resources.</summary>
    /// <param name="disposing">
    /// <see langword="True" /> releases both managed and unmanaged resources; <see langword="False" /> releases only unmanaged resources. </param>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    protected override void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.CloseCurrentStream();
    }

    private string LogFileName
    {
      get
      {
        string path1;
        switch (this.Location)
        {
          case LogFileLocation.TempDirectory:
            path1 = Path.GetTempPath();
            break;
          case LogFileLocation.LocalUserApplicationDirectory:
            path1 = Application.UserAppDataPath;
            break;
          case LogFileLocation.CommonApplicationDirectory:
            path1 = Application.CommonAppDataPath;
            break;
          case LogFileLocation.ExecutableDirectory:
            path1 = Path.GetDirectoryName(Application.ExecutablePath);
            break;
          case LogFileLocation.Custom:
            path1 = Operators.CompareString(this.CustomLocation, "", false) != 0 ? this.CustomLocation : Application.UserAppDataPath;
            break;
          default:
            path1 = Application.UserAppDataPath;
            break;
        }
        string path2 = this.BaseFileName;
        switch (this.LogFileCreationSchedule)
        {
          case LogFileCreationScheduleOption.Daily:
            path2 = path2 + "-" + DateAndTime.Now.Date.ToString("yyyy-MM-dd", (IFormatProvider) CultureInfo.InvariantCulture);
            break;
          case LogFileCreationScheduleOption.Weekly:
            this.m_FirstDayOfWeek = DateAndTime.Now.AddDays((double) checked (-unchecked ((int) DateAndTime.Now.DayOfWeek)));
            path2 = path2 + "-" + this.m_FirstDayOfWeek.Date.ToString("yyyy-MM-dd", (IFormatProvider) CultureInfo.InvariantCulture);
            break;
        }
        return Path.Combine(path1, path2);
      }
    }

    private FileLogTraceListener.ReferencedStream ListenerStream
    {
      get
      {
        this.EnsureStreamIsOpen();
        return this.m_Stream;
      }
    }

    private FileLogTraceListener.ReferencedStream GetStream()
    {
      int num = 0;
      FileLogTraceListener.ReferencedStream referencedStream = (FileLogTraceListener.ReferencedStream) null;
      string fullPath = Path.GetFullPath(this.LogFileName + ".log");
      while (referencedStream == null && num < int.MaxValue)
      {
        string str = num != 0 ? Path.GetFullPath(this.LogFileName + "-" + num.ToString((IFormatProvider) CultureInfo.InvariantCulture) + ".log") : Path.GetFullPath(this.LogFileName + ".log");
        string upper = str.ToUpper(CultureInfo.InvariantCulture);
        lock (FileLogTraceListener.m_Streams)
        {
          if (FileLogTraceListener.m_Streams.ContainsKey(upper))
          {
            referencedStream = FileLogTraceListener.m_Streams[upper];
            if (!referencedStream.IsInUse)
            {
              FileLogTraceListener.m_Streams.Remove(upper);
              referencedStream = (FileLogTraceListener.ReferencedStream) null;
            }
            else
            {
              if (this.Append)
              {
                new FileIOPermission(FileIOPermissionAccess.Write, str).Demand();
                referencedStream.AddReference();
                this.m_FullFileName = str;
                return referencedStream;
              }
              checked { ++num; }
              referencedStream = (FileLogTraceListener.ReferencedStream) null;
              continue;
            }
          }
          Encoding encoding = this.Encoding;
          try
          {
            if (this.Append)
              encoding = this.GetFileEncoding(str) ?? this.Encoding;
            referencedStream = new FileLogTraceListener.ReferencedStream(new StreamWriter(str, this.Append, encoding));
            referencedStream.AddReference();
            FileLogTraceListener.m_Streams.Add(upper, referencedStream);
            this.m_FullFileName = str;
            return referencedStream;
          }
          catch (IOException ex)
          {
          }
          checked { ++num; }
        }
      }
      throw ExceptionUtils.GetInvalidOperationException("ApplicationLog_ExhaustedPossibleStreamNames", fullPath);
    }

    private void EnsureStreamIsOpen()
    {
      if (this.m_Stream != null)
        return;
      this.m_Stream = this.GetStream();
    }

    private void CloseCurrentStream()
    {
      if (this.m_Stream == null)
        return;
      lock (FileLogTraceListener.m_Streams)
      {
        this.m_Stream.CloseStream();
        if (!this.m_Stream.IsInUse)
          FileLogTraceListener.m_Streams.Remove(this.m_FullFileName.ToUpper(CultureInfo.InvariantCulture));
        this.m_Stream = (FileLogTraceListener.ReferencedStream) null;
      }
    }

    private bool DayChanged()
    {
      return DateTime.Compare(this.m_Day.Date, DateAndTime.Now.Date) != 0;
    }

    private bool WeekChanged()
    {
      return DateTime.Compare(this.m_FirstDayOfWeek.Date, FileLogTraceListener.GetFirstDayOfWeek(DateAndTime.Now.Date)) != 0;
    }

    private static DateTime GetFirstDayOfWeek(DateTime checkDate)
    {
      return checkDate.AddDays((double) checked (-unchecked ((int) checkDate.DayOfWeek))).Date;
    }

    private void HandleDateChange()
    {
      if (this.LogFileCreationSchedule == LogFileCreationScheduleOption.Daily)
      {
        if (!this.DayChanged())
          return;
        this.CloseCurrentStream();
      }
      else
      {
        if (this.LogFileCreationSchedule != LogFileCreationScheduleOption.Weekly || !this.WeekChanged())
          return;
        this.CloseCurrentStream();
      }
    }

    private bool ResourcesAvailable(long newEntrySize)
    {
      if (checked (this.ListenerStream.FileSize + newEntrySize) > this.MaxFileSize)
      {
        if (this.DiskSpaceExhaustedBehavior == DiskSpaceExhaustedOption.ThrowException)
          throw new InvalidOperationException(Utils.GetResourceString("ApplicationLog_FileExceedsMaximumSize"));
        return false;
      }
      if (checked (this.GetFreeDiskSpace() - newEntrySize) >= this.ReserveDiskSpace)
        return true;
      if (this.DiskSpaceExhaustedBehavior == DiskSpaceExhaustedOption.ThrowException)
        throw new InvalidOperationException(Utils.GetResourceString("ApplicationLog_ReservedSpaceEncroached"));
      return false;
    }

    private long GetFreeDiskSpace()
    {
      string pathRoot = Path.GetPathRoot(Path.GetFullPath(this.FullLogFileName));
      long UserSpaceFree = -1;
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, pathRoot).Demand();
      long TotalUserSpace=0;
      long TotalFreeSpace = 0;
      if (Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.GetDiskFreeSpaceEx(pathRoot, ref UserSpaceFree, ref TotalUserSpace, ref TotalFreeSpace) && UserSpaceFree > -1L)
        return UserSpaceFree;
      throw ExceptionUtils.GetWin32Exception("ApplicationLog_FreeSpaceError");
    }

    private Encoding GetFileEncoding(string fileName)
    {
      if (File.Exists(fileName))
      {
        StreamReader streamReader = (StreamReader) null;
        try
        {
          streamReader = new StreamReader(fileName, this.Encoding, true);
          if (streamReader.BaseStream.Length > 0L)
          {
            streamReader.ReadLine();
            return streamReader.CurrentEncoding;
          }
        }
        finally
        {
          streamReader?.Close();
        }
      }
      return (Encoding) null;
    }

    private string HostName
    {
      get
      {
        if (Operators.CompareString(this.m_HostName, "", false) == 0)
          this.m_HostName = Environment.MachineName;
        return this.m_HostName;
      }
    }

    private void DemandWritePermission()
    {
      new FileIOPermission(FileIOPermissionAccess.Write, Path.GetDirectoryName(this.LogFileName)).Demand();
    }

    private void ValidateLogFileLocationEnumValue(LogFileLocation value, string paramName)
    {
      switch (value)
      {
        case LogFileLocation.TempDirectory:
        case LogFileLocation.LocalUserApplicationDirectory:
        case LogFileLocation.CommonApplicationDirectory:
        case LogFileLocation.ExecutableDirectory:
        case LogFileLocation.Custom:
          break;
        default:
          throw new InvalidEnumArgumentException(paramName, (int) value, typeof (LogFileLocation));
      }
    }

    private void ValidateDiskSpaceExhaustedOptionEnumValue(DiskSpaceExhaustedOption value, string paramName)
    {
      switch (value)
      {
        case DiskSpaceExhaustedOption.ThrowException:
        case DiskSpaceExhaustedOption.DiscardMessages:
          break;
        default:
          throw new InvalidEnumArgumentException(paramName, (int) value, typeof (DiskSpaceExhaustedOption));
      }
    }

    private void ValidateLogFileCreationScheduleOptionEnumValue(LogFileCreationScheduleOption value, string paramName)
    {
      switch (value)
      {
        case LogFileCreationScheduleOption.None:
        case LogFileCreationScheduleOption.Daily:
        case LogFileCreationScheduleOption.Weekly:
          break;
        default:
          throw new InvalidEnumArgumentException(paramName, (int) value, typeof (LogFileCreationScheduleOption));
      }
    }

    private static string StackToString(Stack stack)
    {
      int length = ", ".Length;
      StringBuilder stringBuilder = new StringBuilder();
      try
      {
        foreach (object obj in stack)
          stringBuilder.Append(obj.ToString() + ", ");
      }
      finally
      {
        IEnumerator enumerator=null;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      stringBuilder.Replace("\"", "\"\"");
      if (stringBuilder.Length >= length)
        stringBuilder.Remove(checked (stringBuilder.Length - length), length);
      return "\"" + stringBuilder.ToString() + "\"";
    }

    internal class ReferencedStream : IDisposable
    {
      private StreamWriter m_Stream;
      private int m_ReferenceCount;
      private object m_SyncObject;
      private bool m_Disposed;

      internal ReferencedStream(StreamWriter stream)
      {
        this.m_ReferenceCount = 0;
        this.m_SyncObject = new object();
        this.m_Disposed = false;
        this.m_Stream = stream;
      }

      internal void Write(string message)
      {
        object syncObject = this.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          this.m_Stream.Write(message);
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }

      internal void WriteLine(string message)
      {
        object syncObject = this.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          this.m_Stream.WriteLine(message);
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }

      internal void AddReference()
      {
        object syncObject = this.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          checked { ++this.m_ReferenceCount; }
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }

      internal void Flush()
      {
        object syncObject = this.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          this.m_Stream.Flush();
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }

      internal void CloseStream()
      {
        object syncObject = this.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          try
          {
            checked { --this.m_ReferenceCount; }
            this.m_Stream.Flush();
          }
          finally
          {
            if (this.m_ReferenceCount <= 0)
            {
              this.m_Stream.Close();
              this.m_Stream = (StreamWriter) null;
            }
          }
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }

      internal bool IsInUse
      {
        get
        {
          return this.m_Stream != null;
        }
      }

      internal long FileSize
      {
        get
        {
          return this.m_Stream.BaseStream.Length;
        }
      }

      private void Dispose(bool disposing)
      {
        if (!disposing || this.m_Disposed)
          return;
        if (this.m_Stream != null)
          this.m_Stream.Close();
        this.m_Disposed = true;
      }

      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      ~ReferencedStream()
      {
        this.Dispose(false);
        // ISSUE: explicit finalizer call
        //base.Finalize();
      }
    }
  }
}

*/