using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColorController : MonoBehaviour
{
    Material[] mat;

    void Start()    //マテリアルを配列で取得
    {
        mat = this.GetComponent<Renderer>().materials;

        //シェーダーの色状態をfalse(黒色)にする命令
        foreach (Material rend in mat)
        {
            rend.DisableKeyword("IS_CLEARED");
            rend.DisableKeyword("IS_OPENED");
        }
    }

    public void ChangeClear()
    {
        if (mat != null)
        {
            foreach (Material rend in mat)
            {
                rend.EnableKeyword("IS_CLEARED");
            }
        }
    }

    public void ChangeOpen()
    {
        if (mat != null)
        {
            foreach (Material rend in mat)
            {
                rend.EnableKeyword("IS_OPENED");
            }
        }
    }
    private void OnDestroy()
    {
        if (mat != null)
        {
            foreach (Material rend in mat)
            {
                Destroy(rend);
            }
            
        }
    }
}
