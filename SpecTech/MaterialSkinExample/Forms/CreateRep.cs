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
    public partial class CreateRep : MaterialForm
    {
        public CreateRep()
        {
            InitializeComponent();


        }

        private void CreateRep_Load(object sender, EventArgs e)
        {
            textBox1.Text = StatTech.rep_text;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            DBF_Tech.crtRep(StatTech.ID, textBox1.Text);
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            DBF_Tech.delRep(StatTech.ID);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
