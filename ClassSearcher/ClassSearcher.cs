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
            }
            return words;
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

        private int[] getResultByCardPropsValue(string[] words)
        {
            int[] resultId;
            string str = "";
            foreach (string word in words)
            {
                string query = "SELECT id_file " +
                    "FROM tb_card_props_value " +
                    "WHERE value LIKE '%" + word + "%'";
                var rows = classMysql.getArrayByQuery(query);
                
                // создаем строку из id файлов
                foreach(var row in rows)
                {
                    if (row["id_file"] != "")
                    {
                        str += " " + row["id_file"];
                    }
                }
            }

            if (str == "")
            {
                return null;
            }

            str = str.TrimStart(' ');
            string[] arrStr = str.Split(' ');
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
                var rows = classMysql.getArrayByQuery(query);
                foreach(var row in rows)
                {
                    if (row["id"] != "")
                    {
                        str += " " + row["id"];
                    }
                }
            }

            if (str == "")
            {
                return null;
            }

            str = str.TrimStart(' ');
            string[] arrStr = str.Split(' ');
            resultId = new int[arrStr.Length];
            for (int i = 0; i < arrStr.Length; i++)
            {
                resultId[i] = Convert.ToInt32(arrStr[i]);
            }

            return resultId;
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
    }
}