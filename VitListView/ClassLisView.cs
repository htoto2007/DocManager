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

        public void FromTreeVuew(TreeView treeView, ListView listView)
        {
            listView.Columns.Clear();
            listView.Items.Clear();
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            listView.LargeImageList = formCompanents.imageListColor;
            listView.SmallImageList = formCompanents.imageListColor;
            listView.StateImageList = formCompanents.imageListColor;
            listView.View = View.Details;
            listView.MultiSelect = true;
            listView.FullRowSelect = true;
            listView.BeginUpdate();
            listView.Columns.Add("");
            listView.Columns.Add("Имя");
            listView.Columns.Add("тип");
            listView.Columns.Add("Дата создания");

            VitFiles.ClassFiles classFiles = new ClassFiles();
            ClassFiles.FileCollection fileCollection = new ClassFiles.FileCollection();

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
                int id = Convert.ToInt32(tn.Name.Split('_')[1]);
                if (ClassTree.TypeNodeCollection.FILE == type)
                {
                    listViewItem.ImageKey = "icons8-document-48.png";
                    fileCollection = classFiles.GetFileById(id);
                    listViewItem.SubItems.Add(fileCollection.name);
                    listViewItem.SubItems.Add(type);
                    listViewItem.SubItems.Add(fileCollection.createDateTime.ToString());
                    listView.Items.Add(listViewItem);
                }
                else if (ClassTree.TypeNodeCollection.FOLDER == type)
                {
                    listViewItem.ImageKey = "icons8-folder-48.png";
                    string name = tn.Text;
                    listViewItem.SubItems.Add(name);
                    listViewItem.SubItems.Add(type);
                    listView.Items.Add(listViewItem);
                }
                else
                {
                    listViewItem.ImageKey = "icons8-question-mark-48.png";
                    string name = tn.Text;
                    listViewItem.SubItems.Add(name);
                    listViewItem.SubItems.Add(type);
                    listView.Items.Add(listViewItem);
                }
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.EndUpdate();
            listView.Update();
        }

        public void FromSearch(ClassFiles.FileCollection[] fileCollections, ListView listView)
        {
            listView.Columns.Clear();
            listView.Items.Clear();
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            listView.LargeImageList = formCompanents.imageListColor;
            listView.SmallImageList = formCompanents.imageListColor;
            listView.StateImageList = formCompanents.imageListColor;
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
                ListViewItem listViewItem = new ListViewItem
                {
                    ImageKey = "icons8-document-48.png"
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
    }
}