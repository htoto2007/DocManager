using System;
using System.Collections.Generic;
using VitDBConnect;
using VitMysql;
using VitNotifyMessage;

namespace VitTypeCard
{
    /// <summary>
    /// Обеспечивает работу с типами карточек файлов
    /// </summary>
    public class ClassTypeCard
    {
        /// <summary>
        /// Константа определяющая пустую карту
        /// </summary>
        public const string EMPTY_CARD = "Пустая";

        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassMysql classMysql = new ClassMysql();
        private ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();

        

        

        public int add(string name)
        {
            string query = "" +
                    "INSERT INTO tb_type_card " +
                    "SET " +
                    "name = '" + name + "'";
            
            return classMysql.Insert(query);
        }

        public void DeleteById(int id)
        {
            string query = "" +
                    "DELETE " +
                    "FROM tb_type_card " +
                    "WHERE " +
                    "id = '" + id + "'";

            classMysql.UpdateOrDelete(query);
        }

        public TypeCardCollection[] getAllInfo()
        {
            string query = "" +
                    "SELECT * " +
                    "FROM tb_type_card ";

            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);
            int numRows = rows.GetLength(0);

            TypeCardCollection[] typeCardCollections = new TypeCardCollection[numRows];
            int iterator = 0;
            foreach (Dictionary<string, string> row in rows)
            {
                typeCardCollections[iterator].id = Convert.ToInt32(row["id"]);
                typeCardCollections[iterator].namne = row["name"];
                iterator++;
            }

            return typeCardCollections;
        }

        public int getIdByName(string name)
        {
            string query = "" +
                    "SELECT * " +
                    "FROM tb_type_card " +
                    "WHERE " +
                    "name = '" + name + "'";

            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);
            if (rows.GetLength(0) < 1)
            {
                return 0;
            }

            Dictionary<string, string> row = rows[0];
            return int.Parse(row["id"]);
        }

        public struct TypeCardCollection
        {
            public int id;
            public string namne;
        }
    }
}