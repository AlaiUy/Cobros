<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFiltroCLientes
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GrillaClientes = New System.Windows.Forms.DataGridView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Statuslbl = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblDatoFiltro = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ckFiltro = New System.Windows.Forms.CheckBox()
        Me.txtDato = New System.Windows.Forms.TextBox()
        Me.lblFiltro = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.GrillaClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.GrillaClientes)
        Me.Panel1.Controls.Add(Me.StatusStrip1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(888, 618)
        Me.Panel1.TabIndex = 0
        '
        'GrillaClientes
        '
        Me.GrillaClientes.AllowUserToAddRows = False
        Me.GrillaClientes.AllowUserToDeleteRows = False
        Me.GrillaClientes.AllowUserToResizeRows = False
        Me.GrillaClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.GrillaClientes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.GrillaClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrillaClientes.Location = New System.Drawing.Point(5, 60)
        Me.GrillaClientes.MultiSelect = False
        Me.GrillaClientes.Name = "GrillaClientes"
        Me.GrillaClientes.ReadOnly = True
        Me.GrillaClientes.RowHeadersVisible = False
        Me.GrillaClientes.RowTemplate.Height = 24
        Me.GrillaClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GrillaClientes.Size = New System.Drawing.Size(879, 530)
        Me.GrillaClientes.TabIndex = 2
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Statuslbl, Me.lblDatoFiltro})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 593)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(888, 25)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Statuslbl
        '
        Me.Statuslbl.Name = "Statuslbl"
        Me.Statuslbl.Size = New System.Drawing.Size(0, 20)
        '
        'lblDatoFiltro
        '
        Me.lblDatoFiltro.Name = "lblDatoFiltro"
        Me.lblDatoFiltro.Size = New System.Drawing.Size(13, 20)
        Me.lblDatoFiltro.Text = " "
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.ckFiltro)
        Me.Panel2.Controls.Add(Me.txtDato)
        Me.Panel2.Controls.Add(Me.lblFiltro)
        Me.Panel2.Location = New System.Drawing.Point(5, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(879, 50)
        Me.Panel2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(743, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Quitar filtro (F2)"
        '
        'ckFiltro
        '
        Me.ckFiltro.AutoSize = True
        Me.ckFiltro.Checked = True
        Me.ckFiltro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckFiltro.Location = New System.Drawing.Point(331, 13)
        Me.ckFiltro.Name = "ckFiltro"
        Me.ckFiltro.Size = New System.Drawing.Size(107, 21)
        Me.ckFiltro.TabIndex = 3
        Me.ckFiltro.Text = "Filtro parcial"
        Me.ckFiltro.UseVisualStyleBackColor = True
        '
        'txtDato
        '
        Me.txtDato.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDato.Location = New System.Drawing.Point(67, 10)
        Me.txtDato.Name = "txtDato"
        Me.txtDato.Size = New System.Drawing.Size(258, 28)
        Me.txtDato.TabIndex = 2
        '
        'lblFiltro
        '
        Me.lblFiltro.AutoSize = True
        Me.lblFiltro.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFiltro.Location = New System.Drawing.Point(7, 13)
        Me.lblFiltro.Name = "lblFiltro"
        Me.lblFiltro.Size = New System.Drawing.Size(54, 21)
        Me.lblFiltro.TabIndex = 1
        Me.lblFiltro.Text = "Filtro:"
        '
        'frmFiltroCLientes
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(895, 627)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmFiltroCLientes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmFiltroCLientes"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GrillaClientes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ckFiltro As CheckBox
    Friend WithEvents txtDato As TextBox
    Friend WithEvents lblFiltro As Label
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents Statuslbl As ToolStripStatusLabel
    Friend WithEvents GrillaClientes As DataGridView
    Friend WithEvents lblDatoFiltro As ToolStripStatusLabel
    Friend WithEvents Label1 As Label
End Class
