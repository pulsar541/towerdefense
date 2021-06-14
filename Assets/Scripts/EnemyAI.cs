﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    private CharacterController _charController;

    GameObject hpLine;

    public Transform hpLineTransform;
    public float maxSpeed = 2.0f;

    private float _currentSpeed = 0;
    private int _nearestCastleIndex = -1;

    private float _msek = 0;
    
    private float _health = 100;
    public float Health {
        get {return _health;}
        set {_health = value;}
    }

    private Vector3 _moveDir;
    public Vector3 MoveDir {
        get{return _moveDir;}
        set{_moveDir = value;}
    }

    public float CurrentSpeed {
        get { return _currentSpeed; } 
        set { }
    }

    private Vector3 _startPosition = new Vector3(0, 0, 0);
 
    public void SetStartPosition(Vector3 position) {
        _startPosition = position;
    }

    public void SetPosition(Vector3 position) {  
        _charController.enabled = false;
        _charController.transform.position = position;
        _charController.enabled = true; 
    }
 
     // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _moveDir = transform.TransformDirection(new Vector3(0,0,1) );
        SetPosition(_startPosition); 

        hpLineTransform = this.gameObject.transform.GetChild(0); 
    }

    // Update is called once per frame
    void Update()
    {

        if(Health <= 0) {
            DestroyEnemy();
            return;
        }

         StartCoroutine(findNearestCastleIndex(SceneController.castles, transform.position)); 
         if(_msek > 0.25) {        
            //_nearestCastleIndex = MathLib.nearestGameObjectIndex(SceneController.castles, transform.position);
            if(_nearestCastleIndex > -1) { 
                Vector3 targetPosition = SceneController.castles[_nearestCastleIndex].transform.position;
                targetPosition.y = transform.position.y; 
                transform.LookAt(targetPosition); 
                _currentSpeed = maxSpeed;
            }   
            _moveDir  = transform.TransformDirection(new Vector3(0,0,1) );
        } 

        Vector3 movement =  _moveDir * _currentSpeed * Time.deltaTime;
        movement = Vector3.ClampMagnitude(movement, _currentSpeed);
        _charController.Move(movement); 
        _msek += Time.deltaTime;


        hpLineTransform.localScale  = new Vector3(Health * 0.01f, 0.05f, 0.1f);
        var hpLineRenderer = hpLineTransform.GetComponent<Renderer>();  
        hpLineRenderer.material.SetColor("_Color", new Color(1.0f - Health * 0.01f , 0 , Health * 0.01f)); 
    }

    public void DestroyEnemy() {
        Destroy(this.gameObject);
    }

      
    public IEnumerator findNearestCastleIndex(List<GameObject> list, Vector3 sourcePos)
    {  
        float tmpSquareDist = Mathf.Infinity; 
        int lisrSize  = list.Count;
        for(int i = 0; i < lisrSize; i++) {
            if(list[i] != null) {
                float squareDistance = MathLib.squareDist(list[i].transform.position, sourcePos);
                if(squareDistance < tmpSquareDist) {
                    tmpSquareDist = squareDistance;  
                    _nearestCastleIndex = i;
                     yield return null;
                }
            }
        } 
        _nearestCastleIndex = -1;
        yield return null;
    }
 
    //  void OnCollisionEnter(Collision collision)
    // {
   
    //     if(collision.gameObject.name.IndexOf("Projectile") > -1) {  
    //         Destroy(gameObject); 
    //     }

         
    // }
    
}
