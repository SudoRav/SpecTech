using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;

namespace MaterialSkinExample
{
    public partial class DetailTech : MaterialForm
    {
        public DetailTech()
        {
            InitializeComponent();

            load();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {

        }

        private void DetailTech_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton3_Click_1(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton4_Click_1(object sender, EventArgs e)
        {
            load();
        }

        private void load()
        {
            materialSingleLineTextField1.Text = StatTech.name;
            this.Text = StatTech.name;

            try { picture_tech.Image = Image.FromStream(StatTech.photo); }
            catch { }

            DBF_Tech.LoadCharacts(dataGridView1);
            textBox1.Text = StatTech.desc;

            materialLabel1.Text = StatTech.type;
            DBF_Tech.fillType(comboBox1);
            comboBox1.SelectedItem = StatTech.typeID;

            if (StatTech.discount != 0)
            {
                materialLabel9.Text = "Цена -" + StatTech.discount + "%";

                double sallery = StatTech.price;
                sallery = sallery * (100 - StatTech.discount) / 100;

                materialSingleLineTextField2.Text = sallery.ToString();
            }
            else
                materialSingleLineTextField2.Text = StatTech.price.ToString();

            if (StatTech.status_leas == "True")
                materialLabel5.Text = "В аренде";
            else
                materialLabel5.Text = "На стоянке";
            if (StatTech.status_rep == "True")
                materialLabel6.Text = "Требуется ремонт";
            else
                materialLabel6.Text = "Ремонт не требуется";
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            DBF_Tech.delTech();
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            DBF_Tech.delCharacts(dataGridView1);
        }

        private void materialRaisedButton3_Click_2(object sender, EventArgs e)
        {
            new CreateCharact().ShowDialog();
        }

        private void materialRaisedButton5_Click_1(object sender, EventArgs e)
        {
            DBF_Tech.delCharacts(dataGridView1);
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "" || materialSingleLineTextField2.Text == "" || materialLabel3.Text == "" || textBox1.Text == "" || comboBox1.Text == "")
                MessageBox.Show("Все поля обязательны для заполнения", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                //if (materialLabel3.Text != "")
                    DBF_Tech.updTech(materialSingleLineTextField1.Text, Convert.ToInt32(comboBox1.Text), materialLabel3.Text, textBox1.Text, Convert.ToInt32(materialSingleLineTextField2.Text));
                //else
                //    MessageBox.Show("Путь к фото обязателен для заполнения.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialLabel11_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton7_Click(object sender, EventArgs e)
        {
            new CreateDiscount().Show();
        }

        private void materialRaisedButton8_Click(object sender, EventArgs e)
        {
            new CreateRep().Show();
        }

        private void materialRaisedButton9_Click(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picture_tech.Image = new Bitmap(ofd.FileName);
                    materialLabel3.Text = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void materialSingleLineTextField2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
                e.Handled = true;
        }
    }
}
