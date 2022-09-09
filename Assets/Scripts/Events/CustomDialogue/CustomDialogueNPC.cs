using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelCrushers.DialogueSystem;
using TMPro;

public class CustomDialogueNPC : MonoBehaviour
{
    public GameObject dialogueCloud;
    public TextMeshPro textMeshPro;
    public Animator cloudAnimator;
    public Animator cloudTextAnimator;
    public void ActiveCloud(){
        cloudAnimator.SetBool("active", true);
    }
    public void DisactiveCloud(){
        StartCoroutine(TextFadeOutTimeout(.15f));
    }

    public void SetText(string text){
        textMeshPro.text = text;
        StartCoroutine(TextFadeInTimeout(.15f));
    }

    IEnumerator TextFadeInTimeout(float time){
        yield return new WaitForSeconds(time);
        cloudTextAnimator.SetBool("active", true);
    }

    IEnumerator TextFadeOutTimeout(float time){
        yield return new WaitForSeconds(time);
        cloudTextAnimator.SetBool("active", false);
        yield return new WaitForSeconds(.3f);
        cloudAnimator.SetBool("active", false);
        yield return new WaitForSeconds(.5f);
        textMeshPro.text = "";
    }
}