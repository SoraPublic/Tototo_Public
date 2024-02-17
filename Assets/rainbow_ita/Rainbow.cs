using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour
{
    
    private ParticleSystem rainbow;
    private ParticleSystem.MainModule rainbowMain;

    private Material mat;
    [SerializeField]
    private float generateTime = 2;
    
    private float currentTime = 0;
    private float gTime = 0;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        rainbow = GetComponent<ParticleSystem>();
        rainbowMain = rainbow.main;
        rainbowMain.startLifetime = this.generateTime;
        rainbowMain.duration = this.generateTime;
    }

    private void Init()
    {
        currentTime = 0;
        gTime = 0;
        mat.SetFloat("_Generate", 0);


    }

    private void OnEnable()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        gTime = currentTime / generateTime;
        mat.SetFloat("_Generate", gTime);
    }
}
