using System;
using Microsoft.AspNetCore.Identity;

public class Encryption
{
    public Encryption()
    { }

    public static string HashPassword(string username, string password)
    {
        var ph = new PasswordHasher<string>();
        return ph.HashPassword(username, password);
    }

    public static bool VerifyHashedPassword(string username, string currentHashedPassword, string password)
    {
        var ph = new PasswordHasher<string>();
        try
        {
            var res = ph.VerifyHashedPassword(username, currentHashedPassword, password);
            return res == PasswordVerificationResult.Success ? true : false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
