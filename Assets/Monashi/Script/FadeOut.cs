using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private SpriteRenderer spRenderer;

    private float alpha = 1f;
    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(Time.deltaTime);

        alpha -= sin;

        var color = spRenderer.color;
        color.a = alpha;
        spRenderer.color = color;
    }
}
