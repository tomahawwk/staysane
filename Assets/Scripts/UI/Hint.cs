using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {
    public Animator animator;
    public bool active = false;
    AudioSource audioSource;
    public AudioClip fadeIn;
    public AudioClip fadeOut;
    ActionEvent action;
    void Start(){
        audioSource = GetComponent<AudioSource>();
        action = GetComponent<ActionEvent>();
    }
    void Update(){
        ActionHintHandler();
    }

    public void SetActive(){
        if(!active){
            active = true;
            animator.SetBool("active", true);
            action.canAction = true;
        }
    }

    public void ResetActive(){
        active = false;
        animator.SetBool("active", false);
        Debug.Log(123);
        StartCoroutine(ActionActivityTimeout());
    }

    public void ActionHintHandler(){
        if(Input.GetButtonDown("Action")){
            if(active){
               ResetActive();
            }
        }
    }
    IEnumerator ActionActivityTimeout(){
        yield return new WaitForSeconds(.3f);
        action.canAction = false;
    }
}