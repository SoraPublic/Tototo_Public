using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTileDisplay : MonoBehaviour
{
    public const float CHANGE_TIME = 5f;

    float time;
    int displayNum = 0;
    int waveLength;
    private StageGenerator stageGenerator;

    [SerializeField] private Image[] circles;
    [SerializeField] private Image guage;

    [SerializeField] private Color selectColor;
    [SerializeField] private Color unselectColor;


    private void Start()
    {
        time = 0;
        stageGenerator = StageManager.instance.stageGenerator;
        waveLength = StageManager.stageEntity.waveEntities.Length;
    }

    public void Update()
    {
        time += Time.deltaTime;

        guage.fillAmount = time / CHANGE_TIME;

        if (StageManager.instance.state == StageManager.State.SelectCard)
        {
            if (time > CHANGE_TIME)
            {
                displayNum++;
                if (displayNum >= waveLength)
                {
                    displayNum = 0;
                }

                changeTile(displayNum);

                time = 0f;
            }
        }
    }

    private void changeTile(int num)
    {

        foreach(Image image in circles)
        {
            image.color = unselectColor;
        }

        circles[num].color = selectColor;

        stageGenerator.TileDisplay(displayNum);

    }

}
