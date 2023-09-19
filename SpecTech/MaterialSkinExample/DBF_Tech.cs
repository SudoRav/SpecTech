using System;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms.DataVisualization.Charting;

namespace MaterialSkinExample
{
    class DBF_Tech
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

        static public void LoadTech(DataGridView grid)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT tech.ID, tech.name as 'Название', type.[typeName] as 'Тип', [price] as 'Цена', [status_leas] as 'Статус Аренды', [status_rep] as 'Состояние', [discount] as 'Скидка' FROM tech " +
                                                            $"JOIN type ON type.ID = tech.ID_type ", con);
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
        static public void LoadTechNotAtt(DataGridView grid)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT tech.ID, tech.name as 'Название', type.[typeName] as 'Тип', [price] as 'Цена', [status_leas] as 'Статус Аренды', [status_rep] as 'Состояние', [discount] as 'Скидка' FROM tech " +
                                                            $"JOIN type ON type.ID = tech.ID_type " +
                                                            $"WHERE status_leas = 'False'", con);
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
        static public void fillGisto(Chart chart)
        {
            Con();
            try
            {
                chart.Series[0].Points.Clear();

                cmd = new SqlCommand($"SELECT MAX(ID) FROM tech ", con);
                int mid = Convert.ToInt32(cmd.ExecuteScalar()) + 10;
                int po = 0;

                for (int i = 0; i < mid; i++)
                {
                    cmd = new SqlCommand($"SELECT COUNT(ID) FROM tech WHERE ID_type = {i} ", con);
                    int wid = Convert.ToInt32(cmd.ExecuteScalar());

                    if (wid > 0)
                    {
                        cmd = new SqlCommand($"SELECT type.typeName FROM tech " +
                                             $"JOIN type ON type.ID = tech.ID_type " +
                                             $"WHERE tech.ID_type = {i} ", con);
                        string name = cmd.ExecuteScalar().ToString();

                        chart.Series[0].Points.Add(wid);
                        chart.Series[0].Points[po].LegendText = name;
                        chart.Series[0].Points[po].Label = name;
                        po++;
                    }
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
        static public void findTechType(DataGridView grid, string ID_type)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM tech " +
                                                            $"WHERE ID_type = {ID_type}", con);
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
        static public void findTechName(DataGridView grid, string name)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM tech " +
                                                            //$"JOIN users ON users.ID = tab.user_ID " +
                                                            $"WHERE name LIKE '%{name}%'", con);
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

        static public void crtTech(string name, int ID_type, string filePath, string desc, int price)
        {
            Con();
            try
            {
                using (Stream stream = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);

                    string fileExtn = new FileInfo(filePath).Extension;
                    string fileName = new FileInfo(filePath).Name;

                    string query = $"INSERT INTO tech (name, ID_type, photo, [desc], price, status_leas, status_rep, rep_text, discount) " +
                                   $"VALUES('{name}', {ID_type}, @data, '{desc}', {price}, 0, 0, '', 0)";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = fileName;
                    cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = buffer;
                    cmd.Parameters.Add("@extn", SqlDbType.Char).Value = fileExtn;
                    cmd.ExecuteNonQuery();
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
        static public void updTech(string name, int ID_type, string filePath, string desc, int price)
        {
            Con();
            try
            {
                using (Stream stream = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);

                    string fileExtn = new FileInfo(filePath).Extension;
                    string fileName = new FileInfo(filePath).Name;


                    string query = $"UPDATE tech SET name = '{name}', ID_type = '{ID_type}', photo = NULL, [desc] = '{desc}', price = '{price}', " +
                                   $"status_leas = '{StatTech.status_leas}', status_rep = '{StatTech.status_rep}', rep_text = '{StatTech.rep_text}', discount = '{StatTech.discount}' " +
                                   $"WHERE tech.ID = {StatTech.ID}";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = fileName;
                    cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = buffer;
                    cmd.Parameters.Add("@extn", SqlDbType.Char).Value = fileExtn;
                }

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

        static private int tof(string q)
        {
            if (q == "True")
                return 1;
            else
                return 0;
        }

        static public void delTech()
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM characts " +
                                     $"WHERE ID_tech = '{StatTech.ID}'", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"DELETE FROM tech " +
                                     $"WHERE ID = '{StatTech.ID}'", con);
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
        static public void delTech2(DataGridView grid)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM characts " +
                                     $"WHERE ID_tech = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"DELETE FROM tech " +
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

        static public void setDiscount(int ID_tech, int discount)
        {
            if (discount < 0)
                discount = 0;
            if (discount > 100)
                discount = 100;

            Con();
            try
            {
                cmd = new SqlCommand($"UPDATE tech SET discount = '{discount}' WHERE tech.ID = '{ID_tech}'", con);
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

        static public void crtRep(int ID_tech, string text_rep)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"UPDATE tech SET status_rep = 'True' WHERE tech.ID = '{ID_tech}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"UPDATE tech SET rep_text = '{text_rep}' WHERE tech.ID = '{ID_tech}'", con);
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

        static public void delRep(int ID_tech)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"UPDATE tech SET status_rep = 'False' WHERE tech.ID = '{ID_tech}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"UPDATE tech SET rep_text = '' WHERE tech.ID = '{ID_tech}'", con);
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

        static public void fillType(ComboBox combo)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM type", con);
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

        static public void detail(DataGridView grid)
        {
            Con();
            try
            {
                DataTable tbl = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd = new SqlCommand($"SELECT * FROM tech WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                adapter.SelectCommand = cmd;
                adapter.Fill(tbl);

                if (tbl.Rows.Count != 0)
                {
                    cmd = new SqlCommand($"SELECT ID FROM tech           WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.ID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand($"SELECT name FROM tech         WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.name = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand($"SELECT type.typeName FROM tech " +
                                         $"JOIN type ON type.ID = tech.ID_type " +
                                         $"WHERE tech.ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.type = cmd.ExecuteScalar().ToString();

                    try
                    {
                        cmd = new SqlCommand($"SELECT type.ID_type FROM tech WHERE tech.ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                        StatTech.typeID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch { StatTech.typeID = -1; }

                    selectPicture(grid);

                    cmd = new SqlCommand($"SELECT [desc] FROM tech       WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.desc = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand($"SELECT price FROM tech        WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.price = Convert.ToDouble(cmd.ExecuteScalar());

                    cmd = new SqlCommand($"SELECT status_leas FROM tech  WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.status_leas = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand($"SELECT status_rep FROM tech   WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.status_rep = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand($"SELECT rep_text FROM tech     WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.rep_text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand($"SELECT discount FROM tech     WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                    StatTech.discount = Convert.ToInt32(cmd.ExecuteScalar());

                    new DetailTech().ShowDialog();
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

        static public void selectPicture(DataGridView grid)
        {
            SqlCommand cmd = new SqlCommand($"SELECT photo FROM tech " +
                                            $"WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "tech");
            int c = ds.Tables["tech"].Rows.Count;

            if (c > 0)
            {
                try
                {
                    Byte[] byteBLOBData = new Byte[0];
                    byteBLOBData = (Byte[])(ds.Tables["tech"].Rows[c - 1]["photo"]);
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    StatTech.photo = stmBLOBData;
                }
                catch
                {
                    StatTech.photo = null;
                }
            }
        }

        static public void LoadCharacts(DataGridView grid)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, name as 'Название', val as 'Значение' FROM characts WHERE ID_tech = '{StatTech.ID}'", con);
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

        static public void delCharacts(DataGridView grid)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM characts " +
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

        static public void crtCharacts(string name, string val)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO characts (ID_tech, name, val) VALUES ('{StatTech.ID}', '{name}', '{val}')", con);
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

        static public void LoadType(DataGridView grid)
        {
            Con();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, [typeName] as 'Название' FROM type", con);
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
        static public void delType(DataGridView grid)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"DELETE FROM type " +
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
        static public void crtType(string typeName)
        {
            Con();
            try
            {
                cmd = new SqlCommand($"INSERT INTO type (typeName) VALUES ('{typeName}')", con);
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

        static public void CreateWordFileTech()
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

                    cmd = new SqlCommand($"SELECT COUNT(*) FROM tech", con);
                    int rows = Convert.ToInt32(cmd.ExecuteScalar());

                    rng.Tables.Add(word_doc.Paragraphs[2].Range, rows, 5, ref missing, ref missing);

                    Word.Table tbl = word_doc.Tables[1];
                    tbl.Range.Font.Size = 9;
                    tbl.Range.Font.Name = "Times New Roman";
                    tbl.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Columns.DistributeWidth();

                    SqlDataAdapter adapter = new SqlDataAdapter($"SELECT tech.ID, tech.name as 'Название', type.[typeName] as 'Тип', [price] as 'Цена', [status_leas] as 'Статус Аренды', [status_rep] as 'Состояние', [discount] as 'Скидка' FROM tech " +
                                                                $"JOIN type ON type.ID = tech.ID_type ", con);
                    DataSet data = new DataSet();
                    adapter.Fill(data);

                    tbl.Cell(1, 1).Range.Text = "№";
                    tbl.Cell(1, 2).Range.Text = "Название";
                    tbl.Cell(1, 3).Range.Text = "Тип";
                    tbl.Cell(1, 4).Range.Text = "Цена";
                    tbl.Cell(1, 5).Range.Text = "Скидка";

                    for (int q = 2; q <= rows; q++)
                    {
                        tbl.Cell(q, 1).Range.Text = (q - 1).ToString();
                        tbl.Cell(q, 2).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[1].ToString();
                        tbl.Cell(q, 3).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[2].ToString();
                        tbl.Cell(q, 4).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[3].ToString();
                        tbl.Cell(q, 5).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[7].ToString();
                    }

                    object filename = dialog.SelectedPath + @"\" + "Отчёт Техника";
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
