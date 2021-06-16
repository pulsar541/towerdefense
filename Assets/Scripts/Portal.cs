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
    // Start is called before the first frame update

    
    void Awake() {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();  
    }
    void Start()
    {
     //   _sceneController.sceneObjects.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    { 
        if(_sceneController.isPause)  
        {

        }
        else {
            if(_msek > intervalSpawnSec ) {
                _msek = 0; 
                _sceneController.SpawnEnemy(transform.position); 
                intervalSpawnSec = Random.Range(1.0f, 4.0f);
            } 

           // if(_msek != -1)
                _msek += Time.deltaTime;       
        }
    }

}
