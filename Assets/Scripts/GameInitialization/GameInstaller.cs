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
    [SerializeField] private BoardController _boardController;
    private EventManager _eventManager;
    [SerializeField] private CasePresenter _casePresenter;
    [SerializeField] private FeedBackFactory _feedBackFactory;
    [SerializeField] private StatItemsPresenter _statItemsPresenter;
    [SerializeField] private CharactersDataHolder _charactersDataHolder;
    private FeedBackListener _feedBackListener;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private CaseSystem _caseSystem;
    private void Start()
    {
        _eventManager = EventManager.Instance;
        _guiAnimatonManager = GuiAnimationManager.Instance;
        _gameController = new GameController(_cameraController , _guiAnimatonManager , _characterSelection , _boardController  );
        _gameStateSaver = new PlayerPrefsGameStateSaver();
        _characterManager = new CharacterManager();
        _itemManager = new ItemManager();
        _gameStateManager = GameStateManager.Instance;
        _boardController.Initialize(_itemSpriteHolder);
        _gameStateManager.Initialize(_characterManager , _itemManager , _gameStateSaver);
        _characterSelection.Initialize(_characterManager, _guiAnimatonManager, _gameController ,_charactersDataHolder ,_statItemsPresenter );
        _inventorySystem.Initialize(_itemManager , _itemSpriteHolder , _eventManager);
        _caseSystem.Initialize(_boardController ,_itemSpriteHolder ,_itemManager ,_characterManager , _eventManager);
        _feedBackListener = new FeedBackListener(_feedBackFactory ,_itemSpriteHolder , _characterManager , _eventManager);
    }
}

