using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour {
    
    AudioSource impactSound;

    void Start(){
        impactSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision){
        if(collision.relativeVelocity.magnitude > .5){
            impactSound.Play();
            Debug.Log(1);
        }
    }
}