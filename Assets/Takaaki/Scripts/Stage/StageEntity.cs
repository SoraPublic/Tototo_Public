using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageEntity", menuName = "Create StageEntity")]
public class StageEntity : ScriptableObject
{
    [Header("ステージのナンバー")]
    public int stgaeNum;

    [Header("ステージの背景")]
    public GameObject stageBackGround;

    [Header("Wave管理")]
    public WaveEntity[] waveEntities;

    [Header("次のステージ")]
    public StageEntity stageEntity;

    [Header("評価基準")]
    public ResultEntity resultEntity;

    [Header("カード選択画面の背景")]
    public Sprite cardSelectBack;

    [Header("このステージで獲得できるカード")]
    public MagicEntity[] magics;

    [Header("このステージのターゲッティングの色")]
    public Color TargetColor;
}
