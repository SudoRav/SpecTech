using System;
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
    public partial class CreateTech : MaterialForm
    {
        public CreateTech()
        {
            InitializeComponent();

            DBF_Tech.fillType(comboBox1);
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
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
                    materialLabel1.Text = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "" || materialSingleLineTextField2.Text == "" || materialLabel1.Text == "" || textBox1.Text == "" || comboBox1.Text == "")
                MessageBox.Show("Все поля обязательны для заполнения", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DBF_Tech.crtTech(materialSingleLineTextField1.Text, Convert.ToInt32(comboBox1.Text), materialLabel1.Text, textBox1.Text, Convert.ToInt32(materialSingleLineTextField2.Text));
            }
        }

        private void CreateTech_Load(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
                e.Handled = true;
        }
    }
}
