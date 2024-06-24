using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceConstants
{
    private static Dictionary<Vector3 ,int> DiceFaces = new Dictionary<Vector3 , int>()
    {
        {new Vector3(0, 0, 0) ,2 },
        {new Vector3(0, 0, 90) , 4}
        ,{new Vector3(0, 0, 270) , 3},
        {new Vector3(0, 0, 180) , 5},
        {new Vector3(90, 0, 0) , 6},
        {new Vector3(270, 0, 0) , 1}
        
    };

    public static int GetFace(Vector3 rot)
    {
        rot.y = 0;
        rot.x = Mathf.RoundToInt(rot.x);
        rot.z = Mathf.RoundToInt(rot.z);
        if (rot.x == 360) rot.x = 0;

        if (rot.z == 360) rot.z = 0;

        var faceValue = DiceFaces[rot];
        return faceValue;
    }
}
