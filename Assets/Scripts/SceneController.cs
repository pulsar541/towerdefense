using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
     [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy; 

     [SerializeField] private GameObject projectilePrefab;
     private GameObject _projectile; 


    public static List<GameObject> enemies = new List<GameObject>();
    public static List<GameObject> castles = new List<GameObject>(); 

    float _msek = 0;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
          
        if(_msek > 1) {
            _msek = 0;
       
          
        }

        _msek += Time.deltaTime;
    }

    public void SpawnEnemy(Vector3 position) {
        _enemy = Instantiate(enemyPrefab) as GameObject; 
        Vector3 enemyPosition = position; 
        _enemy.GetComponent<EnemyAI>().SetStartPosition(enemyPosition);   
        enemies.Add(_enemy);
    }


    public void CreateProjectile(Vector3 position, Vector3 dir, float impulseForce) {
        _projectile = Instantiate(projectilePrefab) as GameObject; 
        _projectile.transform.position = position;
        Rigidbody rbProjectile = _projectile.gameObject.GetComponent<Rigidbody>(); 
        rbProjectile.AddForce(dir.normalized * impulseForce, ForceMode.Impulse); 
    }


}
