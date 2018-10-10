namespace VitTextStringMask
{
    public class ClassTextStringMask
    {
        /// <summary>
        /// переменная для счетчика с нуля
        /// </summary>
        private int counter0 = 0;

        /// <summary>
        /// переменная для счетчика с еденицы
        /// </summary>
        private int counter1 = 1;

        /// <summary>
        /// Набор шаблонов
        /// </summary>
        private struct Patern
        {
            /// <summary>
            /// счетчик с еденицы
            /// </summary>
            public const string counter1 = "{C}";

            /// <summary>
            /// счетчик с нуля
            /// </summary>
            public const string counter0 = "{c}";
        };

        /// <summary>
        /// Определяет метод по шаблону
        /// </summary>
        /// <param name="str">текст с шаблоном</param>
        /// <returns></returns>
        public string DeterminationMask(string str)
        {
            if (str.Contains(Patern.counter0))
            {
                return Counter0(str);
            }
            else if (str.Contains(Patern.counter1))
            {
                return Counter1(str);
            }

            return null;
        }

        /// <summary>
        /// Начинает подсчет с нуля
        /// </summary>
        /// <param name="str">Текст с шаблоном</param>
        /// <returns></returns>
        public string Counter0(string str)
        {
            str = str.Replace(Patern.counter0, counter0.ToString());
            counter0++;
            return str;
        }

        /// <summary>
        /// Начинает подсчет с еденицы
        /// </summary>
        /// <param name="str">Текст с шаблоном</param>
        /// <returns></returns>
        public string Counter1(string str)
        {
            str = str.Replace(Patern.counter1, counter1.ToString());
            counter1++;
            return str;
        }
    }
}