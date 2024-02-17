using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //���@�w�]�X
    [SerializeField] private TileCheck tileCheck;
     
    //�^�C������̔z��
    [System.NonSerialized]
    public TileState[] tileStates = new TileState[25];

    [System.NonSerialized]
    public int row; //�s
    [System.NonSerialized]
    public int column;�@// ��

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

    //�^�C���S�������Z�b�g
    public void ResetState()
    {
        for (int i = 0; i < 25; i++)
        {
            tileStates[i].ResetState();
        }
    }

    //���@�w�̃`�F�b�N
    public void Check()
    {
        tileCheck.Check(tileStates);
    }

    //�^�C�����ق����Ƃ��Ɏg��
    public TileState GetTile(int x, int y)
    {
        if (x < 0 || row <= x)
        {
            Debug.Log("���̍s�͑��݂��܂���");
        }
        else if (y < 0 || column <= y) 
        {
            Debug.Log("���̗�͑��݂��܂���");
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
