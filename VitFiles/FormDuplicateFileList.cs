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
        public FormDuplicateFileList(string[] fileNames, string[] targetFiles)
        {
            InitializeComponent();
            Init(fileNames, targetFiles);
        }

        private void Init(string[] fileNames, string[] targetFiles)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = false;
            listView1.Columns.Add("Имя выбранного файла");
            listView1.Columns.Add("Имя файла в директории назначения");
            for (int i = 0; i < fileNames.GetLength(0); i++)
            {
                ListViewItem item = new ListViewItem(fileNames[i]);
                item.SubItems.Add(targetFiles[i]);
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void buttonCreateIndex_Click(object sender, EventArgs e)
        {

        }
    }
}
