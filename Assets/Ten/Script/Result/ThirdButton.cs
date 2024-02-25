using System.Collections.Generic;
using UnityEngine;

public class ThirdButton : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private Animator thisAnimator;
    private ResultManager resultManager;
    //private int card;
    private bool isGetCard;
    private List<int> willGetCardNum;
    [SerializeField]
    private MagicCardGenerator cardGenerator;
    [SerializeField]
    private MagicStorage magicStorage;

    private void Start()
    {
        thisAnimator = transform.parent.gameObject.GetComponent<Animator>();
        resultManager = new ResultManager();
        ResultData result = resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.StageFile(StageManager.stageEntity.name));
        //card = result.nowGetCard - 1;
        isGetCard = false;
    }

    public void SetCard(List<int> list)
    {
        willGetCardNum = new List<int>(list);
    }

    public void OnClick()
    {
        thisAnimator.SetTrigger("out");
    }

    public void CardChange()
    {
        if (willGetCardNum.Count > 0)
        {
            //Debug.Log("表示残り:" + willGetCardNum.Count + "枚");
            isGetCard = true;
            GameObject card = cardGenerator.GenerateMagicCard(magicStorage.MagicList[willGetCardNum[0]]);
            card.transform.SetParent(transform.parent.transform.Find("Card").gameObject.transform);
            card.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            card.transform.localPosition = new Vector3(0f, 30f, 0f);
            //Debug.Log("表示中:" + magicStorage.MagicList[willGetCardNum[0]] + "番");
            willGetCardNum.RemoveAt(0);
        }

        thisAnimator.SetBool("isGetCard", isGetCard);

        if (!isGetCard)
        {
            animator.SetTrigger("next");
            //transform.parent.gameObject.GetComponent<Animator>().SetTrigger("push");
            StageManager.instance.stageEffecter.cameraManager.SetCamera(false);
        }

        isGetCard = false;
        //thisAnimator.SetBool("isGetCard", isGetCard);
    }

    public void FlagChange()
    {
        thisAnimator.SetBool("isGetCard", isGetCard);
    }
}
