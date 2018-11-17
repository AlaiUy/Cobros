<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.DGMovimientoGenerals = New System.Windows.Forms.DataGridView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbFormaPago = New System.Windows.Forms.ComboBox()
        Me.chkDescuento = New System.Windows.Forms.CheckBox()
        Me.btnVerRecibos = New System.Windows.Forms.Button()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.btnMarcar = New System.Windows.Forms.Button()
        Me.txtImporteMarcar = New System.Windows.Forms.TextBox()
        Me.lbltxtImporte = New System.Windows.Forms.Label()
        Me.btnPagar = New System.Windows.Forms.Button()
        Me.ChkMora = New System.Windows.Forms.CheckBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSubPesos = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtGranTotal = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SubTotalD = New System.Windows.Forms.TextBox()
        Me.subTotalP = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDescDolares = New System.Windows.Forms.TextBox()
        Me.txtDescPesos = New System.Windows.Forms.TextBox()
        Me.txtMoraTotalDolares = New System.Windows.Forms.TextBox()
        Me.txtMoraTotalPesos = New System.Windows.Forms.TextBox()
        Me.txtSaldoDolares = New System.Windows.Forms.TextBox()
        Me.txtSaldoPesos = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.txtPesos = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.txtMoraDolares = New System.Windows.Forms.TextBox()
        Me.txtImporteDolares = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtBonificacionDolares = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.txtMoraPesos = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtBonificacionPesos = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtImportePesos = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtEstado = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.LinkCuenta = New System.Windows.Forms.LinkLabel()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDireccion = New System.Windows.Forms.TextBox()
        Me.txtCuenta = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolsDetalle = New System.Windows.Forms.ToolTip(Me.components)
        Me.BW = New System.ComponentModel.BackgroundWorker()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.DGMovimientoGenerals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1018, 762)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 125)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1018, 449)
        Me.Panel3.TabIndex = 5
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.DGMovimientoGenerals)
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1018, 449)
        Me.Panel5.TabIndex = 4
        '
        'DGMovimientoGenerals
        '
        Me.DGMovimientoGenerals.AllowUserToAddRows = False
        Me.DGMovimientoGenerals.AllowUserToDeleteRows = False
        Me.DGMovimientoGenerals.AllowUserToResizeColumns = False
        Me.DGMovimientoGenerals.AllowUserToResizeRows = False
        Me.DGMovimientoGenerals.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGMovimientoGenerals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.830189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGMovimientoGenerals.DefaultCellStyle = DataGridViewCellStyle1
        Me.DGMovimientoGenerals.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGMovimientoGenerals.EnableHeadersVisualStyles = False
        Me.DGMovimientoGenerals.Location = New System.Drawing.Point(4, 61)
        Me.DGMovimientoGenerals.MultiSelect = False
        Me.DGMovimientoGenerals.Name = "DGMovimientoGenerals"
        Me.DGMovimientoGenerals.RowHeadersVisible = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DGMovimientoGenerals.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGMovimientoGenerals.RowTemplate.Height = 24
        Me.DGMovimientoGenerals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGMovimientoGenerals.ShowCellErrors = False
        Me.DGMovimientoGenerals.ShowCellToolTips = False
        Me.DGMovimientoGenerals.ShowEditingIcon = False
        Me.DGMovimientoGenerals.ShowRowErrors = False
        Me.DGMovimientoGenerals.Size = New System.Drawing.Size(1009, 383)
        Me.DGMovimientoGenerals.TabIndex = 1
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Panel6.Controls.Add(Me.Label12)
        Me.Panel6.Controls.Add(Me.cbFormaPago)
        Me.Panel6.Controls.Add(Me.chkDescuento)
        Me.Panel6.Controls.Add(Me.btnVerRecibos)
        Me.Panel6.Controls.Add(Me.Panel13)
        Me.Panel6.Controls.Add(Me.ChkMora)
        Me.Panel6.Location = New System.Drawing.Point(4, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1009, 52)
        Me.Panel6.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(212, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Forma de pago:"
        '
        'cbFormaPago
        '
        Me.cbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFormaPago.FormattingEnabled = True
        Me.cbFormaPago.Location = New System.Drawing.Point(306, 16)
        Me.cbFormaPago.Name = "cbFormaPago"
        Me.cbFormaPago.Size = New System.Drawing.Size(158, 21)
        Me.cbFormaPago.TabIndex = 9
        '
        'chkDescuento
        '
        Me.chkDescuento.AutoSize = True
        Me.chkDescuento.Checked = True
        Me.chkDescuento.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDescuento.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDescuento.ForeColor = System.Drawing.Color.Black
        Me.chkDescuento.Location = New System.Drawing.Point(120, 4)
        Me.chkDescuento.Name = "chkDescuento"
        Me.chkDescuento.Size = New System.Drawing.Size(91, 18)
        Me.chkDescuento.TabIndex = 8
        Me.chkDescuento.Text = "Descuento"
        Me.ToolsDetalle.SetToolTip(Me.chkDescuento, "Aplica descuento")
        Me.chkDescuento.UseVisualStyleBackColor = True
        '
        'btnVerRecibos
        '
        Me.btnVerRecibos.BackColor = System.Drawing.Color.White
        Me.btnVerRecibos.FlatAppearance.BorderSize = 0
        Me.btnVerRecibos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVerRecibos.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerRecibos.Image = Global.Aguiñagalde.UI.My.Resources.Resources.seo
        Me.btnVerRecibos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnVerRecibos.Location = New System.Drawing.Point(5, 4)
        Me.btnVerRecibos.Name = "btnVerRecibos"
        Me.btnVerRecibos.Padding = New System.Windows.Forms.Padding(0, 0, 7, 0)
        Me.btnVerRecibos.Size = New System.Drawing.Size(97, 43)
        Me.btnVerRecibos.TabIndex = 2
        Me.btnVerRecibos.Text = "Ver recibos"
        Me.btnVerRecibos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnVerRecibos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolsDetalle.SetToolTip(Me.btnVerRecibos, "Generar Pago")
        Me.btnVerRecibos.UseVisualStyleBackColor = False
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.btnMarcar)
        Me.Panel13.Controls.Add(Me.txtImporteMarcar)
        Me.Panel13.Controls.Add(Me.lbltxtImporte)
        Me.Panel13.Controls.Add(Me.btnPagar)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel13.Location = New System.Drawing.Point(470, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(539, 52)
        Me.Panel13.TabIndex = 7
        '
        'btnMarcar
        '
        Me.btnMarcar.BackColor = System.Drawing.Color.White
        Me.btnMarcar.FlatAppearance.BorderSize = 0
        Me.btnMarcar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMarcar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMarcar.Image = Global.Aguiñagalde.UI.My.Resources.Resources.Check
        Me.btnMarcar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMarcar.Location = New System.Drawing.Point(295, 7)
        Me.btnMarcar.Name = "btnMarcar"
        Me.btnMarcar.Size = New System.Drawing.Size(117, 39)
        Me.btnMarcar.TabIndex = 3
        Me.btnMarcar.Text = "Marcar"
        Me.btnMarcar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolsDetalle.SetToolTip(Me.btnMarcar, "Marcar el importe en grilla")
        Me.btnMarcar.UseVisualStyleBackColor = False
        '
        'txtImporteMarcar
        '
        Me.txtImporteMarcar.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImporteMarcar.Location = New System.Drawing.Point(101, 11)
        Me.txtImporteMarcar.Name = "txtImporteMarcar"
        Me.txtImporteMarcar.Size = New System.Drawing.Size(188, 30)
        Me.txtImporteMarcar.TabIndex = 2
        Me.txtImporteMarcar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbltxtImporte
        '
        Me.lbltxtImporte.AutoSize = True
        Me.lbltxtImporte.BackColor = System.Drawing.Color.Transparent
        Me.lbltxtImporte.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltxtImporte.ForeColor = System.Drawing.Color.White
        Me.lbltxtImporte.Location = New System.Drawing.Point(15, 14)
        Me.lbltxtImporte.Name = "lbltxtImporte"
        Me.lbltxtImporte.Size = New System.Drawing.Size(77, 23)
        Me.lbltxtImporte.TabIndex = 1
        Me.lbltxtImporte.Text = "Importe"
        '
        'btnPagar
        '
        Me.btnPagar.BackColor = System.Drawing.Color.White
        Me.btnPagar.FlatAppearance.BorderSize = 0
        Me.btnPagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPagar.Image = Global.Aguiñagalde.UI.My.Resources.Resources.Cobrar
        Me.btnPagar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPagar.Location = New System.Drawing.Point(417, 7)
        Me.btnPagar.Name = "btnPagar"
        Me.btnPagar.Size = New System.Drawing.Size(117, 39)
        Me.btnPagar.TabIndex = 0
        Me.btnPagar.Text = "Generar pago"
        Me.btnPagar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolsDetalle.SetToolTip(Me.btnPagar, "Generar Pago")
        Me.btnPagar.UseVisualStyleBackColor = False
        '
        'ChkMora
        '
        Me.ChkMora.AutoSize = True
        Me.ChkMora.Checked = True
        Me.ChkMora.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkMora.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkMora.ForeColor = System.Drawing.Color.Black
        Me.ChkMora.Location = New System.Drawing.Point(120, 28)
        Me.ChkMora.Name = "ChkMora"
        Me.ChkMora.Size = New System.Drawing.Size(57, 18)
        Me.ChkMora.TabIndex = 4
        Me.ChkMora.Text = "Mora"
        Me.ToolsDetalle.SetToolTip(Me.ChkMora, "Se debe cobrar mora:")
        Me.ChkMora.UseVisualStyleBackColor = True
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel8.Controls.Add(Me.Panel10)
        Me.Panel8.Controls.Add(Me.Panel9)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel8.Location = New System.Drawing.Point(0, 574)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1018, 188)
        Me.Panel8.TabIndex = 4
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel10.Controls.Add(Me.Panel11)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(631, 188)
        Me.Panel10.TabIndex = 2
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.White
        Me.Panel11.Controls.Add(Me.GroupBox1)
        Me.Panel11.Controls.Add(Me.SubTotalD)
        Me.Panel11.Controls.Add(Me.subTotalP)
        Me.Panel11.Controls.Add(Me.Label15)
        Me.Panel11.Controls.Add(Me.Label16)
        Me.Panel11.Controls.Add(Me.txtDescDolares)
        Me.Panel11.Controls.Add(Me.txtDescPesos)
        Me.Panel11.Controls.Add(Me.txtMoraTotalDolares)
        Me.Panel11.Controls.Add(Me.txtMoraTotalPesos)
        Me.Panel11.Controls.Add(Me.txtSaldoDolares)
        Me.Panel11.Controls.Add(Me.txtSaldoPesos)
        Me.Panel11.Controls.Add(Me.Label9)
        Me.Panel11.Controls.Add(Me.Label10)
        Me.Panel11.Controls.Add(Me.Label8)
        Me.Panel11.Controls.Add(Me.Label7)
        Me.Panel11.Controls.Add(Me.Label6)
        Me.Panel11.Controls.Add(Me.Label5)
        Me.Panel11.Location = New System.Drawing.Point(9, 7)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(614, 176)
        Me.Panel11.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSubPesos)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtGranTotal)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 79)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(395, 90)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sub totales"
        '
        'txtSubPesos
        '
        Me.txtSubPesos.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubPesos.Location = New System.Drawing.Point(165, 20)
        Me.txtSubPesos.Name = "txtSubPesos"
        Me.txtSubPesos.ReadOnly = True
        Me.txtSubPesos.Size = New System.Drawing.Size(215, 24)
        Me.txtSubPesos.TabIndex = 28
        Me.txtSubPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(10, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 17)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "Sub total pesos:"
        '
        'txtGranTotal
        '
        Me.txtGranTotal.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGranTotal.Location = New System.Drawing.Point(165, 54)
        Me.txtGranTotal.Name = "txtGranTotal"
        Me.txtGranTotal.ReadOnly = True
        Me.txtGranTotal.Size = New System.Drawing.Size(215, 24)
        Me.txtGranTotal.TabIndex = 26
        Me.txtGranTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 17)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Total en pesos:"
        '
        'SubTotalD
        '
        Me.SubTotalD.Enabled = False
        Me.SubTotalD.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubTotalD.Location = New System.Drawing.Point(498, 41)
        Me.SubTotalD.Name = "SubTotalD"
        Me.SubTotalD.ReadOnly = True
        Me.SubTotalD.Size = New System.Drawing.Size(107, 24)
        Me.SubTotalD.TabIndex = 28
        Me.SubTotalD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'subTotalP
        '
        Me.subTotalP.Enabled = False
        Me.subTotalP.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.subTotalP.Location = New System.Drawing.Point(498, 12)
        Me.subTotalP.Name = "subTotalP"
        Me.subTotalP.ReadOnly = True
        Me.subTotalP.Size = New System.Drawing.Size(107, 24)
        Me.subTotalP.TabIndex = 27
        Me.subTotalP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(433, 47)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 13)
        Me.Label15.TabIndex = 26
        Me.Label15.Text = "Total U$S"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(433, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(40, 13)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "Total $"
        '
        'txtDescDolares
        '
        Me.txtDescDolares.Enabled = False
        Me.txtDescDolares.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescDolares.Location = New System.Drawing.Point(349, 44)
        Me.txtDescDolares.Name = "txtDescDolares"
        Me.txtDescDolares.ReadOnly = True
        Me.txtDescDolares.Size = New System.Drawing.Size(83, 24)
        Me.txtDescDolares.TabIndex = 23
        Me.txtDescDolares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescPesos
        '
        Me.txtDescPesos.Enabled = False
        Me.txtDescPesos.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescPesos.Location = New System.Drawing.Point(349, 15)
        Me.txtDescPesos.Name = "txtDescPesos"
        Me.txtDescPesos.ReadOnly = True
        Me.txtDescPesos.Size = New System.Drawing.Size(83, 24)
        Me.txtDescPesos.TabIndex = 22
        Me.txtDescPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMoraTotalDolares
        '
        Me.txtMoraTotalDolares.Enabled = False
        Me.txtMoraTotalDolares.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMoraTotalDolares.Location = New System.Drawing.Point(221, 46)
        Me.txtMoraTotalDolares.Name = "txtMoraTotalDolares"
        Me.txtMoraTotalDolares.ReadOnly = True
        Me.txtMoraTotalDolares.Size = New System.Drawing.Size(82, 24)
        Me.txtMoraTotalDolares.TabIndex = 21
        Me.txtMoraTotalDolares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMoraTotalPesos
        '
        Me.txtMoraTotalPesos.Enabled = False
        Me.txtMoraTotalPesos.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMoraTotalPesos.Location = New System.Drawing.Point(221, 15)
        Me.txtMoraTotalPesos.Name = "txtMoraTotalPesos"
        Me.txtMoraTotalPesos.ReadOnly = True
        Me.txtMoraTotalPesos.Size = New System.Drawing.Size(82, 24)
        Me.txtMoraTotalPesos.TabIndex = 20
        Me.txtMoraTotalPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSaldoDolares
        '
        Me.txtSaldoDolares.Enabled = False
        Me.txtSaldoDolares.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaldoDolares.Location = New System.Drawing.Point(74, 46)
        Me.txtSaldoDolares.Name = "txtSaldoDolares"
        Me.txtSaldoDolares.ReadOnly = True
        Me.txtSaldoDolares.Size = New System.Drawing.Size(103, 24)
        Me.txtSaldoDolares.TabIndex = 19
        Me.txtSaldoDolares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSaldoPesos
        '
        Me.txtSaldoPesos.Enabled = False
        Me.txtSaldoPesos.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaldoPesos.Location = New System.Drawing.Point(74, 15)
        Me.txtSaldoPesos.Name = "txtSaldoPesos"
        Me.txtSaldoPesos.ReadOnly = True
        Me.txtSaldoPesos.Size = New System.Drawing.Size(103, 24)
        Me.txtSaldoPesos.TabIndex = 18
        Me.txtSaldoPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(183, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Mora"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(183, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Mora"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(306, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Desc."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(306, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Desc."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(10, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Sub Total:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(11, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Sub Total:"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel9.Controls.Add(Me.Panel15)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel9.Location = New System.Drawing.Point(660, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(358, 188)
        Me.Panel9.TabIndex = 1
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.White
        Me.Panel15.Controls.Add(Me.txtPesos)
        Me.Panel15.Controls.Add(Me.Label4)
        Me.Panel15.Controls.Add(Me.Panel17)
        Me.Panel15.Controls.Add(Me.Panel16)
        Me.Panel15.Location = New System.Drawing.Point(8, 7)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(338, 176)
        Me.Panel15.TabIndex = 8
        '
        'txtPesos
        '
        Me.txtPesos.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPesos.Location = New System.Drawing.Point(130, 141)
        Me.txtPesos.Name = "txtPesos"
        Me.txtPesos.Size = New System.Drawing.Size(199, 24)
        Me.txtPesos.TabIndex = 30
        Me.txtPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(121, 36)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Total a cobrar en Pesos"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.White
        Me.Panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel17.Controls.Add(Me.txtMoraDolares)
        Me.Panel17.Controls.Add(Me.txtImporteDolares)
        Me.Panel17.Controls.Add(Me.Label20)
        Me.Panel17.Controls.Add(Me.Label22)
        Me.Panel17.Controls.Add(Me.txtBonificacionDolares)
        Me.Panel17.Controls.Add(Me.Label21)
        Me.Panel17.Location = New System.Drawing.Point(169, 3)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(163, 129)
        Me.Panel17.TabIndex = 1
        '
        'txtMoraDolares
        '
        Me.txtMoraDolares.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMoraDolares.Location = New System.Drawing.Point(43, 100)
        Me.txtMoraDolares.Name = "txtMoraDolares"
        Me.txtMoraDolares.Size = New System.Drawing.Size(105, 24)
        Me.txtMoraDolares.TabIndex = 11
        Me.txtMoraDolares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtImporteDolares
        '
        Me.txtImporteDolares.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImporteDolares.Location = New System.Drawing.Point(6, 30)
        Me.txtImporteDolares.Name = "txtImporteDolares"
        Me.txtImporteDolares.Size = New System.Drawing.Size(153, 24)
        Me.txtImporteDolares.TabIndex = 7
        Me.txtImporteDolares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(2, 107)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(37, 13)
        Me.Label20.TabIndex = 10
        Me.Label20.Text = "Mora.:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(36, 11)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(112, 13)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "Importe en dolares"
        '
        'txtBonificacionDolares
        '
        Me.txtBonificacionDolares.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonificacionDolares.Location = New System.Drawing.Point(43, 65)
        Me.txtBonificacionDolares.Name = "txtBonificacionDolares"
        Me.txtBonificacionDolares.Size = New System.Drawing.Size(105, 24)
        Me.txtBonificacionDolares.TabIndex = 9
        Me.txtBonificacionDolares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(4, 72)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(35, 13)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "Bonf.:"
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.White
        Me.Panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel16.Controls.Add(Me.txtMoraPesos)
        Me.Panel16.Controls.Add(Me.Label19)
        Me.Panel16.Controls.Add(Me.txtBonificacionPesos)
        Me.Panel16.Controls.Add(Me.Label18)
        Me.Panel16.Controls.Add(Me.txtImportePesos)
        Me.Panel16.Controls.Add(Me.Label17)
        Me.Panel16.Location = New System.Drawing.Point(4, 3)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(162, 129)
        Me.Panel16.TabIndex = 0
        '
        'txtMoraPesos
        '
        Me.txtMoraPesos.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMoraPesos.Location = New System.Drawing.Point(51, 99)
        Me.txtMoraPesos.Name = "txtMoraPesos"
        Me.txtMoraPesos.Size = New System.Drawing.Size(105, 24)
        Me.txtMoraPesos.TabIndex = 5
        Me.txtMoraPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 106)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(37, 13)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Mora.:"
        '
        'txtBonificacionPesos
        '
        Me.txtBonificacionPesos.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonificacionPesos.Location = New System.Drawing.Point(51, 64)
        Me.txtBonificacionPesos.Name = "txtBonificacionPesos"
        Me.txtBonificacionPesos.Size = New System.Drawing.Size(105, 24)
        Me.txtBonificacionPesos.TabIndex = 3
        Me.txtBonificacionPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 71)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(35, 13)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "Bonf.:"
        '
        'txtImportePesos
        '
        Me.txtImportePesos.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImportePesos.Location = New System.Drawing.Point(4, 30)
        Me.txtImportePesos.Name = "txtImportePesos"
        Me.txtImportePesos.Size = New System.Drawing.Size(152, 24)
        Me.txtImportePesos.TabIndex = 1
        Me.txtImportePesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(15, 11)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(104, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Importe en pesos"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtEstado)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1018, 125)
        Me.Panel2.TabIndex = 2
        '
        'txtEstado
        '
        Me.txtEstado.AutoSize = True
        Me.txtEstado.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstado.ForeColor = System.Drawing.Color.Red
        Me.txtEstado.Location = New System.Drawing.Point(6, 11)
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.Size = New System.Drawing.Size(0, 19)
        Me.txtEstado.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel7)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(483, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(535, 125)
        Me.Panel4.TabIndex = 1
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.LinkCuenta)
        Me.Panel7.Controls.Add(Me.txtNombre)
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Controls.Add(Me.txtDireccion)
        Me.Panel7.Controls.Add(Me.txtCuenta)
        Me.Panel7.Controls.Add(Me.Label2)
        Me.Panel7.Location = New System.Drawing.Point(3, 6)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(523, 115)
        Me.Panel7.TabIndex = 0
        '
        'LinkCuenta
        '
        Me.LinkCuenta.AutoSize = True
        Me.LinkCuenta.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkCuenta.Location = New System.Drawing.Point(35, 10)
        Me.LinkCuenta.Name = "LinkCuenta"
        Me.LinkCuenta.Size = New System.Drawing.Size(57, 17)
        Me.LinkCuenta.TabIndex = 13
        Me.LinkCuenta.TabStop = True
        Me.LinkCuenta.Text = "Cuenta:"
        '
        'txtNombre
        '
        Me.txtNombre.Enabled = False
        Me.txtNombre.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombre.Location = New System.Drawing.Point(96, 37)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(416, 27)
        Me.txtNombre.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 19)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Nombre:"
        '
        'txtDireccion
        '
        Me.txtDireccion.Enabled = False
        Me.txtDireccion.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDireccion.Location = New System.Drawing.Point(96, 72)
        Me.txtDireccion.Multiline = True
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(416, 37)
        Me.txtDireccion.TabIndex = 10
        '
        'txtCuenta
        '
        Me.txtCuenta.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCuenta.Location = New System.Drawing.Point(96, 5)
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(416, 27)
        Me.txtCuenta.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 19)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Direccion:"
        '
        'ToolsDetalle
        '
        Me.ToolsDetalle.IsBalloon = True
        Me.ToolsDetalle.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolsDetalle.ToolTipTitle = "Funcion de este boton:"
        '
        'BW
        '
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipText = "qweqwe"
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.DGMovimientoGenerals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents txtDescDolares As System.Windows.Forms.TextBox
    Friend WithEvents txtDescPesos As System.Windows.Forms.TextBox
    Friend WithEvents txtMoraTotalDolares As System.Windows.Forms.TextBox
    Friend WithEvents txtMoraTotalPesos As System.Windows.Forms.TextBox
    Friend WithEvents txtSaldoDolares As System.Windows.Forms.TextBox
    Friend WithEvents txtSaldoPesos As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ToolsDetalle As System.Windows.Forms.ToolTip
    Friend WithEvents SubTotalD As TextBox
    Friend WithEvents subTotalP As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtSubPesos As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtGranTotal As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents LinkCuenta As LinkLabel
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtDireccion As TextBox
    Friend WithEvents txtCuenta As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel15 As Panel
    Friend WithEvents txtPesos As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel17 As Panel
    Friend WithEvents txtMoraDolares As TextBox
    Friend WithEvents txtImporteDolares As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents txtBonificacionDolares As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents Panel16 As Panel
    Friend WithEvents txtMoraPesos As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtBonificacionPesos As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtImportePesos As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents DGMovimientoGenerals As DataGridView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents chkDescuento As CheckBox
    Friend WithEvents btnVerRecibos As Button
    Friend WithEvents Panel13 As Panel
    Friend WithEvents btnMarcar As Button
    Friend WithEvents txtImporteMarcar As TextBox
    Friend WithEvents lbltxtImporte As Label
    Friend WithEvents btnPagar As Button
    Friend WithEvents ChkMora As CheckBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cbFormaPago As ComboBox
    Friend WithEvents txtEstado As Label
    Friend WithEvents NotifyIcon1 As NotifyIcon
End Class
