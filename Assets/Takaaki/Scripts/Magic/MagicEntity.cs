using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicEntity", menuName = "Create MagicEntity")]

public class MagicEntity : ScriptableObject
{
    //�`
    [Header("���@�w�̌`")]
    [Range(0, 1)]
    public int[] array;



    //[Header("�U����")] public float attackDamage;

    [Header("���x��")] public int level;


    //�\��
    [Header("�X�L��")] public Skill skill;



    //�摜
    //�܂��g��Ȃ�
    public Sprite icon;

    [Header("�J�[�h�̒ʂ��ԍ�")]
    public int cardNum;
}
