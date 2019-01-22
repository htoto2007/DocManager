using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitTypeCard;
using vitTypeCardProps;

namespace VitCardPropsValue
{
    public partial class FormEditFileCard : Form
    {
        private string filePath = "";
        
        /// <summary>
        /// Выдает карточку документа по его пути
        /// </summary>
        /// <param name="filePath">Удаленный путь к документу. </param>
        public FormEditFileCard(string filePath)
        {
            InitializeComponent();
            this.filePath = filePath;
        }

        private void FormEditFileCard_Shown(object sender, EventArgs e)
        {
            textBoxFilePath.Text = filePath;

            // получаем значение полей карточки документа
            ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
            var fileCard = classCardPropsValue.getByFilePath(filePath);
            
            // получаем все типы карточек
            ClassTypeCard classTypeCard = new ClassTypeCard();
            var cardTypes = classTypeCard.getAllInfo();
            // выводим список типов
            comboBoxCardTypes.Items.Clear();
            foreach (var cardType in cardTypes) comboBoxCardTypes.Items.Add(cardType.namne);
            if (fileCard == null) return;
            // вписываем тиа карточки файла
            ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();
            var typeCardProps = classTypeCardProps.getInfoById(fileCard[0].idCardProp);
            string cardTypeName = classTypeCard.getByid(typeCardProps.idType).namne;
            comboBoxCardTypes.Text = cardTypeName;
            
            showCardProps(classTypeCard.getByid(typeCardProps.idType).id);
        }

        private void showCardProps(int idTypeCard)
        {
            panelPropsTypeCard.Controls.Clear();
            ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();
            var cardTypeProps = classTypeCardProps.getInfoByIdType(idTypeCard);
            foreach (var cardTypeProp in cardTypeProps)
            {
                int heightLastControl = 0;
                int yLocation = 0;
                if (panelPropsTypeCard.Controls.Count > 0)
                {
                    int controlCount = panelPropsTypeCard.Controls.Count;
                    var lastControl = getLastControl(new TextBox());
                    if (lastControl == null) return;
                    heightLastControl = lastControl.Height;
                    yLocation = lastControl.Location.Y;
                }

                Console.WriteLine("heightLastControl " + heightLastControl + " - yLocation " + yLocation);

                Label label = new Label();
                label.Text = cardTypeProp.name;
                label.Location = new Point(5, 5 + heightLastControl + yLocation);
                panelPropsTypeCard.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = cardTypeProp.id.ToString();
                textBox.Location = new Point(300, 5 + +heightLastControl + yLocation);
                panelPropsTypeCard.Controls.Add(textBox);
            }
        }

        private Control getLastControl(Control typeControl)
        {
            Control returnControl = null;
            foreach (Control control in panelPropsTypeCard.Controls)
            {
                if (control.GetType() == typeControl.GetType()) returnControl = control;
            }
            return returnControl;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxCardTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassTypeCard classTypeCard = new ClassTypeCard();
            int idTypeCard = classTypeCard.getIdByName(comboBoxCardTypes.Text);
            showCardProps(idTypeCard);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
            

            //ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
            ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();
            foreach (Control control in panelPropsTypeCard.Controls)
            {
                if (control.GetType() == (new TextBox()).GetType())
                {
                    int idTypeCard = Convert.ToInt32(control.Name);
                    classCardPropsValue.createValue(idTypeCard, ((TextBox)control).Text, textBoxFilePath.Text);
                }
            }
        }
    }
}
