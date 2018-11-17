using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aguinagalde.Controles
{
    public partial class TextBoxNumerico : TextBox
    {
        public TextBoxNumerico()
        {
            InitializeComponent();
        }


        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if(Char.IsNumber(e.KeyChar)) //Al pulsar un Numero
            {
                e.Handled = false; //Se acepta (todo OK)
            }
            else if (Char.IsControl(e.KeyChar)) //Al pulsar teclas como Borrar y eso.
            {
                e.Handled = false; //Se acepta (todo OK)
            }
            else //Para todo lo demas
            {
                e.Handled = true; //No se acepta (si pulsas cualquier otra cosa pues no se envia)
            }
        }
    }
}
