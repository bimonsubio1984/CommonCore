// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.MyServices.ClipboardProxy
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Ported.VisualBasic.MyServices
{
  /// <summary>Provides methods for manipulating the Clipboard.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class ClipboardProxy
  {
    internal ClipboardProxy()
    {
    }

    /// <summary>Retrieves text from the Clipboard.</summary>
    /// <returns>The Clipboard text data or an empty string if the Clipboard does not contain data in the <see cref="F:System.Windows.Forms.DataFormats.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</returns>
    public string GetText()
    {
      return Clipboard.GetText();
    }

    /// <summary>Retrieves text from the Clipboard.</summary>
    /// <param name="format">
    /// <see cref="T:System.Windows.Forms.TextDataFormat" />. If specified, identifies what text format should be retrieved. Default is <see cref="F:System.Windows.Forms.TextDataFormat.CommaSeparatedValue" />. Required. </param>
    /// <returns>The Clipboard text data or an empty string if the Clipboard does not contain data in the specified format.</returns>
    public string GetText(TextDataFormat format)
    {
      return Clipboard.GetText(format);
    }

    /// <summary>Determines if there is text on the Clipboard.</summary>
    /// <returns>
    /// <see langword="True" /> if the Clipboard contains text; otherwise <see langword="False" />.</returns>
    public bool ContainsText()
    {
      return Clipboard.ContainsText();
    }

    /// <summary>Determines if there is text on the Clipboard.</summary>
    /// <param name="format">
    /// <see cref="T:System.Windows.Forms.TextDataFormat" />. If specified, identifies what text format to be checked for. Required. </param>
    /// <returns>
    /// <see langword="True" /> if the Clipboard contains text; otherwise <see langword="False" />.</returns>
    public bool ContainsText(TextDataFormat format)
    {
      return Clipboard.ContainsText(format);
    }

    /// <summary>Writes text to the Clipboard.</summary>
    /// <param name="text">
    /// <see langword="String" />. Text to be written. Required. </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="text" /> is an empty string.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="text" /> is <see langword="Nothing" />.</exception>
    public void SetText(string text)
    {
      Clipboard.SetText(text);
    }

    /// <summary>Writes text to the Clipboard.</summary>
    /// <param name="text">
    /// <see langword="String" />. Text to be written. Required. </param>
    /// <param name="format">
    /// <see cref="T:System.Windows.Forms.TextDataFormat" />. Format to be used when writing text. Default is <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" />. Required. </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="text" /> is an empty string.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="text" /> is <see langword="Nothing" />.</exception>
    public void SetText(string text, TextDataFormat format)
    {
      Clipboard.SetText(text, format);
    }

    /// <summary>Retrieves an image from the Clipboard.</summary>
    /// <returns>An <see cref="T:System.Drawing.Image" /> representing the Clipboard image data or <see langword="Nothing" /> if the Clipboard does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</returns>
    public Image GetImage()
    {
      return Clipboard.GetImage();
    }

    /// <summary>Returns a <see langword="Boolean" /> indicating whether an image is stored on the Clipboard.</summary>
    /// <returns>
    /// <see langword="True" /> if an image is stored on the Clipboard; otherwise <see langword="False" />.</returns>
    public bool ContainsImage()
    {
      return Clipboard.ContainsImage();
    }

    /// <summary>Writes an image to the Clipboard.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" />. Image to be written. Required. </param>
    public void SetImage(Image image)
    {
      Clipboard.SetImage(image);
    }

    /// <summary>Retrieves an audio stream from the Clipboard.</summary>
    /// <returns>A <see cref="T:System.IO.Stream" /> object containing audio data or <see langword="Nothing" /> if the Clipboard does not contain any audio data.</returns>
    public Stream GetAudioStream()
    {
      return Clipboard.GetAudioStream();
    }

    /// <summary>Indicates whether the Clipboard contains audio data.</summary>
    /// <returns>
    /// <see langword="True" /> if audio data is stored on the Clipboard; otherwise <see langword="False" />.</returns>
    public bool ContainsAudio()
    {
      return Clipboard.ContainsAudio();
    }

    /// <summary>Writes audio data to the Clipboard.</summary>
    /// <param name="audioBytes">
    /// <see langword="Byte" /> array. Audio data to be written to the Clipboard. Required. </param>
    public void SetAudio(byte[] audioBytes)
    {
      Clipboard.SetAudio(audioBytes);
    }

    /// <summary>Writes audio data to the Clipboard.</summary>
    /// <param name="audioStream">
    /// <see cref="T:System.IO.Stream" /> Audio data to be written to the clipboard. Required. </param>
    public void SetAudio(Stream audioStream)
    {
      Clipboard.SetAudio(audioStream);
    }

    /// <summary>Retrieves a collection of strings representing file names from the Clipboard.</summary>
    /// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing file names or <see langword="Nothing" /> if the Clipboard does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</returns>
    public StringCollection GetFileDropList()
    {
      return Clipboard.GetFileDropList();
    }

    /// <summary>Returns a <see langword="Boolean" /> indicating whether the Clipboard contains a file drop list.</summary>
    /// <returns>
    /// <see langword="True" /> if a file drop list is stored on the Clipboard; otherwise <see langword="False" />.</returns>
    public bool ContainsFileDropList()
    {
      return Clipboard.ContainsFileDropList();
    }

    /// <summary>Writes a collection of strings representing file paths to the Clipboard.</summary>
    /// <param name="filePaths">
    /// <see cref="T:System.Collections.Specialized.StringCollection" />. List of file names. Required. </param>
    public void SetFileDropList(StringCollection filePaths)
    {
      Clipboard.SetFileDropList(filePaths);
    }

    /// <summary>Retrieves data in a custom format from the Clipboard.</summary>
    /// <param name="format">
    /// <see langword="String" />. Name of the data format. Required. </param>
    /// <returns>An <see langword="Object" /> representing the Clipboard data or <see langword="Nothing" /> if the Clipboard does not contain any data that is in the specified format or can be converted to that format.</returns>
    public object GetData(string format)
    {
      return Clipboard.GetData(format);
    }

    /// <summary>Indicates whether the Clipboard contains data in the specified custom format.</summary>
    /// <param name="format">
    /// <see langword="String" />. Name of the custom format to be checked. Required. </param>
    /// <returns>
    /// <see langword="True" /> if data in the specified custom format is stored on the Clipboard; otherwise <see langword="False" />.</returns>
    public bool ContainsData(string format)
    {
      return Clipboard.ContainsData(format);
    }

    /// <summary>Writes data in a custom format to the Clipboard.</summary>
    /// <param name="format">
    /// <see langword="String" />. Format of data. Required. </param>
    /// <param name="data">
    /// <see langword="Object" />. Data object to be written to the Clipboard. Required. </param>
    public void SetData(string format, object data)
    {
      Clipboard.SetData(format, data);
    }

    /// <summary>Clears the Clipboard.</summary>
    public void Clear()
    {
      Clipboard.Clear();
    }

    /// <summary>Retrieves data from the Clipboard as an <see cref="T:System.Windows.Forms.IDataObject" />.</summary>
    /// <returns>An <see cref="T:System.Windows.Forms.IDataObject" /> object that represents the data currently on the Clipboard, or <see langword="Nothing" /> if there is no data on the Clipboard.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public IDataObject GetDataObject()
    {
      return Clipboard.GetDataObject();
    }

    /// <summary>Writes a <see cref="T:System.Windows.Forms.DataObject" /> to the Clipboard.</summary>
    /// <param name="data">
    /// <see cref="T:System.Windows.Forms.DataObject" />. Data object to be written to the Clipboard. Required. </param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void SetDataObject(DataObject data)
    {
      Clipboard.SetDataObject((object) data);
    }
  }
}

*/