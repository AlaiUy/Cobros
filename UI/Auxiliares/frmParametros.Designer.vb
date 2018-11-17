<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParametros
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
        Me.FileBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.pMain = New System.Windows.Forms.Panel()
        Me.DGParametros = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtValor = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pMain.SuspendLayout()
        CType(Me.DGParametros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pMain
        '
        Me.pMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pMain.BackColor = System.Drawing.Color.White
        Me.pMain.Controls.Add(Me.DGParametros)
        Me.pMain.Controls.Add(Me.Panel2)
        Me.pMain.Controls.Add(Me.Panel1)
        Me.pMain.Location = New System.Drawing.Point(5, 4)
        Me.pMain.Name = "pMain"
        Me.pMain.Size = New System.Drawing.Size(499, 483)
        Me.pMain.TabIndex = 0
        '
        'DGParametros
        '
        Me.DGParametros.AllowUserToAddRows = False
        Me.DGParametros.AllowUserToDeleteRows = False
        Me.DGParametros.AllowUserToResizeRows = False
        Me.DGParametros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGParametros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGParametros.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGParametros.Location = New System.Drawing.Point(0, 113)
        Me.DGParametros.Name = "DGParametros"
        Me.DGParametros.RowHeadersVisible = False
        Me.DGParametros.Size = New System.Drawing.Size(499, 332)
        Me.DGParametros.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Button4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 445)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(499, 38)
        Me.Panel2.TabIndex = 1
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(414, 7)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "Do It!"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.txtDescripcion)
        Me.Panel1.Controls.Add(Me.txtValor)
        Me.Panel1.Controls.Add(Me.txtID)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(499, 113)
        Me.Panel1.TabIndex = 24
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(374, 78)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 28
        Me.btnAdd.Text = "Agregar"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(259, 78)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(100, 20)
        Me.txtDescripcion.TabIndex = 27
        '
        'txtValor
        '
        Me.txtValor.Location = New System.Drawing.Point(135, 78)
        Me.txtValor.Name = "txtValor"
        Me.txtValor.Size = New System.Drawing.Size(100, 20)
        Me.txtValor.TabIndex = 26
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(10, 78)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(100, 20)
        Me.txtID.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(104, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(271, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CONFIGURACION DE PARAMETROS"
        '
        'frmParametros
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(509, 491)
        Me.Controls.Add(Me.pMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmParametros"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmParametros"
        Me.pMain.ResumeLayout(False)
        CType(Me.DGParametros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FileBrowser As FolderBrowserDialog
    Friend WithEvents pMain As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button4 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents DGParametros As DataGridView
    Friend WithEvents btnAdd As Button
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents txtValor As TextBox
    Friend WithEvents txtID As TextBox
End Class
