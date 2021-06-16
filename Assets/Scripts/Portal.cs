using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    float _msek = 0;
    float _intervalSpawnSec = 1;
    SceneController _sceneController;
    Random rand = new Random();


    void Awake()
    {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    void Start()
    {
        _intervalSpawnSec = 0.5f;
    }

    void Update()
    {
        if (_sceneController.IsPaused())
            return;

        if (_msek > _intervalSpawnSec)
        {
            _msek = 0;
            Vector3 spawnEnemyPos = transform.position + new Vector3(0, 3, 0);
            _sceneController.SpawnEnemy(spawnEnemyPos);
            _intervalSpawnSec = Random.Range(2.0f, 4.0f);
        }

        _msek += Time.deltaTime;

    }

}
