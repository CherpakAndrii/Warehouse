using System.Reflection;
using Models.DBModels;

namespace Infrastructure.Services;

[Obfuscation]
public class PasswordDecryptor
{
    private const long InternalPassword = 5612389589465625739;

    public bool CheckPassword(User user, string password)
    {
        char[] encryptedPassword = user.EncryptedPassword.ToCharArray();
        char[] pass1 = user.Login.ToCharArray();
        char[] pass2 = InternalPassword.ToString().ToCharArray();
        string decryptedPassword = "";

        for (int i = 0, m = 1; i < encryptedPassword.Length; i++, m *= -1)
        {
            char c = (char)(byte)(encryptedPassword[i] + m * (pass1[i % pass1.Length] - pass2[i % pass2.Length]));
            decryptedPassword += c;
        }

        return password == decryptedPassword;
    }
    
    public string EncryptPassword(string userLogin, string password)
    {
        char[] userPassword = password.ToCharArray();
        char[] pass1 = userLogin.ToCharArray();
        char[] pass2 = InternalPassword.ToString().ToCharArray();
        string encryptedPassword = "";

        for (int i = 0, m = -1; i < userPassword.Length; i++, m *= -1)
        {
            char c = (char)(byte)(userPassword[i] + m * (pass1[i % pass1.Length] - pass2[i % pass2.Length]));
            encryptedPassword += c;
        }

        return encryptedPassword;
    }

    public string ReencryptPasswordOnLoginUpdate(string oldUserLogin, string newUserLogin, string oldEncryptedPassword)
    {
        char[] pass1 = oldUserLogin.ToCharArray();
        char[] pass2 = InternalPassword.ToString().ToCharArray();
        string oldPass = "";
        
        for (int i = 0, m = 1; i < oldEncryptedPassword.Length; i++, m *= -1)
        {
            char c = (char)(byte)(oldEncryptedPassword[i] + m * (pass1[i % pass1.Length] - pass2[i % pass2.Length]));
            oldPass += c;
        }

        return EncryptPassword(newUserLogin, oldPass);
    }
}