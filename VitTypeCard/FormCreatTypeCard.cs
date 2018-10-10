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
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int lastElementNumber = int.Parse(panel1.Controls[panel1.Controls.Count - 1].Name.Split('_')[1]);
            textBoxNameAdd(lastElementNumber);
            comboBoxTypeAdd(lastElementNumber);
            buttonDeletePropAdd(lastElementNumber);
        }

        private void textBoxNameAdd(int lastElementNumber)
        {
            TextBox textBox = new TextBox();
            int x = panel1.Controls["textBoxName_" + (lastElementNumber).ToString()].Location.X;
            int y = panel1.Controls["textBoxName_" + (lastElementNumber).ToString()].Location.Y;
            textBox.Location = new Point(x, y + 20);
            textBox.Width = panel1.Controls["textBoxName_" + (lastElementNumber).ToString()].Width;
            textBox.Height = panel1.Controls["textBoxName_" + (lastElementNumber).ToString()].Height;
            textBox.Name = "textBoxName_" + (lastElementNumber + 1).ToString();
            //textBox.Text = textBox.Name;
            panel1.Controls.Add(textBox);
        }

        private void comboBoxTypeAdd(int lastElementNumber)
        {
            ComboBox comboBoxType = new ComboBox();
            int x = panel1.Controls["comboBoxType_" + (lastElementNumber).ToString()].Location.X;
            int y = panel1.Controls["comboBoxType_" + (lastElementNumber).ToString()].Location.Y;
            comboBoxType.Location = new Point(x, y + 20);
            comboBoxType.Width = panel1.Controls["comboBoxType_" + (lastElementNumber).ToString()].Width;
            comboBoxType.Height = panel1.Controls["comboBoxType_" + (lastElementNumber).ToString()].Height;
            comboBoxType.Name = "comboBoxType_" + (lastElementNumber + 1).ToString();
            //comboBoxType.Text = comboBoxType.Name;

            ComboBox.ObjectCollection items = comboBoxType_0.Items;
            foreach (object item in items)
            {
                comboBoxType.Items.Add(item);
            }

            panel1.Controls.Add(comboBoxType);
        }

        private void buttonDeletePropAdd(int lastElementNumber)
        {
            Button buttonDeleteProp = new Button();
            int x = panel1.Controls["buttonDeleteProp_" + (lastElementNumber).ToString()].Location.X;
            int y = panel1.Controls["buttonDeleteProp_" + (lastElementNumber).ToString()].Location.Y;
            buttonDeleteProp.Location = new Point(x, y + 20);
            buttonDeleteProp.ImageList = imageList1;
            buttonDeleteProp.ImageIndex = 1;
            buttonDeleteProp.Width = panel1.Controls["buttonDeleteProp_" + (lastElementNumber).ToString()].Width;
            buttonDeleteProp.Height = panel1.Controls["buttonDeleteProp_" + (lastElementNumber).ToString()].Height;
            buttonDeleteProp.Name = "buttonDeleteProp_" + (lastElementNumber + 1).ToString();
            buttonDeleteProp.Text = "";
            buttonDeleteProp.Click += new EventHandler(buttonDeleteProp_Click);
            panel1.Controls.Add(buttonDeleteProp);
        }

        private void buttonDeleteProp_Click(object sender, EventArgs e)
        {
            string elementNumber = ((Button)sender).Name.Split('_')[1];
            panel1.Controls["textBoxName_" + elementNumber].Dispose();
            panel1.Controls["comboBoxType_" + elementNumber].Dispose();
            panel1.Controls["buttonDeleteProp_" + elementNumber].Dispose();
            updatePosition();
        }

        private void updatePosition()
        {
            string buf = "";
            int groupHeght = panel1.Controls["textBoxName_0"].Height + panel1.Controls["textBoxName_0"].Location.Y;
            int positionIterator = panel1.Controls["textBoxName_0"].Location.Y;
            buf = panel1.Controls["textBoxName_0"].Name.Split('_')[1];
            foreach (Control control in panel1.Controls)
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
            Hide();
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

            foreach (Control control in panel1.Controls)
            {
                string number = control.Name.Split('_')[1];
                if (buf == number)
                {
                    continue;
                }

                buf = number;
                TextBox tb = (TextBox)panel1.Controls["textBoxName_" + number];
                string name = tb.Text;

                if (name == "")
                {
                    continue;
                }

                ComboBox cb = (ComboBox)panel1.Controls["comboBoxType_" + number];
                int typeValue = cb.SelectedIndex;

                //if (typeValue == "") continue;

                classTypeCardProps.add(name, typeValue, idType);
            }
        }
    }
}