using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VitDBConnect;
using VitSettings;

namespace VitMysql
{
    public class ClassMysql
    {
        private static readonly ClassSettings classSettings = new ClassSettings();
        private ClassDBConnect classDB = new ClassDBConnect();

        /// <summary>
        /// Выдает результат как асоциативный массив
        /// </summary>
        /// <param name="query">Mysql запрос (SELECT)</param>
        /// <returns></returns>
        public Dictionary<string, string>[] getArrayByQuery(string query)
        {
            int numRows = getNumRows(query);
            MySqlDataReader[] mySqlDataReaders = new MySqlDataReader[numRows];

            Dictionary<string, string>[] rows = new Dictionary<string, string>[numRows];
            try
            {
                classDB.dbLink.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                rows = null;
                return rows;
            }

            MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
            MySqlDataReader mySqlDataReader = command.ExecuteReader();

            numRows = 0;
            while (mySqlDataReader.Read())
            {
                rows[numRows] = new Dictionary<string, string>();
                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    string field = mySqlDataReader.GetName(i);
                    string value = mySqlDataReader.GetString(i);
                    rows[numRows].Add(field, value);
                }
                numRows++;
            }
            classDB.dbLink.Close();
            return rows;
        }

        public int getNumRows(string query)
        {
            int numRows = 0;
            try
            {
                classDB.dbLink.Open();
                MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    numRows++;
                }

                classDB.dbLink.Close();
            }
            catch (Exception e)
            {
                string currMthod = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string currClass = ToString();
                string text = currClass + "->" + currMthod + " saye: " + e.ToString() + "\n\n" + query;
                MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return numRows;
        }

        /// <summary>
        /// Отправляет mysql запрос и по окончанию запроса выдает последний вставленый id
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Последний вставленый индекс</returns>
        public int Insert(string query)
        {
            classDB.dbLink.Open();
            MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
            MySqlDataReader mySqlDataReader = command.ExecuteReader();
            int lastId = Convert.ToInt32(command.LastInsertedId);
            classDB.dbLink.Close();
            return lastId;
        }

        /// <summary>
        /// Отправляет mysql запрос и по окончанию запроса выдает количество строк
        /// </summary>
        /// <param name="query">Mysql запрос</param>
        /// <returns>Количество измененных строк</returns>
        public int UpdateOrDelete(string query)
        {
            classDB.dbLink.Open();
            MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
            int res = 0;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                MessageBox.Show(e.Message + "\n\n\n " + query);
            }
            classDB.dbLink.Close();
            return res;
        }

        private static void startServer()
        {
            //Process.Start(classSettings.GetProperties;
        }
    }
}