using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{ 
    private CharacterController _charController; 
    private SceneController _sceneController; 
    private SceneObject _sceneObject; 
    
    GameObject hpLine; 
    public Transform hpLineTransform;
    public float maxSpeed = 2.0f; 
    public float maxHealth = 100; 
    private float _currentSpeed = 0;
    private int _nearestCastleIndex = -1;

    private float _msek = 0;
 
    private Vector3 _moveDir;
    public Vector3 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value; }
    }

    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        set { }
    }


    void Awake()
    {
        _sceneObject = GetComponent<SceneObject>();
        _charController = GetComponent<CharacterController>();
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();

    }

    void Start()
    {
        _moveDir = transform.TransformDirection(new Vector3(1, 0, 0));
        hpLineTransform = this.gameObject.transform.GetChild(0);
         _sceneObject.GetComponent<SceneObject>().Health = maxHealth;
    }
 
    void Update()
    {   
        float health = _sceneObject.GetComponent<SceneObject>().Health;  
        hpLineTransform.localScale = new Vector3(health * 0.01f, 0.05f, 0.1f);

        if (_sceneController.IsPaused())
            return;

      
        if (health <= 0)
        {
            _sceneObject.RemoveFromScene();
            return;
        }

        _nearestCastleIndex = MathLib.nearestGameObjectIndex(_sceneController.sceneObjects, transform.position, "Castle");

        if (_msek > 0.25)
        {
            Debug.Log("ni " + _nearestCastleIndex.ToString());

            if (_nearestCastleIndex > -1)
            {
                Vector3 targetPosition = _sceneController.sceneObjects[_nearestCastleIndex].transform.position;
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition);
                _currentSpeed = maxSpeed;

            }
            _moveDir = transform.TransformDirection(new Vector3(0, 0, 1));
        }

        Vector3 movement = _moveDir * _currentSpeed * Time.deltaTime;
        movement = Vector3.ClampMagnitude(movement, _currentSpeed);
        _charController.Move(movement);

        _msek += Time.deltaTime;


    }
}
