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
    public partial class CreateUser : MaterialForm
    {
        public CreateUser()
        {
            InitializeComponent();

            DBF_Users.fillPost(comboBox1);
            materialSingleLineTextField3.MaxLength = 16;
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "" || materialSingleLineTextField2.Text == "" || materialSingleLineTextField3.Text == "" || materialSingleLineTextField4.Text == "" || materialSingleLineTextField5.Text == "" || materialSingleLineTextField6.Text == "" || comboBox1.Text == "")
                MessageBox.Show("Все поля обязательны для заполнения", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                materialSingleLineTextField3.Text = Regex.Replace(materialSingleLineTextField3.Text, @"(?:\+7|8)?(?:\()?(\d{3})(?:\))?(\d{3})(?:-)?(\d{2})(?:-)?(\d{2})", "+7($1)$2-$3-$4");
                materialSingleLineTextField3.Text = materialSingleLineTextField3.Text.Substring(0, 16);

                if (materialSingleLineTextField5.Text.Length >= 8)
                {

                    if (DBF_Users.validLog(materialSingleLineTextField6.Text))
                        DBF_Users.crtUser(materialSingleLineTextField6.Text, materialSingleLineTextField5.Text, materialSingleLineTextField1.Text, materialSingleLineTextField2.Text,
                                          materialSingleLineTextField3.Text, materialSingleLineTextField4.Text, Convert.ToInt32(comboBox1.Text));
                    else
                        MessageBox.Show("Пользователь с этим логином уже существует", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Длинна пароля должна ссоставлять не менее 8 символов.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CreateUser_Load(object sender, EventArgs e)
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
