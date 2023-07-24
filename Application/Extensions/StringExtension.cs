﻿using System.Text;
using System.Security.Cryptography;

namespace Application.Extensions;

public static class StringExtension
{
    public static string GetHashString(this string text)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashbytes = sha256.ComputeHash(bytes);
            text = Convert.ToBase64String(hashbytes);

        }
        return text;
    }
}
