using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCamera _characterSelectionCamera, _boardCamera;

   public void SetTarget(CameraName cameraName, Transform target)
   {
      switch (cameraName)
      {
         case CameraName.Board:
            _boardCamera.Follow = target;
            break;
         case CameraName.CharacterSelection:
            _characterSelectionCamera.Follow = target;
            break;
      }
   }
   public void SwitchCamera(CameraName cameraName)
   {
      switch (cameraName)
      {
         case CameraName.Board:
            _characterSelectionCamera.gameObject.SetActive(false);
            _boardCamera.gameObject.SetActive(true);
            break;
         case CameraName.CharacterSelection:
            _characterSelectionCamera.gameObject.SetActive(true);
            _boardCamera.gameObject.SetActive(false);
            break;
      }
   }
}

public enum CameraName
{
   CharacterSelection , Board
}
