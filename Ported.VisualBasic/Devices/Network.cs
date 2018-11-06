// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.Network
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Ported.VisualBasic.CompilerServices;
using Ported.VisualBasic.FileIO;
using Ported.VisualBasic.MyServices.Internal;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides a property, event, and methods for interacting with the network to which the computer is connected.</summary>
  //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class Network
  {
    private byte[] m_PingBuffer;
    private const int BUFFER_SIZE = 32;
    private const int DEFAULT_TIMEOUT = 100000;
    private const int DEFAULT_PING_TIMEOUT = 1000;
    private const string DEFAULT_USERNAME = "";
    private const string DEFAULT_PASSWORD = "";
    private bool m_Connected;
    private object m_SyncObject;
    private ArrayList m_NetworkAvailabilityEventHandlers;
    private SynchronizationContext m_SynchronizationContext;
    private SendOrPostCallback m_NetworkAvailabilityChangedCallback;

    /// <summary>Occurs when the network availability changes.</summary>
    public event NetworkAvailableEventHandler NetworkAvailabilityChanged
    {
      add
      {
        try
        {
          this.m_Connected = this.IsAvailable;
        }
        catch (SecurityException ex)
        {
          return;
        }
        catch (PlatformNotSupportedException ex)
        {
          return;
        }
        object syncObject = this.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          if (this.m_NetworkAvailabilityEventHandlers == null)
            this.m_NetworkAvailabilityEventHandlers = new ArrayList();
          this.m_NetworkAvailabilityEventHandlers.Add((object) value);
          if (this.m_NetworkAvailabilityEventHandlers.Count != 1)
            return;
          this.m_NetworkAvailabilityChangedCallback = new SendOrPostCallback(this.NetworkAvailabilityChangedHandler);
          if (AsyncOperationManager.SynchronizationContext == null)
            return;
          this.m_SynchronizationContext = AsyncOperationManager.SynchronizationContext;
          try
          {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(this.OS_NetworkAvailabilityChangedListener);
          }
          catch (PlatformNotSupportedException ex)
          {
          }
          catch (NetworkInformationException ex)
          {
          }
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }
      remove
      {
        if (this.m_NetworkAvailabilityEventHandlers == null || this.m_NetworkAvailabilityEventHandlers.Count <= 0)
          return;
        this.m_NetworkAvailabilityEventHandlers.Remove((object) value);
        if (this.m_NetworkAvailabilityEventHandlers.Count != 0)
          return;
        NetworkChange.NetworkAddressChanged -= new NetworkAddressChangedEventHandler(this.OS_NetworkAvailabilityChangedListener);
        this.DisconnectListener();
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
          if (availabilityEventHandler != null)
            availabilityEventHandler(sender, e);
        }
      }
      finally
      {
        IEnumerator enumerator=null;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.Devices.Network" /> class. </summary>
    public Network()
    {
      this.m_SyncObject = new object();
    }

    /// <summary>Indicates whether a computer is connected to a network.</summary>
    /// <returns>
    /// <see langword="True" /> if the computer is connected to a network; otherwise <see langword="False" />.</returns>
    public bool IsAvailable
    {
      get
      {
        return NetworkInterface.GetIsNetworkAvailable();
      }
    }

    /// <summary>Pings the specified server.</summary>
    /// <param name="hostNameOrAddress">The URL, computer name, or IP number of the server to ping.</param>
    /// <returns>
    /// <see langword="True" /> if the operation was successful; otherwise <see langword="False" />.</returns>
    /// <exception cref="T:System.InvalidOperationException">No network connection is available.</exception>
    /// <exception cref="T:System.Net.NetworkInformation.PingException">URL was not valid.</exception>
    public bool Ping(string hostNameOrAddress)
    {
      return this.Ping(hostNameOrAddress, 1000);
    }

    /// <summary>Pings the specified server.</summary>
    /// <param name="address">The URI of the server to ping.</param>
    /// <returns>
    /// <see langword="True" /> if the operation was successful; otherwise <see langword="False" />.</returns>
    /// <exception cref="T:System.InvalidOperationException">No network connection is available.</exception>
    /// <exception cref="T:System.Net.NetworkInformation.PingException">URL was not valid.</exception>
    public bool Ping(Uri address)
    {
      if ((object) address == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (address));
      return this.Ping(address.Host, 1000);
    }

    /// <summary>Pings the specified server.</summary>
    /// <param name="hostNameOrAddress">The URL, computer name, or IP number of the server to ping.</param>
    /// <param name="timeout">Time threshold in milliseconds for contacting the destination. Default is 500.</param>
    /// <returns>
    /// <see langword="True" /> if the operation was successful; otherwise <see langword="False" />.</returns>
    /// <exception cref="T:System.InvalidOperationException">No network connection is available.</exception>
    /// <exception cref="T:System.Net.NetworkInformation.PingException">URL was not valid.</exception>
    public bool Ping(string hostNameOrAddress, int timeout)
    {
      if (!this.IsAvailable)
        throw ExceptionUtils.GetInvalidOperationException("Network_NetworkNotAvailable");
      return new Ping().Send(hostNameOrAddress, timeout, this.PingBuffer).Status == IPStatus.Success;
    }

    /// <summary>Pings the specified server.</summary>
    /// <param name="address">The URI of the server to ping.</param>
    /// <param name="timeout">Time threshold in milliseconds for contacting the destination. Default is 500.</param>
    /// <returns>
    /// <see langword="True" /> if the operation was successful; otherwise <see langword="False" />.</returns>
    /// <exception cref="T:System.InvalidOperationException">No network connection is available.</exception>
    /// <exception cref="T:System.Net.NetworkInformation.PingException">URL was not valid.</exception>
    public bool Ping(Uri address, int timeout)
    {
      if ((object) address == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (address));
      return this.Ping(address.Host, timeout);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the default timeout (100 seconds).</exception>
    /// <exception cref="T:System.Security.SecurityException">The target web site requires user credentials.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(string address, string destinationFileName)
    {
      this.DownloadFile(address, destinationFileName, "", "", false, 100000, false);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the default timeout (100 seconds).</exception>
    /// <exception cref="T:System.Security.SecurityException">The target web site requires user credentials.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(Uri address, string destinationFileName)
    {
      this.DownloadFile(address, destinationFileName, "", "", false, 100000, false);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string, "". </param>
    /// <param name="password">Password to authenticate. Default is an empty string, "". </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the default timeout (100 seconds).</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(string address, string destinationFileName, string userName, string password)
    {
      this.DownloadFile(address, destinationFileName, userName, password, false, 100000, false);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string, "". </param>
    /// <param name="password">Password to authenticate. Default is an empty string, "". </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the default timeout (100 seconds).</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(Uri address, string destinationFileName, string userName, string password)
    {
      this.DownloadFile(address, destinationFileName, userName, password, false, 100000, false);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string, "". </param>
    /// <param name="password">Password to authenticate. Default is an empty string, "". </param>
    /// <param name="showUI">
    /// <see langword="True" /> to display the progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">
    /// <see cref="T:System.Int32" />. Timeout interval, in milliseconds. Default is 100 seconds. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> to overwrite existing files; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="overwrite" /> is set to <see langword="False" /> and the destination file already exists.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(string address, string destinationFileName, string userName, string password, bool showUI, int connectionTimeout, bool overwrite)
    {
      this.DownloadFile(address, destinationFileName, userName, password, showUI, connectionTimeout, overwrite, UICancelOption.ThrowException);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string, "". </param>
    /// <param name="password">Password to authenticate. Default is an empty string, "". </param>
    /// <param name="showUI">
    /// <see langword="True" /> to display the progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval, in milliseconds. Default is 100 seconds. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> to overwrite existing files; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="onUserCancel">Specifies behavior when the user clicks Cancel or No on the dialog box shown as a result of <paramref name="ShowUI" /> set to <see langword="True" />. Default is <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />. </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="overwrite" /> is set to <see langword="False" /> and the destination file already exists.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(string address, string destinationFileName, string userName, string password, bool showUI, int connectionTimeout, bool overwrite, UICancelOption onUserCancel)
    {
      if (string.IsNullOrEmpty(address) || Operators.CompareString(address.Trim(), "", false) == 0)
        throw ExceptionUtils.GetArgumentNullException(nameof (address));
      Uri uri = this.GetUri(address.Trim());
      ICredentials networkCredentials = this.GetNetworkCredentials(userName, password);
      this.DownloadFile(uri, destinationFileName, networkCredentials, showUI, connectionTimeout, overwrite, onUserCancel);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string, "". </param>
    /// <param name="password">Password to authenticate. Default is an empty string, "". </param>
    /// <param name="showUI">
    /// <see langword="True" /> to display the progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval, in milliseconds. Default is 100 seconds. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> to overwrite existing files; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="overwrite" /> is set to <see langword="False" /> and the destination file already exists.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(Uri address, string destinationFileName, string userName, string password, bool showUI, int connectionTimeout, bool overwrite)
    {
      this.DownloadFile(address, destinationFileName, userName, password, showUI, connectionTimeout, overwrite, UICancelOption.ThrowException);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string, "". </param>
    /// <param name="password">Password to authenticate. Default is an empty string, "". </param>
    /// <param name="showUI">
    /// <see langword="True" /> to display the progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval, in milliseconds. Default is 100 seconds. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> to overwrite existing files; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="onUserCancel">Specifies behavior when the user clicks Cancel or No on the dialog box shown as a result of <paramref name="ShowUI" /> set to <see langword="True" />. Default is <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />. </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="overwrite" /> is set to <see langword="False" /> and the destination file already exists.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(Uri address, string destinationFileName, string userName, string password, bool showUI, int connectionTimeout, bool overwrite, UICancelOption onUserCancel)
    {
      ICredentials networkCredentials = this.GetNetworkCredentials(userName, password);
      this.DownloadFile(address, destinationFileName, networkCredentials, showUI, connectionTimeout, overwrite, onUserCancel);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">
    /// <see langword="String" /> or <see cref="T:System.Uri" />. Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">
    /// <see langword="String" />. File name and path of the downloaded file. </param>
    /// <param name="networkCredentials">
    /// <see cref="T:System.Net.ICredentials" />. Credentials to be supplied. </param>
    /// <param name="showUI">
    /// <see langword="True" /> to display the progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval, in milliseconds. Default is 100 seconds. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> to overwrite existing files; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="overwrite" /> is set to <see langword="False" /> and the destination file already exists.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(Uri address, string destinationFileName, ICredentials networkCredentials, bool showUI, int connectionTimeout, bool overwrite)
    {
      this.DownloadFile(address, destinationFileName, networkCredentials, showUI, connectionTimeout, overwrite, UICancelOption.ThrowException);
    }

    /// <summary>Downloads the specified remote file and saves it in the specified location.</summary>
    /// <param name="address">Path of the file to download, including file name and host address. </param>
    /// <param name="destinationFileName">File name and path of the downloaded file. </param>
    /// <param name="networkCredentials">Credentials to be supplied. </param>
    /// <param name="showUI">
    /// <see langword="True" /> to display the progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval, in milliseconds. Default is 100 seconds. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> to overwrite existing files; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="onUserCancel">Specifies behavior when the user clicks Cancel or No on the dialog box shown as a result of <paramref name="showUI" /> set to <see langword="True" />. Default is <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />. </param>
    /// <exception cref="T:System.ArgumentException">The drive name is not valid</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> ends with a trailing slash.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="overwrite" /> is set to <see langword="False" /> and the destination file already exists.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void DownloadFile(Uri address, string destinationFileName, ICredentials networkCredentials, bool showUI, int connectionTimeout, bool overwrite, UICancelOption onUserCancel)
    {
      if (connectionTimeout <= 0)
        throw ExceptionUtils.GetArgumentExceptionWithArgName("connectionTimeOut", "Network_BadConnectionTimeout");
      if ((object) address == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (address));
      using (WebClientExtended webClientExtended = new WebClientExtended())
      {
        webClientExtended.Timeout = connectionTimeout;
        webClientExtended.UseNonPassiveFtp = showUI;
        string str = Ported.VisualBasic.FileIO.FileSystem.NormalizeFilePath(destinationFileName, nameof (destinationFileName));
        if (Directory.Exists(str))
          throw ExceptionUtils.GetInvalidOperationException("Network_DownloadNeedsFilename");
        if (System.IO.File.Exists(str) & !overwrite)
          throw new IOException(Utils.GetResourceString("IO_FileExists_Path", new string[1]
          {
            destinationFileName
          }));
        if (networkCredentials != null)
          webClientExtended.Credentials = networkCredentials;
        ProgressDialog dialog = (ProgressDialog) null;
        if (showUI && Environment.UserInteractive)
        {
          new UIPermission(UIPermissionWindow.SafeSubWindows).Demand();
          dialog = new ProgressDialog();
          dialog.Text = Utils.GetResourceString("ProgressDialogDownloadingTitle", new string[1]
          {
            address.AbsolutePath
          });
          dialog.LabelText = Utils.GetResourceString("ProgressDialogDownloadingLabel", address.AbsolutePath, str);
        }
        string directoryName = Path.GetDirectoryName(str);
        if (Operators.CompareString(directoryName, "", false) == 0)
          throw ExceptionUtils.GetInvalidOperationException("Network_DownloadNeedsFilename");
        if (!Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
        new WebClientCopy((WebClient) webClientExtended, dialog).DownloadFile(address, str);
        if (showUI && Environment.UserInteractive && onUserCancel == UICancelOption.ThrowException & dialog.UserCanceledTheDialog)
          throw new OperationCanceledException();
      }
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">The target server requires user credentials.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the default timeout (100 seconds).</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, string address)
    {
      this.UploadFile(sourceFileName, address, "", "", false, 100000);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">The target server requires user credentials.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the default timeout (100 seconds).</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, Uri address)
    {
      this.UploadFile(sourceFileName, address, "", "", false, 100000);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string: "".</param>
    /// <param name="password">Password to authenticate. Default is an empty string: "".</param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="connectionTimeout" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">Authentication failed.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the default timeout (100 seconds).</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, string address, string userName, string password)
    {
      this.UploadFile(sourceFileName, address, userName, password, false, 100000);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string: "".</param>
    /// <param name="password">Password to authenticate. Default is an empty string: "".</param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="connectionTimeout" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">Authentication failed.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the default timeout (100 seconds).</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, Uri address, string userName, string password)
    {
      this.UploadFile(sourceFileName, address, userName, password, false, 100000);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string: "".</param>
    /// <param name="password">Password to authenticate. Default is an empty string: "".</param>
    /// <param name="showUI">
    /// <see langword="True" /> to display progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval in milliseconds. Default is 100 seconds. </param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="connectionTimeout" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">Authentication failed.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, string address, string userName, string password, bool showUI, int connectionTimeout)
    {
      this.UploadFile(sourceFileName, address, userName, password, showUI, connectionTimeout, UICancelOption.ThrowException);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string: "".</param>
    /// <param name="password">Password to authenticate. Default is an empty string: "".</param>
    /// <param name="showUI">Whether to display progress of the operation. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval in milliseconds. Default is 100 seconds. </param>
    /// <param name="onUserCancel">Action to be taken when the user clicks Cancel. Default is <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />.</param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="connectionTimeout" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">Authentication failed.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, string address, string userName, string password, bool showUI, int connectionTimeout, UICancelOption onUserCancel)
    {
      if (string.IsNullOrEmpty(address) || Operators.CompareString(address.Trim(), "", false) == 0)
        throw ExceptionUtils.GetArgumentNullException(nameof (address));
      Uri uri = this.GetUri(address.Trim());
      if (Operators.CompareString(Path.GetFileName(uri.AbsolutePath), "", false) == 0)
        throw ExceptionUtils.GetInvalidOperationException("Network_UploadAddressNeedsFilename");
      this.UploadFile(sourceFileName, uri, userName, password, showUI, connectionTimeout, onUserCancel);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string: "".</param>
    /// <param name="password">Password to authenticate. Default is an empty string: "".</param>
    /// <param name="showUI">
    /// <see langword="True" /> to display progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval in milliseconds. Default is 100 seconds. </param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="connectionTimeout" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">Authentication failed.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, Uri address, string userName, string password, bool showUI, int connectionTimeout)
    {
      this.UploadFile(sourceFileName, address, userName, password, showUI, connectionTimeout, UICancelOption.ThrowException);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <param name="userName">User name to authenticate. Default is an empty string: "".</param>
    /// <param name="password">Password to authenticate. Default is an empty string: "".</param>
    /// <param name="showUI">Whether to display progress of the operation. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval in milliseconds. Default is 100 seconds. </param>
    /// <param name="onUserCancel">Action to be taken when the user clicks Cancel. Default is <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />.</param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="connectionTimeout" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">Authentication failed.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, Uri address, string userName, string password, bool showUI, int connectionTimeout, UICancelOption onUserCancel)
    {
      ICredentials networkCredentials = this.GetNetworkCredentials(userName, password);
      this.UploadFile(sourceFileName, address, networkCredentials, showUI, connectionTimeout, onUserCancel);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <param name="networkCredentials">Credentials for authentication. </param>
    /// <param name="showUI">
    /// <see langword="True" /> to display progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval in milliseconds. Default is 100 seconds. </param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="connectionTimeout" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">Authentication failed.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, Uri address, ICredentials networkCredentials, bool showUI, int connectionTimeout)
    {
      this.UploadFile(sourceFileName, address, networkCredentials, showUI, connectionTimeout, UICancelOption.ThrowException);
    }

    /// <summary>Sends the specified file to the specified host address.</summary>
    /// <param name="sourceFileName">Path and name of file to upload. </param>
    /// <param name="address">URL, IP address, or URI of destination server. </param>
    /// <param name="networkCredentials">Credentials for authentication. </param>
    /// <param name="showUI">
    /// <see langword="True" /> to display progress of the operation; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <param name="connectionTimeout">Timeout interval in milliseconds. Default is 100 seconds. </param>
    /// <param name="onUserCancel">Action to be taken when the user clicks Cancel. Default is <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />.</param>
    /// <exception cref="T:System.ArgumentException">The source file path is not valid.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="connectionTimeout" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="address" /> does not include a file name.</exception>
    /// <exception cref="T:System.Security.SecurityException">Authentication failed.</exception>
    /// <exception cref="T:System.TimeoutException">The server does not respond within the specified <paramref name="connectionTimeout" />.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user authentication fails.</exception>
    /// <exception cref="T:System.Security.SecurityException">User lacks necessary permissions to perform a network operation.</exception>
    /// <exception cref="T:System.Net.WebException">The request is denied by the target web server.</exception>
    public void UploadFile(string sourceFileName, Uri address, ICredentials networkCredentials, bool showUI, int connectionTimeout, UICancelOption onUserCancel)
    {
      sourceFileName = Ported.VisualBasic.FileIO.FileSystem.NormalizeFilePath(sourceFileName, nameof (sourceFileName));
      if (!System.IO.File.Exists(sourceFileName))
        throw new FileNotFoundException(Utils.GetResourceString("IO_FileNotFound_Path", new string[1]
        {
          sourceFileName
        }));
      if (connectionTimeout <= 0)
        throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (connectionTimeout), "Network_BadConnectionTimeout");
      if ((object) address == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (address));
      using (WebClientExtended webClientExtended = new WebClientExtended())
      {
        webClientExtended.Timeout = connectionTimeout;
        if (networkCredentials != null)
          webClientExtended.Credentials = networkCredentials;
        ProgressDialog dialog = (ProgressDialog) null;
        if (showUI && Environment.UserInteractive)
        {
          dialog = new ProgressDialog();
          dialog.Text = Utils.GetResourceString("ProgressDialogUploadingTitle", new string[1]
          {
            sourceFileName
          });
          dialog.LabelText = Utils.GetResourceString("ProgressDialogUploadingLabel", sourceFileName, address.AbsolutePath);
        }
        new WebClientCopy((WebClient) webClientExtended, dialog).UploadFile(sourceFileName, address);
        if (showUI && Environment.UserInteractive && onUserCancel == UICancelOption.ThrowException & dialog.UserCanceledTheDialog)
          throw new OperationCanceledException();
      }
    }

    internal void DisconnectListener()
    {
      NetworkChange.NetworkAddressChanged -= new NetworkAddressChangedEventHandler(this.OS_NetworkAvailabilityChangedListener);
    }

    private void OS_NetworkAvailabilityChangedListener(object sender, EventArgs e)
    {
      object syncObject = this.m_SyncObject;
      ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
      Monitor.Enter(syncObject);
      try
      {
        this.m_SynchronizationContext.Post(this.m_NetworkAvailabilityChangedCallback, (object) null);
      }
      finally
      {
        Monitor.Exit(syncObject);
      }
    }

    private void NetworkAvailabilityChangedHandler(object state)
    {
      bool isAvailable = this.IsAvailable;
      if (this.m_Connected == isAvailable)
        return;
      this.m_Connected = isAvailable;
      this.raise_NetworkAvailabilityChanged((object) this, new NetworkAvailableEventArgs(isAvailable));
    }

    private byte[] PingBuffer
    {
      get
      {
        if (this.m_PingBuffer == null)
        {
          this.m_PingBuffer = new byte[32];
          int index = 0;
          do
          {
            this.m_PingBuffer[index] = Convert.ToByte((object) checked (97 + unchecked (index % 23)), (IFormatProvider) CultureInfo.InvariantCulture);
            checked { ++index; }
          }
          while (index <= 31);
        }
        return this.m_PingBuffer;
      }
    }

    private Uri GetUri(string address)
    {
      try
      {
        return new Uri(address);
      }
      catch (UriFormatException ex)
      {
        throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (address), "Network_InvalidUriString", address);
      }
    }

    private ICredentials GetNetworkCredentials(string userName, string password)
    {
      if (userName == null)
        userName = "";
      if (password == null)
        password = "";
      if (Operators.CompareString(userName, "", false) == 0 & Operators.CompareString(password, "", false) == 0)
        return (ICredentials) null;
      return (ICredentials) new NetworkCredential(userName, password);
    }
  }
}

*/