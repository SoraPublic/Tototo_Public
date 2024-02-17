using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMover : MonoBehaviour
{
    //プレイヤーの移動

    private TileManager tileManager;
    private StageManager stageManager;
    private PlayerManager playerManager;

    public PlayerAnimator playerAnimator;

    private float count = 0f;

    private Tween doX;
    private Tween doZ;

    [SerializeField] private AudioSource moveSound;

    public void SetUp() 
    {
        tileManager = StageManager.instance.tileManager;
        stageManager = StageManager.instance;
        playerManager = StageManager.instance.playerManager;
    }

    private void Update()
    {
        count -= Time.deltaTime;
    }

    //移動の判定
    public void MoveCheck(int x, int y)
    {
        int nextX = playerManager.nowX + x;
        int nextY = playerManager.nowY + y;

        //移動不可を検知
        if ( 0 < count ) 
        {
            Debug.Log("移動不可：移動中");
            return;
        }
        if (nextX < 0 || stageManager.row <= nextX)
        {
            Debug.Log("移動不可：範囲外");
            return;
        }
        if (nextY < 0 || stageManager.column <= nextY)
        {
            Debug.Log("移動不可：範囲外");
            return;
        }
        if (tileManager.GetTile(nextX, nextY).situation == TileState.TileSituation.Block) 
        {
            Debug.Log("移動不可：障害物あり");
            return;
        }
        if (tileManager.GetTile(nextX, nextY).situation == TileState.TileSituation.Empty)
        {
            Debug.Log("移動不可：タイルなし");
            return;
        }
        if(stageManager.playerState == PlayerState.Stop)
        {
            Debug.Log("移動不可：被弾中");
            return;
        }

        //方向を変更
        float angle = x * 90 + y*(y + 1) * 90;
        Transform player = this.gameObject.transform;
        player.DORotate(new Vector3(0f,angle,0f),0f);

        moveSound.Play();

        MovePlayer(nextX,nextY,PlayerStatus.instance.GetMoveSpeed());


    }

    //x,yの情報から移動
    public void MovePlayer(int x, int y, float moveTime) 
    {
        TileState preTile = tileManager.GetTile(playerManager.nowX, playerManager.nowY);
        if(preTile != null)
        {
            preTile.isPressing = false;
        }

        playerManager.nowX = x;
        playerManager.nowY = y;

        count = moveTime;

        TileState tileState =  tileManager.GetTile(x,y);
        tileState.isPressing = true;

        Transform transform = tileState.GetComponent<Transform>();

        Vector3 vector3 = transform.position;

        MovePlayer(vector3, moveTime);
    }
    

    //vector3の情報から移動

    private void MovePlayer(Vector3 vector3, float moveTime)
    {
        Transform player = this.gameObject.transform;

        playerAnimator.WalkAnim(true);
        doX = player.DOMove(new Vector3(vector3.x, player.position.y, vector3.z), moveTime).SetEase(Ease.InOutSine).OnComplete(() => 
        {
            playerAnimator.WalkAnim(false);
        });

        //doX = player.DOMoveX(vector3.x, moveTime).SetEase(Ease.InOutSine);

        //doZ = player.DOMoveZ(vector3.z, moveTime).SetEase(Ease.InOutSine);
        

    }

    public void SuspendMove()
    {
        doX.Kill();
        doZ.Kill();
    }
}
