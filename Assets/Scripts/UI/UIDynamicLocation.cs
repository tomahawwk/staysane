using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDynamicLocation : MonoBehaviour{
    RectTransform rect;
    Camera camera;
    public GameObject targetObject;
    public float xOffset = 0f;
    public float yOffset = 0f;
    void Start(){
        rect = GetComponent<RectTransform>();
        camera = Camera.main;
    }

    void Update(){
        rect.SetPositionAndRotation(camera.WorldToScreenPoint(new Vector2(targetObject.transform.position.x + xOffset, targetObject.transform.position.y + yOffset)), Quaternion.identity);
    }
}