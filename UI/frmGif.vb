Public Class frmGif

    Private Contador As Integer = 1

    Private Sub frmGif_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '   Timer1.Start()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LblLoading.Text = "Cargando... Tiempo: " + Contador.ToString()
        Contador += 1
    End Sub
End Class