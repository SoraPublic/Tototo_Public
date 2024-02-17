using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResultManager
{
    public void CreateResultData(string datapath)
    {
        ResultData data = new ResultData();//�V�������U���g�f�[�^�𐶐�

        if (File.Exists(datapath))//�ߋ��Ƀv���C���Ă����X�e�[�W�Ȃ�i���U���g�f�[�^������Ȃ�j
        {
            data = LoadResultData(datapath);//�O��̃f�[�^���擾
            data.star = 0;//�l�����Ă����������Z�b�g
            //data.oldGotCard += data.nowGetCard;
            data.nowGetCard = 0;//�J�[�h�l�����������Z�b�g
        }
        else if (!Directory.Exists(Application.dataPath + "/" + SavePathName.ResultDataPath))  // "/ResultData"))//�t�H���_���疳��������i�قڋ@�\���Ȃ����ǃo�O�΍�j
        {
            Directory.CreateDirectory(Application.dataPath + "/" + SavePathName.ResultDataPath); //"/ResultData");//�t�H���_�𐶐�
        }

        data.hit = 0;//��e�������Z�b�g
        data.time = 0;//�������Ԃ����Z�b�g

        SaveResultData(data, Application.dataPath + "/" + SavePathName.CurrentStageFile);//"/ResultData/Current_Data.json");//�ߋ��̃f�[�^�ɏ㏑�����Ȃ�
    }

    public void SaveResultData(ResultData data, string filePath)
    {
        // �Z�[�u�f�[�^��JSON�`���̕�����ɕϊ�
        string json = JsonUtility.ToJson(data);

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

        /*
        string jsonstr = JsonUtility.ToJson(data);
        StreamWriter writer = new StreamWriter(datapath, false);
        writer.WriteLine(jsonstr);
        writer.Flush();
        writer.Close();
        */
    }

    public ResultData LoadResultData(string filePath)
    {
        ResultData resultData;
        //���Ƀf�[�^�����݂���ꍇ
        if (File.Exists(filePath))
        {
            //�t�@�C�����[�h���I�[�v���ɂ���
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // �t�@�C���ǂݍ���
            byte[] arrRead = File.ReadAllBytes(filePath);

            //�������ȂǂȂ�
            string decryptStr = ConversionManager.ConvertIntoJson(arrRead);

            resultData = JsonUtility.FromJson<ResultData>(decryptStr);

            // �t�@�C�������
            if (file != null)
            {
                file.Close();
            }

            Debug.Log("Load : �����f�[�^����");

        }
        else
        {
            resultData = new ResultData();
            Debug.Log("Load : �����f�[�^�Ȃ�");
        }

        return resultData;
        /*
        StreamReader reader = new StreamReader(datapath); //�󂯎�����p�X�̃t�@�C����ǂݍ���
        string datastr = reader.ReadToEnd();//�t�@�C���̒��g�����ׂēǂݍ���
        reader.Close();


        return JsonUtility.FromJson<ResultData>(datastr);//�ǂݍ���json�t�@�C����ResultData�^�ɕϊ����ĕԂ�
    */
    }

    public void EvaluateResultData(ResultData result, ResultEntity entity)
    {
        Debug.Log("��" + result.star);
        Debug.Log("now" + result.nowGetCard);
        Debug.Log("old" + result.oldGotCard);
        //��𖞂����Ă���ΐ��l��
        if (result.clear >= entity.clear)
        {
            result.star++;
        }
        if (result.hit <= entity.hit)
        {
            result.star++;
        }
        if ((int)result.time <= entity.clearTime)
        {
            result.star++;
        }

        //�l������J�[�h�̖����𒲍�
        int willGetCard = result.star - result.oldGotCard;

        if ((willGetCard <= 0) || (result.oldGotCard >= 3))
        {
            result.nowGetCard = 0;
        }
        else
        {
            result.nowGetCard = willGetCard;
        }

        /*if ((result.oldGotCard == 1) && (result.star >= 3))
        {
            result.nowGetCard++;
        }
        if (result.oldGotCard == 0)
        {
            if (result.star >= 1)
            {
                result.nowGetCard++;
            }
            if (result.star >= 3)
            {
                result.nowGetCard++;
            }
        }*/
    }
}