using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class TransformRecorder : MonoBehaviour
{
    [SerializeField] private string _filePath;
    private bool _isRecording = false;
    private bool _canRecord = false;

    private Dictionary<Transform, List<Vector3>> _positionLogs = new();
    private Dictionary<Transform, List<Quaternion>> _rotationLogs = new();
    private List<Transform> _transforms = new();

    private void Awake()
    {
        StartRecord();
    }

    public void StartRecord()
    {
        _transforms.Clear();
        _positionLogs.Clear();
        _rotationLogs.Clear();

        GetComponentsInChildren(_transforms);

        foreach (var t in _transforms)
        {
            _positionLogs[t] = new List<Vector3>();
            _rotationLogs[t] = new List<Quaternion>();
        }

        _canRecord = true;
        _isRecording = true;
    }

    void FixedUpdate()
    {
        if (_isRecording && _canRecord)
        {
            foreach (var t in _transforms)
            {
                _positionLogs[t].Add(t.localPosition);
                _rotationLogs[t].Add(t.localRotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isRecording = false;
        }
        // Example stopping condition
        if (_canRecord && !_isRecording)
        {
            _isRecording = false;
            _canRecord = false;
            WriteDataToFile();
        }
    }

    void WriteDataToFile()
    {
        _filePath = Application.dataPath;
        string filePath = Path.Combine(_filePath +"/" + gameObject.name + "_multi.csv");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Header
            writer.Write("Frame,");
            for (int i = 0; i < _transforms.Count; i++)
            {   
                if(_transforms[i] == transform) continue;
                writer.Write($"{_transforms[i].name}_PosX,{_transforms[i].name}_PosY,{_transforms[i].name}_PosZ,");
                writer.Write($"{_transforms[i].name}_RotX,{_transforms[i].name}_RotY,{_transforms[i].name}_RotZ,{_transforms[i].name}_RotW");
                if (i < _transforms.Count - 1) writer.Write(",");
            }
            writer.WriteLine();

            int frameCount = _positionLogs[_transforms[0]].Count;
            for (int frame = 0; frame < frameCount; frame++)
            {
                writer.Write($"{frame},");
                for (int i = 0; i < _transforms.Count; i++)
                {
                    Vector3 pos = _positionLogs[_transforms[i]][frame];
                    Quaternion rot = _rotationLogs[_transforms[i]][frame];
                    writer.Write($"{pos.x},{pos.y},{pos.z},{rot.x},{rot.y},{rot.z},{rot.w}");
                    if (i < _transforms.Count - 1) writer.Write(",");
                }
                writer.WriteLine();
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Multi-transform data saved to " + filePath);
    }
}
