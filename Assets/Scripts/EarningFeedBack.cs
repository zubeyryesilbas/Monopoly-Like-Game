using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EarningFeedBack : MonoBehaviour
{
    [SerializeField] private Image _earningImage, _bonusImage;
    [SerializeField] private TextMeshProUGUI _earningText, _bonusEarningText;

    public void SetEarnings(int baseEarning, int bonusEarning , Sprite sprite)
    {
        _earningText.text = "+" + baseEarning;
        _bonusEarningText.text = "+" + bonusEarning;
        _earningImage.sprite = sprite;
        _bonusImage.sprite = sprite;
    }
}
