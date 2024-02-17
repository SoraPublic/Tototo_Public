using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    [SerializeField] private MagicStorage magicStorage;
    [SerializeField] private MagicCardGenerator magicCardGenerator;

    [SerializeField] private GameObject contentParent;
    [SerializeField] private GameObject listParent;

    public struct SelectedCard
    {
        public MagicEntity MagicEntity;
        public GameObject Card;
    }

    //private List<GameObject> selectedList = new List<GameObject>();
    //private List<MagicEntity> magicEntities = new List<MagicEntity>();

    public List<SelectedCard> selectedCards = new List<SelectedCard>();
    private List<Card> cardList = new List<Card>();

    private List<Card> selectContentList = new List<Card>();


    public int selectedNum;

    private void Start()
    {
        selectedNum = 0;

        DataManger dataManger = new DataManger();
        GameData gameData = dataManger.LoadGameData();

        //ストレージにあるカードを生成

        foreach (int j in gameData.cardLists)
        {
            MagicEntity magic = magicStorage.MagicList[j];

            GameObject card = magicCardGenerator.GenerateMagicCard(magic);
            card.transform.SetParent(contentParent.transform);
            card.transform.localScale = new Vector3(1f, 1f, 1f);

            Card card1 = card.GetComponent<Card>();
            card1.magicEntity = magic;
            cardList.Add(card1);
        }

        /*
        int i = 0;
        foreach(MagicEntity magic in magicStorage.MagicList)
        {
            if (gameData.cardLists.Contains(i)) 
            {
                GameObject card = magicCardGenerator.GenerateMagicCard(magic);
                card.transform.SetParent(contentParent.transform);
                card.transform.localScale = new Vector3(1f, 1f, 1f);

                Card card1 = card.GetComponent<Card>();
                card1.magicEntity = magic;
                cardList.Add(card1);
            }
            i++;
        }
        */

        //リストに空のカードを生成
        for (int i = 0; i < 5; i++)
        {
            GameObject card = magicCardGenerator.GenerateEmptyCard();
            card.transform.SetParent(listParent.transform);
            card.transform.localScale = new Vector3(1f, 1f, 1f);

            SelectedCard selected;
            selected.MagicEntity = null;
            selected.Card = card;

            selectedCards.Add(selected);



        }

    }


    public void SelectCard(MagicEntity magicEntity)
    {
        //魔法のリストに入れる
        SelectedCard selected;
        selected.MagicEntity = magicEntity;
        selected.Card = selectedCards[selectedNum].Card;
        selectedCards[selectedNum] = selected;

        //カードの見た目を表示する
        selectedCards[selectedNum].Card.SetActive(true);
        magicCardGenerator.CardView(selectedCards[selectedNum].Card,magicEntity);

        //Cardを設定する
        Card card = selectedCards[selectedNum].Card.GetComponent<Card>();
        card.state = CardState.SELECTED_LIST;
        card.magicEntity = magicEntity;

        Card content = SerchCard(magicEntity);
        selectContentList.Add(content);
        content.SetNumText(selectedNum + 1);

        selectedNum++;

        CardSelectManager.instance.SetCardNum(selectedNum);
    }

    public void UnselectCard(MagicEntity magicEntity)
    {
        for(int i=0; i < selectedNum; i++)
        {
            if (selectedCards[i].MagicEntity == magicEntity)
            {
                selectedCards[i].Card.SetActive(false);
                selectedCards[i].Card.transform.SetAsLastSibling();

                GameObject card = selectedCards[i].Card;

                selectedCards.RemoveAt(i);

                SelectedCard selected;
                selected.MagicEntity = null;
                selected.Card = card;

                selectedCards.Add(selected);

                Card content = SerchCard(magicEntity);
                selectContentList.Remove(content);

                CurverNum();

                selectedNum--;

                CardSelectManager.instance.SetCardNum(selectedNum);
                //return;
            }
        }
    }

    public Card SerchCard(MagicEntity magicEntity)
    {
        foreach (Card card in cardList)
        {
            if(card.magicEntity == magicEntity)
            {
                return card;
            }
        }

        return null;
    }

    private void CurverNum()
    {
        int i = 0;
        foreach (Card card in selectContentList)
        {
            i++;
            card.SetNumText(i);
        }
    }


}
