using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    bool mouseOver = false;
    public float brightnessFactor;
    //private Interactive interactive;
    Color color;

    void Start (){
        //interactive = GameObject.Find("Interactive").GetComponent<Interactive>();
        color = GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }
    private void OnMouseEnter()
    {
        // if(interactive.Interactable == true){
        //     mouseOver = true;
        //     var hoverColor = color * brightnessFactor;
        //     GetComponent<Renderer>().material.SetColor("_EmissionColor", hoverColor);
        // }
        
    }


    private void OnMouseExit()

    {
        mouseOver = false;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}
