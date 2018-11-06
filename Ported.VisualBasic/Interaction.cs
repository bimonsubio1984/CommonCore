// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Interaction
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll



using Ported.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
//using System.Windows.Forms;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="Interaction" /> module contains procedures used to interact with objects, applications, and systems. </summary>
  [StandardModule]
  public sealed class Interaction
  {
    /*
    private static object m_EnvironSyncObject = new object();
    private static SortedList m_SortedEnvList;
    private static string m_CommandLine;

    /// <summary>Runs an executable program and returns an integer containing the program's process ID if it is still running.</summary>
    /// <param name="PathName">Required. <see langword="String" />. Name of the program to execute, together with any required arguments and command-line switches. <paramref name="PathName" /> can also include the drive and the directory path or folder.If you do not know the path to the program, you can use the <see cref="Overload:Ported.VisualBasic.FileIO.FileSystem.GetFiles" /> to locate it. For example, you can call My.Computer.FileSystem.GetFiles("C:\", True, "testFile.txt"), which returns the full path of every file named testFile.txt anywhere on drive C:\.</param>
    /// <param name="Style">Optional. <see langword="AppWinStyle" />. A value chosen from the <see cref="T:Ported.VisualBasic.AppWinStyle" /> specifying the style of the window in which the program is to run. If <paramref name="Style" /> is omitted, <see langword="Shell" /> uses <see langword="AppWinStyle.MinimizedFocus" />, which starts the program minimized and with focus. </param>
    /// <param name="Wait">Optional. <see langword="Boolean" />. A value indicating whether the <see langword="Shell" /> function should wait for completion of the program. If <paramref name="Wait" /> is omitted, <see langword="Shell" /> uses <see langword="False" />.</param>
    /// <param name="Timeout">Optional. <see langword="Integer" />. The number of milliseconds to wait for completion if <paramref name="Wait" /> is <see langword="True" />. If <paramref name="Timeout" /> is omitted, <see langword="Shell" /> uses -1, which means there is no timeout and <see langword="Shell" /> does not return until the program finishes. Therefore, if you omit <paramref name="Timeout" /> or set it to -1, it is possible that <see langword="Shell" /> might never return control to your program.</param>
    /// <returns>Runs an executable program and returns an integer containing the program's process ID if it is still running.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Style" /> is not within range 0 through 9, inclusive.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <see langword="Shell" /> cannot find the <paramref name="PathName" /> file.</exception>
    /// <exception cref="T:System.NullReferenceException">
    /// <paramref name="PathName" /> is <see langword="Nothing" />.</exception>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static int Shell(string PathName, AppWinStyle Style = AppWinStyle.MinimizedFocus, bool Wait = false, int Timeout = -1)
    {
      NativeTypes.STARTUPINFO lpStartupInfo = new NativeTypes.STARTUPINFO();
      NativeTypes.PROCESS_INFORMATION lpProcessInformation = new NativeTypes.PROCESS_INFORMATION();
      try
      {
        new UIPermission(UIPermissionWindow.AllWindows).Demand();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      if (PathName == null)
        throw new NullReferenceException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          "Pathname"
        }));
      switch (Style)
      {
        case AppWinStyle.Hide:
        case AppWinStyle.NormalFocus:
        case AppWinStyle.MinimizedFocus:
        case AppWinStyle.MaximizedFocus:
        case AppWinStyle.NormalNoFocus:
        case AppWinStyle.NormalFocus | AppWinStyle.NormalNoFocus:
        case AppWinStyle.MinimizedNoFocus:
        case AppWinStyle.MaximizedFocus | AppWinStyle.NormalNoFocus:
        case (AppWinStyle) 8:
        case (AppWinStyle) 9:
          Ported.VisualBasic.CompilerServices.NativeMethods.GetStartupInfo(lpStartupInfo);
          try
          {
            lpStartupInfo.dwFlags = 1;
            lpStartupInfo.wShowWindow = (short) Style;
            IntPtr lpEnvironment= IntPtr.Zero;
            int process = Ported.VisualBasic.CompilerServices.NativeMethods.CreateProcess((string) null, PathName, (NativeTypes.SECURITY_ATTRIBUTES) null, (NativeTypes.SECURITY_ATTRIBUTES) null, false, 32, lpEnvironment, (string) null, lpStartupInfo, lpProcessInformation);
            try
            {
              if (process != 0)
              {
                if (Wait)
                {
                  if (Ported.VisualBasic.CompilerServices.NativeMethods.WaitForSingleObject(lpProcessInformation.hProcess, Timeout) == 0)
                    return 0;
                  return lpProcessInformation.dwProcessId;
                }
                Ported.VisualBasic.CompilerServices.NativeMethods.WaitForInputIdle(lpProcessInformation.hProcess, 10000);
                return lpProcessInformation.dwProcessId;
              }
              if (Marshal.GetLastWin32Error() == 5)
                throw ExceptionUtils.VbMakeException(70);
              throw ExceptionUtils.VbMakeException(53);
            }
            finally
            {
              ((IDisposable)lpProcessInformation).Dispose();
            }
          }
          finally
          {
            ((IDisposable)lpStartupInfo).Dispose();
          }
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            nameof (Style)
          }));
      }
    }

    /// <summary>Activates an application that is already running.</summary>
    /// <param name="ProcessId">
    /// <see langword="Integer" /> specifying the Win32 process ID number assigned to this process. You can use the ID returned by the <see cref="M:Ported.VisualBasic.Interaction.Shell(System.String,Ported.VisualBasic.AppWinStyle,System.Boolean,System.Int32)" />, provided it is not zero.</param>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static void AppActivate(int ProcessId)
    {
      IntPtr window;
      int lpdwProcessId=0;
      for (window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(Ported.VisualBasic.CompilerServices.NativeMethods.GetDesktopWindow(), 5); window != IntPtr.Zero; window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(window, 2))
      {
        Ported.VisualBasic.CompilerServices.SafeNativeMethods.GetWindowThreadProcessId(window, ref lpdwProcessId);
        if (lpdwProcessId == ProcessId && Ported.VisualBasic.CompilerServices.SafeNativeMethods.IsWindowEnabled(window) && Ported.VisualBasic.CompilerServices.SafeNativeMethods.IsWindowVisible(window))
          break;
      }
      if (window == IntPtr.Zero)
      {
        for (window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(Ported.VisualBasic.CompilerServices.NativeMethods.GetDesktopWindow(), 5); window != IntPtr.Zero; window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(window, 2))
        {
          Ported.VisualBasic.CompilerServices.SafeNativeMethods.GetWindowThreadProcessId(window, ref lpdwProcessId);
          if (lpdwProcessId == ProcessId)
            break;
        }
      }
      if (window == IntPtr.Zero)
        throw new ArgumentException(Utils.GetResourceString("ProcessNotFound", new string[1]
        {
          Conversions.ToString(ProcessId)
        }));
      Interaction.AppActivateHelper(window);
    }

    /// <summary>Activates an application that is already running.</summary>
    /// <param name="Title">
    /// <see langword="String" /> expression specifying the title in the title bar of the application you want to activate. You can use the title assigned to the application when it was launched.</param>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static void AppActivate(string Title)
    {
      string lpClassName = (string) null;
      IntPtr window = Ported.VisualBasic.CompilerServices.NativeMethods.FindWindow(ref lpClassName, ref Title);
      if (window == IntPtr.Zero)
      {
        string empty = string.Empty;
        StringBuilder lpString = new StringBuilder(511);
        int num = Strings.Len(Title);
        for (window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(Ported.VisualBasic.CompilerServices.NativeMethods.GetDesktopWindow(), 5); window != IntPtr.Zero; window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(window, 2))
        {
          int windowText = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindowText(window, lpString, lpString.Capacity);
          string strA = lpString.ToString();
          if (windowText >= num && string.Compare(strA, 0, Title, 0, num, StringComparison.OrdinalIgnoreCase) == 0)
            break;
        }
        if (window == IntPtr.Zero)
        {
          for (window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(Ported.VisualBasic.CompilerServices.NativeMethods.GetDesktopWindow(), 5); window != IntPtr.Zero; window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(window, 2))
          {
            int windowText = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindowText(window, lpString, lpString.Capacity);
            string str = lpString.ToString();
            if (windowText >= num && string.Compare(Strings.Right(str, num), 0, Title, 0, num, StringComparison.OrdinalIgnoreCase) == 0)
              break;
          }
        }
      }
      if (window == IntPtr.Zero)
        throw new ArgumentException(Utils.GetResourceString("ProcessNotFound", new string[1]
        {
          Title
        }));
      Interaction.AppActivateHelper(window);
    }

    private static void AppActivateHelper(IntPtr hwndApp)
    {
      try
      {
        new UIPermission(UIPermissionWindow.AllWindows).Demand();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      if (!Ported.VisualBasic.CompilerServices.SafeNativeMethods.IsWindowEnabled(hwndApp) || !Ported.VisualBasic.CompilerServices.SafeNativeMethods.IsWindowVisible(hwndApp))
      {
        IntPtr window;
        for (window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(hwndApp, 0); window != IntPtr.Zero; window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(window, 2))
        {
          if (Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(window, 4) == hwndApp)
          {
            if (!Ported.VisualBasic.CompilerServices.SafeNativeMethods.IsWindowEnabled(window) || !Ported.VisualBasic.CompilerServices.SafeNativeMethods.IsWindowVisible(window))
            {
              hwndApp = window;
              window = Ported.VisualBasic.CompilerServices.NativeMethods.GetWindow(hwndApp, 0);
            }
            else
              break;
          }
        }
        if (window == IntPtr.Zero)
          throw new ArgumentException(Utils.GetResourceString("ProcessNotFound"));
        hwndApp = window;
      }
      int lpdwProcessId=0;
      Ported.VisualBasic.CompilerServices.NativeMethods.AttachThreadInput(0, Ported.VisualBasic.CompilerServices.SafeNativeMethods.GetWindowThreadProcessId(hwndApp, ref lpdwProcessId), 1);
      Ported.VisualBasic.CompilerServices.NativeMethods.SetForegroundWindow(hwndApp);
      Ported.VisualBasic.CompilerServices.NativeMethods.SetFocus(hwndApp);
      Ported.VisualBasic.CompilerServices.NativeMethods.AttachThreadInput(0, Ported.VisualBasic.CompilerServices.SafeNativeMethods.GetWindowThreadProcessId(hwndApp, ref lpdwProcessId), 0);
    }

    /// <summary>Returns the argument portion of the command line used to start Visual Basic or an executable program developed with Visual Basic. The <see langword="My" /> feature provides greater productivity and performance than the <see langword="Command " />function. For more information, see <see cref="P:Ported.VisualBasic.ApplicationServices.ConsoleApplicationBase.CommandLineArgs" />.</summary>
    /// <returns>Returns the argument portion of the command line used to start Visual Basic or an executable program developed with Visual Basic.The <see langword="My" /> feature provides greater productivity and performance than the <see langword="Command " />function. For more information, see <see cref="P:Ported.VisualBasic.ApplicationServices.ConsoleApplicationBase.CommandLineArgs" />.</returns>
    public static string Command()
    {
      new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
      if (Interaction.m_CommandLine == null)
      {
        string str = Environment.CommandLine;
        if (str == null || str.Length == 0)
          return "";
        int length = Environment.GetCommandLineArgs()[0].Length;
        int startIndex=0;
        do
        {
          startIndex = str.IndexOf('"', startIndex);
          if (startIndex >= 0 && startIndex <= length)
            str = str.Remove(startIndex, 1);
        }
        while (startIndex >= 0 && startIndex <= length);
        Interaction.m_CommandLine = startIndex == 0 || startIndex > str.Length ? "" : Strings.LTrim(str.Substring(length));
      }
      return Interaction.m_CommandLine;
    }

    /// <summary>Returns the string associated with an operating-system environment variable. </summary>
    /// <param name="Expression">Required. Expression that evaluates either a string containing the name of an environment variable, or an integer corresponding to the numeric order of an environment string in the environment-string table.</param>
    /// <returns>Returns the string associated with an operating-system environment variable. </returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Expression" /> is missing.</exception>
    public static string Environ(int Expression)
    {
      if (Expression <= 0 || Expression > (int) byte.MaxValue)
        throw new ArgumentException(Utils.GetResourceString("Argument_Range1toFF1", new string[1]
        {
          nameof (Expression)
        }));
      if (Interaction.m_SortedEnvList == null)
      {
        object environSyncObject = Interaction.m_EnvironSyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(environSyncObject);
        Monitor.Enter(environSyncObject);
        try
        {
          if (Interaction.m_SortedEnvList == null)
          {
            new EnvironmentPermission(PermissionState.Unrestricted).Assert();
            Interaction.m_SortedEnvList = new SortedList(Environment.GetEnvironmentVariables());
            PermissionSet.RevertAssert();
          }
        }
        finally
        {
          Monitor.Exit(environSyncObject);
        }
      }
      if (Expression > Interaction.m_SortedEnvList.Count)
        return "";
      string pathList = Interaction.m_SortedEnvList.GetKey(checked (Expression - 1)).ToString();
      string str = Interaction.m_SortedEnvList.GetByIndex(checked (Expression - 1)).ToString();
      new EnvironmentPermission(EnvironmentPermissionAccess.Read, pathList).Demand();
      return pathList + "=" + str;
    }

    /// <summary>Returns the string associated with an operating-system environment variable. </summary>
    /// <param name="Expression">Required. Expression that evaluates either a string containing the name of an environment variable, or an integer corresponding to the numeric order of an environment string in the environment-string table.</param>
    /// <returns>Returns the string associated with an operating-system environment variable. </returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Expression" /> is missing.</exception>
    public static string Environ(string Expression)
    {
      Expression = Strings.Trim(Expression);
      if (Expression.Length == 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Expression)
        }));
      return Environment.GetEnvironmentVariable(Expression);
    }

    /// <summary>Sounds a tone through the computer's speaker.</summary>
    public static void Beep()
    {
      try
      {
        new UIPermission(UIPermissionWindow.SafeSubWindows).Demand();
      }
      catch (SecurityException ex1)
      {
        try
        {
          new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        }
        catch (SecurityException ex2)
        {
          return;
        }
      }
      Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.MessageBeep(0);
    }

    /// <summary>Displays a prompt in a dialog box, waits for the user to input text or click a button, and then returns a string containing the contents of the text box.</summary>
    /// <param name="Prompt">Required <see langword="String" /> expression displayed as the message in the dialog box. The maximum length of <paramref name="Prompt" /> is approximately 1024 characters, depending on the width of the characters used. If <paramref name="Prompt" /> consists of more than one line, you can separate the lines using a carriage return character (<see langword="Chr(" />13<see langword=")" />), a line feed character (<see langword="Chr(" />10<see langword=")" />), or a carriage return/line feed combination (<see langword="Chr(" />13<see langword=")" /> &amp; <see langword="Chr(" />10<see langword=")" />) between each line.</param>
    /// <param name="Title">Optional. <see langword="String" /> expression displayed in the title bar of the dialog box. If you omit <paramref name="Title" />, the application name is placed in the title bar.</param>
    /// <param name="DefaultResponse">Optional. <see langword="String" /> expression displayed in the text box as the default response if no other input is provided. If you omit <paramref name="DefaultResponse" />, the displayed text box is empty.</param>
    /// <param name="XPos">Optional. Numeric expression that specifies, in twips, the distance of the left edge of the dialog box from the left edge of the screen. If you omit <paramref name="XPos" />, the dialog box is centered horizontally.</param>
    /// <param name="YPos">Optional. Numeric expression that specifies, in twips, the distance of the upper edge of the dialog box from the top of the screen. If you omit <paramref name="YPos" />, the dialog box is positioned vertically approximately one-third of the way down the screen.</param>
    /// <returns>Displays a prompt in a dialog box, waits for the user to input text or click a button, and then returns a string containing the contents of the text box.</returns>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.UI)]
    public static string InputBox(string Prompt, string Title = "", string DefaultResponse = "", int XPos = -1, int YPos = -1)
    {
      IWin32Window ParentWindow = (IWin32Window) null;
      IVbHost vbHost = HostServices.VBHost;
      if (vbHost != null)
        ParentWindow = vbHost.GetParentWindow();
      if (Title.Length == 0)
        Title = vbHost != null ? vbHost.GetWindowTitle() : Interaction.GetTitleFromAssembly(Assembly.GetCallingAssembly());
      if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
        return Interaction.InternalInputBox(Prompt, Title, DefaultResponse, XPos, YPos, ParentWindow);
      Interaction.InputBoxHandler inputBoxHandler = new Interaction.InputBoxHandler(Prompt, Title, DefaultResponse, XPos, YPos, ParentWindow);
      Thread thread = new Thread(new ThreadStart(inputBoxHandler.StartHere));
      thread.Start();
      thread.Join();
      return inputBoxHandler.Result;
    }

    private static string GetTitleFromAssembly(Assembly CallingAssembly)
    {
      try
      {
        return CallingAssembly.GetName().Name;
      }
      catch (SecurityException ex)
      {
        string fullName = CallingAssembly.FullName;
        int length = fullName.IndexOf(',');
        if (length >= 0)
          return fullName.Substring(0, length);
        return "";
      }
    }

    private static string InternalInputBox(string Prompt, string Title, string DefaultResponse, int XPos, int YPos, IWin32Window ParentWindow)
    {
      VBInputBox vbInputBox = new VBInputBox(Prompt, Title, DefaultResponse, XPos, YPos);
      int num = (int) vbInputBox.ShowDialog(ParentWindow);
      string output = vbInputBox.Output;
      vbInputBox.Dispose();
      return output;
    }

    /// <summary>Displays a message in a dialog box, waits for the user to click a button, and then returns an integer indicating which button the user clicked.</summary>
    /// <param name="Prompt">Required. <see langword="String" /> expression displayed as the message in the dialog box. The maximum length of <paramref name="Prompt" /> is approximately 1024 characters, depending on the width of the characters used. If <paramref name="Prompt" /> consists of more than one line, you can separate the lines using a carriage return character (<see langword="Chr(" />13<see langword=")" />), a line feed character (<see langword="Chr(" />10<see langword=")" />), or a carriage return/linefeed character combination (<see langword="Chr(" />13<see langword=")" /> &amp; <see langword="Chr(" />10<see langword=")" />) between each line.</param>
    /// <param name="Buttons">Optional. Numeric expression that is the sum of values specifying the number and type of buttons to display, the icon style to use, the identity of the default button, and the modality of the message box. If you omit <paramref name="Buttons" />, the default value is zero.</param>
    /// <param name="Title">Optional. <see langword="String" /> expression displayed in the title bar of the dialog box. If you omit <paramref name="Title" />, the application name is placed in the title bar.</param>
    /// <returns>ConstantValue
    ///   <see langword="OK" />
    /// 1
    ///   <see langword="Cancel" />
    /// 2
    ///   <see langword="Abort" />
    /// 3
    ///   <see langword="Retry" />
    /// 4
    ///   <see langword="Ignore" />
    /// 5
    ///   <see langword="Yes" />
    /// 6
    ///   <see langword="No" />
    /// 7</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Prompt" /> is not a <see langword="String" /> expression, or <paramref name="Title" /> is invalid.</exception>
    /// <exception cref="T:System.InvalidOperationException">Process is not running in User Interactive mode.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">One or more parameters not a member of <see langword="MsgBoxResult" /> or <see langword="MsgBoxStyle" /> enumeration.</exception>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.UI)]
    public static MsgBoxResult MsgBox(object Prompt, MsgBoxStyle Buttons = MsgBoxStyle.OkOnly, object Title = null)
    {
      string text = (string) null;
      IWin32Window owner = (IWin32Window) null;
      IVbHost vbHost = HostServices.VBHost;
      if (vbHost != null)
        owner = vbHost.GetParentWindow();
      if ((Buttons & (MsgBoxStyle) 15) <= MsgBoxStyle.RetryCancel && (Buttons & (MsgBoxStyle) 240) <= MsgBoxStyle.Information)
      {
        if ((Buttons & (MsgBoxStyle) 3840) <= MsgBoxStyle.DefaultButton3)
          goto label_5;
      }
      Buttons = MsgBoxStyle.OkOnly;
label_5:
      try
      {
        if (Prompt != null)
          text = (string) Conversions.ChangeType(Prompt, typeof (string));
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
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValueType2", nameof (Prompt), "String"));
      }
      string caption;
      try
      {
        caption = Title != null ? Conversions.ToString(Title) : (vbHost != null ? vbHost.GetWindowTitle() : Interaction.GetTitleFromAssembly(Assembly.GetCallingAssembly()));
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
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValueType2", nameof (Title), "String"));
      }
      return (MsgBoxResult) MessageBox.Show(owner, text, caption, (MessageBoxButtons) (Buttons & (MsgBoxStyle) 15), (MessageBoxIcon) (Buttons & (MsgBoxStyle) 240), (MessageBoxDefaultButton) (Buttons & (MsgBoxStyle) 3840), (MessageBoxOptions) (Buttons & (MsgBoxStyle) (-4096)));
    }
    

    /// <summary>Selects and returns a value from a list of arguments.</summary>
    /// <param name="Index">Required. <see langword="Double" />. Numeric expression that results in a value between 1 and the number of elements passed in the <paramref name="Choice" /> argument.</param>
    /// <param name="Choice">Required. <see langword="Object" /> parameter array. You can supply either a single variable or an expression that evaluates to the <see langword="Object" /> data type, to a list of <see langword="Object" /> variables or expressions separated by commas, or to a single-dimensional array of <see langword="Object" /> elements.</param>
    /// <returns>Selects and returns a value from a list of arguments.</returns>
    public static object Choose(double Index, params object[] Choice)
    {
      int index = checked ((int) Math.Round(unchecked (Conversion.Fix(Index) - 1.0)));
      if (Choice.Rank != 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_RankEQOne1", new string[1]
        {
          nameof (Choice)
        }));
      if (index < 0 || index > Choice.GetUpperBound(0))
        return (object) null;
      return Choice[index];
    }
*/
    /// <summary>Returns one of two objects, depending on the evaluation of an expression.</summary>
    /// <param name="Expression">Required. <see langword="Boolean" />. The expression you want to evaluate.</param>
    /// <param name="TruePart">Required. <see langword="Object" />. Returned if <paramref name="Expression" /> evaluates to <see langword="True" />.</param>
    /// <param name="FalsePart">Required. <see langword="Object" />. Returned if <paramref name="Expression" /> evaluates to <see langword="False" />.</param>
    /// <returns>Returns one of two objects, depending on the evaluation of an expression.</returns>
    public static object IIf(bool Expression, object TruePart, object FalsePart)
    {
      if (Expression)
        return TruePart;
      return FalsePart;
    }

    internal static T IIf<T>(bool Condition, T TruePart, T FalsePart)
    {
      if (Condition)
        return TruePart;
      return FalsePart;
    }
/*
    /// <summary>Returns a string representing the calculated range that contains a number.</summary>
    /// <param name="Number">Required. <see langword="Long" />. Whole number that you want to locate within one of the calculated ranges.</param>
    /// <param name="Start">Required. <see langword="Long" />. Whole number that indicates the start of the set of calculated ranges. <paramref name="Start" /> cannot be less than 0.</param>
    /// <param name="Stop">Required. <see langword="Long" />. Whole number that indicates the end of the set of calculated ranges. <paramref name="Stop" /> cannot be less than or equal to <paramref name="Start" />.</param>
    /// <param name="Interval">Required. <see langword="Long" />. Whole number that indicates the size of each range calculated between <paramref name="Start" /> and <paramref name="Stop" />. <paramref name="Interval" /> cannot be less than 1.</param>
    /// <returns>Returns a string representing the calculated range that contains a number.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Start" /> &lt; 0, <paramref name="Stop" /> &lt;= <paramref name="Start" />, or <paramref name="Interval" /> &lt; 1.</exception>
    public static string Partition(long Number, long Start, long Stop, long Interval)
    {
      string Buffer1 = (string) null;
      if (Start < 0L)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Start)
        }));
      if (Stop <= Start)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Stop)
        }));
      if (Interval < 1L)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Interval)
        }));
      long Num1=0;
      bool flag1 = false;
      long Num2 = 0;
      bool flag2 = false;
      if (Number < Start)
      {
        Num1 = checked (Start - 1L);
        flag1 = true;
      }
      else if (Number > Stop)
      {
        Num2 = checked (Stop + 1L);
        flag2 = true;
      }
      else if (Interval == 1L)
      {
        Num2 = Number;
        Num1 = Number;
      }
      else
      {
        Num2 = checked (unchecked (checked (Number - Start) / Interval) * Interval + Start);
        Num1 = checked (Num2 + Interval - 1L);
        if (Num1 > Stop)
          Num1 = Stop;
        if (Num2 < Start)
          Num2 = Start;
      }
      string Expression1 = Conversions.ToString(checked (Stop + 1L));
      string Expression2 = Conversions.ToString(checked (Start - 1L));
      long Spaces = Strings.Len(Expression1) <= Strings.Len(Expression2) ? (long) Strings.Len(Expression2) : (long) Strings.Len(Expression1);
      if (flag1)
      {
        string Expression3 = Conversions.ToString(Num1);
        if (Spaces < (long) Strings.Len(Expression3))
          Spaces = (long) Strings.Len(Expression3);
      }
      if (flag1)
        Interaction.InsertSpaces(ref Buffer1, Spaces);
      else
        Interaction.InsertNumber(ref Buffer1, Num2, Spaces);
      string Buffer2 = Buffer1 + ":";
      if (flag2)
        Interaction.InsertSpaces(ref Buffer2, Spaces);
      else
        Interaction.InsertNumber(ref Buffer2, Num1, Spaces);
      return Buffer2;
    }

    private static void InsertSpaces(ref string Buffer, long Spaces)
    {
      while (Spaces > 0L)
      {
        Buffer += " ";
        checked { --Spaces; }
      }
    }

    private static void InsertNumber(ref string Buffer, long Num, long Spaces)
    {
      string Expression = Conversions.ToString(Num);
      Interaction.InsertSpaces(ref Buffer, checked (Spaces - (long) Strings.Len(Expression)));
      Buffer += Expression;
    }

    /// <summary>Evaluates a list of expressions and returns an <see langword="Object" /> value corresponding to the first expression in the list that is <see langword="True" />.</summary>
    /// <param name="VarExpr">Required. <see langword="Object" /> parameter array. Must have an even number of elements. You can supply a list of <see langword="Object" /> variables or expressions separated by commas, or a single-dimensional array of <see langword="Object" /> elements.</param>
    /// <returns>Evaluates a list of expressions and returns an <see langword="Object" /> value corresponding to the first expression in the list that is <see langword="True" />.</returns>
    /// <exception cref="T:System.ArgumentException">Number of arguments is odd.</exception>
    public static object Switch(params object[] VarExpr)
    {
      if (VarExpr == null)
        return (object) null;
      int length = VarExpr.Length;
      int index = 0;
      if (length % 2 != 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (VarExpr)
        }));
      while (length > 0)
      {
        if (Conversions.ToBoolean(VarExpr[index]))
          return VarExpr[checked (index + 1)];
        checked { index += 2; }
        checked { length -= 2; }
      }
      return (object) null;
    }

    /// <summary>Deletes a section or key setting from an application's entry in the Windows registry. The <see langword="My" /> feature gives you greater productivity and performance in registry operations than the <see langword="DeleteSetting " />function. For more information, see <see cref="P:Ported.VisualBasic.Devices.ServerComputer.Registry" /> .</summary>
    /// <param name="AppName">Required. <see langword="String" /> expression containing the name of the application or project to which the section or key setting applies.</param>
    /// <param name="Section">Required. <see langword="String" /> expression containing the name of the section from which the key setting is being deleted. If only <paramref name="AppName" /> and <paramref name="Section" /> are provided, the specified section is deleted along with all related key settings.</param>
    /// <param name="Key">Optional. <see langword="String" /> expression containing the name of the key setting being deleted.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Section" />, <paramref name="AppName" />, or <paramref name="Key" /> setting does not exist.</exception>
    /// <exception cref="T:System.ArgumentException">User is not logged in. </exception>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public static void DeleteSetting(string AppName, string Section = null, string Key = null)
    {
      RegistryKey registryKey = (RegistryKey) null;
      Interaction.CheckPathComponent(AppName);
      string str = Interaction.FormRegKey(AppName, Section);
      try
      {
        RegistryKey currentUser = Registry.CurrentUser;
        if (Information.IsNothing((object) Key) || Key.Length == 0)
        {
          currentUser.DeleteSubKeyTree(str);
        }
        else
        {
          registryKey = currentUser.OpenSubKey(str, true);
          if (registryKey == null)
            throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
            {
              nameof (Section)
            }));
          registryKey.DeleteValue(Key);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        registryKey?.Close();
      }
    }

    /// <summary>Returns a list of key settings and their respective values (originally created with <see langword="SaveSetting" />) from an application's entry in the Windows registry. Using the <see langword="My" /> feature gives you greater productivity and performance in registry operations than <see langword="GetAllSettings" />. For more information, see <see cref="P:Ported.VisualBasic.Devices.ServerComputer.Registry" />.</summary>
    /// <param name="AppName">Required. <see langword="String" /> expression containing the name of the application or project whose key settings are requested.</param>
    /// <param name="Section">Required. <see langword="String" /> expression containing the name of the section whose key settings are requested. <see langword="GetAllSettings" /> returns an object that contains a two-dimensional array of strings. The strings contain all the key settings in the specified section, plus their corresponding values.</param>
    /// <returns>Returns a list of key settings and their respective values (originally created with <see langword="SaveSetting" />) from an application's entry in the Windows registry.Using the <see langword="My" /> feature gives you greater productivity and performance in registry operations than <see langword="GetAllSettings" />. For more information, see <see cref="P:Ported.VisualBasic.Devices.ServerComputer.Registry" />.</returns>
    /// <exception cref="T:System.ArgumentException">User is not logged in. </exception>
    public static string[,] GetAllSettings(string AppName, string Section)
    {
      Interaction.CheckPathComponent(AppName);
      Interaction.CheckPathComponent(Section);
      string name1 = Interaction.FormRegKey(AppName, Section);
      RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name1);
      if (registryKey == null)
        return (string[,]) null;
      string[,] strArray1 = (string[,]) null;
      try
      {
        if (registryKey.ValueCount != 0)
        {
          string[] valueNames = registryKey.GetValueNames();
          int upperBound = valueNames.GetUpperBound(0);
          string[,] strArray2 = new string[checked (upperBound + 1), 2];
          int num1 = 0;
          int num2 = upperBound;
          int index = num1;
          while (index <= num2)
          {
            string name2 = valueNames[index];
            strArray2[index, 0] = name2;
            object obj = registryKey.GetValue(name2);
            if (obj != null && obj is string)
              strArray2[index, 1] = obj.ToString();
            checked { ++index; }
          }
          strArray1 = strArray2;
        }
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
      }
      finally
      {
        registryKey.Close();
      }
      return strArray1;
    }

    /// <summary>Returns a key setting value from an application's entry in the Windows registry. The <see langword="My" /> feature gives you greater productivity and performance in registry operations than <see langword="GetAllSettings" />. For more information, see <see cref="P:Ported.VisualBasic.Devices.ServerComputer.Registry" />.</summary>
    /// <param name="AppName">Required. <see langword="String" /> expression containing the name of the application or project whose key setting is requested.</param>
    /// <param name="Section">Required. <see langword="String" /> expression containing the name of the section in which the key setting is found.</param>
    /// <param name="Key">Required. <see langword="String" /> expression containing the name of the key setting to return.</param>
    /// <param name="Default">Optional. Expression containing the value to return if no value is set in the <paramref name="Key" /> setting. If omitted, <paramref name="Default" /> is assumed to be a zero-length string ("").</param>
    /// <returns>Returns a key setting value from an application's entry in the Windows registry.The <see langword="My" /> feature gives you greater productivity and performance in registry operations than <see langword="GetAllSettings" />. </returns>
    /// <exception cref="T:System.ArgumentException">One or more arguments are not <see langword="String" /> expressions, or user is not logged in.</exception>
    public static string GetSetting(string AppName, string Section, string Key, string Default = "")
    {
      RegistryKey registryKey = (RegistryKey) null;
      Interaction.CheckPathComponent(AppName);
      Interaction.CheckPathComponent(Section);
      Interaction.CheckPathComponent(Key);
      if (Default == null)
        Default = "";
      string name = Interaction.FormRegKey(AppName, Section);
      object obj;
      try
      {
        registryKey = Registry.CurrentUser.OpenSubKey(name);
        if (registryKey == null)
          return Default;
        obj = registryKey.GetValue(Key, (object) Default);
      }
      finally
      {
        registryKey?.Close();
      }
      if (obj == null)
        return (string) null;
      if (obj is string)
        return (string) obj;
      throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
    }

    /// <summary>Saves or creates an application entry in the Windows registry. The <see langword="My" /> feature gives you greater productivity and performance in registry operations than <see langword="SaveSetting" />. For more information, see <see cref="P:Ported.VisualBasic.Devices.ServerComputer.Registry" />.</summary>
    /// <param name="AppName">Required. <see langword="String" /> expression containing the name of the application or project to which the setting applies.</param>
    /// <param name="Section">Required. <see langword="String" /> expression containing the name of the section in which the key setting is being saved.</param>
    /// <param name="Key">Required. <see langword="String" /> expression containing the name of the key setting being saved.</param>
    /// <param name="Setting">Required. Expression containing the value to which <paramref name="Key" /> is being set.</param>
    /// <exception cref="T:System.ArgumentException">Key registry could not be created, or user is not logged in.</exception>
    public static void SaveSetting(string AppName, string Section, string Key, string Setting)
    {
      Interaction.CheckPathComponent(AppName);
      Interaction.CheckPathComponent(Section);
      Interaction.CheckPathComponent(Key);
      string subkey = Interaction.FormRegKey(AppName, Section);
      RegistryKey subKey = Registry.CurrentUser.CreateSubKey(subkey);
      if (subKey == null)
        throw new ArgumentException(Utils.GetResourceString("Interaction_ResKeyNotCreated1", new string[1]
        {
          subkey
        }));
      try
      {
        subKey.SetValue(Key, (object) Setting);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        subKey.Close();
      }
    }

    private static string FormRegKey(string sApp, string sSect)
    {
      return Information.IsNothing((object) sApp) || sApp.Length == 0 ? "Software\\VB and VBA Program Settings" : (Information.IsNothing((object) sSect) || sSect.Length == 0 ? "Software\\VB and VBA Program Settings\\" + sApp : "Software\\VB and VBA Program Settings\\" + sApp + "\\" + sSect);
    }

    private static void CheckPathComponent(string s)
    {
      if (s == null || s.Length == 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_PathNullOrEmpty"));
    }

    /// <summary>Creates and returns a reference to a COM object. <see langword="CreateObject" /> cannot be used to create instances of classes in Visual Basic unless those classes are explicitly exposed as COM components.</summary>
    /// <param name="ProgId">Required. <see langword="String" />. The program ID of the object to create.</param>
    /// <param name="ServerName">Optional. <see langword="String" />. The name of the network server where the object will be created. If <paramref name="ServerName" /> is an empty string (""), the local computer is used.</param>
    /// <returns>Creates and returns a reference to a COM object. <see langword="CreateObject" /> cannot be used to create instances of classes in Visual Basic unless those classes are explicitly exposed as COM components.</returns>
    /// <exception cref="T:System.Exception">
    /// <paramref name="ProgId" /> not found or not supplied-or-
    /// <paramref name="ServerName" /> fails the <see langword="DnsValidateName" /> function, most likely because it is longer than 63 characters or contains an invalid character.</exception>
    /// <exception cref="T:System.Exception">Server is unavailable</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">No object of the specified type exists</exception>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static object CreateObject(string ProgId, string ServerName = "")
    {
      if (ProgId.Length == 0)
        throw ExceptionUtils.VbMakeException(429);
      if (ServerName == null || ServerName.Length == 0)
        ServerName = (string) null;
      else if (string.Compare(Environment.MachineName, ServerName, StringComparison.OrdinalIgnoreCase) == 0)
        ServerName = (string) null;
      try
      {
        return Activator.CreateInstance(ServerName != null ? System.Type.GetTypeFromProgID(ProgId, ServerName, true) : System.Type.GetTypeFromProgID(ProgId));
      }
      catch (COMException ex)
      {
        if (ex.ErrorCode == -2147023174)
          throw ExceptionUtils.VbMakeException(462);
        throw ExceptionUtils.VbMakeException(429);
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
        throw ExceptionUtils.VbMakeException(429);
      }
    }

    /// <summary>Returns a reference to an object provided by a COM component.</summary>
    /// <param name="PathName">Optional. <see langword="String" />. The full path and name of the file containing the object to retrieve. If <paramref name="PathName" /> is omitted, <paramref name="Class" /> is required.</param>
    /// <param name="Class">Required if <paramref name="PathName" /> is not supplied. <see langword="String" />. A string representing the class of the object. The <paramref name="Class" /> argument has the following syntax and parts:
    ///   <paramref name="appname" />
    ///   .
    ///   <paramref name="objecttype" />
    /// [1|1] Parameter[1|2] Description[2|1] <paramref name="appname" />[2|2] Required. <see langword="String" />. The name of the application providing the object.[3|1] <paramref name="objecttype" />[3|2] Required. <see langword="String" />. The type or class of object to create.</param>
    /// <returns>Returns a reference to an object provided by a COM component.</returns>
    /// <exception cref="T:System.Exception">No object of the specified class type exists.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">No object with the specified path and file name exists.</exception>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public static object GetObject(string PathName = null, string Class = null)
    {
      if (Strings.Len(Class) == 0)
      {
        try
        {
          return Marshal.BindToMoniker(PathName);
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
          throw ExceptionUtils.VbMakeException(429);
        }
      }
      else if (PathName == null)
      {
        try
        {
          return Marshal.GetActiveObject(Class);
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
          throw ExceptionUtils.VbMakeException(429);
        }
      }
      else if (Strings.Len(PathName) == 0)
      {
        try
        {
          return Activator.CreateInstance(System.Type.GetTypeFromProgID(Class));
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
          throw ExceptionUtils.VbMakeException(429);
        }
      }
      else
      {
        Interaction.IPersistFile activeObject;
        try
        {
          activeObject = (Interaction.IPersistFile) Marshal.GetActiveObject(Class);
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
          throw ExceptionUtils.VbMakeException(432);
        }
        try
        {
          activeObject.Load(PathName, 0);
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
          throw ExceptionUtils.VbMakeException(429);
        }
        return (object) activeObject;
      }
    }

    /// <summary>Executes a method on an object, or sets or returns a property on an object.</summary>
    /// <param name="ObjectRef">Required. <see langword="Object" />. A pointer to the object exposing the property or method.</param>
    /// <param name="ProcName">Required. <see langword="String" />. A string expression containing the name of the property or method on the object.</param>
    /// <param name="UseCallType">Required. An enumeration member of type <see cref="T:Ported.VisualBasic.CallType" /> representing the type of procedure being called. The value of <see langword="CallType" /> can be <see langword="Method" />, <see langword="Get" />, or <see langword="Set" />.</param>
    /// <param name="Args">Optional. <see langword="ParamArray" />. A parameter array containing the arguments to be passed to the property or method being called.</param>
    /// <returns>Executes a method on an object, or sets or returns a property on an object.</returns>
    /// <exception cref="T:System.ArgumentException">Invalid <paramref name="UseCallType" /> value; must be <see langword="Method" />, <see langword="Get" />, or <see langword="Set" />.</exception>
    public static object CallByName(object ObjectRef, string ProcName, CallType UseCallType, params object[] Args)
    {
      switch (UseCallType)
      {
        case CallType.Method:
          return LateBinding.InternalLateCall(ObjectRef, (System.Type) null, ProcName, Args, (string[]) null, (bool[]) null, false);
        case CallType.Get:
          return LateBinding.LateGet(ObjectRef, (System.Type) null, ProcName, Args, (string[]) null, (bool[]) null);
        case CallType.Let:
        case CallType.Set:
          object o = ObjectRef;
          System.Type type = (System.Type) null;
          ref System.Type local1 = ref type;
          string name = ProcName;
          object[] args = Args;
          // ISSUE: variable of the null type
          object local2 = null;
          int num1 = 0;
          int num2 = (int) UseCallType;
          LateBinding.InternalLateSet(o, ref local1, name, args, (string[]) local2, num1 != 0, (CallType) num2);
          return (object) null;
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            "CallType"
          }));
      }
    }

    private sealed class InputBoxHandler
    {
      private string m_Prompt;
      private string m_Title;
      private string m_DefaultResponse;
      private int m_XPos;
      private int m_YPos;
      private string m_Result;
      private IWin32Window m_ParentWindow;

      public InputBoxHandler(string Prompt, string Title, string DefaultResponse, int XPos, int YPos, IWin32Window ParentWindow)
      {
        this.m_Prompt = Prompt;
        this.m_Title = Title;
        this.m_DefaultResponse = DefaultResponse;
        this.m_XPos = XPos;
        this.m_YPos = YPos;
        this.m_ParentWindow = ParentWindow;
      }

      [STAThread]
      public void StartHere()
      {
        this.m_Result = Interaction.InternalInputBox(this.m_Prompt, this.m_Title, this.m_DefaultResponse, this.m_XPos, this.m_YPos, this.m_ParentWindow);
      }

      public string Result
      {
        get
        {
          return this.m_Result;
        }
      }
    }

    [Guid("0000010B-0000-0000-C000-000000000046")]
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IPersistFile
    {
      void GetClassID(ref Guid pClassID);

      void IsDirty();

      void Load(string pszFileName, int dwMode);

      void Save(string pszFileName, int fRemember);

      void SaveCompleted(string pszFileName);

      string GetCurFile();
    }
    */
  }
}

