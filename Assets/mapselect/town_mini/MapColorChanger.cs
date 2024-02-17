using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColorChanger : MonoBehaviour
{
    public bool isOpened = false;   //マップが解放されているか否か
    public bool isCleared = false;  //マップがクリアされているか否か

    Material[] mat;

    void Start()    //マテリアルを配列で取得
    {
        mat = GetComponent<Renderer>().materials;

        //シェーダーの色状態をfalse(黒色)にする命令
        foreach (Material rend in mat)
        {
            rend.DisableKeyword("IS_CLEARED");
            rend.DisableKeyword("IS_OPENED");
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Material rend in mat)
        {


            if (isCleared)//クリアしたらシェーダー側のブール値を変更し、色をつける
            {
                rend.EnableKeyword("IS_CLEARED");
            }
            else if (isOpened)//オープンしたらシェーダー側のブール値を変更し、グレイスケールにする
            {
                rend.EnableKeyword("IS_OPENED");

            }
            else {
                rend.DisableKeyword("IS_CLEARED");
                rend.DisableKeyword("IS_OPENED");
            }
        }
    }
}
