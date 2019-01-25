using System;
using System.Drawing;
using System.Windows.Forms;
using VitDBConnect;
using VitFiles;
using VitTypeCard;
using vitTypeCardProps;

namespace VitTree
{
    public partial class FormFiles : Form
    {
        public bool isCheck = true;
        private ClassDBConnect classDB = new ClassDBConnect();
        private ClassFiles classFiles = new ClassFiles();

        private ClassTypeCard classTypeCard = new ClassTypeCard();
        private ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();

        /// <summary>
        /// Форма добавления и изменения свойств документа
        /// </summary>
        public FormFiles()
        {
            InitializeComponent();
            comboBox1.Text = "Пустая";
        }

        private void addFieldsProps(ClassTypeCardProps.TypeCardProps[] typeCardProps)
        {
            int x = 10;
            int y = 10;
            panel1.Controls.Clear();
            Label labelPre = new Label
            {
                Text = typeCardProps.GetLength(0).ToString(),
                Location = new Point(20, 20)
            };
            foreach (ClassTypeCardProps.TypeCardProps typeCardProp in typeCardProps)
            {
                Label label = new Label
                {
                    Text = typeCardProp.name,
                    Width = 200,
                    Location = new Point(x, y)
                };

                if (typeCardProp.typeValue == 0) // строуовые
                {
                    TextBox comtrol = new TextBox
                    {
                        Name = typeCardProp.typeValue + "_" + typeCardProp.id.ToString(),
                        Location = new Point(x + 30 + label.Width, y)
                    };
                    panel1.Controls.Add(comtrol);
                }
                if (typeCardProp.typeValue == 1) // числовые
                {
                    NumericUpDown control = new NumericUpDown
                    {
                        Name = typeCardProp.typeValue + "_" + typeCardProp.id.ToString(),
                        Location = new Point(x + 30 + label.Width, y)
                    };
                    panel1.Controls.Add(control);
                }
                if (typeCardProp.typeValue == 2) // дата
                {
                    MaskedTextBox control = new MaskedTextBox
                    {
                        Mask = "00/00/0000",
                        Name = typeCardProp.typeValue + "_" + typeCardProp.id.ToString(),
                        Location = new Point(x + 30 + label.Width, y)
                    };
                    panel1.Controls.Add(control);
                }
                if (typeCardProp.typeValue == 3) // дата и время
                {
                    MaskedTextBox control = new MaskedTextBox
                    {
                        Mask = "00/00/0000 00:00",
                        Name = typeCardProp.typeValue + "_" + typeCardProp.id.ToString(),
                        Location = new Point(x + 30 + label.Width, y)
                    };
                    panel1.Controls.Add(control);
                }
                if (typeCardProp.typeValue == 4) // логический
                {
                    CheckBox control = new CheckBox
                    {
                        Name = typeCardProp.typeValue + "_" + typeCardProp.id.ToString(),
                        Location = new Point(x + 30 + label.Width, y)
                    };
                    panel1.Controls.Add(control);
                }

                panel1.Controls.Add(label);
                y += 30;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxNameFile.Text = "";
            comboBox1.Text = "";
            panel1.Controls.Clear();
            Hide();
        }

        private void buttonCreateFile_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(isCheck.ToString());
            if (isCheck == false)
            {
                Hide();
                return;
            }
            if (checkField() == true)
            {
                Hide();
            }
            //MessageBox.Show("");
        }

        private bool checkField()
        {
            /*
            string str = textBoxNameFile.Text.Replace(" ", "");
            if (str == "")
            {
                MessageBox.Show("Название не может состоять из одних пробелов или быть пустым!");
                return false;
            }
            */
            textBoxNameFile.Text.TrimEnd(' ');
            textBoxNameFile.Text.TrimStart(' ');
            int id = classTypeCard.getIdByName(comboBox1.Text.ToString());
            if (id < 1)
            {
                MessageBox.Show("Не выбран тип карточки!");
                return false;
            }
            return true;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ClassTypeCard.TypeCardCollection[] typeCardCollections = classTypeCard.getAllInfo();
            comboBox1.Items.Clear();

            foreach (ClassTypeCard.TypeCardCollection typeCardCollection in typeCardCollections)
            {
                comboBox1.Items.Add(typeCardCollection.namne);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int id = classTypeCard.getIdByName(comboBox1.SelectedItem.ToString());
            ClassTypeCardProps.TypeCardProps[] typeCardProps = classTypeCardProps.getInfoByIdType(id);
            addFieldsProps(typeCardProps);
        }
    }
}