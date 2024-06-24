using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField]private float _xSpeed, _ySpeed, _zSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up , _ySpeed *Time.deltaTime);
        transform.Rotate(Vector3.right , _xSpeed *Time.deltaTime);
        transform.Rotate(Vector3.forward , _zSpeed *Time.deltaTime);
    }
}
