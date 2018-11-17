using System.Windows.Forms;
using Aguiñagalde.UI;

namespace Aguiñagalde.Lanzador
{
     class Iniciar
    {

     


    


        public static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI.frmParametros());
        }
    }
}
