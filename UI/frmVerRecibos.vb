Imports Aguiñagalde.Gestoras

Public Class frmVerRecibos

    Private Sub frmVerRecibos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGVistaRecivos.DataSource = GCobros.getInstance().VerRecibos()
    End Sub


    Private Sub btnImprimirRecibo_Click(sender As Object, e As EventArgs) Handles btnImprimirRecibo.Click
        Dim zNumero As Integer
        Dim sSerie As String
        For Each R As DataGridViewRow In DGVistaRecivos.SelectedRows
            zNumero = R.Cells("NUMERO").Value
            sSerie = R.Cells("SERIE").Value
        Next
        GCobros.getInstance().ReimprimirRecibo(zNumero, sSerie)

    End Sub

    Private Sub frmVerRecibos_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub


End Class