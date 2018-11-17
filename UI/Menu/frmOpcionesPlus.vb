Imports Aguiñagalde.Entidades
Imports Aguiñagalde.Interfaces

Public Class frmOpcionesPlus

    Dim _Cliente As ClienteActivo
    Dim _Observador As IObserver
    Private Sub frmOpcionesPlus_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New(ByVal xCliente As ClienteActivo, ByVal xObservador As IObserver)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        _Cliente = xCliente
        _Observador = xObservador
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub btn_Descatalogar_Click(sender As Object, e As EventArgs) Handles btn_Descatalogar.Click
        Dim frDescatalogar As New frm_Descatalogar()
        frDescatalogar.Show()
        Me.Close()
    End Sub

    Private Sub frmOpcionesPlus_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub frmOpcionesPlus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub btnDelacasa_Click(sender As Object, e As EventArgs) Handles btnDelacasa.Click
        Dim frmDeCasa As New frmDelacasa()
        frmDeCasa.Show()
        Me.Close()
    End Sub

    Private Sub btnEC_Click(sender As Object, e As EventArgs) Handles btnEC.Click
        If IsNothing(_Cliente) Then
            Return
        End If
        Dim frmEntrega As Form = New frmEC(_Cliente, _Observador)
        frmEntrega.Show()
        Close()
    End Sub


End Class