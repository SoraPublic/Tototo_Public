using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] private MagicActivate magicActivate;
    [SerializeField] private MagicCardView magicCardView;

    public int[] array;
    public float attackDamage;
    public Skill skill;
    public Sprite icon;

    //Entityの情報を移す
    public void SetEntity(MagicEntity entity, Sprite[] cards, Sprite[] skills)
    {
        array = entity.array;
        attackDamage = entity.level * PlayerStatus.attack;
        skill = entity.skill;
        
        icon = entity.icon;

        magicActivate.SetWave();
        magicCardView.SetCard(icon, cards[entity.level-1], skills[(int)entity.skill], (int)entity.skill);
    }

    public void Active()
    {
        magicActivate.Active(attackDamage,skill);
    }


}
