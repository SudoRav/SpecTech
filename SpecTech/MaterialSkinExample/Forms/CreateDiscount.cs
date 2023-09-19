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
    public partial class CreateDiscount : MaterialForm
    {
        public CreateDiscount()
        {
            InitializeComponent();
        }

        private void CreateDiscount_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            DBF_Tech.setDiscount(Convert.ToInt32(StatTech.ID), Convert.ToInt32(materialSingleLineTextField1.Text));
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
