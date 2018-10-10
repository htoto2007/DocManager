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

        public void listViewFromTreeVuew(TreeView treeView, ListView listView)
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

            TreeNode treeNode = treeView.SelectedNode;
            TreeNodeCollection treeNodeCollection;
            try
            {
                treeNodeCollection = treeNode.Nodes;
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("treeNode.Nodes is null");
                return;
            }

            Console.WriteLine("treeNode.Nodes out to listView");
            foreach (TreeNode tn in treeNodeCollection)
            {
                Console.WriteLine(tn.Name);
                ListViewItem listViewItem = new ListViewItem();
                string type = tn.Name.Split('_')[0];
                if (ClassTree.TypeNodeCollection.FILE == type)
                {
                    Console.WriteLine(ClassTree.TypeNodeCollection.FILE + " = " + type);
                    listViewItem.ImageKey = "icons8-document-48.png";
                }
                else if (ClassTree.TypeNodeCollection.FOLDER == type)
                {
                    Console.WriteLine(ClassTree.TypeNodeCollection.FILE + " = " + type);
                    listViewItem.ImageKey = "icons8-folder-48.png";
                }
                else
                {
                    Console.WriteLine(ClassTree.TypeNodeCollection.FILE + " = " + type + " (undefined)");
                    listViewItem.ImageKey = "icons8-question-mark-48.png";
                }

                string name = tn.Text;
                listViewItem.SubItems.Add(name);
                listViewItem.SubItems.Add(type);
                listView.Items.Add(listViewItem);
                //int id = Convert.ToInt32(tn.Name.Split('_')[1]);
                //classFiles.GetFileById(id).
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.EndUpdate();
            listView.Update();
            Console.WriteLine("\n");
        }
    }
}