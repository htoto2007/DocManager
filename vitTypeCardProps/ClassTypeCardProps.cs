using MySql.Data.MySqlClient;
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
            classDB.dbLink.Open();
            MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            int id = Convert.ToInt32(command.LastInsertedId);
            classDB.dbLink.Close();
            return id;
        }

        public TypeCardProps[] getInfoByIdType(int idType)
        {
            string query = "" +
                "SELECT * " +
                "FROM tb_type_card_props " +
                "WHERE " +
                "id_type = '" + idType + "'";
            int numRows = classMysql.getNumRows(query);
            TypeCardProps[] typeCardProps = new TypeCardProps[numRows];

            classDB.dbLink.Open();
            MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
            MySqlDataReader mysqlDataReaderFiles = command.ExecuteReader();

            numRows = 0;
            while (mysqlDataReaderFiles.Read())
            {
                typeCardProps[numRows].id = Convert.ToInt32(mysqlDataReaderFiles.GetString("id"));
                typeCardProps[numRows].idType = Convert.ToInt32(mysqlDataReaderFiles.GetString("id_type"));
                typeCardProps[numRows].typeValue = Convert.ToInt32(mysqlDataReaderFiles.GetString("type_value"));
                typeCardProps[numRows].name = mysqlDataReaderFiles.GetString("name");
                numRows++;
            }
            classDB.dbLink.Close();
            return typeCardProps;
        }
    }
}