using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionView : MonoBehaviour, ICharacterSelectionView
{
    [SerializeField] private Button _rightButton, _leftButton , _selectionButton;
    [SerializeField] private Transform _characterHolderTransform;
    [SerializeField] private TextMeshProUGUI _nameText, _luckText, _bonusText;

    public Button RightButton => _rightButton;
    public Button LeftButton => _leftButton;
    public Button SelectionButton => _selectionButton;
    public Transform CharacterHolderTransform => _characterHolderTransform;

    public void UpdateCharacterStats( string name ,float luck, float bonus)
    {
        _nameText.text = name;
        _luckText.text = ""+luck;
        _bonusText.text = "" + bonus;
    }

    public void DisplayCharacter(GameObject character)
    {
        character.SetActive(true);
    }

    public void HideCharacter(GameObject character)
    {
        character.SetActive(false);
    }
}