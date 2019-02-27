using System.Windows.Forms;
using VitAccessGroup;
using VitSubdivision;
using VitUserPositions;
using static VitUsers.ClassUsers;

namespace VitUsers
{
    class ClassTabPageUsers
    {
        protected internal void initUsers(ListView listViewUsers)
        {
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            listViewUsers.Clear();
            listViewUsers.MultiSelect = true;
            listViewUsers.LargeImageList = formCompanents.imageListColor;
            listViewUsers.SmallImageList = formCompanents.imageListColor;
            listViewUsers.BeginUpdate();
            listViewUsers.View = View.Details;
            listViewUsers.FullRowSelect = true;
            listViewUsers.HideSelection = false;
            listViewUsers.Columns.Clear();
            listViewUsers.Columns.Add("");
            listViewUsers.Columns.Add("id");
            listViewUsers.Columns.Add("Имя");
            listViewUsers.Columns.Add("Логин");
            listViewUsers.Columns.Add("Тип доступа");
            listViewUsers.Columns.Add("Пароль");
            listViewUsers.Columns.Add("Должность");
            listViewUsers.Columns.Add("Подразделение");
            listViewUsers.Columns.Add("Электронная почта");
            ClassUsers classUsers = new ClassUsers();
            UserColection[] userColections = classUsers.GetAllUsers();
            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            foreach (UserColection userColection in userColections)
            {
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
                listViewItem.SubItems.Add(userColection.password).Name = "password";
                ClassUserPositions classUserPositions = new ClassUserPositions();
                var userPosition = classUserPositions.getInfoById(userColection.idPosition);
                listViewItem.SubItems.Add(userPosition.name).Name = "userPosition";
                ClassSubdivision classSubdivision = new ClassSubdivision();
                var subdivision = classSubdivision.getInfoById(userColection.idSubdivision);
                listViewItem.SubItems.Add(subdivision.name).Name = "subdivision";
                listViewItem.SubItems.Add(userColection.mail).Name = "Mail";
                listViewUsers.Items.Add(listViewItem);
            }
            listViewUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewUsers.EndUpdate();
            listViewUsers.Update();
        }



        
    }
}
