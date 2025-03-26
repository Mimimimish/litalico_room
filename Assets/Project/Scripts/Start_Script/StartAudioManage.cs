using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioManage : MonoBehaviour
{
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.time = 3.5f;
        audioSource.Play();
    }
}
