using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClimbCheckerDown : MonoBehaviour {
    public ObstacleChecker obstacleChecker;

    void OnTriggerEnter(Collider other){
        if(other.tag == "ClimbDownTrigger"){
            obstacleChecker.ClimbDownDirection = other.GetComponent<LadderClimbDown>().climbDirection;
            obstacleChecker.ClimbDownTarget = other.GetComponent<LadderClimbDown>().downPoint.transform.position;
            obstacleChecker.CanClimbToLadder = true;
            Debug.Log(obstacleChecker.ClimbDownTarget);
        }  
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "ClimbDownTrigger"){
            obstacleChecker.CanClimbToLadder = false;
            obstacleChecker.ClimbDownDirection = 0;
        }  
    }
}