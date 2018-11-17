Public Class frmClavesAcceso
    Private _clave As Boolean = False


    Public Sub New(ByVal xClave As Boolean)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        _clave = xClave



        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TryCast(sender, TextBox).Text = "098920300" And _clave = False Then
                DialogResult = DialogResult.OK
            ElseIf TryCast(sender, TextBox).Text = "098920300" And _Clave = True Then
                DialogResult = DialogResult.Cancel
            End If
        End If
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub

    Private Sub frmClavesAcceso_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()

        End If
    End Sub
End Class