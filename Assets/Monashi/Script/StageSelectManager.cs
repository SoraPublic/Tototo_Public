using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using naichilab;

public class StageSelectManager : MonoBehaviour
{
    private Animator anim;
    private const string isWalk = "isWalk";

    [SerializeField]
    private GameObject Player,Cat;

    [SerializeField]
    private GameObject Wal1,Wal2,Wal3;
    private Vector3 target; //進行方向のタイルのPosition

    [SerializeField]
    private GameObject Stage;
    private GameObject[] child;
  
    private int Count = 0;
    public int ClearCount;

    [SerializeField]
    private SpriteRenderer[] Sprite;
    private float alpha_out,alpha_in;

    [SerializeField]
    private Material[] mat;

    [SerializeField] private StageEntity[] stageEntities;

    [SerializeField] private Transform unMask;

    [SerializeField] private InformationText informationText;
    [SerializeField] private RankingViewer rankingViewer;
    [SerializeField] private RankingInfo[] boards;

    [SerializeField] private Image Effect;

    [SerializeField] private AudioSource moveSound;
    [SerializeField] private AudioSource sceenSound;

    [SerializeField] private MapColorManager mapColorManager;
    
    private bool FirstCheck = true;
    private bool flag1 = true;
    private bool flag2 = false;

    private const int stageNum = castleStageNum;
    private const int forestStageNum = 2;
    private const int townStageNum = forestStageNum + 2;
    private const int castleStageNum = townStageNum + 1;

    private const float moveSpeed = 1f;

    private enum playerState
    {
        WAIT,
        MOVE,
    }

    private playerState state;
    // Start is called before the first frame update
    void Start()
    {
        Effect.gameObject.SetActive(true);
        Effect.DOFade(0, 1).OnComplete(() =>
        {
            Effect.gameObject.SetActive(false);
        });

        anim = Cat.GetComponent<Animator>();

        child = new GameObject[Stage.transform.childCount];
 
        for (int i = 0; i < Stage.transform.childCount; i++)
        {
            child[i] = Stage.transform.GetChild(i).gameObject;          
        }

        target = child[0].transform.position;

        SetClearCount();

        state = playerState.WAIT;

        ResultManager resultManager = new ResultManager();
        informationText.ChangeText(stageEntities[0].resultEntity, resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.StageFile(stageEntities[Count].name)), stageEntities[Count].name);
        rankingViewer.ChangeRanking(boards[0]);

    }

    // Update is called once per frame
    void Update()
    {
        if(state == playerState.WAIT)
        {
            InputManager();
        }
        
        WallPaperChange(Count);
    }

    private void MoveLeft()
    {
        Count--;
        moveSound.Play();
        anim.SetBool(isWalk, true);
        state = playerState.MOVE;
        Player.transform.DOMoveX(child[Count].transform.position.x,moveSpeed).OnComplete(()=>
        {
            anim.SetBool(isWalk,false);
            state = playerState.WAIT;
        });
        Player.transform.rotation = Quaternion.Euler(0f,270f,0f);

        ResultManager resultManager = new ResultManager();
        informationText.ChangeText(stageEntities[Count].resultEntity, resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.StageFile(stageEntities[Count].name)), stageEntities[Count].name);
        rankingViewer.ChangeRanking(boards[Count]);
    }

    private void MoveRight()
    {
        Count++;
        moveSound.Play();
        anim.SetBool(isWalk, true);
        state = playerState.MOVE;
        Player.transform.DOMoveX(child[Count].transform.position.x, moveSpeed).OnComplete(() =>
        {
            anim.SetBool(isWalk, false);
            state = playerState.WAIT;
        });
        Player.transform.rotation = Quaternion.Euler(0f, -270f, 0f);

        ResultManager resultManager = new ResultManager();
        informationText.ChangeText(stageEntities[Count].resultEntity, resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.StageFile(stageEntities[Count].name)), stageEntities[Count].name);
        rankingViewer.ChangeRanking(boards[Count]);

    }

    void InputManager() //進みたいタイルの座標を更新する関数
    {        
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) &&(Count!=0))
        {
            MoveLeft();
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) &&(Count!=stageNum-1)&&(ClearCount > Count))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StageIn(Count);
        }
    }

    void StageIn(int No) //ステージを呼び出す関数(各caseにシーン呼び出しを入れてください)
    {
        sceenSound.Play();
        state = playerState.MOVE;
        SceneLoader sceneLoader = new SceneLoader();
        anim.SetTrigger("AttackTrigger"); 
        unMask.DOScale(Vector3.zero, 1.5f).OnComplete(() =>
        {
            sceneLoader.LoadBattleScene(stageEntities[No]);
        });
    }

    void WallPaperChange(int BG) //壁紙チェンジ 地獄のコードになってしまいました。。。。。。。
    {
        if ((BG >= 0) && (BG <= forestStageNum-1)) //一番手前 ステージ数が変化した場合、BGの値を変更してください
        {
            if (FirstCheck == false)
            {
                if (BG == forestStageNum-1 && flag1 == false)
                {
                    alpha_in = 0;
                    alpha_out = 1;
                    flag1 = true;
                    flag2 = false;
                }

                Fadein(0);
            }
        }

        if ((BG >= forestStageNum) && (BG <= townStageNum-1))
        {
           if((BG == forestStageNum || BG == townStageNum-1) && (flag1 == true))
           {
                alpha_in = 0;
                alpha_out = 1;
                FirstCheck = false;
                flag1 = false;
           }

           if(flag1 == false && flag2 == false)
           {
                Fadeout(0);
           }

           if(flag2 == true)
           {
                Fadein(1);
           }
        }

        if ((BG >= townStageNum) && (BG <= castleStageNum-1)) //一番後ろ
        {
            if(BG == townStageNum && flag1 == false)
            {
                alpha_in = 0;
                alpha_out = 1;
             
                flag1 = true;
                flag2 = true;
            }

            Fadeout(1);
        }
    }

    void TileColor(int count)
    {
        for(int i=0; i < count; i++)
        {
            Renderer renderer = child[i].GetComponent<Renderer>();
            renderer.material = mat[2];//クリア済みに
            mapColorManager.ChangeColor(i,MapColorManager.STATE.CLEAR);
        }
        if (count != stageNum)
        {
            child[count].GetComponent<Renderer>().material = mat[0];//オープン状態に
            mapColorManager.ChangeColor(count, MapColorManager.STATE.OPEN);

        }
    }

    /// <summary>
    /// セーブデータを見て、クリア状況を把握
    /// </summary>
    public void SetClearCount()
    {
        string stageName = "Stage";
        ResultManager resultManager = new ResultManager();
        for(int i =1; i <= stageNum; i++)
        {

            string str = stageName + i.ToString("00");
            ResultData result = resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.StageFile(str));

            if(result.clear == 0) //クリアしていない
            {
                ClearCount = i-1;
                TileColor(ClearCount);
                return;
            }
        }

        ClearCount = stageNum;
        TileColor(ClearCount);
    }

    void Fadeout(int Number_out)
    {
        float sin = Mathf.Sin(Time.deltaTime);

        alpha_out -= sin;

        var color = Sprite[Number_out].color;
        color.a = alpha_out;
        Sprite[Number_out].color = color;
    }

    void Fadein(int Number_in)
    {
        float sin = Mathf.Sin(Time.deltaTime);

        alpha_in += sin;

        var color = Sprite[Number_in].color;
        color.a = alpha_in;
        Sprite[Number_in].color = color;
    }
}
