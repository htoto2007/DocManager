﻿using MySql.Data.MySqlClient;
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
        //public FormUsers formUsers = new FormUsers();
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

            var id = classMysql.Insert("" +
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

            classMysql.UpdateOrDelete("" +
                "UPDATE tb_users " +
                "SET login = '" + login + "-" + id.ToString() + "' " +
                "WHERE id = " + id);
        }

        public void deleteById(int id)
        {
            classMysql.UpdateOrDelete("" +
                "DELETE FROM tb_users " +
                "WHERE id = " + id);
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

            UserColection userColection = new UserColection();
            //if (rows.GetLength(0) < 1) return userColection;


            userColection.firstName = rows[0]["first_name"];
            userColection.lastName = rows[0]["last_name"];
            userColection.middleName = rows[0]["middle_name"];
            userColection.login = rows[0]["login"];
            userColection.idPosition = Convert.ToInt32(rows[0]["id_position"]);
            userColection.idSubdivision = Convert.ToInt32(rows[0]["id_subdivision"]);
            userColection.id = Convert.ToInt32(rows[0]["id"]);
            userColection.password = rows[0]["password"];
            userColection.imagePath = rows[0]["image"];
            userColection.idAccessGroup = Convert.ToInt32(rows[0]["id_access_group"]);
            

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
            Console.WriteLine("Делаем проверку кеша логина -> ");
            if (CheckLoginCesh())
            {
                Console.Write("вналичии");
                return 1;
            }
            Console.Write("отсутствует");


            FormUserLogin formUserLogin = new FormUserLogin();
            UserColection[] userColections = GetAllUsers();

            foreach (UserColection userColection in userColections)
            {
                formUserLogin.comboBoxLogin.Items.Add(userColection.login);
            }
            Console.WriteLine("Выводим форму пользователя -> ");
            DialogResult dialogResult = formUserLogin.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                Console.Write(" Форма закрыта");
                return 2;
            }
            Console.Write(" форма отправлена на подтверждение.");

            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
            "SELECT id " +
            "FROM tb_users " +
            "WHERE " +
            "login = '" + MySqlHelper.EscapeString(formUserLogin.comboBoxLogin.Text) + "' AND " +
            "password = '" + MySqlHelper.EscapeString(formUserLogin.textBoxPass.Text) + "'");

            Console.WriteLine("Проверяем наличие пользователя согласно данным из формы -> ");
            if (rows.GetLength(0) == 1)
            {
                Console.Write("пользователь найден");
                CashLogin(rows[0]["id"]);
                return 1;
            }
            Console.Write("пользователь не найден!");
            return 0;
        }

        public void logOut()
        {
            File.Delete(cashLoginFile);
        }

        public void SendToEdit()
        {
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
            Console.WriteLine("Проверка файла пользователя " + programPath + "\\" + tmpFile + "-> ");
            if (!File.Exists(programPath + "\\" + tmpFile))
            {
                Console.Write("user file not found " + programPath + "\\" + tmpFile);
                return false;
            }
            Console.Write("Успешно");

            string[] text = File.ReadAllLines(programPath + "\\" + tmpFile);
            Console.WriteLine("Проверка содержимого файла пользователя -> ");
            if (text.GetLength(0) != 3)
            {
                Console.Write("Ошибка! Count " + text.GetLength(0).ToString());
                return false;
            }
            Console.Write("Успешно ");

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