using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationText : MonoBehaviour
{
   private TextMeshProUGUI text;

    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private GameObject[] stars; 

    public void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(ResultEntity resultEntity,ResultData resultData, string stageName)
    {
        stageText.text = stageName;

        text.text = "" + resultEntity.clear.ToString("d2") + "回以上クリア\n"
            + "被弾回数" + resultEntity.hit.ToString("d2") + "回以下\n"
            + "討伐時間" + (resultEntity.clearTime / 60).ToString("d2") + ":" + (resultEntity.clearTime % 60).ToString("d2") + "以内";

        if (resultData.clear >= resultEntity.clear)
        {
            stars[0].SetActive(true);
        }
        else
        {
            stars[0].SetActive(false);
        }
        if (resultData.hit <= resultEntity.hit)
        {
            stars[1].SetActive(true);
        }
        else
        {
            stars[1].SetActive(false);
        }
        if ((int)resultData.time <= resultEntity.clearTime)
        {
            stars[2].SetActive(true);
        }
        else
        {
            stars[2].SetActive(false);
        }
    }
}

