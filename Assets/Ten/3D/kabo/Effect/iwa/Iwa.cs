using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iwa : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem iwa;
    private Material mat;



    private ParticleSystem iwaParticle;
    private ParticleSystem.MainModule iwaMain;

    [SerializeField]
    private float generateTime = 3f;
    private float gTime = 0;
    [SerializeField]
    private float keepTime = 3f;
    [SerializeField]
    private float dissolveTime = 3f;
    private float dTime = 0;
    private float lifeTime; //generateTime + dissolveTime

    private float currentTime;
    private bool isGenerated = false;

    // Start is called before the first frame update

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        iwaParticle = this.GetComponent<ParticleSystem>();
        iwaMain = iwaParticle.main;

        lifeTime = generateTime + keepTime + dissolveTime;
        iwaMain.startLifetime = this.lifeTime;
        
    }

    private void init()
    {
        isGenerated = false;
        mat.SetInt("_generated", 0);
        currentTime = 0;
        gTime = 0f;
        dTime = 0f;
        mat.SetFloat("_ClipTime", gTime);
        mat.SetFloat("_DissolveTime", dTime);
    }
    private void OnEnable()
    {
        init();
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        currentTime += Time.deltaTime;
        if (this.generateTime + this.keepTime < currentTime)
        {
            mat.SetInt("_generated", 1);
            isGenerated = true;
        }

        if (!isGenerated)
        {
            gTime += Time.deltaTime / generateTime;
            mat.SetFloat("_ClipTime", gTime);
        }
        else
        {
            dTime += Time.deltaTime / dissolveTime;
            mat.SetFloat("_DissolveTime", dTime);
        }


        


        

    }
}
