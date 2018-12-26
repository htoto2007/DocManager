using VitMysql;
using VitNotifyMessage;

namespace VitCardPropsValue
{
    public class ClassCardPropsValue
    {
        private ClassMysql classMysql = new ClassMysql();

        public int createValue(int idCardProps, string value, string filePath)
        {
            if(IsMatches(filePath) == true)
            {
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, filePath + " - файл с таким же расположением уже существует!");
                return 0;
            }

            int lastId = classMysql.Insert("" +
                "INSERT tb_card_props_value " +
                "SET " +
                "id_card_prop = '" + idCardProps + "', " +
                "value = '" + value + "', " +
                "file_path = '" + filePath + "'");
            return lastId;
        }

        public bool IsMatches(string filePath)
        {
            var row = classMysql.getArrayByQuery("" +
                "SELECT id " +
                "FROM tb_card_props_value " +
                "WHERE file_path = '" + filePath + "'");
            if (row.GetLength(0) > 0) return true;
            return false;
        }
        
    }
}