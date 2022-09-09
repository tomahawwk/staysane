using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelCrushers.DialogueSystem;

public class CustomDialogueManager : MonoBehaviour
{
    public PlayerController controller;
    public CinemachineSwitcher cameraState;
    public DialogueSystemController dialogueSystem;
    public GameObject[] dialogVariants;
    public KeyCode variantButtonOne;
    public KeyCode variantButtonTwo;
    bool canAnswer = false;
    bool firstMenuAnimation = true;

    void Update(){
        if(canAnswer){
            if(Input.GetKeyDown(variantButtonOne)){
                dialogVariants[0].GetComponent<CustomDialogueVariant>().SelectTrigger();
                StartCoroutine(ConversationMenuResetTimeout());
                dialogueSystem.ConversationController.GotoFirstResponse();
            }
            if(Input.GetKeyDown(variantButtonTwo)){
                dialogVariants[1].GetComponent<CustomDialogueVariant>().SelectTrigger();
                StartCoroutine(ConversationMenuResetTimeout());
                dialogueSystem.ConversationController.GotoLastResponse();
            }
        }
    }

    public void DialogueActive(){
        controller.enabled = false;
        cameraState.Dialogue();
    }

    public void DialogueDisactive(){
        controller.enabled = true;
        cameraState.Main();
    }

    public void ConversationStart(){
        dialogueSystem.currentConversant.GetComponent<Conversant>().TalkActive(true);
        dialogueSystem.currentConversant.GetComponent<CustomDialogueNPC>().ActiveCloud();
    }
    public void ConversationEnd(){
        ConversationMenuReset();
        dialogueSystem.currentConversant.GetComponent<CustomDialogueNPC>().DisactiveCloud();
        dialogueSystem.currentConversant.GetComponent<Conversant>().TalkActive(false);
    }

    public void ConversationMenu(){
        if(firstMenuAnimation){
            firstMenuAnimation = false;
            StartCoroutine(ConversationMenuTimeout(0f));
        }
        else{
            StartCoroutine(ConversationMenuTimeout(.7f));
        }
    }

    void ConversationMenuReset(){
         foreach (GameObject variant in dialogVariants){
            variant.GetComponent<CustomDialogueVariant>().Disactive();
        }
    }

    public void Conversation(){
        string subtitle = dialogueSystem.ConversationController.currentState.subtitle.formattedText.text;
        CustomDialogueNPC npc = dialogueSystem.currentConversant.GetComponent<CustomDialogueNPC>();
        if(subtitle != ""){
            if(dialogueSystem.ConversationController.currentState.hasNPCResponse){
                Debug.Log("Игрок говорит: " + subtitle);
            }
            else{
                Conversant conversant = dialogueSystem.currentConversant.GetComponent<Conversant>();
                conversant.Answer();
                npc.SetText(subtitle);
                canAnswer = false;
            }
        }
    }

    IEnumerator ConversationMenuTimeout(float time){
        yield return new WaitForSeconds(time);
        Response[] responses = dialogueSystem.ConversationController.currentState.pcResponses;
        int index = 0;
        foreach (Response response in responses){
            dialogVariants[index].GetComponent<CustomDialogueVariant>().Active(response.formattedText.text);
            index++;
        }
        canAnswer = true;
    }

    IEnumerator ConversationMenuResetTimeout(){
        yield return new WaitForSeconds(.4f);
        ConversationMenuReset();
    }
}