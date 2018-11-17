Imports Aguiñagalde.Gestoras

Public Class FacturaDetalle
    Private _tabla As DataTable
    Private _Importe As Decimal = Nothing

    Public Sub New(ByVal xTable As DataTable)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _tabla = xTable

    End Sub
    Public Sub New(ByVal xTable As DataTable, ByVal xImporte As Decimal)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _tabla = xTable
        _Importe = xImporte


    End Sub

    Private Sub FacturaDetalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGFactura.DataSource = _tabla
        If Not IsNothing(_Importe) Then
            lblImporte.Visible = True
            txtImporte.Visible = True
            txtImporte.Text = _Importe
            txtImporte.BackColor = Color.White
        End If
    End Sub

    Private Sub FacturaDetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub DGFactura_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGFactura.CellContentClick

    End Sub
End Class