using System.IO;
using UnityEngine;

public class DataManger 
{
    public void SaveGameData(GameData gameData)
    {
        string filePath = Application.dataPath + "/" + SavePathName.GameDataFile;

        // セーブデータをJSON形式の文字列に変換
        string json = JsonUtility.ToJson(gameData);

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
        //Debug.Log("セーブ完了");
    }

    /// <summary>
    /// GameDataのLoad　ゲーム開始時にのみ読み出し
    /// </summary>
    public GameData LoadGameData()
    {
        string filePath = Application.dataPath +"/" + SavePathName.GameDataFile;
        GameData gameData = new GameData();

        //既にデータが存在する場合
        if (File.Exists(filePath))
        {
            //ファイルモードをオープンにする
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // ファイル読み込み
            byte[] arrRead = File.ReadAllBytes(filePath);

            //複合化などなど
            string decryptStr = ConversionManager.ConvertIntoJson(arrRead);

            gameData = JsonUtility.FromJson<GameData>(decryptStr);

            // ファイルを閉じる
            if (file != null)
            {
                file.Close();
            }

            //Debug.Log("Load : 既存データあり");
        }
        //データがない。初めからの場合
        else
        {
            //GameDataに初期値を入力しているので何もしなくても大丈夫(たぶん)

            //Debug.Log("Load : 既存データなし");

            SaveGameData(gameData);
        }
        
        return gameData;
    }
}
