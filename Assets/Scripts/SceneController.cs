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

    public static  int GetNewUID() {
        return _lastUID++;
    }

    public bool isRewind = false;

    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
          
        if (Input.GetMouseButtonDown(1)) {
            isRewind = !isRewind;
        }

        if(_msek > 0.1) {
            _msek = 0; 
 
        }
  
        
        _msek += Time.deltaTime;
    }

    public void SpawnEnemy(Vector3 position) {
        _enemy = Instantiate(enemyPrefab) as GameObject; 
        Vector3 enemyPosition = position; 
        _enemy.GetComponent<EnemyAI>().SetPosition(enemyPosition);   
        sceneObjects.Add(_enemy); 
 
    }


    public void CreateProjectile(Vector3 position, Vector3 dir, float impulseForce) {
        _projectile = Instantiate(projectilePrefab) as GameObject; 
        _projectile.transform.position = position;
        Rigidbody rbProjectile = _projectile.gameObject.GetComponent<Rigidbody>(); 
        rbProjectile.AddForce(dir.normalized * impulseForce, ForceMode.Impulse);  
         sceneObjects.Add(_projectile);  
    }

    // private void ObjectListCollector() {
    //     int objectsSize = sceneObjects.Count; 
    //     for(int i = 0; i < objectsSize; i++) {
    //         if(sceneObjects[i] != null && sceneObjects[i].GetComponent<TDMonoBehaviour>().mustDestroy) {
    //             sceneObjects[i].SetActive(false);
    //             //sceneObjects.RemoveAt(i);
    //             break;
    //         }
    //     }
 
    // } 
 
    public void DeleteGameObject(int uid) {
        int objectsSize = sceneObjects.Count; 
        for(int i = 0; i < objectsSize; i++) {
            SceneObject so = sceneObjects[i].GetComponent<SceneObject>();
            if(sceneObjects[i] != null && so != null && so.UID == uid) {  
                sceneObjects.RemoveAt(i);
                break;
            }
        }     
    }

    
    public GameObject FindGameObject(int uid ) {
        int objectsSize = sceneObjects.Count; 
        for(int i = 0; i < objectsSize; i++) {
            SceneObject so = sceneObjects[i].GetComponent<SceneObject>();
            if(sceneObjects[i] != null && so != null && so.UID == uid) { 
                    return sceneObjects[i];
            }
        }   
        return null;      
    }

}
