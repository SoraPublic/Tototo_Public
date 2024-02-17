using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCardView : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private Image icon;
    [SerializeField] private Image skill;
    [SerializeField] private Image guage;

    public void SetCard(Sprite icon, Sprite backGround, Sprite skill, int SkillNum)
    {
        this.icon.sprite = icon;
        this.backGround.sprite = backGround;

        if(SkillNum == PlayerStatus.no_skill)
        {
            this.skill.gameObject.SetActive(false);
        }
        else
        {
            this.skill.gameObject.SetActive(true);
            this.skill.sprite = skill;
        }
    }


    public void CoolTimeDisplay(float count)
    {
        guage.fillAmount = count / PlayerStatus.coolTime;
    }
}
