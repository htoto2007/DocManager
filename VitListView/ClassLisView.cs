using System;
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
        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();
        private readonly ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();
        private VitIcons.ClassImageList classImageList = new VitIcons.ClassImageList();

        public void FromSearch(ClassFiles.FileCollection[] fileCollections, ListView listView)
        {
            listView.Columns.Clear();
            listView.Items.Clear();
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            listView.LargeImageList = classImageList.imageList;
            listView.SmallImageList = classImageList.imageList;
            listView.StateImageList = classImageList.imageList;
            listView.View = View.Details;
            listView.MultiSelect = true;
            listView.FullRowSelect = true;
            listView.BeginUpdate();
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
            listView.Columns.Clear();
            listView.Items.Clear();

            listView.LargeImageList = classImageList.imageList;
            listView.SmallImageList = classImageList.imageList;
            listView.StateImageList = classImageList.imageList;
            listView.View = View.Details;
            listView.MultiSelect = true;
            listView.FullRowSelect = true;
            listView.BeginUpdate();
            listView.Columns.Add("");
            listView.Columns.Add("#");
            listView.Columns.Add("Имя");
            listView.Columns.Add("тип");
            listView.Columns.Add("Дата создания");
            listView.Columns.Add("Путь");

            VitFiles.ClassFiles classFiles = new ClassFiles();
            ClassFiles.FileCollection fileCollection = new ClassFiles.FileCollection();
            VitFolder.ClassFolder classFolder = new VitFolder.ClassFolder();
            VitFolder.ClassFolder.FolderCollection foldersCollection = new VitFolder.ClassFolder.FolderCollection();

            TreeNode treeNode = treeView.SelectedNode;
            TreeNodeCollection treeNodeCollection;
            try
            {
                treeNodeCollection = treeNode.Nodes;
            }
            catch (System.NullReferenceException)
            {
                return;
            }

            foreach (TreeNode tn in treeNodeCollection)
            {
                ListViewItem listViewItem = new ListViewItem();
                string type = tn.Name.Split('_')[0];
                string imageKey = "";
                int id = Convert.ToInt32(tn.Name.Split('_')[1]);
                if (ClassTree.TypeNodeCollection.FILE == type)
                {
                    fileCollection = classFiles.GetFileById(id);
                    imageKey = classImageList.addIconFile(fileCollection.path);
                    listViewItem.ImageKey = imageKey;
                    listViewItem.SubItems.Add(fileCollection.id.ToString()).Name = "id";
                    listViewItem.SubItems.Add(fileCollection.name).Name = "name";
                    listViewItem.SubItems.Add(type).Name = "type";
                    listViewItem.SubItems.Add(fileCollection.createDateTime.ToString()).Name = "createDateTime";
                    listViewItem.SubItems.Add(fileCollection.path).Name = "path";
                    listView.Items.Add(listViewItem);
                }
                else if (ClassTree.TypeNodeCollection.FOLDER == type)
                {
                    listViewItem.ImageKey = "default_folder";
                    foldersCollection = classFolder.GetFolderById(id, true);
                    listViewItem.SubItems.Add(foldersCollection.id.ToString()).Name = "id";
                    listViewItem.SubItems.Add(foldersCollection.name).Name = "name";
                    listViewItem.SubItems.Add(type).Name = "type";
                    listViewItem.SubItems.Add(foldersCollection.createDateTime.ToString()).Name = "createDateTime";
                    listViewItem.SubItems.Add(foldersCollection.path).Name = "path";
                    listView.Items.Add(listViewItem);
                }
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.EndUpdate();
            listView.Update();
        }
    }
}