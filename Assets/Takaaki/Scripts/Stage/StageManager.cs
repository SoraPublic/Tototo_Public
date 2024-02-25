using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public static StageEntity stageEntity;

    public StageGenerator stageGenerator;
    public StageInputManager stageInputManager;

    public TileManager tileManager;
    public TileCheck tileCheck;

    public PlayerManager playerManager;
    public PlayerMover playerMover;
    public PlayerInput playerInput;

    [SerializeField] private PlayerAnimator playerAnimator;

    public PlayerState playerState;

    public MagicManager magicManager;

    private SceneLoader sceneLoader;
    public StageEffecter stageEffecter;
    private ResultManager resultManager;
    private DataManger dataManger;

    [SerializeField] public EnemyController enemy;

    [SerializeField] private TimeText timeText;


    [System.NonSerialized]
    public int row;
    [System.NonSerialized]
    public int column;

    [System.NonSerialized]
    public int waveNum;

    //[System.NonSerialized]
    public WaveEntity wave;

    private GameObject tiles;

    public float clearTime = 0;

    public GameObject chest;

    public List<int> willGetCardNum;

    public enum State
    {
        SelectCard,    //カード選択画面
        Battle,//バトル中
        Menu,//メニュー
        Result,//リザルト
        Effect,//演出
    }

    public State state;

    private void Awake()
    {
        StageManager.instance = this;
    }

    private void Start()
    {
        sceneLoader = new SceneLoader();
        dataManger = new DataManger();
        //初期化
        waveNum = 0;
        wave = stageEntity.waveEntities[waveNum];

        willGetCardNum = new List<int>();

        //準備
        Preparation();
        resultManager.CreateResultData(Application.dataPath + "/" + SavePathName.StageFile(stageEntity.name));//"/" + SavePathName.ResultDataPath + "/" + stageEntity.name + "_Data.json");//リザルト生成
    }

    private void Update()
    {
        //残り時間を表示
        timeText.DisplayTime(stageEntity.resultEntity.rimitTime - clearTime);

        //時間を測る
        if (state == State.Battle)
        {
            //タイムオーバーならゲームオーバーシーンへ
            if (clearTime >= stageEntity.resultEntity.rimitTime)
            {
                sceneLoader.LoadGameOver();
                state = State.Result;
            }
            clearTime += Time.deltaTime;
        }
    }

    private void SetUP()
    {
        playerState = PlayerState.Move;

        playerMover.SetUp();

        playerInput.SetUp();

        stageInputManager.SetUp();

        tileManager.SetUp();

        stageEffecter.SetUp();

        enemy.SetUp();
    }

    //準備
    private void Preparation()
    {
        //タイルの表示
        //SetTiles();
        stageGenerator.GenerateStage(stageEntity.waveEntities);
        stageGenerator.SetTile(waveNum);

        //クラスの配布？
        SetUP();

        //初期位置へ
        playerMover.MovePlayer(stageEntity.waveEntities[0].playerX, stageEntity.waveEntities[0].playerY, 0f);

        //開始の演出 カード選択画面の遷移もここ
        stageEffecter.StageIn();

        //リザルトの初期化
        resultManager = new ResultManager();
    }

    public void SetBattle()
    {
        //バトル開始  
        //タイル生成
        stageGenerator.SetTile(waveNum);
        //カード選択で選択した魔法をセット
        magicManager.SetEntities();
        tileCheck.SetMagic();
        //カード選択画面を非表示
        sceneLoader.UnLoadCardSelect();
        //演出
        stageEffecter.BattleStart();
    }

    private void SetResult()
    {
        //Resultの作成
        ResultData result = resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.CurrentStageFile); //"/ResultData/Current_Data.json");
        ResultData oldResult = new ResultData();
        if (File.Exists(Application.dataPath + "/" + SavePathName.StageFile(stageEntity.name)))
        {
            oldResult = resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.StageFile(stageEntity.name));
        }
        result.clear++;
        result.time = clearTime;
        resultManager.EvaluateResultData(result, stageEntity.resultEntity);
        GameData gameData = dataManger.LoadGameData();

        for (int i = result.nowGetCard; i > 0; i--)
        {
            if (result.oldGotCard >= 3)
            {
                break;
            }
            Array.Resize(ref gameData.cardLists, gameData.cardLists.Length + 1);
            gameData.cardLists[gameData.cardLists.Length - 1] = stageEntity.magics[result.oldGotCard].cardNum;
            willGetCardNum.Add(stageEntity.magics[result.oldGotCard].cardNum);
            result.oldGotCard++;
        }

        //Debug.Log("old" + result.oldGotCard);
        Array.Sort(gameData.cardLists);
        dataManger.SaveGameData(gameData);
        resultManager.SaveResultData(result, Application.dataPath + "/" + SavePathName.CurrentStageFile);
        if (result.star >= oldResult.star)
        {
            resultManager.SaveResultData(result, Application.dataPath + "/" + SavePathName.StageFile(stageEntity.name));//"/ResultData/" + stageEntity.name + "_Data.json");
            //過去のデータに今のデータを上書き
        }

        //Resultの表示
        stageEffecter.OpenResult();
    }


    public void AllEnemyKill()//すべての敵が倒されたとき 
    {
        waveNum++;

        if (waveNum < stageEntity.waveEntities.Length)
        {
            //次のウェーブへの演出
            stageEffecter.ChangeWave();
        }
        else
        {
            //ゲームクリア
            SetResult();
        }
    }

    public void ChangeWave()　//ウェーブの変更 
    {
        //SetTiles();
        stageGenerator.SetTile(waveNum);

        wave = stageEntity.waveEntities[waveNum];
        tileCheck.SetMagic();

        SetUP();
        playerMover.SuspendMove();
        playerMover.MovePlayer(stageEntity.waveEntities[waveNum].playerX, stageEntity.waveEntities[waveNum].playerY, 0f); //初期位置へ
        magicManager.SetEntities();

        PlayerStatus.instance.ResetBuff();

    }

    public void MenuControll()
    {
        if (state == State.Menu)
        {
            sceneLoader.UnLoadMenu();
            state = State.Battle;
        }
        else if (state == State.Battle)
        {
            sceneLoader.LoadMenu();
            state = State.Menu;
        }
    }

    public void Check()
    {
        //タイルの判定を行う
        tileManager.Check();
    }

    public IEnumerator StopPlayer()
    {
        playerState = PlayerState.Stop;
        playerAnimator.StopAnim();

        yield return new WaitForSeconds(PlayerStatus.stopTime);
        playerState = PlayerState.Move;
        yield break;
    }
}
