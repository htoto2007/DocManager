using System;
using System.Collections.Generic;
using VitMysql;

namespace VitUserPositions
{
    public class ClassUserPositions
    {
        private ClassMysql classMysql = new ClassMysql();

        public positionCollection getInfoById(int id)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_user_positions " +
                "WHERE id = " + id);

            positionCollection positionCollection = new positionCollection
            {
                id = 0,
                name = ""
            };

            if (rows.GetLength(0) < 1)
            {
                return positionCollection;
            }

            positionCollection.id = Convert.ToInt32(rows[0]["id"]);
            positionCollection.name = rows[0]["name"];
            return positionCollection;
        }

        public positionCollection getInfoByName(string name)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_user_positions " +
                "WHERE name = '" + name + "'");

            positionCollection positionCollection = new positionCollection
            {
                id = 0,
                name = ""
            };

            if (rows.GetLength(0) < 1)
            {
                return positionCollection;
            }

            positionCollection.id = Convert.ToInt32(rows[0]["id"]);
            positionCollection.name = rows[0]["name"];
            return positionCollection;
        }

        public positionCollection[] getAllInfo()
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_user_positions ");

            positionCollection[] positionCollections = new positionCollection[rows.GetLength(0)];

            if (rows.GetLength(0) < 1)
            {
                return null;
            }

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                positionCollections[i].id = Convert.ToInt32(rows[i]["id"]);
                positionCollections[i].name = rows[i]["name"];
            }
            return positionCollections;
        }

        public struct positionCollection
        {
            public int id;
            public string name;
        }
    }
}