using System;
using System.Windows.Forms;
using VitAccessGroup;
using VitSubdivision;
using VitUserPositions;
using static VitUsers.ClassUsers;

namespace VitUsers
{
    public partial class FormUsers : Form
    {
        ClassTabPageUsers ClassTabPageUsers = new ClassTabPageUsers();
        ClassTabPageGroups ClassTabPageGroups = new ClassTabPageGroups();

        public FormUsers()
        {
            InitializeComponent();
            Init();
            
        }

        private void Init()
        {
            ClassTabPageUsers.initUsers(listViewUsers);
            ClassTabPageGroups.initListViewGroups(listViewUsersGroups);
            ClassTabPageGroups.initListViewGroups(listViewGroups);
        }

        private void ShowSelectUserProperties()
        {
            if (listViewUsers.SelectedItems.Count == 1)
            {
                textBoxUsersUserName.Text = listViewUsers.SelectedItems[0].SubItems["name"].Text;
                textBoxUsersAccessGroupName.Text = listViewUsers.SelectedItems[0].SubItems["accessGroup"].Text;
                textBoxUsersUserPositions.Text = listViewUsers.SelectedItems[0].SubItems["userPosition"].Text;
                textBoxUsersSubdivisions.Text = listViewUsers.SelectedItems[0].SubItems["subdivision"].Text;
                textBoxUsersUserMail.Text = listViewUsers.SelectedItems[0].SubItems["Mail"].Text;
            }

            if (listViewUsers.SelectedItems.Count != 1)
            {
                textBoxUsersUserName.Text = "";
                textBoxUsersAccessGroupName.Text = "";
                textBoxUsersUserPositions.Text = "";
                textBoxUsersSubdivisions.Text = "";
                textBoxUsersUserMail.Text = "";
            }
        }

        private void EnableButtonsBySelectItems()
        {
            if (listViewUsers.SelectedItems.Count == 1)
            {
                buttonUsersUserEdit.Enabled = true;
            }

            if (listViewUsers.SelectedItems.Count != 1)
            {
                buttonUsersUserEdit.Enabled = false;
            }

            if (listViewUsers.SelectedItems.Count <= 0)
            {
                buttonUsersDeleteUser.Enabled = false;
            }

            if (listViewUsers.SelectedItems.Count >= 1)
            {
                buttonUsersDeleteUser.Enabled = true;
            }
        }

        private void buttonUsersAddUser_Click(object sender, EventArgs e)
        {
            FormUserEdit formUserAdd = new FormUserEdit();
            formUserAdd.ShowDialog();
            Init();
        }
        

        private void listViewUsers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ShowSelectUserProperties();
            EnableButtonsBySelectItems();
        }

        private void buttonUsersDeleteUser_Click(object sender, EventArgs e)
        {
            var selectItems = listViewUsers.SelectedItems;
            VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
            DialogResult dialogResult = classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.QUESTION, "Ужадить выделеных пользователей?");
            if (dialogResult != DialogResult.Yes) return;
            ClassUsers classUsers = new ClassUsers();
            foreach (ListViewItem item in selectItems)
            {
                int id = Convert.ToInt32(item.SubItems["id"].Text);
                classUsers.deleteById(id);
            }
            Init();
        }

        private void buttonUsersUserEdit_Click(object sender, EventArgs e)
        {
            int idUser = Convert.ToInt32(listViewUsers.SelectedItems[0].SubItems["id"].Text);
            FormUserPropertyEdit formUserPropertyEdit = new FormUserPropertyEdit(idUser);
            formUserPropertyEdit.ShowDialog();
            Init();
        }

        private void listViewGroups_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listViewGroups.SelectedItems.Count == 1)
            {
                textBoxGroupsNameGroup.Text = listViewGroups.SelectedItems[0].SubItems["name"].Text;
                textBoxGroupsUsersCount.Text = listViewGroups.SelectedItems[0].SubItems["usersCount"].Text;
                ClassAccessGroup classAccessGroup = new ClassAccessGroup();
                int id = Convert.ToInt32(listViewGroups.SelectedItems[0].SubItems["id"].Text);
                textBoxGroupsDescription.Text = classAccessGroup.getInfoById(id).description;
            }
        }
    }
}