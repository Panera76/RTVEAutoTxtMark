'Fuente del código: http://www.recursosvisualbasic.com.ar/htm/vb-net/18-filesystemwatcher.htm

Imports System
Imports System.IO
Imports System.Text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Text.RegularExpressions

Public Class Form1

    Dim FicheroAbierto As String

    Public Function FileInUse(ByVal sFile As String) As Boolean
        If System.IO.File.Exists(sFile) Then
            Try
                Dim F As Short = FreeFile()
                FileOpen(F, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FileClose(F)
            Catch
                Return True
            End Try
        End If
        Return False
    End Function

    Private Sub FileSystemWatcher1_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles FileSystemWatcher1.Created
        ' System.Threading.Thread.Sleep(1000) ' 1 segundo
        ProcesarFichero(e.FullPath)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' inicializa las propiedades del FileSystemWatcher  
        With FileSystemWatcher1
            ' no incluye directorios en el monitoreo  
            .IncludeSubdirectories = False
            .EnableRaisingEvents = False
            ' Monitoreas todos los archivos pdf del directorio  
            .Filter = "*.pdf"
            ' filtros : Creación, cambios en el archivo ( sin directorios )  
            .NotifyFilter = NotifyFilters.CreationTime Or
                            NotifyFilters.Size Or
                            NotifyFilters.FileName
        End With
        cambiar_FSW(FileSystemWatcher1)

    End Sub

    Sub cambiar_FSW(ByVal FSW As FileSystemWatcher)

        Try
            ' comprueba que el directorio existe  
            If Directory.Exists(txtCarpetaEntrada.Text) = True Then
                FSW.Path = txtCarpetaEntrada.Text
                FSW.EnableRaisingEvents = True
            Else
                MsgBox("La carpeta especificada no existe", MsgBoxStyle.Critical)
                FSW.EnableRaisingEvents = False
            End If
            ' error  
        Catch sError As Exception
            MsgBox(sError.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Function ProcesarFichero(RutaFichero As String) As Integer
        If FileInUse(RutaFichero) Then
            FicheroAbierto = RutaFichero
            Timer1.Interval = 5000
            Timer1.Enabled = True
            lblEstado.Text = "Esperando cierre del fichero " & IO.Path.GetFileName(RutaFichero)
        Else
            ExtraerTXTdesdePDF(RutaFichero)
        End If
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If FileInUse(FicheroAbierto) = False Then
            Timer1.Enabled = False
            lblEstado.Text = "Fichero cerrado"
        End If
    End Sub

    Private Sub txtPath_LostFocus(sender As Object, e As EventArgs) Handles txtCarpetaEntrada.LostFocus
        cambiar_FSW(FileSystemWatcher1)
    End Sub

    Private Sub btnSeleccionarCarpetaEntrada_Click(sender As Object, e As EventArgs) Handles btnSeleccionarCarpetaEntrada.Click
        Dim oFolderBrowser As New FolderBrowserDialog

        With oFolderBrowser
            .SelectedPath = ""
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                txtCarpetaEntrada.Text = .SelectedPath
                .Dispose()
                cambiar_FSW(FileSystemWatcher1)
            End If
        End With
    End Sub

    Private Sub btnSeleccionarCarpetaSalida_Click(sender As Object, e As EventArgs) Handles btnSeleccionarCarpetaSalida.Click
        Dim oFolderBrowser As New FolderBrowserDialog

        With oFolderBrowser
            .SelectedPath = ""
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                txtCarpetaSalida.Text = .SelectedPath
                .Dispose()
            End If
        End With
    End Sub

    Private Function ExtraerTXTdesdePDF(RutaNombreFicheroPDF As String) As Integer
        ' El formato es %%LA VANGUARDIA / 20180416 / 1~
        ' Expresión RegEx = ((?:(?:[1]{1}\d{1}\d{1}\d{1})|(?:[2]{1}\d{3}))(?:[0]?[1-9]|[1][012])(?:(?:[0-2]?\d{1})|(?:[3][01]{1})))(5D|PA|AB|CO|PE|MD|RZ|MO|MA|VG|SG|LX|TM|CA16|HL|SM)

        Dim Fecha, AcronimoPub, Publicacion, NombreFichero, SubCarpetaDestino, Cadena As String
        Dim TextoPDF As String = "", NumPaginas As Integer

        NombreFichero = IO.Path.GetFileNameWithoutExtension(RutaNombreFicheroPDF)
        ' Expresión Regex para AAAAMMDD y publicación
        Dim RegExpr As String = "((?:(?:[1]{1}\d{1}\d{1}\d{1})|(?:[2]{1}\d{3}))(?:[0]?[1-9]|[1][012])(?:(?:[0-2]?\d{1})|(?:[3][01]{1})))(5D|PA|AB|CO|PE|MD|RZ|MO|MA|VG|SG|LX|TM|CA16|HL|SM)"
        Dim r As Regex = New Regex(RegExpr, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
        If r.IsMatch(NombreFichero) = False Then
            MsgBox("El fichero contiene un nombre erróneo")
            Return -1
        End If

        Fecha = Strings.Left(NombreFichero, 8)
        AcronimoPub = Strings.Right(NombreFichero, NombreFichero.Length - 8)
        Publicacion = ObtenerPublicacion(AcronimoPub)

        If Publicacion = "Error" Then
            MsgBox("El fichero contiene un nombre de publicación erróneo")
            Return -2
        End If

        SubCarpetaDestino = txtCarpetaSalida.Text & "\" & AcronimoPub & "\" & AcronimoPub & Strings.Left(Fecha, 6)
        lblEstado.Text = "Procesando archivo " & RutaNombreFicheroPDF
        lblEstado.Refresh()

        Cadena = "%%" & Publicacion & " / " & Fecha & " / "

        Dim LectorPDF As New PdfReader(RutaNombreFicheroPDF)
        NumPaginas = LectorPDF.NumberOfPages

        For j = 1 To NumPaginas
            Dim ExtractorPDF As ITextExtractionStrategy = New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy()
            Dim TextoPagina As String = PdfTextExtractor.GetTextFromPage(LectorPDF, j, ExtractorPDF)
            TextoPDF += Cadena & j & "~" & vbCrLf & TextoPagina & vbCrLf
            If j < NumPaginas Then
                TextoPDF += vbFormFeed & vbCrLf
            End If
            ProgressBar1.Value = j / NumPaginas * 100
        Next

        LectorPDF.Close()
        ProgressBar1.Value = 0

        Try
            My.Computer.FileSystem.CreateDirectory(SubCarpetaDestino)
            File.WriteAllText(SubCarpetaDestino & "\" & Fecha & ".txt", TextoPDF, Encoding.UTF8)
            lblEstado.Text = "En espera"
            txtLog.Text += "Generado fichero " & Fecha & ".txt de " & Publicacion & vbCrLf
            Me.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error en la escritura del fichero " & SubCarpetaDestino & "\" & Fecha & ".txt" & " - Descripción: " & ex.Message)
            lblEstado.Text = "En espera"
            txtLog.Text += "Error en la generación del fichero " & Fecha & ".txt de " & Publicacion & vbCrLf
        End Try

        Try
            My.Computer.FileSystem.MoveFile(RutaNombreFicheroPDF, SubCarpetaDestino & "\" & Fecha & ".pdf")
            txtLog.Text += "Movido el fichero " & Fecha & ".pdf de " & Publicacion & vbCrLf
            Me.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error moviendo el fichero " & RutaNombreFicheroPDF & " - Descripción: " & ex.Message)
            lblEstado.Text = "En espera"
            txtLog.Text += "Error moviendo el fichero " & RutaNombreFicheroPDF & vbCrLf
        End Try

    End Function

    Private Function ObtenerPublicacion(Acron As String) As String
        Select Case Acron
            Case "PA"
                Return "EL PAIS"
            Case "AB"
                Return "ABC"
            Case "CO"
                Return "CORREO"
            Case "PE"
                Return "PERIODICO DE CATALUÑA"
            Case "MD"
                Return "EL MUNDO"
            Case "RZ"
                Return "LA RAZON"
            Case "MO"
                Return "LE MONDE"
            Case "MA"
                Return "MARCA"
            Case "5D"
                Return "CINCO DIAS"
            Case "VG"
                Return "LA VANGUARDIA"
            Case "SG"
                Return "SIGLO"
            Case "LX"
                Return "L'EXPRESS"
            Case "TM"
                Return "TIME"
            Case "CA16"
                Return "CAMBIO 16"
            Case "HL"
                Return "HOLA"
            Case "SM"
                Return "SEMANA"
            Case Else
                Return "Error"
        End Select
    End Function













    '    ' Objeto NotifyIcon con evento para colocar el formulario en el tray  
    '    Private WithEvents mNotifyIcon As New NotifyIcon


    '    ' Subs y funciones  
    '    ' ''''''''''''''''''''''''''''  

    '    ' botón que abre el diálogo para seleccionar el directorio a monitorear  
    '    Private Sub btnPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPath.Click

    '        Dim oFolderBrowser As New FolderBrowserDialog

    '        With oFolderBrowser
    '            .SelectedPath = ""
    '            If .ShowDialog = Windows.Forms.DialogResult.OK Then
    '                txtPath.Text = .SelectedPath
    '                .Dispose()
    '                cambiar_FSW(FileSystemWatcher1)
    '            End If
    '        End With
    '    End Sub

    '    ' elimina en el mNotifyIcon  
    '    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '        mNotifyIcon.Dispose()
    '    End Sub


    '    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '        ' inicializa las propiedades del FileSystemWatcher  
    '        With FileSystemWatcher1
    '            ' no incluye directorios en el monitoreo  
    '            .IncludeSubdirectories = False
    '            .EnableRaisingEvents = False
    '            ' Monitoreas todos los archivos del directorio  
    '            .Filter = "*.*"
    '            ' filtros : Creación, cambios en el archivo ( sin directorios )  
    '            .NotifyFilter = NotifyFilters.CreationTime Or
    '                            NotifyFilters.Size Or
    '                            NotifyFilters.FileName

    '            'MsgBox(.NotifyFilter)  
    '            chkActivar.Text = "Activar/Desactivar notificación"
    '        End With

    '        ' agrega columnas ( path, el tipo de cambio y la fecha)  
    '        With ListView1
    '            .View = View.Details
    '            .Columns.Add("Archivos ", 300)
    '            .Columns.Add("Tipo de modificación", 120)
    '            .Columns.Add("fecha", 200)
    '        End With

    '        btnPath.Text = "Seleccionar directorio"

    '        ' establece el ícono del form para NotifyIcon  
    '        With mNotifyIcon
    '            .Icon = Me.Icon
    '        End With
    '    End Sub

    '    ' checkbox que activa o desactiva el FileSystemWatcher  
    '    Private Sub chkActivar_CheckedChanged(ByVal sender As System.Object,
    '                                          ByVal e As System.EventArgs) _
    '                                          Handles chkActivar.CheckedChanged
    '        cambiar_FSW(FileSystemWatcher1)
    '    End Sub

    '    ' cambia el path del FileSystemWatcher y lo habilita o deshabilita  
    '    Sub cambiar_FSW(ByVal FSW As FileSystemWatcher)

    '        Try
    '            ' comprueba que el directorio existe  
    '            If Directory.Exists(txtPath.Text) = True Then
    '                With FSW
    '                    .Path = txtPath.Text
    '                    ' activa o desactiva el  FileSystemWatcher  
    '                    .EnableRaisingEvents = chkActivar.Checked
    '                End With
    '            End If
    '            ' error  
    '        Catch sError As Exception
    '            MsgBox(sError.Message, MsgBoxStyle.Critical)
    '        End Try
    '    End Sub

    '    ' agrega los datos al listview ( el nombre del archivo   
    '    ' modificado, el tipo de cambio y la fecha)  
    '    Sub Notificar_Cambio(ByVal Path As String,
    '                         ByVal tipo As String)

    '        Dim oItem As New ListViewItem(Path)
    '        With oItem
    '            .SubItems.Add(tipo)
    '            .SubItems.Add(Date.Now.ToString)
    '            ListView1.Items.Add(oItem)
    '        End With

    '        With mNotifyIcon
    '            .ShowBalloonTip(5000, tipo, Path, ToolTipIcon.Info)
    '        End With

    '    End Sub

    '    'eventos para el control FileSystemWatcher  
    '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''  

    '    ' cambio en carpeta o archivos  
    '    Private Sub FileSystemWatcher1_Changed(ByVal sender As System.Object,
    '                                           ByVal e As System.IO.FileSystemEventArgs) _
    '                                           Handles FileSystemWatcher1.Changed
    '        Notificar_Cambio(e.Name, "Cambio")
    '    End Sub

    '    ' creación   
    '    Private Sub FileSystemWatcher1_Created(ByVal sender As Object,
    '                                           ByVal e As System.IO.FileSystemEventArgs) _
    '                                           Handles FileSystemWatcher1.Created
    '        Notificar_Cambio(e.Name, "Creación")
    '    End Sub
    '    ' eliminación de archivos o carpetas      
    '    Private Sub FileSystemWatcher1_Deleted(ByVal sender As Object,
    '                                           ByVal e As System.IO.FileSystemEventArgs) _
    '                                           Handles FileSystemWatcher1.Deleted
    '        Notificar_Cambio(e.Name, "Eliminación")
    '    End Sub

    '    ' evento de error  
    '    Private Sub FileSystemWatcher1_Error(ByVal sender As Object,
    '                                         ByVal e As System.IO.ErrorEventArgs) _
    '                                         Handles FileSystemWatcher1.Error
    '        MsgBox(e.GetException.ToString)
    '    End Sub

    '    ' eventos del componente NotifyIcon  
    '    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''  
    '    Private Sub mNotifyIcon_Click(ByVal sender As Object,
    '                                  ByVal e As System.EventArgs) _
    '                                  Handles mNotifyIcon.Click
    '        Me.Visible = True
    '        Me.WindowState = FormWindowState.Normal
    '    End Sub

    '    ' hace visible el NotifyIcon cuando se minimiza el formulario  
    '    Private Sub Form1_Resize(ByVal sender As Object,
    '                             ByVal e As System.EventArgs) _
    '                             Handles Me.Resize

    '        Select Case Me.WindowState
    '            Case FormWindowState.Minimized
    '                Me.Visible = False
    '                mNotifyIcon.Visible = True
    '                'Case FormWindowState.Normal  
    '                '    mNotifyIcon.Visible = False  
    '                '    Me.Visible = True  
    '        End Select
    '    End Sub

End Class
