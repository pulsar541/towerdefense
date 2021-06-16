using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    //private Rigidbody _rigidBody; 
    private SceneController _sceneController;
    private CharacterController _characterController;
    private Vector3 _movement;

    private Vector3 _gravity = new Vector3(0,-9.8f,0);


    private SceneObject _sceneObject;

    void Awake() {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();  
        _sceneObject = GetComponent<SceneObject>();
    }

    void Start()
    {    
    }

    float damage = 50;

    public void Init(Vector3 position, Vector3 movoment) {
        transform.position = position;
        _movement = movoment;
    }

    // Update is called once per frame
    void Update()
    {     
        if(_sceneController.isPause)
            return;

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
        _movement += _gravity * Time.deltaTime;

        transform.position += _movement * Time.deltaTime;
    }
  
}
