using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class CharacterSelectionPresenter
{
    private readonly ICharacterSelectionView _view;
    private readonly CharactersDataHolder _charactersDataHolder;
    private Dictionary<CharacterType, GameObject> _characterDictionary = new Dictionary<CharacterType, GameObject>();
    private Dictionary<int, CharacterType> _characterTypeDic = new Dictionary<int, CharacterType>();
    private int _characterIndex;
    private GameObject _selectedCharacter;
    private ICharacterManager _characterManager;
    private IGuiAnimatonManager _guiAnimatonManager;
    private GameController _gameController;

    public CharacterSelectionPresenter(ICharacterSelectionView view, CharactersDataHolder charactersDataHolder ,ICharacterManager characterManager , IGuiAnimatonManager guiAnimatonManager
    ,GameController gameController)
    {
        _view = view;
        _charactersDataHolder = charactersDataHolder;
        this._characterManager = characterManager;
        this._guiAnimatonManager = guiAnimatonManager;
        this._gameController = gameController;
        Initialize();
    }

    private void Initialize()
    {
        _view.RightButton.onClick.AddListener(() => ChangeCharacter(1));
        _view.LeftButton.onClick.AddListener(() => ChangeCharacter(-1));
        _view.SelectionButton.onClick.AddListener(SelectCharacter);

        int characterIndex = 0;
        foreach (var item in _charactersDataHolder.CharacterData)
        {
            var characterInstance = GameObject.Instantiate(item.CharacterSelectionPrefab, _view.CharacterHolderTransform.position,
                quaternion.identity, _view.CharacterHolderTransform);
            characterInstance.transform.localPosition = Vector3.zero;
            characterInstance.transform.localEulerAngles = Vector3.zero;
            _characterDictionary.Add(item.CharcterType, characterInstance);
            _characterTypeDic.Add(characterIndex, item.CharcterType);
            characterInstance.SetActive(false);
            characterIndex++;
        }

        ChangeCharacter(0); // Display the first character initially
    }

    private void SelectCharacter()
    {
        var selectedCharacter = _characterTypeDic[_characterIndex];
        var stat = _charactersDataHolder.CharacterData.FirstOrDefault(x => x.CharcterType == selectedCharacter)
            .CharacterStat;
        _characterManager.UpdateCharacter(selectedCharacter ,stat);
        _guiAnimatonManager.Hide(AnimationNames.CharacterSelection);
        _gameController.SwitchToBoardMode();
    }
    private void ChangeCharacter(int dir)
    {
        _characterIndex += dir;
        if (_characterIndex > _characterDictionary.Count - 1)
        {
            _characterIndex = _characterDictionary.Count - 1;
        }
        else if (_characterIndex < 0)
        {
            _characterIndex = 0;
        }

        var selectedType = _characterTypeDic[_characterIndex];
        var selectedCharacter = _characterDictionary[selectedType];
        if (_selectedCharacter != null)
        {
            _view.HideCharacter(_selectedCharacter);
        }

        _selectedCharacter = selectedCharacter;
        _view.DisplayCharacter(_selectedCharacter);
        var name = _charactersDataHolder.CharacterData[_characterIndex].CharacterName;
        var luck = _charactersDataHolder.CharacterData[_characterIndex].CharacterStat.Luck;
        var bonus = _charactersDataHolder.CharacterData[_characterIndex].CharacterStat.Bonus;
        _view.UpdateCharacterStats(name , luck ,bonus);
    }
}
