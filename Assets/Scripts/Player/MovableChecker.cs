using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableChecker : MonoBehaviour
{
    public ObstacleChecker obstacleChecker;
    public PlayerController controller;

    void OnTriggerEnter(Collider other){
        if(other.tag == "MovableObstacle"){
            obstacleChecker.movableEntered = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "MovableObstacle"){
            obstacleChecker.movableEntered = false;
            controller.ResetSpeed();
        }
    }
}
