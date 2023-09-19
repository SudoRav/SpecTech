using System;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace MaterialSkinExample
{
    class DBF_Leas
    {
        static SqlConnection con;
        static SqlCommand cmd;

        static private void Con()
        {
            try
            {
                con = new SqlConnection(Properties.Settings.Default.connectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Ошибка подключения Базы Данных. Перейти к строке подключения?", "Ошибка!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    new SetConnectionString().Show();
                else
                    MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        static public void LoadLeas(DataGridView grid)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, [ID_user] as 'Пользователь', [address] as 'Аддресс', [data_start] as 'Дата начала', [data_fin] as 'Дата окончания', [summ] as 'Сумма' FROM leas", con);
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
        static public void LoadLeas_Order(DataGridView grid, string ID_leas)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, [ID_user] as 'Пользователь', [address] as 'Аддресс', [data_start] as 'Дата начала', [data_fin] as 'Дата окончания', [summ] as 'Сумма' FROM leas " +
                                                            $"WHERE ID = {ID_leas}", con);
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

        static public void detail(DataGridView grid)
        {
            Con();
            try
            {
                DataTable tbl = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd = new SqlCommand($"SELECT * FROM leas WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                adapter.SelectCommand = cmd;
                adapter.Fill(tbl);

                if (tbl.Rows.Count != 0)
                {
                    cmd = new SqlCommand($"SELECT ID FROM leas      WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatLeas.ID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand($"SELECT address FROM leas WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatLeas.address = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand($"SELECT summ FROM leas    WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatLeas.summ = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand($"SELECT users.F + ' ' + users.I FROM leas " +
                                         $"JOIN users ON users.ID = leas.ID_user " +
                                         $"WHERE leas.ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatLeas.FI_user = cmd.ExecuteScalar().ToString();

                    new DetailLeas().Show();
                }
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
        static public void crtLeas(int ID_user, string address, string data_start, string data_fin, string summ)
        {
            Con();
            try
            {
                    cmd = new SqlCommand($"INSERT INTO leas (ID_user, address, data_start, data_fin, summ) " +
                                         $"VALUES ({ID_user}, '{address}', '{data_start}', '{data_fin}', '{summ}')", con);
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
        static public void delLeas(DataGridView grid)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM leas " +
                                     $"WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
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
        static public void delLeas2()
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM leas " +
                                     $"WHERE ID = '{StatLeas.ID}'", con);
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

        static public void LoadAttached(DataGridView grid, string ID_leas)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID_tech, * FROM list_tech " +
                                                            $"WHERE ID_leas = '{ID_leas}'", con);
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
        static public void delAttached(DataGridView grid)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM list_tech " +
                                     $"WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"UPDATE tech SET status_leas = 'False' WHERE tech.ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
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

        static public void crtAttached(string ID_tech)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO list_tech (ID_leas, ID_tech) " +
                                     $"VALUES ('{StatLeas.ID}', '{ID_tech}')", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"UPDATE tech SET status_leas = 'True' WHERE tech.ID = '{ID_tech}'", con);
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

        static public void CreateWordFileLeas()
        {
            Con();
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Word._Application word_app = new Word.Application();
                    word_app.Visible = true;
                    object missing = Type.Missing;
                    Word._Document word_doc = word_app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    object start = 0, end = 0;
                    Word.Range rng = word_doc.Range(ref start, ref end);

                    rng.InsertBefore("Отчёт Техника");
                    rng.Font.Name = "Times New Roman";
                    rng.Font.Size = 12;
                    rng.InsertParagraphAfter();
                    rng.InsertParagraphAfter();
                    rng.SetRange(rng.End, rng.End);

                    cmd = new SqlCommand($"SELECT COUNT(*) FROM leas", con);
                    int rows = Convert.ToInt32(cmd.ExecuteScalar());

                    rng.Tables.Add(word_doc.Paragraphs[2].Range, rows, 5, ref missing, ref missing);

                    Word.Table tbl = word_doc.Tables[1];
                    tbl.Range.Font.Size = 9;
                    tbl.Range.Font.Name = "Times New Roman";
                    tbl.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Columns.DistributeWidth();
                    ////                                                         0                         1                        2                              3                                  4                                    5                         6                      
                    //SqlDataAdapter adapter = new SqlDataAdapter($"SELECT leas.ID, users.F + ' ' + users.I as 'Пользователь', leas.[address] as 'Аддресс', leas.[data_start] as 'Дата начала', leas.[data_fin] as 'Дата окончания', leas.[summ] as 'Сумма', tech.[name] FROM leas " +
                    //                                            $"JOIN users ON users.ID = leas.[ID_user] " +
                    //                                            $"JOIN list_tech ON list_tech.[ID_leas] = leas.ID " +
                    //                                            $"JOIN tech ON tech.ID = list_tech.[ID_tech] " +
                    //                                            $"WHERE tech.ID_leas = ", con);
                    SqlDataAdapter adapter = new SqlDataAdapter($"SELECT leas.ID, users.F + ' ' + users.I as 'Пользователь', leas.[address] as 'Аддресс', leas.[data_start] as 'Дата начала', leas.[data_fin] as 'Дата окончания', leas.[summ] as 'Сумма' FROM leas " +
                                                                $"JOIN users ON users.ID = leas.[ID_user] ", con);

                    DataSet data = new DataSet();
                    adapter.Fill(data);

                    tbl.Cell(1, 1).Range.Text = "№";
                    tbl.Cell(1, 2).Range.Text = "Пользователь";
                    tbl.Cell(1, 3).Range.Text = "Аддресс";
                    tbl.Cell(1, 4).Range.Text = "Срок";
                    tbl.Cell(1, 5).Range.Text = "Сумма";

                    for (int q = 2; q <= rows; q++)
                    {
                        tbl.Cell(q, 1).Range.Text = (q - 1).ToString();
                        tbl.Cell(q, 2).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[1].ToString();
                        tbl.Cell(q, 3).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[2].ToString();
                        tbl.Cell(q, 4).Range.Text = ("С " + data.Tables[0].Rows[q - 2].ItemArray[3].ToString() + " по " + data.Tables[0].Rows[q - 2].ItemArray[4].ToString());
                        tbl.Cell(q, 5).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[5].ToString();
                    }

                    object filename = dialog.SelectedPath + @"\" + "Отчёт Аренда";
                    word_doc.SaveAs(ref filename, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing);

                    //object save_changes = false;
                    //word_doc.Close(ref save_changes, ref missing, ref missing);
                    //word_app.Quit(ref save_changes, ref missing, ref missing);
                }
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
