using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFaceArranger : MonoBehaviour
{
    [SerializeField] private Vector3 _rotOffest;
    public void SetDiceFace(int face)
    {
        switch (face)
        {
            case 1:
                transform.localEulerAngles = _rotOffest+ new Vector3(-90, 0, 0);
                break;
            case 2:
                transform.localEulerAngles = _rotOffest + new Vector3(0, 0, 0);
                break;
            case 3:
                transform.localEulerAngles = _rotOffest + new Vector3(0, 0, 270);
                break;
            case 4:
                transform.localEulerAngles = _rotOffest + new Vector3(0, 0, 90);
                break;
            case 5:
                transform.localEulerAngles = _rotOffest + new Vector3(0, 0, 180);
                break;
            case 6:
                transform.localEulerAngles = _rotOffest + new Vector3(90, 0, 270);
                break;
        }
    }
}
