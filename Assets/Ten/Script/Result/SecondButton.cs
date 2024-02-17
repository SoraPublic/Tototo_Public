using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondButton : MonoBehaviour
{
    private Animator animator;
    ResultData result;
    [SerializeField]
    private GameObject thirdPage;
    private SceneLoader sceneLoader = new SceneLoader();
    private bool isfinish = false;
    private List<int> willGetCardNum;
    public ThirdButton thirdButton;
    [SerializeField]
    private MagicCardGenerator cardGenerator;
    [SerializeField]
    private MagicStorage magicStorage;

    private void Start()
    {
        GameObject parent = transform.parent.parent.gameObject;
        animator = parent.GetComponent<Animator>();
        ResultManager resultManager = new ResultManager();
        result = resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.StageFile(StageManager.stageEntity.name));//"/ResultData/" + StageManager.instance.stageEntity.name + "_Data.json");
    }

    public void SetCard(List<int> list)
    {
        willGetCardNum = new List<int>(list);
    }

    public void OnClick()
    {
        Debug.Log("now" + willGetCardNum.Count + "枚");
        if (willGetCardNum.Count > 0/*result.nowGetCard > 0*/)
        {
            StartCoroutine(GetCard());
            //sceneLoader.LoadGetChest();
            //animator.SetTrigger("next");
        }
        else
        {
            animator.SetTrigger("next");
        }
    }

    private IEnumerator GetCard()
    {
        StageManager.instance.stageEffecter.cameraManager.SetCamera(true);
        animator.SetTrigger("fade");

        yield return new WaitForSeconds(1f);

        StageManager.instance.chest.SetActive(true);

        while (!isfinish)
        {
            isfinish = StageManager.instance.chest.transform.gameObject.GetComponent<AnimFinish>().isfinish;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        thirdPage.transform.gameObject.SetActive(true);
        GameObject card = cardGenerator.GenerateMagicCard(magicStorage.MagicList[willGetCardNum[0]]);
        card.transform.SetParent(thirdPage.transform.Find("Card").gameObject.transform);
        card.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        card.transform.localPosition = new Vector3(0f, 30f, 0f);
        willGetCardNum.RemoveAt(0);
        thirdButton.SetCard(willGetCardNum);

        yield break;
    }
}
