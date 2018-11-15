using System;
using System.Threading;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFiles;
using VitTree;
using VitTypeCard;
using vitTypeCardProps;

namespace VitListView
{
    public class ClassLisView
    {
        private readonly ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
        private readonly ClassFiles classFiles = new ClassFiles();
        private readonly VitFolder.ClassFolder classFolder = new VitFolder.ClassFolder();
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
            listView.Columns.Add("");
            listView.Columns.Add("#");
            listView.Columns.Add("Имя");
            listView.Columns.Add("тип");
            listView.Columns.Add("Дата создания");
            listView.Columns.Add("Путь");
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
    }

    public class Lv
    {
        public ListView ListView;
    }
}