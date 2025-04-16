using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Globalization;

public class MultiTransformAnimationEditor : EditorWindow
{
    private TextAsset csvFile;
    private string animationName = "MultiTransformAnim";

    [MenuItem("Tools/Multi-Transform Animation Creator")]
    public static void ShowWindow()
    {
        GetWindow<MultiTransformAnimationEditor>("Multi-Transform Animation");
    }

    void OnGUI()
    {
        GUILayout.Label("Multi-Transform Animation Creator", EditorStyles.boldLabel);
        csvFile = (TextAsset)EditorGUILayout.ObjectField("CSV File", csvFile, typeof(TextAsset), false);
        animationName = EditorGUILayout.TextField("Animation Name", animationName);

        if (GUILayout.Button("Create Animation") && csvFile != null)
        {
            CreateAnimation();
        }
    }

    void CreateAnimation()
    {
        string[] lines = csvFile.text.Split('\n');
        if (lines.Length < 2) return;

        string[] headers = lines[0].Split(',');
        int transformCount = (headers.Length - 1) / 7;

        List<string> transformNames = new();
        for (int i = 0; i < transformCount; i++)
        {
            string name = headers[1 + i * 7].Split('_')[0];
            transformNames.Add(name);
        }

        var clip = new AnimationClip { legacy = true };
        Dictionary<string, AnimationCurve[]> curves = new();

        foreach (string name in transformNames)
        {
            curves[name + "_posX"] = new AnimationCurve[1] { new AnimationCurve() };
            curves[name + "_posY"] = new AnimationCurve[1] { new AnimationCurve() };
            curves[name + "_posZ"] = new AnimationCurve[1] { new AnimationCurve() };
            curves[name + "_rotX"] = new AnimationCurve[1] { new AnimationCurve() };
            curves[name + "_rotY"] = new AnimationCurve[1] { new AnimationCurve() };
            curves[name + "_rotZ"] = new AnimationCurve[1] { new AnimationCurve() };
            curves[name + "_rotW"] = new AnimationCurve[1] { new AnimationCurve() };
        }

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] values = lines[i].Split(',');
            float time = float.Parse(values[0]) * Time.fixedDeltaTime;

            for (int t = 0; t < transformCount; t++)
            {
                int baseIndex = 1 + t * 7;
                string name = transformNames[t];
                float px = float.Parse(values[baseIndex + 0], CultureInfo.InvariantCulture);
                float py = float.Parse(values[baseIndex + 1], CultureInfo.InvariantCulture);
                float pz = float.Parse(values[baseIndex + 2], CultureInfo.InvariantCulture);
                float rx = float.Parse(values[baseIndex + 3], CultureInfo.InvariantCulture);
                float ry = float.Parse(values[baseIndex + 4], CultureInfo.InvariantCulture);
                float rz = float.Parse(values[baseIndex + 5], CultureInfo.InvariantCulture);
                float rw = float.Parse(values[baseIndex + 6], CultureInfo.InvariantCulture);

                curves[name + "_posX"][0].AddKey(time, px);
                curves[name + "_posY"][0].AddKey(time, py);
                curves[name + "_posZ"][0].AddKey(time, pz);
                curves[name + "_rotX"][0].AddKey(time, rx);
                curves[name + "_rotY"][0].AddKey(time, ry);
                curves[name + "_rotZ"][0].AddKey(time, rz);
                curves[name + "_rotW"][0].AddKey(time, rw);
            }
        }

        foreach (string name in transformNames)
        {
            string path = FindChildPath(name);
            clip.SetCurve(path, typeof(Transform), "localPosition.x", curves[name + "_posX"][0]);
            clip.SetCurve(path, typeof(Transform), "localPosition.y", curves[name + "_posY"][0]);
            clip.SetCurve(path, typeof(Transform), "localPosition.z", curves[name + "_posZ"][0]);
            clip.SetCurve(path, typeof(Transform), "localRotation.x", curves[name + "_rotX"][0]);
            clip.SetCurve(path, typeof(Transform), "localRotation.y", curves[name + "_rotY"][0]);
            clip.SetCurve(path, typeof(Transform), "localRotation.z", curves[name + "_rotZ"][0]);
            clip.SetCurve(path, typeof(Transform), "localRotation.w", curves[name + "_rotW"][0]);
        }

        string pathSave = $"Assets/{animationName}.anim";
        AssetDatabase.CreateAsset(clip, pathSave);
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("Done", "Animation saved to " + pathSave, "OK");
    }

    string FindChildPath(string name)
    {
        GameObject root = Selection.activeGameObject;
        if (root == null) return name;

        Transform target = root.transform.Find(name);
        return AnimationUtility.CalculateTransformPath(target ?? root.transform, root.transform);
    }
}
