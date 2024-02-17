using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardViewer : MonoBehaviour
{
    [SerializeField] private Image card;
    [SerializeField] private Image block;
    [SerializeField] private Image skill;

    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI skillText;

    public void SetImage(Sprite cardSprite, Sprite blockSprite, Sprite skillSprite)
    {
        card.sprite = cardSprite;
        block.sprite = blockSprite;

        if (skillSprite != null)
        {
            skill.sprite = skillSprite;
        }
        else
        {
            skill.gameObject.SetActive(false);
        }
    }

    public void SetText(string attackString, string skillString)
    {
        attackText.text = attackString;
        skillText.text = skillString;
    }


}
