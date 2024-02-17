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
        //ステージ開始からカード選択の間

        StageManager.instance.state = StageManager.State.Effect;
        //演出

        //カメラを上向きに
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
        //カード選択からバトル開始までの間

        StageManager.instance.state = StageManager.State.Effect;
        //演出

        //ステージ数の表示
        telop.DisplayTelop("Stage "+ StageManager.stageEntity.stgaeNum, EffectTime);

        yield return new WaitForSeconds(EffectTime + 1f/10f);

        //ウェーブ数の表示
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
        //バトルからバトルの間　ウェーブの変更

        StageManager.instance.state = StageManager.State.Effect;
        //演出
        cameraManager.SetCamera(false);

        yield return new WaitForSeconds(MoveCameraTime);

        //カメラが上を向いたのでウェーブの変更を行う
        StageManager.instance.ChangeWave();


        //ウェーブ数の表示
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
        //リザルトの表示
        StageManager.instance.state = StageManager.State.Effect;
        //演出

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
