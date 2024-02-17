using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGaugeController : MonoBehaviour
{
    [SerializeField]
    private Color color_1, color_2, color_3, color_4;
    private float maxHP;
    private Image image_HPgauge;
    private float hp_ratio;

    public void SetUp(int hp)
    {
        image_HPgauge = gameObject.GetComponent<Image>();
        maxHP = hp;
        HPGaugeUpdate(hp);
    }

    public void HPGaugeUpdate(int hp)
    {
        hp_ratio = hp / maxHP;

        if (hp_ratio > 0.75f)
        {
            image_HPgauge.color = Color.Lerp(color_2, color_1, (hp_ratio - 0.75f) * 4f);
        }
        else if (hp_ratio > 0.25f)
        {
            image_HPgauge.color = Color.Lerp(color_3, color_2, (hp_ratio - 0.25f) * 4f);
        }
        else
        {
            image_HPgauge.color = Color.Lerp(color_4, color_3, hp_ratio * 4f);
        }

        image_HPgauge.fillAmount = hp_ratio;
    }
}
