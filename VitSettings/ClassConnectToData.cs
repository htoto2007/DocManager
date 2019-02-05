using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VitSettings
{
    public class ClassConnectToData
    {
        private TabPage TabPage;

        public ClassConnectToData(TabPage tabPage)
        {
            this.TabPage = TabPage;

                    TextBox textBoxServer = (TextBox)TabPage.Controls["textBoxServer"];
                    textBoxServer.Text = "wwwwwww";
            
        }


    }
}
