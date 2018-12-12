using System;
using System.Windows.Forms;
using VitDBConnect;
using VitMysql;

namespace vitTypeCardProps
{
    public class ClassTypeCardProps
    {
        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassMysql classMysql = new ClassMysql();

        public struct TypeCardProps
        {
            public string name;
            public int typeValue;
            public int idType;
            public int id;
        }

        public int add(string name, int typeValue, int idType)
        {
            string query = "" +
                    "INSERT INTO tb_type_card_props " +
                    "SET " +
                    "name = '" + name + "', " +
                    "type_value = '" + typeValue + "', " +
                    "id_type = '" + idType + "'";
            
            return classMysql.Insert(query);
        }

        public TypeCardProps[] getInfoByIdType(int idType)
        {
            string query = "" +
                "SELECT * " +
                "FROM tb_type_card_props " +
                "WHERE " +
                "id_type = '" + idType + "'";
            var rows =  classMysql.getArrayByQuery(query);

            int numRows = classMysql.getNumRows(query);
            TypeCardProps[] typeCardProps = new TypeCardProps[rows.GetLength(0)];

            numRows = 0;
            foreach (var row in rows)
            {
                typeCardProps[numRows].id = Convert.ToInt32(row["id"]);
                typeCardProps[numRows].idType = Convert.ToInt32(row["id_type"]);
                typeCardProps[numRows].typeValue = Convert.ToInt32(row["type_value"]);
                typeCardProps[numRows].name = row["name"];
                numRows++;
            }
            return typeCardProps;
        }
    }
}