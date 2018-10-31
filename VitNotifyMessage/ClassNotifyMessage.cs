using System.Windows.Forms;

namespace VitNotifyMessage
{
    public class ClassNotifyMessage
    {
        /// <summary>
        /// Выводит пользовательские сообщение различных типов.
        /// </summary>
        /// <param name="typeMessage">Определяет тип пользовательского сообщения. Может быть задан структурой <see cref="TypeMessage"/></param>
        /// <param name="textMessage">Задает отображаемый текст в теле сообщения</param>
        /// <returns></returns>
        public DialogResult showDialog(int typeMessage, string textMessage)
        {
            DialogResult dialogResult = DialogResult.None;
            switch (typeMessage)
            {
                case TypeMessage.SYSTEM_ERROR:
                    dialogResult = MessageBox.Show(textMessage, "Системная ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return dialogResult;

                case TypeMessage.USER_ERROR:
                    dialogResult = MessageBox.Show(textMessage, "Пользовательская ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return dialogResult;

                case TypeMessage.QUESTION:
                    dialogResult = MessageBox.Show(textMessage, "Выберите действие.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    return dialogResult;

                case TypeMessage.INFORMATION:
                    dialogResult = MessageBox.Show(textMessage, "Оповещение.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return dialogResult;

                case TypeMessage.WARNING:
                    dialogResult = MessageBox.Show(textMessage, "Обратите внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return dialogResult;

                default:
                    dialogResult = MessageBox.Show("Ошибка вывода сообщения об ошибке или какого либо другого сообщения!", "Ну все, приехали!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return dialogResult;
            }
        }

        /// <summary>
        /// Структура типов сообщений
        /// </summary>
        public struct TypeMessage
        {
            /// <summary>
            /// Информационное сообщение
            /// </summary>
            public const int INFORMATION = 3;

            /// <summary>
            /// Сообщения с вопросом к пользователю
            /// </summary>
            public const int QUESTION = 2;

            /// <summary>
            /// Предназначен для системных ошибок
            /// </summary>
            public const int SYSTEM_ERROR = 0;

            /// <summary>
            /// Предназначен для пользовательских ошибок
            /// </summary>
            public const int USER_ERROR = 1;

            /// <summary>
            /// Предостерегающее сообщение
            /// </summary>
            public const int WARNING = 4;
        }
    }
}