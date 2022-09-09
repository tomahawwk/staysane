using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    AudioSource audioS;
    public AudioClip changeLocation;
    void Start(){
        audioS = GetComponent<AudioSource>();
    }

    // Смена локации
    public void PlayChangeLocation(){
        audioS.clip = changeLocation;
        audioS.Play();
    }
}
