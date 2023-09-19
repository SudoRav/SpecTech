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
    public partial class SetConnectionString : MaterialForm
    {
        public SetConnectionString()
        {
            InitializeComponent();

            materialSingleLineTextField1.Text = Properties.Settings.Default.connectionString;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text != Properties.Settings.Default.connectionString)
                if (MessageBox.Show("Изменение строки подключения БД, может привести к не корректной работе приложения. Продолжить?", "Редактирование строки подключения", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Properties.Settings.Default.connectionString = materialSingleLineTextField1.Text.ToString();
                    Properties.Settings.Default.Save();
                }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text != Properties.Settings.Default.connectionString)
                if (MessageBox.Show("Изменение строки подключения БД, может привести к не корректной работе приложения. Продолжить?", "Редактирование строки подключения", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Properties.Settings.Default.connectionString = materialSingleLineTextField1.Text.ToString();
                    Properties.Settings.Default.Save();
                    this.Close();
                }
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            materialSingleLineTextField1.Text = @"Data Source=locahost;Initial Catalog=SpecTech;Integrated Security=True";
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            materialSingleLineTextField1.Text = @"Data Source=.;AttachDbFileName=|DataDirectory|\Data\SpecTech.mdf;Initial Catalog=SpecTech;Integrated Security=True"; ;
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            materialSingleLineTextField1.Text = @"Data Source=DESKTOP-6ELC94T\SQLEXPRESS;Initial Catalog=SpecTech;Integrated Security=True";
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            materialSingleLineTextField1.Text = @"Data Source=localhost;Initial Catalog=SpecTech;Integrated Security=True";
        }
    }
}
