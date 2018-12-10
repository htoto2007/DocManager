﻿using System;
using System.Windows.Forms;
using VitAccessGroup;
using VitSubdivision;
using VitUserPositions;
using static VitUsers.ClassUsers;

namespace VitUsers
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
            Init();
            
        }

        private void Init()
        {
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            //VitIcons.ClassImageList classImageList = new VitIcons.ClassImageList();
            listViewUsers.Clear();
            listViewUsers.MultiSelect = true;
            listViewUsers.LargeImageList = formCompanents.imageListColor;
            listViewUsers.SmallImageList = formCompanents.imageListColor;
            listViewUsers.BeginUpdate();
            listViewUsers.View = View.Details;
            listViewUsers.FullRowSelect = true;
            listViewUsers.HideSelection = false;
            listViewUsers.Columns.Clear();
            listViewUsers.Columns.Add("");
            listViewUsers.Columns.Add("id");
            listViewUsers.Columns.Add("Имя");
            listViewUsers.Columns.Add("Логин");
            listViewUsers.Columns.Add("Тип доступа");
            listViewUsers.Columns.Add("Пароль");
            listViewUsers.Columns.Add("Должность");
            listViewUsers.Columns.Add("Подразделение");
            listViewUsers.Columns.Add("Электронная почта");
            ClassUsers classUsers = new ClassUsers();
            UserColection[] userColections = classUsers.GetAllUsers();
            VitAccessGroup.ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            foreach (ClassUsers.UserColection userColection in userColections)
            {
                string accessGroupName = classAccessGroup.getNameById(userColection.idAccessGroup);

                ListViewItem listViewItem = new ListViewItem();

                if (accessGroupName == "Админ")
                {
                    listViewItem.ImageKey = "icons8-crown-50.png";
                }
                else
                {
                    listViewItem.ImageKey = "icons8-user-avatar-48.png";
                }

                listViewItem.SubItems.Add(userColection.id.ToString()).Name = "id";
                listViewItem.SubItems.Add(userColection.firstName + " " + userColection.lastName + " " + userColection.middleName).Name = "name";
                listViewItem.SubItems.Add(userColection.login).Name = "login";
                listViewItem.SubItems.Add(accessGroupName).Name = "accessGroup";
                listViewItem.SubItems.Add(userColection.password).Name = "password";
                VitUserPositions.ClassUserPositions classUserPositions = new ClassUserPositions();
                var userPosition = classUserPositions.getInfoById(userColection.idPosition);
                listViewItem.SubItems.Add(userPosition.name).Name = "userPosition";
                VitSubdivision.ClassSubdivision classSubdivision = new ClassSubdivision();
                var subdivision = classSubdivision.getInfoById(userColection.idSubdivision);
                listViewItem.SubItems.Add(subdivision.name).Name = "subdivision";
                listViewItem.SubItems.Add(userColection.mail).Name = "Mail";
                listViewUsers.Items.Add(listViewItem);
            }
            listViewUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewUsers.EndUpdate();
            listViewUsers.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormUserEdit formUserAdd = new FormUserEdit();
            formUserAdd.ShowDialog();
            Init();
        }
        

        private void listViewUsers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listViewUsers.SelectedItems.Count == 1) {
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
            if (listViewUsers.SelectedItems.Count <= 0)
            {
                buttonUsersDeleteUser.Enabled = false;
            }
            if (listViewUsers.SelectedItems.Count >= 1)
            {
                buttonUsersDeleteUser.Enabled = true;
            }
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
    }
}