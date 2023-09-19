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
    public partial class DetailPost : MaterialForm
    {
        public DetailPost()
        {
            InitializeComponent();

            DBF_Users.LoadPost(dataGridView1);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            DBF_Users.delPost(dataGridView1);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            DBF_Users.LoadPost(dataGridView1);
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            new CreatePost().ShowDialog();
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            StatPost.ID_post = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            StatPost.postName= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            StatPost.access1 = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            StatPost.access2 = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            StatPost.access3 = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            StatPost.access4 = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            StatPost.access5 = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            StatPost.access6 = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            StatPost.salary  = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[8].Value);

            new DetailPost2().ShowDialog();
        }

        private void DetailPost_Load(object sender, EventArgs e)
        {

        }
    }
}
