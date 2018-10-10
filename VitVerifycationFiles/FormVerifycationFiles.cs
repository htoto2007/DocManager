using System;
using System.Windows.Forms;

namespace VitVerifycationFiles
{
    public partial class FormVerifycationFiles : Form
    {
        private ClassVerifycationFiles classVerifycationFiles = new ClassVerifycationFiles();

        public FormVerifycationFiles()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Название файла", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Путь");
            listView1.Columns.Add("Дата размещения");
        }

        private void timerCheckFiles_Tick(object sender, EventArgs e)
        {
            ClassVerifycationFiles.FileColection[] fileColections = classVerifycationFiles.CheckFiles();
            listView1.Items.Clear();
            foreach (ClassVerifycationFiles.FileColection fileColection in fileColections)
            {
                ListViewItem listViewItem = new ListViewItem(fileColection.name);
                listViewItem.SubItems.Add(fileColection.path);
                listViewItem.SubItems.Add(fileColection.createDateTime.ToString());
                listView1.Items.Add(listViewItem);
            }
        }
    }
}