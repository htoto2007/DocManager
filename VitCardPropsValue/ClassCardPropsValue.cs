using VitMysql;

namespace VitCardPropsValue
{
    public class ClassCardPropsValue
    {
        private ClassMysql classMysql = new ClassMysql();

        public int createValue(int idCardProps, string value, int idFile)
        {
            int lastId = classMysql.Insert("" +
                "INSERT tb_card_props_value " +
                "SET " +
                "id_card_prop = '" + idCardProps + "', " +
                "value = '" + value + "', " +
                "id_file = '" + idFile + "'");
            return lastId;
        }
    }
}