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
    public partial class DetailLeas : MaterialForm
    {
        public DetailLeas()
        {
            InitializeComponent();

            DBF_Leas.LoadAttached(dataGridView1, StatLeas.ID.ToString());
            materialLabel1.Text = StatLeas.FI_user;
            materialLabel2.Text = StatLeas.address;
            materialLabel3.Text = StatLeas.summ;

        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            DBF_Leas.delLeas2();
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            DBF_Leas.LoadAttached(dataGridView1, StatLeas.ID.ToString());
            materialLabel1.Text = StatLeas.FI_user;
            materialLabel2.Text = StatLeas.address;
            materialLabel3.Text = StatLeas.summ;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            DBF_Leas.delAttached(dataGridView1);
            DBF_Leas.LoadAttached(dataGridView1, StatLeas.ID.ToString());
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            new JoinTechToLeas().ShowDialog();
        }

        private void materialRaisedButton7_Click(object sender, EventArgs e)
        {
            DBF_Tech.detail(dataGridView1);
        }

        private void DetailLeas_Load(object sender, EventArgs e)
        {

        }
    }
}
