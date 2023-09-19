﻿using System;
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
    public partial class DetailType : MaterialForm
    {
        public DetailType()
        {
            InitializeComponent();

            DBF_Tech.LoadType(dataGridView1);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            DBF_Tech.delType(dataGridView1);
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            //new CreateTech_Type().ShowDialog();
            //DBF_Tech.crtType(materialSingleLineTextField1.Text);
            new CreateType().ShowDialog();
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            DBF_Tech.LoadType(dataGridView1);
        }
    }
}
