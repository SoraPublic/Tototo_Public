using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileView : MonoBehaviour
{
    //ï®óùïîï™
    //ïœçXçœÇ›
    [SerializeField]
    private Image[] magicImages = new Image[12];
    [SerializeField]
    private Image targetImage;
    public GameObject particle;
    private GameObject directon;


    private void Start()
    {
        if(particle != null)
        {
            particle.SetActive(false);

        }
    }


    private Color colored = new Color(1f, 1f, 1f, 1f);
    private Color colorless = new Color(0f, 0f, 0f, 0f);

    public void SetMagicUp(float middle)
    {
        int step = (int)((middle - 0.3f) * 10 * 3);

        if (middle < 0.3 || 0.7 < middle)
        {
            return;
        }

        SetMagicImage(step);
    }

    private void SetMagicImage(int num)
    {
        for (int i = 0; i <= num; i++)
        {
            magicImages[i].gameObject.SetActive(true);
            magicImages[i].color = colored;
        }
    }

    public void SetMagicDown(float middle)
    {
        if (0.7 < middle)
        {
            return;
        }

        int step = (int)((middle - 0.3f) * 10 * 3);

        float alpha;

        if (middle < 0.3f)
        {
            alpha = 0f;
        }
        else if (middle > 0.7)
        {
            alpha = 1f;
        }
        else
        {
            alpha = (middle - 0.3f) / 0.4f;
        }

        SetImageAlpha(alpha);
    }

    private void SetImageAlpha(float alpha)
    {
        foreach (var image in magicImages)
        {
            image.color = new Color(1f, 1f, 1f, alpha);
        }

        if (alpha == 0)
        {
            SetFast();
        }
    }

    public void SetFast()
    {
        foreach (Image image in magicImages)
        {
            image.gameObject.SetActive(false);
            image.color = colorless;
        }
    }

    public void SetTarget(float rate)
    {
        targetImage.fillAmount = rate;
    }

    public void SetTargetImage(Color color)
    {
        targetImage.color = color;
    }

    public void SetDirection(GameObject obj)
    {
        var parent = this.transform;
        directon = Instantiate(obj, parent, false);
    }

    public void DeleteDirection()
    {
        Destroy(directon, 3f);
    }
}
