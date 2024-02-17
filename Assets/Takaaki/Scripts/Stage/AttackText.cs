using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackText : MonoBehaviour
{
    private float time = 5f;

    private void Update()
    {
        time -= Time.deltaTime;
        
        if(time < 0f)
        {
            gameObject.SetActive(false);
            transform.SetAsLastSibling();
        }
    }

    private void OnEnable()
    {
        time = 5f;
    }
}
