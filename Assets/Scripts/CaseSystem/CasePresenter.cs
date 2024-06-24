using System;
using System.Linq;
using Item;

public class CasePresenter
{
    private readonly CaseModel _model;
    private readonly CaseView _view;
    private readonly BoardController _boardController;
    private readonly SpriteTypeHolderSO _spriteHolder;
    private readonly EventManager _eventManager;

    public CasePresenter(CaseModel model, CaseView view, BoardController boardController, SpriteTypeHolderSO spriteHolder , EventManager eventManager)
    {
        _model = model;
        _view = view;
        _boardController = boardController;
        _spriteHolder = spriteHolder;
        _eventManager = eventManager;

        _view.OnCaseButtonClicked += FlyElements;
        eventManager.AddEventListener(EventConstants.BoardEvents.ONLASTSTEPCOMPLETED, (data) => UpdateItems((TileData)data));

        InitializeView();
    }

    private void InitializeView()
    {
        _view.Initialize(_spriteHolder, _model.GetItemTypes());
        foreach (var itemType in _model.ItemAmounts.Keys)
        {
            _view.UpdateItemCount(itemType, 0);
        }
    }

    private void FlyElements()
    {   
        var itemTypes = _model.ItemAmounts.Keys.ToList();
        foreach (var item in itemTypes)
        {   
            _eventManager.TriggerEvent(EventConstants.CaseEvents.ONCASECOLLECTED , (new Tuple<ItemType , int>(item ,_model.ItemAmounts[item])));
            _model.ResetItem(item);
            _view.UpdateItemCount(item, 0);
        }
    }

    private void UpdateItems(TileData tileData)
    {
        switch (tileData.ItemType)
        {
            case ItemType.Empty:
                return;
            case ItemType.Looseall:
                _model.ResetItems();
                break;
            case ItemType.X2:
                _model.MultiplyItems(2);
                break;
            default:
                _model.AddItem(tileData);
                break;
        }

        foreach (var itemType in _model.ItemAmounts.Keys)
        {
            _view.UpdateItemCount(itemType, _model.ItemAmounts[itemType]);
        }
    }
}
