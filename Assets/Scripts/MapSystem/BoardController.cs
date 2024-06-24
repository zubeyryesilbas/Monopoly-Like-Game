using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Mathematics;
using UnityEditor;

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
    private CancellationTokenSource _cts;
    
    public void Initialize(SpriteTypeHolderSO spriteTypeHolderSo)
    {
        _objectPooler = ObjectPooler.Instance;
        _spriteTypeHolderSo = spriteTypeHolderSo;
        _tiles = new List<TileData>(JsonHandler.ReadTileDataListFromJson(AssetDatabase.GetAssetPath(_textAsset)));
        var pos = Vector3.zero;
        foreach (var item in _tiles)
        {
            pos = item.Coordinate.x * Vector3.forward * 32f;
            var tileInstance = _objectPooler.SpawnFromPool(PoolType.TilePrefab, pos, Quaternion.identity).GetComponent<Tile>();
            var sprite = _spriteTypeHolderSo.Sprites.FirstOrDefault(x => x.ItemType == item.ItemType).SpriteTile;
            var amount = item.ItemAmount;
            tileInstance.transform.position = pos;
            tileInstance.SetTile(item, sprite);
            _tileInstances.Add(tileInstance);
        }
    }
    
    public async void Move(int diceSum)
    {
        // Cancel any ongoing movement
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        var currentTileInteractible = _tileInstances[_currentIndex].GetComponent<IInteractible>();
        currentTileInteractible.OnReset();
        _lastStep = false;
        
        try
        {
            while (diceSum > 0)
            {
                diceSum--;
                await Task.Run(() =>
                {
                    while (!_movementEnded)
                    {
                        Task.Yield();
                    }
                }, token);
                if (diceSum == 0) _lastStep = true;

                var stepCount = 1;
                if (_currentIndex == _tileInstances.Count-1)
                {   
                    stepCount = Mathf.RoundToInt((_currentIndex + 1)/5);
                    
                     if(stepCount >3)
                    {
                        stepCount = 3;
                    }
                     else if(stepCount <1)
                     {
                         stepCount = 1;
                     }
                }
                MoveStep(stepCount);
                await Task.Delay(300, token);
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Dice movement was cancelled.");
        }
    }

    private async void InteractWithTile()
    {
        await Task.Delay(800);
        var targetTile = _tileInstances[_currentIndex];
        if (targetTile != null)
        {
            var interactible = targetTile.GetComponent<IInteractible>();
            interactible.OnInteract();
            if (!_lastStep)
            {
                interactible.OnReset();
            }
        }
    }

    public void PlaceCharacterToBoard(CharacterMovement characterMovement)
    {
        _movementInstance = characterMovement;
        _movementInstance.Move(_tileInstances[0].transform.position, 1);
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
                EventManager.Instance.TriggerEvent(EventConstants.BoardEvents.ONLASTSTEPCOMPLETED, _tiles[_currentIndex]);
            }
        };
    }

    private void MoveStep(int stepCount)
    {
        _movementEnded = false;
        _currentIndex += 1;
        if (_currentIndex >= _tiles.Count)
        {
            _currentIndex = 0;
        }
        var targetTile = _tileInstances[_currentIndex];
        _movementInstance.Move(targetTile.transform.position, stepCount);
    }
}
