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

        public void Delete(int idFolder)
        {
            string query = "" +
                "DELETE FROM tb_relation_folders " +
                "WHERE id_folder = " + idFolder;
            classMysql.UpdateOrDelete(query);
        }

        /// <summary>
        /// Выдает номер отношения сущности папки с другой папке.
        /// </summary>
        /// <param name="idFolder">Номер папки отношение которой нужно вывести</param>
        /// <returns></returns>
        public int GetIdParentByIdFolder(int idFolder)
        {
            string query = "" +
                "SELECT id_folder_parent " +
                "FROM tb_relation_folders " +
                "WHERE " +
                "id_folder = " + idFolder;
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);
            if (rows.GetLength(0) == 0)
            {
                return 0;
            }

            return Convert.ToInt32(rows[0]["id_folder_parent"]);
        }
    }
}