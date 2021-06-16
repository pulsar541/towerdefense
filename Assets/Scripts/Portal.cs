using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
  //  [SerializeField] private GameObject enemyPrefab;
 //   private GameObject _enemy; 
    float _msek = 0; 
    float intervalSpawnSec = 1;
    SceneController _sceneController; 
    Random rand = new Random();
     
     
    void Awake() {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();  
    } 

    void Start() {
        intervalSpawnSec = Random.Range(2.0f, 4.0f);
    }
    // Update is called once per frame
    void Update()
    { 
        if(_sceneController.isPause)  
            return; 
    
        if(_msek > intervalSpawnSec ) {
            _msek = 0; 
            Vector3 spawnEnemyPos =  transform.position + new Vector3(0, 3 ,0);
            _sceneController.SpawnEnemy(spawnEnemyPos); 
            intervalSpawnSec = Random.Range(2.0f, 4.0f);
        } 
        
        _msek += Time.deltaTime;       
        
    }

}
