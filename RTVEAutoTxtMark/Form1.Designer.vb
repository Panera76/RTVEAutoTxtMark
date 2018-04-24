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
        Me.txtCarpetaEntrada = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCarpetaSalida = New System.Windows.Forms.TextBox()
        Me.btnSeleccionarCarpetaEntrada = New System.Windows.Forms.Button()
        Me.btnSeleccionarCarpetaSalida = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.txtLog = New System.Windows.Forms.TextBox()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCarpetaEntrada
        '
        Me.txtCarpetaEntrada.Location = New System.Drawing.Point(117, 12)
        Me.txtCarpetaEntrada.Name = "txtCarpetaEntrada"
        Me.txtCarpetaEntrada.Size = New System.Drawing.Size(290, 20)
        Me.txtCarpetaEntrada.TabIndex = 0
        Me.txtCarpetaEntrada.Text = "C:\Users\Pat\Desktop\Watched"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Carpeta de Entrada"
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(12, 203)
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
        Me.Label2.Location = New System.Drawing.Point(12, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Carpeta de Salida"
        '
        'txtCarpetaSalida
        '
        Me.txtCarpetaSalida.Location = New System.Drawing.Point(117, 39)
        Me.txtCarpetaSalida.Name = "txtCarpetaSalida"
        Me.txtCarpetaSalida.Size = New System.Drawing.Size(290, 20)
        Me.txtCarpetaSalida.TabIndex = 8
        Me.txtCarpetaSalida.Text = "C:\Users\Pat\Desktop\Watched"
        '
        'btnSeleccionarCarpetaEntrada
        '
        Me.btnSeleccionarCarpetaEntrada.Location = New System.Drawing.Point(413, 12)
        Me.btnSeleccionarCarpetaEntrada.Name = "btnSeleccionarCarpetaEntrada"
        Me.btnSeleccionarCarpetaEntrada.Size = New System.Drawing.Size(33, 20)
        Me.btnSeleccionarCarpetaEntrada.TabIndex = 9
        Me.btnSeleccionarCarpetaEntrada.Text = "..."
        Me.btnSeleccionarCarpetaEntrada.UseVisualStyleBackColor = True
        '
        'btnSeleccionarCarpetaSalida
        '
        Me.btnSeleccionarCarpetaSalida.Location = New System.Drawing.Point(413, 39)
        Me.btnSeleccionarCarpetaSalida.Name = "btnSeleccionarCarpetaSalida"
        Me.btnSeleccionarCarpetaSalida.Size = New System.Drawing.Size(33, 20)
        Me.btnSeleccionarCarpetaSalida.TabIndex = 10
        Me.btnSeleccionarCarpetaSalida.Text = "..."
        Me.btnSeleccionarCarpetaSalida.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(15, 219)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(431, 23)
        Me.ProgressBar1.TabIndex = 11
        '
        'txtLog
        '
        Me.txtLog.Enabled = False
        Me.txtLog.Location = New System.Drawing.Point(15, 76)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(431, 124)
        Me.txtLog.TabIndex = 12
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 254)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnSeleccionarCarpetaSalida)
        Me.Controls.Add(Me.btnSeleccionarCarpetaEntrada)
        Me.Controls.Add(Me.txtCarpetaSalida)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblEstado)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCarpetaEntrada)
        Me.Name = "Form1"
        Me.Text = "RTVE Generador de TXT automático"
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
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
End Class
