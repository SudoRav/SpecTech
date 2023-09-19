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
    public partial class Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private int colorSchemeIndex;
        public Main()
        {
            InitializeComponent();
            this.Text = StatUser.F + " " + StatUser.I;

            EnableTab(materialTabControl1.TabPages[0], false);
            EnableTab(materialTabControl1.TabPages[1], false);
            EnableTab(materialTabControl1.TabPages[2], false);
            EnableTab(materialTabControl1.TabPages[3], false);
            EnableTab(materialTabControl1.TabPages[4], false);
            EnableTab(materialTabControl1.TabPages[5], false);
            materialTabControl1.TabPages[0].Text = "";
            materialTabControl1.TabPages[1].Text = "";
            materialTabControl1.TabPages[2].Text = "";
            materialTabControl1.TabPages[3].Text = "";
            materialTabControl1.TabPages[4].Text = "";
            materialTabControl1.TabPages[5].Text = "";
            if (Convert.ToBoolean(StatUser.access1))
            {
                EnableTab(materialTabControl1.TabPages[0], true);
                materialTabControl1.TabPages[0].Text = "Техника";
            }
            if (Convert.ToBoolean(StatUser.access2))
            {
                EnableTab(materialTabControl1.TabPages[1], true);
                materialTabControl1.TabPages[1].Text = "Аренды";
            }
            if (Convert.ToBoolean(StatUser.access3))
            {
                EnableTab(materialTabControl1.TabPages[2], true);
                materialTabControl1.TabPages[2].Text = "Сотрудники";
            }
            if (Convert.ToBoolean(StatUser.access4))
            {
                EnableTab(materialTabControl1.TabPages[3], true);
                materialTabControl1.TabPages[3].Text = "История";
            }
            if (Convert.ToBoolean(StatUser.access5))
            {
                EnableTab(materialTabControl1.TabPages[4], true);
                materialTabControl1.TabPages[4].Text = "Личный кабинет";
            }
            if (Convert.ToBoolean(StatUser.access6))
            {
                EnableTab(materialTabControl1.TabPages[5], true);
                materialTabControl1.TabPages[5].Text = "Настройки";
            }

            //EnableTab(materialTabControl1.TabPages[0], Convert.ToBoolean(StatUser.access1));
            //EnableTab(materialTabControl1.TabPages[1], Convert.ToBoolean(StatUser.access2));
            //EnableTab(materialTabControl1.TabPages[2], Convert.ToBoolean(StatUser.access3));
            //EnableTab(materialTabControl1.TabPages[3], Convert.ToBoolean(StatUser.access4));
            //EnableTab(materialTabControl1.TabPages[4], Convert.ToBoolean(StatUser.access5));
            //EnableTab(materialTabControl1.TabPages[5], Convert.ToBoolean(StatUser.access6));

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            switch(Properties.Settings.Default.Theme.ToString())
            {
                case "LIGHT": materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; break;
                case "DARK": materialSkinManager.Theme = MaterialSkinManager.Themes.DARK; break;
                default: materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; break;
            }
            colorSchemeIndex = Properties.Settings.Default.ColorScheme;
            switchColorScheme();

            materialLabel1.Text = StatUser.F;
            materialLabel2.Text = StatUser.I;
            materialLabel3.Text = StatUser.phone;
            materialLabel4.Text = StatUser.email;
            materialLabel5.Text = StatUser.post;

            DBF_Tech.fillType(comboBox1);

            DBF_Tech.LoadTech(dataGridView1);

            DBF_Leas.LoadLeas(dataGridView2);

            DBF_Users.fillPost(comboBox2);

            DBF_Users.LoadUsers(dataGridView3);

            DBF.LoadHistory(dataGridView4);

            DBF_Tech.fillGisto(chart1);
            chart1.Visible = false;
        }

        static void EnableTab(TabPage page, bool en)
        {
            foreach (Control ctl in page.Controls) ctl.Enabled = en;
            foreach (Control ctl in page.Controls) ctl.Visible = en;
        }

        private void materialListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            new SetConnectionString().Show();
        }

        private void materialTabSelector2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBF_Tech.findTechType(dataGridView1, comboBox1.Text);
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage_settings_Click(object sender, EventArgs e)
        {

        }

        private void pagetech_btn_create_Click(object sender, EventArgs e)
        {
            new CreateTech().ShowDialog();
        }

        private void pagetech_btn_delete_Click(object sender, EventArgs e)
        {
            DBF_Tech.delTech2(dataGridView1);
            DBF_Tech.LoadTech(dataGridView1);
        }

        private void pagetech_btn_detail_Click(object sender, EventArgs e)
        {
            DBF_Tech.detail(dataGridView1);
            DBF_Tech.LoadTech(dataGridView1);
        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DBF_Tech.findTechName(dataGridView1, textBox1.Text);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            DBF_Tech.fillType(comboBox1);
            DBF_Tech.LoadTech(dataGridView1);
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            new DetailType().ShowDialog();
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            DBF_Leas.LoadLeas(dataGridView2);
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            new DetailPost().Show();
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            DBF_Leas.detail(dataGridView2);
            //new DetailLeas().ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton7_Click(object sender, EventArgs e)
        {
            DBF_Leas.delLeas(dataGridView2);
        }

        private void materialRaisedButton8_Click(object sender, EventArgs e)
        {
            new CreateLeas().ShowDialog();
        }

        private void materialRaisedButton9_Click(object sender, EventArgs e)
        {
            DBF_Users.GetInfoUser(dataGridView3);
            new DetailUser().ShowDialog();
        }

        private void materialRaisedButton10_Click(object sender, EventArgs e)
        {
            DBF_Users.delUser(dataGridView3);
        }

        private void materialRaisedButton4_Click_1(object sender, EventArgs e)
        {
            //comboBox2.SelectedIndex = -1;
            DBF_Users.fillPost(comboBox2);
            DBF_Users.LoadUsers(dataGridView3);
        }

        private void materialRaisedButton12_Click(object sender, EventArgs e)
        {
            new DetailPost().ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBF_Users.findUserPost(dataGridView3, comboBox2.Text);
        }

        private void materialRaisedButton13_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;
            Properties.Settings.Default.Theme = materialSkinManager.Theme.ToString();
            Properties.Settings.Default.Save();
        }

        private void materialRaisedButton14_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2) colorSchemeIndex = 0;

            switchColorScheme();

            Properties.Settings.Default.ColorScheme = colorSchemeIndex;
            Properties.Settings.Default.Save();
        }
        private void switchColorScheme()
        {
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;
                case 1:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;
                case 2:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;
            }
        }
        private void materialRaisedButton11_Click(object sender, EventArgs e)
        {
            new CreateUser().ShowDialog();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            DBF_Users.findUserFI(dataGridView3, textBox2.Text);
        }

        private void materialFlatButton1_Click_2(object sender, EventArgs e)
        {
            this.Close();
            new Auth().Show();
        }

        private void materialRaisedButton15_Click(object sender, EventArgs e)
        {
            DBF.LoadHistory(dataGridView4);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton16_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Создать отчёт можно, только при условии лицензионного ворда.\nПродолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                DBF_Tech.CreateWordFileTech();
        }

        private void materialRaisedButton17_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Создать отчёт можно, только при условии лицензионного ворда.\nПродолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                DBF_Leas.CreateWordFileLeas();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        bool gis_vis = false;
        private void materialRaisedButton18_Click(object sender, EventArgs e)
        {
            if(gis_vis == false)
            {
                DBF_Tech.fillGisto(chart1);
                chart1.Visible = true;

                gis_vis = true;
            }
            else
            {
                chart1.Visible = false;

                gis_vis = false;
            }
        }

        private void tabPage_users_Click(object sender, EventArgs e)
        {

        }

        private void tabPage_tech_Click(object sender, EventArgs e)
        {

        }
    }
}
