using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VitAccessGroup;
using VitMysql;
using VitSettings;

namespace VitUsers
{
    public class ClassUsers
    {
        public FormUsers formUsers = new FormUsers();
        public int id = 0;
        public int idAccessGroup = 0;
        public string name = "";
        private readonly string programPath = "";
        private readonly string tmpFile = "tm.dm";
        private readonly string tmpPath = Path.GetTempPath() + @"\dm\";
        private ClassAccessGroup classAccessGroup = new ClassAccessGroup();
        private ClassMysql classMysql = new ClassMysql();
        private ClassSettings classSettings = new ClassSettings();

        public ClassUsers()
        {
            programPath = classSettings.GetProperties().generalsSttings.programPath + "\\";
        }

        public void AddUser()
        {
            string password = formUsers.textBoxPassword.Text;
            string name = formUsers.textBoxLogin.Text;
            int idAccessGroup = classAccessGroup.getIdByName(formUsers.listBoxAccessGroup.SelectedItem.ToString());

            classMysql.Insert("" +
                "INSERT INTO tb_users " +
                "SET " +
                "name = '" + name + "', " +
                "id_access_group = '" + idAccessGroup + "', " +
                "password = '" + password + "'");
        }

        public UserColection[] GetAllUsers()
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * FROM tb_users");
            UserColection[] userColections = new UserColection[rows.GetLength(0)];

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                userColections[i].name = rows[i]["name"];
                userColections[i].id = Convert.ToInt32(rows[i]["id"]);
                userColections[i].password = rows[i]["password"];
                userColections[i].idAccessGroup = Convert.ToInt32(rows[i]["id_access_group"]);
            }

            return userColections;
        }

        /// <summary>
        /// Форма входа пользователя
        /// </summary>
        /// <returns>
        /// 0 - не правильные данные
        /// 1 - данные верны
        /// 2 - операция отменена
        /// </returns>
        public int Login()
        {
            if (CheckLoginCesh())
            {
                return 1;
            }

            FormUserLogin formUserLogin = new FormUserLogin();
            DialogResult dialogResult = formUserLogin.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return 2;
            }

            string name = formUserLogin.textBoxLogin.Text;
            string password = formUserLogin.textBoxPass.Text;

            int res = classMysql.getNumRows("" +
                "SELECT id " +
                "FROM tb_users " +
                "WHERE " +
                "name = '" + name + "' AND " +
                "password = '" + password + "'");

            if (res == 1)
            {
                ceshLogin();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void logOut()
        {
            File.Delete(programPath + tmpFile);
        }

        public void SendToEdit()
        {
            ListView.SelectedListViewItemCollection selectedListViewItemCollection = formUsers.listView1.SelectedItems;
            ListViewItem listViewItem = null;
            foreach (ListViewItem listViewItem2 in selectedListViewItemCollection)
            {
                listViewItem = listViewItem2;
            }

            if (listViewItem == null)
            {
                Console.WriteLine("ERR: listViewItem is null!");
                return;
            }

            formUsers.textBoxIdUser.Text = listViewItem.SubItems["id"].Text;
            formUsers.textBoxLogin.Text = listViewItem.SubItems["name"].Text;
            for (int i = 0; i < formUsers.listBoxAccessGroup.Items.Count; i++)
            {
                if (formUsers.listBoxAccessGroup.Items[i].ToString() == listViewItem.SubItems["idAccessGroup"].Text)
                {
                    formUsers.listBoxAccessGroup.SetSelected(i, true);
                }
            }
            formUsers.textBoxPassword.Text = listViewItem.SubItems["password"].Text;
        }

        public DialogResult showDialog()
        {
            Init();
            return formUsers.ShowDialog();
        }

        private void ceshLogin()
        {
            Directory.CreateDirectory(programPath);
            string text = dateLogin();
            File.WriteAllText(programPath + "\\" + tmpFile, text);
        }

        private int ChangeAccessGroupById(int id, int idAccessGroup)
        {
            return classMysql.UpdateOrDelete(
                "UPDATE FROM tb_users " +
                "SET " +
                "id_access_group = '" + idAccessGroup + "' " +
                "WHERE " +
                "id = '" + id + "'");
        }

        private bool CheckLoginCesh()
        {
            if (!File.Exists(programPath + tmpFile))
            {
                return false;
            }

            string text = File.ReadAllText(programPath + tmpFile);
            if (!text.Equals(dateLogin()))
            {
                return false;
            }

            return true;
        }

        private string dateLogin()
        {
            return (char)DateTime.Now.Year + "." + (char)DateTime.Now.Month + "." + (char)DateTime.Now.Day + "." + (char)DateTime.Now.Hour;
        }

        private void Init()
        {
            formUsers.listBoxAccessGroup.Items.Clear();
            ClassAccessGroup.AccessGroup[] accessGroups = classAccessGroup.getInfo();
            foreach (ClassAccessGroup.AccessGroup accessGroup in accessGroups)
            {
                formUsers.listBoxAccessGroup.Items.Add(accessGroup.name);
            }
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            formUsers.listView1.LargeImageList = formCompanents.imageListColor;
            formUsers.listView1.SmallImageList = formCompanents.imageListColor;
            formUsers.listView1.BeginUpdate();
            formUsers.listView1.View = View.Details;
            formUsers.listView1.FullRowSelect = true;
            formUsers.listView1.MultiSelect = false;
            formUsers.listView1.Columns.Clear();
            formUsers.listView1.Columns.Add("");
            formUsers.listView1.Columns.Add("id");
            formUsers.listView1.Columns.Add("Имя");
            formUsers.listView1.Columns.Add("Тип доступа");
            formUsers.listView1.Columns.Add("Пароль");
            ClassUsers classUsers = new ClassUsers();
            UserColection[] userColections = classUsers.GetAllUsers();
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
                listViewItem.SubItems.Add(userColection.name).Name = "name";
                listViewItem.SubItems.Add(accessGroupName).Name = "idAccessGroup";
                listViewItem.SubItems.Add(userColection.password).Name = "password";
                formUsers.listView1.Items.Add(listViewItem);
            }
            formUsers.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            formUsers.listView1.EndUpdate();
            formUsers.listView1.Update();
        }

        public struct UserColection
        {
            public int id;
            public int idAccessGroup;
            public string name;
            public string password;
        }
    }
}