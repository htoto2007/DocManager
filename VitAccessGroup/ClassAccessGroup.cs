using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using VitFolderAccess;
using VitMysql;

namespace VitAccessGroup
{
    public class ClassAccessGroup
    {
        public int id = 0;
        public string name = "";
        private ClassMysql classMysql = new ClassMysql();

        public int getIdByName(string name)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT id " +
                "FROM tb_access_group " +
                "WHERE name = '" + MySqlHelper.EscapeString(name) + "'");

            if (rows.GetLength(0) < 1)
            {
                return 0;
            }

            return Convert.ToInt32(rows[0]["id"]);
        }

        public AccessGroupCollection[] getInfo()
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("SELECT id FROM tb_access_group");
            AccessGroupCollection[] accessGroupCollections = new AccessGroupCollection[rows.GetLength(0)];
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                accessGroupCollections[i] = getInfoById(Convert.ToInt32(rows[i]["id"]));
            }
            return accessGroupCollections;
        }

        public AccessGroupCollection getInfoById(int id)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("SELECT * FROM tb_access_group WHERE id = " + id);
            AccessGroupCollection accessGroupCollection = new AccessGroupCollection
            {
                name = rows[0]["name"],
                id = Convert.ToInt32(rows[0]["id"]),
                rank = Convert.ToInt32(rows[0]["rank"])
            };
            return accessGroupCollection;
        }

        public string getNameById(int id)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT name " +
                "FROM tb_access_group " +
                "WHERE id = '" + id + "'");
            return rows[0]["name"];
        }

        public void initFormAccessGroup()
        {
            FormAccessGroup formAccessGroup = new FormAccessGroup();
            AccessGroupCollection[] accessGroups = getInfo();
            ClassFolderAccess classFolderAccess = new ClassFolderAccess();
            classFolderAccess.getInfo();

            formAccessGroup.listView1.View = System.Windows.Forms.View.Details;
            formAccessGroup.listView1.MultiSelect = true;
            formAccessGroup.listView1.FullRowSelect = true;
            formAccessGroup.listView1.Columns.Clear();
            formAccessGroup.listView1.Columns.Add("name", 200, System.Windows.Forms.HorizontalAlignment.Center);
            formAccessGroup.listView1.Columns.Add("Количество пользователей", 200, System.Windows.Forms.HorizontalAlignment.Center);
        }

        public struct AccessGroupCollection
        {
            public int id;
            public string name;
            public int rank;
        }

        public struct Ranks
        {
            public const int ADMIN = 1;
            public const int USER = 0;
        }
    }
}