using System.Text.RegularExpressions;

namespace MovieStore.Services.Validators
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;
            
            cpf = cpf.Replace(".", "").Replace("-", "").Trim();
            
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                return false;
            
            if (cpf.All(c => c == cpf[0]))
                return false;
            
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * (10 - i);

            int remainder = sum % 11;
            int firstDigit = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(cpf[9].ToString()) != firstDigit)
                return false;
            
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * (11 - i);

            remainder = sum % 11;
            int secondDigit = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cpf[10].ToString()) == secondDigit;
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;
            
            phone = phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
            
            if (phone.Length != 10 && phone.Length != 11)
                return false;
            
            if (!phone.All(char.IsDigit))
                return false;
            
            if (phone.Length == 11)
            {
                var ddd = phone.Substring(0, 2);
                var ninthDigit = phone[2];
                
                return IsValidDDD(ddd) && ninthDigit == '9';
            }
            else
            {
                var ddd = phone.Substring(0, 2);
                return IsValidDDD(ddd);
            }
        }

        private static bool IsValidDDD(string ddd)
        {
            var validDDDs = new[]
            {
                "11", "12", "13", "14", "15", "16", "17", "18", "19", // SP
                "21", "22", "24", // RJ
                "27", "28", // ES
                "31", "32", "33", "34", "35", "37", "38", // MG
                "41", "42", "43", "44", "45", "46", // PR
                "47", "48", "49", // SC
                "51", "53", "54", "55", // RS
                "61", // DF
                "62", "64", // GO
                "63", // TO
                "65", "66", // MT
                "67", // MS
                "68", // AC
                "69", // RO
                "71", "73", "74", "75", "77", // BA
                "79", // SE
                "81", "87", // PE
                "82", // AL
                "83", // PB
                "84", // RN
                "85", "88", // CE
                "86", "89", // PI
                "91", "93", "94", // PA
                "92", "97", // AM
                "95", // RR
                "96", // AP
                "98", "99" // MA
            };

            return validDDDs.Contains(ddd);
        }

        public static string FormatCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return cpf;

            cpf = cpf.Replace(".", "").Replace("-", "").Trim();
            
            if (cpf.Length == 11)
            {
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
            }

            return cpf;
        }

        public static string FormatPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return phone;

            phone = phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

            if (phone.Length == 11)
            {
                return $"({phone.Substring(0, 2)}) {phone.Substring(2, 5)}-{phone.Substring(7, 4)}";
            }
            else if (phone.Length == 10)
            {
                return $"({phone.Substring(0, 2)}) {phone.Substring(2, 4)}-{phone.Substring(6, 4)}";
            }

            return phone;
        }
    }
}
