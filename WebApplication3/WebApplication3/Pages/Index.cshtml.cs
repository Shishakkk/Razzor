using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication3.Pages
{
    public class IndexModel : PageModel
    {
        public string? Password { get; set; }
        public string? Hash { get; set; }
        public List<string> Errors { get; set; } = new();
        public bool IsStrong { get; set; }

        public int Length { get; set; } = 12;
        public bool UseUpper { get; set; } = true;
        public bool UseLower { get; set; } = true;
        public bool UseDigits { get; set; } = true;
        public bool UseSpecial { get; set; } = true;

        public void OnGet() { }

        public void OnPost(
            int length,
            bool useUpper,
            bool useLower,
            bool useDigits,
            bool useSpecial)
        {
            Length = length;
            UseUpper = useUpper;
            UseLower = useLower;
            UseDigits = useDigits;
            UseSpecial = useSpecial;

            
            var alphabet = new StringBuilder();
            if (useUpper) alphabet.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            if (useLower) alphabet.Append("abcdefghijklmnopqrstuvwxyz");
            if (useDigits) alphabet.Append("0123456789");
            if (useSpecial) alphabet.Append("!@#$%^&*()-_=+[]{};:,.?");

            if (alphabet.Length == 0)
            {
                Errors.Add("Выберите хотя бы один набор символов.");
                return;
            }
            if (length < 6 || length > 64)
            {
                Errors.Add("Длина пароля должна быть от 6 до 64 символов.");
                return;
            }

            
            var chars = new char[length];
            for (int i = 0; i < length; i++)
                chars[i] = alphabet[RandomNumberGenerator.GetInt32(alphabet.Length)];
            var body = new string(chars);

            
            Hash = ComputeHash(body)[..8];
            Password = Hash + body + Hash;

           
            CheckStrength(body);
        }

        private static string ComputeHash(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input + Guid.NewGuid()));
            return Convert.ToHexString(bytes);
        }

        private void CheckStrength(string pwd)
        {
            bool hasUpper = pwd.Any(char.IsUpper);
            bool hasLower = pwd.Any(char.IsLower);
            bool hasDigit = pwd.Any(char.IsDigit);
            bool hasSpecial = pwd.Any(c => !char.IsLetterOrDigit(c));
            bool longEnough = pwd.Length >= 10;
            bool noRepeats = pwd.Distinct().Count() >= pwd.Length * 0.6;

            if (!hasUpper) Errors.Add("Нет заглавных букв.");
            if (!hasLower) Errors.Add("Нет строчных букв.");
            if (!hasDigit) Errors.Add("Нет цифр.");
            if (!hasSpecial) Errors.Add("Нет специальных символов.");
            if (!longEnough) Errors.Add("Длина меньше 10 символов.");
            if (!noRepeats) Errors.Add("Слишком много повторяющихся символов.");

            IsStrong = Errors.Count == 0;
        }
    }
}
