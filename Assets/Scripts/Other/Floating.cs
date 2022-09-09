using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {
    public float t = 0;
    public float Amp = 0.25f;
    public float Freq = 2;
    public float Offset = 0;
    public Vector3 StartPos;
    public bool randomRotate;
    void Start (){
        StartPos = transform.position;
    }

    void Update (){
        t = t + Time.deltaTime;
        Offset = Amp * Mathf.Sin (t * Freq);
        transform.position = StartPos + new Vector3 (0, Offset, 0);
        if(randomRotate)
            transform.Rotate (new Vector3 (0, 0.5f, 0));
    }
}