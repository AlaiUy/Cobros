Imports Aguiñagalde.Entidades

Public Class frmClienteDatos
    Private _Cliente As ClienteActivo


    Public Sub New(ByVal xCliente As ClienteActivo)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        _Cliente = xCliente
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub frmClienteDatos_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub frmClienteDatos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtNombre.Text = _Cliente.Nombre
        txtDireccion.Text = _Cliente.Direccion
        txtTelefono.Text = _Cliente.Telefono
        txtOtroTelefono.Text = _Cliente.Celular
        txtObs.Text = _Cliente.CamposLibres.OtrasObservaciones
    End Sub
End Class