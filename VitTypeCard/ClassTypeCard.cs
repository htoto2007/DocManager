using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VitDBConnect;
using VitMysql;
using VitNotifyMessage;

namespace VitTypeCard
{
    public class ClassTypeCard
    {
        /// <summary>
        /// Константа определяющая пустую карту
        /// </summary>
        public const string EMPTY_CARD = "Пустая";

        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassMysql classMysql = new ClassMysql();
        private ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();

        public string[] typeProp = new string[]
        {
            "Строковый",
            "Числовой",
            "Дата",
            "Дата и всремя",
            "Логический",
            "Текстовой"
        };

        public string getNamePropById(int id)
        {
            if(id < 0)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Номер типа значения свойства меньше нуля!");
                return "";
            }
            if (id > typeProp.GetLength(0) - 1)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Номер типа значения свойства выходит за границу списка!");
                return "";
            }

            return typeProp[id];
        }

        /// <summary>
        /// Получает коллекцию типа карточки по ее номеру
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TypeCardCollection getByid(int id)
        {
            string query = "" +
                    "SELECT * " +
                    "FROM tb_type_card " +
                    "WHERE " +
                    "id = '" + id + "'";

            Dictionary<string, string>[] rows = classMysql.getArrayByQuery(query);
            TypeCardCollection typeCardCollection = new TypeCardCollection();
            if (rows.GetLength(0) < 1) return typeCardCollection;
            
            typeCardCollection.id = Convert.ToInt32(rows[0]["id"]);
            typeCardCollection.namne = rows[0]["name"];
            return typeCardCollection;
        }

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