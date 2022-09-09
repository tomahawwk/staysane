using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {
    public AudioClip[] draggingSounds;
    public bool canMoving = true;
    Rigidbody rigidbody;
    AudioSource audioSource;
    public bool moving = false;

    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Obstacle" || collision.collider.tag == "ClimbObstacle"){
            if(transform.parent != null && transform.parent.tag == "Player"){
                canMoving = false;
                transform.parent.GetComponent<PlayerController>().MovableObjectOut();
            }
        }
    }

    public void StartMoving(){
        moving = true;
        audioSource.clip = draggingSounds[Random.Range(0, draggingSounds.Length - 1)];
        audioSource.Play();
    }

    public void EndMoving(){
        moving = false;
        audioSource.Stop();
    }
}