using System;
using Item;

public class InventoryPresenter
{
    private readonly InventoryModel _model;
    private readonly InventoryView _view;
    private readonly SpriteTypeHolderSO _spriteHolder;
    private readonly EventManager _eventManager;

    public InventoryPresenter(InventoryModel model, InventoryView view, SpriteTypeHolderSO spriteHolder, EventManager eventManager)
    {
        _model = model;
        _view = view;
        _spriteHolder = spriteHolder;
        _eventManager = eventManager;
        _eventManager.AddEventListener(EventConstants.CaseEvents.ONCASECOLLECTED,(data) => UpdateItems((Tuple<ItemType,int>)data));
        InitializeView();
    }

    private void InitializeView()
    {
        _view.Initialize(_spriteHolder, _model.GetItems());

        foreach (var item in _model.GetItems())
        {
            var type = item.ItemType;
            var position = _model.GetItemPosition(type);
            _view.SetItemPosition(type, position);
        }
    }

    public void UpdateItems(Tuple<ItemType ,int> tuple)
    {
        var type = tuple.Item1;
        var amount = tuple.Item2;
        _model.AddItems(type, amount);
        var updatedAmount = _model.GetItemAmount(type);
        _view.UpdateItemCount(type, updatedAmount);
    }
}