using System.Text;
using System.Security.Cryptography;

public static class ConversionManager 
{

    //�C�ӂ̔��p�p��16����
    private static string aesIv = "1234567890123456";
    private static string aesKey = "1234567890123456";


    /// <summary>
    /// �Í�������json�֕�����
    /// </summary>
    /// <param name="bytes">�Í���</param>
    /// <returns></returns>
    public static string ConvertIntoJson(byte[] bytes)
    {
        // ������
        byte[] arrDecrypt = AesDecrypt(bytes);

        // byte�z��𕶎���ɕϊ�
        string json = Encoding.UTF8.GetString(arrDecrypt);

        return json;
    }

    /// <summary>
    /// json����Í����ֈÍ���
    /// </summary>
    /// <param name="json">json</param>
    /// <returns></returns>
    public static byte[] ConvertIntoCipher(string json)
    {
        // �������byte�z��ɕϊ�
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        // AES�Í���
        byte[] arrEncrypted = AesEncrypt(bytes);

        return arrEncrypted;
    }







    /// AesManaged�}�l�[�W���[���擾

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

    /// AES�Í���
    private static byte[] AesEncrypt(byte[] byteText)
    {
        // AES�}�l�[�W���[�̎擾
        AesManaged aes = GetAesManager();
        // �Í���
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// AES������
    private static byte[] AesDecrypt(byte[] byteText)
    {
        // AES�}�l�[�W���[�擾
        var aes = GetAesManager();
        // ������
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);//byteText.Length);

        return decryptText;
    }
}
