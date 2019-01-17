using System.Windows.Forms;
using VitAccessGroup;


namespace VitUsers
{
    class ClassTabPageGroups
    {
        VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
        protected internal void initListViewGroups(ListView listViewGroups)
        {
            listViewGroups.Clear();
            listViewGroups.MultiSelect = true;
            listViewGroups.LargeImageList = formCompanents.imageListColor;
            listViewGroups.SmallImageList = formCompanents.imageListColor;
            listViewGroups.BeginUpdate();
            listViewGroups.View = View.Details;
            listViewGroups.FullRowSelect = true;
            listViewGroups.HideSelection = false;
            listViewGroups.Columns.Clear();
            listViewGroups.Columns.Add("#");
            listViewGroups.Columns.Add("Название группы");
            listViewGroups.Columns.Add("Количество пользователей");
            listViewGroups.Columns.Add("Ранг");
            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            var groups = classAccessGroup.getInfo();
            foreach (var group in groups)
            {
                ClassUsers classUsers = new ClassUsers();
                var users = classUsers.GetUserByidAccessGroup(group.id);
                ListViewItem listViewItem = new ListViewItem(group.id.ToString());
                listViewItem.Name = "id";
                listViewItem.SubItems.Add(group.name).Name = "name";
                listViewItem.SubItems.Add(users.GetLength(0).ToString()).Name = "usersCount";
                listViewItem.SubItems.Add(group.rank.ToString()).Name = "rank";
                listViewGroups.Items.Add(listViewItem);
            }
            listViewGroups.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewGroups.EndUpdate();
            listViewGroups.Update();
        }
    }
}
