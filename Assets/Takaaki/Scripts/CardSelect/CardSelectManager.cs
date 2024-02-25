using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelectManager : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;

    public static CardSelectManager instance;
    public CardSelect cardSelect;

    private int cardNum;

    private void Start()
    {
        SetCardNum(0);
        instance = this;
        button.onClick.AddListener(EndCardSelect);
    }

    private MagicEntity[] GetMagic(List<CardSelect.SelectedCard> selectedCards )
    {
        MagicEntity[] magicEntities = new MagicEntity[5];

        int i = 0;
        foreach( CardSelect.SelectedCard card in selectedCards)
        {
            magicEntities[i] = card.MagicEntity;
            i++;
        }

        return magicEntities;
    }


    public void EndCardSelect() 
    {
        if(cardNum == 5)
        {
            StageManager.instance.magicManager.magicEntities = GetMagic(cardSelect.selectedCards);

            StageManager.instance.SetBattle();
        }
    }

    public void SetCardNum(int num)
    {
        text.text = "魔法を選択\n(" + num + " / 5)";
        cardNum = num;
    }
}