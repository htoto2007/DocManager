﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitNotifyMessage;
using VitTypeCard;
using vitTypeCardProps;

namespace VitCardPropsValue
{
    public partial class FormFileCard : Form
    {
        public FormFileCard()
        {
            InitializeComponent();
            ClassTypeCard classTypeCard = new ClassTypeCard();
            ClassTypeCard.TypeCardCollection[] typeCardCollections = classTypeCard.getAllInfo();
            comboBoxTypeCard.Items.Clear();
            foreach(var typeCardCollection in typeCardCollections)
            {
                comboBoxTypeCard.Items.Add(typeCardCollection.namne);
            }
        }

        private void comboBoxTypeCard_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ClassTypeCard classTypeCard = new ClassTypeCard();
            var idTypeCard = classTypeCard.getIdByName(comboBox.Text);
            ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();
            ClassTypeCardProps.TypeCardProps[] typeCardProps = classTypeCardProps.getInfoByIdType(idTypeCard);
            panelCardProps.Controls.Clear();


            int counter = 0;
            foreach (var typeCardProp in typeCardProps)
            {
                Label label = new Label();
                label.Text = typeCardProp.name;
                label.Location = new Point(5, 5 + counter);
                panelCardProps.Controls.Add(label);

                MaskedTextBox maskedTextBox;
                TextBox textBox;
                CheckBox checkBox;
                RichTextBox richTextBox;
                switch (typeCardProp.typeValue)
                {
                    case 0:
                        textBox = new TextBox();
                        textBox.Name = "tb_" + typeCardProp.id.ToString();
                        textBox.Size = new Size(300, Height);
                        textBox.Location = new Point(300, 5 + counter);
                        panelCardProps.Controls.Add(textBox);
                        counter += textBox.Height;
                        break;
                    case 1:
                        maskedTextBox = new MaskedTextBox("0");
                        maskedTextBox.Name = "tb_" + typeCardProp.id.ToString();
                        maskedTextBox.Size = new Size(300, Height);
                        maskedTextBox.Location = new Point(300, 5 + counter);
                        panelCardProps.Controls.Add(maskedTextBox);
                        counter += maskedTextBox.Height;
                        break;
                    case 2:
                        maskedTextBox = new MaskedTextBox("00/00/0000");
                        maskedTextBox.Name = "tb_" + typeCardProp.id.ToString();
                        maskedTextBox.Size = new Size(300, Height);
                        maskedTextBox.Location = new Point(300, 5 + counter);
                        panelCardProps.Controls.Add(maskedTextBox);
                        counter += maskedTextBox.Height;
                        break;
                    case 3:
                        maskedTextBox = new MaskedTextBox("00/00/0000 90:00");
                        maskedTextBox.Name = "tb_" + typeCardProp.id.ToString();
                        maskedTextBox.Size = new Size(300, Height);
                        maskedTextBox.Location = new Point(300, 5 + counter);
                        panelCardProps.Controls.Add(maskedTextBox);
                        counter += maskedTextBox.Height;
                        break;
                    case 4:
                        checkBox = new CheckBox();
                        checkBox.Name = "tb_" + typeCardProp.id.ToString();
                        checkBox.Location = new Point(300, 5 + counter);
                        panelCardProps.Controls.Add(checkBox);
                        counter += checkBox.Height;
                        break;
                    case 5:
                        richTextBox = new RichTextBox();
                        richTextBox.Name = "tb_" + typeCardProp.id.ToString();
                        richTextBox.Size = new Size(300, 200);
                        richTextBox.Multiline = true;
                        richTextBox.Location = new Point(300, 5 + counter);
                        panelCardProps.Controls.Add(richTextBox);
                        counter += richTextBox.Height;
                        break;
                    default:
                        ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                        classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не правильный номер типа значекния!");
                        break;
                }
            }
        }

        public struct CardPropCollection
        {
            public string text;
            public int idProp;
        }

        public CardPropCollection getValueByControl(Control control)
        {
            MaskedTextBox maskedTextBox = null;
            TextBox textBox = null;
            CheckBox checkBox = null;
            RichTextBox richTextBox = null;

            CardPropCollection cardPropCollection = new CardPropCollection();

            if(control.GetType() == maskedTextBox.GetType())
            {
                maskedTextBox = (MaskedTextBox)control;
                cardPropCollection.text = maskedTextBox.Text;
                cardPropCollection.idProp = Convert.ToInt32(maskedTextBox.Name.Split('_')[1]);
            }

            if (control.GetType() == textBox.GetType())
            {
                textBox = (TextBox)control;
                cardPropCollection.text = textBox.Text;
                cardPropCollection.idProp = Convert.ToInt32(textBox.Name.Split('_')[1]);
            }

            if (control.GetType() == checkBox.GetType())
            {
                checkBox = (CheckBox)control;
                cardPropCollection.text = checkBox.Checked.ToString();
                cardPropCollection.idProp = Convert.ToInt32(maskedTextBox.Name.Split('_')[1]);
            }

            if (control.GetType() == richTextBox.GetType())
            {
                richTextBox = (RichTextBox)control;
                cardPropCollection.text = richTextBox.Text;
                cardPropCollection.idProp = Convert.ToInt32(richTextBox.Name.Split('_')[1]);
            }

            return cardPropCollection;
        }
    }
}
