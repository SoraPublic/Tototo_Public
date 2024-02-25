using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectTimeManager : MonoBehaviour
{
    [SerializeField] private SkillEffectTimeView[] skillEffectTimeViews;

    public void SkillEffectTime(float[] buffs)
    {
        for(int i = 0; i < buffs.Length; i++)
        {
            skillEffectTimeViews[i].ChangeGuage(1 - (buffs[i]/PlayerStatus.buffDuration));
        }
    }

}
