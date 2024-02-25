using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicEntity", menuName = "Create MagicEntity")]

public class MagicEntity : ScriptableObject
{
    //形
    [Header("魔法陣の形")]
    [Range(0, 1)]
    public int[] array;



    //[Header("攻撃力")] public float attackDamage;

    [Header("レベル")] public int level;


    //能力
    [Header("スキル")] public Skill skill;



    //画像
    //まだ使わない
    public Sprite icon;

    [Header("カードの通し番号")]
    public int cardNum;
}
