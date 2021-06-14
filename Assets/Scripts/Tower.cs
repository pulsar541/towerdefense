using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{ 

    private int _nearestEnemyIndex = -1;
    private float _msek = 0;
    // Start is called before the first frame update
 

    GameObject towerCannon;
    void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.gameObject.name.IndexOf("TowerCannon") > -1) {
                towerCannon = child.gameObject;
                break;
            }
        } 
    }

    // Update is called once per frame
    void Update()
    { 
        ///transform.Rotate(new Vector3(0,1,0), 10.0f * Time.deltaTime); 
        if(_msek > 0.5) { 
            if(_nearestEnemyIndex > -1) { 

                if(SceneController.enemies[_nearestEnemyIndex] != null) { 
                    EnemyAI enemy =   SceneController.enemies[_nearestEnemyIndex].GetComponent<EnemyAI>();
                    Vector3 targetLook =  enemy.transform.position + enemy.CurrentSpeed * enemy.MoveDir * 1.0f;


                    targetLook.y = transform.position.y;
                    transform.LookAt(targetLook); 
                    towerCannon.GetComponent<TowerCannon>().Shooting = true; 
                }   
            } else {
                towerCannon.GetComponent<TowerCannon>().Shooting = false;
            } 
            StartCoroutine(findNearestEnemyIndex(SceneController.enemies, transform.position)); 
        } 
        _msek += Time.deltaTime;
    }
 
    public IEnumerator findNearestEnemyIndex(List<GameObject> list, Vector3 sourcePos)
    {  
        float tmpSquareDist = Mathf.Infinity; 
        int lisrSize  = list.Count;
        _nearestEnemyIndex = -1;
        yield return null;
        for(int i = 0; i < lisrSize; i++) {
            if(list[i] != null) {
                float squareDistance = MathLib.squareDist(list[i].transform.position, sourcePos);
                if(squareDistance < tmpSquareDist) {
                    tmpSquareDist = squareDistance;  
                    _nearestEnemyIndex = i;
                    yield return null;
                }
            }
        } 
    }

    
}
