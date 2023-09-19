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
    public partial class CreateLeas : MaterialForm
    {
        public CreateLeas()
        {
            InitializeComponent();

            DBF_Users.fillUsers(comboBox1);
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        private void CreateLeas_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "" || textBox1.Text == "" || comboBox1.Text == "")
                MessageBox.Show("Все поля обязательны для заполнения", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                string dt_st = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
                string dt_fn = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString();
                DBF_Leas.crtLeas(Convert.ToInt32(comboBox1.Text), textBox1.Text, dt_st, dt_fn, materialSingleLineTextField1.Text);
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if(!Char.IsDigit(number) && number != 8)
                e.Handled = true;
        }
    }
}
