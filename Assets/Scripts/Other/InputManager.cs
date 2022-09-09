using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public string MainController = "keyboard";

    public string climbUpHelp;
    public string climbDownHelp;

    [Header("Клавиатура")]
    public string KeyBoardClimbUp;
    public string KeyBoardClimbDown;
    
    [Header("Геймпад")]
    public string GamepadClimbUp;
    public string GamepadClimbDown;

    void Update(){
        if(MainController == "keyboard"){
            climbUpHelp = KeyBoardClimbUp;
            climbDownHelp = KeyBoardClimbDown;
        }

        if(MainController == "gamepad"){
            climbUpHelp = GamepadClimbUp;
            climbDownHelp = GamepadClimbDown;
        }
    }
}

