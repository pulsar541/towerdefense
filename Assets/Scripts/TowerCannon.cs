using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCannon : MonoBehaviour
{
    
    SceneController _sceneController;
    [SerializeField] private GameObject projectilePrefab;
    private GameObject _projectile; 
    float _msek = 0;
    // Start is called before the first frame update

    
    public float cannonImpulseForce = 9.0f;

    private bool _isShooting = false;
    public bool Shooting {
        get{return _isShooting;}
        set{_isShooting = value;}
    }
    

    void Start()
    {
        GameObject ob = GameObject.Find("SceneController");
        _sceneController = (SceneController)ob.GetComponent<SceneController>();       
    }

    // Update is called once per frame
    void Update()
    {
        if(_msek > 1) {
			_msek = 0;
            if(_isShooting) {
                 Vector3 shootDir =  transform.TransformDirection(new Vector3(0, -1, 0));
                _sceneController.CreateProjectile(transform.position + shootDir.normalized * transform.localScale.y / 2 + shootDir,
                                                  shootDir,
                                                  cannonImpulseForce);

                // _projectile = Instantiate(projectilePrefab.projectilePrefab) as GameObject;
               
                // _projectile.transform.position = transform.position + shootDir.normalized * transform.localScale.y / 2 + shootDir;
                // Rigidbody rbProjectile = _projectile.gameObject.GetComponent<Rigidbody>(); 
                // rbProjectile.AddForce(shootDir.normalized * cannonImpulseForce, ForceMode.Impulse);
 
            }
		}
 
        _msek += Time.deltaTime;
    }

    public void Shoot() {
        _projectile = Instantiate(projectilePrefab) as GameObject;
        Vector3 shootDir =  transform.TransformDirection(new Vector3(0, -1, 0));
        _projectile.transform.position = transform.position + shootDir.normalized * transform.localScale.y / 2 + shootDir;
        Rigidbody rbProjectile = _projectile.gameObject.GetComponent<Rigidbody>(); 
        rbProjectile.AddForce(shootDir.normalized * cannonImpulseForce, ForceMode.Impulse);       
    }
    
}
