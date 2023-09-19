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
    public partial class CreatePost : MaterialForm
    {
        public CreatePost()
        {
            InitializeComponent();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "" || materialSingleLineTextField2.Text == "")
                MessageBox.Show("Все поля обязательны для заполнения", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DBF_Users.crtPost(materialSingleLineTextField1.Text, materialCheckBox1.Checked, materialCheckBox2.Checked, materialCheckBox3.Checked,
                                                                                materialCheckBox4.Checked, materialCheckBox5.Checked, materialCheckBox6.Checked, Convert.ToInt32(materialSingleLineTextField2.Text));
            }

        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(materialCheckBox1.Checked.ToString());
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialSingleLineTextField2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
                e.Handled = true;
        }
    }
}
