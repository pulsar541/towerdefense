﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindController : MonoBehaviour
{
  
     [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy; 

     [SerializeField] private GameObject projectilePrefab;

    SceneController _sceneController;
    private int _gameTime = 0;
  //  List<LogUnit> log = new List<LogUnit>(); 
    public int GameTime {
        get{ return _gameTime; }
    }

    Dictionary<int, TimeUnit> sceneLog = new Dictionary<int, TimeUnit>(); 

    // Start is called before the first frame update
    void Awake()
    {
        _sceneController = GetComponent<SceneController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if(_sceneController.isRewind) {
            _gameTime --;
            RestoreScene(_gameTime);
        }   
        else {
            RecordScene();
            _gameTime ++;
        }     
 
    }

    void RecordScene() {
        if(!sceneLog.ContainsKey(_gameTime)) {
            TimeUnit timeUnit = new TimeUnit();
            foreach(GameObject go in _sceneController.sceneObjects) {
                if(go != null && go.activeSelf) {
                    LogUnit logUnit = new LogUnit(); 
                    logUnit.uid = go.GetComponent<TDMonoBehaviour>().UID; 
                    go.GetComponent<TDMonoBehaviour>().GetTransformation(out logUnit.position, out logUnit.rotation);
                    logUnit.name = go.name; 
                    timeUnit.logUnits.Add(logUnit);
                }
            }
            sceneLog.Add(_gameTime, timeUnit);
        }    
    }
 
    void RestoreScene(int _gameTime) {  
         
        if(sceneLog.ContainsKey(_gameTime)) { 
            TimeUnit timeUnit = sceneLog[_gameTime];   
            int sceneObjSize = _sceneController.sceneObjects.Count;
 
            for(int k = 0 ; k< sceneObjSize ; k++) {          
                for(int i = 0 ; i< sceneObjSize ; i++) {
                    GameObject go = _sceneController.sceneObjects[i];
                    int uid = go.GetComponent<TDMonoBehaviour>().UID;
                     if(timeUnit.FindLogUnit(uid) == null) { 
                        Destroy(go.gameObject); 
                        _sceneController.DeleteGameObject(uid); 
                        sceneObjSize = _sceneController.sceneObjects.Count;
                        break; 
                     }
                } 
            } 

            foreach(LogUnit logUnit in timeUnit.logUnits) {
                 GameObject go = _sceneController.FindGameObject(logUnit.uid);
                 if(go != null) { 
                     TDMonoBehaviour trb =  go.GetComponent<TDMonoBehaviour>();
                     if(trb != null) { 
                        trb.SetTransformation(logUnit.position, logUnit.rotation);     
                     } 

                 } else {  
                        if(logUnit.name.IndexOf("Enemy") > -1) { 
                            GameObject gameObject = Instantiate(enemyPrefab) as GameObject;   
                            gameObject.GetComponent<TDMonoBehaviour>().SetTransformation(logUnit.position, logUnit.rotation);
                            gameObject.GetComponent<TDMonoBehaviour>().UID = logUnit.uid;  
                            _sceneController.sceneObjects.Add(gameObject);
                        } else if(logUnit.name.IndexOf("Projectile") > -1) {
                             GameObject gameObject = Instantiate(projectilePrefab) as GameObject;   
                            gameObject.GetComponent<TDMonoBehaviour>().SetTransformation(logUnit.position, logUnit.rotation);
                            gameObject.GetComponent<TDMonoBehaviour>().UID = logUnit.uid;  
                            _sceneController.sceneObjects.Add(gameObject);                           
                        } else if(logUnit.name.IndexOf("Castle") > -1) {                           
                        } 
                 }
            }

        }
    }
 
}
