using System;
using System.Collections.Generic;
using VitMysql;

namespace VitFolderAccess
{
    public class ClassFolderAccess
    {
        private ClassMysql classMysql = new ClassMysql();

        public FolderAccess[] getIdUserByIdAccessGroup(int idAccessGroup)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELSECT id_user " +
                "FROM tb_folder_access " +
                "WHERE " +
                "id_access_group = '" + idAccessGroup + "'");
            FolderAccess[] folderAccesses = new FolderAccess[rows.GetLength(0)];

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                folderAccesses[i].id = Convert.ToInt32(rows[i]["id"]);
                folderAccesses[i].idAcceddGroup = Convert.ToInt32(rows[i]["id_access_group"]);
                folderAccesses[i].idFolder = Convert.ToInt32(rows[i]["id_folder"]);
                folderAccesses[i].read = Convert.ToBoolean(rows[i]["read"]);
                folderAccesses[i].write = Convert.ToBoolean(rows[i]["write"]);
            }

            return folderAccesses;
        }

        public FolderAccess[] getInfo()
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELSECT ^ " +
                "FROM tb_folder_access");
            FolderAccess[] folderAccesses = new FolderAccess[rows.GetLength(0)];

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                folderAccesses[i].id = Convert.ToInt32(rows[i]["id"]);
                folderAccesses[i].idAcceddGroup = Convert.ToInt32(rows[i]["id_access_group"]);
                folderAccesses[i].idFolder = Convert.ToInt32(rows[i]["id_folder"]);
                folderAccesses[i].read = Convert.ToBoolean(rows[i]["read"]);
                folderAccesses[i].write = Convert.ToBoolean(rows[i]["write"]);
            }

            return folderAccesses;
        }

        public struct FolderAccess
        {
            public int id;
            public int idAcceddGroup;
            public int idFolder;
            public bool read;
            public bool write;
        }
    }
}