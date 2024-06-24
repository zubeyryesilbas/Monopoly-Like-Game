using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private Image _statImage;

    public void Initialize(string str )
    {
        _statText.text = str;
    }
}
