using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultWriter : MonoBehaviour
{
    public GameObject data;
    public GameObject[] star = new GameObject[3];
    private TextMeshProUGUI text_result;
    private ResultManager resultManager;
    public GameObject resultTerms;
    private TextMeshProUGUI text_terms;
    private ResultEntity resultEntity;
    private string[] marks = new string[3];
    [SerializeField]
    GameObject firstButton;
    [SerializeField]
    GameObject secondButton;
    private List<int> willGetCardNum;
    private DataManger dataManger;

    // Start is called before the first frame update
    void Start()
    {
        resultManager = new ResultManager();
        dataManger = new DataManger();
        willGetCardNum = new List<int>(StageManager.instance.willGetCardNum);
        resultEntity = StageManager.stageEntity.resultEntity;
        ResultData result = resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.CurrentStageFile);//Application.dataPath + "/" + SavePathName.StageFile(StageManager.stageEntity.name));//"/ResultData/" + StageManager.instance.stageEntity.name + "_Data.json");
        GameData gameData = dataManger.LoadGameData();
        text_result = data.GetComponent<TextMeshProUGUI>();

        text_result.text = "・クリア回数: " + result.clear.ToString("d2") + "回\n"
            + "・被弾回数: " + result.hit.ToString("d2") + "回\n"
            + "・討伐時間: " + ((int)result.time / 60).ToString("d2") + ":" + ((int)result.time % 60).ToString("d2");

        text_terms = resultTerms.GetComponent<TextMeshProUGUI>();

        if (result.clear >= resultEntity.clear)
        {
            marks[0] = "<sprite=1>";
        }
        else
        {
            marks[0] = "<sprite=0>";
        }
        if (result.hit <= resultEntity.hit)
        {
            marks[1] = "<sprite=1>";
        }
        else
        {
            marks[1] = "<sprite=0>";
        }
        if ((int)result.time <= resultEntity.clearTime)
        {
            marks[2] = "<sprite=1>";
            /*if (!gameData.cardLists.Contains(StageManager.stageEntity.magics[2].cardNum))
            {
                willGetCardNum.Add(StageManager.stageEntity.magics[2].cardNum);
            }*/
        }
        else
        {
            marks[2] = "<sprite=0>";
        }

        secondButton.transform.gameObject.GetComponent<SecondButton>().SetCard(willGetCardNum);

        text_terms.text = marks[0] + resultEntity.clear.ToString("d2") + "回以上クリア\n"
            + marks[1] + "被弾回数" + resultEntity.hit.ToString("d2") + "回以下\n"
            + marks[2] + "討伐時間" + ((int)resultEntity.clearTime / 60).ToString("d2") + ":" + ((int)resultEntity.clearTime % 60).ToString("d2") + "以内";

        StartCoroutine(FirstAnim());
    }
    private IEnumerator FirstAnim()
    {
        yield return new WaitForSeconds(1.0f);
        firstButton.gameObject.SetActive(true);
        firstButton.GetComponent<Animator>().SetTrigger("Up");
        yield break;
    }

    public IEnumerator SecondAnim(int numofStar)
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < numofStar; i++)
        {
            star[i].transform.Find("Star-get").gameObject.SetActive(true);
            Animator animator = star[i].GetComponent<Animator>();
            animator.SetTrigger("get");
            yield return new WaitForSeconds(0.3f);
        }

        secondButton.gameObject.SetActive(true);
        secondButton.GetComponent<Animator>().SetTrigger("Up");

        yield break;
    }
}
