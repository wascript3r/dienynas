using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Baigiamasis_d
{
    class Prisijungimas_class
    {
        // Patikrina, ar vartotojo įvesti duomenys yra teisingi
        public bool Validate(string username, string slaptazodis)
        {
            bool valid = false;
            MySqlCommand query = new MySqlCommand("SELECT * FROM vartotojai WHERE username = @username AND busena = 1", Database_class.conn);
            query.Parameters.AddWithValue("@username", username);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            if (row.HasRows)
            {
                row.Read();
                string hash = row.GetString("slaptazodis");
                if (Hash_class.Validate(slaptazodis, hash))
                    valid = true;
            }
            row.Close();
            return valid;
        }
    }
}
