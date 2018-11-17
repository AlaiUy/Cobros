using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Aguiñagalde.Reportes
{
    public partial class frmImpresion : Form
    {
        public frmImpresion()
        {
            InitializeComponent();
        }

        private void frmImpresion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
