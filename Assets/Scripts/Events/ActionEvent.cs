using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ActionEvent : MonoBehaviour {
    //public PlayerController playerController;
    public bool canAction = false;
    [Header("Вызвать мысль")]
    public bool thought = false;
    public string thoughtText;
    public float thoughtTime;
    public ThoughtsManager thoughtsManager;

    [Header("Вызвать диалог")]
    public bool dialogue = false;
    public DialogueSystemTrigger dialogueSystemTrigger;
    public CustomDialogueManager dialogueManager;

    void Update(){
        Event();
    }

    public void Event(){
        if(Input.GetButtonDown("Action") && canAction){
            if(thought){
               thoughtsManager.ThoughtActive(thoughtText, thoughtTime);
            }

            if(dialogue){
                StartCoroutine(DialogTimeout());
                dialogueManager.DialogueActive();
            }
        }
    }

    IEnumerator DialogTimeout(){
        yield return new WaitForSeconds(.7f);
        dialogueSystemTrigger.OnUse();
    }
}