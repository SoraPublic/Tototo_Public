using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCheck : MonoBehaviour
{
    //論理部分変更はとりあえずなし

    [SerializeField] private MagicEntity shape1;
    [SerializeField] private MagicEntity shape2;
    [SerializeField] private MagicEntity shape3;

    private int[,] MC1;
    private int[,] MC2;
    private int[,] MC3;
    private int[,] MC4;
    private int[,] MC5;

    private int[,,] magicArray;

    public MagicManager magicManager;

    private bool isCheck = false;
    private const int tileSize = 5;

    public void SetMagic()
    {
        magicManager = StageManager.instance.magicManager;

        MC1 = SizeCheck(magicManager.magics[0].array);
        MC2 = SizeCheck(magicManager.magics[1].array);
        MC3 = SizeCheck(magicManager.magics[2].array);
        MC4 = SizeCheck(magicManager.magics[3].array);
        MC5 = SizeCheck(magicManager.magics[4].array);

        isCheck = true;
    }

    public void Check(TileState[] tileStates)
    {
        if (isCheck == false)
        {
            //準備できていないので、何もしない
            //CardSelect中
            return;
        }

        if (ShapeCheck(tileStates, MC1))
        {
            //Debug.Log("shape1あり");
            magicManager.magics[0].Active();
        }
        else
        {
            //Debug.Log("shape1なし");
        }
        if (ShapeCheck(tileStates, MC2))
        {
            //Debug.Log("shape2あり");
            magicManager.magics[1].Active();
        }
        else
        {
            //Debug.Log("shape2なし");
        }
        if (ShapeCheck(tileStates, MC3))
        {
            //Debug.Log("shape3あり");
            magicManager.magics[2].Active();
        }
        else
        {
            //Debug.Log("shape3なし");
        }
        if (ShapeCheck(tileStates, MC4))
        {
            //Debug.Log("shape4あり");
            magicManager.magics[3].Active();
        }
        else
        {
            //Debug.Log("shape4なし");
        }
        if (ShapeCheck(tileStates, MC5))
        {
            //
            //Debug.Log("shape5あり");
            magicManager.magics[4].Active();
        }
        else
        {
            //Debug.Log("shape5なし");
        }
    }

    /***********************************************************************************/
    /*  同時に成立可能         */

    private bool ShapeCheck(TileState[] tileStates, int[,] MC)
    {

        //tileの数がshapeの数に満たない場合はshapeなし
        if (TileNumCheck(tileStates) < LevelCheck(MC))
        {
            return false;
        }

        int xSize = MC.GetLength(0);
        int ySize = MC.GetLength(1);

        //tile側

        for (int i=0; i <= tileSize - xSize;i++) 
        {
            for (int j=0;j<= tileSize - ySize;j++) 
            {
                if (SquareCheck(tileStates,MC,i,j)) 
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool SquareCheck(TileState[] tileStates, int[,] MC, int x, int y)
    {
        int xSize = MC.GetLength(0);
        int ySize = MC.GetLength(1);

        for (int i=0;i<xSize;i++) 
        {
            for (int j=0;j<ySize;j++) 
            {
                if (MC[i,j] == 1) 
                {
                    if (tileStates[x + i + 5*(y + j)].GetState() != 1)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }



    //shapeのtileの数を数える
    private int LevelCheck(int[,] MC)
    {
        int num = 0;

        for (int i = 0; i < MC.GetLength(0); i++)
        {
            for (int j = 0; j < MC.GetLength(1); j++)
            {
                num += MC[i,j];
            }
        }

        return num;
    }

    //乾いたtileの数を数える
    private int TileNumCheck(TileState[] tileStates)
    {
        int num = 0;
        for (int i = 0; i < tileStates.Length; i++)
        {
            num += tileStates[i].GetState();
        }

        return num;
    }

    //1次元配列で正方形で入力されたものを2次元配列の適切な大きさな図形に変更
    private int[,] SizeCheck(int[] shape)
    {
        float sqrt = Mathf.Sqrt(shape.Length);
        int edge = (int)sqrt;
        int[,] twoShape = new int[edge, edge];

        //1次元配列から2次元配列へ
        for (int i = 0; i < edge; i++)
        {
            for (int j = 0; j < edge; j++)
            {
                twoShape[i, j] = shape[i + j * edge];
            }
        }

        //正方形から適切な大きさに変更
        int[,] num = ArrayTrans(twoShape);


        /*
        for (int i = 0; i < num.GetLength(0); i++)
        {
            for (int j = 0; j < num.GetLength(1); j++)
            {
                Debug.Log("(" + i + "," + j + ") = " + num[i, j]);
            }
        }
        */

        return num;

    }

    private int[,] ArrayTrans(int[,] array) 
    {
        int[,] num = ArrayTrasnsTop(array);
        num = ArrayTrasnsUnder(num);
        num = ArrayTrasnsLeft(num);
        num = ArrayTrasnsRight(num);

        return num;
    }

    private int[,] ArrayTrasnsTop(int[,] array)
    {
        //xの長さ
        int x = array.GetLength(0);
        int y = array.GetLength(1);

        //一番上のxの数を測る
        int num = 0;
        for (int i = 0; i < x; i++)
        {
            num += array[i, 0];
        }

        if (num == 0)
        {
            int[,] vs = new int[x, y - 1];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y - 1; j++)
                {
                    vs[i, j] = array[i, j + 1];
                }
            }

            return ArrayTrasnsTop(vs);
        }
        else
        {
            return array;
        }
    }

    private int[,] ArrayTrasnsUnder(int[,] array)
    {
        //xの長さ
        int x = array.GetLength(0);
        int y = array.GetLength(1);

        //一番下のxの数を測る
        int num = 0;
        for (int i = 0; i < x; i++)
        {
            num += array[i, y - 1];
        }

        if (num == 0)
        {
            int[,] vs = new int[x, y - 1];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y - 1; j++)
                {
                    vs[i, j] = array[i, j];
                }
            }

            return ArrayTrasnsUnder(vs);
        }
        else
        {
            return array;
        }
    }

    private int[,] ArrayTrasnsLeft(int[,] array)
    {
        int x = array.GetLength(0);
        int y = array.GetLength(1);

        //一番左のxの数を測る
        int num = 0;
        for (int i = 0; i < y; i++)
        {
            num += array[0, i];
        }
        if (num == 0)
        {
            int[,] vs = new int[x - 1, y];

            for (int i = 0; i < x - 1; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    vs[i, j] = array[i + 1, j];
                }
            }

            return ArrayTrasnsLeft(vs);
        }
        else
        {
            return array;
        }
    }

    private int[,] ArrayTrasnsRight(int[,] array)
    {
        int x = array.GetLength(0);
        int y = array.GetLength(1);

        //一番左のxの数を測る
        int num = 0;
        for (int i = 0; i < y; i++)
        {
            num += array[x - 1, i];
        }
        if (num == 0)
        {
            int[,] vs = new int[x - 1, y];

            for (int i = 0; i < x - 1; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    vs[i, j] = array[i, j];
                }
            }

            return ArrayTrasnsRight(vs);
        }
        else
        {
            return array;
        }
    }
}
