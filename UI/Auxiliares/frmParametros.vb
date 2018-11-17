Imports Aguiñagalde.Entidades
Imports Aguiñagalde.Gestoras

Public Class frmParametros
    Private Sub frmParametros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CheckForIllegalCrossThreadCalls = False

        CargarParametos()
    End Sub

    Private Sub CargarParametos()
        Try
            DGParametros.DataSource = GCobros.getInstance().getParametros()
        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Sub



    Private Sub frmParametros_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ActualizarParametros()
    End Sub

    Private Sub ActualizarParametros()
        Try
            Dim ListaConfig As List(Of Config) = New List(Of Config)


            For Each R As DataGridViewRow In DGParametros.Rows
                If Not IsNothing(R.Cells("ID").Value) Then
                    ListaConfig.Add(New Config(R.Cells("VALOR").Value.ToString(), R.Cells("ID").Value))
                End If

            Next

            GCobros.getInstance().UpdateParameters(ListaConfig)
            MsgBox("Exito")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim Tabla As DataTable = DGParametros.DataSource

        Dim row As DataRow
        row = Tabla.NewRow()
        row("ID") = txtID.Text
        row("Valor") = txtValor.Text
    End Sub

    Private Sub txtDescripcion_TextChanged(sender As Object, e As EventArgs) Handles txtDescripcion.TextChanged

    End Sub
End Class