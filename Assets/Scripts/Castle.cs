using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private SceneController _sceneController;

    bool m_Started;

    void Awake()
    {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    void Start()
    {
        m_Started = true; 
    }
 
    void Update()
    {
        if (_sceneController.IsPaused())
            return;

        Collider[] hitColliders = Physics.OverlapCapsule(
            transform.position + new Vector3(0, -transform.localScale.y / 2, 0),
            transform.position + new Vector3(0, transform.localScale.y / 2, 0),
            transform.localScale.y / 2);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.name.IndexOf("Enemy") > -1)
            { 
                hitCollider.gameObject.GetComponent<SceneObject>().RemoveFromScene(); 
                //Debug.Log ("Попадание:" + hitCollider.name  );
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
