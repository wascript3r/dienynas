using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Baigiamasis_d
{
    class Registracija_class
    {
        // Užregistruoja vartotoją
        public void Register(string username, string email, string slaptazodis, string slaptazodis2, string regkodas)
        {
            if (slaptazodis != slaptazodis2)
                throw new Klaida("Slaptažodžiai nesutampa!");
            else if (!Validator_class.isEmail(email))
                throw new Klaida("Neteisingas el. pašto formatas!");
            else if (EgzistuojaUsername(username))
                throw new Klaida("Toks vartotojo vardas jau egzistuoja!");
            else if (EgzistuojaEmail(email))
                throw new Klaida("Toks el. paštas jau egzistuoja!");

            DataTable kodas = Kodas(regkodas);
            if (kodas.Rows.Count == 0)
                throw new Klaida("Neteisingas registracijos kodas!");
            PanaudotasKodas(regkodas);

            MySqlCommand sql = new MySqlCommand("INSERT INTO vartotojai (zmogus, username, email, slaptazodis, tipas, klase, parent, dalykas) VALUES (@zmogus, @username, @email, @slaptazodis, @tipas, @klase, @parent, @dalykas)", Database_class.conn);
            sql.Parameters.AddWithValue("@zmogus", int.Parse(kodas.Rows[0]["zmogus"].ToString()));
            sql.Parameters.AddWithValue("@username", username);
            sql.Parameters.AddWithValue("@email", email);
            sql.Parameters.AddWithValue("@slaptazodis", Hash_class.Create(slaptazodis));
            sql.Parameters.AddWithValue("@tipas", int.Parse(kodas.Rows[0]["tipas"].ToString()));
            sql.Parameters.AddWithValue("@klase", int.Parse(kodas.Rows[0]["klase"].ToString()));
            sql.Parameters.AddWithValue("@parent", int.Parse(kodas.Rows[0]["parent"].ToString()));
            sql.Parameters.AddWithValue("@dalykas", int.Parse(kodas.Rows[0]["dalykas"].ToString()));
            sql.Prepare();
            sql.ExecuteNonQuery();
        }

        // Patikrina, ar egzistuoja vartotojas su nurodytu vartotojo vardu
        public bool EgzistuojaUsername(string username)
        {
            bool exists = true;
            MySqlCommand query = new MySqlCommand("SELECT * FROM vartotojai WHERE username = @username", Database_class.conn);
            query.Parameters.AddWithValue("@username", username);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            if (!row.HasRows)
                exists = false;
            row.Close();
            return exists;
        }

        // Patikrina, ar egzistuoja vartotojas su nurodytu el. paštu
        public bool EgzistuojaEmail(string email)
        {
            bool exists = true;
            MySqlCommand query = new MySqlCommand("SELECT * FROM vartotojai WHERE email = @email", Database_class.conn);
            query.Parameters.AddWithValue("@email", email);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            if (!row.HasRows)
                exists = false;
            row.Close();
            return exists;
        }

        // Patikrina, ar registracijos kodas yra teisingas ir nepanaudotas
        public DataTable Kodas(string regkodas)
        {
            MySqlCommand query = new MySqlCommand("SELECT * FROM kodai WHERE kodas = @kodas AND busena = 0", Database_class.conn);
            query.Parameters.AddWithValue("@kodas", regkodas);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            DataTable result = new DataTable();
            result.Load(row);
            row.Close();
            return result;
        }

        // Nustato registracijos kodą kaip panaudotą
        public void PanaudotasKodas(string regkodas)
        {
            MySqlCommand sql = new MySqlCommand("UPDATE kodai SET busena = 1 WHERE kodas = @kodas", Database_class.conn);
            sql.Parameters.AddWithValue("@kodas", regkodas);
            sql.Prepare();
            sql.ExecuteNonQuery();
        }
    }
}
