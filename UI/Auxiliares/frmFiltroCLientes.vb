Imports Aguiñagalde.Gestoras

Public Class frmFiltroCLientes

    Private tClientes As DataTable = GClientes.Instance().TablaClientes
    Private mIndex As Byte = 0 'Columna seleccionada
    Private mActiva As String = "CODIGO" ' Nombre columna activa
    Private mFiltro As String = ""
    Private mCodCliente As Integer


    Public ReadOnly Property NumeroCliente() As Integer
        Get
            Return mCodCliente
        End Get
    End Property






    Private Sub frmFiltroCLientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Funciones.DoubleBuffered(GrillaClientes, True)
        With GrillaClientes
            .DataSource = tClientes
            For Each ObjCol As DataGridViewColumn In .Columns
                ObjCol.SortMode = DataGridViewColumnSortMode.Programmatic
            Next
            .Columns(mIndex).DefaultCellStyle.BackColor = Color.Beige
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

        txtDato.AutoCompleteCustomSource = LoadAutoComplete()
        txtDato.AutoCompleteMode = AutoCompleteMode.Suggest
        txtDato.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Private Sub frmFiltroCLientes_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.F2
                tClientes = GClientes.Instance().TablaClientes
                GrillaClientes.DataSource = tClientes
        End Select

    End Sub

    Private Sub GrillaClientes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GrillaClientes.CellContentClick

    End Sub

    Private Sub GrillaClientes_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GrillaClientes.ColumnHeaderMouseClick

        mFiltro = ""
        mActiva = (sender.Columns(e.ColumnIndex).Name)
        With sender
            .Columns(mIndex).DefaultCellStyle.BackColor = Color.White
            .Columns(e.ColumnIndex).DefaultCellStyle.BackColor = Color.Beige
        End With
        mIndex = e.ColumnIndex

        If txtDato.Text.Length > 1 Then
            Try

                tClientes = (From Cliente In tClientes.AsEnumerable()
                             Where Cliente(mActiva).Contains(txtDato.Text.ToUpper())
                             Select Cliente).CopyToDataTable()
                GrillaClientes.DataSource = tClientes
                txtDato.Clear()
            Catch ex As Exception


            End Try
        End If


    End Sub

    Private Sub GrillaClientes_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GrillaClientes.CellDoubleClick
        mCodCliente = GrillaClientes.Item("CODIGO", GrillaClientes.CurrentRow.Index).Value()
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub txtDato_TextChanged(sender As Object, e As EventArgs) Handles txtDato.TextChanged

    End Sub

    Public Function LoadAutoComplete() As AutoCompleteStringCollection


        Dim stringCol As New AutoCompleteStringCollection()

        For Each row As DataRow In tClientes.Rows
            stringCol.Add(Convert.ToString(row("Nombre")))
        Next

        Return stringCol
    End Function
End Class