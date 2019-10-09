Imports Aguiñagalde.Entidades
Imports Aguiñagalde.Gestoras
Imports System.Drawing.Printing
Imports Aguiñagalde.Tools
Imports System.Globalization
Imports Aguiñagalde.Interfaces
Imports System.Collections.Generic
Imports System.Reflection

Public Class Main
    Implements IObserver
    Private _Cliente As ClienteActivo = Nothing
    Private _Movimientos As List(Of MovimientoGeneral) = Nothing
    Private _MonedaP As Moneda
    Private _MonedaD As Moneda
    Private _SelectAll As Boolean = False
    Private f As frmGif = Nothing
    Private _Pagar As List(Of MovimientoGeneral)
    Private _Keep As Boolean


    Private ImportePesos As Decimal = 0
    Private ImporteDolares As Decimal = 0
    Private DescuentoPesos As Decimal = 0
    Private MoraPesos As Decimal = 0
    Private DescuentoDolares As Decimal = 0
    Private MoraDolares As Decimal = 0



    Private BanderaDescuento As Boolean = True
    Private BanderaMora As Boolean = True

    Private TablaCompleta As DataTable

    Public Enum MonedaValores
        Pesos = 1
        Dolares = 2
    End Enum

    Private Sub ReloadMoves()
        If IsNothing(_Cliente) Then
            Return
        End If
        _Movimientos = Nothing
        DGMovimientoGenerals.DataSource = Nothing
        _Movimientos = GCuentas.getInstance().PendientesByCliente(_Cliente)
        TablaCompleta = Mostrar()
        DGMovimientoGenerals.DataSource = TablaCompleta
        DGMovimientoGenerals.Columns("FECHA").DefaultCellStyle.Format = "dd/MM/yy"
    End Sub

    Private Sub getTotales()

        If IsNothing(_Movimientos) Then
            Return
        End If

        ImportePesos = 0
        ImporteDolares = 0
        DescuentoDolares = 0
        DescuentoPesos = 0
        MoraPesos = 0
        MoraDolares = 0


        Dim tmpDescuento, tmpMora As Decimal
        For Each M As MovimientoGeneral In _Movimientos
            tmpDescuento = 0
            tmpMora = 0
            tmpDescuento = M.getDescuento(GCobros.getInstance().Caja.NumeroDescuento, cbFormaPago.SelectedValue, _Keep)
            tmpMora = M.getMora()
            If M.Moneda.Codmoneda = 1 Then
                ImportePesos += M.Importe
                If (tmpDescuento <> 0) Then
                    DescuentoPesos += tmpDescuento
                End If
                If tmpMora <> 0 Then
                    MoraPesos += tmpMora
                End If
            ElseIf M.Moneda.Codmoneda = 2 Then
                ImporteDolares += M.Importe
                If (tmpDescuento <> 0) Then
                    DescuentoDolares += tmpDescuento
                End If
                If tmpMora <> 0 Then
                    MoraDolares += tmpMora
                End If
            End If
        Next
    End Sub

    Private Sub PopularTotales()
        txtImportePesos.Text = FormatearImporte(0)
        txtImporteDolares.Text = FormatearImporte(0)

        If chkDescuento.Checked Then
            txtBonificacionDolares.Text = FormatearImporte(0)
            txtBonificacionPesos.Text = FormatearImporte(0)
        End If

        If ChkMora.Checked Then
            txtMoraPesos.Text = FormatearImporte(0)
            txtMoraDolares.Text = FormatearImporte(0)
        End If

        txtMoraDolares.Text = FormatearImporte(0)
        txtMoraPesos.Text = FormatearImporte(0)
        txtPesos.Text = FormatearImporte(0)
        txtSaldoPesos.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", Decimal.Round(ImportePesos, 2))
        txtSaldoDolares.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", ImporteDolares, 2)
        txtMoraTotalDolares.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", MoraDolares, 2)
        txtMoraTotalPesos.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", MoraPesos, 2)
        txtDescPesos.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", DescuentoPesos)
        txtDescDolares.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", DescuentoDolares)
        txtSubPesos.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", ((ImportePesos + (ImporteDolares * GCobros.getInstance().Caja.Cotizacion))))
        subTotalP.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", (ImportePesos + MoraPesos) - Math.Abs(DescuentoPesos))
        SubTotalD.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", (ImporteDolares + MoraDolares) - Math.Abs(DescuentoDolares))
        Dim tmpMoraPesos As Decimal = 0
        Dim tmpMoraDolares As Decimal = 0
        Dim tmpDescPesos As Decimal = 0
        Dim tmpDescDolares As Decimal = 0
        If BanderaDescuento Then
            tmpDescDolares = DescuentoDolares
            tmpDescPesos = DescuentoPesos
        End If
        If BanderaMora Then
            tmpMoraDolares = MoraDolares
            tmpMoraPesos = MoraPesos
        End If
        txtGranTotal.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", (ImportePesos + tmpMoraPesos - Math.Abs(tmpDescPesos)) + ((ImporteDolares + tmpMoraDolares - Math.Abs(tmpDescDolares)) * GCobros.getInstance().Caja.Cotizacion), 2)
    End Sub

    Private Function getDescuento(ByVal xImporte As Integer, ByVal xCodSel As Integer) As Decimal
        Select Case (xCodSel)
            Case 1
                Return xImporte - (xImporte / 1.3)
            Case 2
                Return xImporte - (xImporte / 1.25)
            Case 3
                Return xImporte - (xImporte / 1.2)
        End Select
        Return 0
    End Function

    Public Function Mostrar() As DataTable
        If IsNothing(_Movimientos) Then
            Return Nothing
        End If
        Dim T As New DataTable("MovimientoGenerals")
        Dim ColFecha As DataColumn = New DataColumn("FECHA", Type.GetType("System.String"))
        Dim ColPos As DataColumn = New DataColumn("POSICION", Type.GetType("System.Int32"))
        Dim ColImporte As DataColumn = New DataColumn("IMPORTE", Type.GetType("System.Single"))
        Dim ColDesc As DataColumn = New DataColumn("DESCUENTO", Type.GetType("System.Single"))
        Dim ColMora As DataColumn = New DataColumn("MORA", Type.GetType("System.Single"))
        Dim ColDiasVencidos As DataColumn = New DataColumn("DIAS VENCIDOS", Type.GetType("System.Int32"))
        Dim ColFechaVecimiento As DataColumn = New DataColumn("FECHA VENCIMIENTO", Type.GetType("System.String"))
        Dim ColSerie As DataColumn = New DataColumn("SERIE", Type.GetType("System.String"))
        Dim ColNumero As DataColumn = New DataColumn("NUMERO", Type.GetType("System.Int32"))
        Dim ColSaldo As DataColumn = New DataColumn("SALDO", Type.GetType("System.Single"))
        Dim ColdIdMoneda As DataColumn = New DataColumn("CODMONEDA", Type.GetType("System.Int32"))
        Dim FechaContado As DataColumn = New DataColumn("VENCIMIENTOCONTADO", Type.GetType("System.String"))
        Dim sCodMoneda As DataColumn = New DataColumn("SUB-FIJO", Type.GetType("System.String"))
        Dim ColSel As DataColumn = New DataColumn("SELECCIONADO", Type.GetType("System.Int32"))
        T.Columns.Add(ColdIdMoneda)
        T.Columns.Add(ColPos)
        T.Columns.Add(ColSerie)
        T.Columns.Add(ColNumero)
        T.Columns.Add(ColFecha)
        T.Columns.Add(ColFechaVecimiento)
        T.Columns.Add(FechaContado)
        T.Columns.Add(sCodMoneda)
        T.Columns.Add(ColDiasVencidos)
        T.Columns.Add(ColImporte)
        T.Columns.Add(ColMora)
        T.Columns.Add(ColDesc)
        T.Columns.Add(ColSaldo)
        T.Columns.Add(ColSel)

        For Each M As MovimientoGeneral In _Movimientos
            Dim Row As DataRow = T.NewRow()
            Row.Item("CODMONEDA") = M.Moneda.Codmoneda
            Row.Item("FECHA") = M.Fecha.ToString("dd/MM/yyyy")
            Row.Item("Fecha Vencimiento") = M.FechaVencimiento().ToString("dd/MM/yyyy")
            Row.Item("Dias Vencidos") = M.getDiasVencidos()
            Row.Item("IMPORTE") = M.Importe
            Row.Item("SUB-FIJO") = M.Moneda.Subfijo
            Row.Item("SELECCIONADO") = 0

            If M.Importe > 0 Then
                If BanderaMora Then
                    Row.Item("MORA") = M.getMora()
                Else
                    Row.Item("MORA") = 0
                End If
                If Not IsNothing(_Cliente.Tarifa) Then
                    If BanderaDescuento And _Cliente.Tarifa.ID <> 1 Then
                        Row.Item("DESCUENTO") = Math.Round(M.getDescuento(GCobros.getInstance().Caja.NumeroDescuento, cbFormaPago.SelectedValue, _Keep), 2)
                    Else
                        Row.Item("DESCUENTO") = 0
                    End If
                Else
                    Row.Item("DESCUENTO") = Math.Round(M.getDescuento(GCobros.getInstance().Caja.NumeroDescuento, cbFormaPago.SelectedValue, _Keep), 2)
                End If
            End If



            Row.Item("SERIE") = M.Serie
            Row.Item("POSICION") = M.Posicion
            Row.Item("NUMERO") = M.Numero
            Row.Item("VENCIMIENTOCONTADO") = M.VencimientoContado.ToString("dd/MM/yyyy")
            T.Rows.Add(Row)
        Next
        Return T
    End Function



    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PopularForm()
        Funciones.DoubleBuffered(DGMovimientoGenerals, True)

        Dim comboSource As New Dictionary(Of String, String)()
        comboSource.Add("1", "EFECTIVO")
        comboSource.Add("2", "DEBITO")
        comboSource.Add("3", "CREDITO")
        cbFormaPago.DataSource = New BindingSource(comboSource, Nothing)
        cbFormaPago.DisplayMember = "Value"
        cbFormaPago.ValueMember = "Key"

        CheckForIllegalCrossThreadCalls = False
        Dim r As New Globalization.CultureInfo("es-UY")
        r.NumberFormat.CurrencyDecimalSeparator = "."
        r.NumberFormat.NumberDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = r
        _MonedaP = GCobros.getInstance().ListaMonedas.Find(Function(Mon As Moneda) Mon.Codmoneda = 1)
        _MonedaD = GCobros.getInstance().ListaMonedas.Find(Function(Mon As Moneda) Mon.Codmoneda = 2)


    End Sub

    Private Sub PopularForm()
        Redondear(Me)
        Redondear(Panel11)
        Redondear(Panel1)
        Redondear(Panel9)
        Redondear(Panel6)
        Redondear(btnPagar)
        Redondear(btnMarcar)
        Redondear(btnVerRecibos)
        Redondear(Panel7)
        Redondear(Panel15)
    End Sub















    Private Sub DGMovimientoGenerals_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGMovimientoGenerals.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        If e.ColumnIndex = DGMovimientoGenerals.Columns("SERIE").DisplayIndex Or e.ColumnIndex = DGMovimientoGenerals.Columns("POSICION").DisplayIndex Then
            Dim serie As String = DGMovimientoGenerals.Item(DGMovimientoGenerals.Columns("SERIE").Index, e.RowIndex).Value
            Dim numero As Integer = DGMovimientoGenerals.Item(DGMovimientoGenerals.Columns("NUMERO").Index, e.RowIndex).Value
            Try
                If e.ColumnIndex = DGMovimientoGenerals.Columns("SERIE").DisplayIndex Then
                    Dim fDetalle As New FacturaDetalle(GCuentas.getInstance().DetalleFactura(serie, numero))
                    fDetalle.ShowDialog()
                    Return
                Else
                    Dim Importe As Decimal = GCuentas.getInstance.getImporteByFactura(numero, serie)
                    Dim FPDetalle As New FacturaDetalle(GCuentas.getInstance().DetallePosicion(serie, numero), Importe)
                    FPDetalle.ShowDialog()
                    Return
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If


        If DGMovimientoGenerals.Rows(e.RowIndex).Cells("Saldo").Value Is DBNull.Value Then

            PintarSeleccionados(e.RowIndex)
        Else
            Despintar(e.RowIndex)
        End If
        Totalizar()
    End Sub

    Private Sub PintarSeleccionados(ByVal Index As Integer)
        If Index < 0 Then
            Return
        End If

        Dim Importe As Decimal = 0
        Dim Descuento As Decimal = 0
        Dim Mora As Decimal = 0

        Importe = DGMovimientoGenerals.Rows(Index).Cells("Importe").Value

        If (Importe < 0) Then
            Return
        End If

        DGMovimientoGenerals.Rows(Index).Selected = True


        Descuento = DGMovimientoGenerals.Rows(Index).Cells("Descuento").Value
        Mora = DGMovimientoGenerals.Rows(Index).Cells("Mora").Value
        DGMovimientoGenerals.DefaultCellStyle.SelectionBackColor = Color.White
        DGMovimientoGenerals.DefaultCellStyle.SelectionForeColor = Color.Black
        If (_Cliente.Type <> 9) Then
            If Not (BanderaMora) Then
                Mora = 0
            End If
            If Not (BanderaDescuento) Then
                Descuento = 0
            End If
        End If


        DGMovimientoGenerals.Rows(Index).Cells("Saldo").Value = ImporteFinal(Importe, Descuento, Mora)
        DGMovimientoGenerals.CurrentCell() = DGMovimientoGenerals.Rows(Index).Cells("SALDO")
        DGMovimientoGenerals.Rows(Index).Cells("SALDO").Style.BackColor = Color.DarkSalmon
        DGMovimientoGenerals.DefaultCellStyle.SelectionBackColor = Color.Orange
        DGMovimientoGenerals.DefaultCellStyle.SelectionForeColor = Color.Black
        DGMovimientoGenerals.CellBorderStyle = DataGridViewCellBorderStyle.Single
        DGMovimientoGenerals.Rows(Index).Cells("SELECCIONADO").Value = 1
    End Sub

    Private Function ImporteFinal(ByVal xImporte As Decimal, ByVal xDescuento As Decimal, ByVal xMora As Decimal)
        Return (Math.Abs(xImporte) + Math.Abs(xMora)) - Math.Abs(xDescuento)
    End Function

    Private Sub PintarSeleccionados(ByVal Index As Integer, ByVal xImporte As Decimal)
        If Index < 0 Then
            Return
        End If

        DGMovimientoGenerals.Rows(Index).Cells("SELECCIONADO").Value = 1
        DGMovimientoGenerals.Rows(Index).Selected = True

        DGMovimientoGenerals.DefaultCellStyle.SelectionBackColor = Color.White
        DGMovimientoGenerals.DefaultCellStyle.SelectionForeColor = Color.Black
        DGMovimientoGenerals.Rows(Index).Cells("Saldo").Value = xImporte
        DGMovimientoGenerals.CurrentCell() = DGMovimientoGenerals.Rows(Index).Cells("SALDO")
        DGMovimientoGenerals.Rows(Index).Cells("SALDO").Style.BackColor = Color.DarkSalmon
        DGMovimientoGenerals.DefaultCellStyle.SelectionBackColor = Color.Orange
        DGMovimientoGenerals.DefaultCellStyle.SelectionForeColor = Color.Black
        DGMovimientoGenerals.CellBorderStyle = DataGridViewCellBorderStyle.Single

    End Sub

    Private Sub Despintar(ByVal Index As Integer)
        If Index < 0 Then
            Return
        End If

        DGMovimientoGenerals.Rows(Index).Cells("SELECCIONADO").Value = 0
        DGMovimientoGenerals.Rows(Index).Selected = False

        DGMovimientoGenerals.Rows(Index).Cells("SALDO").Style.BackColor = Color.White
        DGMovimientoGenerals.Rows(Index).Cells("SALDO").Value = DBNull.Value
        DGMovimientoGenerals.CurrentCell() = DGMovimientoGenerals.Rows(Index).Cells("SALDO")
        DGMovimientoGenerals.DefaultCellStyle.SelectionBackColor = Color.White
        DGMovimientoGenerals.DefaultCellStyle.SelectionForeColor = Color.Black
        'txtPagarPesos.Text = _mCobrar.Parcial(chkMora.Checked)
    End Sub






    Private Sub DGMovimientoGenerals_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGMovimientoGenerals.CellEndEdit
        If IsDBNull(TryCast(sender, DataGridView).Rows(e.RowIndex).Cells("SALDO").Value) Then
            Despintar(e.RowIndex)
            Return
        End If

        If TryCast(sender, DataGridView).Rows(e.RowIndex).Cells("SALDO").Value = 0 Then
            Despintar(e.RowIndex)
            Return
        End If

        If Not IsNumeric(TryCast(sender, DataGridView).Rows(e.RowIndex).Cells("SALDO").Value) Then
            Return
        End If

        Dim Saldo As Decimal = TryCast(sender, DataGridView).Rows(e.RowIndex).Cells("SALDO").Value

        Dim Importe As Decimal = TryCast(sender, DataGridView).Rows(e.RowIndex).Cells("IMPORTE").Value

        If Saldo > Importe Then
            Saldo = Importe
        End If

        PintarSeleccionados(e.RowIndex, Saldo)
        Totalizar()
    End Sub

    Private Sub ChkMora_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMora.CheckedChanged
        If IsNothing(_Cliente) Then
            Return
        End If
        'BorrarDatagrid()
        BanderaMora = Not BanderaMora

        If Not IsNothing(DGMovimientoGenerals.DataSource) Then
            DGMovimientoGenerals.DataSource = TablaCompleta.Copy()
            DGMovimientoGenerals.Columns("FECHA").DefaultCellStyle.Format = "dd/MM/yy"
            If Not BanderaDescuento Then
                SetDescuentoZero()
            End If
            If Not BanderaMora Then
                SetMoraZero()
            End If
            PopularGrilla()
        End If

        PopularTotales()
    End Sub

    Private Sub SetMoraZero()
        If DGMovimientoGenerals.RowCount > 0 Then
            For Each Row As DataGridViewRow In DGMovimientoGenerals.Rows
                Row.Cells("MORA").Value = 0
            Next
        End If
    End Sub


    Private Sub btnPagar_Click(sender As Object, e As EventArgs) Handles btnPagar.Click


        Try
            TryCast(sender, Button).Enabled = False
            If Not GCobros.getInstance().CajaAbierta() Then
                MsgBox("La caja se encuentra cerrada", vbOK, "Reiniciar")
                Return
            End If
            _Pagar = New List(Of MovimientoGeneral)
            For Each Row As DataGridViewRow In DGMovimientoGenerals.Rows
                If Row.Cells("SELECCIONADO").Value = 1 Then
                    If Row.Cells("SALDO").Value < 0 Then
                        MsgBox("No puede hacer un pago con un MovimientoGeneral negativo, adjudique antes.", vbOKOnly, "Advertencia!")
                        Return
                    End If
                    Dim M As MovimientoGeneral = getMovimientoGeneralFromPendientes(Row.Cells("Serie").Value, Row.Cells("Numero").Value, Row.Cells("Posicion").Value)
                    If Not IsNothing(M) Then
                        M.ImportePagado = Row.Cells("SALDO").Value
                    End If
                    _Pagar.Add(M)
                End If
            Next

            If (_Pagar.Count > 0) Then
                f = New frmGif()
                f.TopMost = True
                f.Show()
                BW.RunWorkerAsync("Pagar")
            Else
                MsgBox("No hay movimientos para saldar", vbOKOnly, "Verificar")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbOK, "Informar")
            PopularGrilla()
        Finally

        End Try
    End Sub

    Private Function getMovimientoGeneralFromPendientes(ByVal xSerie As String, ByVal xNumero As Integer, ByVal xPosicion As Integer) As MovimientoGeneral
        Try
            Return (From d In _Movimientos Where d.Numero = xNumero And d.Serie = xSerie And d.Posicion = xPosicion).FirstOrDefault
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Nothing
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        frmLogin.ShowDialog()
    End Sub


    Private Sub txtCuenta_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCuenta.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCuenta.Text.Length < 1 Then
                MsgBox("El Codigo de cliente no puede ser vacio", vbOKOnly, "Error en datos")
                Return
            End If

            If Not Tools.Numeros.IsNumeric(txtCuenta.Text) Then
                MsgBox("El codigo de cliente no es valido", vbOKOnly, "Error Documento")
                Return
            End If

            If Val(txtCuenta.Text) < 1 Then
                MsgBox("El codigo de cliente no es valido", vbOKOnly, "Error Documento")
                Return
            End If

            If txtCuenta.Text.Length > 6 And txtCuenta.Text.Length < 9 Then
                If Not Tools.Numeros.Cedula(txtCuenta.Text) Then
                    MsgBox("La cedula no se puede verificar", vbOKOnly, "Error documento")
                    Return
                End If
            End If
            Try

                _Cliente = GClientes.Instance().getByID(txtCuenta.Text, False)
                _Movimientos = Nothing
                PopularOpciones()
                ReloadMoves()
                getTotales()
                PopularGrilla()
                PopularTotales()

            Catch ex As Exception
                MsgBox(ex.Message, vbOKOnly, "Error!")

            End Try
        End If

    End Sub




    Private Sub btnVerRecibos_Click(sender As Object, e As EventArgs) Handles btnVerRecibos.Click
        For Each f As Form In Application.OpenForms
            If f.Name = "frmVerRecibos" Then
                f.TopMost = True
                Return
            End If
        Next
        Dim frmVistaRecibos = New frmVerRecibos()
        frmVistaRecibos.Show()
    End Sub

    Private Sub Main_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If MsgBox("Cerramos?", vbOKCancel, "Estas Seguro?") = MsgBoxResult.Ok Then
                CambiarRegistro()
                Me.Close()
            End If
        End If

        If e.KeyCode = Keys.F11 Then
            Dim param = New frmParametros()
            param.Show()
        End If



        If e.KeyCode = Keys.F10 Then
            Dim Opc = New frmOpcionesPlus(_Cliente, Me)
            Opc.Show()
        End If

        If e.KeyCode = Keys.F12 Then
            Dim fAboutUs As New frmAboutUs()
            fAboutUs.ShowDialog()
        End If
        If e.KeyCode = Keys.F5 Then
            Dim frmClave As New frmClavesAcceso(_Keep)
            frmClave.ShowDialog()
            If frmClave.DialogResult = DialogResult.OK Then
                txtEstado.Text = "Clave ingresada"
                _Keep = True
                PopularGrilla()
            ElseIf frmClave.DialogResult = DialogResult.Cancel Then
                txtEstado.Text = ""
                _Keep = False
            End If
        End If
    End Sub

    Private Sub DGMovimientoGenerals_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGMovimientoGenerals.CellMouseEnter
        If Not IsNothing(DGMovimientoGenerals.Columns("SERIE")) Then
            If e.ColumnIndex = DGMovimientoGenerals.Columns("SERIE").DisplayIndex Then
                Me.Cursor = System.Windows.Forms.Cursors.Hand
            End If
        End If

        If Not IsNothing(DGMovimientoGenerals.Columns("POSICION")) Then
            If e.ColumnIndex = DGMovimientoGenerals.Columns("POSICION").DisplayIndex Then
                Me.Cursor = System.Windows.Forms.Cursors.Hand
            End If
        End If


    End Sub

    Private Sub DGMovimientoGenerals_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DGMovimientoGenerals.CellMouseLeave
        If Not IsNothing(DGMovimientoGenerals.Columns("SERIE")) Then
            If e.ColumnIndex = DGMovimientoGenerals.Columns("SERIE").DisplayIndex Then
                Me.Cursor = System.Windows.Forms.Cursors.Default
            End If
        End If

        If Not IsNothing(DGMovimientoGenerals.Columns("POSICION")) Then
            If e.ColumnIndex = DGMovimientoGenerals.Columns("POSICION").DisplayIndex Then
                Me.Cursor = System.Windows.Forms.Cursors.Default
            End If
        End If
    End Sub

    Private Sub DGMovimientoGenerals_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)

        DGMovimientoGenerals.EditMode = DataGridViewEditMode.EditProgrammatically
    End Sub

    Private Sub DGMovimientoGenerals_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DGMovimientoGenerals.KeyPress
        DGMovimientoGenerals.BeginEdit(True)
        SendKeys.Send(e.KeyChar)
    End Sub



    Private Sub DGMovimientoGenerals_KeyDown(sender As Object, e As KeyEventArgs) Handles DGMovimientoGenerals.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtCuenta.Focus()
        End If
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        If txtImporteMarcar.Text.Length < 1 Then
            Return
        End If

        If Not IsNumeric(txtImporteMarcar.Text) Then
            MsgBox("No es numerico")
            Return
        End If

        Dim zImporteTotal As Decimal = Convert.ToDecimal(txtImporteMarcar.Text)

        If zImporteTotal < 1 Then
            Return
        End If

        DespintarAll()

        Dim zCotizacion As Decimal = GCobros.getInstance.Caja.Cotizacion


        For Each Row As DataGridViewRow In DGMovimientoGenerals.Rows
            If zImporteTotal > 0 Then
                Dim zImporte As Decimal = Row.Cells("IMPORTE").Value
                If zImporte > 0 Then
                    Dim zCodMoneda As Integer = Row.Cells("CODMONEDA").Value
                    Dim zDescuento As Decimal = Math.Abs(Row.Cells("DESCUENTO").Value)
                    Dim zMora As Decimal = Row.Cells("MORA").Value
                    Dim zImporteMarcar As Decimal = 0
                    If zCodMoneda = 1 Then
                        zImporteMarcar = zImporte
                        If BanderaMora And zMora > 0 Then
                            zImporteMarcar += zMora
                        Else
                            zImporteMarcar -= zDescuento
                        End If
                        If zImporteTotal > zImporteMarcar Then
                            PintarSeleccionados(Row.Index)
                        Else
                            PintarSeleccionados(Row.Index, zImporteTotal)
                        End If
                        zImporteTotal -= zImporteMarcar
                    Else
                        zImporteMarcar = zImporte
                        If BanderaMora And zMora > 0 Then
                            zImporteMarcar += zMora
                        Else
                            zImporteMarcar -= zDescuento
                        End If
                        If zImporteTotal > (zImporteMarcar * zCotizacion) Then
                            PintarSeleccionados(Row.Index)
                        Else
                            PintarSeleccionados(Row.Index, zImporteTotal / zCotizacion)
                        End If
                        zImporteTotal -= zImporteMarcar * zCotizacion
                    End If
                End If
            Else
                DGMovimientoGenerals.CurrentCell = Row.Cells("SALDO")
                Exit For
            End If
        Next
        txtImporteMarcar.Clear()
        Totalizar()

    End Sub

    Private Sub DespintarAll()
        For Each Row As DataGridViewRow In DGMovimientoGenerals.Rows
            Row.Cells("SELECCIONADO").Value = 0
            Row.Selected = False
            Row.Cells("SALDO").Style.BackColor = Color.White
            Row.Cells("SALDO").Value = DBNull.Value
            'DGMovimientoGenerals.CurrentCell() = DGMovimientoGenerals.Rows(Index).Cells("SALDO")
            Row.DefaultCellStyle.SelectionBackColor = Color.White
            Row.DefaultCellStyle.SelectionForeColor = Color.Black
        Next

    End Sub

    Private Sub txtImporteMarcar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtImporteMarcar.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnMarcar.PerformClick()
        End If
    End Sub

    Private Sub LinkCuenta_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkCuenta.LinkClicked
        Dim fClientes = New frmFiltroCLientes()
        fClientes.ShowDialog()
        If (fClientes.DialogResult = Windows.Forms.DialogResult.OK) Then
            _Cliente = GClientes.Instance().getByID(fClientes.NumeroCliente, False)
            fClientes.Close()
            PopularOpciones()
            ReloadMoves()
            getTotales()
            PopularGrilla()
            PopularTotales()
        End If
    End Sub

    Private Sub CKDesc_CheckedChanged(sender As Object, e As EventArgs) Handles chkDescuento.CheckedChanged
        If IsNothing(_Cliente) Then
            Return
        End If
        BanderaDescuento = Not BanderaDescuento
        If Not IsNothing(DGMovimientoGenerals.DataSource) Then
            DGMovimientoGenerals.DataSource = TablaCompleta.Copy()
            DGMovimientoGenerals.Columns("FECHA").DefaultCellStyle.Format = "dd/MM/yy"
            If Not BanderaDescuento Then
                SetDescuentoZero()
            End If
            If Not BanderaMora Then
                SetMoraZero()
            End If
            PopularGrilla()
        End If
        PopularTotales()
    End Sub

    Private Sub SetDescuentoZero()
        If DGMovimientoGenerals.Rows.Count > 0 Then
            For Each Row As DataGridViewRow In DGMovimientoGenerals.Rows
                Row.Cells("DESCUENTO").Value = 0
            Next
        End If

    End Sub

    Private Sub btnMarcar_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If _SelectAll = False Then
                If DGMovimientoGenerals.Rows.Count > 0 Then
                    For Each R As DataGridViewRow In DGMovimientoGenerals.Rows
                        PintarSeleccionados(R.Index)
                        Totalizar()
                        _SelectAll = True
                    Next
                End If
            Else
                'BorrarDatagrid()
                _SelectAll = False
                Totalizar()
            End If
        End If
    End Sub

    Private Sub DGMovimientoGenerals_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.ColumnIndex = DGMovimientoGenerals.Columns("SALDO").Index Then
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If _SelectAll = False Then
                    If DGMovimientoGenerals.Rows.Count > 0 Then
                        For Each R As DataGridViewRow In DGMovimientoGenerals.Rows
                            PintarSeleccionados(R.Index)
                            Totalizar()
                            _SelectAll = True
                        Next
                    End If
                Else
                    'BorrarDatagrid()
                    _SelectAll = False
                    Totalizar()
                End If

            End If
        End If
    End Sub

    Private Sub Totalizar()
        Dim zSumaP As Decimal = 0
        Dim zSumaD As Decimal = 0
        Dim zDescuentoD As Decimal = 0
        Dim zDescuentoP As Decimal = 0
        Dim zMoraP As Decimal = 0
        Dim zMoraD As Decimal = 0
        Dim zTotal As Decimal = 0


        For Each Row As DataGridViewRow In DGMovimientoGenerals.Rows
            If Row.Cells("SELECCIONADO").Value = 1 Then
                Dim xImporte As Decimal = Row.Cells("IMPORTE").Value
                Dim xMonto As Decimal = Row.Cells("SALDO").Value
                Dim xDescuento As Decimal = Row.Cells("DESCUENTO").Value
                Dim xMora As Decimal = Row.Cells("MORA").Value
                Dim zDias As Int16 = Row.Cells("DIAS VENCIDOS").Value
                Dim xFechaContado As DateTime = Row.Cells("VENCIMIENTOCONTADO").Value
                Dim zCodMoneda As Integer = Row.Cells("CODMONEDA").Value
                If zCodMoneda = 1 Then

                    If xImporte > 0 Then
                        zSumaP += xMonto
                        If chkDescuento.Checked And (Math.Abs(xDescuento) > 0) Then
                            zDescuentoP += MovimientoGeneral.Descuento(zDias, xImporte, xMonto, GCobros.getInstance().Caja.NumeroDescuento, xFechaContado, cbFormaPago.SelectedValue, _Keep)
                        End If
                        If ChkMora.Checked And (xMora > 0) Then
                            If Math.Abs(xMonto) = xImporte Then
                                zMoraP += xMora
                            Else
                                zMoraP += MovimientoGeneral.MoraParcial(zDias, GCobros.getInstance().ListaMonedas.Find(Function(Mon As Moneda) Mon.Codmoneda = zCodMoneda).Mora, xImporte, xMonto)
                            End If

                        End If

                    End If
                Else
                    If xImporte > 0 Then
                        zSumaD += xMonto
                        If chkDescuento.Checked And (Math.Abs(xDescuento) > 0) Then
                            zDescuentoD += MovimientoGeneral.Descuento(zDias, xImporte, xMonto, GCobros.getInstance().Caja.NumeroDescuento, xFechaContado, cbFormaPago.SelectedValue, _Keep)
                        End If
                        If ChkMora.Checked And (xMora > 0) Then
                            If Math.Abs(xMonto) = xImporte Then
                                zMoraD += xMora
                            Else
                                zMoraD += MovimientoGeneral.MoraParcial(zDias, GCobros.getInstance().ListaMonedas.Find(Function(Mon As Moneda) Mon.Codmoneda = zCodMoneda).Mora, xImporte, xMonto)
                            End If

                        End If

                    End If

                End If
            End If
        Next

        txtImportePesos.Text = FormatearImporte(zSumaP)
        txtImporteDolares.Text = FormatearImporte(zSumaD)

        If chkDescuento.Checked Then
            txtBonificacionDolares.Text = FormatearImporte(zDescuentoD)
            txtBonificacionPesos.Text = FormatearImporte(zDescuentoP)
        End If

        If ChkMora.Checked Then
            txtMoraPesos.Text = FormatearImporte(zMoraP)
            txtMoraDolares.Text = FormatearImporte(zMoraD)
        End If

        zTotal = zSumaP + (zSumaD * GCobros.getInstance().Caja.Cotizacion)
        txtPesos.Text = FormatearImporte(zTotal)

    End Sub



    Private Function MismaMoneda(ByVal L As List(Of MovimientoGeneral)) As Boolean
        If IsNothing(L) Then
            Return False
        End If
        Dim CodMoneda As Integer = 0
        CodMoneda = L(0).Moneda.Codmoneda
        For Each M As MovimientoGeneral In L
            If M.Moneda.Codmoneda <> CodMoneda Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub CambiarRegistro()
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\UsuarioAguina", "Usuario", GCobros.getInstance().Caja.Usuario.NombreUsuario)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\UsuarioAguina", "Password", GCobros.getInstance().Caja.Usuario.Password)

    End Sub


    Public Overloads Sub Update(Obj As Object) Implements IObserver.Update
        If TypeOf Obj Is ClienteActivo Then
            If TryCast(Obj, ClienteActivo).IdCliente = _Cliente.IdCliente Then
                PopularOpciones()
                ReloadMoves()
                getTotales()
                PopularGrilla()
                PopularTotales()
            End If
        End If
    End Sub

    Private Sub BW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        Try
            GCobros.getInstance().Pagar(_Pagar, BanderaMora, _Cliente, BanderaDescuento, cbFormaPago.SelectedValue, _Keep)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try

    End Sub



    Private Sub DGMovimientoGenerals_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs)
        If Not IsNumeric(TryCast(sender, DataGridView).Rows(e.RowIndex).Cells("SALDO").Value) Then
            Return
        End If
    End Sub

    Private Sub DGMovimientoGenerals_CellParsing(sender As Object, e As DataGridViewCellParsingEventArgs) Handles DGMovimientoGenerals.CellParsing
        If Not IsNumeric(e.Value) Then
            Return
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub BW_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        PopularGrilla()
        If Not IsNothing(f) Then
            f.Close()
        End If
        btnPagar.Enabled = True
        PopularOpciones()
        ReloadMoves()
        getTotales()
        PopularGrilla()
        PopularTotales()
        Application.DoEvents()
    End Sub

    Private Sub cbFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFormaPago.SelectedIndexChanged
        SetDescuentoZero()
    End Sub

    Private Sub PopularOpciones()
        If IsNothing(_Cliente) Then
            Return
        End If
        If _Cliente.Type = 9 Then
            chkDescuento.Checked = False
            ChkMora.Checked = False
            BanderaDescuento = False
            BanderaMora = False
            chkDescuento.Enabled = False
            ChkMora.Enabled = False
        Else
            chkDescuento.Checked = True
            ChkMora.Checked = True
            BanderaDescuento = True
            BanderaMora = True
            chkDescuento.Enabled = True
            ChkMora.Enabled = True
        End If
        TryCast(btnPagar, Button).Enabled = True

    End Sub

    Private Sub PopularGrilla()

        If IsNothing(_Cliente) Then
            Return
        End If



        If Not IsNothing(_Cliente) Then
            txtCuenta.Text = _Cliente.IdCliente
            txtNombre.Text = _Cliente.Nombre
            txtDireccion.Text = _Cliente.Direccion
            Dim Estilo As DataGridViewCellStyle = New DataGridViewCellStyle()
            Estilo.Font = New Font("Thaoma", 10, FontStyle.Underline)
            Estilo.ForeColor = Color.Blue

            DGMovimientoGenerals.Columns("SERIE").Visible = True
            DGMovimientoGenerals.Columns("SERIE").DefaultCellStyle = Estilo

            DGMovimientoGenerals.Columns("SERIE").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            DGMovimientoGenerals.Columns("DIAS VENCIDOS").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            DGMovimientoGenerals.Columns("FECHA").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader

            DGMovimientoGenerals.Columns("CODMONEDA").Visible = False
            DGMovimientoGenerals.Columns("SELECCIONADO").Visible = False
            DGMovimientoGenerals.Columns("VENCIMIENTOCONTADO").Visible = False
            DGMovimientoGenerals.Columns("POSICION").DefaultCellStyle = Estilo


            DGMovimientoGenerals.Columns("IMPORTE").DefaultCellStyle.Format = "n2"
            DGMovimientoGenerals.Columns("IMPORTE").DefaultCellStyle.BackColor = Color.AntiqueWhite
            DGMovimientoGenerals.Columns("DESCUENTO").DefaultCellStyle.BackColor = Color.LightGreen
            DGMovimientoGenerals.Columns("MORA").DefaultCellStyle.BackColor = Color.LightPink
            DGMovimientoGenerals.Columns("DIAS VENCIDOS").Visible = False
            DGMovimientoGenerals.Columns("SALDO").ReadOnly = False

            For Each C As DataGridViewColumn In DGMovimientoGenerals.Columns
                If C.Name <> "SALDO" Then
                    C.ReadOnly = True
                End If
            Next

            For Each R As DataGridViewRow In DGMovimientoGenerals.Rows
                Despintar(R.Index)
            Next
        End If


    End Sub

    Private Sub chkDescuento_CheckStateChanged(sender As Object, e As EventArgs) Handles chkDescuento.CheckStateChanged

    End Sub

    Private Sub DGMovimientoGenerals_Layout(sender As Object, e As LayoutEventArgs) Handles DGMovimientoGenerals.Layout

    End Sub

    Private Sub txtCuenta_TextChanged(sender As Object, e As EventArgs) Handles txtCuenta.TextChanged

    End Sub



    Private Sub txtImporteMarcar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImporteMarcar.KeyPress
        e.Handled = ValidarImportes(e.KeyChar, txtImporteMarcar.Text, txtImporteMarcar.SelectionLength, txtImporteMarcar.SelectionStart)
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

    End Sub

    Private Sub DGMovimientoGenerals_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGMovimientoGenerals.CellContentClick

    End Sub

    Private Sub txtSubPesos_TextChanged(sender As Object, e As EventArgs) Handles txtSubPesos.TextChanged

    End Sub

    Private Sub txtImporteMarcar_TextChanged(sender As Object, e As EventArgs) Handles txtImporteMarcar.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        If Not IsNothing(_Cliente) Then
            Dim formDatos As Form = New frmClienteDatos(_Cliente)
            formDatos.ShowDialog()
        End If
    End Sub
End Class