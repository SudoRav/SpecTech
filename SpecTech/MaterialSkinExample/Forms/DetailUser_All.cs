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
    public partial class DetailUser_All : MaterialForm
    {
        public DetailUser_All()
        {
            InitializeComponent();

            switch(StatUser_sel.query)
            {
                case "education": DBF_Users.LoadQuery(dataGridView1, StatUser_sel.query, StatUser_sel.ID); break;
                case "absence":   DBF_Users.LoadQuery(dataGridView1, StatUser_sel.query, StatUser_sel.ID); break;
                case "pay":       DBF_Users.LoadQuery(dataGridView1, StatUser_sel.query, StatUser_sel.ID); break;
                case "prize":     DBF_Users.LoadQuery(dataGridView1, StatUser_sel.query, StatUser_sel.ID); break;
            }
        }

        private void DetailUser_Education_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            DBF_Users.delUserElement(dataGridView1, StatUser_sel.query);

            switch (StatUser_sel.query)
            {
                case "education": DBF_Users.LoadQuery(dataGridView1, StatUser_sel.query, StatUser_sel.ID); break;
                case "absence": DBF_Users.LoadQuery(dataGridView1, StatUser_sel.query, StatUser_sel.ID); break;
                case "pay": DBF_Users.LoadQuery(dataGridView1, StatUser_sel.query, StatUser_sel.ID); break;
                case "prize": DBF_Users.LoadQuery(dataGridView1, StatUser_sel.query, StatUser_sel.ID); break;
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            switch (StatUser_sel.query)
            {
                case "education": new CreateUser_Education().ShowDialog(); break;
                case "absence": new CreateUser_Absence().ShowDialog(); break;
                case "pay": new CreateUser_Pay().ShowDialog(); break;
                case "prize": new CreateUser_Prize().ShowDialog(); break;
            }
        }
    }
}
