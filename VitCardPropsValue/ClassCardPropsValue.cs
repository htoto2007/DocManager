using System;
using VitMysql;
using VitNotifyMessage;

namespace VitCardPropsValue
{
    public class ClassCardPropsValue
    {
        private ClassMysql classMysql = new ClassMysql();

        public int createValue(int idCardProps, string value, string filePath)
        {
            int id = IsMatches(filePath, idCardProps);
            if (id != 0)
            {
                //ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                //classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, filePath + " - файл с таким же расположением уже существует!");
                updateById(idCardProps, value, filePath, id);
                return id;
            }

            int lastId = classMysql.Insert("" +
                "INSERT tb_card_props_value " +
                "SET " +
                "id_card_prop = '" + idCardProps + "', " +
                "value = '" + value + "', " +
                "file_path = '" + filePath + "'");
            return lastId;
        }

        public CardPropsValueCollection[] getByFilePath(string filePath)
        {
            var rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_card_props_value " +
                "WHERE " +
                "`file_path` = '" + filePath + "' ");
            CardPropsValueCollection[] cardPropsValueCollections = null;
            if (rows.GetLength(0) < 1) return cardPropsValueCollections;
            cardPropsValueCollections = new CardPropsValueCollection[rows.GetLength(0)];

            int iterator = 0;
            foreach (var row in rows)
            {
                cardPropsValueCollections[iterator].filePath = row["file_path"];
                cardPropsValueCollections[iterator].value = row["value"];
                cardPropsValueCollections[iterator].idCardProp = Convert.ToInt32(row["id_card_prop"]);
                cardPropsValueCollections[iterator].id = Convert.ToInt32(row["id"]);
            }
            return cardPropsValueCollections;
        }

        public void updateById(int idCardProps, string value, string filePath, int id)
        {
            int lastId = classMysql.Insert("" +
                "UPDATE tb_card_props_value " +
                "SET " +
                "id_card_prop = '" + idCardProps + "', " +
                "value = '" + value + "', " +
                "file_path = '" + filePath + "' " +
                "WHERE " +
                "id = '" + id + "'");
            return;
        }

        /// <summary>
        /// Определяет по пути файла и по номеру свойства карточки наличие записи
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="idCardProps"></param>
        /// <returns>0 - если запись не найдена. Все что отлично от нуля, то запись есть.</returns>
        public int IsMatches(string filePath, int idCardProps)
        {
            var rows = classMysql.getArrayByQuery("" +
                "SELECT id " +
                "FROM tb_card_props_value " +
                "WHERE " +
                "file_path = '" + filePath + "' AND " +
                "id_card_prop = '" + idCardProps + "'");
            if (rows.GetLength(0) > 0) return Convert.ToInt32(rows[0]["id"]);
            return 0;
        }

        public struct CardPropsValueCollection
        {
            public int idCardProp;
            public string value;
            public string filePath;
            public int id;
        }


    }
}