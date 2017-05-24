using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Baigiamasis_d
{
    public partial class VVP : Form
    {
        public string session_username = "-";
        public DataTable session_user;
        public int session_tipas;
        VVP_class Dienynas;
        Dictionary<int, int> dalykai_ids = new Dictionary<int, int>(); // Saugo dalykų id pagal grid eiliškumą
        Dictionary<int, int> klases_ids = new Dictionary<int, int>();
        Dictionary<int, int> pazymiai_ids = new Dictionary<int, int>();
        Dictionary<int, int> namu_darbai_ids = new Dictionary<int, int>();
        Dictionary<int, int> mokiniai_ids = new Dictionary<int, int>();
        Dictionary<int, int> mokiniai2_ids = new Dictionary<int, int>();
        Dictionary<int, int> tevai_ids = new Dictionary<int, int>();
        Dictionary<int, int> mokytojai_ids = new Dictionary<int, int>();
        string grid_currentValue = "";

        public VVP()
        {
            InitializeComponent();
        }

        private void atsijungti_Click(object sender, EventArgs e)
        {
            // Atjungia vartotoją
            this.Hide();
            var form = new Prisijungimas();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void VVP_Load(object sender, EventArgs e)
        {
            Database_class.Connect();
            pazymiu_lentele.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Dienynas = new VVP_class(session_username);
            session_user = Dienynas.getUser();
            if (session_user.Rows.Count > 0)
            {
                username.Text = session_user.Rows[0]["vardas"].ToString() + " " + session_user.Rows[0]["pavarde"].ToString();
                session_tipas = int.Parse(session_user.Rows[0]["tipas"].ToString());

                if (session_tipas <= 1)
                {
                    puslapiai.TabPages.Remove(pazymiu_valdymas);
                    puslapiai.TabPages.Remove(namu_darbai_mokytojas);
                    puslapiai.TabPages.Remove(vartotojai);

                    if (session_tipas == 0)
                        dizainas.Text += " - mokinys";
                    else
                    {
                        Dienynas = new VVP_class("", int.Parse(session_user.Rows[0]["parent"].ToString()));
                        session_user = Dienynas.getUser();
                        dizainas.Text += " - tėvai (" + session_user.Rows[0]["vardas"].ToString() + " " + session_user.Rows[0]["pavarde"].ToString() + ")";
                    }

                    Tab1();
                }
                else if (session_tipas == 2)
                {
                    puslapiai.TabPages.Remove(pazymiai);
                    puslapiai.TabPages.Remove(vartotojai);
                    puslapiai.TabPages.Remove(namu_darbai_mokinys);

                    dizainas.Text += " - mokytojas";
                    dalykas.Text = Dienynas.DalykoPavadinimas(int.Parse(session_user.Rows[0]["dalykas"].ToString()));
                    dalykas2.Text = Dienynas.DalykoPavadinimas(int.Parse(session_user.Rows[0]["dalykas"].ToString()));

                    Tab2();
                }
                else if (session_tipas == 3)
                {
                    puslapiai.TabPages.Remove(pazymiai);
                    puslapiai.TabPages.Remove(pazymiu_valdymas);
                    puslapiai.TabPages.Remove(namu_darbai_mokinys);
                    puslapiai.TabPages.Remove(namu_darbai_mokytojas);

                    dizainas.Text += " - administratorius";

                    Tab3();
                }
            }
            else
                Helper_class.SistemosKlaida();
        }

        private void Tab3()
        {
            // Administratoriaus teisės
            DataTable visi_dalykai = Dienynas.Dalykai();
            int rows = visi_dalykai.Rows.Count;

            dalykai.Items.Clear();
            dalykai_ids.Clear();

            for (int i = 0; i < rows; i++)
            {
                string dalykas = visi_dalykai.Rows[i]["dalykas"].ToString();
                dalykai.Items.Add(dalykas);
                dalykai_ids.Add(i, int.Parse(visi_dalykai.Rows[i]["id"].ToString()));
            }

            DataTable visos_klases = Dienynas.Klases();
            rows = visos_klases.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                string klase = visos_klases.Rows[i]["klase"].ToString();
                klases.Items.Add(klase);
                klases_ids.Add(i, int.Parse(visos_klases.Rows[i]["id"].ToString()));
            }

            loadMokiniai();
            loadTevai();
            loadMokytojai();
        }

        private void Tab2()
        {
            // Mokytojo teisės
            DataTable visos_klases = Dienynas.Klases();
            int rows = visos_klases.Rows.Count;

            klases2.Items.Clear();
            klases3.Items.Clear();
            klases4.Items.Clear();
            klases5.Items.Clear();
            klases_ids.Clear();

            for (int i = 0; i < rows; i++)
            {
                string klase = visos_klases.Rows[i]["klase"].ToString();
                klases2.Items.Add(klase);
                klases3.Items.Add(klase);
                klases4.Items.Add(klase);
                klases5.Items.Add(klase);
                klases_ids.Add(i, int.Parse(visos_klases.Rows[i]["id"].ToString()));
            }

            DateTime current_date = Helper_class.currentDate();
            int n;
            if (current_date.Month >= 9 && current_date.Month <= 12)
                n = current_date.Month - 9 + 1;
            else
                n = current_date.Month + 4;
            for (int i = 0; i < n; i++)
            {
                laikotarpis2.Items.Add(current_date.ToString("yyyy-MM"));
                current_date = current_date.AddMonths(-1);
            }
            laikotarpis2.Items.Add("Visi metai");
            laikotarpis2.SelectedIndex = laikotarpis2.Items.Count - 1;
        }

        private void Tab1(string laikotarpis_str = "")
        {
            // Mokinio teisės
            DateTime current_date, current_date2 = Helper_class.currentDate();
            if (laikotarpis_str == "")
                current_date = current_date2;
            else
                current_date = DateTime.Parse(laikotarpis_str);

            int days = Helper_class.monthDays(current_date.Year, current_date.Month);

            for (int i = 1; i <= days; i++)
            {
                string name = "c" + current_date.Year + "_" + current_date.Month.ToString("00") + "_" + i.ToString("00");
                string text = current_date.Month.ToString("00") + "-" + i.ToString("00");
                pazymiu_lentele.Columns.Add(name, text);
                pazymiu_lentele.Columns[i - 1].Width = 60;
                pazymiu_lentele.Columns[i - 1].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DataTable dalykai = Dienynas.Dalykai();
            int rows = dalykai.Rows.Count;
            Dictionary<int, int> p_ids = new Dictionary<int, int>();
            dalykai_ids.Clear();

            for (int i = 0; i < rows; i++)
            {
                int dalykas = int.Parse(dalykai.Rows[i]["id"].ToString());
                pazymiu_lentele.Rows.Add();
                pazymiu_lentele.Rows[i].HeaderCell.Value = dalykai.Rows[i]["dalykas"].ToString();
                vidurkis_dalykai.Items.Add(dalykai.Rows[i]["dalykas"].ToString());
                dalykai2.Items.Add(dalykai.Rows[i]["dalykas"].ToString());
                p_ids.Add(dalykas, i);
                dalykai_ids.Add(i, dalykas);
            }
            vidurkis_dalykai.Items.Add("Visi dalykai");
            dalykai2.Items.Add("Visi dalykai");
            pazymiu_lentele.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            DataTable pazymiai = Dienynas.getDiary(current_date.Year, current_date.Month);
            rows = pazymiai.Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                int dalykas = int.Parse(pazymiai.Rows[i]["id"].ToString());
                DateTime data = DateTime.Parse(pazymiai.Rows[i]["data"].ToString());
                string name = "c" + data.Year + "_" + data.Month.ToString("00") + "_" + data.Day.ToString("00");
                string paz = pazymiai.Rows[i]["pazymys"].ToString();
                pazymiu_lentele.Rows[p_ids[dalykas]].Cells[name].Value = paz;
                if (paz == "n")
                    pazymiu_lentele.Rows[p_ids[dalykas]].Cells[name].Style.BackColor = Color.Red;
                else if (paz == "p")
                    pazymiu_lentele.Rows[p_ids[dalykas]].Cells[name].Style.BackColor = Color.Yellow;
                else if (int.Parse(paz) < 4)
                    pazymiu_lentele.Rows[p_ids[dalykas]].Cells[name].Style.BackColor = Color.Red;
                else if (int.Parse(paz) >= 4 && int.Parse(paz) < 8)
                    pazymiu_lentele.Rows[p_ids[dalykas]].Cells[name].Style.BackColor = Color.LightGray;
                else
                    pazymiu_lentele.Rows[p_ids[dalykas]].Cells[name].Style.BackColor = Color.LightGreen;
            }

            int n;
            if (current_date2.Month >= 9 && current_date2.Month <= 12)
                n = current_date2.Month - 9 + 1;
            else
                n = current_date2.Month + 4;
            for (int i = 0; i < n; i++)
            {
                laikotarpis.Items.Add(current_date2.ToString("yyyy-MM"));
                vidurkis_laikotarpis.Items.Add(current_date2.ToString("yyyy-MM"));
                laikotarpis3.Items.Add(current_date2.ToString("yyyy-MM"));
                current_date2 = current_date2.AddMonths(-1);
            }
            vidurkis_laikotarpis.Items.Add("Visi metai");
            laikotarpis3.Items.Add("Visi metai");
            vidurkis.Text = "-";
            laikotarpis3.SelectedIndex = laikotarpis3.Items.Count - 1;
        }

        private void vardas1_Enter(object sender, EventArgs e)
        {
            if (vardas1.Text == "Vardas")
                vardas1.Text = "";
        }

        private void vardas1_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(vardas1.Text))
                vardas1.Text = "Vardas";
        }

        private void vardas2_Enter(object sender, EventArgs e)
        {
            if (vardas2.Text == "Vardas")
                vardas2.Text = "";
        }

        private void vardas2_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(vardas2.Text))
                vardas2.Text = "Vardas";
        }

        private void vardas3_Enter(object sender, EventArgs e)
        {
            if (vardas3.Text == "Vardas")
                vardas3.Text = "";
        }

        private void vardas3_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(vardas3.Text))
                vardas3.Text = "Vardas";
        }

        private void vardas4_Enter(object sender, EventArgs e)
        {
            if (vardas4.Text == "Vardas")
                vardas4.Text = "";
        }

        private void vardas4_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(vardas4.Text))
                vardas4.Text = "Vardas";
        }

        private void pavarde1_Enter(object sender, EventArgs e)
        {
            if (pavarde1.Text == "Pavardė")
                pavarde1.Text = "";
        }

        private void pavarde1_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(pavarde1.Text))
                pavarde1.Text = "Pavardė";
        }

        private void pavarde2_Enter(object sender, EventArgs e)
        {
            if (pavarde2.Text == "Pavardė")
                pavarde2.Text = "";
        }

        private void pavarde2_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(pavarde2.Text))
                pavarde2.Text = "Pavardė";
        }

        private void pavarde3_Enter(object sender, EventArgs e)
        {
            if (pavarde3.Text == "Pavardė")
                pavarde3.Text = "";
        }

        private void pavarde3_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(pavarde3.Text))
                pavarde3.Text = "Pavardė";
        }

        private void pavarde4_Enter(object sender, EventArgs e)
        {
            if (pavarde4.Text == "Pavardė")
                pavarde4.Text = "";
        }

        private void pavarde4_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(pavarde4.Text))
                pavarde4.Text = "Pavardė";
        }

        private void tevai1_CheckedChanged(object sender)
        {
            if (tevai1.Checked)
            {
                vardas2.Enabled = true;
                pavarde2.Enabled = true;
            }
            else
            {
                vardas2.Enabled = false;
                pavarde2.Enabled = false;
            }
        }

        private void tevai2_CheckedChanged(object sender)
        {
            if (tevai2.Checked)
            {
                vardas3.Enabled = true;
                pavarde3.Enabled = true;
            }
            else
            {
                vardas3.Enabled = false;
                pavarde3.Enabled = false;
            }
        }

        private void generuoti_Click(object sender, EventArgs e)
        {
            // Sugeneruoja naują registracijos kodą mokiniui ar tėvams
            if (session_tipas == 3)
            {
                if ((vardas1.Text == "Vardas" || pavarde1.Text == "Pavardė" || klases.SelectedIndex == -1) || ((tevai1.Checked) && (vardas2.Text == "Vardas" || pavarde2.Text == "Pavardė")) || ((tevai2.Checked) && (vardas3.Text == "Vardas" || pavarde3.Text == "Pavardė")))
                    MessageBox.Show("Prašome užpildyti visus laukelius!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    generuoti.Enabled = false;
                    regkodas1.Text = Dienynas.generateCode(vardas1.Text, pavarde1.Text, 0, klases_ids[klases.SelectedIndex]);
                    regkodas1.Enabled = true;
                    int parent = Dienynas.getParent(vardas1.Text, pavarde1.Text);

                    if (tevai1.Checked)
                    {
                        regkodas2.Text = Dienynas.generateCode(vardas2.Text, pavarde2.Text, 1, 0, parent);
                        regkodas2.Enabled = true;
                    }

                    if (tevai2.Checked)
                    {
                        regkodas3.Text = Dienynas.generateCode(vardas3.Text, pavarde3.Text, 1, 0, parent);
                        regkodas3.Enabled = true;
                    }

                    generuoti.Enabled = true;
                }
            }
        }

        private void atnaujinti_Click(object sender, EventArgs e)
        {
            // Atnaujina pažymius
            if (session_tipas <= 1)
            {
                atnaujinti.Enabled = false;
                pazymiu_lentele.Columns.Clear();
                pazymiu_lentele.Rows.Clear();
                pazymiu_lentele.Refresh();
                string laikotarpis_str = laikotarpis.Text;
                int selected_index = laikotarpis.SelectedIndex;
                laikotarpis.Items.Clear();
                vidurkis_dalykai.Items.Clear();
                dalykai2.Items.Clear();
                vidurkis_laikotarpis.Items.Clear();
                Tab1(laikotarpis_str);
                if (selected_index > -1)
                    laikotarpis.SelectedIndex = selected_index;
                atnaujinti.Enabled = true;
            }
        }

        private void generuoti2_Click(object sender, EventArgs e)
        {
            // Sugeneruoja naują registracijos kodą mokytojui
            if (session_tipas == 3)
            {
                if (vardas4.Text != "Vardas" && pavarde4.Text != "Pavardė" && dalykai.SelectedIndex > -1)
                {
                    generuoti2.Enabled = false;
                    regkodas4.Text = Dienynas.generateCode(vardas4.Text, pavarde4.Text, 2, 0, 0, dalykai_ids[dalykai.SelectedIndex]);
                    regkodas4.Enabled = true;
                    generuoti2.Enabled = true;
                }
                else
                    MessageBox.Show("Prašome užpildyti visus laukelius!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void puslapiai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (puslapiai.SelectedTab == puslapiai.TabPages["vartotojai"])
            {
                // if (pamokos.Items.Count == 0)
                // {

                // }
            }
        }

        private void istrinti1_Click(object sender, EventArgs e)
        {
            // Mokinio ištrynimas
            if (session_tipas == 3)
            {
                if (mokiniai.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Ar tikrai norite ištrinti vartotoją " + mokiniai.Text + "?", "Patvirtinimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Dienynas.Trinti(mokiniai_ids[mokiniai.SelectedIndex]);
                        MessageBox.Show("Vartotojas " + mokiniai.Text + " buvo sėkmingai ištrintas!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mokiniai.Items.Clear();
                        loadMokiniai();
                    }
                }
                else
                    MessageBox.Show("Prašome pasirinkti vartotoją!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void istrinti2_Click(object sender, EventArgs e)
        {
            // Tevų ištrynimas
            if (session_tipas == 3)
            {
                if (tevai.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Ar tikrai norite ištrinti vartotoją " + tevai.Text + "?", "Patvirtinimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Dienynas.Trinti(tevai_ids[tevai.SelectedIndex]);
                        MessageBox.Show("Vartotojas " + tevai.Text + " buvo sėkmingai ištrintas!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tevai.Items.Clear();
                        loadTevai();
                    }
                }
                else
                    MessageBox.Show("Prašome pasirinkti vartotoją!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void istrinti3_Click(object sender, EventArgs e)
        {
            // Mokytojo ištrynimas
            if (session_tipas == 3)
            {
                if (mokytojai.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Ar tikrai norite ištrinti vartotoją " + mokytojai.Text + "?", "Patvirtinimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Dienynas.Trinti(mokytojai_ids[mokytojai.SelectedIndex]);
                        MessageBox.Show("Vartotojas " + mokytojai.Text + " buvo sėkmingai ištrintas!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mokytojai.Items.Clear();
                        loadMokytojai();
                    }
                }
                else
                    MessageBox.Show("Prašome pasirinkti vartotoją!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void loadMokiniai()
        {
            // Mokinių sąrašas
            DataTable mokiniu_list = Dienynas.MokiniuList();
            int rows = mokiniu_list.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                int id = int.Parse(mokiniu_list.Rows[i]["id"].ToString());
                mokiniai.Items.Add(mokiniu_list.Rows[i]["vardas"].ToString() + " " + mokiniu_list.Rows[i]["pavarde"].ToString());
                mokiniai_ids.Add(i, id);
            }
        }

        private void loadTevai()
        {
            // Tevų sąrašas
            DataTable tevu_list = Dienynas.TevuList();
            int rows = tevu_list.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                int id = int.Parse(tevu_list.Rows[i]["id"].ToString());
                tevai.Items.Add(tevu_list.Rows[i]["vardas"].ToString() + " " + tevu_list.Rows[i]["pavarde"].ToString());
                tevai_ids.Add(i, id);
            }
        }

        private void loadMokytojai()
        {
            // Mokytojų sąrašas
            DataTable mokytoju_list = Dienynas.MokytojuList();
            int rows = mokytoju_list.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                int id = int.Parse(mokytoju_list.Rows[i]["id"].ToString());
                mokytojai.Items.Add(mokytoju_list.Rows[i]["vardas"].ToString() + " " + mokytoju_list.Rows[i]["pavarde"].ToString());
                mokytojai_ids.Add(i, id);
            }
        }

        private void klases2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Išgauna mokinius pagal nurodytą klasę
            if (klases2.SelectedIndex > -1)
            {
                mokiniai_ids.Clear();
                mokiniai2.Items.Clear();

                DataTable mokiniu_list = Dienynas.MokiniuList(klases_ids[klases2.SelectedIndex]);
                int rows = mokiniu_list.Rows.Count;

                for (int i = 0; i < rows; i++)
                {
                    int id = int.Parse(mokiniu_list.Rows[i]["id"].ToString());
                    mokiniai2.Items.Add(mokiniu_list.Rows[i]["vardas"].ToString() + " " + mokiniu_list.Rows[i]["pavarde"].ToString());
                    mokiniai_ids.Add(i, id);
                }
            }
        }

        private void pazymys_Enter(object sender, EventArgs e)
        {
            nedalyvavo.Checked = false;
            pavelavo.Checked = false;
            pazymys.Select(0, pazymys.Text.Length);
        }

        private void nedalyvavo_CheckedChanged(object sender, EventArgs e)
        {
            pazymys.Value = 0;
        }

        private void pavelavo_CheckedChanged(object sender, EventArgs e)
        {
            pazymys.Value = 0;
        }

        private void issaugoti_Click(object sender, EventArgs e)
        {
            // Prideda naują pažymį
            if (mokiniai2.SelectedIndex > -1 && (pazymys.Value > 0 || nedalyvavo.Checked == true || pavelavo.Checked == true))
            {
                if (!Dienynas.EgiztuojaPazymys(mokiniai_ids[mokiniai2.SelectedIndex], int.Parse(session_user.Rows[0]["dalykas"].ToString()), data.Value.ToString("yyyy-MM-dd")))
                {
                    if (DateTime.Parse(data.Value.ToString("yyyy-MM-dd")) <= Helper_class.currentDate())
                    {
                        string paz = "";
                        if (pazymys.Value > 0)
                            paz = pazymys.Value.ToString();
                        else if (nedalyvavo.Checked == true)
                            paz = "n";
                        else if (pavelavo.Checked == true)
                            paz = "p";
                        Dienynas.PridetiPazymi(mokiniai_ids[mokiniai2.SelectedIndex], int.Parse(session_user.Rows[0]["dalykas"].ToString()), paz, data.Value.ToString("yyyy-MM-dd"));
                        klases2.SelectedIndex = -1;
                        mokiniai2.Items.Clear();
                        pazymys.Value = 0;
                        nedalyvavo.Checked = false;
                        pavelavo.Checked = false;
                        data.Value = DateTime.Now;
                        MessageBox.Show("Pažymys sėkmingai išsaugotas!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Neteisinga data!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Šis mokinys jau yra gavęs pažymį nurodytą dieną!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Prašome užpildyti visus laukelius!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void klases3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Išgauna mokinius pagal nurodytą klasę
            if (klases3.SelectedIndex > -1)
            {
                mokiniai2_ids.Clear();
                mokiniai3.Items.Clear();

                DataTable mokiniu_list = Dienynas.MokiniuList(klases_ids[klases3.SelectedIndex]);
                int rows = mokiniu_list.Rows.Count;

                for (int i = 0; i < rows; i++)
                {
                    int id = int.Parse(mokiniu_list.Rows[i]["id"].ToString());
                    mokiniai3.Items.Add(mokiniu_list.Rows[i]["vardas"].ToString() + " " + mokiniu_list.Rows[i]["pavarde"].ToString());
                    mokiniai2_ids.Add(i, id);
                }
            }
        }

        private void ieskoti_Click(object sender, EventArgs e)
        {
            // Pažymių paieška pagal nurodytą mokinį
            if (mokiniai3.SelectedIndex > -1)
            {
                pazymiai_ids.Clear();
                pazymiu_list.Rows.Clear();

                DataTable pazymiai = Dienynas.getDiary2(mokiniai2_ids[mokiniai3.SelectedIndex], int.Parse(session_user.Rows[0]["dalykas"].ToString()));
                int rows = pazymiai.Rows.Count;
                for (int i = 0; i < rows; i++)
                {
                    DateTime data = DateTime.Parse(pazymiai.Rows[i]["data"].ToString());
                    pazymiu_list.Rows.Add();
                    pazymiu_list.Rows[i].Cells["grid_data"].Value = data.ToString("yyyy-MM-dd");
                    pazymiu_list.Rows[i].Cells["grid_pazymys"].Value = pazymiai.Rows[i]["pazymys"].ToString();
                    pazymiu_list.Rows[i].Cells["grid_veiksmas"].Value = "Ištrinti";
                    pazymiu_list.Rows[i].Height = 40;
                    pazymiai_ids.Add(i, int.Parse(pazymiai.Rows[i]["id"].ToString()));
                }

                if (rows > 0)
                {
                    pazymiu_list.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    pazymiu_list.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                }
                else
                    MessageBox.Show("Pažymių nerasta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Prašome pasirinkti mokinį!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void pazymiu_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                int pazid = pazymiai_ids[e.RowIndex];
                if (MessageBox.Show("Ar tikrai norite ištrinti pažymį?", "Patvirtinimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Dienynas.TrintiPazymi(pazid);
                    pazymiu_list.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void pazymiu_list_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string value = pazymiu_list.Rows[e.RowIndex].Cells["grid_pazymys"].Value.ToString();
            if ((Validator_class.isInt(value) && int.Parse(value) >= 1 && int.Parse(value) <= 10) || value == "n" || value == "p")
            {
                if (value != grid_currentValue)
                    Dienynas.AtnaujintiPazymi(pazymiai_ids[e.RowIndex], value);
            }
            else
            {
                pazymiu_list.Rows[e.RowIndex].Cells["grid_pazymys"].Value = grid_currentValue;
                MessageBox.Show("Neteisingas pažymio formatas!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pazymiu_list_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            grid_currentValue = pazymiu_list.Rows[e.RowIndex].Cells["grid_pazymys"].Value.ToString();
        }

        private void Vidurkis()
        {
            // Mokinio vidurkis
            if (vidurkis_dalykai.SelectedIndex > -1 && vidurkis_laikotarpis.SelectedIndex > -1)
            {
                int selected_dalykas = (vidurkis_dalykai.Text == "Visi dalykai") ? 0 : dalykai_ids[vidurkis_dalykai.SelectedIndex];
                int metai = 0, menuo = 0;
                if (vidurkis_laikotarpis.Text != "Visi metai")
                {
                    DateTime selected_laikotarpis = DateTime.Parse(vidurkis_laikotarpis.Text);
                    metai = selected_laikotarpis.Year;
                    menuo = selected_laikotarpis.Month;
                }
                double vid = Dienynas.Vidurkis(selected_dalykas, metai, menuo);
                vidurkis.Text = (vid > 0) ? Math.Round(vid, 2).ToString() : "-";
            }
        }

        private void vidurkis_dalykai_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vidurkis();
        }

        private void vidurkis_laikotarpis_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vidurkis();
        }

        private void issaugoti2_Click(object sender, EventArgs e)
        {
            if (klases4.SelectedIndex > -1 && namu_darbas.Text != "")
            {
                if (!Dienynas.EgiztuojaND(klases_ids[klases4.SelectedIndex], int.Parse(session_user.Rows[0]["dalykas"].ToString()), data2.Value.ToString("yyyy-MM-dd")))
                {
                    Dienynas.PridetiND(klases_ids[klases4.SelectedIndex], int.Parse(session_user.Rows[0]["dalykas"].ToString()), namu_darbas.Text, data2.Value.ToString("yyyy-MM-dd"));
                    klases4.SelectedIndex = -1;
                    namu_darbas.Text = "";
                    data2.Value = DateTime.Now;
                    MessageBox.Show("Namų darbas sėkmingai išsaugotas!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Šiai klasei jau yra užduoti namų darbai nurodytą dieną!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Prašome užpildyti visus laukelius!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void nd_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                int ndid = namu_darbai_ids[e.RowIndex];
                if (MessageBox.Show("Ar tikrai norite ištrinti namų darbą?", "Patvirtinimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Dienynas.TrintiND(ndid);
                    nd_list.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void nd_list_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string value = nd_list.Rows[e.RowIndex].Cells["grid_namu_darbas"].Value.ToString();
            Dienynas.AtnaujintiND(namu_darbai_ids[e.RowIndex], value);
        }

        private void ieskoti2_Click(object sender, EventArgs e)
        {
            // Namų darbų paieška pagal nurodytą klasę
            if (klases5.SelectedIndex > -1)
            {
                if (laikotarpis2.SelectedIndex > -1)
                {
                    namu_darbai_ids.Clear();
                    nd_list.Rows.Clear();

                    int metai = 0, menuo = 0;
                    if (laikotarpis2.Text != "Visi metai")
                    {
                        DateTime selected_laikotarpis = DateTime.Parse(laikotarpis2.Text);
                        metai = selected_laikotarpis.Year;
                        menuo = selected_laikotarpis.Month;
                    }

                    DataTable namu_darbai = Dienynas.getND(klases_ids[klases5.SelectedIndex], int.Parse(session_user.Rows[0]["dalykas"].ToString()), metai, menuo);
                    int rows = namu_darbai.Rows.Count;
                    for (int i = 0; i < rows; i++)
                    {
                        DateTime data = DateTime.Parse(namu_darbai.Rows[i]["data"].ToString());
                        nd_list.Rows.Add();
                        nd_list.Rows[i].Cells["grid_data2"].Value = data.ToString("yyyy-MM-dd");
                        nd_list.Rows[i].Cells["grid_namu_darbas"].Value = namu_darbai.Rows[i]["namu_darbas"].ToString();
                        nd_list.Rows[i].Cells["grid_veiksmas2"].Value = "Ištrinti";
                        nd_list.Rows[i].Height = 40;
                        namu_darbai_ids.Add(i, int.Parse(namu_darbai.Rows[i]["id"].ToString()));
                    }

                    if (rows > 0)
                    {
                        nd_list.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        nd_list.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                        nd_list.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    }
                    else
                        MessageBox.Show("Namų darbų neužduota", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Prašome pasirinkti laikotarpį!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Prašome pasirinkti klasę!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ieskoti3_Click(object sender, EventArgs e)
        {
            if (dalykai2.SelectedIndex > -1)
            {
                if (laikotarpis3.SelectedIndex > -1)
                {
                    namu_darbai_ids.Clear();
                    nd_list2.Rows.Clear();
                    int selected_dalykas = (dalykai2.Text == "Visi dalykai") ? 0 : dalykai_ids[dalykai2.SelectedIndex];

                    int metai = 0, menuo = 0;
                    if (laikotarpis3.Text != "Visi metai")
                    {
                        DateTime selected_laikotarpis = DateTime.Parse(laikotarpis3.Text);
                        metai = selected_laikotarpis.Year;
                        menuo = selected_laikotarpis.Month;
                    }

                    DataTable namu_darbai = Dienynas.getND(int.Parse(session_user.Rows[0]["klase"].ToString()), selected_dalykas, metai, menuo);
                    int rows = namu_darbai.Rows.Count;
                    for (int i = 0; i < rows; i++)
                    {
                        DateTime data = DateTime.Parse(namu_darbai.Rows[i]["data"].ToString());
                        nd_list2.Rows.Add();
                        nd_list2.Rows[i].Cells["grid_data3"].Value = data.ToString("yyyy-MM-dd");
                        nd_list2.Rows[i].Cells["grid_dalykas"].Value = namu_darbai.Rows[i]["dal"].ToString();
                        nd_list2.Rows[i].Cells["grid_namu_darbas2"].Value = namu_darbai.Rows[i]["namu_darbas"].ToString();
                        nd_list2.Rows[i].Height = 40;
                        namu_darbai_ids.Add(i, int.Parse(namu_darbai.Rows[i]["id"].ToString()));
                    }

                    if (rows > 0)
                    {
                        nd_list2.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        nd_list2.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                        nd_list2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    }
                    else
                        MessageBox.Show("Namų darbų neužduota", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Prašome pasirinkti laikotarpį!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Prašome pasirinkti dalyką!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}