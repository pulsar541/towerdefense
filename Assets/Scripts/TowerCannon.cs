using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCannon : MonoBehaviour
{
    
    SceneController _sceneController; 
    float _msek = 0;
    // Start is called before the first frame update

    
    public float projectileDamage = 50;
    public float impulseForce = 10; 
    public float angleFromHor = 20;
    public float shootInterval = 2; 


    private bool _isShooting = false;
    public bool Shooting {
        get{return _isShooting;}
        set{_isShooting = value;}
    }
    

    void Awake()
    {
        GameObject ob = GameObject.Find("SceneController");
        _sceneController = (SceneController)ob.GetComponent<SceneController>();       
    }

    // Update is called once per frame
    void Update()
    {
        if(_sceneController.isPause)
            return;

        if(_msek > shootInterval) {
			_msek = 0;
            if(_isShooting) {
                 Vector3 shootDir =  transform.TransformDirection(new Vector3(0, -1, 0));
                _sceneController.CreateProjectile(transform.position + shootDir.normalized * transform.localScale.y / 2 + shootDir,
                                                  shootDir,
                                                  impulseForce,
                                                  projectileDamage); 
            }
		}
 
        _msek += Time.deltaTime;
    } 
}
