using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : TDMonoBehaviour
{
    
 
    private CharacterController _charController;

    private SceneController _sceneController;

    //TimeRewind _timeRewind;

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

    public void SetRotation(Vector3 rotation) {  
        _charController.enabled = false;
        _charController.transform.localEulerAngles = rotation;
        _charController.enabled = true; 
    }
     // Start is called before the first frame update
 
 
    void Awake() {
       _charController = GetComponent<CharacterController>();        
    }

    void Start()
    {    _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        //_timeRewind = GameObject.Find("SceneController").GetComponent<TimeRewind>();
 
        _moveDir = transform.TransformDirection(new Vector3(1,0,0) );
        //SetPosition(_startPosition); 
 
        _uid = SceneController.GetNewUID();    

        hpLineTransform = this.gameObject.transform.GetChild(0);  
    }

    // Update is called once per frame
    void Update()
    { 

        if(_sceneController.isRewind) {
            
        }
        else {

            if(Health <= 0) {
                RemoveEnemy();
                return;
            }

            _nearestCastleIndex = MathLib.nearestGameObjectIndex(_sceneController.sceneObjects, transform.position, "Castle"); 

            if(_msek > 0.25) {         
                if(_nearestCastleIndex > -1) { 
                    Vector3 targetPosition = _sceneController.sceneObjects[_nearestCastleIndex].transform.position;
                    targetPosition.y = transform.position.y; 
                    transform.LookAt(targetPosition); 
                    _currentSpeed = maxSpeed;
 
                 }   
                 _moveDir  = transform.TransformDirection(new Vector3(0,0,1)); 
             } 

            Vector3 movement =  _moveDir * _currentSpeed * Time.deltaTime;
            movement = Vector3.ClampMagnitude(movement, _currentSpeed);
            _charController.Move(movement);  
           
            _msek += Time.deltaTime;


             hpLineTransform.localScale  = new Vector3(Health * 0.01f, 0.05f, 0.1f);
            //var hpLineRenderer = hpLineTransform.GetComponent<Renderer>();  
            // hpLineRenderer.material.SetColor("_Color", new Color(1.0f - Health * 0.01f , 0 , Health * 0.01f)); 
            //hpLineRenderer.GetComponent<TextMesh>().GetComponent<Renderer>().material.SetColor("_Color", new Color(1 , 0 , 0)); 



        }
    }

    public void RemoveEnemy() { 
         //mustDestroy = true;
         this.gameObject.SetActive(false);
        _sceneController.DeleteGameObject(_uid);
        Destroy(this.gameObject); 
    }
 
    // public void SetTransformation(Vector3 position, Quaternion rotation) { 
    //     _charController.enabled = false;
    //     _charController.transform.position = position;
    //     _charController.transform.rotation = rotation;
    //     _charController.enabled = true;  
    // }
}
