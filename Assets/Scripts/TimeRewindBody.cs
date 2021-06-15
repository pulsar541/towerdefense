using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindBody : MonoBehaviour
{ 
    CharacterController _charController; 
    void Start()
    { 
        _charController = GetComponent<CharacterController>();
    }
 
 
    public void SetTransformation(Vector3 position, Quaternion rotation) {
        if( _charController != null )   {
            _charController.enabled = false;
            _charController.transform.position = position;
            _charController.transform.rotation = rotation;
            _charController.enabled = true; 
        } else {
            transform.position = position;
            transform.rotation = rotation;
        } 
    }
}
