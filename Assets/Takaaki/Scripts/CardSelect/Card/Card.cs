using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private GameObject selectedCurver;
    [SerializeField] private TextMeshProUGUI text; 

    public CardState state;
    public MagicEntity magicEntity;


    private void Start()
    {
        selectedCurver.SetActive(false);
    }

    public void OnClick()
    {
        if(state == CardState.SELECTED_CONTENT)
        {
            UnselectCard_Content();
        }
        else if(state == CardState.UNSELECTED)
        {
            SelectCard();
        }
        else if (state == CardState.SELECTED_LIST)
        {
            UnselectCard_List();
        }
    }

    public void SelectCard()
    {
        if (CardSelectManager.instance.cardSelect.selectedNum >= 5)
        {
            return;
        }
        state = CardState.SELECTED_CONTENT;
        selectedCurver.SetActive(true);

        CardSelectManager.instance.cardSelect.SelectCard(magicEntity);
        
    }

    public void UnselectCard_Content()
    {
        state = CardState.UNSELECTED;
        selectedCurver.SetActive(false);

        CardSelectManager.instance.cardSelect.UnselectCard(magicEntity);
    }

    public void UnselectCard_List()
    {
        //contentの方のカードを探してcurverをはがす

        Card card = CardSelectManager.instance.cardSelect.SerchCard(magicEntity);

        card.NoCurver();

        CardSelectManager.instance.cardSelect.UnselectCard(magicEntity);
    }

    public void NoCurver()
    {
        selectedCurver.SetActive(false);
    }

    public void SetNumText(int num)
    {
        text.text = num.ToString();
    }
}
