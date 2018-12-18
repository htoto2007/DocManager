using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFiles;
using VitSettings;
using VitTree;
using VitTypeCard;

namespace VitSendToProgram
{
    public class ClassSendToProgram
    {
        private readonly ClassSettings classSettings = new ClassSettings();
        private ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
        private readonly ClassFiles classFile = new ClassFiles();
        private readonly ClassTree classTree = new ClassTree();
        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();
        private readonly FormFiles formFiles = new FormFiles();

        public void createMenuItem()
        {
            string userName = Environment.UserName;
            string systemDisk = Path.GetPathRoot(Environment.SystemDirectory);
            string shortcutName = "Doc Manager (Без карточки).lnk";
            string shortcutPath = systemDisk + "Users\\" + userName + "\\AppData\\Roaming\\Microsoft\\Windows\\SendTo\\" + shortcutName;
            string pathToProgram = Process.GetCurrentProcess().MainModule.FileName;

            WshShell shell = new WshShell();
            IWshShortcut shortcut;
            if (System.IO.File.Exists(shortcutPath) != true)
            {
                shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.Description = "Для отправки файла в архив";
                shortcut.TargetPath = pathToProgram;
                shortcut.Arguments = "noCard";
                shortcut.Save();
            }

            shortcutName = "Doc Manager (с карточкой).lnk";
            shortcutPath = systemDisk + "Users\\" + userName + "\\AppData\\Roaming\\Microsoft\\Windows\\SendTo\\" + shortcutName;
            if (System.IO.File.Exists(shortcutPath) != true)
            {
                shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.Description = "Для отправки файла в архив";
                shortcut.TargetPath = pathToProgram;
                shortcut.Arguments = "withCard";
                shortcut.Save();
            }
        }

        public void Definition(string[] args)
        {
            if (args.GetLength(0) < 1)
            {
                return;
            }

            switch (args[0])
            {
                case "noCard":
                    NoCard(args);
                    break;

                case "withCard":
                    WithCard(args);
                    break;
            }
        }

        private void NoCard(string[] args)
        {
        }

        private void sendValueOfFields(Control.ControlCollection controlCollection, int idFile)
        {
            // идем по полуеным элементам полей карточки документа
            foreach (Control control in controlCollection)
            {
                Console.Write(control.GetType().ToString() + " ");
                Console.WriteLine(control.Name);
                string[] arrNameControl;
                int idCardProps = 0;
                string value = "";
                switch (control.GetType().ToString())
                {
                    case "System.Windows.Forms.TextBox":
                        // разбиваем имя элемента
                        arrNameControl = ((TextBox)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = ((TextBox)control).Text;
                        //classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.NumericUpDown":
                        // разбиваем имя элемента
                        arrNameControl = ((NumericUpDown)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = ((NumericUpDown)control).Text;
                        //classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.MaskedTextBox":
                        // разбиваем имя элемента
                        arrNameControl = ((MaskedTextBox)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = ((MaskedTextBox)control).Text;
                        //classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.CheckBox":
                        // разбиваем имя элемента
                        arrNameControl = ((CheckBox)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = Convert.ToInt32(((CheckBox)control).Checked).ToString();
                        //classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.Label":
                        // Пропускаем этот элемент
                        break;

                    default:
                        MessageBox.Show("Не известный тип элемента поля " + control.GetType().ToString());
                        break;
                }
            }
        }

        private void WithCard(string[] args)
        {
        }
    }
}