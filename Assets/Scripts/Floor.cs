using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    SceneController _sceneController; 


    void Awake() {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();  
    }
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit; 
        //     if(Physics.Raycast(ray, out hit)) {
        //         //Debug.Log("This hit at " + hit.point );
        //         _sceneController.SpawnEnemy(hit.point);                
        //     } 
        // }
    }
}
