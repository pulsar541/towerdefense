using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : TDMonoBehaviour
{
    // Start is called before the first frame update

     private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>(); 
    }

    float damage = 50;

    // Update is called once per frame
    void Update()
    { 
        // Debug.Log("s ");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.localScale.y / 2 * 1.5f);
        foreach (Collider hitCollider in hitColliders) {
            if(hitCollider.name.IndexOf("Floor") > -1) { 
                Destroy(gameObject);
            } 
            if(hitCollider.name.IndexOf("Enemy") > -1) {  
                hitCollider.gameObject.GetComponent<EnemyAI>().Health -= damage; 
                //hitCollider.SendMessage("Destroy",
                //        SendMessageOptions.DontRequireReceiver); 
                Destroy(gameObject);         
            } 
           
           //Debug.Log("ee " + hitCollider.name);
        } 
 
        if(transform.position.y < 0) {
            //Destroy(gameObject);
            RemoveFromScene();
        }
    }


    // void OnCollisionEnter(Collision collision)
    // {
       

    //     if(collision.gameObject.name.IndexOf("Enemy") > -1) {
    //         collision.gameObject.GetComponent<EnemyAI>().Health -= damage; 
    //         Destroy(gameObject); 
    //     }

    //     if(collision.gameObject.name.IndexOf("Floor") > -1) { 
    //         Destroy(gameObject); 
    //     }

    // }
}
