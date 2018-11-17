Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text

Module Funciones



    Public Function isUpdated() As Boolean
        Dim HashLocal As String = hash_generator("md5", ".\Config.ini")
        Dim HashFTP As String = hash_generator("md5", "C:\Config.ini")
        If Not HashFTP.Equals(HashLocal) Then
            Return False
        End If
        Return True
    End Function

    Private Function hash_generator(ByVal hash_type As String, ByVal file_name As String)

        ' We declare the variable : hash
        Dim hash
        If hash_type.ToLower = "md5" Then
            ' Initializes a md5 hash object
            hash = MD5.Create
        ElseIf hash_type.ToLower = "sha1" Then
            ' Initializes a SHA-1 hash object
            hash = SHA1.Create()
        ElseIf hash_type.ToLower = "sha256" Then
            ' Initializes a SHA-256 hash object
            hash = SHA256.Create()
        Else
            MsgBox("Unknown type of hash : " & hash_type, MsgBoxStyle.Critical)
            Return False
        End If

        ' We declare a variable to be an array of bytes
        Dim hashValue() As Byte

        ' We create a FileStream for the file passed as a parameter
        Dim fileStream As FileStream = File.OpenRead(file_name)
        ' We position the cursor at the beginning of stream
        fileStream.Position = 0
        ' We calculate the hash of the file
        hashValue = hash.ComputeHash(fileStream)
        ' The array of bytes is converted into hexadecimal before it can be read easily
        Dim hash_hex = PrintByteArray(hashValue)

        ' We close the open file
        fileStream.Close()

        ' The hash is returned
        Return hash_hex

    End Function

    Private Function PrintByteArray(ByVal array() As Byte)

        Dim hex_value As String = ""

        ' We traverse the array of bytes
        Dim i As Integer
        For i = 0 To array.Length - 1

            ' We convert each byte in hexadecimal
            hex_value += array(i).ToString("X2")

        Next i

        ' We return the string in lowercase
        Return hex_value.ToLower

    End Function

    Public Function DownloadConfig(ByVal xFichero As String, ByVal xUser As String, ByVal xPassword As String, ByVal xDestino As String) As Boolean

        Dim PathFTP As FtpWebRequest = CType(FtpWebRequest.Create(xFichero), FtpWebRequest)

        ' Los datos del usuario (credenciales)
        Dim Credenciales As New NetworkCredential(xUser, xPassword)
        PathFTP.Credentials = Credenciales

        ' El comando a ejecutar usando la enumeración de WebRequestMethods.Ftp
        PathFTP.Method = WebRequestMethods.Ftp.DownloadFile

        ' Obtener el resultado del comando
        Dim Reader As New StreamReader(PathFTP.GetResponse().GetResponseStream())

        ' Leer el stream (el contenido del archivo)
        Dim Result As String = Reader.ReadToEnd()
        ' Guardarlo localmente con la extensión .txt
        Dim PathLocal As String = Path.Combine(xDestino, Path.GetFileName("Config.ini"))
        Dim sw As New StreamWriter(PathLocal, False, Encoding.Default)
        sw.Write(Result)
        sw.Close()

        ' Cerrar el stream abierto.
        Reader.Close()
        Return True
    End Function

    Public Function getDescuento(ByVal xImporte As Decimal, ByVal xMonto As Decimal, ByVal xDescuentoTotal As Decimal) As Decimal
        Dim Descuento As Decimal = 0
        If (xMonto = (xImporte - Math.Abs(xDescuentoTotal))) Then
            Descuento = xDescuentoTotal
        ElseIf xMonto > 0 Then
            Dim DT As Decimal = Math.Abs(xDescuentoTotal)
            Dim Coef As Decimal = xMonto / (xImporte - DT)
            Descuento = DT * Coef
        End If
        Return Math.Abs(Descuento)
    End Function


    Public Sub DoubleBuffered(ByVal dgv As DataGridView, ByVal setting As Boolean)
        Dim dgvType As Type = dgv.[GetType]()
        Dim pi As PropertyInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        pi.SetValue(dgv, setting, Nothing)
    End Sub

    Public P As New Permisos


    Public Sub Redondear(ByRef xObj)
        Dim Region As New Drawing2D.GraphicsPath()
        Region.StartFigure()
        Region.AddArc(New Rectangle(0, 0, 10, 10), 180, 90)
        Region.AddLine(10, 0, xObj.Width - 10, 0)
        Region.AddArc(New Rectangle(xObj.Width - 10, 0, 10, 10), -90, 90)
        Region.AddLine(xObj.Width, 10, xObj.Width, xObj.Height - 10)
        Region.AddArc(New Rectangle(xObj.Width - 10, xObj.Height - 10, 10, 10), 0, 90)
        Region.AddLine(xObj.Width - 10, xObj.Height, 10, xObj.Height)
        Region.AddArc(New Rectangle(0, xObj.Height - 10, 10, 10), 90, 90)
        Region.CloseFigure()
        xObj.Region = New Region(Region)
    End Sub

    Public Sub RedondearTop(ByRef xObj)
        Dim Region As New Drawing2D.GraphicsPath()
        Region.StartFigure()
        Region.AddArc(New Rectangle(0, 0, 10, 10), 180, 90)
        Region.AddLine(10, 0, xObj.Width - 10, 0)
        Region.AddArc(New Rectangle(xObj.Width, 0, 10, 10), -90, 90) 'redondea arriba
        Region.AddLine(xObj.Width, 10, xObj.Width, xObj.Height - 10)
        Region.AddArc(New Rectangle(xObj.Width, xObj.Height - 10, 10, 10), 0, 90) 'redondea arriba
        Region.AddLine(xObj.Width - 10, xObj.Height, 10, xObj.Height)
        Region.AddArc(New Rectangle(0, xObj.Height, 10, 10), 90, 90)
        Region.CloseFigure()
        xObj.Region = New Region(Region)
    End Sub

    Public Sub RedondearBottom(ByRef xObj)
        Dim Region As New Drawing2D.GraphicsPath()
        Region.StartFigure()
        Region.AddArc(New Rectangle(-10, 0, 10, 10), 180, 90)
        Region.AddLine(10, 0, xObj.Width - 10, 0)
        Region.AddArc(New Rectangle(xObj.Width, 0, 10, 10), -90, 90)
        Region.AddLine(xObj.Width, 10, xObj.Width, xObj.Height - 10)
        Region.AddArc(New Rectangle(xObj.Width - 10, xObj.Height - 10, 10, 10), 0, 90)
        Region.AddLine(xObj.Width - 10, xObj.Height, 10, xObj.Height)
        Region.AddArc(New Rectangle(0, xObj.Height - 10, 10, 10), 90, 90)
        Region.CloseFigure()
        xObj.Region = New Region(Region)
    End Sub


    Public Function FormatearImporte(ByVal xImporte As Decimal) As String
        If xImporte < 10 Then
            Return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", xImporte)
        Else
            Return String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", xImporte)
        End If

    End Function

    Public Function ValidarImportes(ByRef xIngreso As Char, ByVal xTextoIngresado As String, ByVal xLenSelecionado As Integer, ByVal xLenStart As Integer)
        Dim Separador As Char = Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.Chars(0)
        Dim PosicionSeparador As Integer = 0

        If ((xIngreso = "."c) OrElse (xIngreso = ","c)) Then
            If xTextoIngresado.Length = 0 Or xLenSelecionado = xTextoIngresado.Length Or xLenStart = 0 Then
                Return True
            End If
            ' Obtenemos el carácter separador decimal existente
            ' actualmente en la configuración regional de Windows.
            xIngreso = Separador
        End If

        Dim Punto As Boolean = False

        If xTextoIngresado.Contains(Separador) = True Then
            Punto = True
            PosicionSeparador = InStr(1, xTextoIngresado, Separador, CompareMethod.Text)
        Else
            Punto = False
        End If




        If (Char.IsDigit(xIngreso) Or xIngreso = Separador) Then
            'validar si el usuario esta ingresando el caracter (punto)
            If (xIngreso = Separador) Then
                ' limitar el numero de puntos en el textbox
                If (Punto = False) Then
                    'si no existe ningun punto en el textbox, permitir el caracter
                    Return False
                    Punto = True
                End If
            End If
        End If

        If PosicionSeparador > 0 And Not (Char.IsControl(xIngreso)) Then
            If xTextoIngresado.Length > PosicionSeparador + 1 Then
                Return True
            End If
        End If
        If (Char.IsNumber(xIngreso)) Then ' Al  pulsar un Numero
            Return False '; //Se acepta (todo OK)
        ElseIf (Char.IsControl(xIngreso)) Then  ' Al  pulsar teclas como Borrar y eso.
            Return False '; //Se acepta (todo OK)
        Else '//Para todo lo demas
            Return True '; //No se acepta (si pulsas cualquier otra cosa pues no se envia)
        End If
    End Function

    Public Function MD5EncryptPass(ByVal StrPass As String) As String

        Dim md5 As MD5CryptoServiceProvider
        Dim bytValue() As Byte
        Dim bytHash() As Byte
        Dim strPassOutput As String
        Dim i As Integer
        strPassOutput = ""

        md5 = New MD5CryptoServiceProvider

        bytValue = System.Text.Encoding.UTF8.GetBytes(StrPass)

        bytHash = md5.ComputeHash(bytValue)
        md5.Clear()

        For i = 0 To bytHash.Length - 1
            strPassOutput &= bytHash(i).ToString("x").PadLeft(2, "0")
        Next

        Return strPassOutput

    End Function


End Module
