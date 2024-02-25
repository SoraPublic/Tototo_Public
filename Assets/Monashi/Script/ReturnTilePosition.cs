using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTilePosition : MonoBehaviour
{
    [SerializeField] private List<GameObject> tiles = new List<GameObject>(); //全てのタイル（ゲームオブジェクト）を格納
    private int i;
   
    public Vector3 TileSearch(int x,int y,Vector3 beforepos)
    {
        i = 0;

        while ((y != tiles[i].GetComponent<TileCoordinate>().coordinate.y)) //タイルのy座標検索
        {
            i = i + 1;
        }

        while (x != tiles[i].GetComponent<TileCoordinate>().coordinate.x) //タイルのx座標検索
        {
            i = i + 1;
        }

        if(tiles[i].GetComponent<TileCoordinate>().coordinate.Close == true) //タイルに障害物があるかどうかのbool変数を取得
        {
            return beforepos; //今いるポジションを返して進まないようにする
        }

        return tiles[i].transform.position; //進みたい方向のタイルのポジションを返す
    }

}
