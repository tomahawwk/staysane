using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThoughtsManager : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public Animator playerAnimator;
    public Animator thoughtAnimator;
    bool busy = false;
    public void ThoughtActive(string text, float time){
        if(!busy){
            busy = true;
            textMeshPro.text = text;
            playerAnimator.SetTrigger("Thinking");
            StartCoroutine(ThoughtActiveTimeout(time));
        }
    }

    public void ThoughtDisactive(){
        thoughtAnimator.SetBool("active", false);
        busy = false;
    }

    IEnumerator ThoughtActiveTimeout(float time){
        yield return new WaitForSeconds(.1f);
        thoughtAnimator.SetBool("active", true);
        yield return new WaitForSeconds(time);
        ThoughtDisactive();
    }
}