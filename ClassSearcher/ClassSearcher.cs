using System;
using System.Collections.Generic;
using System.Linq;
using VitDBConnect;
using VitFiles;
using VitMysql;

namespace VitSearcher
{
    public class ClassSearcher
    {
        private readonly ClassFiles classFiles = new ClassFiles();
        ClassMysql classMysql = new ClassMysql();

        public fileIdCollection[] Start(string frase)
        {
            string[] words = fraseToWords(frase);
            int[] byCardPropsValueResult = ByCardPropsValue(words);
            int[] byFileNameResult = ByFileName(words);
            int[] allResults = new int[byCardPropsValueResult.Length + byFileNameResult.Length];
            byCardPropsValueResult.CopyTo(allResults, 0);
            byFileNameResult.CopyTo(allResults, byCardPropsValueResult.Length);
            fileIdCollection[] fileIdCollection = getRankedResult(allResults);
            return fileIdCollection = SortRankedResult(fileIdCollection);
        }

        /// <summary>
        /// Разбивает фразу на слова
        /// </summary>
        /// <param name="frase">фраза</param>
        /// <returns></returns>
        private string[] fraseToWords(string frase)
        {
            string[] words;
            while (true)
            {
                if (frase.Contains("  ") == true)
                    frase = frase.Replace("  ", " ");
                else
                {
                    frase = frase.Trim(' ');
                    words = frase.Split(' ');
                    if (words == null) return null;

                    List<string> listWord = words.ToList();
                    listWord.RemoveAll(item => item == "");
                    words = listWord.ToArray();
                    break;
                }
            }
            return words;
        }

        /// <summary>
        /// Выдает ранжированый массив слов
        /// </summary>
        /// <param name="arrIdFiles">Номера файлов, в которых найдены слова</param>
        /// <returns></returns>
        private fileIdCollection[] getRankedResult(int[] arrIdFiles)
        {
            List<int> listIdFile = arrIdFiles.ToList();
            List<int> findResult = new List<int>();
            List<int> listCountWords = new List<int>();
            
            // создаем списки с номерами файлов и количеством совпадений
            foreach (int id in arrIdFiles)
            {
                listCountWords.Add(listIdFile.FindAll(item => item == id).Count);
                listIdFile.RemoveAll(item => item == id);
                listIdFile.Add(id);
                //Console.WriteLine("id file " + listIdFile.ToString());
                //Console.WriteLine("Размер списка количства id " + listCountWords.Count);
            }
            // записываем информацию из списков в коллекцию
            fileIdCollection[] fileIdCollections = new fileIdCollection[listIdFile.Count];
            for (int i = 0; i < listIdFile.Count; i++)
            {
                fileIdCollections[i].id = listIdFile[i];
                fileIdCollections[i].countMatchWords = listCountWords[i];
            }

            return fileIdCollections;
        }

        private int[] ByCardPropsValue(string[] words)
        {
            List<int> listIdFiles = new List<int>();
            foreach (string word in words)
            {
                string query = "SELECT id_file " +
                    "FROM tb_card_props_value " +
                    "WHERE value LIKE '%" + word + "%'";
                var rows = classMysql.getArrayByQuery(query);
                if (rows.GetLength(0) < 1) continue;
                foreach (var row in rows) listIdFiles.Add(Convert.ToInt32(row["id_file"]));
            }
            return listIdFiles.ToArray();
        }

        /// <summary>
        /// Производит поиск слов в пути файлов
        /// </summary>
        /// <param name="words">массив искомых слов</param>
        /// <returns></returns>
        private int[] ByFileName(string[] words)
        {
            List<int> listIdFiles = new List<int>();
            foreach (string word in words)
            {
                string query = "SELECT id " +
                    "FROM tb_files " +
                    "WHERE path LIKE '%" + word + "%'";
                var rows = classMysql.getArrayByQuery(query);
                if (rows.GetLength(0) < 1) continue;
                foreach (var row in rows) listIdFiles.Add(Convert.ToInt32(row["id"]));
            }
            return listIdFiles.ToArray();
        }

        /// <summary>
        /// Сортирует результат поиска согласно рангу
        /// </summary>
        /// <param name="fileIdCollections"></param>
        /// <returns></returns>
        private fileIdCollection[] SortRankedResult(fileIdCollection[] fileIdCollections)
        {
            fileIdCollection fileIdCollectionBuf = new fileIdCollection();
            for (int i = 0; i < fileIdCollections.GetLength(0); i++)
            {
                for (int j = 0; j < fileIdCollections.GetLength(0); j++)
                {
                    if (fileIdCollections[i].countMatchWords > fileIdCollections[j].countMatchWords)
                    {
                        fileIdCollectionBuf = fileIdCollections[j];
                        fileIdCollections[j] = fileIdCollections[i];
                        fileIdCollections[i] = fileIdCollectionBuf;
                    }
                }
            }
            return fileIdCollections;
        }

        public struct fileIdCollection
        {
            public int id;
            public int countMatchWords;
        }
    }
}