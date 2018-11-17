Imports Aguiñagalde.Entidades
Imports Aguiñagalde.Gestoras
Imports Aguiñagalde.Interfaces

Public Class frmEC
    Implements IObservable
    Private _Cliente As ClienteActivo
    Private _Observadores As List(Of IObserver)
    Public Sub New(ByVal xCLiente As ClienteActivo, ByVal xObserver As IObserver)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        _Cliente = xCLiente
        _Observadores = New List(Of IObserver)
        _Observadores.Add(xObserver)

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub frmEC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CBMoneda.DataSource = GCobros.getInstance().ListaMonedas
        txtImporte.Focus()
    End Sub

    Private Sub btnDoIT_Click(sender As Object, e As EventArgs) Handles btnDoIt.Click
        If txtImporte.Text.Length < 1 Then
            Return
        End If

        If Not IsNumeric(txtImporte.Text) Then
            MsgBox("No es numerico")
            Return
        End If

        Dim zImporteTotal As Decimal = Convert.ToDecimal(txtImporte.Text)

        If zImporteTotal < 1 Then
            Return
        End If
        Try
            Dim C As CajaGeneral = GCobros.getInstance().Caja
            Dim M As Integer = TryCast(CBMoneda.SelectedValue, Moneda).Codmoneda
            Dim Recibo As Integer = GCobros.getInstance().GenerarEntrega(zImporteTotal, M, _Cliente)
            GCobros.getInstance().ReimprimirRecibo(Recibo, C.Recibos, _Cliente.IdCliente, M, DateTime.Today)
            notifyObservers()
            Me.Close()
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub frmEC_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub txtImporte_KeyDown(sender As Object, e As KeyEventArgs) Handles txtImporte.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnDoIt.PerformClick()
        End If
    End Sub

    Private Sub txtImporte_TextChanged(sender As Object, e As EventArgs) Handles txtImporte.TextChanged

    End Sub

    Public Sub Register(xObserver As IObserver) Implements IObservable.Register
        _Observadores.Add(xObserver)
    End Sub

    Public Sub UnRegister(xObserver As IObserver) Implements IObservable.UnRegister
        _Observadores.Remove(xObserver)
    End Sub

    Public Sub notifyObservers() Implements IObservable.notifyObservers
        For Each O As IObserver In _Observadores
            TryCast(O, IObserver).Update(_Cliente)
        Next

    End Sub

    Private Sub txtImporte_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImporte.KeyPress
        e.Handled = ValidarImportes(e.KeyChar, txtImporte.Text, txtImporte.SelectionLength, txtImporte.SelectionStart)
    End Sub
End Class