using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Baigiamasis_d
{
    class Database_class
    {
        public static MySqlConnection conn = null;

        // Atsijungimas nuo duomenų bazės
        public static void Close()
        {
            if (conn != null)
                conn.Close();
        }

        // Prisijungimas prie duomenų bazės
        public static void Connect()
        {
            try
            {
                if (conn == null)
                {
                    conn = new MySqlConnection("server=localhost;Database=dienynas;Uid=root;password=;CharSet=utf8;");
                    conn.Open();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nepavyko prisijungti prie duomenų bazės!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }
    }
}
