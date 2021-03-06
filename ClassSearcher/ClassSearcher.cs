﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using VitDBConnect;
using VitFiles;

namespace VitSearcher
{
    public class ClassSearcher
    {
        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassFiles classFiles = new ClassFiles();

        public ClassFiles.FileCollection[] start(string strSearch)
        {
            string[] arrStr = fraseToWords(strSearch);
            if (arrStr == null)
            {
                return null;
            }

            // производим поиск по имени файла
            int[] resByFileName = getResultByFileName(arrStr);

            // производим поиск по свойствам карточки документа
            int[] resByCardPropsValue = getResultByCardPropsValue(arrStr);

            int[] res;
            if ((resByCardPropsValue != null) && (resByFileName != null))
            {
                res = resByFileName.Concat(resByCardPropsValue).ToArray();
            }
            else if (resByCardPropsValue != null)
            {
                res = resByCardPropsValue;
            }
            else if (resByFileName != null)
            {
                res = resByFileName;
            }
            else
            {
                return null;
            }

            int[,] runkResult = getRankedResult(res);
            runkResult = sortRankedResult(runkResult);
            ClassFiles.FileCollection[] fileCollection;
            fileCollection = getFoundFiles(runkResult);
            return fileCollection;
        }

        private string[] fraseToWords(string frase)
        {
            string[] words;
            while (true)
            {
                if (frase.Contains("  ") == true)
                {
                    frase = frase.Replace("  ", " ");
                }
                else
                {
                    frase = frase.Trim(' ');
                    words = frase.Split(' ');
                    if (words == null)
                    {
                        return null;
                    }

                    List<string> listWord = words.ToList();
                    listWord.RemoveAll(item => item == "");
                    words = listWord.ToArray();
                    break;
                }
                //Console.WriteLine("fraseToWords [" +  + "]");
            }
            return words;
        }

        private int[] getResultByCardPropsValue(string[] words)
        {
            int[] resultId;
            string str = "";
            foreach (string word in words)
            {
                string query = "SELECT id_file " +
                    "FROM tb_card_props_value " +
                    "WHERE value LIKE '%" + word + "%'";

                classDB.dbLink.Open();
                MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
                MySqlDataReader mysqlDataReader = command.ExecuteReader();

                // создаем строку из id файлов
                while (mysqlDataReader.Read())
                {
                    if (mysqlDataReader.GetString("id_file") != "")
                    {
                        str += " " + mysqlDataReader.GetString("id_file");
                    }
                }

                classDB.dbLink.Close();
            }
            Console.WriteLine("[" + str + "]");
            if (str == "")
            {
                return null;
            }

            str = str.TrimStart(' ');
            Console.WriteLine("Trim [" + str + "]");
            string[] arrStr = str.Split(' ');
            Console.WriteLine("arrStr [" + arrStr.ToString() + "]");
            resultId = new int[arrStr.Length];
            for (int i = 0; i < arrStr.Length; i++)
            {
                resultId[i] = Convert.ToInt32(arrStr[i]);
            }

            return resultId;
        }

        private int[] getResultByFileName(string[] words)
        {
            int[] resultId;
            string str = "";
            foreach (string word in words)
            {
                string query = "SELECT id " +
                    "FROM tb_files " +
                    "WHERE name LIKE '%" + word + "%'";

                classDB.dbLink.Open();
                MySqlCommand command = new MySqlCommand(query, classDB.dbLink);
                MySqlDataReader mysqlDataReader = command.ExecuteReader();

                while (mysqlDataReader.Read())
                {
                    if (mysqlDataReader.GetString("id") != "")
                    {
                        str += " " + mysqlDataReader.GetString("id");
                    }
                }

                classDB.dbLink.Close();
            }
            //Console.WriteLine("[" + str + "]");
            if (str == "")
            {
                return null;
            }

            str = str.TrimStart(' ');
            //Console.WriteLine("Trim [" + str + "]");
            string[] arrStr = str.Split(' ');
            //Console.WriteLine("arrStr [" + arrStr.ToString() + "]");
            resultId = new int[arrStr.Length];
            for (int i = 0; i < arrStr.Length; i++)
            {
                resultId[i] = Convert.ToInt32(arrStr[i]);
            }

            return resultId;
        }

        private int[,] getRankedResult(int[] arrIdFiles)
        {
            string strId = "", strCount = "";
            List<int> listIdFile = arrIdFiles.ToList();
            List<int> findResult = null;

            foreach (int id in arrIdFiles)
            {
                findResult = listIdFile.FindAll(item => item == id);
                strCount += " " + findResult.Count().ToString();
                strId += " " + id;
                listIdFile.RemoveAll(item => item == id);
                arrIdFiles = listIdFile.ToArray();
                if (arrIdFiles.Length == 0)
                {
                    break;
                }
            }
            strCount = strCount.TrimStart(' ');
            strId = strId.TrimStart(' ');

            string[] arrStrCount = strCount.Split(' ');
            string[] arrStrId = strId.Split(' ');

            int[,] result = new int[arrStrId.Length, 2];
            for (int i = 0; i < arrStrId.Length; i++)
            {
                Console.WriteLine("arrStrId [" + arrStrId[i] + "]");
                result[i, 0] = Convert.ToInt32(arrStrId[i]);
                Console.WriteLine("arrStrCount [" + arrStrCount[i] + "]");
                result[i, 1] = Convert.ToInt32(arrStrCount[i]);
            }

            return result;
        }

        private int[,] sortRankedResult(int[,] rankedResult)
        {
            for (int i = 0; i < rankedResult.GetLength(0); i++)
            {
                for (int j = 0; j < rankedResult.GetLength(0); j++)
                {
                    if (rankedResult[i, 1] > rankedResult[j, 1])
                    {
                        rankedResult[i, 1] += rankedResult[j, 1];
                        rankedResult[j, 1] = rankedResult[i, 1] - rankedResult[j, 1];
                        rankedResult[i, 1] = rankedResult[i, 1] - rankedResult[j, 1];
                    }
                }
            }
            return rankedResult;
        }

        private ClassFiles.FileCollection[] getFoundFiles(int[,] rankResult)
        {
            ClassFiles.FileCollection[] fileCollection = new ClassFiles.FileCollection[rankResult.GetLength(0)];
            for (int i = 0; i < rankResult.GetLength(0); i++)
            {
                fileCollection[i] = classFiles.GetFileById(rankResult[i, 0]);
            }

            return fileCollection;
        }
    }
}