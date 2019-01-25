using System.Windows.Forms;
using static VitAccessGroup.ClassAccessGroup;

namespace VitAccessGroup
{
    public partial class FormAccessGroup : Form
    {
        public FormAccessGroup()
        {
            InitializeComponent();
            initFormAccessGroup();
        }

        private void FormAccessGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
        }

        private void buttonClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        public void initFormAccessGroup()
        {
            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            AccessGroupCollection[] accessGroups = classAccessGroup.getInfo();
            
            listViewGroups.View = System.Windows.Forms.View.Details;
            listViewGroups.MultiSelect = true;
            listViewGroups.FullRowSelect = true;
            listViewGroups.Columns.Clear();
            listViewGroups.Columns.Add("name", 200, System.Windows.Forms.HorizontalAlignment.Center);
            listViewGroups.Columns.Add("Количество пользователей", 200, System.Windows.Forms.HorizontalAlignment.Center);
        }
    }
}