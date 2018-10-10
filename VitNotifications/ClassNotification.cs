using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VitNotifications
{
    public class ClassNotification {

        public void error(string message)
        {
            string currMthod = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string currClass = this.ToString();
            MessageBox.Show(currClass + "->" + currMthod + " saye: " + e.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
