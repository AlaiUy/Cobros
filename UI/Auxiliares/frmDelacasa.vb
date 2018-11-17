Imports Aguiñagalde.Gestoras
Imports Aguiñagalde.Reportes

Public Class frmDelacasa
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Me.Enabled = False
            Dim Imp As Impresion = New Impresion()
            Imp.ImprimirCredito(txtCedula.Text, txtAutorizacion.Text, Convert.ToDecimal(txtImporte.Text))
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Enabled = True
        End Try
    End Sub

    Private Sub frmDelacasa_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class