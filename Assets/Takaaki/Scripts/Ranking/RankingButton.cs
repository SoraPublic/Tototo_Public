using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenRanking);
    }


    private void OpenRanking()
    {
        var time = (int)(StageManager.instance.clearTime);
        int minutes = (int)(time / 60);
        int second = (int)(time % 60);
        var timeScore = new System.TimeSpan(0, 0, minutes, second, 0);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(timeScore,StageManager.stageEntity.stgaeNum);
    }
}
