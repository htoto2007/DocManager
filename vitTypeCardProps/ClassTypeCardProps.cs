using System;
using VitDBConnect;
using VitMysql;

namespace vitCardProps
{
    /// <summary>
    /// Обеспечивает инфроструктуру свойств карточек документов
    /// </summary>
    public class ClassCardProps
    {
        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassMysql classMysql = new ClassMysql();

        /// <summary>
        /// Коллекция типов свойств карточек
        /// </summary>
        public struct CardPropsCollection
        {
            /// <summary>
            /// Название свойства (например: год рождения)
            /// </summary>
            public string name;
            /// <summary>
            /// тип значения 
            /// </summary>
            public int typeValue;
            /// <summary>
            /// Номер типа карточки к которой привязано свойство
            /// </summary>
            public int idTypeCard;
            /// <summary>
            /// Номер свойства
            /// </summary>
            public int id;
        }

        /// <summary>
        /// Создает свойство карточки
        /// </summary>
        /// <param name="name">Название свойства</param>
        /// <param name="typeValue">Тип применяемого значения</param>
        /// <param name="idType">Номер типа карточки, к которой будет привязано свойство</param>
        /// <returns></returns>
        public int add(string name, int typeValue, int idType)
        {
            string query = "" +
                    "INSERT INTO tb_type_card_props " +
                    "SET " +
                    "name = '" + name + "', " +
                    "type_value = '" + typeValue + "', " +
                    "id_type = '" + idType + "'";
            
            return classMysql.Insert(query);
        }

        /// <summary>
        /// Выдает свойства по номеру типа карточки
        /// </summary>
        /// <param name="idType">номер карточки</param>
        /// <returns></returns>
        public CardPropsCollection[] getInfoByIdTypeCard(int idType)
        {
            string query = "" +
                "SELECT * " +
                "FROM tb_type_card_props " +
                "WHERE " +
                "id_type = '" + idType + "'";
            var rows =  classMysql.getArrayByQuery(query);

            int numRows = classMysql.getNumRows(query);
            CardPropsCollection[] typeCardProps = new CardPropsCollection[rows.GetLength(0)];

            numRows = 0;
            foreach (var row in rows)
            {
                typeCardProps[numRows].id = Convert.ToInt32(row["id"]);
                typeCardProps[numRows].idTypeCard = Convert.ToInt32(row["id_type"]);
                typeCardProps[numRows].typeValue = Convert.ToInt32(row["type_value"]);
                typeCardProps[numRows].name = row["name"];
                numRows++;
            }
            return typeCardProps;
        }

        /// <summary>
        /// Выдает информацию о свойстве по его номеру
        /// </summary>
        /// <param name="id">номер свойства</param>
        /// <returns></returns>
        public CardPropsCollection getInfoById(int id)
        {
            string query = "" +
                "SELECT * " +
                "FROM tb_type_card_props " +
                "WHERE " +
                "id = '" + id + "'";

            var rows = classMysql.getArrayByQuery(query);

            CardPropsCollection typeCardProps = new CardPropsCollection();
            if (rows.GetLength(0) < 1) return typeCardProps;
            var row = rows[0];
            typeCardProps.id = Convert.ToInt32(row["id"]);
            typeCardProps.idTypeCard = Convert.ToInt32(row["id_type"]);
            typeCardProps.typeValue = Convert.ToInt32(row["type_value"]);
            typeCardProps.name = row["name"];
            return typeCardProps;
        }
    }
}