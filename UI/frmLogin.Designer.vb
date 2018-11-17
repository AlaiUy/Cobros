<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
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
        Me.PanelBack = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PanelBack_Bottom = New System.Windows.Forms.Panel()
        Me.lbl_version = New System.Windows.Forms.Label()
        Me.btnIniciar = New System.Windows.Forms.Button()
        Me.txt_Login_Pass = New System.Windows.Forms.TextBox()
        Me.txt_Login_User = New System.Windows.Forms.TextBox()
        Me.PanelBack_Top = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Informarcion = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PanelBack.SuspendLayout()
        Me.PanelBack_Bottom.SuspendLayout()
        Me.PanelBack_Top.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelBack
        '
        Me.PanelBack.BackColor = System.Drawing.Color.White
        Me.PanelBack.Controls.Add(Me.Label1)
        Me.PanelBack.Controls.Add(Me.PanelBack_Bottom)
        Me.PanelBack.Controls.Add(Me.txt_Login_Pass)
        Me.PanelBack.Controls.Add(Me.txt_Login_User)
        Me.PanelBack.Controls.Add(Me.PanelBack_Top)
        Me.PanelBack.Controls.Add(Me.Informarcion)
        Me.PanelBack.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PanelBack.Location = New System.Drawing.Point(3, 155)
        Me.PanelBack.Name = "PanelBack"
        Me.PanelBack.Size = New System.Drawing.Size(307, 207)
        Me.PanelBack.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Image = Global.Aguiñagalde.UI.My.Resources.Resources.Key
        Me.Label1.Location = New System.Drawing.Point(7, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 39)
        Me.Label1.TabIndex = 2
        '
        'PanelBack_Bottom
        '
        Me.PanelBack_Bottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.PanelBack_Bottom.Controls.Add(Me.lbl_version)
        Me.PanelBack_Bottom.Controls.Add(Me.btnIniciar)
        Me.PanelBack_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBack_Bottom.Location = New System.Drawing.Point(0, 166)
        Me.PanelBack_Bottom.Name = "PanelBack_Bottom"
        Me.PanelBack_Bottom.Size = New System.Drawing.Size(307, 41)
        Me.PanelBack_Bottom.TabIndex = 2
        '
        'lbl_version
        '
        Me.lbl_version.AutoSize = True
        Me.lbl_version.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_version.ForeColor = System.Drawing.Color.Black
        Me.lbl_version.Location = New System.Drawing.Point(8, 14)
        Me.lbl_version.Name = "lbl_version"
        Me.lbl_version.Size = New System.Drawing.Size(50, 16)
        Me.lbl_version.TabIndex = 4
        Me.lbl_version.Text = "Label2"
        '
        'btnIniciar
        '
        Me.btnIniciar.BackColor = System.Drawing.Color.Transparent
        Me.btnIniciar.FlatAppearance.BorderSize = 0
        Me.btnIniciar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnIniciar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIniciar.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIniciar.ForeColor = System.Drawing.Color.Black
        Me.btnIniciar.Image = Global.Aguiñagalde.UI.My.Resources.Resources.play_button
        Me.btnIniciar.Location = New System.Drawing.Point(251, 4)
        Me.btnIniciar.Name = "btnIniciar"
        Me.btnIniciar.Size = New System.Drawing.Size(50, 33)
        Me.btnIniciar.TabIndex = 3
        Me.btnIniciar.UseVisualStyleBackColor = False
        '
        'txt_Login_Pass
        '
        Me.txt_Login_Pass.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txt_Login_Pass.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Login_Pass.Font = New System.Drawing.Font("Tahoma", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Login_Pass.Location = New System.Drawing.Point(50, 97)
        Me.txt_Login_Pass.Name = "txt_Login_Pass"
        Me.txt_Login_Pass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.txt_Login_Pass.Size = New System.Drawing.Size(253, 36)
        Me.txt_Login_Pass.TabIndex = 3
        Me.txt_Login_Pass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_Login_User
        '
        Me.txt_Login_User.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txt_Login_User.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Login_User.Font = New System.Drawing.Font("Tahoma", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Login_User.Location = New System.Drawing.Point(50, 55)
        Me.txt_Login_User.Name = "txt_Login_User"
        Me.txt_Login_User.Size = New System.Drawing.Size(253, 36)
        Me.txt_Login_User.TabIndex = 2
        Me.txt_Login_User.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PanelBack_Top
        '
        Me.PanelBack_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.PanelBack_Top.Controls.Add(Me.Label4)
        Me.PanelBack_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelBack_Top.Location = New System.Drawing.Point(0, 0)
        Me.PanelBack_Top.Name = "PanelBack_Top"
        Me.PanelBack_Top.Size = New System.Drawing.Size(307, 50)
        Me.PanelBack_Top.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(6, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(146, 19)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "LOGIN - COBROS"
        '
        'Informarcion
        '
        Me.Informarcion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Informarcion.ForeColor = System.Drawing.Color.Black
        Me.Informarcion.Location = New System.Drawing.Point(50, 139)
        Me.Informarcion.Name = "Informarcion"
        Me.Informarcion.Size = New System.Drawing.Size(253, 22)
        Me.Informarcion.TabIndex = 2
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Aguiñagalde.UI.My.Resources.Resources.logo1
        Me.PictureBox1.Location = New System.Drawing.Point(81, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(147, 136)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'frmLogin
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(313, 366)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PanelBack)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parametros"
        Me.TransparencyKey = System.Drawing.Color.Lime
        Me.PanelBack.ResumeLayout(False)
        Me.PanelBack.PerformLayout()
        Me.PanelBack_Bottom.ResumeLayout(False)
        Me.PanelBack_Bottom.PerformLayout()
        Me.PanelBack_Top.ResumeLayout(False)
        Me.PanelBack_Top.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelBack As Panel
    Friend WithEvents PanelBack_Bottom As Panel
    Friend WithEvents btnIniciar As Button
    Friend WithEvents txt_Login_Pass As TextBox
    Friend WithEvents txt_Login_User As TextBox
    Friend WithEvents PanelBack_Top As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Informarcion As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lbl_version As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
