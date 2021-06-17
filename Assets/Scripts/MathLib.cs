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
 
    public static int nearestGameObjectIndex(List<GameObject> list, Vector3 sourcePos, string type, float searchRadius = Mathf.Infinity)
    {
        float tmpSquareDist = searchRadius * searchRadius;
        int lisrSize = list.Count;
        int nearestCastleIndex = -1;
        for (int i = 0; i < lisrSize; i++)
        {
            if (list[i] != null && list[i].name.IndexOf(type) > -1)
            {
                float squareDistance = MathLib.squareDist(list[i].transform.position, sourcePos);
                if (squareDistance < tmpSquareDist)
                {
                    tmpSquareDist = squareDistance;
                    nearestCastleIndex = i;
                }
            }
        }

        return nearestCastleIndex;
    }
 
}
