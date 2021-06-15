using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{ 

    SceneController _sceneController;

    private int _nearestEnemyIndex = -1;
    private float _msek = 0; 
    GameObject towerCannon;

    void Awake() {
         _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>(); 
    }
    void Start()
    { 
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.gameObject.name.IndexOf("TowerCannon") > -1) {
                towerCannon = child.gameObject;
                break;
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {  
         if(_msek > 0.5) { 
             _nearestEnemyIndex = MathLib.nearestGameObjectIndex(_sceneController.sceneObjects, transform.position, "Enemy");
             if(_nearestEnemyIndex > -1) {  
                 if(_sceneController.sceneObjects[_nearestEnemyIndex] != null) { 
                     EnemyAI enemy =  _sceneController.sceneObjects[_nearestEnemyIndex].GetComponent<EnemyAI>();
                     if(enemy != null) {
                         Vector3 targetLook =  enemy.transform.position + enemy.CurrentSpeed * enemy.MoveDir * 1.0f; 
                         targetLook.y = transform.position.y;
                         transform.LookAt(targetLook); 
                         towerCannon.GetComponent<TowerCannon>().Shooting = true; 
                     }
                 }   
             } else {
                 towerCannon.GetComponent<TowerCannon>().Shooting = false;
             } 
           
         } 
         _msek += Time.deltaTime;
    }
  

    
}
