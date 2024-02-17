using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPageEvent : MonoBehaviour
{
    [SerializeField]
    private ThirdButton thirdButton;

    public void CardChangeEvent()
    {
        thirdButton.CardChange();
    }

    public void FlagChangeEvent()
    {
        thirdButton.FlagChange();
    }
}
