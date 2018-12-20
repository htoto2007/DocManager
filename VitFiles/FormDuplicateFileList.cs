using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VitFiles
{
    public partial class FormDuplicateFileList : Form
    {
        public FormDuplicateFileList(string[] fileNames)
        {
            InitializeComponent();
            Init(fileNames);
        }

        private void Init(string[] fileNames)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = false;
            listView1.Columns.Add("Имя файла");
            foreach (var fileName in fileNames)
            {
                ListViewItem item = new ListViewItem(fileName);
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
