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
    public partial class DetailPost2 : MaterialForm
    {
        public DetailPost2()
        {
            InitializeComponent();

            materialSingleLineTextField1.Text = StatPost.postName;
            materialCheckBox1.Checked = tof(StatPost.access1);
            materialCheckBox2.Checked = tof(StatPost.access2);
            materialCheckBox3.Checked = tof(StatPost.access3);
            materialCheckBox4.Checked = tof(StatPost.access4);
            materialCheckBox5.Checked = tof(StatPost.access5);
            materialCheckBox6.Checked = tof(StatPost.access6);
            materialSingleLineTextField2.Text = StatPost.salary.ToString();
        }

        private bool tof(string q)
        {
            if (q == "True" || q == "1")
                return true;
            else
                return false;
        }

        private void DetailPost2_Load(object sender, EventArgs e)
        {
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "" || materialSingleLineTextField2.Text == "")
                MessageBox.Show("Все поля обязательны для заполнения", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                    DBF_Users.updPost(StatPost.ID_post, materialSingleLineTextField1.Text, materialCheckBox1.Checked, materialCheckBox2.Checked, materialCheckBox3.Checked,
                                                    materialCheckBox4.Checked, materialCheckBox5.Checked, materialCheckBox6.Checked, Convert.ToInt32(materialSingleLineTextField2.Text));
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
