using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ReturnTilePosition ReturnTilePosition;

    private int nowx, nowy; //現在の座標
    private bool close;
    private Vector3 target; //進行方向のタイルのPosition
    private int x, y;
    [SerializeField] private float step = 2f; //移動速度
    
    private Vector3 latestPos;
    void Start()
    {
        target = this.transform.position;
    }
 
    void Update()
    {
        Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
        latestPos = transform.position;  //前回のPositionの更新

        nowStateCheck(); //現在いるタイルの情報を取得
        x = nowx; //現在の座標を変数に代入
        y = nowy;

        if ((this.transform.position.x == target.x) && (this.transform.position.z == target.z)) //自身が移動中でなければ移動可能
        {
            TargetChange();
        }

        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
        }

        this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x,this.transform.position.y,target.z), step * Time.deltaTime); //移動処理（今はxとzしか動かしていない）
      
    }

    void TargetChange() //進みたいタイルの座標を更新する関数
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            y = y - 1;
            target = ReturnTilePosition.TileSearch(x, y,this.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            y = y + 1;
            target = ReturnTilePosition.TileSearch(x, y, this.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            x = x - 1;
            target = ReturnTilePosition.TileSearch(x, y, this.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            x = x + 1;
            target = ReturnTilePosition.TileSearch(x, y, this.transform.position);
        }
    }

    void nowStateCheck() //真下にレイを飛ばしてタイルの座標を取得（それぞれのタイルは構造体で座標を自分で持っている）
    {        
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 6.0f))
        {
            nowx = hit.collider.gameObject.GetComponent<TileCoordinate>().coordinate.x;
            nowy = hit.collider.gameObject.GetComponent<TileCoordinate>().coordinate.y;     
        }
    }

}
