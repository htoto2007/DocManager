using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VitCardPropsValue;
using VitColors;
using VitFiles;
using VitFTP;
using VitNotifyMessage;
using VitTree;
using VitTypeCard;
using vitTypeCardProps;
using VitUsers;

namespace VitListView
{
    public class ClassLisView
    {
        private readonly ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
        private readonly ClassFiles classFiles = new ClassFiles();
        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();
        private readonly ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();
        private VitIcons.ClassImageList classImageList = new VitIcons.ClassImageList();
        private ClassFiles.FileCollection fileCollection = new ClassFiles.FileCollection();
        private readonly ClassColors classColors = new ClassColors();

        public void FromSearch(ClassFiles.FileCollection[] fileCollections, ListView listView)
        {
            Init(listView);
            listView.Columns.Add("");
            listView.Columns.Add("Имя");
            listView.Columns.Add("тип");
            listView.Columns.Add("Путь к файлу");
            
            foreach (ClassFiles.FileCollection fileCollection in fileCollections)
            {
                string imageKey = classImageList.addIconFile(fileCollection.path);
                ListViewItem listViewItem = new ListViewItem
                {
                    ImageKey = imageKey
                };

                listViewItem.SubItems.Add(Path.GetFileName(fileCollection.path)).Name = "name";
                listViewItem.SubItems.Add("file").Name = "type";
                listViewItem.SubItems.Add(fileCollection.path).Name = "path";
                listViewItem.SubItems.Add(fileCollection.createDateTime.ToString()).Name = "createDateTime";
                var item = listView.Items.Add(listViewItem);
                if (item.Index % 2 == 0) item.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.EndUpdate();
            listView.Update();
        }

        

        public void FromTreeVuew(TreeView treeView, ListView listView)
        {
            Init(listView);
            // проверяем является ли выделенный узел файлом
            if (Path.GetExtension(treeView.SelectedNode.Name) != "")
            {
                FromTreeVuewFile(treeView, listView);
            }
            else
            {
                FromTreeVuewFolder(treeView, listView);
            }
        }

        private void FromTreeVuewFile(TreeView treeView, ListView listView)
        {
            TreeNode treeNode = treeView.SelectedNode;
            int idFile = classFiles.getInfoByFilePath(treeNode.Name).id;
            ClassCardPropsValue.CardPropsValueCollection[] cardPropsValueCollections = classCardPropsValue.getByIdFile(idFile);
            
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            var file = classFTP.getFileInfo(treeNode.Name);
            classFTP.sessionClose();
            listView.Columns.Add("Название");
            listView.Columns.Add("Значение");
            ListViewItem listViewItem;
            listViewItem = new ListViewItem();
            listViewItem.Text = "Полный путь";
            listViewItem.Name = "name";
            listViewItem.SubItems.Add(file.fullName.ToString()).Name = "value";
            var item = listView.Items.Add(listViewItem);
            if (item.Index % 2 == 0) item.BackColor = System.Drawing.Color.WhiteSmoke;

            listViewItem = new ListViewItem();
            listViewItem.Text = "Дата последнего изменения";
            listViewItem.Name = "name";
            listViewItem.SubItems.Add(file.LastWriteTime.ToString()).Name = "value";
            item = listView.Items.Add(listViewItem);
            if (item.Index % 2 == 0) item.BackColor = System.Drawing.Color.WhiteSmoke;

            // если неудалось найти карточку
            if (cardPropsValueCollections == null)
            {
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView.EndUpdate();
                listView.Update();
                return;
            }
            
            foreach (ClassCardPropsValue.CardPropsValueCollection cardValue in cardPropsValueCollections)
            {
                int idCardProp = cardValue.idCardProp;
                listViewItem = new ListViewItem
                {
                    Text = classTypeCardProps.getInfoById(idCardProp).name,
                    Name = "name",
                    
                };
                listViewItem.SubItems.Add(cardValue.value).Name = "value";

                item = listView.Items.Add(listViewItem);
                if (item.Index % 2 == 0) item.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.EndUpdate();
            //listView.Update();
        }

        private void FromTreeVuewFolder(TreeView treeView, ListView listView)
        {
            listView.Columns.Add("Имя").Width = 500;
            listView.Columns.Add("Тиа").Width = 50;
            listView.Columns.Add("Путь").Width = 400;
            
            TreeNode treeNode = treeView.SelectedNode;
            foreach (TreeNode tn in treeNode.Nodes)
            {
                ListViewItem listViewItem = new ListViewItem
                {
                    Text = tn.Text,
                    ImageKey = tn.ImageKey,
                    Name = "name"
                };
                if (Path.GetExtension(tn.Text) != "")
                    listViewItem.SubItems.Add("файл").Name = "type";
                else
                    listViewItem.SubItems.Add("папка").Name = "type";
                listViewItem.SubItems.Add(tn.Name.Replace('\\', '/')).Name = "path";
                var item = listView.Items.Add(listViewItem);
                if (item.Index % 2 == 0) item.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            //listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.EndUpdate();
            //listView.Update();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="listView"></param>
        private void Init(ListView listView)
        {
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.LargeImageList = classImageList.imageList;
            listView.SmallImageList = classImageList.imageList;
            listView.StateImageList = classImageList.imageList;
            listView.View = View.Details;
            listView.MultiSelect = true;
            listView.FullRowSelect = true;
            listView.BeginUpdate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lv">Принемает объект <see cref="Lv"/> содержащий <see cref="ListView"/></param>
        private void LoadInfo(object lv)
        {
            Lv lvObj = (Lv)lv;
            ListView listView = lvObj.ListView;
            listView.Invoke((MethodInvoker)delegate
            {
                foreach (ListViewItem listViewItemCollection in listView.Items)
                {
                    if (listViewItemCollection.SubItems["type"].Text != ClassTree.TypeNodeCollection.FOLDER)
                    {
                        int id = Convert.ToInt32(listViewItemCollection.SubItems["id"].Text);
                        //fileCollection = classFiles.GetFileById(id);
                        listViewItemCollection.SubItems["path"].Text = fileCollection.path;
                        listViewItemCollection.SubItems["createDateTime"].Text = fileCollection.createDateTime.ToString();
                        string imageKey = classImageList.addIconFile(fileCollection.path);
                        listViewItemCollection.ImageKey = imageKey;
                    }
                }
            });
        }

        /// <summary>
        /// Запуск удаления файла из ListView
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        public ListViewItem[] deleteFiles(ListView listView)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            DialogResult dialogResult = classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.QUESTION, "Вы точно хотите удплить выбранные обьекты?");
            ListViewItem[] listViewItems = null;
            if (dialogResult == DialogResult.Yes)
            {
                List<string> fileList = new List<string>();
                listViewItems = new ListViewItem[listView.SelectedItems.Count];
                int iterator = 0;
                foreach (ListViewItem listViewItem in listView.SelectedItems)
                {
                    fileList.Add(listViewItem.SubItems["path"].Text);
                    listViewItems[iterator] = listViewItem;
                    iterator++;
                    listViewItem.Remove();
                }

                classFiles.DeleteFiles(fileList.ToArray());
            }
            return listViewItems;
        }
    }

    public class Lv
    {
        public ListView ListView;
    }
}