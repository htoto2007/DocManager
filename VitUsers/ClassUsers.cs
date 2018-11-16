using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VitAccessGroup;
using VitMysql;
using VitSettings;
using VitSubdivision;
using VitUserPositions;

namespace VitUsers
{
    public class ClassUsers
    {
        public FormUsers formUsers = new FormUsers();
        public int id = 0;
        public int idAccessGroup = 0;
        public string name = "";
        private readonly string cashLoginFile = "";
        private readonly string programPath = "";
        private readonly string repositiryPayh;
        private readonly string tmpFile = "tm.dm";
        private ClassAccessGroup classAccessGroup = new ClassAccessGroup();
        private ClassMysql classMysql = new ClassMysql();
        private ClassSettings classSettings = new ClassSettings();
        private ClassSubdivision classSubdivision = new ClassSubdivision();
        private ClassUserPositions classUserPositions = new ClassUserPositions();

        public ClassUsers()
        {
            programPath = classSettings.GetProperties().generalsSttings.programPath;
            repositiryPayh = classSettings.GetProperties().generalsSttings.repositiryPayh;
            cashLoginFile = programPath + "\\" + tmpFile;
        }

        public void AddUser(string lastName, string firstName, string MiddleName, string mail, string mailPass, string accessGroup, string position, string subdivision, string login, string password)
        {
            int idAccessGroup = classAccessGroup.getIdByName(accessGroup);
            int idUserPosition = classUserPositions.getInfoByName(position).id;
            int idDivision = classSubdivision.getInfoByName(subdivision).id;

            classMysql.Insert("" +
                "INSERT INTO tb_users " +
                "SET " +
                "first_name = '" + firstName + "', " +
                "last_name = '" + lastName + "', " +
                "middle_name = '" + MiddleName + "', " +
                "login = '" + login + "', " +
                "mail = '" + mail + "', " +
                "mail_password = '" + mailPass + "', " +
                "id_subdivision = '" + idDivision + "', " +
                "id_position = '" + idUserPosition + "', " +
                "id_access_group = '" + idAccessGroup + "', " +
                "password = '" + password + "'");

            Init();
        }

        public bool changePassword(string oldPass, string newPass, string retryPass)
        {
            UserColection userColection = getThisUser();
            if (userColection.password.Equals(oldPass) == false)
            {
                Console.WriteLine("newPass " + userColection.password + " " + newPass);
                return false;
            }

            if (newPass.Equals(retryPass) == false)
            {
                Console.WriteLine("retryPass " + newPass + " " + retryPass);
                return false;
            }

            classMysql.UpdateOrDelete(
                "UPDATE tb_users " +
                "SET " +
                "password = '" + MySqlHelper.EscapeString(newPass) + "' " +
                "WHERE " +
                "id = '" + userColection.id + "'");
            return true;
        }

        public UserColection[] GetAllUsers()
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * FROM tb_users");
            UserColection[] userColections = new UserColection[rows.GetLength(0)];

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                userColections[i] = GetUserByid(Convert.ToInt32(rows[i]["id"]));
            }

