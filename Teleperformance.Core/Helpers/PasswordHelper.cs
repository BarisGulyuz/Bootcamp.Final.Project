using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Core.Helpers
{
    public class PasswordHelper
    {
        public static string GenerateRandomPassword(PasswordSize passwordSize)
        {
            int passwordLength = 0;
            string passwordCharacters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789!@#$%^&*";
            switch (passwordSize)
            {
                case PasswordSize.Medium:
                    passwordLength = 8;
                    break;
                case PasswordSize.Strong:
                    passwordLength = 12;
                    break;
                case PasswordSize.TooStrong:
                    passwordLength = 16;
                    break;
            }
            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i <= passwordLength; i++)
            {
                int index = random.Next(i, passwordCharacters.Length);
                passwordBuilder.Append(passwordCharacters[index]);
            }
            return passwordBuilder.ToString();
        }

        public enum PasswordSize
        {
            Medium = 1,
            Strong,
            TooStrong
        }
    }
}
