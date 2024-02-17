using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaminari : MonoBehaviour
{
    Material mat;
    Material Tmat;
    public float duration = 1.0f;
    public float intensity = 2.5f;

    private float n = 0f;
    private float x_tmp = 0.0f;
    private float sumTime = 0;

    public float duration2 = 1.0f;
    public float length = 1.0f;

    ParticleSystem ps;
    ParticleSystemRenderer psr;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();
        mat = psr.material;
        Tmat = psr.trailMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        float phi = Time.time / duration * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
        mat.EnableKeyword("_EMISSION");
        float factor = Mathf.Pow(2, intensity);
        sumTime += Time.deltaTime;
        n = x_tmp + Mathf.PingPong(sumTime * length / duration2 * 2, length);

        mat.SetColor("_EmissionColor", psr.material.color * factor * n);
        ps.startColor = psr.material.color * factor * n;

        Tmat.SetColor("_EmissionColor", psr.material.color * factor * n);
    }
}
