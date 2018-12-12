using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VitDBConnect;
using VitNotifyMessage;
using VitSettings;

namespace VitMysql
{
    public class ClassMysql
    {
        private static readonly ClassSettings classSettings = new ClassSettings();
        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();

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
                ShowDialogMysqlSettingsAndRestart();
                return null;
            }

            MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
            MySqlDataReader mySqlDataReader = command.ExecuteReader();

            numRows = 0;
            while (mySqlDataReader.Read())
            {
                rows[numRows] = new Dictionary<string, string>();
                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    rows[numRows].Add(mySqlDataReader.GetName(i), mySqlDataReader.GetString(i));
                }
                numRows++;
            }
            classDB.dbLink.Close();
            return rows;
        }

        /// <summary>
        /// Получает количество строк по запросу
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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
                ShowDialogMysqlSettingsAndRestart();
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
            try
            {
                MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                int lastId = Convert.ToInt32(command.LastInsertedId);
                classDB.dbLink.Close();
                return lastId;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, e.Message + "\n\n" + query);
                return 0;
            }
        }

        /// <summary>
        /// Отправляет mysql запрос и по окончанию запроса выдает количество строк
        /// </summary>
        /// <param name="query">Mysql запрос</param>
        /// <returns>Количество измененных строк</returns>
        public void UpdateOrDelete(string query)
        {
            classDB.dbLink.Open();
            MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                //MessageBox.Show(e.Message + "\n\n\n " + query);
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, e.Message + "\n\n" + query);
                ShowDialogMysqlSettingsAndRestart();
            }
            classDB.dbLink.Close();
        }

        private static void startServer()
        {
            //Process.Start(classSettings.GetProperties;
        }

        /// <summary>
        /// Вызывает диалоговое окно настроек mysql и после перезагружает программу для переинициализации
        /// </summary>
        private void ShowDialogMysqlSettingsAndRestart()
        {
            FormDBConnect formDBConnect = new FormDBConnect();
            formDBConnect.ShowDialog();
            Application.Restart();
            Environment.Exit(0);
        }
    }
}