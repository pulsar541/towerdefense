using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{  
    SceneController _sceneController;

    private int _nearestEnemyIndex = -1;
    private float _msek = 0; 

    GameObject  _towerCannonGO;
    TowerCannon _towerCannonComp;

    public float cannonProjectileDamage = 50;
    public float shootInterval = 2;

    public float attackRadius = 7;

    public float cannonImpulseForce = 10; 
    public float cannonAngleFromHor = 20; 

    void Awake() {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>(); 
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.gameObject.name.IndexOf("TowerCannon") > -1) {
                _towerCannonGO = child.gameObject;
                _towerCannonComp = _towerCannonGO.GetComponent<TowerCannon>();
                break;
            }
        } 
    }
    void Start()
    {    
        _towerCannonComp.impulseForce = cannonImpulseForce;
        _towerCannonComp.projectileDamage = cannonProjectileDamage;
        _towerCannonComp.shootInterval = shootInterval; 

        _towerCannonGO.transform.localEulerAngles = new Vector3(-90 - cannonAngleFromHor ,0, 0);
    }

    // Update is called once per frame
    void Update()
    {  
         if(_msek > 0.5) { 
             _nearestEnemyIndex = MathLib.nearestGameObjectIndex(_sceneController.sceneObjects, transform.position, "Enemy", attackRadius);
             if(_nearestEnemyIndex > -1) {  
                 if(_sceneController.sceneObjects[_nearestEnemyIndex] != null) { 
                     EnemyAI enemy =  _sceneController.sceneObjects[_nearestEnemyIndex].GetComponent<EnemyAI>();
                     if(enemy != null) {
                         Vector3 targetLook =  enemy.transform.position + enemy.CurrentSpeed * enemy.MoveDir * 1.0f; 
                         targetLook.y = transform.position.y;
                         transform.LookAt(targetLook); 
                         _towerCannonComp.Shooting = true; 
                     }
                 }   
             } else {
                 _towerCannonComp.Shooting = false;
             } 
           
         } 
         _msek += Time.deltaTime;
    }
  

    
}
