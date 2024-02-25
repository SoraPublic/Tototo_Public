using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image BackGround;
    [SerializeField] private Sprite sprite;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Material mat;
    [SerializeField] private GameObject[] rain;

    // Start is called before the first frame update
    void Start()
    {
        image.color = new Color(1,1,1,0);

        if (ClearCheck())
        {
            BackGround.sprite = sprite;
            RenderSettings.skybox = mat;
            foreach(GameObject obj in rain)
            {
                obj.SetActive(false);
            }
        }
    }

    private bool ClearCheck()
    {
        string stageName = "Stage";
        ResultManager resultManager = new ResultManager();


        string str = stageName + 5.ToString("00");
        ResultData result = resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.StageFile(str));

        if (result.clear == 0) //クリアしていない
        {
            return false;
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneLoader sceneLoader = new SceneLoader();

            audioSource.Play();

            image.DOFade(1, 1f).OnComplete(() =>
            {
                image.DOColor(new Color(0, 0, 0), 1f).OnComplete(() => 
                { 
                    sceneLoader.LoadStageSelect();
                });
            });

            
        }
    }
}
