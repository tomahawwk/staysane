using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CustomDialogueVariant : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    Animator variantAnimator;
    public float delay = 0f;
    
    void Start(){
        variantAnimator = GetComponent<Animator>();
    }

    public void Active(string text){
        textMeshPro.text = text;
        StartCoroutine(FadeInTimeout());
    }

    public void Disactive(){
        variantAnimator.SetBool("active", false);
    }

    public void SelectTrigger(){
        variantAnimator.SetTrigger("select");
    }

    IEnumerator FadeInTimeout(){
        yield return new WaitForSeconds(delay);
        variantAnimator.SetBool("active", true);
    }
}