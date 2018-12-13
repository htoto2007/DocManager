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
                    FormError formErrorSystem = new FormError(textMessage, "Системная ошибка");
                    dialogResult = formErrorSystem.ShowDialog();
                    //dialogResult = MessageBox.Show(textMessage, "Системная ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return dialogResult;

                case TypeMessage.USER_ERROR:
                    FormError formErrorUser = new FormError(textMessage, "Пользовательска ошибка");
                    dialogResult = formErrorUser.ShowDialog();
                    //dialogResult = MessageBox.Show(textMessage, "Пользовательская ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return dialogResult;

                case TypeMessage.QUESTION:
                    FormQuestion formQuestion = new FormQuestion(textMessage);
                    dialogResult = formQuestion.ShowDialog();
                    //dialogResult = MessageBox.Show(textMessage, "Выберите действие.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    return dialogResult;

                case TypeMessage.INFORMATION:
                    FormInformation formInformation = new FormInformation(textMessage);
                    dialogResult = formInformation.ShowDialog();
                    //dialogResult = MessageBox.Show(textMessage, "Оповещение.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return dialogResult;

                case TypeMessage.WARNING:
                    FormWarning formWarning = new FormWarning(textMessage);
                    dialogResult = formWarning.ShowDialog();
                    //dialogResult = MessageBox.Show(textMessage, "Обратите внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return dialogResult;

                default:
                    FormError formError = new FormError("Ошибка вывода сообщения об ошибке или какого либо другого сообщения!", "Ну все, приехали!");
                    dialogResult = formError.ShowDialog();
                    //dialogResult = MessageBox.Show("Ошибка вывода сообщения об ошибке или какого либо другого сообщения!", "Ну все, приехали!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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