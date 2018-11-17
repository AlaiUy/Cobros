Public Class Start
    <STAThread()>
    Shared Sub Main()
        Try
#If DEBUG Then
            Application.SetCompatibleTextRenderingDefault(False)
            Application.EnableVisualStyles()
            Dim Login As Form = New frmLogin()
            Login.ShowDialog()
            If (Login.DialogResult = DialogResult.OK) Then
                Dim MainForm As Form = New Main()
                Application.Run(MainForm)
            Else
                Return
            End If
#Else
            Application.SetCompatibleTextRenderingDefault(False)
            Application.EnableVisualStyles()
            Dim Login As Form = New frmLogin()
            Login.ShowDialog()
            If (Login.DialogResult = DialogResult.OK) Then
                Dim MainForm As Form = New Main()
                Application.Run(MainForm)
            Else
                Return
            End If
            'Application.SetCompatibleTextRenderingDefault(False)
            'Application.EnableVisualStyles()
            'Dim Login As Form = New frmLogin()
            'Login.ShowDialog()
            'If (Login.DialogResult = DialogResult.OK) Then
            '    Dim MainForm As Form = New Main()
            '    Application.Run(MainForm)
            'Else
            '    Return
            'End If
            ''Dim _Soft = "COBROS"
            ''Dim _FTP = "ftp://ftp.aguinagalde.com.uy/Alai/UPDATES/" + _Soft + "/Config.ini"
            ''Dim _File = "ftp://ftp.aguinagalde.com.uy/Alai/UPDATES/" + _Soft + "/UP.zip"
            ''Dim _User As String = "alai@aguinagalde.com.uy"
            ''Dim _Pass As String = "#25106/jos"
            ''DownloadConfig(_FTP, _User, _Pass, "C:\")

            ''If isUpdated() Then
            ''    Application.EnableVisualStyles()
            ''    Application.SetCompatibleTextRenderingDefault(False)
            ''    Dim Login As Form = New frmLogin()
            ''    Login.ShowDialog()
            ''    If (Login.DialogResult = DialogResult.OK) Then
            ''        Dim MainForm As Form = New Main()
            ''        Application.Run(MainForm)
            ''    Else
            ''        Return
            ''    End If
            ''Else
            ''    Dim Parametro As String
            ''    Dim Param As Integer = Environment.GetCommandLineArgs.Length
            ''    Try
            ''        Parametro = Environment.GetCommandLineArgs(1)
            ''    Catch ex As Exception
            ''        Dim p As ProcessStartInfo
            ''        p = New ProcessStartInfo(".\Updater.exe", "57ca089205081040146f36a2fa6e3dcc")
            ''        Process.Start(p)
            ''        Return
            ''    End Try

            ''    ''WeLiveSafe
            ''    If Parametro = MD5EncryptPass("WeLiveSafe") Then
            ''        Application.EnableVisualStyles()
            ''        Application.SetCompatibleTextRenderingDefault(False)
            ''        Dim Login As Form = New frmLogin()
            ''        Login.ShowDialog()
            ''        If (Login.DialogResult = DialogResult.OK) Then
            ''            Dim MainForm As Form = New Main()
            ''            Application.Run(MainForm)
            ''        Else
            ''            Return
            ''        End If
            ''    End If
            ''End If


#End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
