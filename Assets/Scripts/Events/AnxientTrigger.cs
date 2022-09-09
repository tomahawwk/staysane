using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxientTrigger : MonoBehaviour
{
    bool used = false;
    public PlayerState playerState;

    public void UseTrigger(){
        if(!used){
            used = true;
            playerState.AnxientyEnable();
        }
    }
}
