using MySql.Data.MySqlClient;

namespace VitDBConnect
{
    public class ClassDBConnect
    {
        public MySqlConnection dbLink;

        public ClassDBConnect()
        {
            var settings =  VitSettings.Properties.SettingsDataBase.Default;
            string dbUser = "" +
                "server=" + settings.host + ";" +
                "user=" + settings.userLogin + ";" +
                "database=" + settings.dataName + ";" +
                "password=" + settings.userPassword + ";" +
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