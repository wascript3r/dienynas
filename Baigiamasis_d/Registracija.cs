using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baigiamasis_d
{
    public partial class Registracija : Form
    {
        public Registracija()
        {
            InitializeComponent();
        }

        private void username_Enter(object sender, EventArgs e)
        {
            if (username.Text == "Vartotojo vardas")
                username.Text = "";
        }

        private void username_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(username.Text))
                username.Text = "Vartotojo vardas";
        }

        private void email_Enter(object sender, EventArgs e)
        {
            if (email.Text == "El. paštas")
                email.Text = "";
        }

        private void email_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(email.Text))
                email.Text = "El. paštas";
        }

        private void slaptazodis_Enter(object sender, EventArgs e)
        {
            if (slaptazodis.Text == "Slaptažodis")
            {
                slaptazodis.Text = "";
                slaptazodis.UseSystemPasswordChar = true;
            }
        }

        private void slaptazodis_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(slaptazodis.Text))
            {
                slaptazodis.Text = "Slaptažodis";
                slaptazodis.UseSystemPasswordChar = false;
            }
        }

        private void slaptazodis2_Enter(object sender, EventArgs e)
        {
            if (slaptazodis2.Text == "Pakartokite slaptažodį")
            {
                slaptazodis2.Text = "";
                slaptazodis2.UseSystemPasswordChar = true;
            }
        }

        private void slaptazodis2_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(slaptazodis2.Text))
            {
                slaptazodis2.Text = "Pakartokite slaptažodį";
                slaptazodis2.UseSystemPasswordChar = false;
            }
        }

        private void regkodas_Enter(object sender, EventArgs e)
        {
            if (regkodas.Text == "Registracijos kodas")
                regkodas.Text = "";
        }

        private void regkodas_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(regkodas.Text))
                regkodas.Text = "Registracijos kodas";
        }

        private void prisijungimas_Click(object sender, EventArgs e)
        {
            // Pereina į prisijungimo formą
            this.Hide();
            var form = new Prisijungimas();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void Registracija_Load(object sender, EventArgs e)
        {
            Database_class.Connect(); // Prisijungia prie duomenų bazės
        }

        private void registruotis_Click(object sender, EventArgs e)
        {
            if (username.Text != "Vartotojo vardas" && email.Text != "El. paštas" && slaptazodis.Text != "Slaptažodis" && slaptazodis2.Text != "Pakartokite slaptažodį" && regkodas.Text != "Registracijos kodas")
            {
                Registracija_class Registracija = new Registracija_class();
                try
                {
                    Registracija.Register(username.Text, email.Text, slaptazodis.Text, slaptazodis2.Text, regkodas.Text);
                    MessageBox.Show("Sėkmingai užsiregistravote!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Pereina į prisijungimo formą
                    this.Hide();
                    var form = new Prisijungimas();
                    form.Closed += (s, args) => this.Close();
                    form.Show();
                }
                catch (Klaida klaida)
                {
                    MessageBox.Show(klaida.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Prašome užpildyti visus laukelius!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
