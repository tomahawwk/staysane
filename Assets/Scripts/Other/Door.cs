using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip skrip;

    public void Play1(){
        audioS.PlayOneShot(skrip);
    }
}
