using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxientyHeal : MonoBehaviour {
    bool active = false;
    public void Activated(){
        if(!active){
            active = true;
            Destroy(gameObject);
        }
    }
}