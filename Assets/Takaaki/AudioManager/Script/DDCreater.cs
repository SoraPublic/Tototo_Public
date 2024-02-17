using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDCreater : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private static bool flag = true;

    private void Start()
    {
        if (flag)
        {
            Instantiate(prefab);
            flag = false;
        }

    }

}
