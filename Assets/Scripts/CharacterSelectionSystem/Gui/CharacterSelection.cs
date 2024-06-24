using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private CharacterSelectionView _view;
    private CharactersDataHolder _charactersDataHolder;
    private CharacterSelectionPresenter _presenter;
    private ICharacterManager _characterManager;
    private IGuiAnimatonManager _guiAnimationManager;
    private GameController _gameController;
    private StatItemsPresenter _statItemsPresenter;

    public void Initialize( ICharacterManager characterManager , IGuiAnimatonManager guiAnimatonManager , GameController gameController , CharactersDataHolder charactersDataHolder,StatItemsPresenter statItemsPresenter)
    {
        _charactersDataHolder = charactersDataHolder;
        _characterManager = characterManager;
        _guiAnimationManager = guiAnimatonManager;
        _gameController = gameController;
        _statItemsPresenter = statItemsPresenter;
        _presenter = new CharacterSelectionPresenter(_view, _charactersDataHolder , _characterManager ,_guiAnimationManager ,_gameController );
    }

    public CharacterMovement CreateCharacterMovementAtPos(Vector3 pos)
    {
        var data = _charactersDataHolder.CharacterData.FirstOrDefault(x =>
            x.CharcterType == _characterManager.CharacterType);
        var prefab =data.CharacterMovementPrefab;
        _statItemsPresenter.Initialize(data.CharacterStat.Luck , data.CharacterStat.Bonus);
        var character =  Instantiate(prefab, Vector3.zero, quaternion.identity);
        return character.GetComponent<CharacterMovement>();
    }
}