using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversant : MonoBehaviour
{
    public Animator animator;
    bool hello = false;
    public void TalkActive(bool state){
        hello = false;
        animator.SetBool("talk", state);
    }

    public void Answer(){
        if(!hello){
            hello = true;
            animator.SetTrigger("hello");
        }
        else{
            Debug.Log(Random.Range(1, 2));
            animator.SetTrigger("answer" + Random.Range(1, 3));
        }
    }
}