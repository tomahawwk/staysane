using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpChecker : MonoBehaviour
{
    public ObstacleChecker obstacleChecker;
    public PlayerController controller;

    void OnTriggerEnter(Collider other){
        if(other.tag == "Obstacle"){
            obstacleChecker.UpChecker = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Obstacle"){
            obstacleChecker.UpChecker = false;
            if(!controller.crouch){
                StartCoroutine(Standup());
            }
        }
    }

    IEnumerator Standup() {
		yield return new WaitForSeconds(.6f);
        controller.CrouchToIdle();
	}
}
