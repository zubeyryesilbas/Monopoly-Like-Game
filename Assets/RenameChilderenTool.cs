using UnityEditor;
using UnityEngine;

public class RenameChildrenTool : EditorWindow
{
    private GameObject _parent;

    [MenuItem("Tools/Rename Children by Index")]
    public static void ShowWindow()
    {
        GetWindow<RenameChildrenTool>("Rename Children");
    }

    private void OnGUI()
    {
        GUILayout.Label("Rename Children by Sibling Index", EditorStyles.boldLabel);

        _parent = (GameObject)EditorGUILayout.ObjectField("Parent Object", _parent, typeof(GameObject), true);

        if (_parent == null)
        {
            EditorGUILayout.HelpBox("Please assign a GameObject with children.", MessageType.Info);
            return;
        }

        if (GUILayout.Button("Rename Children"))
        {
            RenameChildren();
        }
    }

    private void RenameChildren()
    {
        if (_parent == null) return;

        int childCount = _parent.transform.childCount;

        Undo.RegisterFullObjectHierarchyUndo(_parent, "Rename Children");

        for (int i = 0; i < childCount; i++)
        {
            Transform child = _parent.transform.GetChild(i);
            child.name = $"Child_{i}";
        }

        EditorUtility.SetDirty(_parent);
        Debug.Log($"Renamed {childCount} children.");
    }
}