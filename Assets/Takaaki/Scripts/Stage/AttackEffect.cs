using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource audioSource;

    // 1. 再生
    public void Play()
    {
        audioSource.Play();
        particle.Play();
    }
}
