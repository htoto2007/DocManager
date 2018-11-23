using MySql.Data.MySqlClient;
using System;
using VitDBConnect;
using VitFTP;
using VitMysql;
using VitNotifyMessage;
using VitSettings;
using VitTextStringMask;
using VitTypeCard;

namespace VitFiles
{
    public class ClassFiles
    {
        private readonly ClassDBConnect classDB = new ClassDBConnect();

        private readonly ClassMysql classMysql = new ClassMysql();
        private readonly ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
        private readonly ClassSettings classSettings = new ClassSettings();

        private readonly ClassTextStringMask classTextStringMask = new ClassTextStringMask();

        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();

        private readonly string repositiryPayh = "";

        private readonly string root = "";

        private ClassFTP classFTP = new ClassFTP();

        public ClassFiles()

        {
            
        }

        public void createFile(string path, string hashCode, int idTypeCard)
        {
            classMysql.Insert("" +
                "insert into tb_tb+files " +
                "SET " +
                "name = '" + MySqlHelper.EscapeString(path) +"', " +
                "hash_code = '" + hashCode + "' " +
                "id_typw_card = '" + idTypeCard + "' ");
        }

        public void createFile(string[] arrPath, string hashCode, int idTypeCard)
        {
            foreach (string path in arrPath)
            {
                classMysql.Insert("" +
                    "INSERT INTO tb_files " +
                    "SET " +
                    "name = '" + MySqlHelper.EscapeString(path) + "', " +
                    "hash_code = '" + hashCode + "' " +
                    "id_typw_card = '" + idTypeCard + "' ");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">z://directory/fileName.ext</param>
        public void getInfoByName(string name)
        {
                classMysql.Insert("" +
                    "SELECT * " +
                    "FROM tb_files " +
                    "WHERE " +
                    "name = '" + MySqlHelper.EscapeString(name) + "'");
            
        }

        public struct WhereParams
        {
            public const string hashCode = "hash_code";
            public const string id = "id";
            public const string idTypeCard = "id_typw_card";
            public const string name = "name";
        }

        public struct FileCollection
        {
            public DateTime createDateTime;
            public int hashCode;
            public int id;
            public int idTypeCard;

            /// <summary>
            /// fileName.ext
            /// </summary>
            public string name;

            /// <summary>
            /// disk://directory/fileName.ext
            /// </summary>
            public string path;

            /// <summary>
            /// disk://directory/
            /// </summary>
            public string pathWithoutFileName;
        }
    }
}