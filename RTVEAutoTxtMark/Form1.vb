'Fuente del código: http://www.recursosvisualbasic.com.ar/htm/vb-net/18-filesystemwatcher.htm

Imports System.IO
Imports System.Text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Text.RegularExpressions

Public Class Form1

    Dim FicheroAbierto As String
    Dim quFicherosAñadidos As New Queue(Of String)

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
        quFicherosAñadidos.Enqueue(e.FullPath)
        ProcesarFicheros()
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

    Private Function ProcesarFicheros() As Integer

        Dim PrimerFichero As String = quFicherosAñadidos(0)

        If FileInUse(PrimerFichero) Then
            Timer1.Interval = 5000
            Timer1.Enabled = True
            lblEstado.Text = "Esperando cierre del fichero " & IO.Path.GetFileName(PrimerFichero)
        Else
            If quFicherosAñadidos.Count > 0 Then
                quFicherosAñadidos.Dequeue()
                ExtraerTXTdesdePDF(PrimerFichero)
            End If
            If quFicherosAñadidos.Count > 0 Then ProcesarFicheros()
        End If
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim PrimerFichero As String = quFicherosAñadidos(0)

        If FileInUse(PrimerFichero) = False Then
            Timer1.Enabled = False
            lblEstado.Text = "Fichero cerrado"
            If quFicherosAñadidos.Count > 0 Then
                quFicherosAñadidos.Dequeue()
                ExtraerTXTdesdePDF(PrimerFichero)
            End If
            If quFicherosAñadidos.Count > 0 Then ProcesarFicheros()
        End If
    End Sub

    Private Sub txtCarpetaEntrada_LostFocus(sender As Object, e As EventArgs) Handles txtCarpetaEntrada.LostFocus
        cambiar_FSW(FileSystemWatcher1)
        'My.Settings.CarpetaEntrada = txtCarpetaEntrada.Text
    End Sub

    Private Sub txtCarpetaSalida_LostFocus(sender As Object, e As EventArgs) Handles txtCarpetaSalida.LostFocus
        'My.Settings.CarpetaSalida = txtCarpetaSalida.Text
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
        ' My.Settings.CarpetaEntrada = txtCarpetaEntrada.Text
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
        'My.Settings.CarpetaSalida = txtCarpetaSalida.Text
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
            lblEstado.Text = "En espera"
            txtLog.Text += DateTime.Now & " - El fichero " & RutaNombreFicheroPDF & " contiene un nombre erróneo" & vbCrLf
            txtLog.Select(txtLog.Text.Length, 0)
            Me.Refresh()
            Return -1
        End If

        Fecha = Strings.Left(NombreFichero, 8)
        AcronimoPub = Strings.Right(NombreFichero, NombreFichero.Length - 8).ToUpper
        Publicacion = ObtenerPublicacion(AcronimoPub)

        If Publicacion = "Error" Then
            lblEstado.Text = "En espera"
            txtLog.Text += DateTime.Now & " - El archivo " & RutaNombreFicheroPDF & " contiene un nombre erróneo" & vbCrLf
            txtLog.Select(txtLog.Text.Length, 0)
            Me.Refresh()
            Return -2
        End If

        SubCarpetaDestino = txtCarpetaSalida.Text & "\" & AcronimoPub & "\" & AcronimoPub & Strings.Left(Fecha, 6)
        lblEstado.Text = "Procesando archivo " & RutaNombreFicheroPDF
        lblEstado.Refresh()

        Cadena = "%%" & Publicacion & " / " & Fecha & " / "
        If My.Computer.FileSystem.FileExists(RutaNombreFicheroPDF) = False Then
            lblEstado.Text = "En espera"
            'txtLog.Text += DateTime.Now & " - Error leyendo archivo " & RutaNombreFicheroPDF & vbCrLf
            Me.Refresh()
            Return -3
        End If
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
            txtLog.Text += DateTime.Now & " - Generado fichero " & Fecha & ".txt de " & Publicacion & vbCrLf
            txtLog.Select(txtLog.Text.Length, 0)
            Me.Refresh()
        Catch ex As Exception
            'MessageBox.Show("Error en la escritura del fichero " & SubCarpetaDestino & "\" & Fecha & ".txt" & " - Descripción: " & ex.Message)
            lblEstado.Text = "En espera"
            txtLog.Text += DateTime.Now & " - Error en la escritura del fichero " & SubCarpetaDestino & "\" & Fecha & ".txt de " & Publicacion & " - Descripción: " & ex.Message & vbCrLf
            txtLog.Select(txtLog.Text.Length, 0)
        End Try

        Try
            My.Computer.FileSystem.MoveFile(RutaNombreFicheroPDF, SubCarpetaDestino & "\" & Fecha & ".pdf", True)
            txtLog.Text += DateTime.Now & " - Movido el fichero " & Fecha & ".pdf de " & Publicacion & vbCrLf
            txtLog.Select(txtLog.Text.Length, 0)
            Me.Refresh()
        Catch ex As Exception
            'MessageBox.Show("Error moviendo el fichero " & RutaNombreFicheroPDF & " - Descripción: " & ex.Message)
            lblEstado.Text = "En espera"
            txtLog.Text += DateTime.Now & " - Error moviendo el fichero " & RutaNombreFicheroPDF & " - Descripción: " & ex.Message & vbCrLf
            txtLog.Select(txtLog.Text.Length, 0)
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

    Private Sub txtLog_DoubleClick(sender As Object, e As EventArgs) Handles txtLog.DoubleClick
        txtLog.Text = ""
    End Sub
End Class
