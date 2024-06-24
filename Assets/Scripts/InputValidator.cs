using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputValidator : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    private int _faceValue = 1;

    private void Awake()
    {
        _inputField.onValueChanged.AddListener(ValidateInput);
    }

    void ValidateInput(string input)
    {
        if (int.TryParse(input, out int faceValue))
        {
            if (faceValue >= 1 && faceValue <= 6)
            {
                _faceValue = faceValue;
            }
            else
            {  
                _inputField.text = _faceValue.ToString();
            }
        }
        else
        {
            _inputField.text = _faceValue.ToString();
        }
    }

    public int GetValidFace()
    {
        return _faceValue;
    }
}
