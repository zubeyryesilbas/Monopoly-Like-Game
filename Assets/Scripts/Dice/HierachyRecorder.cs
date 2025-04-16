using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class HierarchyRecorder : MonoBehaviour
{
    private Dictionary<Transform, List<Vector3>> _localPositions = new Dictionary<Transform, List<Vector3>>();
    private Dictionary<Transform, List<Quaternion>> _localRotations = new Dictionary<Transform, List<Quaternion>>();
   [SerializeField] private List<Transform> _transforms = new List<Transform>();
    private bool _isRecording = false;
    [SerializeField] private string _fileName = "HierarchyRecording.csv";

    private void Awake()
    {
        StartRecording();
    }

    public void StartRecording()
    {
        _localPositions.Clear();
        _localRotations.Clear();

      
        foreach (Transform t in _transforms)
        {
            Debug.LogError(""+t.name); 
            _localPositions.Add(t , new List<Vector3>());
            _localRotations.Add(t , new List<Quaternion>());
        }
        
        _isRecording = true;
    }

    void FixedUpdate()
    {
        if (!_isRecording) return;

        foreach (var t in _transforms)
        {   
            _localPositions[t].Add(t.localPosition);
            _localRotations[t].Add(t.localRotation);
        }

        // Stop when velocity is almost zero
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isRecording = false;
            WriteDataToFile();
        }
        
    }
    void WriteDataToFile()
    {
        string filePath = Path.Combine(Application.dataPath + "/test" + gameObject.name + ".csv");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Name,PositionX,PositionY,PositionZ,RotationX,RotationY,RotationZ,RotationW");

            foreach (var item in _transforms)
            {
                List<Vector3> positions = _localPositions[item];
                List<Quaternion> rotations = _localRotations[item];

                for (int i = 0; i < positions.Count; i++) // FIX: Use positions.Count
                {
                    Vector3 pos = positions[i];
                    Quaternion rot = rotations[i];
                    writer.WriteLine($"{item.name},{pos.x},{pos.y},{pos.z},{rot.x},{rot.y},{rot.z},{rot.w}");
                }
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("Data written to " + filePath);
    }

    
}
