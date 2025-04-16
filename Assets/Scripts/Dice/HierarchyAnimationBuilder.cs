using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class HierarchyAnimationBuilder : EditorWindow
{
    private TextAsset _csvFile;
    private string _animationName = "HierarchyAnimation";

    [MenuItem("Tools/Hierarchy Animation Builder")]
    static void ShowWindow()
    {
        GetWindow<HierarchyAnimationBuilder>("Hierarchy Animation Builder");
    }

    void OnGUI()
    {
        GUILayout.Label("Animation From CSV", EditorStyles.boldLabel);

        _csvFile = (TextAsset)EditorGUILayout.ObjectField("CSV File", _csvFile, typeof(TextAsset), false);
        _animationName = EditorGUILayout.TextField("Animation Name", _animationName);

        if (GUILayout.Button("Create Animation") && _csvFile != null)
        {
            CreateAnimationFromCSV();
        }
    }

    void CreateAnimationFromCSV()
    {
        Dictionary<string, List<Keyframe>> posX = new();
        Dictionary<string, List<Keyframe>> posY = new();
        Dictionary<string, List<Keyframe>> posZ = new();
        Dictionary<string, List<Keyframe>> rotX = new();
        Dictionary<string, List<Keyframe>> rotY = new();
        Dictionary<string, List<Keyframe>> rotZ = new();
        Dictionary<string, List<Keyframe>> rotW = new();

        using (StringReader reader = new StringReader(_csvFile.text))
        {
            reader.ReadLine(); // Skip header

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');

                string name = parts[0];
                float time = float.Parse(parts[1]);

                float px = float.Parse(parts[2]);
                float py = float.Parse(parts[3]);
                float pz = float.Parse(parts[4]);
                float rx = float.Parse(parts[5]);
                float ry = float.Parse(parts[6]);
                float rz = float.Parse(parts[7]);
                float rw = float.Parse(parts[8]);

                if (!posX.ContainsKey(name))
                {
                    posX[name] = new(); posY[name] = new(); posZ[name] = new();
                    rotX[name] = new(); rotY[name] = new(); rotZ[name] = new(); rotW[name] = new();
                }

                posX[name].Add(new Keyframe(time, px));
                posY[name].Add(new Keyframe(time, py));
                posZ[name].Add(new Keyframe(time, pz));
                rotX[name].Add(new Keyframe(time, rx));
                rotY[name].Add(new Keyframe(time, ry));
                rotZ[name].Add(new Keyframe(time, rz));
                rotW[name].Add(new Keyframe(time, rw));
            }
        }

        AnimationClip clip = new AnimationClip();
        clip.legacy = true;

        foreach (var name in posX.Keys)
        {
            string path = name == _csvFile.name ? "" : name;

            clip.SetCurve(path, typeof(Transform), "localPosition.x", new AnimationCurve(posX[name].ToArray()));
            clip.SetCurve(path, typeof(Transform), "localPosition.y", new AnimationCurve(posY[name].ToArray()));
            clip.SetCurve(path, typeof(Transform), "localPosition.z", new AnimationCurve(posZ[name].ToArray()));

            clip.SetCurve(path, typeof(Transform), "localRotation.x", new AnimationCurve(rotX[name].ToArray()));
            clip.SetCurve(path, typeof(Transform), "localRotation.y", new AnimationCurve(rotY[name].ToArray()));
            clip.SetCurve(path, typeof(Transform), "localRotation.z", new AnimationCurve(rotZ[name].ToArray()));
            clip.SetCurve(path, typeof(Transform), "localRotation.w", new AnimationCurve(rotW[name].ToArray()));
        }

        string pathToSave = $"Assets/{_animationName}.anim";
        AssetDatabase.CreateAsset(clip, pathToSave);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("Done", "Animation created at " + pathToSave, "OK");
    }
}
