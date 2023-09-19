using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;
using System.Security.Cryptography;

namespace MaterialSkinExample
{
    class DBF
    {
        static SqlConnection con;
        static SqlCommand cmd;

        static public void TTTT()
        {
            Con();
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        static void Con()
        {
            try
            {
                con = new SqlConnection(Properties.Settings.Default.connectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                if(MessageBox.Show("Ошибка подключения Базы Данных. Перейти к строке подключения?", "Ошибка!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    new SetConnectionString().Show();
                else
                    MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        static public bool Login(string log, string pass, Form frm)
        {
            Con();
            try
            {
                DataTable tbl = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd = new SqlCommand($"SELECT * FROM users WHERE log = '{log}' AND pass = '{CalculateMD5Hash(pass)}'", con);
                adapter.SelectCommand = cmd;
                adapter.Fill(tbl);

                if (tbl.Rows.Count != 0)
                {
                    frm.Hide();

                    StatUser.ID = Convert.ToInt32(new SqlCommand($"SELECT ID FROM users WHERE log = '{log}'", con).ExecuteScalar());
                    StatUser.login = new SqlCommand($"SELECT log FROM users             WHERE log = '{log}'", con).ExecuteScalar().ToString();
                    StatUser.password = new SqlCommand($"SELECT pass FROM users         WHERE log = '{log}'", con).ExecuteScalar().ToString();
                    StatUser.F = new SqlCommand($"SELECT F FROM users                   WHERE log = '{log}'", con).ExecuteScalar().ToString();
                    StatUser.I = new SqlCommand($"SELECT I FROM users                   WHERE log = '{log}'", con).ExecuteScalar().ToString();
                    StatUser.email = new SqlCommand($"SELECT email FROM users           WHERE log = '{log}'", con).ExecuteScalar().ToString();
                    StatUser.phone = new SqlCommand($"SELECT phone FROM users           WHERE log = '{log}'", con).ExecuteScalar().ToString();

                    cmd = new SqlCommand($"SELECT post.ID FROM users " +
                                         $"JOIN post ON post.ID = users.ID_post " +
                                         $"WHERE users.log = '{log}'", con);
                    StatUser.ID_post = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand($"SELECT post.postName FROM users " +
                                         $"JOIN post ON post.ID = users.ID_post " +
                                         $"WHERE users.log = '{log}'", con);
                    StatUser.post = cmd.ExecuteScalar().ToString();

                    StatUser.access1 = new SqlCommand($"SELECT access1 FROM post WHERE ID = '{StatUser.ID_post}'", con).ExecuteScalar().ToString();
                    StatUser.access2 = new SqlCommand($"SELECT access2 FROM post WHERE ID = '{StatUser.ID_post}'", con).ExecuteScalar().ToString();
                    StatUser.access3 = new SqlCommand($"SELECT access3 FROM post WHERE ID = '{StatUser.ID_post}'", con).ExecuteScalar().ToString();
                    StatUser.access4 = new SqlCommand($"SELECT access4 FROM post WHERE ID = '{StatUser.ID_post}'", con).ExecuteScalar().ToString();
                    StatUser.access5 = new SqlCommand($"SELECT access5 FROM post WHERE ID = '{StatUser.ID_post}'", con).ExecuteScalar().ToString();
                    StatUser.access6 = new SqlCommand($"SELECT access6 FROM post WHERE ID = '{StatUser.ID_post}'", con).ExecuteScalar().ToString();

                    //new Main().Show();
                    con.Close();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally { }
        }
        static public string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            return sb.ToString();
        }
        static public void LoadHistory(DataGridView grid)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, [objectID] as 'ID объекта', [operation] as 'Операция', [date] as 'Дата' FROM history", con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                grid.DataSource = data.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
