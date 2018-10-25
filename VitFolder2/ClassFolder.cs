using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly MySqlConnection dbLink;
        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassMysql classMysql = new ClassMysql();
        private ClassRelationFolders classRelation = new ClassRelationFolders();

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
            string pathParent = getPathById(indexParent);
            if (Directory.Exists(pathParent + "\\" + folderName))
            {
                MessageBox.Show("Директория с таким именем уже существует!");
                return 0;
            }

            Directory.CreateDirectory(pathParent + "\\" + folderName);

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

        /// <summary>
        /// Выодит все папки как массив <see cref="FolderCollection"/>
        /// </summary>
        /// /// <param name="pathToFolder">Включает или отключает информацию о пути к папке</param>
        /// <returns><see cref="FolderCollection"/></returns>
        public FolderCollection[] GetAllFolders(bool pathToFolder)
        {
            // считаем количество папок
            int rowCount = classMysql.getNumRows("SELECT id FROM tb_folders");

            // Создаем массив коллекций нужного размера
            FolderCollection[] foldersCollection = new FolderCollection[rowCount];

            // Получаем все папки из базы
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("SELECT id FROM tb_folders");

            // добавляем папки из базы в массив коллекций
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                foldersCollection[i] = GetFolderById(Convert.ToInt32(rows[i]["id"]), pathToFolder);
            }
            return foldersCollection;
        }

        /// <summary>
        /// Получает коллекцию свойств папки
        /// </summary>
        /// <param name="id">Номер папки</param>
        /// <param name="pathToFolder">Включает или выключает добавление пути в коллекцию</param>
        /// <returns></returns>
        public FolderCollection GetFolderById(int id, bool pathToFolder)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_folders " +
                "WHERE id = " + id);

            FolderCollection foldersCollection = new FolderCollection();
            string repositoryPath = VitSettings.Properties.GeneralsSettings.Default.repositiryPayh;
            string path = "";
            if (pathToFolder == true)
            {
                path = repositoryPath + @"\r\" + getPathById(Convert.ToInt32(rows[0]["id"]));
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foldersCollection.path = path;
                foldersCollection.createDateTime = directoryInfo.CreationTime;
            }

            foldersCollection.id = Convert.ToInt32(rows[0]["id"]);
            foldersCollection.name = rows[0]["name"];
            foldersCollection.parentId = classRelation.GetIdParentByIdFolder(id);

            return foldersCollection;
        }

        /// <summary>
        /// Изменяет родительскую папку
        /// </summary>
        /// <param name="id">Номер перемещаемой папки</param>
        /// <param name="idParent">Номер папеи в которую перемещаем</param>
        /// <returns>Количество обновленных строк</returns>
        public void MoveFolder(int id, int idParent)
        {
            if (id == 0)
            {
                return;
            }

            if (idParent == 0)
            {
                classRelation.Delete(id);
                return;
            }

            string query = "" +
                "UPDATE tb_relation_folders " +
                "SET " +
                "`id_folder_parent` = " + idParent + " " +
                "WHERE " +
                "`id_folder` = " + id + "";
            classMysql.UpdateOrDelete(query);
            if (idParent > 0)
            {
                classRelation.Delete(id);
                classRelation.Creat(idParent, id);
            }

            return;
        }

        /// <summary>
        /// Переименовывает папку по ее id
        /// </summary>
        /// <param name="id">Номер изменяемой папки</param>
        /// <param name="name">Новое имя папки</param>
        /// <returns>Количество обновленных строк</returns>
        public void RenameFolder(int id, string name)
        {
            string pathDirectory = getPathById(id);
            FolderCollection folderCollection = GetFolderById(id, false);
            string newPath = "";
            if (Directory.Exists(pathDirectory))
            {
                DirectoryInfo directoryInfo = Directory.GetParent(pathDirectory);
                newPath = directoryInfo.FullName + "\\" + name;
                Directory.Move(pathDirectory, newPath);
            }
            else
            {
                string pathParent = getPathById(folderCollection.parentId);
                newPath = pathParent + "\\" + name;
                Directory.CreateDirectory(newPath);
            }

            if (!Directory.Exists(newPath))
            {
                MessageBox.Show("Не удалось переименовать папку!");
                return;
            }

            if (id == 0)
            {
                return;
            }

            string query = "" +
                "UPDATE tb_folders " +
                "SET " +
                "name = '" + name + "' " +
                "WHERE " +
                "id = '" + id + "'";
            classMysql.UpdateOrDelete(query);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string getPathById(int id)
        {
            // запрашиваем информацию о папке
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("SELECT * FROM tb_folders WHERE id = " + id);
            if (rows.GetLength(0) < 1)
            {
                return "";
            }
            // получаем имя папки
            string path = rows[0]["name"];

            // запрашиваем id родительской папки
            int parentId = classRelation.GetIdParentByIdFolder(id);
            // если id родителя не получили, то прерываем рекурсию
            if (parentId == 0)
            {
                return path;
            }

            //
            path = getPathById(parentId) + "\\" + path;
            return path;
        }

        /// <summary>
        /// Включает в себя основные свойства папки
        /// </summary>
        public struct FolderCollection
        {
            public DateTime createDateTime;

            /// <summary>
            /// номер самой папки
            /// </summary>
            public int id;

            /// <summary>
            /// Имя папки
            /// </summary>
            public string name;

            /// <summary>
            /// Номер родительской папки
            /// </summary>
            public int parentId;

            /// <summary>
            /// disk://directory
            /// </summary>
            public string path;
        }
    }
}