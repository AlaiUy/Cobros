Imports Aguiñagalde.Entidades
Imports Aguiñagalde.Gestoras
Imports Aguiñagalde.Interfaces

Public Class frmLogin
    Implements IObserver
    Private _User As String = "0"
    Private _Pass As String = "0"


    Private x As String = "1"
    Private Sub Parametros_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        PopularForm()




        Dim r As New Globalization.CultureInfo("es-UY")
        r.NumberFormat.CurrencyDecimalSeparator = ","
        r.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture = r

        _User = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\UsuarioAguina", "Usuario", 0)
        _Pass = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\UsuarioAguina", "Password", 0)


        If IsNothing(_User) Or IsNothing(_Pass) Then
            Return
        End If

        If Not _User = "0" And Not _Pass = "0" Then
            Try
                Iniciar(_User, _Pass)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub PopularForm()
        lbl_version.Text = "v" + My.Application.Info.Version.ToString + " " + Chr(169) + " " + Convert.ToString(2017)
        Redondear(Me)
        Redondear(PanelBack)
        RedondearTop(PanelBack_Top)
        Redondear(txt_Login_User)
        Redondear(txt_Login_Pass)
        RedondearBottom(PanelBack_Bottom)
    End Sub

    Public Overloads Sub Update(O As Object) Implements IObserver.Update
        If TypeOf O Is String Then
            Informarcion.Text = TryCast(O, String)
        End If
    End Sub

    Private Sub btnIniciar_Click(sender As Object, e As EventArgs) Handles btnIniciar.Click
        If (txt_Login_Pass.Text.Length < 1 Or txt_Login_User.Text.Length < 1) Then
            MsgBox("Ingrese datos de usuario", vbOKOnly, "Error!")
            Return
        End If
        Try
            Iniciar(txt_Login_User.Text, txt_Login_Pass.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CargarPermisos(xUsuario As Usuario)
        P._CtaDia = xUsuario.Permiso(3)
        P._Cobros = xUsuario.Permiso(2)
    End Sub

    Private Sub Informarcion_TextChanged(sender As Object, e As EventArgs)
        Application.DoEvents()
        Refresh()
    End Sub



    Private Sub Parametros_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
        If e.KeyCode = Keys.F12 Then
            Dim fParametros = New frmParametros()
            fParametros.ShowDialog()
        End If
    End Sub

    Private Sub Iniciar(ByVal xUser As String, ByVal xPass As String)
        GCobros.getInstance().Register(Me)
        GCobros.getInstance().Iniciar(xUser, xPass)
        Dim U As Usuario = GCobros.getInstance.Caja.Usuario
        CargarPermisos(U)
        If Not IsNothing(GCobros.getInstance().Caja.Usuario) Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\UsuarioAguina", "Usuario", U.Nombre)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\UsuarioAguina", "Password", U.Password)
        End If

        Me.DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub PanelBack_Bottom_Paint(sender As Object, e As PaintEventArgs) Handles PanelBack_Bottom.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub txt_Login_Pass_TextChanged(sender As Object, e As EventArgs) Handles txt_Login_Pass.TextChanged

    End Sub

    Private Sub txt_Login_Pass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Login_Pass.KeyPress
        If e.KeyChar = Chr(13) Then
            Me.btnIniciar.PerformClick()
        End If
    End Sub
End Class