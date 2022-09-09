using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClimbCheckerUp : MonoBehaviour
{
    public ObstacleChecker obstacleChecker;

    void OnTriggerEnter(Collider other){
        if(other.tag == "ClimbObstacle"){
            obstacleChecker.OnClimbUp = true;
        }

        if(other.tag == "ClimbUpTrigger"){
            obstacleChecker.climbRoofTarget = other.transform.position;
        }  

        if(other.tag == "ClimbDownTrigger"){
            obstacleChecker.CanClimbDown = true;
        }   
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "ClimbObstacle"){
            obstacleChecker.OnClimbUp = false;
        }

        if(other.tag == "ClimbDownTrigger"){
            obstacleChecker.CanClimbDown = false;
        }  
    }
}
