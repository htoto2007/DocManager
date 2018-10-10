using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VitDBConnect;
using VitMysql;
using VitSettings;
using VitTextStringMask;
using VitTypeCard;

namespace VitFiles
{
    public class ClassFiles
    {
        private readonly ClassDBConnect classDB = new ClassDBConnect();

        private readonly ClassSettings classSettings = new ClassSettings();

        private readonly ClassTextStringMask classTextStringMask = new ClassTextStringMask();

        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();

        private ClassMysql classMysql = new ClassMysql();

        public int Copy(int idFile, int idNewFolder, string newPath)
        {
            FileCollection fileCollection = GetFileById(idFile);
            return Create(idNewFolder, fileCollection.idTypeCard, fileCollection.name, fileCollection.path, newPath);
        }

        /// <summary>
        /// Создает информацию о файле и копирует файл в хранилище
        /// </summary>
        /// <param name="idFolder">номер папки к которой будет привязан файл</param>
        /// <param name="fileName">Имя файла</param>
        /// <param name="fullFilePath">Путь к копируемому файлу</param>
        /// <param name="pathSave">Путь сохранения файла</param>
        /// <returns>Выводит последгтй вставленый индекс</returns>
        public int Create(int idFolder, int idTypeCard, string fileName, string fullFilePath, string pathSave)
        {
            if (upload(fullFilePath, pathSave, fileName) == false)
            {
                MessageBox.Show("Файл не загружен!");
                return 0;
            }
            string sqlFileName = ("upload\\" + pathSave + "\\" + fileName).Replace("\\\\", "\\");
            sqlFileName = sqlFileName.Replace("\\", "%slash%");
            string query = "INSERT INTO tb_files " +
                    "SET " +
                    "name = '" + sqlFileName + "', " +
                    "id_folder_file = " + idFolder + ", " +
                    "id_type_card = " + idTypeCard;
            int id = classMysql.Insert(query);
            return id;
        }

        /// <summary>
        /// Удаляет файл из базы и с диска
        /// </summary>
        /// <param name="idFile">Номен файла</param>
        public void Delete(int idFile)
        {
            string filePath = GetFileById(idFile).path;
            string fileName = GetFileById(idFile).name;

            try { File.Delete(@filePath); }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            string query = "DELETE " +
                "FROM tb_files " +
                "WHERE id = " + idFile;
            classMysql.UpdateOrDelete(query);
        }

        /// <summary>
        /// Выдает коллекцию сведений о файле по его номеру
        /// </summary>
        /// <param name="id">номер файла</param>
        /// <returns></returns>
        public FileCollection GetFileById(int id)
        {
            string query = "" +
                "SELECT * " +
                "FROM tb_files " +
                "WHERE " +
                "id = '" + id + "'";
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);

            return toCanonicalFormat(rows[0]);
        }

        /// <summary>
        /// Приобразовывает асоциативный массив к структуре и даные к каноническомуму виду
        /// </summary>
        /// <param name="info">Обычно это строка ответа из mysql</param>
        /// <returns></returns>
        private FileCollection toCanonicalFormat(Dictionary<string, string> info)
        {
            FileCollection fileCollection = new FileCollection
            {
                id = Convert.ToInt32(info["id"]),
                idFolder = Convert.ToInt32(info["id_folder_file"]),
                name = Path.GetFileName(info["name"].Replace("%slash%", "\\")),
                path = Path.GetFullPath(info["name"].Replace("%slash%", "\\")),
                idTypeCard = Convert.ToInt32(info["id_type_card"]),
                pathWithoutFileName = VitSettings.Properties.GeneralsSettings.Default.programPath + "\\" + Path.GetDirectoryName(info["name"].Replace("%slash%", "\\"))
            };

            return fileCollection;
        }

        public FileCollection getFileByName(string fileName)
        {
            fileName = fileName.Replace(@"\", "%slash%");
            string query = "" +
               "SELECT * " +
               "FROM tb_files " +
               "WHERE " +
               "'" + fileName + "' IN(name)";

            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);

            FileCollection fileCollection = new FileCollection
            {
                id = 0,
                idFolder = 0,
                idTypeCard = 0,
                name = "",
                path = ""
            };

            if (rows.GetLength(0) < 1)
            {
                return fileCollection;
            }

            fileCollection.id = Convert.ToInt32(rows[0]["id"]);
            fileCollection.idFolder = Convert.ToInt32(rows[0]["id_folder_file"]);
            fileCollection.name = Path.GetFileName(rows[0]["name"].Replace("%slash%", "\\"));
            fileCollection.path = Path.GetFullPath(rows[0]["name"].Replace("%slash%", "\\"));
            fileCollection.idTypeCard = Convert.ToInt32(rows[0]["id_type_card"]);
            fileCollection.pathWithoutFileName = Path.GetDirectoryName(rows[0]["name"].Replace("%slash%", "\\"));
            return fileCollection;
        }

