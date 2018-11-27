using System;
using System.Collections.Generic;
using VitMysql;

namespace VitSubdivision
{
    public class ClassSubdivision
    {
        private ClassMysql classMysql = new ClassMysql();

        public SubdivisionCollection getInfoById(int id)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_subdivision " +
                "WHERE id = " + id);

            SubdivisionCollection subdivisionCollection = new SubdivisionCollection
            {
                id = 0,
                name = ""
            };

            if (rows.GetLength(0) < 1)
            {
                return subdivisionCollection;
            }

            subdivisionCollection.id = Convert.ToInt32(rows[0]["id"]);
            subdivisionCollection.name = rows[0]["name"];
            return subdivisionCollection;
        }

        public SubdivisionCollection getInfoByName(string name)
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_subdivision " +
                "WHERE name = '" + name + "'");

            SubdivisionCollection subdivisionCollection = new SubdivisionCollection
            {
                id = 0,
                name = ""
            };

            if (rows.GetLength(0) < 1)
            {
                return subdivisionCollection;
            }

            subdivisionCollection.id = Convert.ToInt32(rows[0]["id"]);
            subdivisionCollection.name = rows[0]["name"];
            return subdivisionCollection;
        }

        public SubdivisionCollection[] getAllInfo()
        {
            Dictionary<string, string>[] rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_subdivision ");

            SubdivisionCollection[] subdivisionCollections = new SubdivisionCollection[rows.GetLength(0)];

            if (rows.GetLength(0) < 1)
            {
                return null;
            }

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                subdivisionCollections[i].id = Convert.ToInt32(rows[i]["id"]);
                subdivisionCollections[i].name = rows[i]["name"];
            }
            return subdivisionCollections;
        }

        public struct SubdivisionCollection
        {
            public int id;
            public string name;
        }
    }
}