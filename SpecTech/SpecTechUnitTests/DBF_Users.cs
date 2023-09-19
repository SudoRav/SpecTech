using SpecTechUnitTests;

using System;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace MaterialSkinExample
{
    class DBF_Users
    {
        static SqlConnection con;
        static SqlCommand cmd;

        static private void Con()
        {
            try
            {
                //con = new SqlConnection(Properties.Settings.Default.connectionString);
                con = new SqlConnection(CS.connectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                //if(MessageBox.Show("Ошибка подключения Базы Данных. Перейти к строке подключения?", "Ошибка!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                //    new SetConnectionString().Show();
                //else
                //    MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        static public string[] GetInfoUser(string id)
        {
            Con();
            try
            {

                cmd = new SqlCommand($"SELECT ID FROM users       WHERE ID = '{id}'", con);
                StatUser_sel.ID = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new SqlCommand($"SELECT F FROM users       WHERE ID = '{id}'", con);
                StatUser_sel.F = cmd.ExecuteScalar().ToString();

                cmd = new SqlCommand($"SELECT I FROM users     WHERE ID = '{id}'", con);
                StatUser_sel.I = cmd.ExecuteScalar().ToString();

                cmd = new SqlCommand($"SELECT phone FROM users    WHERE ID = '{id}'", con);
                StatUser_sel.phone = cmd.ExecuteScalar().ToString();

                cmd = new SqlCommand($"SELECT email FROM users    WHERE ID = '{id}'", con);
                StatUser_sel.email = cmd.ExecuteScalar().ToString();

                cmd = new SqlCommand($"SELECT post.postName FROM users " +
                                     $"JOIN post ON post.ID = users.ID_post " +
                                     $"WHERE users.ID = '{id}'", con);
                StatUser_sel.post = cmd.ExecuteScalar().ToString();

                string[] result = new string[] { StatUser_sel.ID.ToString(), StatUser_sel.F, StatUser_sel.I, StatUser_sel.phone, StatUser_sel.email, StatUser_sel.post };
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        //static public void LoadUsers(string id)
        //{
        //    Con();
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter($"SELECT users.ID, users.F as 'Фамилия', users.I as 'Имя', users.phone as 'Номет телефона', users.email as 'Электронная почта', post.[postName] as 'Должность' FROM users " +
        //                                                    $"JOIN post ON post.ID = users.ID_post ", con);
        //        DataSet data = new DataSet();
        //        adapter.Fill(data);
        //        grid.DataSource = data.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        static public void fillUsers(ComboBox combo)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM users", con);
                DataSet data = new DataSet();
                adapter.Fill(data);

                //combo.DisplayMember = "typeName";
                //combo.ValueMember = "typeName";
                //combo.SelectedIndex = -1;
                combo.DisplayMember = "ID";
                combo.ValueMember = "ID";
                combo.DataSource = data.Tables[0];
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
        static public bool validLog(string log)
        {
            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd = new SqlCommand($"SELECT * FROM users WHERE log = '{log}'", con);
            adapter.SelectCommand = cmd;
            adapter.Fill(tbl);

            if (tbl.Rows.Count == 0)
                return true;
            else
                return false;
        }
        static public bool crtUser(string log, string pass, string F, string I, string phone, string email, int ID_post)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO users (log, pass, F, I, phone, email, ID_post) " +
                                     $"VALUES ('{log}', '{DBF.CalculateMD5Hash(pass)}', '{F}', '{I}', '{phone}', '{email}', '{ID_post}')", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public bool updUser(string F, string I, string phone, string email, int ID_post)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"UPDATE users SET log = '{StatUser.login}', pass = '{StatUser.password}', F = '{F}'," +
                                     $"I = '{I}', phone = '{phone}', email = '{email}', ID_post = {ID_post} " +
                                     $"WHERE users.ID = '{StatUser.ID}'", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public bool delUser(string id)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM absence " +
                                     $"WHERE ID_user = '{id}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"DELETE FROM education " +
                                     $"WHERE ID_user = '{id}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"DELETE FROM pay " +
                                     $"WHERE ID_user = '{id}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"DELETE FROM prize " +
                                     $"WHERE ID_user = '{id}'", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"DELETE FROM users " +
                                     $"WHERE ID = '{id}'", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Невозможно удалить пользователя привязанного к аренде.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public void delUser2()
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM absence " +
                                     $"WHERE ID_user = '{StatUser.ID}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"DELETE FROM education " +
                                     $"WHERE ID_user = '{StatUser.ID}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"DELETE FROM pay " +
                                     $"WHERE ID_user = '{StatUser.ID}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"DELETE FROM prize " +
                                     $"WHERE ID_user = '{StatUser.ID}'", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"DELETE FROM users " +
                                     $"WHERE ID = '{StatUser.ID}'", con);
                cmd.ExecuteNonQuery();
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
        static public void delUserElement(string id, string query)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM {query} " +
                                     $"WHERE ID = '{id}'", con);
                cmd.ExecuteNonQuery();
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
        static public void fillPost(ComboBox combo)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM post", con);
                DataSet data = new DataSet();
                adapter.Fill(data);

                //combo.DisplayMember = "typeName";
                //combo.ValueMember = "typeName";
                //combo.SelectedIndex = -1;
                combo.DisplayMember = "ID";
                combo.ValueMember = "ID";
                combo.DataSource = data.Tables[0];
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
        //static public void findUserPost(string id, string ID_post)
        //{
        //    Con();
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM users WHERE ID_post = {ID_post}", con);
        //        DataSet data = new DataSet();
        //        adapter.Fill(data);
        //        grid.DataSource = data.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //static public void LoadPost(string id)
        //{
        //    Con();
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, [postName] as 'Название', [access1] as 'Вкладка 1', [access2] as 'Вкладка 2', [access3] as 'Вкладка 3', [access4] as 'Вкладка 4', [access5] as 'Вкладка 5', [access6] as 'Вкладка 6', [salary] as 'Зарплата' FROM post", con);
        //        DataSet data = new DataSet();
        //        adapter.Fill(data);
        //        grid.DataSource = data.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //static public void LoadQuery(string id, string query, int ID_user)
        //{
        //    Con();
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {query} " +
        //                                                    $"WHERE ID_user = '{ID_user}'", con);
        //        DataSet data = new DataSet();
        //        adapter.Fill(data);
        //        grid.DataSource = data.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        static public bool delPost(string id)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM post " +
                                     $"WHERE ID = '{id}'", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public bool crtPost(string postName, bool access1, bool access2, bool access3, bool access4, bool access5, bool access6, int salary)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO post (postname, access1, access2, access3, access4, access5, access6, salary) " +
                                     $"VALUES ('{postName}', '{access1}', '{access2}', '{access3}', '{access4}', '{access5}', '{access6}', '{salary}')", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public bool updPost(int ID, string postName, bool access1, bool access2, bool access3, bool access4, bool access5, bool access6, int salary)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"UPDATE post SET postname = '{postName}', access1 = '{access1}', access2 = '{access2}', access3 = '{access3}'," +
                                     $"access4 = '{access4}', access5 = '{access5}', access6 = '{access6}', salary = '{salary}' " +
                                     $"WHERE post.ID = {ID}", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public bool crtAbsence(int ID_user, string cause, string date_start, string date_fin)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO absence (ID_user, cause, date_start, date_fin) " +
                                     $"VALUES ('{ID_user}', '{cause}', '{date_start}', '{date_fin}')", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public bool crtEducation(int ID_user, string education)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO education (ID_user, education) " +
                                     $"VALUES ('{ID_user}', '{education}')", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public bool crtPay(int ID_user, string date, int summ)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO pay (ID_user, date, summ) " +
                                     $"VALUES ('{ID_user}', '{date}', {summ})", con);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static public bool crtPrize(int ID_user, string date, int summ)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO prize (ID_user, date, summ) " +
                                     $"VALUES ('{ID_user}', '{date}', {summ})", con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        //static public void findUserFI(string id, string fi)
        //{
        //    Con();
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM users " +
        //                                                    //$"JOIN users ON users.ID = tab.user_ID " +
        //                                                    $"WHERE F + ' ' + I LIKE '%{fi}%'", con);
        //        DataSet data = new DataSet();
        //        adapter.Fill(data);
        //        grid.DataSource = data.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
    }
}
