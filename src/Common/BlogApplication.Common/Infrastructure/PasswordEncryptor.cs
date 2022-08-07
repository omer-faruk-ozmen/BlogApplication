using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Infrastructure;

public static class PasswordEncryptor
{
    public static string Encrypt(string password)
    {
        using var md5 = MD5.Create();
        using var sha256 = SHA256.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(password);
        byte[] shaBytes = sha256.ComputeHash(inputBytes);
        byte[] hashBytes = md5.ComputeHash(shaBytes);


        return Convert.ToHexString(hashBytes);
    }
}