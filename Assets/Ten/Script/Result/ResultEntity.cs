using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CreateResultEntity")]
public class ResultEntity : ScriptableObject
{
    [Header("�N���A��")]
    public int clear�@= 0;

    [Header("��e��")]
    public int hit;

    [Header("�������ԁi�b�j")]
    public int clearTime;

    [Header("�������ԁi�b�j")]
    public int rimitTime;
}
