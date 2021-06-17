using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindController : MonoBehaviour
{
    SceneController _sceneController;
    private int _gameTime = 0; 
    public int GameTime
    {
        get { return _gameTime; }
    }

    Dictionary<int, TimeUnit> sceneLog = new Dictionary<int, TimeUnit>();

    void Awake()
    {
        _sceneController = GetComponent<SceneController>();
    }

    void Start() {
        RecordScene(0);
    }

    void FixedUpdate()
    {
        if (_sceneController.IsPaused())
            return;

        RecordScene(_gameTime);
        _gameTime++;

    }

    void RecordScene(int gameTime)
    {
        if (!sceneLog.ContainsKey(gameTime))
        {
            TimeUnit timeUnit = new TimeUnit();
            foreach (GameObject go in _sceneController.sceneObjects)
            {
                if (go != null && go.activeSelf)
                {
                    LogUnit logUnit = new LogUnit();
                    logUnit.uid = go.GetComponent<SceneObject>().UID;
                    go.GetComponent<SceneObject>().GetTransformation(out logUnit.position, out logUnit.rotation);
                    logUnit.name = go.name;
                    logUnit.health = go.GetComponent<SceneObject>().Health;
                    logUnit.movement = go.GetComponent<SceneObject>().Movement;
                    timeUnit.logUnits.Add(logUnit);
                }
            }
            sceneLog.Add(gameTime, timeUnit);
        }
    }


    public void ClearHistory(int startGameTime, int endGameTime)
    {
        for (int gt = endGameTime; gt >= startGameTime; gt--)
        {
            sceneLog.Remove(gt);
        }

        _gameTime = startGameTime;
    }

    public void RestoreScene(int gameTime)
    {
        //_gameTime = gameTime;
        if (sceneLog.ContainsKey(gameTime))
        {
            TimeUnit timeUnit = sceneLog[gameTime];
            int sceneObjSize = _sceneController.sceneObjects.Count;

            for (int k = 0; k < sceneObjSize; k++)
            {
                for (int i = 0; i < sceneObjSize; i++)
                {
                    GameObject go = _sceneController.sceneObjects[i];
                    int uid = go.GetComponent<SceneObject>().UID;
                    if (timeUnit.FindLogUnit(uid) == null)
                    {
                        Destroy(go.gameObject);
                        _sceneController.DeleteGameObject(uid);
                        sceneObjSize = _sceneController.sceneObjects.Count;
                        break;
                    }
                }
            }

            foreach (LogUnit logUnit in timeUnit.logUnits)
            {
                GameObject go = _sceneController.FindGameObject(logUnit.uid);
                if (go != null)
                {
                    SceneObject trb = go.GetComponent<SceneObject>();
                    if (trb != null)
                    {
                        trb.SetTransformation(logUnit.position, logUnit.rotation);
                        trb.Health = logUnit.health;
                        trb.Movement = logUnit.movement;
                    }

                }
                else
                {   GameObject gameObject = null;
                    if (logUnit.name.IndexOf("Enemy") > -1)
                    {
                        gameObject = Instantiate(_sceneController.enemyPrefab) as GameObject; 
                    }
                    else if (logUnit.name.IndexOf("Projectile") > -1)
                    {
                        gameObject = Instantiate(_sceneController.projectilePrefab) as GameObject; 
                    }
                    else if (logUnit.name.IndexOf("Castle") > -1)
                    {
                        gameObject = Instantiate(_sceneController.castlePrefab) as GameObject;                        
                    }
                    if(gameObject != null) 
                    {
                        gameObject.GetComponent<SceneObject>().SetTransformation(logUnit.position, logUnit.rotation);
                        gameObject.GetComponent<SceneObject>().UID = logUnit.uid;
                        gameObject.GetComponent<SceneObject>().Health = logUnit.health;
                        gameObject.GetComponent<SceneObject>().Movement = logUnit.movement;
                        _sceneController.sceneObjects.Add(gameObject);    
                    }
                }
            }

        }
    }

}
