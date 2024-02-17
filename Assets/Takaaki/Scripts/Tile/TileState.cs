using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileState : MonoBehaviour
{
    public enum TileSituation //タイルの状況
    {
        Normal, //通常のタイルがある状態
        Block,　//障害物があり通れない状態
        Empty, //タイルがそもそも存在しない状態
    }

    //タイルの見た目の管理
    private TileView tileView;
    
    //濡れた乾いたの情報
    [SerializeField]
    private int state;

    //濡れた乾いたの中間値
    private float middle;

    //プレイヤーが乾かしているならtrue
    public bool isPressing;

    public TileSituation situation;　//タイルの状況

    public AudioSource tileSound;

    private void Awake()
    {
        tileView = this.GetComponent<TileView>();
    }

    private void Start()
    {
        //初期化
        //濡れた状態にする
        middle = 0f;
        SetState(0);

        tileView.SetFast();

        isPressing = false;
    }

    private void Update()
    {
        if (StageManager.instance.state == StageManager.State.Battle)
        {
            //プレイヤーが乾かしているとき
            if (isPressing)
            {
                //乾かす
                IncreaseBorder();
            }
            else
            {
                //濡れる
                DecreaseBorder();
            }
        }
    }

    public void SetUpTileView()
    {
        tileView.SetTargetImage(StageManager.stageEntity.TargetColor);
    }

    public void EnemyAttackING(float i)
    {
        tileView.SetTarget(i);
    }

    public IEnumerator DoEnemyAttack(GameObject direction)
    {
        tileView.SetDirection(direction);
        situation = TileSituation.Block;
        while (true)
        {
            tileView.SetTarget(1);
            SetState(0);
            middle = 0;
            yield return null;
        }
    }

    public void FinishEnemyAttack()
    {
        situation = TileSituation.Normal;
        tileView.SetTarget(0);
        tileView.DeleteDirection();
    }

    //乾かす
    private void IncreaseBorder()
    {
        middle += Time.deltaTime * PlayerStatus.instance.GetWriteSpeed();
        if (middle > 0.7f)
        {
            SetState(1);

            if (middle > 1f)
            {
                middle = 1f;
            }
        }

        tileView.SetMagicUp(middle);
    }

    //濡れた
    private void DecreaseBorder()
    {
        middle -= Time.deltaTime * PlayerStatus.instance.GetEraseSpeed();
        if (middle < 0.3f)
        {
            SetState(0);

            if (middle < 0f)
            {
                middle = 0f;
            }
        }

        tileView.SetMagicDown(middle);
    }

    //濡れた乾いた状態の変更のときに使う
    //見た目まで変えてくれるので絶対ここから変更
    private void SetState(int setValue)
    {
        if (state != setValue)
        {
            state = setValue;

            StageManager.instance.tileManager.Check();

            //魔法陣を描いたとき
            if(setValue == 1)
            {
                tileSound.Play();
                tileView.particle.SetActive(true);
            }

            if(setValue == 0)
            {
                tileView.particle.SetActive(false);

            }
        }
    }

    //状態を反対にする、使ってないかも
    private void ChangeState()
    {
        if (state == 0)
        {
            SetState(1);
        }
        else
        {
            SetState(0);
        }
    }

    public int GetState()
    {
        return state;
    }

    //値をリセットする
    //敵の攻撃で濡らす場合はここを使う
    public void ResetState()
    {
        SetState(0);
        middle = 0f;
    }
}
