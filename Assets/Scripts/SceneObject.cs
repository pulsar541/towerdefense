using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{ 
    protected int _uid;
    public int UID {
        get{return _uid;}
        set{_uid = value;}
    }
 
    private CharacterController _charController; 
    private SceneController _sceneController; 
    private Rigidbody _rigidBody; 

    void Awake()
    { 
        _charController = GetComponent<CharacterController>();
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        _rigidBody = GetComponent<Rigidbody>(); 
    }  

    void Start()
    {  
        _uid = SceneController.GetNewUID();   
         _sceneController.TryInsertSceneObject(this.gameObject);
    }
 
    public void SetTransformation(Vector3 position, Quaternion rotation) {

        if( _rigidBody != null )   {
            _rigidBody.isKinematic = true;
            _rigidBody.MovePosition(position);
            _rigidBody.MoveRotation(rotation);  
        }         
        else if( _charController != null )   {
            _charController.enabled = false;
            _charController.transform.position = position;
            _charController.transform.rotation = rotation;
            _charController.enabled = true; 
        } 
        else {
            transform.position = position;
            transform.rotation = rotation;
        }  
    }

    public void GetTransformation(out Vector3 position, out Quaternion rotation) { 
        if( _rigidBody != null )   { 
            position = _rigidBody.transform.position;
            rotation = _rigidBody.transform.rotation;            
        }         
        else if( _charController != null )   {
            position = _charController.transform.position;
            rotation = _charController.transform.rotation;
        }
        else {
            position = transform.position;
            rotation = transform.rotation;
        }  
    }


    public void RemoveFromScene() {  
         this.gameObject.SetActive(false);
        _sceneController.DeleteGameObject(_uid);
        Destroy(this.gameObject); 
    }
 
}
