Imports System.Globalization
Imports Aguiñagalde.Entidades
Imports Aguiñagalde.Gestoras

Public Class frmVerRecibos

    '//1- Usar cuentas corrientes
    '//2- Usar Cobros
    '//3- ver contados pendientes
    '//4- Imprimir recibos de personal
    '//5- cambios en modificar cliente
    '//6- permitir bonificacion en cuentas con tarifas contado
    '//7- cargar cuentas de personal
    '//8- Anular recibos

    Private Sub PopularForm()
        If GCobros.getInstance().Caja.Usuario.Permiso(8) Then
            btnAnularRecibo.Visible = True
            btnAnularRecibo.Enabled = True
        End If
        If GCobros.getInstance().Caja.Usuario.Permiso(4) Then
            btnImprimirPersonal.Visible = True
            btnImprimirPersonal.Enabled = True
        End If
    End Sub

    Private Sub frmVerRecibos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            PopularForm()
            DGVistaRecivos.DataSource = GCobros.getInstance().VerRecibos()
            DGVistaRecivos.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub btnImprimirRecibo_Click(sender As Object, e As EventArgs) Handles btnImprimirRecibo.Click
        For Each Row As DataGridViewRow In DGVistaRecivos.SelectedRows
            Try
                GCobros.getInstance().ReimprimirRecibo(Row.Cells("NUMERO").Value, Row.Cells("SERIE").Value, Row.Cells("CODIGOINTERNO").Value, Row.Cells("MONEDA").Value, DateTime.Today)
                Application.DoEvents()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Next


    End Sub

    Private Sub frmVerRecibos_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub



    Private Sub btnAnularRecibo_Click(sender As Object, e As EventArgs) Handles btnAnularRecibo.Click
        Try
            Dim Numero As Integer = DGVistaRecivos.Item("NUMERO", DGVistaRecivos.CurrentRow.Index).Value
            Dim Serie As String = DGVistaRecivos.Item("SERIE", DGVistaRecivos.CurrentRow.Index).Value
            Dim codcliente As Integer = DGVistaRecivos.Item("CODIGOINTERNO", DGVistaRecivos.CurrentRow.Index).Value
            Dim moneda As Integer = DGVistaRecivos.Item("MONEDA", DGVistaRecivos.CurrentRow.Index).Value
            GCobros.getInstance().AnularRecibo(Numero, Serie, codcliente, moneda)
            Dim Pago As Recibo = GCobros.getInstance().ObtenerReciboByID(Serie, Numero, codcliente, moneda)
            BGAnularRecibo.RunWorkerAsync(Pago)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub Totalizar()
        Dim zSuma As Decimal = 0
        For Each Row As DataGridViewRow In DGVistaRecivos.SelectedRows
            zSuma += Row.Cells("IMPORTE").Value
        Next
        txtTotalSel.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", zSuma)
    End Sub



    Private Sub DGVistaRecivos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVistaRecivos.CellClick
        Totalizar()
    End Sub


    Private Sub btnImprimirPersonal_Click(sender As Object, e As EventArgs) Handles btnImprimirPersonal.Click
        Dim zSuma As Decimal = 0
        For Each R As DataGridViewRow In DGVistaRecivos.Rows
            If (R.Cells("TIPO").Value = 9) Then
                zSuma += R.Cells("IMPORTE").Value
                Dim zNumero As Integer = R.Cells("NUMERO").Value
                Dim rSerie As String = R.Cells("SERIE").Value
                Dim zCliente As Integer = R.Cells("CODIGOINTERNO").Value
                Dim zMoneda As Integer = R.Cells("MONEDA").Value
                Try
                    GCobros.getInstance().ReimprimirRecibo(zNumero, rSerie, zCliente, zMoneda, DateTime.Today)
                Catch ex As Exception
                    MsgBox(ex.Message + "  " + zNumero, rSerie)
                End Try
            End If
        Next
        MsgBox(zSuma)
        'GCobros.getInstance().ReimprimirRecibo(674347, "VB1X")
    End Sub

    Private Sub DGVistaRecivos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVistaRecivos.CellContentClick

    End Sub
End Class