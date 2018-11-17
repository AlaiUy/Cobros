<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVerRecibos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DGVistaRecivos = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.txtTotalSel = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAnularRecibo = New System.Windows.Forms.Button()
        Me.btnImprimirPersonal = New System.Windows.Forms.Button()
        Me.btnImprimirRecibo = New System.Windows.Forms.Button()
        Me.BGAnularRecibo = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DGVistaRecivos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(578, 442)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.DGVistaRecivos)
        Me.Panel3.Location = New System.Drawing.Point(0, 66)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(578, 376)
        Me.Panel3.TabIndex = 1
        '
        'DGVistaRecivos
        '
        Me.DGVistaRecivos.AllowUserToAddRows = False
        Me.DGVistaRecivos.AllowUserToDeleteRows = False
        Me.DGVistaRecivos.AllowUserToResizeRows = False
        Me.DGVistaRecivos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVistaRecivos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGVistaRecivos.BackgroundColor = System.Drawing.Color.White
        Me.DGVistaRecivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVistaRecivos.EnableHeadersVisualStyles = False
        Me.DGVistaRecivos.Location = New System.Drawing.Point(0, 1)
        Me.DGVistaRecivos.Margin = New System.Windows.Forms.Padding(2)
        Me.DGVistaRecivos.Name = "DGVistaRecivos"
        Me.DGVistaRecivos.ReadOnly = True
        Me.DGVistaRecivos.RowHeadersVisible = False
        Me.DGVistaRecivos.RowTemplate.Height = 24
        Me.DGVistaRecivos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVistaRecivos.Size = New System.Drawing.Size(578, 374)
        Me.DGVistaRecivos.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.btnAnularRecibo)
        Me.Panel2.Controls.Add(Me.btnImprimirPersonal)
        Me.Panel2.Controls.Add(Me.btnImprimirRecibo)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(578, 63)
        Me.Panel2.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.txtTotalSel)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(367, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(211, 63)
        Me.Panel4.TabIndex = 5
        '
        'txtTotalSel
        '
        Me.txtTotalSel.Location = New System.Drawing.Point(52, 26)
        Me.txtTotalSel.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTotalSel.Name = "txtTotalSel"
        Me.txtTotalSel.Size = New System.Drawing.Size(112, 20)
        Me.txtTotalSel.TabIndex = 1
        Me.txtTotalSel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(50, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Total seleccionado"
        '
        'btnAnularRecibo
        '
        Me.btnAnularRecibo.AutoEllipsis = True
        Me.btnAnularRecibo.BackColor = System.Drawing.Color.White
        Me.btnAnularRecibo.Enabled = False
        Me.btnAnularRecibo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnularRecibo.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnularRecibo.Image = Global.Aguiñagalde.UI.My.Resources.Resources.impresora
        Me.btnAnularRecibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnularRecibo.Location = New System.Drawing.Point(274, 5)
        Me.btnAnularRecibo.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAnularRecibo.Name = "btnAnularRecibo"
        Me.btnAnularRecibo.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnAnularRecibo.Size = New System.Drawing.Size(88, 56)
        Me.btnAnularRecibo.TabIndex = 4
        Me.btnAnularRecibo.Text = "Anular"
        Me.btnAnularRecibo.UseVisualStyleBackColor = False
        Me.btnAnularRecibo.Visible = False
        '
        'btnImprimirPersonal
        '
        Me.btnImprimirPersonal.AutoEllipsis = True
        Me.btnImprimirPersonal.BackColor = System.Drawing.Color.White
        Me.btnImprimirPersonal.Enabled = False
        Me.btnImprimirPersonal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimirPersonal.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimirPersonal.Image = Global.Aguiñagalde.UI.My.Resources.Resources.impresora
        Me.btnImprimirPersonal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimirPersonal.Location = New System.Drawing.Point(148, 6)
        Me.btnImprimirPersonal.Margin = New System.Windows.Forms.Padding(2)
        Me.btnImprimirPersonal.Name = "btnImprimirPersonal"
        Me.btnImprimirPersonal.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnImprimirPersonal.Size = New System.Drawing.Size(122, 56)
        Me.btnImprimirPersonal.TabIndex = 3
        Me.btnImprimirPersonal.Text = "Imprimir Personal"
        Me.btnImprimirPersonal.UseVisualStyleBackColor = False
        Me.btnImprimirPersonal.Visible = False
        '
        'btnImprimirRecibo
        '
        Me.btnImprimirRecibo.AutoEllipsis = True
        Me.btnImprimirRecibo.BackColor = System.Drawing.Color.White
        Me.btnImprimirRecibo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimirRecibo.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimirRecibo.Image = Global.Aguiñagalde.UI.My.Resources.Resources.impresora
        Me.btnImprimirRecibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimirRecibo.Location = New System.Drawing.Point(4, 5)
        Me.btnImprimirRecibo.Margin = New System.Windows.Forms.Padding(2)
        Me.btnImprimirRecibo.Name = "btnImprimirRecibo"
        Me.btnImprimirRecibo.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnImprimirRecibo.Size = New System.Drawing.Size(140, 56)
        Me.btnImprimirRecibo.TabIndex = 2
        Me.btnImprimirRecibo.Text = "Imprimir seleccionado"
        Me.btnImprimirRecibo.UseVisualStyleBackColor = False
        '
        'BGAnularRecibo
        '
        Me.BGAnularRecibo.WorkerReportsProgress = True
        '
        'frmVerRecibos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(586, 449)
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmVerRecibos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmVerRecibos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.DGVistaRecivos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnImprimirRecibo As System.Windows.Forms.Button
    Friend WithEvents btnImprimirPersonal As Button
    Friend WithEvents btnAnularRecibo As Button
    Friend WithEvents DGVistaRecivos As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents txtTotalSel As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BGAnularRecibo As System.ComponentModel.BackgroundWorker
End Class
