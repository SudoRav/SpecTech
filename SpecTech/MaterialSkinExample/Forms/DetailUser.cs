using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;

namespace MaterialSkinExample
{
    public partial class DetailUser : MaterialForm
    {
        public DetailUser()
        {
            InitializeComponent();

            materialSingleLineTextField1.Text = StatUser_sel.F;
            materialSingleLineTextField2.Text = StatUser_sel.I;
            materialSingleLineTextField3.Text = StatUser_sel.phone;
            materialSingleLineTextField4.Text = StatUser_sel.email;
            materialLabel5.Text = StatUser_sel.post;
            DBF_Users.fillPost(comboBox1);

            //materialSingleLineTextField3.MaxLength = 16;
        }
        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            StatUser_sel.query = "education";
            new DetailUser_All().ShowDialog();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            StatUser_sel.query = "absence";
            new DetailUser_All().ShowDialog();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            StatUser_sel.query = "pay";
            new DetailUser_All().ShowDialog();
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            StatUser_sel.query = "prize";
            new DetailUser_All().ShowDialog();
        }

        private void materialRaisedButton8_Click(object sender, EventArgs e)
        {
            DBF_Users.delUser2();
        }

        private void DetailUser_Load(object sender, EventArgs e)
        {

        }

        private void materialLabel5_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            materialSingleLineTextField1.Text = StatUser_sel.F;
            materialSingleLineTextField2.Text = StatUser_sel.I;
            materialSingleLineTextField3.Text = StatUser_sel.phone;
            materialSingleLineTextField4.Text = StatUser_sel.email;
            materialLabel5.Text = StatUser_sel.post;
        }

        private void materialRaisedButton7_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "" || materialSingleLineTextField2.Text == "" || materialSingleLineTextField3.Text == "" || materialSingleLineTextField4.Text == "" || comboBox1.Text == "")
                MessageBox.Show("Все поля обязательны для заполнения", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                materialSingleLineTextField3.Text = Regex.Replace(materialSingleLineTextField3.Text, @"(?:\+7|8)?(?:\()?(\d{3})(?:\))?(\d{3})(?:-)?(\d{2})(?:-)?(\d{2})", "+7($1)$2-$3-$4");

                if (materialSingleLineTextField3.Text.Length == 16)
                {
                    DBF_Users.updUser(materialSingleLineTextField1.Text, materialSingleLineTextField2.Text, materialSingleLineTextField3.Text,
                    materialSingleLineTextField4.Text, Convert.ToInt32(comboBox1.Text));
                }
                else
                    MessageBox.Show("Длинна номера телефона должна быть равна 16 символам.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void materialSingleLineTextField3_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
                e.Handled = true;
        }
    }
}
