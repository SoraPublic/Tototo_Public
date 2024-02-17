using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

[CreateAssetMenu(fileName = "WaveEntity", menuName = "Create WaveEntity")]
public class WaveEntity : ScriptableObject
{
    [Header("タイル")]
    public GameObject TilePrefab;

    [Header("タイルの大きさ")]
    public int row;
    public int column;

    [Header("プレイヤーの初期位置")]
    public int playerX;
    public int playerY;

    [Header("敵の情報")]
    public Info_Enemy infoEnemy;
}
