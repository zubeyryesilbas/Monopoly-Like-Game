using UnityEngine;
using UnityEngine.UI;

public interface ICharacterSelectionView
{
    Button RightButton { get; }
    Button LeftButton { get; }
    Button SelectionButton { get; }
    Transform CharacterHolderTransform { get; }
    void UpdateCharacterStats(string name ,float luck, float bonus);
    void DisplayCharacter(GameObject character);
    void HideCharacter(GameObject character);
}