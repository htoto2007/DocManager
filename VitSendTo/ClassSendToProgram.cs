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
        private ClassFiles classFile = new ClassFiles();
        private ClassTree classTree = new ClassTree();
        private ClassTypeCard classTypeCard = new ClassTypeCard();
        private FormFiles formFiles = new FormFiles();

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
            TreeNode treeNode = classTree.TreeViewDialog("Добавление документа", "Добавить");
            int idFolder = Convert.ToInt32(treeNode.Name.Split('_')[1]);
            int idTypeCard = classTypeCard.getIdByName("Пустая");
            string res = "";

            foreach (string arg in args)
            {
                if (arg == "noCard")
                {
                    continue;
                }

                string fileName = Path.GetFileName(arg);
                string pathSave = treeNode.FullPath;
                string fullPath = arg;
                if (classFile.Create(idFolder, idTypeCard, fileName, fullPath, pathSave) == 0)
                {
                    res += arg + " Ошибка загрузки! \n";
                }
            }

            if (res != "")
            {
                MessageBox.Show(res);
            }
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
                        classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.NumericUpDown":
                        // разбиваем имя элемента
                        arrNameControl = ((NumericUpDown)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = ((NumericUpDown)control).Text;
                        classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.MaskedTextBox":
                        // разбиваем имя элемента
                        arrNameControl = ((MaskedTextBox)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = ((MaskedTextBox)control).Text;
                        classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.CheckBox":
                        // разбиваем имя элемента
                        arrNameControl = ((CheckBox)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = Convert.ToInt32(((CheckBox)control).Checked).ToString();
                        classCardPropsValue.createValue(idCardProps, value, idFile);
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
            TreeNode treeNode = classTree.TreeViewDialog("Добавление документа", "Добавить");
            int idFolder = Convert.ToInt32(treeNode.Name.Split('_')[1]);
            int idTypeCard = classTypeCard.getIdByName("Пустая");
            string res = "";

            formFiles.Text = "Укажите имя файла";
            formFiles.textBoxNameFile.ReadOnly = true;
            formFiles.textBoxTreePath.Text = treeNode.FullPath;
            formFiles.isCheck = false;
            formFiles.ShowDialog();

            // получаем получаем номер типа карточки документа по имени имав карточки
            idTypeCard = classTypeCard.getIdByName(formFiles.comboBox1.Text.ToString());

            foreach (string arg in args)
            {
                // если мы попали на ячеку массива с параметром коммандной строки
                if (arg == "withCard")
                {
                    continue;
                }

                string fileName = Path.GetFileName(arg);
                string pathSave = treeNode.FullPath;
                string fullPath = arg;
                int idFile = classFile.Create(idFolder, idTypeCard, fileName, fullPath, pathSave);

                // если файл не создался
                if (idFile != 0)
                {
                    res += arg + " Ошибка загрузки! \n";
                    continue;
                }
                Control.ControlCollection controlCollection = formFiles.panel1.Controls;
                sendValueOfFields(controlCollection, idFile);
            }

            if (res != "")
            {
                MessageBox.Show(res);
            }
        }
    }
}