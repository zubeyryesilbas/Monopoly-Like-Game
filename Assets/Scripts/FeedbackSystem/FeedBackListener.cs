using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using Unity.VisualScripting;
using UnityEngine;

public class FeedBackListener
{
   private BoardController _boardController;
   private FeedBackFactory _feedBackFactory;
   private SpriteTypeHolderSO _spriteTypeHolderSo;
   private Dictionary<ItemType, Sprite> _itemTypeSpriteDic = new FlexibleDictionary<ItemType, Sprite>();
   private int _bonus;
   private ICharacterManager _characterManager;

   public FeedBackListener(BoardController boardController, FeedBackFactory feedBackFactory , SpriteTypeHolderSO spriteTypeHolderSo ,ICharacterManager characterManager)
   {
      _characterManager = characterManager;
      _boardController = boardController;
      _feedBackFactory = feedBackFactory;
      _spriteTypeHolderSo = spriteTypeHolderSo;
      foreach (var item in spriteTypeHolderSo.Sprites)
      {
         _itemTypeSpriteDic.Add(item.ItemType , item.SpriteGui);
      }
      StartListening();
   }

   private void StartListening()
   {
      _boardController.OnLastStepCompleted += OnLastStepCompleted;
   }

   private void OnLastStepCompleted(TileData tileData)
   {
      _bonus = _characterManager.CharacterStat.Bonus;
      switch (tileData.ItemType)
      {
         case ItemType.Looseall:
            _feedBackFactory.CreateFeedBack(ItemType.Looseall);
            break;
         case ItemType.X2:
            _feedBackFactory.CreateFeedBack(ItemType.X2);
            break;
         case ItemType.Apple :
            CreateEarningFeedBack(tileData);
            break;
         case ItemType.Strawberry:
            CreateEarningFeedBack(tileData);
            break;
         case ItemType.Pears:
            CreateEarningFeedBack(tileData);
            break;
         
      }
   }

   private void CreateEarningFeedBack(TileData tileData)
   {
      var feedBack = _feedBackFactory.CreateFeedBack(tileData.ItemType);
      feedBack.Play();
      var sprite = _itemTypeSpriteDic[tileData.ItemType];
      var bonus = BonusCalculator.CalculateBonus(tileData.ItemAmount, _bonus);
      feedBack.GetComponent<EarningFeedBack>().SetEarnings(tileData.ItemAmount,bonus,sprite);
   }
}
