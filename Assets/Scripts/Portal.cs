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
    void Start()
    {
        GameObject ob = GameObject.Find("SceneController");
        _sceneController = (SceneController)ob.GetComponent<SceneController>(); 
    }

    // Update is called once per frame
    void Update()
    { 
        if(_sceneController.isRewind)  
        {

        }
        else {
            if(_msek > intervalSpawnSec) {
                _msek = 0; 
                _sceneController.SpawnEnemy(transform.position); 
                intervalSpawnSec = Random.Range(1.0f, 4.0f);
            } 
            _msek += Time.deltaTime;       
        }
    }

}
