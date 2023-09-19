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
    public partial class JoinTechToLeas : MaterialForm
    {
        public JoinTechToLeas()
        {
            InitializeComponent();

            DBF_Tech.LoadTechNotAtt(dataGridView1);
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            DBF_Leas.crtAttached(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            DBF_Tech.LoadTechNotAtt(dataGridView1);
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void JoinTechToLeas_Load(object sender, EventArgs e)
        {

        }
    }
}
