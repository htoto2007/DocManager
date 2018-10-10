using System.IO;
using System.Reflection;

namespace VitSettings
{
    public class ClassSettings
    {
        public ClassSettings()
        {
            Properties.GeneralsSettings.Default.programPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Properties.GeneralsSettings.Default.Save();
        }

        public Props GetProperties()
        {
            Props props = new Props();
            props.generalsSttings.programPath = Properties.GeneralsSettings.Default.programPath;
            props.generalsSttings.repositiryPayh = Properties.GeneralsSettings.Default.repositiryPayh;
            props.generalsSttings.mysqkRunPath = Properties.GeneralsSettings.Default.mysqkRunPath;
            return props;
        }

        private void saveSettings(Props props)
        {
            if (props.generalsSttings.repositiryPayh != null)
            {
                Properties.GeneralsSettings.Default.repositiryPayh = props.generalsSttings.repositiryPayh;
            }

            if (props.generalsSttings.programPath != null)
            {
                Properties.GeneralsSettings.Default.programPath = props.generalsSttings.programPath;
            }

            if (props.generalsSttings.mysqkRunPath != null)
            {
                Properties.GeneralsSettings.Default.mysqkRunPath = props.generalsSttings.mysqkRunPath;
            }

            Properties.GeneralsSettings.Default.Save();
        }

        public struct GeneralsSttings
        {
            public string mysqkRunPath;
            public string programPath;
            public string repositiryPayh;
        }

        public struct Props
        {
            public GeneralsSttings generalsSttings;
        }
    }
}