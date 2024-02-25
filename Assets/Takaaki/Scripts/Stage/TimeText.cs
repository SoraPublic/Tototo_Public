using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void DisplayTime(float time)
    {
        int minutes = (int)(time / 60);
        int second = (int)(time % 60);

        text.text = minutes.ToString("00") +":" + second.ToString("00");
    }
}
