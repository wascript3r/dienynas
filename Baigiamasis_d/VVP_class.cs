using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Baigiamasis_d
{
    class VVP_class
    {
        public string username; // Vartotojo vardas
        public int userid; // Vartotojo id

        // Konstruktorius
        public VVP_class(string username = "", int userid = 0)
        {
            this.username = username;
            this.userid = userid;
        }

        // Išgauna vartotojo informaciją pagal vartotojo vardą arba id
        public DataTable getUser()
        {
            MySqlCommand query = new MySqlCommand("SELECT v.*, z.vardas, z.pavarde "
                                                + "FROM vartotojai v "
                                                + "LEFT JOIN zmones z ON z.id = v.zmogus "
                                                + ((username != "") ? "WHERE v.username = @username " : "WHERE v.zmogus = @zmogus "), Database_class.conn);
            query.Parameters.AddWithValue("@username", username);
            query.Parameters.AddWithValue("@zmogus", userid);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            DataTable user = new DataTable();
            user.Load(row);
            row.Close();
            return user;
        }

        // Išgauna mokinio pažymius iš duomenų bazės
        public DataTable getDiary(int year, int month)
        {
            MySqlCommand query = new MySqlCommand("SELECT paz.pazymys, paz.data, dal.id, dal.dalykas "
                                                + "FROM vartotojai v "
                                                + "LEFT JOIN pazymiai paz ON paz.vartotojoid = v.id "
                                                + "LEFT JOIN dalykai dal ON dal.id = paz.dalykas "
                                                + ((username != "") ? "WHERE v.username = @username " : "WHERE v.zmogus = @zmogus ")
                                                + "AND YEAR(paz.data) = @year "
                                                + "AND MONTH(paz.data) = @month "
                                                + "ORDER BY dal.dalykas ASC", Database_class.conn);
            query.Parameters.AddWithValue("@username", username);
            query.Parameters.AddWithValue("@zmogus", userid);
            query.Parameters.AddWithValue("@year", year);
            query.Parameters.AddWithValue("@month", month);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            DataTable diary = new DataTable();
            diary.Load(row);
            row.Close();
            return diary;
        }

        // Išgauna mokinio pažymius iš duomenų bazės ir pateikia juos mokytojui
        public DataTable getDiary2(int mokinys, int dalykas)
        {
            MySqlCommand query = new MySqlCommand("SELECT * FROM pazymiai WHERE vartotojoid = @vartotojoid AND dalykas = @dalykas ORDER BY data DESC", Database_class.conn);
            query.Parameters.AddWithValue("@vartotojoid", mokinys);
            query.Parameters.AddWithValue("@dalykas", dalykas);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            DataTable diary = new DataTable();
            diary.Load(row);
            row.Close();
            return diary;
        }

        // Išgauna mokinio namų darbus iš duomenų bazės
        public DataTable getND(int klase, int dalykas, int year = 0, int month = 0)
        {
            MySqlCommand query = new MySqlCommand("SELECT nd.*, dal.dalykas AS dal "
                                                + "FROM namu_darbai nd "
                                                + "JOIN dalykai dal ON dal.id = nd.dalykas "
                                                + "WHERE nd.klase = @klase "
                                                + ((dalykas > 0) ? "AND nd.dalykas = @dalykas " : "")
                                                + ((year > 0) ? "AND YEAR(nd.data) = @year " : "")
                                                + ((month > 0) ? "AND MONTH(nd.data) = @month " : "")
                                                + "ORDER BY data DESC", Database_class.conn);
            query.Parameters.AddWithValue("@klase", klase);
            query.Parameters.AddWithValue("@dalykas", dalykas);
            query.Parameters.AddWithValue("@year", year);
            query.Parameters.AddWithValue("@month", month);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            DataTable nd = new DataTable();
            nd.Load(row);
            row.Close();
            return nd;
        }

        // Išgauna mokinio vidurkį iš duomenų bazės
        public double Vidurkis(int dalykas = 0, int year = 0, int month = 0)
        {
            MySqlCommand query = new MySqlCommand("SELECT AVG(paz.pazymys) AS vidurkis "
                                                + "FROM vartotojai v "
                                                + "LEFT JOIN pazymiai paz ON paz.vartotojoid = v.id "
                                                + "LEFT JOIN dalykai dal ON dal.id = paz.dalykas "
                                                + ((username != "") ? "WHERE v.username = @username " : "WHERE v.zmogus = @zmogus ")
                                                + "AND paz.pazymys REGEXP '^[0-9]+$'"
                                                + ((dalykas > 0) ? "AND paz.dalykas = @dalykas " : "")
                                                + ((year > 0) ? "AND YEAR(paz.data) = @year " : "")
                                                + ((month > 0) ? "AND MONTH(paz.data) = @month" : ""), Database_class.conn);
            query.Parameters.AddWithValue("@username", username);
            query.Parameters.AddWithValue("@zmogus", userid);
            query.Parameters.AddWithValue("@dalykas", dalykas);
            query.Parameters.AddWithValue("@year", year);
            query.Parameters.AddWithValue("@month", month);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            double vidurkis = 0;
            if (row.HasRows)
            {
                row.Read();
                vidurkis = (row.IsDBNull(0)) ? 0 : row.GetDouble("vidurkis");
            }
            row.Close();
            return vidurkis;
        }

        // Išgauna visus dėstomus dalykus
        public DataTable Dalykai()
        {
            MySqlCommand query = new MySqlCommand("SELECT * FROM dalykai ORDER BY dalykas ASC", Database_class.conn);
            MySqlDataReader row = query.ExecuteReader();
            DataTable dalykai = new DataTable();
            dalykai.Load(row);
            row.Close();
            return dalykai;
        }

        // Išgauna visas klases, esančias mokykloje
        public DataTable Klases()
        {
            MySqlCommand query = new MySqlCommand("SELECT * FROM klases ORDER BY klase ASC", Database_class.conn);
            MySqlDataReader row = query.ExecuteReader();
            DataTable klases = new DataTable();
            klases.Load(row);
            row.Close();
            return klases;
        }

        // Išgauna vartotojo id pagal vardą ir pavardę
        public int getParent(string vardas, string pavarde)
        {
            MySqlCommand query = new MySqlCommand("SELECT * FROM zmones WHERE vardas = @vardas AND pavarde = @pavarde ORDER BY id DESC", Database_class.conn);
            query.Parameters.AddWithValue("@vardas", vardas);
            query.Parameters.AddWithValue("@pavarde", pavarde);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            if (row.HasRows)
            {
                row.Read();
                int id = row.GetInt32("id");
                row.Close();
                return id;
            }
            else
            {
                row.Close();
                Helper_class.SistemosKlaida();
            }
            return 0;
        }

        // Sugeneruoja registracijos kodą
        public string generateCode(string vardas, string pavarde, int tipas = 0, int klase = 0, int parent = 0, int dalykas = 0)
        {
            string kodas = Helper_class.randomString();
            MySqlCommand sql = new MySqlCommand("INSERT INTO zmones (vardas, pavarde) VALUES (@vardas, @pavarde)", Database_class.conn);
            sql.Parameters.AddWithValue("@vardas", vardas);
            sql.Parameters.AddWithValue("@pavarde", pavarde);
            sql.Prepare();
            sql.ExecuteNonQuery();
            long lastId = sql.LastInsertedId;

            sql = new MySqlCommand("INSERT INTO kodai (kodas, zmogus, tipas, klase, parent, dalykas) VALUES (@kodas, @zmogus, @tipas, @klase, @parent, @dalykas)", Database_class.conn);
            sql.Parameters.AddWithValue("@kodas", kodas);
            sql.Parameters.AddWithValue("@zmogus", lastId);
            sql.Parameters.AddWithValue("@tipas", tipas);
            sql.Parameters.AddWithValue("@klase", klase);
            sql.Parameters.AddWithValue("@parent", parent);
            sql.Parameters.AddWithValue("@dalykas", dalykas);
            sql.Prepare();
            sql.ExecuteNonQuery();
            return kodas;
        }

        // Išgauna mokinių sąrašą pagal klasę
        public DataTable MokiniuList(int klase = 0)
        {
            MySqlCommand query;

            if (klase == 0)
                query = new MySqlCommand("SELECT v.id, z.vardas, z.pavarde "
                                                    + "FROM vartotojai v "
                                                    + "LEFT JOIN zmones z ON z.id = v.zmogus "
                                                    + "WHERE v.tipas = 0 "
                                                    + "AND v.busena = 1 "
                                                    + "ORDER BY z.vardas ASC, z.pavarde ASC", Database_class.conn);
            else
            {
                query = new MySqlCommand("SELECT v.id, z.vardas, z.pavarde "
                                                    + "FROM vartotojai v "
                                                    + "LEFT JOIN zmones z ON z.id = v.zmogus "
                                                    + "WHERE v.tipas = 0 "
                                                    + "AND v.busena = 1 "
                                                    + "AND klase = @klase "
                                                    + "ORDER BY z.vardas ASC, z.pavarde ASC", Database_class.conn);
                query.Parameters.AddWithValue("@klase", klase);
                query.Prepare();
            }

            MySqlDataReader row = query.ExecuteReader();
            DataTable users = new DataTable();
            users.Load(row);
            row.Close();
            return users;
        }

        // Išgauna tėvų sąrašą
        public DataTable TevuList()
        {
            MySqlCommand query = new MySqlCommand("SELECT v.id, z.vardas, z.pavarde "
                                                + "FROM vartotojai v "
                                                + "LEFT JOIN zmones z ON z.id = v.zmogus "
                                                + "WHERE v.tipas = 1 "
                                                + "ORDER BY z.vardas ASC, z.pavarde ASC", Database_class.conn);
            MySqlDataReader row = query.ExecuteReader();
            DataTable users = new DataTable();
            users.Load(row);
            row.Close();
            return users;
        }

        // Išgauna mokytojų sąrašą
        public DataTable MokytojuList()
        {
            MySqlCommand query = new MySqlCommand("SELECT v.id, z.vardas, z.pavarde "
                                                + "FROM vartotojai v "
                                                + "LEFT JOIN zmones z ON z.id = v.zmogus "
                                                + "WHERE v.tipas = 2 "
                                                + "AND v.busena = 1 "
                                                + "ORDER BY z.vardas ASC, z.pavarde ASC", Database_class.conn);
            MySqlDataReader row = query.ExecuteReader();
            DataTable users = new DataTable();
            users.Load(row);
            row.Close();
            return users;
        }

        // Ištrina vartotoją
        public void Trinti(int id = 0)
        {
            MySqlCommand sql = new MySqlCommand("UPDATE vartotojai SET busena = 0 WHERE id = @id", Database_class.conn);
            sql.Parameters.AddWithValue("@id", id);
            sql.Prepare();
            sql.ExecuteNonQuery();
        }

        // Prideda pažymį į duomenų bazę
        public void PridetiPazymi(int mokinys, int dalykas, string pazymys, string data)
        {
            MySqlCommand sql = new MySqlCommand("INSERT INTO pazymiai (vartotojoid, dalykas, pazymys, data) VALUES (@vartotojoid, @dalykas, @pazymys, @data)", Database_class.conn);
            sql.Parameters.AddWithValue("@vartotojoid", mokinys);
            sql.Parameters.AddWithValue("@dalykas", dalykas);
            sql.Parameters.AddWithValue("@pazymys", pazymys);
            sql.Parameters.AddWithValue("@data", data);
            sql.Prepare();
            sql.ExecuteNonQuery();
        }

        // Ištrina pažymį
        public void TrintiPazymi(int id)
        {
            MySqlCommand sql = new MySqlCommand("DELETE FROM pazymiai WHERE id = @id", Database_class.conn);
            sql.Parameters.AddWithValue("@id", id);
            sql.Prepare();
            sql.ExecuteNonQuery();
        }

        // Atnaujina pažymį
        public void AtnaujintiPazymi(int id, string value)
        {
            MySqlCommand sql = new MySqlCommand("UPDATE pazymiai SET pazymys = @pazymys WHERE id = @id", Database_class.conn);
            sql.Parameters.AddWithValue("@pazymys", value);
            sql.Parameters.AddWithValue("@id", id);
            sql.Prepare();
            sql.ExecuteNonQuery();
        }

        // Prideda namų darbą
        public void PridetiND(int klase, int dalykas, string namu_darbas, string data)
        {
            MySqlCommand sql = new MySqlCommand("INSERT INTO namu_darbai (klase, dalykas, namu_darbas, data) VALUES (@klase, @dalykas, @namu_darbas, @data)", Database_class.conn);
            sql.Parameters.AddWithValue("@klase", klase);
            sql.Parameters.AddWithValue("@dalykas", dalykas);
            sql.Parameters.AddWithValue("@namu_darbas", namu_darbas);
            sql.Parameters.AddWithValue("@data", data);
            sql.Prepare();
            sql.ExecuteNonQuery();
        }

        // Ištrina namų darbą
        public void TrintiND(int id)
        {
            MySqlCommand sql = new MySqlCommand("DELETE FROM namu_darbai WHERE id = @id", Database_class.conn);
            sql.Parameters.AddWithValue("@id", id);
            sql.Prepare();
            sql.ExecuteNonQuery();
        }

        // Atnaujina namų darbą
        public void AtnaujintiND(int id, string value)
        {
            MySqlCommand sql = new MySqlCommand("UPDATE namu_darbai SET namu_darbas = @namu_darbas WHERE id = @id", Database_class.conn);
            sql.Parameters.AddWithValue("@namu_darbas", value);
            sql.Parameters.AddWithValue("@id", id);
            sql.Prepare();
            sql.ExecuteNonQuery();
        }

        // Patikrina, ar tą dieną jau egizistuoja pažymys iš konkretaus dalyko
        public bool EgiztuojaPazymys(int mokinys, int dalykas, string data)
        {
            bool egiztuoja = true;
            MySqlCommand query = new MySqlCommand("SELECT * FROM pazymiai WHERE vartotojoid = @vartotojoid AND dalykas = @dalykas AND data = @data", Database_class.conn);
            query.Parameters.AddWithValue("@vartotojoid", mokinys);
            query.Parameters.AddWithValue("@dalykas", dalykas);
            query.Parameters.AddWithValue("@data", data);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            if (!row.HasRows)
                egiztuoja = false;
            row.Close();
            return egiztuoja;
        }

        // Patikrina, ar tą dieną jau yra užduoti namų darbai iš konkretaus dalyko
        public bool EgiztuojaND(int klase, int dalykas, string data)
        {
            bool egiztuoja = true;
            MySqlCommand query = new MySqlCommand("SELECT * FROM namu_darbai WHERE klase = @klase AND dalykas = @dalykas AND data = @data", Database_class.conn);
            query.Parameters.AddWithValue("@klase", klase);
            query.Parameters.AddWithValue("@dalykas", dalykas);
            query.Parameters.AddWithValue("@data", data);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            if (!row.HasRows)
                egiztuoja = false;
            row.Close();
            return egiztuoja;
        }

        // Išgauna dalyko pavadinimą pagal id
        public string DalykoPavadinimas(int id)
        {
            string pav = "-";
            MySqlCommand query = new MySqlCommand("SELECT * FROM dalykai WHERE id = @id", Database_class.conn);
            query.Parameters.AddWithValue("@id", id);
            query.Prepare();
            MySqlDataReader row = query.ExecuteReader();
            if (row.HasRows)
            {
                row.Read();
                pav = row.GetString("dalykas");
            }
            row.Close();
            return pav;
        }
    }
}
