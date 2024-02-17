using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColorChanger : MonoBehaviour
{
    public bool isOpened = false;   //�}�b�v���������Ă��邩�ۂ�
    public bool isCleared = false;  //�}�b�v���N���A����Ă��邩�ۂ�

    Material[] mat;

    void Start()    //�}�e���A����z��Ŏ擾
    {
        mat = GetComponent<Renderer>().materials;

        //�V�F�[�_�[�̐F��Ԃ�false(���F)�ɂ��閽��
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


            if (isCleared)//�N���A������V�F�[�_�[���̃u�[���l��ύX���A�F������
            {
                rend.EnableKeyword("IS_CLEARED");
            }
            else if (isOpened)//�I�[�v��������V�F�[�_�[���̃u�[���l��ύX���A�O���C�X�P�[���ɂ���
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
