using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardChecker : MonoBehaviour
{
    public ObstacleChecker obstacleChecker;
    public PlayerController controller;

    void OnTriggerEnter(Collider other){
        if(other.tag == "Obstacle"){
            obstacleChecker.ForwardChecker = "Obstacle";
            controller.mustStop = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Obstacle"){
            controller.mustStop = false;
        }
    }

}
