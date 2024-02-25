using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private string StageSelect = "StageSelect_Takaaki";
    private string battle = "BattleTest_Takaaki";
    private string cardSelect = "CardSelect_Takaaki";
    private string menu = "Menu_Takaaki";
    private string result = "ResultTest_Ten";
    private string gameover = "GameOver_Ten";

    public void LoadStageSelect()
    {
        SceneManager.LoadScene(StageSelect);
    }

    public void LoadBattleScene(StageEntity stageEntity)
    {
        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(battle);

        StageManager.stageEntity = stageEntity;
    }

    public void LoadCardSelect() 
    {
        SceneManager.LoadScene(cardSelect, LoadSceneMode.Additive);
    }

    public void UnLoadCardSelect() 
    {
        SceneManager.UnloadSceneAsync(cardSelect);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menu, LoadSceneMode.Additive);
    }

    public void UnLoadMenu() 
    {
        SceneManager.UnloadSceneAsync(menu);
    }

    public void LoadResult()
    {
        SceneManager.LoadScene(result, LoadSceneMode.Additive);
    }

    public void UnLoadResult() 
    {
        SceneManager.UnloadSceneAsync(result);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(gameover, LoadSceneMode.Additive);
    }

    public void UnLoadGameOver()
    {
        SceneManager.UnloadSceneAsync(gameover);
    }
}
