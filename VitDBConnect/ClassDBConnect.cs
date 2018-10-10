using MySql.Data.MySqlClient;

namespace VitDBConnect
{
    public class ClassDBConnect
    {
        public MySqlConnection dbLink;

        public ClassDBConnect()
        {
            string dbUser = "" +
                "server=" + Properties.Settings1.Default.server + ";" +
                "user=" + Properties.Settings1.Default.login + ";" +
                "database=" + Properties.Settings1.Default.dbName + ";" +
                "password=" + Properties.Settings1.Default.pass + ";" +
                "SslMode=none;" +
                "charset=utf8";
            dbLink = new MySqlConnection(dbUser);
        }

        public MySqlConnection link()
        {
            return dbLink;
        }
    }
}