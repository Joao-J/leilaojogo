<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Timer1 = New Timer(components)
        TextBox1 = New TextBox()
        WebView21 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Button1 = New Button()
        TextBox2 = New TextBox()
        Timer2 = New Timer(components)
        Timer6 = New Timer(components)
        Label1 = New Label()
        Label2 = New Label()
        Timer3 = New Timer(components)
        CType(WebView21, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        Timer1.Interval = 1000
        ' 
        ' TextBox1
        ' 
        TextBox1.BackColor = SystemColors.Menu
        TextBox1.Location = New Point(416, 12)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ScrollBars = ScrollBars.Vertical
        TextBox1.Size = New Size(372, 43)
        TextBox1.TabIndex = 0
        ' 
        ' WebView21
        ' 
        WebView21.AllowExternalDrop = True
        WebView21.CreationProperties = Nothing
        WebView21.DefaultBackgroundColor = Color.White
        WebView21.Location = New Point(12, 12)
        WebView21.Name = "WebView21"
        WebView21.Size = New Size(373, 43)
        WebView21.TabIndex = 1
        WebView21.ZoomFactor = 1R
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(403, 61)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 2
        Button1.Text = "carregar pagina"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.BackColor = SystemColors.HotTrack
        TextBox2.Location = New Point(12, 61)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(370, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Timer2
        ' 
        Timer2.Enabled = True
        ' 
        ' Timer6
        ' 
        Timer6.Enabled = True
        Timer6.Interval = 600000
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14F)
        Label1.Location = New Point(12, 151)
        Label1.Name = "Label1"
        Label1.Size = New Size(67, 25)
        Label1.TabIndex = 4
        Label1.Text = "Label1"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 14F)
        Label2.Location = New Point(12, 186)
        Label2.Name = "Label2"
        Label2.Size = New Size(67, 25)
        Label2.TabIndex = 5
        Label2.Text = "Label2"
        ' 
        ' Timer3
        ' 
        Timer3.Enabled = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.DeepPink
        ClientSize = New Size(800, 337)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TextBox2)
        Controls.Add(Button1)
        Controls.Add(WebView21)
        Controls.Add(TextBox1)
        Name = "Form1"
        Text = "Form1"
        CType(WebView21, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents WebView21 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Timer6 As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer3 As Timer

End Class
