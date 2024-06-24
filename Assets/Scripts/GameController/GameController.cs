using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController 
{
   private CameraController _cameraController;
   private IGuiAnimatonManager _guiAnimationManager;
   private BoardController _boardController;
   private CharacterSelection _characterSelection;
   public GameController(CameraController cameraController , IGuiAnimatonManager guiAnimationManager , CharacterSelection characterSelection , BoardController boardController )
   {
      _boardController = boardController;
      _characterSelection = characterSelection;
      _guiAnimationManager = guiAnimationManager;
      _cameraController = cameraController;
      SwitchToCharacterSelectionMode();
   }

   public void SwitchToCharacterSelectionMode()
   {
      _guiAnimationManager.Show(AnimationNames.CharacterSelection);
   }
   public void SwitchToBoardMode()
   { 
      var character = _characterSelection.CreateCharacterMovementAtPos(Vector3.zero);
      _boardController.PlaceCharacterToBoard(character);
      _guiAnimationManager.Show(AnimationNames.GameHud);
      _cameraController.SwitchCamera(CameraName.Board);
      _cameraController.SetTarget( CameraName.Board,character.transform);
   }
   
}
