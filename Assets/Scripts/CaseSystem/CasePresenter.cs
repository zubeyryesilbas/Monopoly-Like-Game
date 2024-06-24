using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CasePresenter : MonoBehaviour
{
   [SerializeField] private InventoryItem _inventoryItem;
   [SerializeField] private Transform _inventoryHolder;
   [SerializeField] private Button _caseButton;
   private IItemManager _itemManager;
   private Dictionary<ItemType, InventoryItem> _inventoryItemDic = new Dictionary<ItemType, InventoryItem>();
   private BoardController _boardController;
   private Dictionary<ItemType, int> _itemAmountDic = new Dictionary<ItemType, int>();
   private ObjectPooler _objectPooler;
   private ItemPresenter _itemPresenter;
   [SerializeField] private Canvas _canvas;
   [SerializeField] private RectTransform _target;
   private int _bonus;
   private ICharacterManager _characterManager;
   private void OnDisable()
   {
      _boardController.OnLastStepCompleted -= UpdateItems;
   }

   public void Initialize(IItemManager itemManager ,SpriteTypeHolderSO  spriteHolder , BoardController boardController ,ItemPresenter itemPresenter , 
      ICharacterManager characterManager )
   {
      _characterManager = characterManager;
     _caseButton.onClick.AddListener(FlyElements);
      _itemPresenter = itemPresenter;
      _objectPooler = ObjectPooler.Instance;
      _itemManager = itemManager;
      _boardController = boardController;
      _boardController.OnLastStepCompleted += UpdateItems;
      foreach (var item in _itemManager.ItemsEarned)
      {
         var type = item.ItemType;
         var amount = 0;
         _itemAmountDic.Add(item.ItemType , amount);
         var itemInstance = Instantiate(_inventoryItem, transform.position, Quaternion.identity, _inventoryHolder);
         var sprite = spriteHolder.Sprites.FirstOrDefault(X => X.ItemType == type).SpriteGui;
         itemInstance.Initialize(sprite , amount);
         _inventoryItemDic.Add(item.ItemType , itemInstance);
      }
   }

   private void FlyElements()
   {  
      var keyList = _itemAmountDic.Keys.ToList();
      foreach (var item in keyList)
      {
         var itemAmount = _itemAmountDic[item];
         _itemPresenter.UpdateItems(item , itemAmount);
         _itemAmountDic[item] = 0;
         _inventoryItemDic[item].UpdateItemCount(0);
      }
     
   }
   private void UpdateItems(TileData tileData)
   {
      switch (tileData.ItemType)
      {
         case ItemType.Empty:
            return;
         case ItemType.Looseall:
            ResetCase();
            break;
         case ItemType.X2:
            MultiplyCase(2);
            break;
         default:
            AddItemToCase(tileData);
            break;
            
      }
   }

   private void AddItemToCase(TileData data)
   {  
      _bonus = _characterManager.CharacterStat.Luck;
      var baseAmount = data.ItemAmount;
      var increase = Mathf.RoundToInt(baseAmount * _bonus / 100f);
      var finalAmount = baseAmount + increase;
      var val = _itemAmountDic[data.ItemType];
      val += finalAmount;
      _itemAmountDic[data.ItemType]= val;
      _inventoryItemDic[data.ItemType].UpdateItemCount(val);
   }
   private void MultiplyCase(int amount)
   {
      var keys = _itemAmountDic.Keys.ToList();                        
      foreach (var key in keys)                              
      {    
         var val = _itemAmountDic[key];
         val *= amount;
         _itemAmountDic[key] = amount ;                            
         _inventoryItemDic[key].UpdateItemCount(val);          
      }                                                      
   }
   private void ResetCase()
   {
      var keys = _itemAmountDic.Keys.ToList();
      foreach (var key in keys)
      {
         _itemAmountDic[key] = 0;
         _inventoryItemDic[key].UpdateItemCount(0);
      }
   }
}
