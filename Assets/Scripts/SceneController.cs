using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;

    [SerializeField] private GameObject projectilePrefab;
    private GameObject _projectile;

    public List<GameObject> sceneObjects = new List<GameObject>();

    //Dictionary<int, GameObject> gameObjects = new Dictionary<int, GameObject>(); 
    float _msek = 0;

    //TimeRewind _timeRewind;

    private static int _lastUID = 0;

    public static int GetNewUID()
    {
        return _lastUID++;
    }

    // public bool isRewind = false;

    public bool isPause = false;

    public bool IsPaused()
    {
        return isPause;
    }

    public void SetPause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }

    public void SpawnEnemy(Vector3 position)
    {
        _enemy = Instantiate(enemyPrefab) as GameObject;
        Vector3 enemyPosition = position;
        _enemy.GetComponent<SceneObject>().SetTransformation(enemyPosition, Quaternion.identity);
    }


    public void CreateProjectile(Vector3 position, Vector3 dir, float impulseForce, float damage)
    {
        _projectile = Instantiate(projectilePrefab) as GameObject;
        _projectile.GetComponent<Projectile>().Init(position, dir * impulseForce, damage); 
    }


    public void TryInsertSceneObject(GameObject go)
    {
        if (!FindGameObject(go.GetComponent<SceneObject>().UID))
            sceneObjects.Add(go);
    }

    public void DeleteGameObject(int uid)
    {
        int objectsSize = sceneObjects.Count;
        for (int i = 0; i < objectsSize; i++)
        {
            SceneObject so = sceneObjects[i].GetComponent<SceneObject>();
            if (sceneObjects[i] != null && so != null && so.UID == uid)
            {
                sceneObjects.RemoveAt(i);
                break;
            }
        }
    }


    public GameObject FindGameObject(int uid)
    {
        int objectsSize = sceneObjects.Count;
        for (int i = 0; i < objectsSize; i++)
        {
            SceneObject so = sceneObjects[i].GetComponent<SceneObject>();
            if (sceneObjects[i] != null && so != null && so.UID == uid)
            {
                return sceneObjects[i];
            }
        }
        return null;
    }

}
