using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleCameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] forestCameras;
    [SerializeField] private GameObject[] townCameras;
    [SerializeField] private GameObject[] castelCameras;

    [SerializeField] private Image BackGround;

    private void Start()
    {
        StartCoroutine(ImageBack());
    }

    private IEnumerator ImageBack()
    {
        yield return new WaitForSeconds(13);

        BackGround.DOFade(0, 2).OnComplete(() =>
        {
            BackGround.gameObject.SetActive(false);
            StartCoroutine(ForestCamera());
        });

        
    }

    private IEnumerator ForestCamera()
    {
        foreach(GameObject gameObject in forestCameras)
        {
            gameObject.SetActive(true);
        }


        yield return null;
        forestCameras[0].SetActive(false);


        yield return new WaitForSeconds(2);

        forestCameras[1].SetActive(false);

        yield return new WaitForSeconds(8);

        forestCameras[2].SetActive(false);

        yield return new WaitForSeconds(5);

        forestCameras[3].SetActive(false);

        StartCoroutine(TownCamera());
    }

    private IEnumerator TownCamera()
    {
        foreach (GameObject gameObject in townCameras)
        {
            gameObject.SetActive(true);
        }
        yield return null;

        townCameras[0].SetActive(false);

        yield return new WaitForSeconds(3);

        townCameras[1].SetActive(false);

        yield return new WaitForSeconds(6);

        townCameras[2].SetActive(false);

        yield return new WaitForSeconds(9);
        townCameras[3].SetActive(false);

        StartCoroutine(CastelCamera());
    }

    private IEnumerator CastelCamera()
    {
        foreach (GameObject gameObject in castelCameras)
        {
            gameObject.SetActive(true);
        }
        yield return null;

        castelCameras[0].SetActive(false);

        yield return new WaitForSeconds(2);

        castelCameras[1].SetActive(false);

        yield return new WaitForSeconds(8);

        castelCameras[2].SetActive(false);

        yield return new WaitForSeconds(5);

        castelCameras[3].SetActive(false);

        StartCoroutine(ForestCamera());
    }
}
