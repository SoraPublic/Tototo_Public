
public static class SavePathName 
{
    public const string GameDataFolder = "GameData";

    public const string GameDataFile = GameDataFolder + "/GameData.bytes";

    public const string ResultDataPath = GameDataFolder + "/ResultData";

    public const string CurrentStageFile = ResultDataPath + "/Current_Data.bytes";

    public static string StageFile(string name)
    {
        return ResultDataPath +"/"+ name + "_Data.bytes";
    }

    //folder
}
