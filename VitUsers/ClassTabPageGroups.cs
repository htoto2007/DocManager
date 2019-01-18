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

        public void listViewGroupAllUser(ClassUsers.UserColection[] usersInGroup, ClassUsers.UserColection[] allUsers, ListView listViewAllUser)
        {
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            listViewAllUser.Clear();
            listViewAllUser.MultiSelect = true;
            listViewAllUser.LargeImageList = formCompanents.imageListColor;
            listViewAllUser.SmallImageList = formCompanents.imageListColor;
            listViewAllUser.BeginUpdate();
            listViewAllUser.View = View.Details;
            listViewAllUser.FullRowSelect = true;
            listViewAllUser.HideSelection = false;
            listViewAllUser.Columns.Clear();
            listViewAllUser.Columns.Add("");
            listViewAllUser.Columns.Add("id");
            listViewAllUser.Columns.Add("Имя");
            listViewAllUser.Columns.Add("Логин");
            listViewAllUser.Columns.Add("Тип доступа");
            
            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            foreach (ClassUsers.UserColection userColection in allUsers)
            {
                if (!findUser(usersInGroup, userColection)) continue;
                string accessGroupName = classAccessGroup.getNameById(userColection.idAccessGroup);
                ListViewItem listViewItem = new ListViewItem();

                if (accessGroupName == "Админ")
                {
                    listViewItem.ImageKey = "icons8-crown-50.png";
                }
                else
                {
                    listViewItem.ImageKey = "icons8-user-avatar-48.png";
                }

                listViewItem.SubItems.Add(userColection.id.ToString()).Name = "id";
                listViewItem.SubItems.Add(userColection.firstName + " " + userColection.lastName + " " + userColection.middleName).Name = "name";
                listViewItem.SubItems.Add(userColection.login).Name = "login";
                listViewItem.SubItems.Add(accessGroupName).Name = "accessGroup";
                listViewAllUser.Items.Add(listViewItem);
            }
            listViewAllUser.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewAllUser.EndUpdate();
            listViewAllUser.Update();
        }

        private bool findUser(ClassUsers.UserColection[] userColections, ClassUsers.UserColection userColectionFinder )
        {
            foreach(ClassUsers.UserColection userColection in userColections)
            {
                if (userColection.Equals(userColectionFinder))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
