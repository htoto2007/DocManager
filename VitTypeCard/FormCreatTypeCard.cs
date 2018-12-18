using System;
using System.Drawing;
using System.Windows.Forms;
using vitTypeCardProps;

namespace VitTypeCard
{
    public partial class FormCreatTypeCard : Form
    {
        //private int elementNumber = 0;
        private ClassTypeCard classTypeCard = new ClassTypeCard();

        private ClassTypeCardProps classTypeCardProps = new ClassTypeCardProps();

        public FormCreatTypeCard()
        {
            InitializeComponent();
            comboBoxType_0.Items.Clear();
            foreach (var prop in classTypeCard.typeProp) {
                comboBoxType_0.Items.Add(prop);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int lastElementNumber = int.Parse(panelProps.Controls[panelProps.Controls.Count - 1].Name.Split('_')[1]);
            textBoxNameAdd(lastElementNumber);
            comboBoxTypeAdd(lastElementNumber);
            buttonDeletePropAdd(lastElementNumber);
        }

        private void textBoxNameAdd(int lastElementNumber)
        {
            TextBox textBox = new TextBox();
            int x = panelProps.Controls["textBoxName_" + (lastElementNumber).ToString()].Location.X;
            int y = 5 + panelProps.Controls["textBoxName_" + (lastElementNumber).ToString()].Location.Y;
            textBox.Location = new Point(x, y + 20);
            textBox.Width = panelProps.Controls["textBoxName_" + (lastElementNumber).ToString()].Width;
            textBox.Height = panelProps.Controls["textBoxName_" + (lastElementNumber).ToString()].Height;
            textBox.Name = "textBoxName_" + (lastElementNumber + 1).ToString();
            //textBox.Text = textBox.Name;
            panelProps.Controls.Add(textBox);
        }

        private void comboBoxTypeAdd(int lastElementNumber)
        {
            ComboBox comboBoxType = new ComboBox();
            int x = panelProps.Controls["comboBoxType_" + (lastElementNumber).ToString()].Location.X;
            int y = 5 + panelProps.Controls["comboBoxType_" + (lastElementNumber).ToString()].Location.Y;
            comboBoxType.Location = new Point(x, y + 20);
            comboBoxType.Width = panelProps.Controls["comboBoxType_" + (lastElementNumber).ToString()].Width;
            comboBoxType.Height = panelProps.Controls["comboBoxType_" + (lastElementNumber).ToString()].Height;
            comboBoxType.Name = "comboBoxType_" + (lastElementNumber + 1).ToString();
            //comboBoxType.Text = comboBoxType.Name;

            ComboBox.ObjectCollection items = comboBoxType_0.Items;
            foreach (object item in items)
            {
                comboBoxType.Items.Add(item);
            }

            panelProps.Controls.Add(comboBoxType);
        }

        private void buttonDeletePropAdd(int lastElementNumber)
        {
            Button buttonDeleteProp = new Button();
            int x = panelProps.Controls["buttonDeleteProp_" + (lastElementNumber).ToString()].Location.X;
            int y = 5 + panelProps.Controls["buttonDeleteProp_" + (lastElementNumber).ToString()].Location.Y;
            buttonDeleteProp.Location = new Point(x, y + 20);
            buttonDeleteProp.ImageList = imageList1;
            buttonDeleteProp.ImageIndex = 1;
            buttonDeleteProp.Width = panelProps.Controls["buttonDeleteProp_" + (lastElementNumber).ToString()].Width;
            buttonDeleteProp.Height = panelProps.Controls["buttonDeleteProp_" + (lastElementNumber).ToString()].Height;
            buttonDeleteProp.Name = "buttonDeleteProp_" + (lastElementNumber + 1).ToString();
            buttonDeleteProp.Text = "";
            buttonDeleteProp.Click += new EventHandler(buttonDeleteProp_Click);
            panelProps.Controls.Add(buttonDeleteProp);
        }

        private void buttonDeleteProp_Click(object sender, EventArgs e)
        {
            string elementNumber = ((Button)sender).Name.Split('_')[1];
            panelProps.Controls["textBoxName_" + elementNumber].Dispose();
            panelProps.Controls["comboBoxType_" + elementNumber].Dispose();
            panelProps.Controls["buttonDeleteProp_" + elementNumber].Dispose();
            updatePosition();
        }

        private void updatePosition()
        {
            string buf = "";
            int groupHeght = panelProps.Controls["textBoxName_0"].Height + panelProps.Controls["textBoxName_0"].Location.Y;
            int positionIterator = panelProps.Controls["textBoxName_0"].Location.Y;
            buf = panelProps.Controls["textBoxName_0"].Name.Split('_')[1];
            foreach (Control control in panelProps.Controls)
            {
                if (buf != control.Name.Split('_')[1])
                {
                    buf = control.Name.Split('_')[1];
                    positionIterator += 20;
                    control.Location = new Point(control.Location.X, positionIterator);
                }
                else
                {
                    control.Location = new Point(control.Location.X, positionIterator);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idType = 0;
            string buf = "";

            if (textBox1.Text == "")
            {
                MessageBox.Show("Карточка должна иметь имя.");
                return;
            }

            idType = classTypeCard.add(textBox1.Text);
            if (idType == 0)
            {
                MessageBox.Show("Не получилось создать тип карточки.");
                return;
            }

            foreach (Control control in panelProps.Controls)
            {
                string number = control.Name.Split('_')[1];
                if (buf == number)
                {
                    continue;
                }

                buf = number;
                TextBox tb = (TextBox)panelProps.Controls["textBoxName_" + number];
                string name = tb.Text;

                if (name == "")
                {
                    continue;
                }

                ComboBox cb = (ComboBox)panelProps.Controls["comboBoxType_" + number];
                int typeValue = cb.SelectedIndex;

                //if (typeValue == "") continue;

                classTypeCardProps.add(name, typeValue, idType);
            }
        }
    }
}