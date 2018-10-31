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

        public struct positionCollection
        {
            public int id;
            public string name;
        }
    }
}