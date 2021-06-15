using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody _rigidBody; 
    private SceneController _sceneController;
    private CharacterController _characterController;
    private Vector3 _movement;


    private SceneObject _sceneObject;

    void Awake() {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>(); 
        _rigidBody = GetComponent<Rigidbody>();  
        _sceneObject = GetComponent<SceneObject>();
    }

    void Start()
    {    
    }

    float damage = 50;

    void Init(Vector3 movoment) {

    }

    // Update is called once per frame
    void Update()
    { 
        if(_sceneController.isRewind) {
            _rigidBody.isKinematic = true;
        }
        else _rigidBody.isKinematic = false;
        // Debug.Log("s ");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.localScale.y / 2 * 1.5f);
        foreach (Collider hitCollider in hitColliders) {
            if(hitCollider.name.IndexOf("Floor") > -1) {  
                 _sceneObject.RemoveFromScene();
            } 
            if(hitCollider.name.IndexOf("Enemy") > -1) {  
                hitCollider.gameObject.GetComponent<EnemyAI>().Health -= damage;  
                _sceneObject.RemoveFromScene();      
            } 
            
        } 
 
        if(transform.position.y < 0) { 
             _sceneObject.RemoveFromScene();
        }
    }
  
}
