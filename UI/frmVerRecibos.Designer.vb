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
        Me.btnImprimirRecibo = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DGVistaRecivos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
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
        Me.Panel1.Location = New System.Drawing.Point(6, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(771, 544)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.DGVistaRecivos)
        Me.Panel3.Location = New System.Drawing.Point(0, 81)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(771, 463)
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
        Me.DGVistaRecivos.Location = New System.Drawing.Point(0, 3)
        Me.DGVistaRecivos.MultiSelect = False
        Me.DGVistaRecivos.Name = "DGVistaRecivos"
        Me.DGVistaRecivos.ReadOnly = True
        Me.DGVistaRecivos.RowHeadersVisible = False
        Me.DGVistaRecivos.RowTemplate.Height = 24
        Me.DGVistaRecivos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVistaRecivos.Size = New System.Drawing.Size(771, 460)
        Me.DGVistaRecivos.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel2.Controls.Add(Me.btnImprimirRecibo)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(771, 78)
        Me.Panel2.TabIndex = 0
        '
        'btnImprimirRecibo
        '
        Me.btnImprimirRecibo.AutoEllipsis = True
        Me.btnImprimirRecibo.BackColor = System.Drawing.Color.White
        Me.btnImprimirRecibo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimirRecibo.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimirRecibo.Image = Global.Aguiñagalde.UI.My.Resources.Resources.impresora
        Me.btnImprimirRecibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimirRecibo.Location = New System.Drawing.Point(6, 6)
        Me.btnImprimirRecibo.Name = "btnImprimirRecibo"
        Me.btnImprimirRecibo.Padding = New System.Windows.Forms.Padding(0, 0, 7, 0)
        Me.btnImprimirRecibo.Size = New System.Drawing.Size(187, 69)
        Me.btnImprimirRecibo.TabIndex = 2
        Me.btnImprimirRecibo.Text = "Imprimir seleccionado"
        Me.btnImprimirRecibo.UseVisualStyleBackColor = False
        '
        'frmVerRecibos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(782, 553)
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmVerRecibos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmVerRecibos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.DGVistaRecivos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DGVistaRecivos As System.Windows.Forms.DataGridView
    Friend WithEvents btnImprimirRecibo As System.Windows.Forms.Button
End Class
