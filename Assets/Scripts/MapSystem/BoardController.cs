using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class BoardController : MonoBehaviour
{
   private CharacterMovement _movementInstance;
   [SerializeField] private List<TileData> _tiles = new List<TileData>();
   [SerializeField] private TextAsset _textAsset;
   [SerializeField] private List<Tile> _tileInstances = new List<Tile>();
   private ObjectPooler _objectPooler;
   private bool _movementEnded = true;
   private bool _lastStep;
   public List<TileData> Tiles => _tiles;
   private SpriteTypeHolderSO _spriteTypeHolderSo;
   private int _currentIndex;
   public Action<TileData> OnLastStepCompleted;
   
   public void Initialize(SpriteTypeHolderSO spriteTypeHolderSo )
   {
      _objectPooler = ObjectPooler.Instance;
      _spriteTypeHolderSo = spriteTypeHolderSo;
      _tiles = new List<TileData>(JsonHandler.ReadTileDataListFromJson(AssetDatabase.GetAssetPath(_textAsset)));
      var pos = Vector3.zero;
      foreach (var item in _tiles)
      {  
         pos = item.Coordinate.x * Vector3.forward * 9f;
         var tileInstance = _objectPooler.SpawnFromPool(PoolType.TilePrefab, pos, Quaternion.identity).GetComponent<Tile>();
         var sprite = _spriteTypeHolderSo.Sprites.FirstOrDefault(x => x.ItemType == item.ItemType).SpriteTile;
         var amount = item.ItemAmount;
         tileInstance.transform.position = pos;
         tileInstance.SetTile(item , sprite);
         _tileInstances.Add(tileInstance);
      }
    
   }

   public async void Move(int diceSum)
   {  
      var currentTileInteractible = _tileInstances[_currentIndex].GetComponent<IInteractible>();
      currentTileInteractible.OnReset();
      _lastStep = false;
      while (diceSum > 0)
      {
         diceSum--;
         await Task.Run(() => 
         {
            while (!_movementEnded)
            {
               Task.Yield();
            }
         });
         if (diceSum == 0) _lastStep = true;
         MoveStep();
         await Task.Delay(300);
      }
   }

   private async void InteractWithTile()
   {
      await Task.Delay(800);
      var targetTile = _tileInstances[_currentIndex];
      var interactible = targetTile.GetComponent<IInteractible>();
      interactible.OnInteract();
      if (!_lastStep)
      {
        interactible.OnReset();
      }
     
   }

   public void PlaceCharacterToBoard(CharacterMovement characterMovement)
   {
      _movementInstance = characterMovement;
      _movementInstance.Move(_tileInstances[0].transform.position);
      _tileInstances[0].OnInteract();
      _movementInstance.OnStart += () =>
      {
         InteractWithTile();
      };
      
      _movementInstance.OnComplete += () =>
      {
         _movementEnded = true;
         if (_lastStep)
         {
            Debug.Log("Last Step Completed " + _tiles[_currentIndex].ItemAmount);
            OnLastStepCompleted?.Invoke(_tiles[_currentIndex]);
         }
      };
   }
   private void MoveStep()
   {  
      _movementEnded = false;
      _currentIndex += 1;
      if (_currentIndex >= _tiles.Count)
      {
         _currentIndex = 0;
      }
      var targetTile = _tileInstances[_currentIndex];
      _movementInstance.Move(targetTile.transform.position);
   }
}
