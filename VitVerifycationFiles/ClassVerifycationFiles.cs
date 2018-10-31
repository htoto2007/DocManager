using System.Collections.Generic;
using System.IO;
using VitFiles;
using VitSettings;

namespace VitVerifycationFiles
{
    public class ClassVerifycationFiles
    {
        private readonly ClassSettings classSettings = new ClassSettings();
        private readonly string repositoryPath = "";
        private readonly string root = "";
        private ClassFiles classFiles = new ClassFiles();

        //FormVerifycationFiles formVerifycationFiles = new FormVerifycationFiles();
        private readonly string repositoryRootFolderName = "";

        public ClassVerifycationFiles()
        {
            root = VitSettings.Properties.GeneralsSettings.Default.programPath;
            repositoryPath = VitSettings.Properties.GeneralsSettings.Default.repositiryPayh;
            repositoryRootFolderName = VitSettings.Properties.GeneralsSettings.Default.repositoryRootFolderName;
        }

        public ClassFiles.FileCollection[] CheckFiles()
        {
            string[] arrPathes = DirectoryScaner();
            string strPathes = "";

            // листаем массив путей к файлм
            foreach (string path in arrPathes)
            {
                if (path == "")
                {
                    continue;
                }

                if (CompareWithData(path) == true)
                {
                    continue;
                }

                strPathes += path + "\n";
            }

            strPathes = strPathes.Trim('\n');
            arrPathes = strPathes.Split('\n');
            ClassFiles.FileCollection[] fileColections = new ClassFiles.FileCollection[arrPathes.GetLength(0)];
            int iterator = 0;
            foreach (string str in arrPathes)
            {
                if (str == "")
                {
                    continue;
                }

                fileColections[iterator].name = Path.GetFileName(str);
                fileColections[iterator].path = str;
                fileColections[iterator].createDateTime = File.GetCreationTime(str);

                iterator++;
            }
            return fileColections;
        }

        public bool CompareWithData(string fileName)
        {
            if (classFiles.getFileByName(fileName).id == 0)
            {
                return false;
            }

            return true;
        }

        public string[] DirectoryScaner()
        {
            string[] arrStr = Directory.GetDirectories(repositoryPath + "\\" + repositoryRootFolderName, "*", SearchOption.AllDirectories);
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
    }
}