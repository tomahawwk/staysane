using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDetectable
{
    private GameObject _gameObject;
    private Transform _transform;
    
    private void Start()
    {
        if(_transform == null) 
            _transform = gameObject.GetComponent<Transform>();
        
        if (_gameObject == null)
            _gameObject = gameObject;
    }
    
    public void Detect()
    {
        Debug.Log("Detected");
    }

    public void UnDetect()
    {
        
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }

    public GameObject GetGameObject()
    {
        return _gameObject;
    }
}