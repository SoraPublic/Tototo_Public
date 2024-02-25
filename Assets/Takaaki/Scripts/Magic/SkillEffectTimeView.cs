using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEffectTimeView : MonoBehaviour
{
    [SerializeField] private Image guage;

    public void ChangeGuage(float amount)
    {
        if(amount > 1)
        {
            gameObject.SetActive(false);
            return;
        }

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        guage.fillAmount = amount;


    }
}
