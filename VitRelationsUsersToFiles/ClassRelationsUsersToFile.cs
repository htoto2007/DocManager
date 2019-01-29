using VitMysql;

namespace VitRelationsUsersToFiles
{
    public class ClassRelationsUsersToFile
    {
        ClassMysql classMysql = new ClassMysql();
        public int add(int idUser, int idFile, string operationName)
        {
            return classMysql.Insert("" +
                "INSERT INTO tb_relations_user_to_file " +
                "SET" +
                "   id_user = '" + idUser + "', " +
                "   id_file = '" + idFile + "', " +
                "   operation_name = '" + operationName + "'");
        }
    }
}
