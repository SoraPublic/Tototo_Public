using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tilesParent;

    [SerializeField] private GameObject[] tiles;
    [SerializeField] private WaveEntity[] waves;

    public void GenerateStage(WaveEntity[] waveEntities)
    {
        waves = waveEntities;

        //  �^�C���̐���
        tiles = new GameObject[waveEntities.Length];

        for(int i = 0; i < waveEntities.Length; i++)
        {
            tiles[i] = Instantiate(waveEntities[i].TilePrefab);

            // �^�C���̏�����
            tiles[i].transform.SetParent(tilesParent.transform);
            tiles[i].transform.localScale = new Vector3(1f, 1f, 1f);
            tiles[i].transform.localPosition = new Vector3(0f, 0f, 0f);

            tiles[i].SetActive(false);
        }

        //�w�i�̕\��
        SetBackGround();
    }

    public void SetTile(int waveNum) 
    {
        TileDisplay(waveNum);

        // �N���X�̐ݒ�
        StageManager.instance.tileManager = tiles[waveNum].GetComponent<TileManager>();
        StageManager.instance.tileCheck = tiles[waveNum].GetComponent<TileCheck>();

        //�^�C���̐ݒ�
        StageManager.instance.row = waves[waveNum].row;
        StageManager.instance.column = waves[waveNum].column;
        StageManager.instance.playerManager.nowX = waves[waveNum].playerX;
        StageManager.instance.playerManager.nowY = waves[waveNum].playerY;
    }

    public void TileDisplay(int waveNum)
    {
        foreach(GameObject gameObject in tiles)
        {
            gameObject.SetActive(false);
        }

        //�^�C���̕\��
        tiles[waveNum].SetActive(true);
    }

    private void SetBackGround()
    {
        GameObject backGround = Instantiate(StageManager.stageEntity.stageBackGround);
    }


    

}
