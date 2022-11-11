using BusinessLogicLayer.Services.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Validators
{
    public class UserRegistrationValidator
    {
        public static bool IsValidName(string value)
        {
            if (value.Any(char.IsDigit))
                return false;
            return true;
        }
        public static bool IsValidEmail(string value)
        {
            bool check = Regex.
                IsMatch(value, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (!check)
                return false;
            return true;
        }

        public static bool IsValidPassword(string value)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            bool check = hasNumber.IsMatch(value) && hasUpperChar.IsMatch(value) && hasMinimum8Chars.IsMatch(value);
            if (!check)
                return false;
            return true;
        }
    }
}
