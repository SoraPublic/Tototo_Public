using System.Text;
using System.Security.Cryptography;

public static class ConversionManager 
{

    //任意の半角英数16文字
    private static string aesIv = "1234567890123456";
    private static string aesKey = "1234567890123456";


    /// <summary>
    /// 暗号文からjsonへ複合化
    /// </summary>
    /// <param name="bytes">暗号文</param>
    /// <returns></returns>
    public static string ConvertIntoJson(byte[] bytes)
    {
        // 復号化
        byte[] arrDecrypt = AesDecrypt(bytes);

        // byte配列を文字列に変換
        string json = Encoding.UTF8.GetString(arrDecrypt);

        return json;
    }

    /// <summary>
    /// jsonから暗号文へ暗号化
    /// </summary>
    /// <param name="json">json</param>
    /// <returns></returns>
    public static byte[] ConvertIntoCipher(string json)
    {
        // 文字列をbyte配列に変換
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        // AES暗号化
        byte[] arrEncrypted = AesEncrypt(bytes);

        return arrEncrypted;
    }







    /// AesManagedマネージャーを取得

    private static AesManaged GetAesManager()
    {


        AesManaged aes = new AesManaged();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.IV = Encoding.UTF8.GetBytes(aesIv);
        aes.Key = Encoding.UTF8.GetBytes(aesKey);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }

    /// AES暗号化
    private static byte[] AesEncrypt(byte[] byteText)
    {
        // AESマネージャーの取得
        AesManaged aes = GetAesManager();
        // 暗号化
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// AES復号化
    private static byte[] AesDecrypt(byte[] byteText)
    {
        // AESマネージャー取得
        var aes = GetAesManager();
        // 復号化
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);//byteText.Length);

        return decryptText;
    }
}
