using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitFileCard
{
    public class ClassFileCard
    {
        public void create()
        {
            FormFileCard formFileCard = new FormFileCard();
            formFileCard.ShowDialog();
        }
    }
}
