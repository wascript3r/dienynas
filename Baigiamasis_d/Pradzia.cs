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
    public partial class Pradzia : Form
    {
        public Pradzia()
        {
            InitializeComponent();
        }

        private void pradeti_Click(object sender, EventArgs e)
        {
            // Pereina į prisijungimo formą
            this.Hide();
            var form = new Prisijungimas();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }
}
