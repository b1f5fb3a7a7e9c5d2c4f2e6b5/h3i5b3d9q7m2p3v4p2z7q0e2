using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public static class DPAPI
{
    public static string Encrypt(string text)
    {
        return Convert.ToBase64String(
            ProtectedData.Protect(
                Encoding.Unicode.GetBytes(text),
                new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 }, // test-> DataPage.Entropy
                DataProtectionScope.LocalMachine));
    }

    public static string Decrypt(string text)
    {
        try
        {
            return Encoding.Unicode.GetString(
            ProtectedData.Unprotect(
                 Convert.FromBase64String(text),
                new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 }, // test-> DataPage.Entropy
                 DataProtectionScope.LocalMachine));
        }
        catch (Exception ex)
        {
            return $"Error ({ex.Source}): {ex.Message}";
        }
        
    }
}