Imports System.Reflection

Public Class frmAboutUs
    Private Sub frmAboutUs_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frmAboutUs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    Private Sub frmAboutUs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim versionNumber As Version = Assembly.GetExecutingAssembly().GetName().Version


        lblVersion.Text = versionNumber.Build.ToString() + "." + versionNumber.Revision.ToString()
    End Sub
End Class