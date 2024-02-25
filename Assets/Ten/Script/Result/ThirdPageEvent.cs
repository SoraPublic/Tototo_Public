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
