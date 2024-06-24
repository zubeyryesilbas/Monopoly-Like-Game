using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardController))]
public class BoardControllerEditor : Editor
{
    private string _filePath = "Assets/tiles.json";
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _filePath = EditorGUILayout.TextField("Save Path", _filePath);
        BoardController boardController = (BoardController)target;

        if (GUILayout.Button("Save Board"))
        {
            JsonHandler.WriteTileDataListToJson(boardController.Tiles, _filePath);
            AssetDatabase.Refresh();
        }
    }
}