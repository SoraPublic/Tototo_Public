using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColorManager : MonoBehaviour
{
    public enum STATE
    {
        CLEAR,
        OPEN,
        UNOPEN,
    }

    [SerializeField] private MapColorController[] stage01;
    [SerializeField] private MapColorController[] stage02;
    [SerializeField] private MapColorController[] stage03;
    [SerializeField] private MapColorController[] stage04;
    [SerializeField] private MapColorController[] stage05;

    [SerializeField] private MapColorChanger[] changer01;
    [SerializeField] private MapColorChanger[] changer02;
    [SerializeField] private MapColorChanger[] changer03;
    [SerializeField] private MapColorChanger[] changer04;
    [SerializeField] private MapColorChanger[] changer05;

    private MapColorController[][] stages = new MapColorController[5][];
    private MapColorChanger[][] changer = new MapColorChanger[5][];
    private void Awake()
    {
        stages[0] = stage01;
        stages[1] = stage02;
        stages[2] = stage03;
        stages[3] = stage04;
        stages[4] = stage05;

        changer[0] = changer01;
        changer[1] = changer02;
        changer[2] = changer03;
        changer[3] = changer04;
        changer[4] = changer05;
    }

    public void ChangeColor(int stageNum, STATE state)
    {
        foreach (MapColorChanger mapColorChanger in changer[stageNum])
        {
            if (state == STATE.CLEAR)
            {
                mapColorChanger.isCleared = true;
            }
            else if (state == STATE.OPEN)
            {
                mapColorChanger.isOpened = true;
            }
        }
    }
}

 