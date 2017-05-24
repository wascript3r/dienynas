using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Baigiamasis_d
{
    class Helper_class
    {
        // Išmeta sistemos klaidą ir išjungia programą
        public static void SistemosKlaida()
        {
            MessageBox.Show("Įvyko sistemos klaida!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Application.Exit();
        }

        // Sugeneruoja atsitiktines raides
        public static string randomString(int length = 10)
        {
            Random rnd = new Random();
            var otp = string.Concat(Enumerable.Range(0, length)
                                              .Select(i => (char)(rnd.Next(26) + 'A')));
            return otp;
        }

        // Dabartinės datos gavimas iš MYSQL
        public static DateTime currentDate()
        {
            MySqlCommand query = new MySqlCommand("SELECT CURDATE() AS date", Database_class.conn);
            MySqlDataReader row = query.ExecuteReader();
            row.Read();
            DateTime date = DateTime.Parse(row.GetString("date"));
            row.Close();
            return date;
        }

        // Randa, kiek tam tikras mėnesis turi dienų
        public static int monthDays(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }
    }
}