            return userColections;
        }

        public UserColection getThisUser()
        {
            UserColection userColection = new UserColection();
            int id = Convert.ToInt32(File.ReadAllLines(cashLoginFile)[1]);
            userColection = GetUserByid(id);
            return userColection;
        }

        public UserColection GetUserByid(int id)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_users " +
                "WHERE id = " + id);
            UserColection userColection = new UserColection
            {
                firstName = rows[0]["first_name"],
                lastName = rows[0]["last_name"],
                middleName = rows[0]["middle_name"],
                login = rows[0]["login"],
                idPosition = Convert.ToInt32(rows[0]["id_position"]),
                idSubdivision = Convert.ToInt32(rows[0]["id_subdivision"]),
                id = Convert.ToInt32(rows[0]["id"]),
                password = rows[0]["password"],
                imagePath = rows[0]["image"],
                idAccessGroup = Convert.ToInt32(rows[0]["id_access_group"])
            };

            return userColection;
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
            UserColection[] userColections = GetAllUsers();

            foreach (UserColection userColection in userColections)
            {
                formUserLogin.comboBoxLogin.Items.Add(userColection.login);
            }
            DialogResult dialogResult = formUserLogin.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return 2;
            }

            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
            "SELECT id " +
            "FROM tb_users " +
            "WHERE " +
            "login = '" + MySqlHelper.EscapeString(formUserLogin.comboBoxLogin.Text) + "' AND " +
            "password = '" + MySqlHelper.EscapeString(formUserLogin.textBoxPass.Text) + "'");

            if (rows.GetLength(0) == 1)
            {
                CashLogin(rows[0]["id"]);
                return 1;
            }
            return 0;
        }

        public void logOut()
        {
            File.Delete(cashLoginFile);
        }

        public void SendToEdit()
        {
        }

        public DialogResult showDialog()
        {
            Init();
            return formUsers.ShowDialog();
        }

        public void updateImage(int id)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "image files(*.jpg) | *.jpg"
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string avatarName = "avatar.jpg";
            string avatarPath = repositiryPayh + "\\" + id.ToString() + "\\" + avatarName;
            if (Directory.Exists(repositiryPayh + "\\" + id.ToString()) == false)
            {
                Directory.CreateDirectory(repositiryPayh + "\\" + id.ToString());
            }

            Image image = Image.FromFile(openFileDialog.FileName);

            if (File.Exists(avatarPath))
            {
                File.Delete(avatarPath);
            }

            image.Save(avatarPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            classMysql.UpdateOrDelete(
                "UPDATE tb_users " +
                "SET " +
                "image = '" + MySqlHelper.EscapeString(avatarPath) + "'" +
                "WHERE " +
                "id = '" + id + "'");
        }

        public void deleteImage(int id)
        {
            string avatarName = "avatar.jpg";
            string avatarPath = repositiryPayh + "\\" + id.ToString() + "\\" + avatarName;
            File.Delete(avatarPath);

            classMysql.UpdateOrDelete(
                "UPDATE tb_users " +
                "SET " +
                "image = ''" +
                "WHERE " +
                "id = '" + id + "'");
        }

        private void CashLogin(string id)
        {
            string text = DateLogin();
            UserColection userColection = GetUserByid(Convert.ToInt32(id));
            File.WriteAllText(cashLoginFile, text.GetHashCode().ToString() + "\n");
            File.AppendAllText(cashLoginFile, id + "\n");
            File.AppendAllText(cashLoginFile, userColection.GetHashCode().ToString() + "\n");
            Console.WriteLine(cashLoginFile);
        }

        private void ChangeAccessGroupById(int id, int idAccessGroup)
        {
            classMysql.UpdateOrDelete(
                "UPDATE FROM tb_users " +
                "SET " +
                "id_access_group = '" + idAccessGroup + "' " +
                "WHERE " +
                "id = '" + id + "'");
        }

        private bool CheckLoginCesh()
        {
            if (!File.Exists(programPath + "\\" + tmpFile))
            {
                Console.WriteLine("user file not found " + programPath + "\\" + tmpFile);
                return false;
            }

            string[] text = File.ReadAllLines(programPath + "\\" + tmpFile);

            if (text.GetLength(0) != 3)
            {
                Console.WriteLine("user count " + text.GetLength(0).ToString());
                return false;
            }

            // проверяем время
            if (!text[0].Equals(DateLogin().GetHashCode().ToString()))
            {
                Console.WriteLine("user date " + text[0] + " " + DateLogin().GetHashCode().ToString());
                return false;
            }

            // проверяем hash
            int id = Convert.ToInt32(text[1]);
            UserColection userColection = new UserColection();
            userColection = GetUserByid(id);
            if (!text[2].Equals(userColection.GetHashCode().ToString()))
            {
                Console.WriteLine("user " + id);
                Console.WriteLine("user hash" + text[2] + " " + userColection.GetHashCode());
                return false;
            }

            return true;
        }

        private string DateLogin()
        {
            string hour = DateTime.Now.Hour.ToString();
            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            return year + "." + month + "." + day + "." + hour;
        }

        private void Init()
        {
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
            formUsers.listView1.Columns.Add("Логин");
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
                listViewItem.SubItems.Add(userColection.firstName + " " + userColection.lastName + " " + userColection.middleName).Name = "name";
                listViewItem.SubItems.Add(userColection.login).Name = "login";
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
            public string firstName;
            public int id;
            public int idAccessGroup;
            public int idPosition;
            public int idSubdivision;
            public string mail;
            public string mailPass;
            public string imagePath;
            public string lastName;
            public string login;
            public string middleName;
            public string password;
        }
    }
}