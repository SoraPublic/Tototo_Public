using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCameraManager : MonoBehaviour
{
    [SerializeField] private GameObject camera1;

    public void SetCamera(bool isCamera1)
    {
        camera1.SetActive(isCamera1);
    }
}
