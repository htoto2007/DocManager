using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitNotifyMessage;
using vitTypeCardProps;

namespace VitTypeCard
{
    public partial class FormTypeCard : Form
    {
        public FormTypeCard()
        {
            InitializeComponent();
            initListViewTypeCard();
            initListViewCardProps();
            initButtons();
        }

        private void initButtons()
        {
            if (listViewTypeCards.SelectedItems.Count < 1)
            {
                buttonChangeType.Enabled = false;
                buttonDeleteType.Enabled = false;
            }
            if (listViewTypeCards.SelectedItems.Count == 1)
            {
                buttonChangeType.Enabled = true;
                buttonDeleteType.Enabled = true;
            }
            if (listViewTypeCards.SelectedItems.Count > 1)
            {
                buttonChangeType.Enabled = false;
                buttonDeleteType.Enabled = true;
            }
        }

        private void initListViewTypeCard()
        {
            ClassTypeCard classTypeCard = new ClassTypeCard();
            ClassTypeCard.TypeCardCollection[] typeCardCollections = classTypeCard.getAllInfo();
            listViewTypeCards.BeginUpdate();
            listViewTypeCards.Clear();
            listViewTypeCards.View = View.Details;
            listViewTypeCards.MultiSelect = true;
            listViewTypeCards.FullRowSelect = true;
            listViewTypeCards.HideSelection = false;

            listViewTypeCards.Columns.Add("").Width = 0;
            listViewTypeCards.Columns.Add("#");
            listViewTypeCards.Columns.Add("Название");
            foreach (var typeCardCollection in typeCardCollections)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems.Add(typeCardCollection.id.ToString()).Name = "id";
                listViewItem.SubItems.Add(typeCardCollection.namne).Name = "name";
                listViewTypeCards.Items.Add(listViewItem);
            }
            listViewTypeCards.EndUpdate();
            listViewTypeCards.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void listViewTypeCards_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            initListViewCardProps();
            initButtons();
        }

        private void initListViewCardProps()
        {
            if(listViewTypeCards.SelectedItems.Count == 1)
            {
                int idTypeCardProp = Convert.ToInt32(listViewTypeCards.SelectedItems[0].SubItems["id"].Text);
                ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();
                ClassTypeCardProps.TypeCardProps[] typeCardProps = classTypeCardProps.getInfoByIdType(idTypeCardProp);

                listViewCardProps.BeginUpdate();
                listViewCardProps.Clear();
                listViewCardProps.View = View.Details;
                listViewCardProps.MultiSelect = true;
                listViewCardProps.FullRowSelect = true;
                listViewCardProps.HideSelection = false;

                listViewCardProps.Columns.Add("").Width = 0;
                listViewCardProps.Columns.Add("#");
                listViewCardProps.Columns.Add("Название");
                listViewCardProps.Columns.Add("Тип");
                foreach (var typeCardProp in typeCardProps)
                {
                    ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems.Add(typeCardProp.id.ToString()).Name = "id";
                    listViewItem.SubItems.Add(typeCardProp.name).Name = "name";
                    ClassTypeCard classTypeCard = new ClassTypeCard();
                string typeValueName = classTypeCard.getNamePropById(typeCardProp.typeValue);
                listViewItem.SubItems.Add(typeValueName);
                    listViewCardProps.Items.Add(listViewItem);
                }
                listViewCardProps.EndUpdate();
                listViewCardProps.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            else
            {
                listViewCardProps.Items.Clear();
            }
        }

        private void buttonAddType_Click(object sender, EventArgs e)
        {
            FormCreatTypeCard formCreatTypeCard = new FormCreatTypeCard();
            formCreatTypeCard.ShowDialog();
            initListViewTypeCard();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonDeleteType_Click(object sender, EventArgs e)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            DialogResult dialogResult = classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.QUESTION, "Вы действительно хотите удалить выделеные объекты?");
            if (dialogResult == DialogResult.Yes)
            {
                foreach (ListViewItem listViewItem in listViewTypeCards.SelectedItems)
                {
                    int idTypeCard = Convert.ToInt32(listViewItem.SubItems["id"].Text);
                    ClassTypeCard classTypeCard = new ClassTypeCard();
                    classTypeCard.DeleteById(idTypeCard);
                }
                initListViewTypeCard();
                initListViewCardProps();
            }
        }

        private void buttonChangeType_Click(object sender, EventArgs e)
        {

        }
    }
}
