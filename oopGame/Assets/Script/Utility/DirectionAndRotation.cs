using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionAndRotation 
{
    // Start is called before the first frame update
    
     public static Vector3 LeveldTargetDirection (Vector3 posOne, Vector3 posTwo)
     {
        posTwo = new Vector3(posTwo.x, posOne.y, posTwo.z); 
        Vector3 generalDirection = posOne - posTwo;
        Debug.Log(generalDirection.normalized);
        //generalDirection = new Vector3(generalDirection.x, posOne.y, generalDirection.z);

        
        return generalDirection;
     }
}
