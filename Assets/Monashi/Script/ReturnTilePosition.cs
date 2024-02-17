using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTilePosition : MonoBehaviour
{
    [SerializeField] private List<GameObject> tiles = new List<GameObject>(); //�S�Ẵ^�C���i�Q�[���I�u�W�F�N�g�j���i�[
    private int i;
   
    public Vector3 TileSearch(int x,int y,Vector3 beforepos)
    {
        i = 0;

        while ((y != tiles[i].GetComponent<TileCoordinate>().coordinate.y)) //�^�C����y���W����
        {
            i = i + 1;
        }

        while (x != tiles[i].GetComponent<TileCoordinate>().coordinate.x) //�^�C����x���W����
        {
            i = i + 1;
        }

        if(tiles[i].GetComponent<TileCoordinate>().coordinate.Close == true) //�^�C���ɏ�Q�������邩�ǂ�����bool�ϐ����擾
        {
            return beforepos; //������|�W�V������Ԃ��Đi�܂Ȃ��悤�ɂ���
        }

        return tiles[i].transform.position; //�i�݂��������̃^�C���̃|�W�V������Ԃ�
    }

}
