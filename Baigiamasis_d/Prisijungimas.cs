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
    public partial class Prisijungimas : Form
    {
        public Prisijungimas()
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

        private void prisijungti_Click(object sender, EventArgs e)
        {
            if (username.Text != "Vartotojo vardas" && slaptazodis.Text != "Slaptažodis")
            {
                Prisijungimas_class Prisijungimas = new Prisijungimas_class();
                bool valid = Prisijungimas.Validate(username.Text, slaptazodis.Text);
                if (valid)
                {
                    this.Hide();
                    var form = new VVP();
                    form.session_username = username.Text;
                    form.Closed += (s, args) => this.Close();
                    form.Show();
                }
                else
                {
                    slaptazodis.UseSystemPasswordChar = false;
                    slaptazodis.Text = "Slaptažodis";
                    MessageBox.Show("Neteisingas vartotojo vardas arba slaptažodis!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Prašome užpildyti visus laukelius!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Prisijungimas_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Database_class.Connect(); // Prisijungia prie duomenų bazės
        }

        private void registracija_Click(object sender, EventArgs e)
        {
            // Pereina į registracijos formą
            this.Hide();
            var form = new Registracija();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void monoFlat_LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Pereina į pradžios formą
            this.Hide();
            var form = new Pradzia();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }
}
