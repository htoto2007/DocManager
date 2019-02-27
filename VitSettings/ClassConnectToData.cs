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
