namespace Aguiñagalde.Reportes
{
    partial class frmImpresion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RPViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // RPViewer
            // 
            this.RPViewer.ActiveViewIndex = -1;
            this.RPViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RPViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.RPViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RPViewer.Location = new System.Drawing.Point(0, 0);
            this.RPViewer.Name = "RPViewer";
            this.RPViewer.ShowCloseButton = false;
            this.RPViewer.ShowCopyButton = false;
            this.RPViewer.ShowExportButton = false;
            this.RPViewer.ShowGroupTreeButton = false;
            this.RPViewer.ShowLogo = false;
            this.RPViewer.ShowParameterPanelButton = false;
            this.RPViewer.ShowRefreshButton = false;
            this.RPViewer.ShowTextSearchButton = false;
            this.RPViewer.ShowZoomButton = false;
            this.RPViewer.Size = new System.Drawing.Size(782, 553);
            this.RPViewer.TabIndex = 0;
            this.RPViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmImpresion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.RPViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmImpresion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmImpresion";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmImpresion_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer RPViewer;
    }
}