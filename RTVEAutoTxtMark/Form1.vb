Imports System.IO

Public Class Form1
    Private Function FileInUse(file As FileInfo) As Boolean
        Dim stream = DirectCast(Nothing, FileStream)
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
        Catch generatedExceptionName As IOException
            'handle the exception your way
            Return True
        Finally
            If stream IsNot Nothing Then
                stream.Close()
            End If
        End Try
        Return False
    End Function

    Public Function FileInUse2(ByVal sFile As String) As Boolean
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

    Private Sub lblEstado_Click(sender As Object, e As EventArgs) Handles lblEstado.Click
        If FileInUse2("C:\Users\Pat\Desktop\Watched\archivo.pdf") Then
            lblEstado.Text = "Archivo en uso"
        Else
            lblEstado.Text = "Archivo desbloqueado"
        End If
    End Sub
End Class
