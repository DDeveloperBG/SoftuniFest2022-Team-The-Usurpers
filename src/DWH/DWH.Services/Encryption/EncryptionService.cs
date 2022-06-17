using System.Security.Cryptography;
using System.Text;

namespace DWH.Services.EncryptText
{
    public class EncryptionService : IEncryptionService
    {
        public string EncryptText(string text)
        {
            byte[] textAsBytes = Encoding.ASCII.GetBytes(text);
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] encodedText = mySHA256.ComputeHash(textAsBytes);
                return Encoding.ASCII.GetString(encodedText);
            }
        }
    }
}
