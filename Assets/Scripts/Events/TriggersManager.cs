using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] anxientSounds;
    public AudioClip anxientHeal;
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void AnxientTrigger() {
        audioSource.clip = anxientSounds[Random.Range(0, anxientSounds.Length)];
        audioSource.Play();
    }

    public void AnxientHealTrigger() {
        audioSource.clip = anxientHeal;
        audioSource.Play();
    }
}
