using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public static class PasswordValidation
    {
        public static bool CheckPasswordComplexity(string password,
            PasswordPolicy passwordPolicy)
        {

            if (password.Length < passwordPolicy.MinimumPwdLength
                || password.Length > passwordPolicy.MaximumPwdLength)
            {
                return false;
            }

            switch (passwordPolicy.PwdContains)
            {
                case 1:
                    {
                        return Regex.IsMatch(password, @"^[a-zA-Z]+$");
                    }
                case 2:
                    {
                        return Regex.IsMatch(password, @"^[0-9]+$");
                    }
                case 3:
                    {
                        return Regex.IsMatch(password, @"^[a-zA-Z0-9]+$");
                    }
                case 4:
                    {
                        return Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-zA-Z])(?!.*\s)[0-9a-zA-Z]*$");
                    }
                case 5:
                    {
                        return Regex.IsMatch(password, @"^(?=.*[0-9])(?=.*[!@#$%^_&*])(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9!@#$%^_&*]{1,106}$");
                    }

                default:
                    {
                        return true;
                    }

            }
        }
    }
}
