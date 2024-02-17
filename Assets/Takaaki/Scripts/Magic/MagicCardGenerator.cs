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
        //�ݒ�ς݂̃J�[�h�𐶐�
        GameObject card = GenerateEmptyCard();

        card.SetActive(true);

        //�J�[�h�̌����ڂ�ύX
        CardView(card, magic);

        return card;
    }

    public GameObject GenerateEmptyCard()
    {
        //���ݒ�̃J�[�h�𐶐�
        GameObject card = Instantiate(cardPrefab);

        card.SetActive(false);

        return card;
    }

    public void CardView(GameObject gameObject, MagicEntity magic)
    {
        //�����ڂ�ݒ�
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
            cardViewer.SetText((magic.level * PlayerStatus.attack).ToString(), "�Ȃ�");
        }
    }
}
