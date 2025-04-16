using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class DiceRecorder : MonoBehaviour
{
    private Dictionary<Transform , List<Vector3>> _positonsDictionary = new Dictionary<Transform , List<Vector3>>();
    private Dictionary<Transform,List<Quaternion>> _rotationsDictionary = new Dictionary<Transform , List<Quaternion>>();
    private bool isRecording = false;
    private List<Transform> _transforms = new List<Transform>();
    [SerializeField] private string _filePath;
    private bool _canRecord;

    private void Awake()
    {
        StartRecord();
    }

    public void StartRecord()
    {
        _canRecord = true;
        isRecording = true;
        _transforms = new List<Transform>();
        foreach (Transform t in transform)
        {   
            if(t == transform) continue;
            _positonsDictionary.Add(t,new List<Vector3>());
            _rotationsDictionary.Add(t,new List<Quaternion>());
            _transforms.Add(t);
            Debug.LogError(t.gameObject.name);
        }
    }

    void FixedUpdate()
    {
        if (isRecording && _canRecord)
        {
            foreach (Transform t in _transforms)
            {
                Debug.LogError(t.gameObject.name);
                _positonsDictionary[t].Add(t.localPosition);
                _rotationsDictionary[t].Add(t.localRotation);
            }
            //positions.Add(diceRigidbody.position);
            //rotations.Add(diceRigidbody.rotation);



            // Stop recording after a certain condition, e.g., dice comes to rest
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isRecording = false;
                _canRecord = false;
                WriteDataToFile();
            }
        }

        void WriteDataToFile()
        {
            string filePath = Path.Combine(_filePath + gameObject.name + ".csv");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in _positonsDictionary)
                {
                    string name = item.Key.name;
                    writer.WriteLine($"# Name: {name}");
                    writer.WriteLine("# PositionX.PositionY.PositionZ.RotationX.RotationY.RotationZ.RotationW");

                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        Vector3 pos = item.Value[i];
                        Quaternion rot = _rotationsDictionary[item.Key][i];
                        writer.WriteLine($"{pos.x}.{pos.y}.{pos.z}.{rot.x}.{rot.y}.{rot.z}.{rot.w}");
                    }

                    writer.WriteLine(); // Empty line for separation
                }
            }

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
            Debug.Log("Data written to " + filePath);
        }
    }
}

