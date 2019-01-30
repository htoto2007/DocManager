using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFiles;
using VitFTP;
using VitNotifyMessage;
using VitTree;
using VitTypeCard;
using vitTypeCardProps;

namespace VitListView
{
    public class ClassLisView
    {
        private readonly ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
        private readonly ClassFiles classFiles = new ClassFiles();
        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();
        private readonly ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();
        private readonly Thread threedLoadInfo;
        private VitIcons.ClassImageList classImageList = new VitIcons.ClassImageList();
        private ClassFiles.FileCollection fileCollection = new ClassFiles.FileCollection();
        private readonly Thread threedLoadIcons;

        public void FromSearch(ClassFiles.FileCollection[] fileCollections, ListView listView)
        {
            Init(listView);
            listView.Columns.Add("");
            listView.Columns.Add("Имя");
            listView.Columns.Add("тип");
            listView.Columns.Add("Дата создания");

            foreach (ClassFiles.FileCollection fileCollection in fileCollections)
            {
                string imageKey = classImageList.addIconFile(fileCollection.path);
                ListViewItem listViewItem = new ListViewItem
                {
                    ImageKey = imageKey
                };

                listViewItem.SubItems.Add(fileCollection.name).Name = "name";
                listViewItem.SubItems.Add("file").Name = "type";
                listViewItem.SubItems.Add(fileCollection.createDateTime.ToString()).Name = "path";
                listViewItem.SubItems.Add(fileCollection.createDateTime.ToString()).Name = "createDateTime";
                listView.Items.Add(listViewItem);
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
            listView.Columns.Add("Название");
            listView.Columns.Add("Значение");
            
            TreeNode treeNode = treeView.SelectedNode;
            classFiles.getInfoByFilePath(treeNode.Name);
            ClassCardPropsValue.CardPropsValueCollection[] cardPropsValueCollections = classCardPropsValue.getByIdFile();
            
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
                ListViewItem listViewItem = new ListViewItem
                {
                    Text = classTypeCardProps.getInfoById(idCardProp).name,
                    Name = "name"
                };
                listViewItem.SubItems.Add(cardValue.value).Name = "value";
                listView.Items.Add(listViewItem);
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.EndUpdate();
            listView.Update();
        }

        private void FromTreeVuewFolder(TreeView treeView, ListView listView)
        {
            listView.Columns.Add("");
            listView.Columns.Add("Имя");
            listView.Columns.Add("тип");
            listView.Columns.Add("Путь");
            
            TreeNode treeNode = treeView.SelectedNode;
            foreach (TreeNode tn in treeNode.Nodes)
            {
                ListViewItem listViewItem = new ListViewItem
                {
                    ImageKey = tn.ImageKey
                };

                listViewItem.SubItems.Add(tn.Text).Name = "name";
                if (Path.GetExtension(tn.Text) != "")
                    listViewItem.SubItems.Add("file").Name = "type";
                else
                    listViewItem.SubItems.Add("folder").Name = "type";
                listViewItem.SubItems.Add(tn.FullPath.Replace('\\', '/')).Name = "path";
                listView.Items.Add(listViewItem);
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.EndUpdate();
            listView.Update();
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