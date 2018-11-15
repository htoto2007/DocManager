using System;
using VitDBConnect;
using VitFTP;
using VitMysql;
using VitNotifyMessage;
using VitSettings;
using VitTextStringMask;
using VitTypeCard;

namespace VitFiles
{
    public class ClassFiles
    {
        private readonly ClassDBConnect classDB = new ClassDBConnect();

        private readonly ClassMysql classMysql = new ClassMysql();
        private readonly ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
        private readonly ClassSettings classSettings = new ClassSettings();

        private readonly ClassTextStringMask classTextStringMask = new ClassTextStringMask();

        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();

        private readonly string repositiryPayh = "";

        private readonly string root = "";

        private ClassFTP classFTP = new ClassFTP();

        public ClassFiles()

        {
            repositiryPayh = VitSettings.Properties.GeneralsSettings.Default.repositiryPayh;
            root = VitSettings.Properties.GeneralsSettings.Default.programPath;
        }

        public void createFile(string source, string destination)
        {
            string respCode = classFTP.UploadFile(source, destination);
        }

        public struct FileCollection
        {
            public DateTime createDateTime;
            public int hashCode;
            public int id;
            public int idFolder;
            public int idTypeCard;

            /// <summary>
            /// fileName.ext
            /// </summary>
            public string name;

            /// <summary>
            /// disk://directory/fileName.ext
            /// </summary>
            public string path;

            /// <summary>
            /// disk://directory/
            /// </summary>
            public string pathWithoutFileName;
        }
    }
}