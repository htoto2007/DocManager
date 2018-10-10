using System;
using System.Collections.Generic;
using System.IO;
using VitFiles;
using VitSettings;

namespace VitVerifycationFiles
{
    public class ClassVerifycationFiles
    {
        private ClassSettings classSettings = new ClassSettings();
        private ClassFiles classFiles = new ClassFiles();

        //FormVerifycationFiles formVerifycationFiles = new FormVerifycationFiles();
        private readonly string root = "";

        public ClassVerifycationFiles()
        {
            root = classSettings.GetProperties().generalsSttings.programPath;
            //formVerifycationFiles.Show();
        }

        public struct FileColection
        {
            public string name;
            public string path;
            public DateTime createDateTime;
        }

        public FileColection[] CheckFiles()
        {
            string[] arrStr = DirectoryScaner();
            string strReturn = "";
            //formVerifycationFiles.richTextBox1.Clear();
            foreach (string str in arrStr)
            {
                if (CompareWithData(str) == false)
                {
                    strReturn += str + "\n";
                }
            }
            Console.WriteLine(strReturn);
            arrStr = strReturn.Split('\n');
            FileColection[] fileColections = new FileColection[arrStr.GetLength(0)];
            int iterator = 0;
            foreach (string str in arrStr)
            {
                if (str == "")
                {
                    continue;
                }

                fileColections[iterator].name = Path.GetFileName(str);
                fileColections[iterator].path = getCanonicalPath(str);
                fileColections[iterator].createDateTime = File.GetCreationTime(str);
                iterator++;
            }
            return fileColections;
        }

        public string[] DirectoryScaner()
        {
            //MessageBox.Show(root + "//upload//");
            string[] arrStr = Directory.GetDirectories(root + "\\upload\\", "*", SearchOption.AllDirectories);
            List<string> lst = new List<string>();
            foreach (string str in arrStr)
            {
                DirectoryInfo dir = new DirectoryInfo(str);
                foreach (FileInfo file in dir.GetFiles())
                {
                    lst.Add(file.DirectoryName + "\\" + file.Name);
                }
            }
            return lst.ToArray();
        }

        private string getCanonicalPath(string path)
        {
            path = path.Replace(root + @"\", "");
            path = path.Replace("/", @"\");
            return path;
        }

        public bool CompareWithData(string fileName)
        {
            fileName = getCanonicalPath(fileName);
            if (classFiles.getFileByName(fileName).id == 0)
            {
                return false;
            }

            return true;
        }
    }
}