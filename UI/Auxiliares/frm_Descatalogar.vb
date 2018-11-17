Imports Aguiñagalde.Gestoras

Public Class frm_Descatalogar


    Private Sub txt_Codigo_KeyPress(sender As Object, e As KeyPressEventArgs)

        If (Char.IsNumber(e.KeyChar)) Then
            e.Handled = False
        ElseIf (Char.IsControl(e.KeyChar)) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btn_DoIt_Click(sender As Object, e As EventArgs) Handles btn_DoIt.Click
        Try
            GArticulos.getInstance().Descatalogar(txtCodigo.Text)
            MsgBox("Descatalogado exitosamente")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frm_Descatalogar_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
End Class