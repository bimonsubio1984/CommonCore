// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.VBInputBox
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
//using System.Windows.Forms;

namespace Ported.VisualBasic.CompilerServices
{
  internal sealed class VBInputBox : Form
  {
    private System.ComponentModel.Container components;
    private TextBox TextBox;
    private Label Label;
    private Button OKButton;
    private Button MyCancelButton;
    public string Output;

    internal VBInputBox()
    {
      this.Output = "";
      this.InitializeComponent();
    }

    internal VBInputBox(string Prompt, string Title, string DefaultResponse, int XPos, int YPos)
    {
      this.Output = "";
      this.InitializeComponent();
      this.InitializeInputBox(Prompt, Title, DefaultResponse, XPos, YPos);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VBInputBox));
      this.OKButton = new Button();
      this.MyCancelButton = new Button();
      this.TextBox = new TextBox();
      this.Label = new Label();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.OKButton, "OKButton", CultureInfo.CurrentUICulture);
      this.OKButton.Name = "OKButton";
      this.MyCancelButton.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.MyCancelButton, "MyCancelButton", CultureInfo.CurrentUICulture);
      this.MyCancelButton.Name = "MyCancelButton";
      componentResourceManager.ApplyResources((object) this.TextBox, "TextBox", CultureInfo.CurrentUICulture);
      this.TextBox.Name = "TextBox";
      componentResourceManager.ApplyResources((object) this.Label, "Label", CultureInfo.CurrentUICulture);
      this.Label.Name = "Label";
      this.AcceptButton = (IButtonControl) this.OKButton;
      componentResourceManager.ApplyResources((object) this, "$this", CultureInfo.CurrentUICulture);
      this.CancelButton = (IButtonControl) this.MyCancelButton;
      this.Controls.Add((Control) this.TextBox);
      this.Controls.Add((Control) this.Label);
      this.Controls.Add((Control) this.OKButton);
      this.Controls.Add((Control) this.MyCancelButton);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (VBInputBox);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void InitializeInputBox(string Prompt, string Title, string DefaultResponse, int XPos, int YPos)
    {
      this.Text = Title;
      this.Label.Text = Prompt;
      this.TextBox.Text = DefaultResponse;
      this.OKButton.Click += new EventHandler(this.OKButton_Click);
      this.MyCancelButton.Click += new EventHandler(this.MyCancelButton_Click);
      Graphics graphics = this.Label.CreateGraphics();
      SizeF sizeF = graphics.MeasureString(Prompt, this.Label.Font, this.Label.Width);
      graphics.Dispose();
      if ((double) sizeF.Height > (double) this.Label.Height)
      {
        int num = checked ((int) Math.Round((double) sizeF.Height) - this.Label.Height);
        checked { this.Label.Height += num; }
        checked { this.TextBox.Top += num; }
        checked { this.Height += num; }
      }
      if (XPos == -1 && YPos == -1)
      {
        this.StartPosition = FormStartPosition.CenterScreen;
      }
      else
      {
        if (XPos == -1)
          XPos = 600;
        if (YPos == -1)
          YPos = 350;
        this.StartPosition = FormStartPosition.Manual;
        this.DesktopLocation = new Point(XPos, YPos);
      }
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      this.Output = this.TextBox.Text;
      this.Close();
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}

*/