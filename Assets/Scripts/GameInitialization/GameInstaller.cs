using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    private IGameStateSaver _gameStateSaver;
    private ICharacterManager _characterManager;
    private IItemManager _itemManager;
    private GameStateManager _gameStateManager;
    private IGuiAnimatonManager _guiAnimatonManager;
    [SerializeField] private CameraController _cameraController;
    private GameController _gameController;
    [SerializeField] private CharacterSelection _characterSelection;
    [SerializeField] private SpriteTypeHolderSO _itemSpriteHolder;
    [SerializeField] private ItemPresenter _itemPresenter;
    [SerializeField] private BoardController _boardController;
    [SerializeField] private CasePresenter _casePresenter;
    [SerializeField] private FeedBackFactory _feedBackFactory;
    [SerializeField] private StatItemsPresenter _statItemsPresenter;
    [SerializeField] private CharactersDataHolder _charactersDataHolder;
    private FeedBackListener _feedBackListener;
    private void Start()
    {
        _guiAnimatonManager = GuiAnimationManager.Instance;
        _gameController = new GameController(_cameraController , _guiAnimatonManager , _characterSelection , _boardController  );
        _gameStateSaver = new PlayerPrefsGameStateSaver();
        _characterManager = new CharacterManager();
        _itemManager = new ItemManager();
        _gameStateManager = GameStateManager.Instance;
        _boardController.Initialize(_itemSpriteHolder);
        _gameStateManager.Initialize(_characterManager , _itemManager , _gameStateSaver);
        _characterSelection.Initialize(_characterManager, _guiAnimatonManager, _gameController ,_charactersDataHolder ,_statItemsPresenter );
        _itemPresenter.Initialize(_itemManager , _itemSpriteHolder , _boardController);
        _casePresenter.Initialize(_itemManager , _itemSpriteHolder , _boardController , _itemPresenter, _characterManager);
        _feedBackListener = new FeedBackListener(_boardController, _feedBackFactory ,_itemSpriteHolder , _characterManager);
    }
}

