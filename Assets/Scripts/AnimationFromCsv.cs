using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class AnimationFromCsv : MonoBehaviour
{
    [MenuItem("Tools/Generate Animation From CSV")]
    public static void GenerateAnimation()
    {
        string path = EditorUtility.OpenFilePanel("Select CSV", Application.dataPath, "csv");
        if (string.IsNullOrEmpty(path)) return;

        Dictionary<string, List<Vector3>> positionData = new();
        Dictionary<string, List<Quaternion>> rotationData = new();
        int frameCount = 0;

        // Step 1: Read CSV
        var lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++) // Skip header
        {
            var parts = lines[i].Split(',');
            string name = parts[0];
            Vector3 pos = new(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
            Quaternion rot = new(float.Parse(parts[4]), float.Parse(parts[5]), float.Parse(parts[6]), float.Parse(parts[7]));

            if (!positionData.ContainsKey(name))
            {
                positionData[name] = new List<Vector3>();
                rotationData[name] = new List<Quaternion>();
            }

            positionData[name].Add(pos);
            rotationData[name].Add(rot);

            frameCount = Mathf.Max(frameCount, positionData[name].Count);
        }

        float frameRate = 50f; // Match FixedUpdate if needed
        AnimationClip clip = new AnimationClip();
        clip.frameRate = frameRate;

        // Step 2: Build curves per object
        foreach (var kvp in positionData)
        {
            string objName = kvp.Key;
            List<Vector3> positions = kvp.Value;
            List<Quaternion> rotations = rotationData[objName];

            AnimationCurve posX = new(), posY = new(), posZ = new();
            AnimationCurve rotX = new(), rotY = new(), rotZ = new(), rotW = new();

            for (int i = 0; i < positions.Count; i++)
            {
                float time = i / frameRate;
                Vector3 pos = positions[i];
                Quaternion rot = rotations[i];

                posX.AddKey(time, pos.x);
                posY.AddKey(time, pos.y);
                posZ.AddKey(time, pos.z);

                rotX.AddKey(time, rot.x);
                rotY.AddKey(time, rot.y);
                rotZ.AddKey(time, rot.z);
                rotW.AddKey(time, rot.w);
            }

            string _path = objName; // assumes GameObject names are unique and match hierarchy

            clip.SetCurve(_path, typeof(Transform), "localPosition.x", posX);
            clip.SetCurve(_path, typeof(Transform), "localPosition.y", posY);
            clip.SetCurve(_path, typeof(Transform), "localPosition.z", posZ);

            clip.SetCurve(_path, typeof(Transform), "localRotation.x", rotX);
            clip.SetCurve(_path, typeof(Transform), "localRotation.y", rotY);
            clip.SetCurve(_path, typeof(Transform), "localRotation.z", rotZ);
            clip.SetCurve(_path, typeof(Transform), "localRotation.w", rotW);
        }

        // Step 3: Save AnimationClip
        string clipPath = "Assets/GeneratedAnimation.anim";
        AssetDatabase.CreateAsset(clip, clipPath);
        AssetDatabase.SaveAssets();
        Debug.Log("AnimationClip saved to: " + clipPath);
    }
}
