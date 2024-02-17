using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageEffecter : MonoBehaviour
{
    [SerializeField] private TelopManager telop;
    [SerializeField] public StageCameraManager cameraManager;
    [SerializeField] private GameObject rain;
    [SerializeField] private AudioSource rainMusic;

    [SerializeField] private AudioSource tileSound;
    [SerializeField] private GameObject Rainbow;
    [SerializeField] private GameObject Light;

    [SerializeField] private GameObject player;
    //[SerializeField] private GameObject enemy;
    [SerializeField] private GameObject tiles;
    [SerializeField] private GameObject enemyCanvas;

    [SerializeField]
    private Material mat;

    private SceneLoader sceneLoader;

    private const float EffectTime = 4f;
    private const float MoveCameraTime = 1f;

    public void SetUp() 
    {
        sceneLoader = new SceneLoader();
    }

    public void StageIn() 
    {
        StartCoroutine(StageInCor());    
    }

    private IEnumerator StageInCor() 
    {
        //�X�e�[�W�J�n����J�[�h�I���̊�

        StageManager.instance.state = StageManager.State.Effect;
        //���o

        //�J�������������
        cameraManager.SetCamera(false);

        yield return new WaitForSeconds(0f);

        StageManager.instance.state = StageManager.State.SelectCard;

        sceneLoader.LoadCardSelect();
    }

    public void BattleStart()
    {
        StartCoroutine(BattleStartCor());
    }

    private IEnumerator BattleStartCor()
    {
        //�J�[�h�I������o�g���J�n�܂ł̊�

        StageManager.instance.state = StageManager.State.Effect;
        //���o

        //�X�e�[�W���̕\��
        telop.DisplayTelop("Stage "+ StageManager.stageEntity.stgaeNum, EffectTime);

        yield return new WaitForSeconds(EffectTime + 1f/10f);

        //�E�F�[�u���̕\��
        telop.DisplayTelop("Wave 1/3", EffectTime);

        yield return new WaitForSeconds(EffectTime);

        cameraManager.SetCamera(true);

        yield return new WaitForSeconds(MoveCameraTime);

        StageManager.instance.state = StageManager.State.Battle;
    }

    public void ChangeWave() 
    {
        StartCoroutine(ChangeWaveCor());
    }

    private IEnumerator ChangeWaveCor() 
    {
        //�o�g������o�g���̊ԁ@�E�F�[�u�̕ύX

        StageManager.instance.state = StageManager.State.Effect;
        //���o
        cameraManager.SetCamera(false);

        yield return new WaitForSeconds(MoveCameraTime);

        //�J����������������̂ŃE�F�[�u�̕ύX���s��
        StageManager.instance.ChangeWave();


        //�E�F�[�u���̕\��
        telop.DisplayTelop("Wave "+ (StageManager.instance.waveNum+1) +"/3", EffectTime);

        yield return new WaitForSeconds(EffectTime);

        cameraManager.SetCamera(true);

        yield return new WaitForSeconds(MoveCameraTime);

        StageManager.instance.state = StageManager.State.Battle;
    }

    public void OpenResult() 
    {
        StartCoroutine(OpenResultCor());
    }

    private IEnumerator OpenResultCor()
    {
        //���U���g�̕\��
        StageManager.instance.state = StageManager.State.Effect;
        //���o

        rainMusic.Stop();
        rain.SetActive(false);
        cameraManager.SetCamera(false);
        Rainbow.SetActive(true);
        Light.SetActive(true);
        RenderSettings.skybox = mat;

        yield return new WaitForSeconds(MoveCameraTime + 2);

        player.SetActive(false);
        tiles.SetActive(false);
        enemyCanvas.SetActive(false);
        StageManager.instance.enemy.nowModel.SetActive(false);

        StageManager.instance.state = StageManager.State.Result;
        sceneLoader.LoadResult();

    }
}
