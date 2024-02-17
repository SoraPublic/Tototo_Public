using Enemy;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExcelFileConverter : AssetPostprocessor
{
    //ファイルパスの定義
    const string EExcelFilePath = "Assets/Ten/ScriptableObject/Enemy/EnemyData.xlsx";
    const string ESkillDataDirPath = "Assets/Ten/ScriptableObject/Enemy/SkillData/";
    const string EAttackRuleDirPath = "Assets/Ten/ScriptableObject/Enemy/AttackRule/";
    const string EInfoDirPath = "Assets/Ten/ScriptableObject/Enemy/Info/";
    const string ESkillDataSN = "ESkillData";
    const string EAttackRuleSN = "EAttackRule";
    const string EInfoSN = "EInfo";
    const string ObjectPathSN = "ObjectPath";

    //Excelファイルのヘッダ行
    private const int Row_Header_Num = 1;

    // Excelファイルのカラム定義.
    private enum SkillDataColumn
    {
        Label,
        Direction,
        AimKind,
        AimCount,
        Coordinate,
        IsDelay,
        DelayTime,
        IsReverse,
        IsHeal,
        HealPoint,
    }

    private enum DataPathColumn
    {
        Label,
        DataPath,
    }

    private enum AttackRuleColumn
    {
        Label,
        SkillDataName,
        Rate,
        CoolTime
    }

    private enum InfoColumn
    {
        Label,
        Name,
        HP,
        Rule,
        Condition,
        Model
    }

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str == EExcelFilePath)
            {
                Debug.Log("Reimported Asset: " + str);
                using (FileStream readFileStream = new FileStream(str, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XSSFWorkbook book = new XSSFWorkbook(readFileStream);
                    if (book == null)
                    {
                        Debug.LogAssertion("No Exist EnemyExcelFile");
                        continue;
                    }

                    ISheet dataSheet = book.GetSheet(ESkillDataSN);
                    if (dataSheet == null)
                    {
                        Debug.LogAssertion("No Exist EnenmySkillDataSheet");
                        continue;
                    }
                    CreateESkillData(dataSheet);

                    ISheet ruleSheet = book.GetSheet(EAttackRuleSN);
                    if(ruleSheet == null)
                    {
                        Debug.LogAssertion("No Exist EnenmyAttackRuleSheet");
                        continue;
                    }
                    CreateEAttackRule(ruleSheet);

                    ISheet infoSheet = book.GetSheet(EInfoSN);
                    if(infoSheet == null)
                    {
                        Debug.LogAssertion("No Exist EnenmyInfoSheet");
                        continue;
                    }
                    CreateEnemyInfo(infoSheet);
                }
            }
        }
    }

    private static void CreateESkillData(ISheet sheet)
    {
        int rowFirst = sheet.FirstRowNum + Row_Header_Num;
        int rowNum = sheet.LastRowNum - rowFirst + 1;

        EnemySkillData targetData = AssetDatabase.LoadAssetAtPath<EnemySkillData>("Assets/Ten/ScriptableObject/Enemy/Test/TestSkillData.asset");
        int arrayCount = 0;

        for (int rowCnt = 0; rowCnt < rowNum; rowCnt++)
        {
            IRow row = sheet.GetRow(rowFirst + rowCnt);
            if (row == null)
            {
                continue;
            }

            for (int columnCnt = row.FirstCellNum; columnCnt <= row.LastCellNum; columnCnt++)
            {
                ICell cell = row.GetCell(columnCnt);
                if (cell == null)
                {
                    continue;
                }

                string cellValue = GetCellValue(cell);
                if(cellValue == string.Empty)
                {
                    continue;
                }

                switch ((SkillDataColumn)columnCnt)
                {
                    case SkillDataColumn.Label:
                        if (cellValue != "add")
                        {
                            string assetfilePath = ESkillDataDirPath + cellValue + ".asset";
                            targetData = AssetDatabase.LoadAssetAtPath<EnemySkillData>(assetfilePath);

                            if (targetData == null)
                            {
                                targetData = ScriptableObject.CreateInstance("EnemySkillData") as EnemySkillData;
                                AssetDatabase.CreateAsset(targetData, assetfilePath);
                            }

                            arrayCount = 0;
                        }
                        else
                        {
                            arrayCount++;
                        }

                        while (targetData.SkillFactor.Count < arrayCount + 1)
                        {
                            targetData.SkillFactor.Add(new EnemySkillFactor());
                        }

                        while (targetData.SkillFactor.Count > arrayCount + 1)
                        {
                            targetData.SkillFactor.RemoveAt(targetData.SkillFactor.Count - 1);
                        }
                        break;

                    case SkillDataColumn.Direction:
                        string objPath = SearchObjectPathFromName(cellValue);
                        if (objPath == string.Empty)
                        {
                            continue;
                        }

                        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(objPath);
                        if (obj == null)
                        {
                            Debug.LogAssertion("No Exist DirectionObject");
                            continue;
                        }

                        targetData.SkillFactor[arrayCount].Direction = obj;
                        break;

                    case SkillDataColumn.AimKind:
                        AimKindEnemyAttack aimKind;
                        if (Enum.TryParse(cellValue, out aimKind) && Enum.IsDefined(typeof(AimKindEnemyAttack), aimKind))
                        {
                            targetData.SkillFactor[arrayCount].AimKind = aimKind;
                        }
                        break;

                    case SkillDataColumn.AimCount:
                        targetData.SkillFactor[arrayCount].AimCount = Convert.ToInt32(cellValue);
                        break;

                    case SkillDataColumn.Coordinate:
                        List<string> value = new List<string>();
                        List<int> returnList = new List<int>();
                        value.AddRange(cellValue.Split(new string[] { ",", " ", "　" }, StringSplitOptions.RemoveEmptyEntries));

                        foreach (string v in value)
                        {
                            returnList.Add(Convert.ToInt32(v));
                        }

                        targetData.SkillFactor[arrayCount].SetCoordinate = returnList;
                        break;

                    case SkillDataColumn.IsDelay:
                        targetData.SkillFactor[arrayCount].IsDelay = StringBoolConvert(cellValue);
                        break;

                    case SkillDataColumn.DelayTime:
                        targetData.SkillFactor[arrayCount].DelayTime = Convert.ToSingle(cellValue);
                        break;

                    case SkillDataColumn.IsReverse:
                        ReverseMode reverseMode;
                        Debug.Log(cellValue);
                        if (Enum.TryParse(cellValue, out reverseMode) && Enum.IsDefined(typeof(ReverseMode), reverseMode))
                        {
                            targetData.SkillFactor[arrayCount].ReverseMode = reverseMode;
                        }
                        break;

                    case SkillDataColumn.IsHeal:
                        targetData.SkillFactor[arrayCount].IsHeal = StringBoolConvert(cellValue);
                        break;

                    case SkillDataColumn.HealPoint:
                        targetData.SkillFactor[arrayCount].HealPoint = Convert.ToInt32(cellValue);
                        break;

                    default:
                        break;
                }
            }

            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
        }

        Debug.Log("Create : EnemySkillData");
    }

    private static void CreateEAttackRule(ISheet sheet)
    {
        int rowFirst = sheet.FirstRowNum + Row_Header_Num;
        int rowNum = sheet.LastRowNum - rowFirst + 1;

        EnemyAttackRule attackRule = AssetDatabase.LoadAssetAtPath<EnemyAttackRule>("Assets/Ten/ScriptableObject/Enemy/Test/TeskAttackRule.asset");
        int arrayCount = 0;

        for (int rowCnt = 0; rowCnt < rowNum; rowCnt++)
        {
            IRow row = sheet.GetRow(rowFirst + rowCnt);
            if (row == null)
            {
                continue;
            }

            for (int columnCnt = row.FirstCellNum; columnCnt <= row.LastCellNum; columnCnt++)
            {
                ICell cell = row.GetCell(columnCnt);
                if (cell == null)
                {
                    continue;
                }

                string cellValue = GetCellValue(cell);
                if (cellValue == string.Empty)
                {
                    continue;
                }

                switch ((AttackRuleColumn)columnCnt)
                {
                    case AttackRuleColumn.Label:
                        if (cellValue != "add")
                        {
                            string assetfilePath = EAttackRuleDirPath + cellValue + ".asset";
                            attackRule = AssetDatabase.LoadAssetAtPath<EnemyAttackRule>(assetfilePath);

                            if (attackRule == null)
                            {
                                attackRule = ScriptableObject.CreateInstance("EnemyAttackRule") as EnemyAttackRule;
                                AssetDatabase.CreateAsset(attackRule, assetfilePath);
                            }

                            arrayCount = 0;
                        }
                        else
                        {
                            arrayCount++;
                        }

                        while (attackRule.SkillDataSets.Count < arrayCount + 1)
                        {
                            attackRule.SkillDataSets.Add(new SkillDataSet());
                        }

                        while (attackRule.SkillDataSets.Count > arrayCount + 1)
                        {
                            attackRule.SkillDataSets.RemoveAt(attackRule.SkillDataSets.Count - 1);
                        }
                        break;

                    case AttackRuleColumn.SkillDataName:
                        string objPath = ESkillDataDirPath + cellValue + ".asset";
                        EnemySkillData newSkill = AssetDatabase.LoadAssetAtPath<EnemySkillData>(objPath);
                        if (newSkill == null)
                        {
                            Debug.LogWarning("No Exist ESkillData");
                            continue;
                        }
                        attackRule.SkillDataSets[arrayCount].Data = newSkill;
                        break;

                    case AttackRuleColumn.Rate:
                        attackRule.SkillDataSets[arrayCount].Rate = Convert.ToInt32(cellValue);
                        break;

                    case AttackRuleColumn.CoolTime:
                        attackRule.CoolTime = Convert.ToSingle(cellValue);
                        break;

                    default:
                        break;
                }
            }

            EditorUtility.SetDirty(attackRule);
            AssetDatabase.SaveAssets();
        }

        Debug.Log("Create : EnemyAttackRule");
    }

    private static void CreateEnemyInfo(ISheet sheet)
    {
        int rowFirst = sheet.FirstRowNum + Row_Header_Num;
        int rowNum = sheet.LastRowNum - rowFirst + 1;

        Info_Enemy enemyInfo = AssetDatabase.LoadAssetAtPath<Info_Enemy>("Assets/Ten/ScriptableObject/Enemy/Test/TestInfo.asset");
        int arrayCount = 0;

        for (int rowCnt = 0; rowCnt < rowNum; rowCnt++)
        {
            IRow row = sheet.GetRow(rowFirst + rowCnt);
            if (row == null)
            {
                continue;
            }

            for (int columnCnt = row.FirstCellNum; columnCnt <= row.LastCellNum; columnCnt++)
            {
                ICell cell = row.GetCell(columnCnt);
                if (cell == null)
                {
                    continue;
                }

                string cellValue = GetCellValue(cell);
                if (cellValue == string.Empty)
                {
                    continue;
                }

                switch ((InfoColumn)columnCnt)
                {
                    case InfoColumn.Label:
                        if (cellValue != "add")
                        {
                            string assetfilePath = EInfoDirPath + cellValue + ".asset";
                            enemyInfo = AssetDatabase.LoadAssetAtPath<Info_Enemy>(assetfilePath);

                            if (enemyInfo == null)
                            {
                                enemyInfo = ScriptableObject.CreateInstance("Info_Enemy") as Info_Enemy;
                                AssetDatabase.CreateAsset(enemyInfo, assetfilePath);
                            }

                            arrayCount = 0;
                        }
                        else
                        {
                            arrayCount++;
                        }

                        while (enemyInfo.PatternList.Count < arrayCount + 1)
                        {
                            enemyInfo.PatternList.Add(new EnemyPattern());
                        }

                        while (enemyInfo.PatternList.Count > arrayCount + 1)
                        {
                            enemyInfo.PatternList.RemoveAt(enemyInfo.PatternList.Count - 1);
                        }
                        break;

                    case InfoColumn.Name:
                        enemyInfo.SetEnemyName(cellValue);
                        break;

                    case InfoColumn.HP:
                        enemyInfo.SetEnemyHP(Convert.ToInt32(cellValue));
                        break;

                    case InfoColumn.Rule:
                        string rulePath = EAttackRuleDirPath + cellValue + ".asset";
                        EnemyAttackRule newRule = AssetDatabase.LoadAssetAtPath<EnemyAttackRule>(rulePath);
                        if (newRule == null)
                        {
                            Debug.LogWarning("No Exist ESkillData");
                            continue;
                        }
                        enemyInfo.PatternList[arrayCount].Rule = newRule;
                        break;

                    case InfoColumn.Condition:
                        enemyInfo.PatternList[arrayCount].Condition = Convert.ToSingle(cellValue);
                        break;

                    case InfoColumn.Model:
                        string objPath = SearchObjectPathFromName(cellValue);
                        if(objPath == string.Empty)
                        {
                            continue;
                        }

                        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(objPath);
                        if (obj == null)
                        {
                            Debug.LogAssertion("No Exist DirectionObject");
                            continue;
                        }

                        enemyInfo.SetEnemyModel(obj);
                        break;

                    default:
                        break;
                }
            }

            EditorUtility.SetDirty(enemyInfo);
            AssetDatabase.SaveAssets();
        }

        Debug.Log("Create : EnemyInfo");
    }

    private static string SearchObjectPathFromName(string name)
    {
        string returnPath = string.Empty;
        using (FileStream readFileStream = new FileStream(EExcelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            XSSFWorkbook book = new XSSFWorkbook(readFileStream);
            if (book == null)
            {
                Debug.LogAssertion("No Exist EnemyExcelFile");
            }

            ISheet sheet = book.GetSheet(ObjectPathSN);
            if (sheet == null)
            {
                Debug.LogAssertion("No Exist EnenmySkillDataSheet");
            }

            int rowFirst = sheet.FirstRowNum + Row_Header_Num;
            int rowNum = sheet.LastRowNum - rowFirst + 1;

            for (int rowCnt = 0; rowCnt < rowNum; rowCnt++)
            {
                IRow row = sheet.GetRow(rowFirst + rowCnt);
                if (row == null)
                {
                    continue;
                }
                ICell cell = row.GetCell(row.FirstCellNum);
                if (cell == null)
                {
                    continue;
                }

                if (name == cell.StringCellValue)
                {
                    ICell pathCell = row.GetCell(row.FirstCellNum + 1);
                    if (pathCell == null)
                    {
                        continue;
                    }

                    returnPath = pathCell.StringCellValue;
                }
            }
        }

        return returnPath;
    }

    private static bool StringBoolConvert(string str)
    {
        switch (str)
        {
            case "TRUE":
            case "True":
            case "true":
                return true;

            case "FALSE":
            case "False":
            case "false":
                return false;

            default:
                Debug.LogAssertion("This is not boolean");
                return false;
        }
    }

    public static string GetCellValue(ICell cell)
    {
        string strval = null;

        switch (cell.CellType)
        {
            case CellType.String:
                //文字型
                strval = cell.StringCellValue;
                break;
            case CellType.Numeric:
                if (DateUtil.IsCellDateFormatted(cell))
                {
                    //日付型
                    strval = cell.DateCellValue.ToString("yyyy/MM/dd");
                }
                else
                {
                    //数値型
                    strval = cell.NumericCellValue.ToString();
                }
                break;
            case CellType.Boolean:
                //真偽型
                strval = cell.BooleanCellValue.ToString();
                break;
            case CellType.Formula:
                //関数
                strval = cell.CellFormula.ToString();
                break;
            case CellType.Blank:
                //ブランク
                strval = "";
                break;
            case CellType.Error:
                //エラー
                strval = cell.ErrorCellValue.ToString();
                break;
            default:
                break;
        }

        return strval;
    }
}
