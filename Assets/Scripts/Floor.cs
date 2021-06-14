using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    SceneController _sceneController; 

    // Start is called before the first frame update
    void Start()
    {
        GameObject ob = GameObject.Find("SceneController");
        _sceneController = (SceneController)ob.GetComponent<SceneController>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            if(Physics.Raycast(ray, out hit)) {
                //Debug.Log("This hit at " + hit.point );
                _sceneController.SpawnEnemy(hit.point);                
            } 
        }
    }
}
