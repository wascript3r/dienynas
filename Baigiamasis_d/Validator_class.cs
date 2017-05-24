using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Baigiamasis_d
{
    class Validator_class
    {
        // Patikrina, ar nurodytas el. paštas yra teisingas
        public static bool isEmail(string email)
        {
            try
            {
                Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                return rx.IsMatch(email);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        // Patikrina, ar nurodytas string'as yra skaičius
        public static bool isInt(string input)
        {
            int number;
            return int.TryParse(input, out number);
        }
    }
}
