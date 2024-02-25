using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private Sprite[] cardSprites;
    [SerializeField] private Sprite[] skillSprites;
    [SerializeField] private string[] skillTexts;


    public GameObject GenerateMagicCard(MagicEntity magic)
    {
        //設定済みのカードを生成
        GameObject card = GenerateEmptyCard();

        card.SetActive(true);

        //カードの見た目を変更
        CardView(card, magic);

        return card;
    }

    public GameObject GenerateEmptyCard()
    {
        //未設定のカードを生成
        GameObject card = Instantiate(cardPrefab);

        card.SetActive(false);

        return card;
    }

    public void CardView(GameObject gameObject, MagicEntity magic)
    {
        //見た目を設定
        CardViewer cardViewer = gameObject.GetComponent<CardViewer>();

        if ((int)magic.skill != PlayerStatus.no_skill)
        {
            int skillNum = (int)magic.skill;
            cardViewer.SetImage(cardSprites[magic.level - 1], magic.icon, skillSprites[skillNum]);
            cardViewer.SetText((magic.level * PlayerStatus.attack).ToString() , skillTexts[skillNum]);
        }
        else
        {
            cardViewer.SetImage(cardSprites[magic.level - 1], magic.icon, null);
            cardViewer.SetText((magic.level * PlayerStatus.attack).ToString(), "なし");
        }
    }
}
