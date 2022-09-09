using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsChecker : MonoBehaviour
{

    void OnTriggerEnter(Collider other){
        if(other.GetComponent<Hint>()){
            other.GetComponent<Hint>().SetActive();
        }
    }

    void OnTriggerExit(Collider other){
        if(other.GetComponent<Hint>()){
            other.GetComponent<Hint>().ResetActive();
        }
    }
}