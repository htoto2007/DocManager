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
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("SELECT id FROM tb_access_group WHERE name='" + name + "'");
            return Convert.ToInt32(rows[0]["id"]);
        }

        public string getNameById(int id)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("SELECT name FROM tb_access_group WHERE id='" + id + "'");
            return rows[0]["name"];
        }

        public AccessGroup[] getInfo()
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("SELECT * FROM tb_access_group");
            AccessGroup[] accessGroups = new AccessGroup[rows.GetLength(0)];
            int iterator = 0;
            foreach (Dictionary<string, string> row in rows)
            {
                accessGroups[iterator].name = row["name"];
                accessGroups[iterator].id = Convert.ToInt32(row["id"]);
                iterator++;
            }
            return accessGroups;
        }

        public void initFormAccessGroup()
        {
            FormAccessGroup formAccessGroup = new FormAccessGroup();
            AccessGroup[] accessGroups = getInfo();
            ClassFolderAccess classFolderAccess = new ClassFolderAccess();
            classFolderAccess.getInfo();

            formAccessGroup.listView1.View = System.Windows.Forms.View.Details;
            formAccessGroup.listView1.MultiSelect = true;
            formAccessGroup.listView1.FullRowSelect = true;
            formAccessGroup.listView1.Columns.Clear();
            formAccessGroup.listView1.Columns.Add("name", 200, System.Windows.Forms.HorizontalAlignment.Center);
            formAccessGroup.listView1.Columns.Add("Количество пользователей", 200, System.Windows.Forms.HorizontalAlignment.Center);
        }

        public struct AccessGroup
        {
            public int id;
            public string name;
        }
    }
}