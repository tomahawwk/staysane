using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    public bool UpChecker = false;
    public string ForwardChecker;
    public bool OnClimbUp = false;
    public bool CanClimbDown = false;
    public bool CanClimbToLadder = false;
    public int ClimbDownDirection;
    public Vector3 climbRoofTarget;
    public Vector3 ClimbDownTarget;
    public bool movableEntered;
}
