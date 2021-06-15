using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : TDMonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody _rigidBody; 
    private SceneController _sceneController;
    private CharacterController _characterController;
    private Vector3 _movement;


    void Awake() {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>(); 
        _rigidBody = GetComponent<Rigidbody>();  
    }

    void Start()
    {  
        _uid = SceneController.GetNewUID();    
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
                //Destroy(gameObject);
              //  RemoveProjectile();
            } 
            if(hitCollider.name.IndexOf("Enemy") > -1) {  
                hitCollider.gameObject.GetComponent<EnemyAI>().Health -= damage; 
                //hitCollider.SendMessage("Destroy",
                //        SendMessageOptions.DontRequireReceiver); 
                //Destroy(gameObject);  
               // RemoveProjectile();      
            } 
           
           //Debug.Log("ee " + hitCollider.name);
        } 
 
        if(transform.position.y < 0) {
            //Destroy(gameObject);
           // RemoveProjectile();
        }
    }
 
    public void RemoveProjectile() { 
         //mustDestroy = true;
         this.gameObject.SetActive(false);
        _sceneController.DeleteGameObject(_uid);
        Destroy(this.gameObject); 
    }
}
