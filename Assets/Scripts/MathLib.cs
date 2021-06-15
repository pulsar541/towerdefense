using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLib
{
    const float piOver180 = Mathf.PI / 180.0f;

    public static float squareDist(Vector3 a, Vector3 b) 
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
    }
    
    public static float angleFromPositions(Vector3 srcPos, Vector3 tPos)
    {
        float X1 = srcPos.x;
        float Y1 = srcPos.y;
        float X2 = tPos.x;
        float Y2 = tPos.y;
        
        float b = Mathf.Abs(Y1-Y2);
        float c = Mathf.Abs(X1-X2);
        float resultAngle = 0;

        if(Y2<=Y1 && X2>=X1) resultAngle = 360 - (float)Mathf.Atan(b/c)/piOver180;
        if(Y2<=Y1 && X2<X1)  resultAngle = 180 + (float)Mathf.Atan(b/c)/piOver180;
        if(Y2>Y1 && X2<=X1)  resultAngle = 180 - (float)Mathf.Atan(b/c)/piOver180;
        if(Y2>Y1 && X2>X1)   resultAngle = (float)Mathf.Atan(b/c)/piOver180;

        return resultAngle;
    }

    
    public static int nearestGameObjectIndex(List<GameObject> list, Vector3 sourcePos, string type)
    {  
        float tmpSquareDist = Mathf.Infinity; 
        int lisrSize  = list.Count; 
        int nearestCastleIndex = -1;
        for(int i = 0; i < lisrSize; i++) {
            if(list[i] != null && list[i].name.IndexOf(type) > -1) {
                float squareDistance = MathLib.squareDist(list[i].transform.position, sourcePos);
                if(squareDistance < tmpSquareDist) {
                    tmpSquareDist = squareDistance;  
                    nearestCastleIndex = i; 
                }
            }
        } 

        return nearestCastleIndex; 
    }

    // public static bool isObjectInSphere(GameObject gameObject, Vector3 spherePos, float sphereRadius) {
        
    //     float bbHalfSize = gameObject.transform.localScale.x * 0.5f; 
    //     Vector3 boundingBoxStart = new Vector3 (
    //         gameObject.transform.position.x - bbHalfSize,
    //         gameObject.transform.position.y - bbHalfSize,     
    //         gameObject.transform.position.z - bbHalfSize                   
    //     );
    //     Vector3 boundingBoxFinish = new Vector3 (
    //         gameObject.transform.position.x + bbHalfSize,
    //         gameObject.transform.position.y + bbHalfSize,     
    //         gameObject.transform.position.z + bbHalfSize                   
    //     );

    //     if(Mathf.Abs(gameObject.transform))

    //     return false;
    // }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
