using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public static class EnDecController
{
    private const string initVector = "tu89geji340t89u2";
    private const string Key = "eL01k3yP2w0rd";
    private const int keysize = 256;

    //public static string EncryptLinkUrlApproval(string Text)
    //{
    //    byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
    //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
    //    PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
    //    byte[] keyBytes = password.GetBytes(keysize / 8);
    //    RijndaelManaged symmetricKey = new RijndaelManaged();
    //    symmetricKey.Mode = CipherMode.CBC;
    //    ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
    //    MemoryStream memoryStream = new MemoryStream();
    //    CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
    //    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
    //    cryptoStream.FlushFinalBlock();
    //    byte[] Encrypted = memoryStream.ToArray();
    //    memoryStream.Close();
    //    cryptoStream.Close();
    //    return Convert.ToBase64String(Encrypted);
    //}

    //public static string DecryptLinkUrlApproval(string EncryptedText)
    //{
    //    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
    //    byte[] DeEncryptedText = Convert.FromBase64String(EncryptedText);
    //    PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
    //    byte[] keyBytes = password.GetBytes(keysize / 8);
    //    RijndaelManaged symmetricKey = new RijndaelManaged();
    //    symmetricKey.Mode = CipherMode.CBC;
    //    ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
    //    MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
    //    CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
    //    byte[] plainTextBytes = new byte[DeEncryptedText.Length];
    //    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
    //    memoryStream.Close();
    //    cryptoStream.Close();
    //    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    //}

    public static string newhaspassword(string value)
    {
        string mergekeyvalue = value + "TAKE";
        MD5 algorithm = MD5.Create();
        byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(mergekeyvalue));
        string sh1 = "";
        for (int i = 0; i < data.Length; i++)
        {
            sh1 += data[i].ToString("x2").ToUpperInvariant();
        }
        return sh1;
    }

    private static byte[] StringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }

    //public static string EncryptLinkUrlApproval(string Text)
    //{
    //    MD5 algorithm = MD5.Create();
    //    byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(Key));
    //    string sh1 = "";
    //    for (int i = 0; i < data.Length; i++)
    //    {
    //        sh1 += data[i].ToString("x2").ToUpperInvariant();
    //    }
    //    return sh1;
    //}

    //public static string DecryptLinkUrlApproval(string EncryptedText)
    //{
    //    MD5 algorithm = MD5.Create();
    //    byte[] data = StringToByteArray(EncryptedText);
    //    algorithm.
    //    return BitConverter.ToString(data);
    //}

    public static string EncryptLinkUrlApproval(string Text)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
        PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] Encrypted = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        string sh1 = "";
        for (int i = 0; i < Encrypted.Length; i++)
        {
            sh1 += Encrypted[i].ToString("x2").ToUpperInvariant();
        }
        return sh1;        
    }

    public static string DecryptLinkUrlApproval(string EncryptedText)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        //byte[] DeEncryptedText = Convert.FromBase64String(EncryptedText);
        byte[] DeEncryptedText = StringToByteArray(EncryptedText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[DeEncryptedText.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
}