        public FileCollection getFileByNameNonCard(string fileName)
        {
            fileName = fileName.Replace(@"\", "%slash%");
            int idTypeCard = classTypeCard.getIdByName("Пустая");
            string query = "" +
               "SELECT * " +
               "FROM tb_files " +
               "WHERE " +
               "'" + fileName + "' IN(name), " +
               "id_type_card = '" + idTypeCard + "'";
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);

            FileCollection fileCollection = new FileCollection();

            if (rows.GetLength(0) < 1)
            {
                fileCollection.id = 0;
                fileCollection.idFolder = 0;
                fileCollection.idTypeCard = 0;
                fileCollection.name = "";
                fileCollection.path = "";
                fileCollection.pathWithoutFileName = "";
                classDB.dbLink.Close();
                return fileCollection;
            }

            fileCollection.id = Convert.ToInt32(rows[0]["id"]);
            fileCollection.idFolder = Convert.ToInt32(rows[0]["id_folder_file"]);
            fileCollection.name = Path.GetFileName(rows[0]["name"].Replace("%slash%", "\\"));
            fileCollection.path = Path.GetFullPath(rows[0]["name"].Replace("%slash%", "\\"));
            fileCollection.idTypeCard = Convert.ToInt32(rows[0]["id_type_card"]);
            fileCollection.pathWithoutFileName = Path.GetDirectoryName(rows[0]["name"].Replace("%slash%", "\\"));

            return fileCollection;
        }

        public void Move(int idFile, int idFolder, string newPath)
        {
            FileCollection fileCollection = GetFileById(idFile);
            string name = (@"upload\" + newPath + "\\" + fileCollection.name).Replace("\\", "\\\\");
            string sqlFileName = name.Replace("\\", "%slash%");
            classMysql.Insert("" +
                "UPDATE tb_files " +
                "SET " +
                "id_folder_file = '" + idFolder + "'," +
                "name = '" + sqlFileName + "' " +
                "WHERE id = '" + idFile + "'");
            File.Move(fileCollection.path, classSettings.GetProperties().generalsSttings.programPath + "\\" + name);
        }

        public void rename(int idFile, string newName)
        {
            FileCollection fileCollection = GetFileById(idFile);
            Console.WriteLine(fileCollection.path);
            return;

            File.Move(fileCollection.path, Path.GetDirectoryName(fileCollection.path) + "\\" + newName);
            string name = Path.GetDirectoryName(fileCollection.path) + "\\";
            string sqlFileName = name.Replace("\\", "%slash%");
            classMysql.Insert("" +
                "UPDATE tb_files " +
                "SET " +
                "name = '" + sqlFileName + "' " +
                "WHERE id = '" + idFile + "'");
            //fileCollection = GetFileById(idFile);
            //MessageBox.Show(classSettings.GetProperties().generalsSttings.programPath + "\\" + name);
            File.Move(fileCollection.path, classSettings.GetProperties().generalsSttings.programPath + "\\" + name);
        }

        public FileCollection[] selectAllFiles()
        {
            string query = "" +
                "SELECT * " +
                "FROM tb_files ";
            int rowCount = classMysql.getNumRows(query);
            FileCollection[] fileCollections = new FileCollection[rowCount];
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                fileCollections[i].id = Convert.ToInt32(rows[i]["id"]);
                fileCollections[i].idFolder = Convert.ToInt32(rows[i]["id_folder_file"]);
                fileCollections[i].name = Path.GetFileName(rows[i]["name"].Replace("%slash%", "\\"));
                fileCollections[i].path = Path.GetFullPath(rows[i]["name"].Replace("%slash%", "\\"));
                fileCollections[i].pathWithoutFileName = Path.GetDirectoryName(rows[i]["name"].Replace("%slash%", "\\"));
                rowCount++;
            }
            return fileCollections;
        }

        private bool upload(string pathUpload, string pathSave, string newFilename)
        {
            string currentPathProgram = classSettings.GetProperties().generalsSttings.programPath;
            string finalPathSave = currentPathProgram + "\\upload\\" + pathSave;
            string destFileName = finalPathSave + "\\" + newFilename;

            if (Directory.Exists(finalPathSave) != true)
            {
                Directory.CreateDirectory(finalPathSave);
            }

            try
            {
                File.Copy(pathUpload, destFileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            if (File.Exists(destFileName) == false)
            {
                return false;
            }

            return true;
        }

        public struct FileCollection
        {
            public int id;
            public int idFolder;
            public int idTypeCard;

            /// <summary>
            /// fileName.ext
            /// </summary>
            public string name;

            /// <summary>
            /// disk://directory/fileName.ext
            /// </summary>
            public string path;

            /// <summary>
            /// disk://directory/
            /// </summary>
            public string pathWithoutFileName;
        }
    }
}