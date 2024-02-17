using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

[CreateAssetMenu(fileName = "WaveEntity", menuName = "Create WaveEntity")]
public class WaveEntity : ScriptableObject
{
    [Header("�^�C��")]
    public GameObject TilePrefab;

    [Header("�^�C���̑傫��")]
    public int row;
    public int column;

    [Header("�v���C���[�̏����ʒu")]
    public int playerX;
    public int playerY;

    [Header("�G�̏��")]
    public Info_Enemy infoEnemy;
}
