using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public  Magic[] magics = new Magic[5];
    public  MagicEntity[] magicEntities = new MagicEntity[5];

    public Sprite[] Cards;
    public Sprite[] Skills;

    private void Start()
    {
        SetEntities();
    }

    //Magic‚Ì‰Šú‰»
    public void SetEntities()
    {
        int i = 0;
        foreach (Magic magic in magics)
        {
            magic.SetEntity(magicEntities[i],Cards, Skills);
            i++;
        }
    }

}
