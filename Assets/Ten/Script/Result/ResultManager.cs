using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResultManager
{
    public void CreateResultData(string datapath)
    {
        ResultData data = new ResultData();//新しいリザルトデータを生成

        if (File.Exists(datapath))//過去にプレイしていたステージなら（リザルトデータがあるなら）
        {
            data = LoadResultData(datapath);//前回のデータを取得
            data.star = 0;//獲得していた星をリセット
            //data.oldGotCard += data.nowGetCard;
            data.nowGetCard = 0;//カード獲得枚数をリセット
        }
        else if (!Directory.Exists(Application.dataPath + "/" + SavePathName.ResultDataPath))  // "/ResultData"))//フォルダすら無かったら（ほぼ機能しないけどバグ対策）
        {
            Directory.CreateDirectory(Application.dataPath + "/" + SavePathName.ResultDataPath); //"/ResultData");//フォルダを生成
        }

        data.hit = 0;//被弾数をリセット
        data.time = 0;//討伐時間をリセット

        SaveResultData(data, Application.dataPath + "/" + SavePathName.CurrentStageFile);//"/ResultData/Current_Data.json");//過去のデータに上書きしない
    }

    public void SaveResultData(ResultData data, string filePath)
    {
        // セーブデータをJSON形式の文字列に変換
        string json = JsonUtility.ToJson(data);

        byte[] arrEncrypted = ConversionManager.ConvertIntoCipher(json);

        // 指定したパスにファイルを作成
        FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

        // ファイルに保存
        file.Write(arrEncrypted, 0, arrEncrypted.Length);

        // ファイルを閉じる
        if (file != null)
        {
            file.Close();
        }
        Debug.Log("セーブ完了");

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
        //既にデータが存在する場合
        if (File.Exists(filePath))
        {
            //ファイルモードをオープンにする
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // ファイル読み込み
            byte[] arrRead = File.ReadAllBytes(filePath);

            //複合化などなど
            string decryptStr = ConversionManager.ConvertIntoJson(arrRead);

            resultData = JsonUtility.FromJson<ResultData>(decryptStr);

            // ファイルを閉じる
            if (file != null)
            {
                file.Close();
            }

            Debug.Log("Load : 既存データあり");

        }
        else
        {
            resultData = new ResultData();
            Debug.Log("Load : 既存データなし");
        }

        return resultData;
        /*
        StreamReader reader = new StreamReader(datapath); //受け取ったパスのファイルを読み込む
        string datastr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
        reader.Close();


        return JsonUtility.FromJson<ResultData>(datastr);//読み込んだjsonファイルをResultData型に変換して返す
    */
    }

    public void EvaluateResultData(ResultData result, ResultEntity entity)
    {
        Debug.Log("星" + result.star);
        Debug.Log("now" + result.nowGetCard);
        Debug.Log("old" + result.oldGotCard);
        //基準を満たしていれば星獲得
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

        //獲得するカードの枚数を調査
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