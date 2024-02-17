using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageEntity", menuName = "Create StageEntity")]
public class StageEntity : ScriptableObject
{
    [Header("�X�e�[�W�̃i���o�[")]
    public int stgaeNum;

    [Header("�X�e�[�W�̔w�i")]
    public GameObject stageBackGround;

    [Header("Wave�Ǘ�")]
    public WaveEntity[] waveEntities;

    [Header("���̃X�e�[�W")]
    public StageEntity stageEntity;

    [Header("�]���")]
    public ResultEntity resultEntity;

    [Header("�J�[�h�I����ʂ̔w�i")]
    public Sprite cardSelectBack;

    [Header("���̃X�e�[�W�Ŋl���ł���J�[�h")]
    public MagicEntity[] magics;

    [Header("���̃X�e�[�W�̃^�[�Q�b�e�B���O�̐F")]
    public Color TargetColor;
}
