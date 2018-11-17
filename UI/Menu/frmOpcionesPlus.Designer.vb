<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOpcionesPlus
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_Descatalogar = New System.Windows.Forms.Button()
        Me.btnDelacasa = New System.Windows.Forms.Button()
        Me.btnEC = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(276, 356)
        Me.Panel1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_Descatalogar, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnDelacasa, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnEC, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.15385!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 272.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(276, 356)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'btn_Descatalogar
        '
        Me.btn_Descatalogar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Descatalogar.Font = New System.Drawing.Font("Tahoma", 11.78182!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Descatalogar.Location = New System.Drawing.Point(3, 45)
        Me.btn_Descatalogar.Name = "btn_Descatalogar"
        Me.btn_Descatalogar.Size = New System.Drawing.Size(270, 35)
        Me.btn_Descatalogar.TabIndex = 1
        Me.btn_Descatalogar.Text = "Descatalogar"
        Me.btn_Descatalogar.UseVisualStyleBackColor = True
        '
        'btnDelacasa
        '
        Me.btnDelacasa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelacasa.Font = New System.Drawing.Font("Tahoma", 11.78182!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelacasa.Location = New System.Drawing.Point(3, 3)
        Me.btnDelacasa.Name = "btnDelacasa"
        Me.btnDelacasa.Size = New System.Drawing.Size(270, 35)
        Me.btnDelacasa.TabIndex = 2
        Me.btnDelacasa.Text = "Creditos de la casa"
        Me.btnDelacasa.UseVisualStyleBackColor = True
        '
        'btnEC
        '
        Me.btnEC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEC.Font = New System.Drawing.Font("Tahoma", 11.78182!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEC.Location = New System.Drawing.Point(3, 87)
        Me.btnEC.Name = "btnEC"
        Me.btnEC.Size = New System.Drawing.Size(270, 35)
        Me.btnEC.TabIndex = 0
        Me.btnEC.Text = "Entrega a cuenta"
        Me.btnEC.UseVisualStyleBackColor = True
        '
        'frmOpcionesPlus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Red
        Me.ClientSize = New System.Drawing.Size(282, 361)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmOpcionesPlus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Opciones+"
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents btnDelacasa As Button
    Friend WithEvents btn_Descatalogar As Button
    Friend WithEvents btnEC As Button
End Class
