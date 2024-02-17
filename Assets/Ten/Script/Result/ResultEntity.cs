using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CreateResultEntity")]
public class ResultEntity : ScriptableObject
{
    [Header("クリア回数")]
    public int clear　= 0;

    [Header("被弾回数")]
    public int hit;

    [Header("討伐時間（秒）")]
    public int clearTime;

    [Header("制限時間（秒）")]
    public int rimitTime;
}
