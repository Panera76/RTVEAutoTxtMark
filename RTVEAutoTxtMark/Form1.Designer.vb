<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSeleccionarCarpetaEntrada = New System.Windows.Forms.Button()
        Me.btnSeleccionarCarpetaSalida = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.txtCarpetaSalida = New System.Windows.Forms.TextBox()
        Me.txtCarpetaEntrada = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Carpeta de Entrada"
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(12, 368)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(40, 13)
        Me.lblEstado.TabIndex = 3
        Me.lblEstado.Text = "Estado"
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'Timer1
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 176)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Carpeta de Salida"
        '
        'btnSeleccionarCarpetaEntrada
        '
        Me.btnSeleccionarCarpetaEntrada.Location = New System.Drawing.Point(413, 146)
        Me.btnSeleccionarCarpetaEntrada.Name = "btnSeleccionarCarpetaEntrada"
        Me.btnSeleccionarCarpetaEntrada.Size = New System.Drawing.Size(33, 20)
        Me.btnSeleccionarCarpetaEntrada.TabIndex = 9
        Me.btnSeleccionarCarpetaEntrada.Text = "..."
        Me.btnSeleccionarCarpetaEntrada.UseVisualStyleBackColor = True
        '
        'btnSeleccionarCarpetaSalida
        '
        Me.btnSeleccionarCarpetaSalida.Location = New System.Drawing.Point(413, 173)
        Me.btnSeleccionarCarpetaSalida.Name = "btnSeleccionarCarpetaSalida"
        Me.btnSeleccionarCarpetaSalida.Size = New System.Drawing.Size(33, 20)
        Me.btnSeleccionarCarpetaSalida.TabIndex = 10
        Me.btnSeleccionarCarpetaSalida.Text = "..."
        Me.btnSeleccionarCarpetaSalida.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(15, 384)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(431, 23)
        Me.ProgressBar1.TabIndex = 11
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(15, 210)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(431, 155)
        Me.txtLog.TabIndex = 12
        '
        'txtCarpetaSalida
        '
        Me.txtCarpetaSalida.Location = New System.Drawing.Point(117, 173)
        Me.txtCarpetaSalida.Name = "txtCarpetaSalida"
        Me.txtCarpetaSalida.Size = New System.Drawing.Size(290, 20)
        Me.txtCarpetaSalida.TabIndex = 8
        Me.txtCarpetaSalida.Text = Global.RTVEAutoTxtMark.My.MySettings.Default.CarpetaSalida
        '
        'txtCarpetaEntrada
        '
        Me.txtCarpetaEntrada.Location = New System.Drawing.Point(117, 146)
        Me.txtCarpetaEntrada.Name = "txtCarpetaEntrada"
        Me.txtCarpetaEntrada.Size = New System.Drawing.Size(290, 20)
        Me.txtCarpetaEntrada.TabIndex = 0
        Me.txtCarpetaEntrada.Text = Global.RTVEAutoTxtMark.My.MySettings.Default.CarpetaEntrada
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.RTVEAutoTxtMark.My.Resources.Resources.Logo_RTVE
        Me.PictureBox2.Location = New System.Drawing.Point(292, 41)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(151, 83)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 14
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.RTVEAutoTxtMark.My.Resources.Resources.Logo_Doc_it
        Me.PictureBox1.Location = New System.Drawing.Point(-1, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(230, 165)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(404, 410)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(37, 13)
        Me.lblVersion.TabIndex = 15
        Me.lblVersion.Text = "v1.0.1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 427)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnSeleccionarCarpetaSalida)
        Me.Controls.Add(Me.btnSeleccionarCarpetaEntrada)
        Me.Controls.Add(Me.txtCarpetaSalida)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblEstado)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCarpetaEntrada)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "RTVE Generador de TXT automático"
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtCarpetaEntrada As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents btnSeleccionarCarpetaSalida As Button
    Friend WithEvents btnSeleccionarCarpetaEntrada As Button
    Friend WithEvents txtCarpetaSalida As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtLog As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblVersion As Label
End Class
