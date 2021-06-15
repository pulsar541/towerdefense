using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMonoBehaviour : MonoBehaviour
{ 
    protected int _uid;
    public int UID {
        get{return _uid;}
        set{_uid = value;}
    }

    public bool mustDestroy = false;
    private CharacterController _charController; 
    private SceneController _sceneController; 
    void Start()
    { 
        _charController = GetComponent<CharacterController>();
        _sceneController = GetComponent<SceneController>();
        _uid = SceneController.GetNewUID(); 
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

    public void RemoveFromScene() { 
         //mustDestroy = true;
         this.gameObject.SetActive(false);
        _sceneController.DeleteGameObject(_uid);
        Destroy(this.gameObject); 
    }
  
}
