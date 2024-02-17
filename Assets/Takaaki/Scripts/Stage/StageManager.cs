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
        SelectCard,    //�J�[�h�I�����
        Battle,//�o�g����
        Menu,//���j���[
        Result,//���U���g
        Effect,//���o
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
        //������
        waveNum = 0;
        wave = stageEntity.waveEntities[waveNum];

        willGetCardNum = new List<int>();

        //����
        Preparation();
        resultManager.CreateResultData(Application.dataPath + "/" + SavePathName.StageFile(stageEntity.name));//"/" + SavePathName.ResultDataPath + "/" + stageEntity.name + "_Data.json");//���U���g����
    }

    private void Update()
    {
        //�c�莞�Ԃ�\��
        timeText.DisplayTime(stageEntity.resultEntity.rimitTime - clearTime);

        //���Ԃ𑪂�
        if (state == State.Battle)
        {
            //�^�C���I�[�o�[�Ȃ�Q�[���I�[�o�[�V�[����
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

    //����
    private void Preparation()
    {
        //�^�C���̕\��
        //SetTiles();
        stageGenerator.GenerateStage(stageEntity.waveEntities);
        stageGenerator.SetTile(waveNum);

        //�N���X�̔z�z�H
        SetUP();

        //�����ʒu��
        playerMover.MovePlayer(stageEntity.waveEntities[0].playerX, stageEntity.waveEntities[0].playerY, 0f);

        //�J�n�̉��o �J�[�h�I����ʂ̑J�ڂ�����
        stageEffecter.StageIn();

        //���U���g�̏�����
        resultManager = new ResultManager();
    }

    public void SetBattle()
    {
        //�o�g���J�n  
        //�^�C������
        stageGenerator.SetTile(waveNum);
        //�J�[�h�I���őI���������@���Z�b�g
        magicManager.SetEntities();
        tileCheck.SetMagic();
        //�J�[�h�I����ʂ��\��
        sceneLoader.UnLoadCardSelect();
        //���o
        stageEffecter.BattleStart();
    }

    private void SetResult()
    {
        //Result�̍쐬
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

        Debug.Log("old" + result.oldGotCard);
        Array.Sort(gameData.cardLists);
        dataManger.SaveGameData(gameData);
        resultManager.SaveResultData(result, Application.dataPath + "/" + SavePathName.CurrentStageFile);
        if (result.star >= oldResult.star)
        {
            resultManager.SaveResultData(result, Application.dataPath + "/" + SavePathName.StageFile(stageEntity.name));//"/ResultData/" + stageEntity.name + "_Data.json");
            //�ߋ��̃f�[�^�ɍ��̃f�[�^���㏑��
        }

        //Result�̕\��
        stageEffecter.OpenResult();
    }


    public void AllEnemyKill()//���ׂĂ̓G���|���ꂽ�Ƃ� 
    {
        waveNum++;

        if (waveNum < stageEntity.waveEntities.Length)
        {
            //���̃E�F�[�u�ւ̉��o
            stageEffecter.ChangeWave();
        }
        else
        {
            //�Q�[���N���A
            SetResult();
        }
    }

    public void ChangeWave()�@//�E�F�[�u�̕ύX 
    {
        //SetTiles();
        stageGenerator.SetTile(waveNum);

        wave = stageEntity.waveEntities[waveNum];
        tileCheck.SetMagic();

        SetUP();
        playerMover.SuspendMove();
        playerMover.MovePlayer(stageEntity.waveEntities[waveNum].playerX, stageEntity.waveEntities[waveNum].playerY, 0f); //�����ʒu��
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
        //�^�C���̔�����s��
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
