// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Ported.VisualBasic.CompilerServices;
using Ported.VisualBasic.Devices;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides properties, methods, and events related to the current application.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class WindowsFormsApplicationBase : ConsoleApplicationBase
  {
    private const int SECOND_INSTANCE_TIMEOUT = 1000;
    private const int ATTACH_TIMEOUT = 2500;
    private ArrayList m_UnhandledExceptionHandlers;
    private bool m_ProcessingUnhandledExceptionEvent;
    private bool m_TurnOnNetworkListener;
    private bool m_FinishedOnInitilaize;
    private ArrayList m_NetworkAvailabilityEventHandlers;
    private EventWaitHandle m_FirstInstanceSemaphore;
    private EventWaitHandle m_MessageRecievedSemaphore;
    private Network m_NetworkObject;
    private string m_MemoryMappedID;
    private SafeFileHandle m_FirstInstanceMemoryMappedFileHandle;
    private bool m_IsSingleInstance;
    private ShutdownMode m_ShutdownStyle;
    private bool m_EnableVisualStyles;
    private bool m_DidSplashScreen;
    private bool m_Ok2CloseSplashScreen;
    private Form m_SplashScreen;
    private int m_MinimumSplashExposure;
    private System.Timers.Timer m_SplashTimer;
    private object m_SplashLock;
    private WindowsFormsApplicationBase.WinFormsAppContext m_AppContext;
    private SynchronizationContext m_AppSyncronizationContext;
    private object m_NetworkAvailChangeLock;
    private bool m_SaveMySettingsOnExit;
    private SendOrPostCallback m_StartNextInstanceCallback;
    private string m_HostName;

    /// <summary>Occurs when the application starts.</summary>
    public event StartupEventHandler Startup;

    /// <summary>Occurs when attempting to start a single-instance application and the application is already active.</summary>
    public event StartupNextInstanceEventHandler StartupNextInstance;

    /// <summary>Occurs when the application shuts down.</summary>
    public event ShutdownEventHandler Shutdown;

    /// <summary>Occurs when the network availability changes.</summary>
    public event NetworkAvailableEventHandler NetworkAvailabilityChanged
    {
      add
      {
        object networkAvailChangeLock = this.m_NetworkAvailChangeLock;
        ObjectFlowControl.CheckForSyncLockOnValueType(networkAvailChangeLock);
        Monitor.Enter(networkAvailChangeLock);
        try
        {
          if (this.m_NetworkAvailabilityEventHandlers == null)
            this.m_NetworkAvailabilityEventHandlers = new ArrayList();
          this.m_NetworkAvailabilityEventHandlers.Add((object) value);
          this.m_TurnOnNetworkListener = true;
          if (!(this.m_NetworkObject == null & this.m_FinishedOnInitilaize))
            return;
          this.m_NetworkObject = new Network();
          this.m_NetworkObject.NetworkAvailabilityChanged += new NetworkAvailableEventHandler(this.NetworkAvailableEventAdaptor);
        }
        finally
        {
          Monitor.Exit(networkAvailChangeLock);
        }
      }
      remove
      {
        if (this.m_NetworkAvailabilityEventHandlers == null || this.m_NetworkAvailabilityEventHandlers.Count <= 0)
          return;
        this.m_NetworkAvailabilityEventHandlers.Remove((object) value);
        if (this.m_NetworkAvailabilityEventHandlers.Count != 0)
          return;
        this.m_NetworkObject.NetworkAvailabilityChanged -= new NetworkAvailableEventHandler(this.NetworkAvailableEventAdaptor);
        if (this.m_NetworkObject == null)
          return;
        this.m_NetworkObject.DisconnectListener();
        this.m_NetworkObject = (Network) null;
      }
    }

    [SpecialName]
    private void raise_NetworkAvailabilityChanged(object sender, NetworkAvailableEventArgs e)
    {
      if (this.m_NetworkAvailabilityEventHandlers == null)
        return;
      try
      {
        foreach (NetworkAvailableEventHandler availabilityEventHandler in this.m_NetworkAvailabilityEventHandlers)
        {
          try
          {
            if (availabilityEventHandler != null)
              availabilityEventHandler(sender, e);
          }
          catch (Exception ex)
          {
            if (!this.OnUnhandledException(new UnhandledExceptionEventArgs(true, ex)))
              throw;
          }
        }
      }
      finally
      {
        IEnumerator enumerator=null;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    /// <summary>Occurs when the application encounters an unhandled exception.</summary>
    public event UnhandledExceptionEventHandler UnhandledException
    {
      add
      {
        if (this.m_UnhandledExceptionHandlers == null)
          this.m_UnhandledExceptionHandlers = new ArrayList();
        this.m_UnhandledExceptionHandlers.Add((object) value);
        if (this.m_UnhandledExceptionHandlers.Count != 1)
          return;
        Application.ThreadException += new ThreadExceptionEventHandler(this.OnUnhandledExceptionEventAdaptor);
      }
      remove
      {
        if (this.m_UnhandledExceptionHandlers == null || this.m_UnhandledExceptionHandlers.Count <= 0)
          return;
        this.m_UnhandledExceptionHandlers.Remove((object) value);
        if (this.m_UnhandledExceptionHandlers.Count != 0)
          return;
        Application.ThreadException -= new ThreadExceptionEventHandler(this.OnUnhandledExceptionEventAdaptor);
      }
    }

    [SpecialName]
    private void raise_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      if (this.m_UnhandledExceptionHandlers == null)
        return;
      this.m_ProcessingUnhandledExceptionEvent = true;
      try
      {
        foreach (UnhandledExceptionEventHandler exceptionHandler in this.m_UnhandledExceptionHandlers)
        {
          if (exceptionHandler != null)
            exceptionHandler(sender, e);
        }
      }
      finally
      {
        IEnumerator enumerator=null;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      this.m_ProcessingUnhandledExceptionEvent = false;
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.WindowsFormsApplicationBase" /> class.</summary>
    public WindowsFormsApplicationBase()
      : this(AuthenticationMode.Windows)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.WindowsFormsApplicationBase" /> class with the specified authentication mode.</summary>
    /// <param name="authenticationMode">
    /// <see cref="T:Ported.VisualBasic.ApplicationServices.AuthenticationMode" />. Specifies the application's authentication mode.</param>
    public WindowsFormsApplicationBase(AuthenticationMode authenticationMode)
    {
      this.m_MinimumSplashExposure = 2000;
      this.m_SplashLock = new object();
      this.m_NetworkAvailChangeLock = new object();
      this.m_Ok2CloseSplashScreen = true;
      this.ValidateAuthenticationModeEnumValue(authenticationMode, nameof (authenticationMode));
      if (authenticationMode == AuthenticationMode.Windows)
      {
        try
        {
          Thread.CurrentPrincipal = (IPrincipal) new WindowsPrincipal(WindowsIdentity.GetCurrent());
        }
        catch (SecurityException ex)
        {
        }
      }
      this.m_AppContext = new WindowsFormsApplicationBase.WinFormsAppContext(this);
      new UIPermission(UIPermissionWindow.AllWindows).Assert();
      this.m_AppSyncronizationContext = AsyncOperationManager.SynchronizationContext;
      AsyncOperationManager.SynchronizationContext = (SynchronizationContext) new WindowsFormsSynchronizationContext();
      PermissionSet.RevertAssert();
    }

    /// <summary>Sets up and starts the Visual Basic Application model. </summary>
    /// <param name="commandLine">Array of type <see langword="String" />. The command line from <see langword="Sub Main" />.</param>
    public void Run(string[] commandLine)
    {
      this.InternalCommandLine = new ReadOnlyCollection<string>((IList<string>) commandLine);
      if (!this.IsSingleInstance)
      {
        this.DoApplicationModel();
      }
      else
      {
        string applicationInstanceId = this.GetApplicationInstanceID(Assembly.GetCallingAssembly());
        this.m_MemoryMappedID = applicationInstanceId + "Map";
        string name1 = applicationInstanceId + "Event";
        string name2 = applicationInstanceId + "Event2";
        this.m_StartNextInstanceCallback = new SendOrPostCallback(this.OnStartupNextInstanceMarshallingAdaptor);
        new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Assert();
        string name3 = WindowsIdentity.GetCurrent().Name;
        bool SecureChannel = Operators.CompareString(name3, "", false) != 0;
        CodeAccessPermission.RevertAssert();
        bool createdNew;
        if (SecureChannel)
        {
          EventWaitHandleAccessRule rule = new EventWaitHandleAccessRule(name3, EventWaitHandleRights.FullControl, AccessControlType.Allow);
          EventWaitHandleSecurity eventSecurity1 = new EventWaitHandleSecurity();
          eventSecurity1.AddAccessRule(rule);
          this.m_FirstInstanceSemaphore = new EventWaitHandle(false, EventResetMode.ManualReset, name1, out createdNew, eventSecurity1);
          int num1 = 0;
          int num2 = 0;
          string name4 = name2;
          bool flag = false;
          ref bool local = ref flag;
          EventWaitHandleSecurity eventSecurity2 = eventSecurity1;
          this.m_MessageRecievedSemaphore = new EventWaitHandle(num1 != 0, (EventResetMode) num2, name4, out local, eventSecurity2);
        }
        else
        {
          this.m_FirstInstanceSemaphore = new EventWaitHandle(false, EventResetMode.ManualReset, name1, out createdNew);
          this.m_MessageRecievedSemaphore = new EventWaitHandle(false, EventResetMode.AutoReset, name2);
        }
        if (createdNew)
        {
          try
          {
            TcpChannel tcpChannel = this.RegisterChannel(SecureChannel);
            WindowsFormsApplicationBase.RemoteCommunicator remoteCommunicator = new WindowsFormsApplicationBase.RemoteCommunicator(this, this.m_MessageRecievedSemaphore);
            string str = applicationInstanceId + ".rem";
            new SecurityPermission(SecurityPermissionFlag.RemotingConfiguration).Assert();
            RemotingServices.Marshal((MarshalByRefObject) remoteCommunicator, str);
            CodeAccessPermission.RevertAssert();
            this.WriteUrlToMemoryMappedFile(tcpChannel.GetUrlsForUri(str)[0]);
            this.m_FirstInstanceSemaphore.Set();
            this.DoApplicationModel();
          }
          finally
          {
            if (this.m_MessageRecievedSemaphore != null)
              this.m_MessageRecievedSemaphore.Close();
            if (this.m_FirstInstanceSemaphore != null)
              this.m_FirstInstanceSemaphore.Close();
            if (this.m_FirstInstanceMemoryMappedFileHandle != null && !this.m_FirstInstanceMemoryMappedFileHandle.IsInvalid)
              this.m_FirstInstanceMemoryMappedFileHandle.Close();
          }
        }
        else
        {
          if (!this.m_FirstInstanceSemaphore.WaitOne(1000, false))
            throw new CantStartSingleInstanceException();
          this.RegisterChannel(SecureChannel);
          string url = this.ReadUrlFromMemoryMappedFile();
          if (url == null)
            throw new CantStartSingleInstanceException();
          WindowsFormsApplicationBase.RemoteCommunicator remoteCommunicator = (WindowsFormsApplicationBase.RemoteCommunicator) RemotingServices.Connect(typeof (WindowsFormsApplicationBase.RemoteCommunicator), url);
          PermissionSet permissionSet = new PermissionSet(PermissionState.None);
          permissionSet.AddPermission((IPermission) new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.SerializationFormatter | SecurityPermissionFlag.ControlPrincipal));
          permissionSet.AddPermission((IPermission) new DnsPermission(PermissionState.Unrestricted));
          permissionSet.AddPermission((IPermission) new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, this.HostName, -1));
          permissionSet.AddPermission((IPermission) new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME"));
          permissionSet.Assert();
          remoteCommunicator.RunNextInstance(this.CommandLineArgs);
          PermissionSet.RevertAssert();
          if (!this.m_MessageRecievedSemaphore.WaitOne(2500, false))
            throw new CantStartSingleInstanceException();
        }
      }
    }

    /// <summary>Gets a collection of all the application's open forms.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.FormCollection" /> object that contains all of the application's open forms.</returns>
    public FormCollection OpenForms
    {
      get
      {
        return Application.OpenForms;
      }
    }

    /// <summary>Gets or sets the main form for this application.</summary>
    /// <returns>Gets or sets the main form for this application.</returns>
    protected Form MainForm
    {
      get
      {
        return Interaction.IIf<Form>(this.m_AppContext != null, this.m_AppContext.MainForm, (Form) null);
      }
      set
      {
        if (value == null)
          throw ExceptionUtils.GetArgumentNullException(nameof (MainForm), "General_PropertyNothing", nameof (MainForm));
        if (value == this.m_SplashScreen)
          throw new ArgumentException(Utils.GetResourceString("AppModel_SplashAndMainFormTheSame"));
        this.m_AppContext.MainForm = value;
      }
    }

    /// <summary>Gets or sets the splash screen for this application.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.Form" /> object that the application uses as the splash screen.</returns>
    /// <exception cref="T:System.ArgumentNullException">The same value is assigned to this property and the <see cref="P:Ported.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.MainForm" />  property.</exception>
    public Form SplashScreen
    {
      get
      {
        return this.m_SplashScreen;
      }
      set
      {
        if (value != null && value == this.m_AppContext.MainForm)
          throw new ArgumentException(Utils.GetResourceString("AppModel_SplashAndMainFormTheSame"));
        this.m_SplashScreen = value;
      }
    }

    /// <summary>Determines the minimum length of time, in milliseconds, for which the splash screen is displayed.</summary>
    /// <returns>
    /// <see langword="Integer" />. The minimum length of time, in milliseconds, for which the splash screen is displayed.</returns>
    public int MinimumSplashScreenDisplayTime
    {
      get
      {
        return this.m_MinimumSplashExposure;
      }
      set
      {
        this.m_MinimumSplashExposure = value;
      }
    }

    /// <summary>When overridden in a derived class, this property allows a designer to specify the default text rendering engine for the application's forms.</summary>
    /// <returns>
    /// <see langword="Boolean" />. A value of <see langword="False" /> indicates that the application should use the default text rendering engine for Visual Basic 2005. A value of <see langword="True" /> indicates that the application should use the text rendering engine for Visual Basic .NET 2002 and Visual Basic .NET 2003.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected static bool UseCompatibleTextRendering
    {
      get
      {
        return false;
      }
    }

    /// <summary>Gets the <see cref="T:System.Windows.Forms.ApplicationContext" /> object for the current thread of a Windows Forms application.</summary>
    /// <returns>This property returns the <see cref="T:System.Windows.Forms.ApplicationContext" /> object for the current thread. That object contains contextual information about the thread.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ApplicationContext ApplicationContext
    {
      get
      {
        return (ApplicationContext) this.m_AppContext;
      }
    }

    /// <summary>Determines whether the application saves the user settings on exit.</summary>
    /// <returns>
    /// <see langword="Boolean" />. <see langword="True" /> indicates that the application saves the user settings on exit. Otherwise, the settings are not implicitly saved.</returns>
    public bool SaveMySettingsOnExit
    {
      get
      {
        return this.m_SaveMySettingsOnExit;
      }
      set
      {
        this.m_SaveMySettingsOnExit = value;
      }
    }

    /// <summary>Processes all Windows messages currently in the message queue.</summary>
    public void DoEvents()
    {
      Application.DoEvents();
    }

    /// <summary>Sets the visual styles, text display styles, and current principal for the main application thread (if the application uses Windows authentication), and initializes the splash screen, if defined.</summary>
    /// <param name="commandLineArgs">A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see langword="String" />, containing the command-line arguments as strings for the current application.</param>
    /// <returns>A <see cref="T:System.Boolean" /> indicating if application startup should continue.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [STAThread]
    protected virtual bool OnInitialize(ReadOnlyCollection<string> commandLineArgs)
    {
      if (this.m_EnableVisualStyles)
        Application.EnableVisualStyles();
      if (!commandLineArgs.Contains("/nosplash") && !this.CommandLineArgs.Contains("-nosplash"))
        this.ShowSplashScreen();
      this.m_FinishedOnInitilaize = true;
      return true;
    }

    /// <summary>When overridden in a derived class, allows for code to run when the application starts.</summary>
    /// <param name="eventArgs">
    /// <see cref="T:Ported.VisualBasic.ApplicationServices.StartupEventArgs" />. Contains the command-line arguments of the application and indicates whether the application startup should be canceled.</param>
    /// <returns>A <see cref="T:System.Boolean" /> that indicates if the application should continue starting up.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual bool OnStartup(StartupEventArgs eventArgs)
    {
      eventArgs.Cancel = false;
      if (this.m_TurnOnNetworkListener & this.m_NetworkObject == null)
      {
        this.m_NetworkObject = new Network();
        this.m_NetworkObject.NetworkAvailabilityChanged += new NetworkAvailableEventHandler(this.NetworkAvailableEventAdaptor);
      }
      StartupEventHandler startupEvent = this.StartupEvent;
      if (startupEvent != null)
        startupEvent((object) this, eventArgs);
      return !eventArgs.Cancel;
    }

    /// <summary>When overridden in a derived class, allows for code to run when a subsequent instance of a single-instance application starts.</summary>
    /// <param name="eventArgs">
    /// <see cref="T:Ported.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs" />. Contains the command-line arguments of the subsequent application instance and indicates whether the first application instance should be brought to the foreground upon exiting the exception handler.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
    {
      StartupNextInstanceEventHandler nextInstanceEvent = this.StartupNextInstanceEvent;
      if (nextInstanceEvent != null)
        nextInstanceEvent((object) this, eventArgs);
      new UIPermission(UIPermissionWindow.AllWindows).Assert();
      if (!eventArgs.BringToForeground || this.MainForm == null)
        return;
      if (this.MainForm.WindowState == FormWindowState.Minimized)
        this.MainForm.WindowState = FormWindowState.Normal;
      this.MainForm.Activate();
    }

    /// <summary>Provides the starting point for when the main application is ready to start running, after the initialization is done.</summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnRun()
    {
      if (this.MainForm == null)
      {
        this.OnCreateMainForm();
        if (this.MainForm == null)
          throw new NoStartupFormException();
        this.MainForm.Load += new EventHandler(this.MainFormLoadingDone);
      }
      try
      {
        Application.Run((ApplicationContext) this.m_AppContext);
      }
      finally
      {
        if (this.m_NetworkObject != null)
          this.m_NetworkObject.DisconnectListener();
        if (this.m_FirstInstanceSemaphore != null)
        {
          this.m_FirstInstanceSemaphore.Close();
          this.m_FirstInstanceSemaphore = (EventWaitHandle) null;
        }
        AsyncOperationManager.SynchronizationContext = this.m_AppSyncronizationContext;
        this.m_AppSyncronizationContext = (SynchronizationContext) null;
      }
    }

    /// <summary>When overridden in a derived class, allows a designer to emit code that initializes the splash screen.</summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCreateSplashScreen()
    {
    }

    /// <summary>When overridden in a derived class, allows a designer to emit code that configures the splash screen and main form.</summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCreateMainForm()
    {
    }

    /// <summary>When overridden in a derived class, allows for code to run when the application shuts down.</summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnShutdown()
    {
      ShutdownEventHandler shutdownEvent = this.ShutdownEvent;
      if (shutdownEvent == null)
        return;
      shutdownEvent((object) this, EventArgs.Empty);
    }

    /// <summary>When overridden in a derived class, allows for code to run when an unhandled exception occurs in the application.</summary>
    /// <param name="e">
    /// <see cref="T:Ported.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs" />.</param>
    /// <returns>A <see cref="T:System.Boolean" /> that indicates whether the <see cref="E:Ported.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.UnhandledException" /> event was raised.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual bool OnUnhandledException(UnhandledExceptionEventArgs e)
    {
      if (this.m_UnhandledExceptionHandlers == null || this.m_UnhandledExceptionHandlers.Count <= 0)
        return false;
      this.raise_UnhandledException((object) this, e);
      if (e.ExitApplication)
        Application.Exit();
      return true;
    }

    /// <summary>Determines if the application has a splash screen defined, and if it does, displays it.</summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected void ShowSplashScreen()
    {
      if (this.m_DidSplashScreen)
        return;
      this.m_DidSplashScreen = true;
      if (this.m_SplashScreen == null)
        this.OnCreateSplashScreen();
      if (this.m_SplashScreen == null)
        return;
      if (this.m_MinimumSplashExposure > 0)
      {
        this.m_Ok2CloseSplashScreen = false;
        this.m_SplashTimer = new System.Timers.Timer((double) this.m_MinimumSplashExposure);
        this.m_SplashTimer.Elapsed += new ElapsedEventHandler(this.MinimumSplashExposureTimeIsUp);
        this.m_SplashTimer.AutoReset = false;
      }
      else
        this.m_Ok2CloseSplashScreen = true;
      new Thread(new ThreadStart(this.DisplaySplash)).Start();
    }

    /// <summary>Hides the application's splash screen.</summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected void HideSplashScreen()
    {
      object splashLock = this.m_SplashLock;
      ObjectFlowControl.CheckForSyncLockOnValueType(splashLock);
      Monitor.Enter(splashLock);
      try
      {
        if (this.m_SplashScreen != null && !this.m_SplashScreen.IsDisposed)
        {
          this.m_SplashScreen.Invoke((Delegate) new WindowsFormsApplicationBase.DisposeDelegate(((Component) this.m_SplashScreen).Dispose));
          this.m_SplashScreen = (Form) null;
        }
        if (this.MainForm == null)
          return;
        new UIPermission(UIPermissionWindow.AllWindows).Assert();
        this.MainForm.Activate();
        PermissionSet.RevertAssert();
      }
      finally
      {
        Monitor.Exit(splashLock);
      }
    }

    /// <summary>Determines what happens when the application's main form closes.</summary>
    /// <returns>A <see cref="T:Ported.VisualBasic.ApplicationServices.ShutdownMode" /> enumeration value, indicating what the application should do when the main form closes.</returns>
    protected internal ShutdownMode ShutdownStyle
    {
      get
      {
        return this.m_ShutdownStyle;
      }
      set
      {
        this.ValidateShutdownModeEnumValue(value, nameof (value));
        this.m_ShutdownStyle = value;
      }
    }

    /// <summary>Determines whether this application will use the Windows XP styles for windows, controls, and so on.</summary>
    /// <returns>A <see cref="T:System.Boolean" /> value that indicates whether this application will use the XP Windows styles for windows, controls, and so on.</returns>
    protected bool EnableVisualStyles
    {
      get
      {
        return this.m_EnableVisualStyles;
      }
      set
      {
        this.m_EnableVisualStyles = value;
      }
    }

    /// <summary>Determines whether this application is a single-instance application.</summary>
    /// <returns>A <see cref="T:System.Boolean" /> value that indicates whether this application is a single-instance application.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected bool IsSingleInstance
    {
      get
      {
        return this.m_IsSingleInstance;
      }
      set
      {
        this.m_IsSingleInstance = value;
      }
    }

    private void ValidateAuthenticationModeEnumValue(AuthenticationMode value, string paramName)
    {
      switch (value)
      {
        case AuthenticationMode.Windows:
        case AuthenticationMode.ApplicationDefined:
          break;
        default:
          throw new InvalidEnumArgumentException(paramName, (int) value, typeof (AuthenticationMode));
      }
    }

    private void ValidateShutdownModeEnumValue(ShutdownMode value, string paramName)
    {
      switch (value)
      {
        case ShutdownMode.AfterMainFormCloses:
        case ShutdownMode.AfterAllFormsClose:
          break;
        default:
          throw new InvalidEnumArgumentException(paramName, (int) value, typeof (ShutdownMode));
      }
    }

    private void DisplaySplash()
    {
      if (this.m_SplashTimer != null)
        this.m_SplashTimer.Enabled = true;
      Application.Run(this.m_SplashScreen);
    }

    private void MinimumSplashExposureTimeIsUp(object sender, ElapsedEventArgs e)
    {
      if (this.m_SplashTimer != null)
      {
        this.m_SplashTimer.Dispose();
        this.m_SplashTimer = (System.Timers.Timer) null;
      }
      this.m_Ok2CloseSplashScreen = true;
    }

    private void MainFormLoadingDone(object sender, EventArgs e)
    {
      this.MainForm.Load -= new EventHandler(this.MainFormLoadingDone);
      while (!this.m_Ok2CloseSplashScreen)
        this.DoEvents();
      this.HideSplashScreen();
    }

    private void OnUnhandledExceptionEventAdaptor(object sender, ThreadExceptionEventArgs e)
    {
      this.OnUnhandledException(new UnhandledExceptionEventArgs(true, e.Exception));
    }

    private void OnStartupNextInstanceMarshallingAdaptor(object args)
    {
      this.OnStartupNextInstance(new StartupNextInstanceEventArgs((ReadOnlyCollection<string>) args, true));
    }

    private void NetworkAvailableEventAdaptor(object sender, NetworkAvailableEventArgs e)
    {
      this.raise_NetworkAvailabilityChanged(sender, e);
    }

    private string HostName
    {
      get
      {
        if (string.IsNullOrEmpty(this.m_HostName))
        {
          if (Socket.SupportsIPv4)
          {
            this.m_HostName = IPAddress.Loopback.ToString();
          }
          else
          {
            if (!Socket.OSSupportsIPv6)
              throw new RemotingException();
            this.m_HostName = IPAddress.IPv6Loopback.ToString();
          }
        }
        return this.m_HostName;
      }
    }

    private SendOrPostCallback RunNextInstanceDelegate
    {
      get
      {
        return this.m_StartNextInstanceCallback;
      }
    }

    private string ReadUrlFromMemoryMappedFile()
    {
      using (SafeHandleZeroOrMinusOneIsInvalid minusOneIsInvalid = (SafeHandleZeroOrMinusOneIsInvalid) new SafeFileHandle(Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.OpenFileMapping(4, false, this.m_MemoryMappedID), true))
      {
        if (minusOneIsInvalid.IsInvalid)
          return (string) null;
        HandleRef handleRef;
        IntPtr num= IntPtr.Zero;
        try
        {
          handleRef = new HandleRef((object) null, minusOneIsInvalid.DangerousGetHandle());
          num = Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.MapViewOfFile(handleRef, 4, 0, 0, 0);
          if (num == IntPtr.Zero)
            throw ExceptionUtils.GetWin32Exception("AppModel_CantGetMemoryMappedFile");
          return Marshal.PtrToStringUni(num);
        }
        finally
        {
          if (num != IntPtr.Zero)
          {
            handleRef = new HandleRef((object) null, num);
            Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.UnmapViewOfFile(handleRef);
          }
        }
      }
    }

    private void WriteUrlToMemoryMappedFile(string URL)
    {
      HandleRef hFile = new HandleRef((object) null, (IntPtr) (-1));
      using (NativeTypes.SECURITY_ATTRIBUTES lpAttributes = new NativeTypes.SECURITY_ATTRIBUTES())
      {
        lpAttributes.bInheritHandle = false;
        bool securityDescriptor=false;
        try
        {
          new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
          securityDescriptor = Ported.VisualBasic.CompilerServices.NativeMethods.ConvertStringSecurityDescriptorToSecurityDescriptor("D:(A;;GA;;;CO)(A;;GR;;;AU)", 1U, ref lpAttributes.lpSecurityDescriptor, IntPtr.Zero);
          CodeAccessPermission.RevertAssert();
        }
        catch (EntryPointNotFoundException ex)
        {
          lpAttributes.lpSecurityDescriptor = IntPtr.Zero;
        }
        catch (DllNotFoundException ex)
        {
          lpAttributes.lpSecurityDescriptor = IntPtr.Zero;
        }
        if (!securityDescriptor)
          lpAttributes.lpSecurityDescriptor = IntPtr.Zero;
        this.m_FirstInstanceMemoryMappedFileHandle = new SafeFileHandle(Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.CreateFileMapping(hFile, lpAttributes, 4, 0, checked (URL.Length + 1 * 2), this.m_MemoryMappedID), true);
        if (this.m_FirstInstanceMemoryMappedFileHandle.IsInvalid)
          throw ExceptionUtils.GetWin32Exception("AppModel_CantGetMemoryMappedFile");
      }
      HandleRef handleRef;
      IntPtr num=IntPtr.Zero;
      try
      {
        handleRef = new HandleRef((object) null, this.m_FirstInstanceMemoryMappedFileHandle.DangerousGetHandle());
        num = Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.MapViewOfFile(handleRef, 2, 0, 0, 0);
        if (num == IntPtr.Zero)
          throw ExceptionUtils.GetWin32Exception("AppModel_CantGetMemoryMappedFile");
        char[] charArray = URL.ToCharArray();
        Marshal.Copy(charArray, 0, num, charArray.Length);
      }
      finally
      {
        if (num != IntPtr.Zero)
        {
          handleRef = new HandleRef((object) null, num);
          Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.UnmapViewOfFile(handleRef);
        }
      }
    }

    private TcpChannel RegisterChannel(bool SecureChannel)
    {
      PermissionSet permissionSet = new PermissionSet(PermissionState.None);
      permissionSet.AddPermission((IPermission) new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.SerializationFormatter | SecurityPermissionFlag.ControlPrincipal));
      permissionSet.AddPermission((IPermission) new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, this.HostName, 0));
      permissionSet.AddPermission((IPermission) new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME"));
      permissionSet.AddPermission((IPermission) new SecurityPermission(SecurityPermissionFlag.RemotingConfiguration));
      permissionSet.Assert();
      IDictionary properties = (IDictionary) new Hashtable(3);
      properties.Add((object) "bindTo", (object) this.HostName);
      properties.Add((object) "port", (object) 0);
      if (SecureChannel)
      {
        properties.Add((object) "secure", (object) true);
        properties.Add((object) "tokenimpersonationlevel", (object) TokenImpersonationLevel.Impersonation);
        properties.Add((object) "impersonate", (object) true);
      }
      TcpChannel tcpChannel = new TcpChannel(properties, (IClientChannelSinkProvider) null, (IServerChannelSinkProvider) null);
      ChannelServices.RegisterChannel((IChannel) tcpChannel, false);
      PermissionSet.RevertAssert();
      return tcpChannel;
    }

    private void DoApplicationModel()
    {
      StartupEventArgs eventArgs = new StartupEventArgs(this.CommandLineArgs);
      if (!Debugger.IsAttached)
      {
        try
        {
          if (!this.OnInitialize(this.CommandLineArgs) || !this.OnStartup(eventArgs))
            return;
          this.OnRun();
          this.OnShutdown();
        }
        catch (Exception ex)
        {
          if (this.m_ProcessingUnhandledExceptionEvent)
          {
            throw;
          }
          else
          {
            if (this.OnUnhandledException(new UnhandledExceptionEventArgs(true, ex)))
              return;
            throw;
          }
        }
      }
      else
      {
        if (!this.OnInitialize(this.CommandLineArgs) || !this.OnStartup(eventArgs))
          return;
        this.OnRun();
        this.OnShutdown();
      }
    }

    private string GetApplicationInstanceID(Assembly Entry)
    {
      PermissionSet permissionSet = new PermissionSet(PermissionState.None);
      permissionSet.AddPermission((IPermission) new FileIOPermission(PermissionState.Unrestricted));
      permissionSet.AddPermission((IPermission) new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
      permissionSet.Assert();
      Guid libGuidForAssembly = Marshal.GetTypeLibGuidForAssembly(Entry);
      string[] strArray = Entry.GetName().Version.ToString().Split(Conversions.ToCharArrayRankOne("."));
      PermissionSet.RevertAssert();
      return libGuidForAssembly.ToString() + strArray[0] + "." + strArray[1];
    }

    private class WinFormsAppContext : ApplicationContext
    {
      private WindowsFormsApplicationBase m_App;

      public WinFormsAppContext(WindowsFormsApplicationBase App)
      {
        this.m_App = App;
      }

      protected override void OnMainFormClosed(object sender, EventArgs e)
      {
        if (this.m_App.ShutdownStyle == ShutdownMode.AfterMainFormCloses)
        {
          base.OnMainFormClosed(sender, e);
        }
        else
        {
          new UIPermission(UIPermissionWindow.AllWindows).Assert();
          FormCollection openForms = Application.OpenForms;
          PermissionSet.RevertAssert();
          if (openForms.Count > 0)
            this.MainForm = openForms[0];
          else
            base.OnMainFormClosed(sender, e);
        }
      }
    }

    private delegate void DisposeDelegate();

    private class RemoteCommunicator : MarshalByRefObject
    {
      private SendOrPostCallback m_StartNextInstanceDelegate;
      private AsyncOperation m_AsyncOp;
      private WindowsIdentity m_OriginalUser;
      private EventWaitHandle m_ConnectionMadeSemaphore;

      internal RemoteCommunicator(WindowsFormsApplicationBase appObject, EventWaitHandle ConnectionMadeSemaphore)
      {
        new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Assert();
        this.m_OriginalUser = WindowsIdentity.GetCurrent();
        CodeAccessPermission.RevertAssert();
        this.m_AsyncOp = AsyncOperationManager.CreateOperation((object) null);
        this.m_StartNextInstanceDelegate = appObject.RunNextInstanceDelegate;
        this.m_ConnectionMadeSemaphore = ConnectionMadeSemaphore;
      }

      [OneWay]
      public void RunNextInstance(ReadOnlyCollection<string> Args)
      {
        new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Assert();
        if (this.m_OriginalUser.User != WindowsIdentity.GetCurrent().User)
          return;
        this.m_ConnectionMadeSemaphore.Set();
        CodeAccessPermission.RevertAssert();
        this.m_AsyncOp.Post(this.m_StartNextInstanceDelegate, (object) Args);
      }

      public override object InitializeLifetimeService()
      {
        return (object) null;
      }
    }
  }
}

*/