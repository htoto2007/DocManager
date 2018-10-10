using System;
using System.Collections.Generic;
using VitMysql;

namespace VitRelationFolders
{
    public class ClassRelationFolders
    {
        private ClassMysql classMysql = new ClassMysql();

        /// <summary>
        /// Добавляет новое риершение папок в базу
        /// </summary>
        /// <param name="indexParent">Индекс папки привязки</param>
        /// <param name="idFolder">Индекс привязываемой папки</param>
        /// <returns></returns>
        public int Creat(int indexParent, int idFolder)
        {
            string query = "" +
                "INSERT INTO tb_relation_folders " +
                "SET " +
                "id_folder_parent = " + indexParent + ", " +
                "id_folder = " + idFolder;
            return classMysql.Insert(query);
        }

        public int Delete(int idFolder)
        {
            string query = "" +
                "DELETE FROM tb_relation_folders " +
                "WHERE id_folder = " + idFolder;
            return classMysql.UpdateOrDelete(query);
        }

        /// <summary>
        /// Выдает номер отношения сущности папки с другой папке.
        /// </summary>
        /// <param name="idFolder">Номер папки отношение которой нужно вывести</param>
        /// <returns></returns>
        public int GetIdParentByIdFolder(int idFolder)
        {
            int id = 0;
            string query = "" +
                "SELECT * FROM tb_relation_folders " +
                "WHERE " +
                "id_folder = " + idFolder;
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);
            foreach (Dictionary<string, string> row in rows)
            {
                id = Convert.ToInt32(row["id_folder_parent"]);
            }

            return id;
        }
    }
}