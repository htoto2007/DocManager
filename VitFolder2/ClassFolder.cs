using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VitDBConnect;
using VitMysql;
using VitRelationFolders;

namespace VitFolder
{
    /// <summary>
    /// Представляет основные методы для работы с папками
    /// </summary>
    public class ClassFolder
    {
        private MySqlConnection dbLink;
        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassRelationFolders classRelation = new ClassRelationFolders();
        private ClassMysql classMysql = new ClassMysql();

        /// <summary>
        /// Включает в себя основные свойства папки
        /// </summary>
        public struct FoldersCollection
        {
            /// <summary>
            /// Номер родительской папки
            /// </summary>
            public int parentId;

            /// <summary>
            /// номер самой папки
            /// </summary>
            public int id;

            /// <summary>
            /// Имя папки
            /// </summary>
            public string name;
        }

        /// <summary>
        /// Производит инициализацию параметров класса
        /// </summary>
        public ClassFolder()
        {
            dbLink = classDB.dbLink;
        }

        /// <summary>
        /// Создает новую папку.
        /// </summary>
        /// <param name="indexParent">Номер родительской папки</param>
        /// <param name="folderName">Имя папки</param>
        /// <returns>Выводит число добавленных строк</returns>
        public int CreateFolder(int indexParent, string folderName)
        {
            string query = "INSERT INTO tb_folders " +
                    "SET " +
                    "name = '" + folderName + "'";
            int lastId = classMysql.Insert(query);
            if (indexParent > 0)
            {
                classRelation.Creat(indexParent, lastId);
            }

            return lastId;
        }

        /// <summary>
        /// Переименовывает папку по ее id
        /// </summary>
        /// <param name="id">Номер изменяемой папки</param>
        /// <param name="name">Новое имя папки</param>
        /// <returns>Количество обновленных строк</returns>
        public int RenameFolder(int id, string name)
        {
            if (id == 0)
            {
                return 0;
            }

            string query = "" +
                "UPDATE tb_folders " +
                "SET " +
                "name = '" + name + "' " +
                "WHERE " +
                "id = '" + id + "'";
            return classMysql.UpdateOrDelete(query);
        }

        /// <summary>
        /// Изменяет родительскую папку
        /// </summary>
        /// <param name="id">Номер перемещаемой папки</param>
        /// <param name="idParent">Номер папеи в которую перемещаем</param>
        /// <returns>Количество обновленных строк</returns>
        public int MoveFolder(int id, int idParent)
        {
            if (id == 0)
            {
                return 0;
            }

            if (idParent == 0)
            {
                return classRelation.Delete(id);
            }

            string query = "" +
                "UPDATE tb_relation_folders " +
                "SET " +
                "`id_folder_parent` = " + idParent + " " +
                "WHERE " +
                "`id_folder` = " + id + "";
            int res = classMysql.UpdateOrDelete(query);
            if (res < 1)
            {
                classRelation.Creat(idParent, id);
            }

            return 1;
        }

        /// <summary>
        /// Удаляет папку по ее номеру
        /// </summary>
        /// <param name="index">Номер папки</param>
        public void DeleteFolder(int index)
        {
            string str = "";
            string[] strArray;
            string query = "SELECT id_folder " +
                "FROM tb_relation_folders " +
                "WHERE id_folder_parent = " + index;
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);

            foreach (Dictionary<string, string> row in rows)
            {
                str += " " + row["id_folder"];
            }

            strArray = str.Split(' ');

            query = "DELETE " +
                "FROM tb_folders " +
                "WHERE id = " + index;
            classMysql.UpdateOrDelete(query);

            if (strArray.Length > 1)
            {
                foreach (string strId in strArray)
                {
                    if (strId != "")
                    {
                        DeleteFolder(Convert.ToInt32(strId));
                    }
                }
            }

            return;
        }

        private int MysqlNumRows(string tableName)
        {
            string query = "SELECT COUNT(*) FROM " + tableName;
            MySqlCommand command = new MySqlCommand(query, dbLink);
            try
            {
                dbLink.Open();
            }
            catch (Exception e)
            {
                string currMthod = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string currClass = ToString();
                MessageBox.Show(currClass + "->" + currMthod + " saye: " + e.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            int rowCount = int.Parse(command.ExecuteScalar().ToString());
            dbLink.Close();
            return rowCount;
        }

        /// <summary>
        /// Выодит все папки как массив <see cref="FoldersCollection"/>
        /// </summary>
        /// <returns><see cref="FoldersCollection"/></returns>
        public FoldersCollection[] SelectAllFolders()
        {
            // считаем количество папок
            //int rowCount = MysqlNumRows("tb_folders");
            int rowCount = classMysql.getNumRows("SELECT id FROM tb_folders");

            // Создаем массив коллекций нужного размера
            FoldersCollection[] foldersCollection = new FoldersCollection[rowCount];

            // Получаем все папки из базы
            string query = "SELECT * FROM tb_folders";
            MySqlCommand command = new MySqlCommand(query, dbLink);
            dbLink.Open();
            MySqlDataReader mysqlDataReaderFolders = command.ExecuteReader();

            rowCount = 0;
            // добавляем папки из базы в массив коллекций
            while (mysqlDataReaderFolders.Read())
            {
                foldersCollection[rowCount].id = Convert.ToInt32(mysqlDataReaderFolders.GetString("id"));
                foldersCollection[rowCount].name = mysqlDataReaderFolders.GetString("name");
                rowCount++;
            }
            dbLink.Close();

            rowCount = 0;
            foreach (FoldersCollection folder in foldersCollection)
            {
                foldersCollection[rowCount].parentId = classRelation.GetIdParentByIdFolder(folder.id);
                rowCount++;
            }
            return foldersCollection;
        }
    }
}