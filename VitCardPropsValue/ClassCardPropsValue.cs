using MySql.Data.MySqlClient;
using System;
using VitMysql;
using VitNotifyMessage;

namespace VitCardPropsValue
{
    public class ClassCardPropsValue
    {
        private ClassMysql classMysql = new ClassMysql();

        /// <summary>
        /// Создает знасения карточки файла
        /// </summary>
        /// <param name="idCardProps"></param>
        /// <param name="value"></param>
        /// <param name="idFile">Номер файла, к которому привязываются свойства карточки</param>
        /// <returns></returns>
        public int createValue(int idCardProps, string value, int idFile)
        {
            int lastId = classMysql.Insert("" +
                "INSERT tb_card_props_value " +
                "SET " +
                "id_card_prop = '" + idCardProps + "', " +
                "value = '" + MySqlHelper.EscapeString(value) + "', " +
                "id_file = '" + idFile + "'");
            return lastId;
        }

        /// <summary>
        /// Копирует значения карточки файла по его пути
        /// </summary>
        /// <param name="souecePathFile">"/directory/fileName.ext"</param>
        /// <param name="targetPathFile">"/directory/fileName.ext"</param>
        public void CopyByIdFile(string souecePathFile, string targetPathFile)
        {
            souecePathFile = souecePathFile.Replace("\\", "/");
            var valuesCard = getByIdFile(souecePathFile);
            if (valuesCard == null) return;
            foreach (var valueCard in valuesCard)
                createValue(valueCard.idCardProp, valueCard.value, targetPathFile);
        }

        /// <summary>
        /// Перемещает значения карточки файла по его пути
        /// </summary>
        /// <param name="souecePathFile">"/directory/fileName.ext"</param>
        /// <param name="targetPathFile">"/directory/fileName.ext"</param>
        public void MoveByFilePath(string souecePathFile, string targetPathFile)
        {
            souecePathFile = souecePathFile.Replace("\\", "/");
            var valuesCard = getByIdFile(souecePathFile);
            if (valuesCard == null) return;
            foreach (var valueCard in valuesCard)
                updateById(valueCard.idCardProp, valueCard.value, targetPathFile, valueCard.id);
        }

        /// <summary>
        /// Выдает все значения карточки по пути файла
        /// </summary>
        /// <param name="idFile">"/directory/fileName.ext"</param>
        /// <returns></returns>
        public CardPropsValueCollection[] getByIdFile(int idFile)
        {
            var rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_card_props_value " +
                "WHERE " +
                "`id_file` = '" + idFile + "' ");
            CardPropsValueCollection[] cardPropsValueCollections = null;
            if (rows.GetLength(0) < 1) return cardPropsValueCollections;
            cardPropsValueCollections = new CardPropsValueCollection[rows.GetLength(0)];

            int iterator = 0;
            foreach (var row in rows)
            {
                cardPropsValueCollections[iterator].idFile = row["id_file"];
                cardPropsValueCollections[iterator].value = row["value"];
                cardPropsValueCollections[iterator].idCardProp = Convert.ToInt32(row["id_card_prop"]);
                cardPropsValueCollections[iterator].id = Convert.ToInt32(row["id"]);
                iterator++;
            }
            return cardPropsValueCollections;
        }

        /// <summary>
        /// Обновляет значение карточки по id файла
        /// </summary>
        /// <param name="idCardProps"></param>
        /// <param name="value"></param>
        /// <param name="filePath">"/directory/fileName.ext"</param>
        /// <param name="id"></param>
        public void updateById(int idCardProps, string value, string filePath, int id)
        {
            int lastId = classMysql.Insert("" +
                "UPDATE tb_card_props_value " +
                "SET " +
                "id_card_prop = '" + idCardProps + "', " +
                "value = '" + MySqlHelper.EscapeString(value) + "', " +
                "file_path = '" + MySqlHelper.EscapeString(filePath) + "' " +
                "WHERE " +
                "id = '" + id + "'");
            return;
        }

        public struct CardPropsValueCollection
        {
            public int idCardProp;
            public string value;
            public int idFile;
            public int id;
        }


    }
}