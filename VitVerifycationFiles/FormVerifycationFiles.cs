using System.Threading;
using System.Windows.Forms;

namespace VitVerifycationFiles
{
    public partial class FormVerifycationFiles : Form
    {
        private ClassVerifycationFiles classVerifycationFiles = new ClassVerifycationFiles();
        private Thread threadVerify;

        public FormVerifycationFiles()
        {
            InitializeComponent();
        }

        private void start()
        {
            Invoke((MethodInvoker)delegate
            {
                listView1.View = View.Details;
                listView1.FullRowSelect = true;
                listView1.Columns.Add("Название файла", 100, HorizontalAlignment.Center);
                listView1.Columns.Add("Путь");
                listView1.Columns.Add("Дата размещения");
            });
            ClassVerifycationFiles.FileColection[] fileColections = classVerifycationFiles.CheckFiles();
            while (true)
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        listView1.Items.Clear();
                        foreach (ClassVerifycationFiles.FileColection fileColection in fileColections)
                        {
                            ListViewItem listViewItem = new ListViewItem(fileColection.name);
                            listViewItem.SubItems.Add(fileColection.path);
                            listViewItem.SubItems.Add(fileColection.createDateTime.ToString());
                            listView1.Items.Add(listViewItem);
                        }
                    });
                }
                catch (System.InvalidOperationException)
                {
                    break;
                }
                Thread.Sleep(30000);
            }
        }

        private void FormVerifycationFiles_Shown(object sender, System.EventArgs e)
        {
            threadVerify = new Thread(start);
            threadVerify.Start();
        }

        private void FormVerifycationFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            threadVerify.Abort();
        }
    }
}