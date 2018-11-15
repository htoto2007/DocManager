using MySql.Data.MySqlClient;
using VitDBConnect;
using VitFTP;
using VitMysql;
using VitRelationFolders;

namespace VitFolder
{
    /// <summary>
    /// Представляет основные методы для работы с папками
    /// </summary>
    public class ClassFolder
    {
        private readonly ClassMysql classMysql = new ClassMysql();
        private readonly ClassRelationFolders classRelation = new ClassRelationFolders();
        private readonly MySqlConnection dbLink;
        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassFTP classFTP = new ClassFTP();

        /// <summary>
        /// Производит инициализацию параметров класса
        /// </summary>
        public ClassFolder()
        {
            dbLink = classDB.dbLink;
        }

        public void Create(string PathParentolder)
        {
            classFTP.MakeDirectory(PathParentolder);
        }

        public void getDirectoryes()
        {
            classFTP.ListDirectoryDetails();
        }
    }
}