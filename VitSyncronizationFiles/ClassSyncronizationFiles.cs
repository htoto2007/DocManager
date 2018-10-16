namespace VitSyncronizationFiles
{
    public class ClassSyncronizationFiles
    {
        /// <summary>
        /// Производим упорядочивание файлов по exel
        /// </summary>
        /// <param name="fileColections"></param>
        private void FromExcel(VitFiles.ClassFiles.FileCollection[] fileColections)
        {
            VitExcel.ClassExcel classExcel = new VitExcel.ClassExcel();
            // получаем массив ячеик из Excel
            string[,] arrString = classExcel.GetRange();
            // листакм массив коллекций файлов
            foreach (VitFiles.ClassFiles.FileCollection fileColection in fileColections)
            {
                // получаем номер из имени файла
                string fileNumber = getNumberFromFileName(fileColection.name);
                // перебираем строки результата из excel
                for (int row = 3; row < arrString.GetLength(0); row++)
                {
                    //if(arrString[row, 1] == fileNumber)
                }
            }
        }

        /// <summary>
        /// Получает номер из названия файла
        /// </summary>
        /// <param name="fileName">fileName.ext</param>
        /// <returns></returns>
        private string getNumberFromFileName(string fileName)
        {
            // отсекаем от названия слово "дело" и получаем номер
            fileName = fileName.Replace("дело", "");
            // убираем приставку "№" из номера
            fileName = fileName.Replace("№", "");
            // убираем пробелы
            fileName = fileName.Replace(" ", "");

            return fileName;
        }
    }
}