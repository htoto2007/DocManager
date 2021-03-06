﻿using System.IO;
using System.Reflection;

namespace VitSettings
{
    public class ClassSettings
    {
        public ClassSettings()
        {
            if (Properties.GeneralsSettings.Default.programPath == "")
            {
                Properties.GeneralsSettings.Default.programPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Properties.GeneralsSettings.Default.Save();
            }
            if (Properties.GeneralsSettings.Default.repositiryPayh == "")
            {
                Properties.GeneralsSettings.Default.repositiryPayh = Properties.GeneralsSettings.Default.programPath + "\\upload";
                Properties.GeneralsSettings.Default.Save();
            }
            if (!Directory.Exists(Properties.GeneralsSettings.Default.repositiryPayh))
            {
                Directory.CreateDirectory(Properties.GeneralsSettings.Default.repositiryPayh);
            }
        }

        public Props GetProperties()
        {
            Props props = new Props();
            props.generalsSttings.programPath = Properties.GeneralsSettings.Default.programPath;
            props.generalsSttings.repositiryPayh = Properties.GeneralsSettings.Default.repositiryPayh;
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