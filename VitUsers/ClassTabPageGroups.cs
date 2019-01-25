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

        public void listViewGroupUserThisGroup(ClassUsers.UserColection[] usersInGroup, ClassUsers.UserColection[] allUsers, ListView listViewUserThisGroup)
        {

            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            listViewUserThisGroup.Clear();
            listViewUserThisGroup.MultiSelect = true;
            listViewUserThisGroup.LargeImageList = formCompanents.imageListColor;
            listViewUserThisGroup.SmallImageList = formCompanents.imageListColor;
            listViewUserThisGroup.BeginUpdate();
            listViewUserThisGroup.View = View.Details;
            listViewUserThisGroup.FullRowSelect = true;
            listViewUserThisGroup.HideSelection = false;
            listViewUserThisGroup.Columns.Clear();
            listViewUserThisGroup.Columns.Add("");
            listViewUserThisGroup.Columns.Add("id");
            listViewUserThisGroup.Columns.Add("Имя");
            listViewUserThisGroup.Columns.Add("Логин");
            listViewUserThisGroup.Columns.Add("Тип доступа");

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
                listViewUserThisGroup.Items.Add(listViewItem);
            }
            listViewUserThisGroup.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewUserThisGroup.EndUpdate();
            listViewUserThisGroup.Update();
        }

        public void listViewGroupUserNoThisGroup(ClassUsers.UserColection[] usersInGroup, ClassUsers.UserColection[] allUsers, ListView listViewUserNoThisGroup)
        {
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            listViewUserNoThisGroup.Clear();
            listViewUserNoThisGroup.MultiSelect = true;
            listViewUserNoThisGroup.LargeImageList = formCompanents.imageListColor;
            listViewUserNoThisGroup.SmallImageList = formCompanents.imageListColor;
            listViewUserNoThisGroup.BeginUpdate();
            listViewUserNoThisGroup.View = View.Details;
            listViewUserNoThisGroup.FullRowSelect = true;
            listViewUserNoThisGroup.HideSelection = false;
            listViewUserNoThisGroup.Columns.Clear();
            listViewUserNoThisGroup.Columns.Add("");
            listViewUserNoThisGroup.Columns.Add("id");
            listViewUserNoThisGroup.Columns.Add("Имя");
            listViewUserNoThisGroup.Columns.Add("Логин");
            listViewUserNoThisGroup.Columns.Add("Тип доступа");
            
            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            foreach (ClassUsers.UserColection userColection in allUsers)
            {
                if (findUser(usersInGroup, userColection)) continue;
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
                listViewUserNoThisGroup.Items.Add(listViewItem);
            }
            listViewUserNoThisGroup.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewUserNoThisGroup.EndUpdate();
            listViewUserNoThisGroup.Update();
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
