using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyOnTrigger : MonoBehaviour
{
    public GameObject destroyableObject;

    void OnTriggerEnter(Collider other){
        Destroy(destroyableObject);
    }
}
