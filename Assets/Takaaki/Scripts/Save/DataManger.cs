using System.IO;
using UnityEngine;

public class DataManger 
{
    public void SaveGameData(GameData gameData)
    {
        string filePath = Application.dataPath + "/" + SavePathName.GameDataFile;

        // �Z�[�u�f�[�^��JSON�`���̕�����ɕϊ�
        string json = JsonUtility.ToJson(gameData);

        byte[] arrEncrypted = ConversionManager.ConvertIntoCipher(json);

        // �w�肵���p�X�Ƀt�@�C�����쐬
        FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

        // �t�@�C���ɕۑ�
        file.Write(arrEncrypted, 0, arrEncrypted.Length);

        // �t�@�C�������
        if (file != null)
        {
            file.Close();
        }
        Debug.Log("�Z�[�u����");
    }

    /// <summary>
    /// GameData��Load�@�Q�[���J�n���ɂ̂ݓǂݏo��
    /// </summary>
    public GameData LoadGameData()
    {
        string filePath = Application.dataPath +"/" + SavePathName.GameDataFile;
        GameData gameData = new GameData();

        //���Ƀf�[�^�����݂���ꍇ
        if (File.Exists(filePath))
        {
            //�t�@�C�����[�h���I�[�v���ɂ���
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // �t�@�C���ǂݍ���
            byte[] arrRead = File.ReadAllBytes(filePath);

            //�������ȂǂȂ�
            string decryptStr = ConversionManager.ConvertIntoJson(arrRead);

            gameData = JsonUtility.FromJson<GameData>(decryptStr);

            // �t�@�C�������
            if (file != null)
            {
                file.Close();
            }

            Debug.Log("Load : �����f�[�^����");
        }
        //�f�[�^���Ȃ��B���߂���̏ꍇ
        else
        {
            //GameData�ɏ����l����͂��Ă���̂ŉ������Ȃ��Ă����v(���Ԃ�)

            Debug.Log("Load : �����f�[�^�Ȃ�");

            SaveGameData(gameData);
        }
        
        return gameData;
    }
}
