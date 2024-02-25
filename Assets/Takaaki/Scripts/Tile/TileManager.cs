using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //魔法陣云々
    [SerializeField] private TileCheck tileCheck;
     
    //タイル一個ずつの配列
    [System.NonSerialized]
    public TileState[] tileStates = new TileState[25];

    [System.NonSerialized]
    public int row; //行
    [System.NonSerialized]
    public int column;　// 列

    [SerializeField] private AudioSource tileSound;

    public void SetUp() 
    {
        tileStates = this.GetComponentsInChildren<TileState>();

        row = StageManager.instance.row;
        column = StageManager.instance.column;

        foreach(TileState tileState in tileStates)
        {
            tileState.tileSound = tileSound;
            tileState.SetUpTileView();
        }
    }

    //タイル全部をリセット
    public void ResetState()
    {
        for (int i = 0; i < 25; i++)
        {
            tileStates[i].ResetState();
        }
    }

    //魔法陣のチェック
    public void Check()
    {
        tileCheck.Check(tileStates);
    }

    //タイルがほしいときに使う
    public TileState GetTile(int x, int y)
    {
        if (x < 0 || row <= x)
        {
            Debug.Log("その行は存在しません");
        }
        else if (y < 0 || column <= y) 
        {
            Debug.Log("その列は存在しません");
        }
        else
        {
            return tileStates[x + column * y];
        }

        return null;
    }

   public TileState.TileSituation GetSituation(int i)
    {
        return tileStates[i].situation;
    }
}
