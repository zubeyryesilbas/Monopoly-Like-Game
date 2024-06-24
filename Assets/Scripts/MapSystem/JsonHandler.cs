using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonHandler 
{
    public static void WriteTileDataListToJson(List<TileData> tileDataList, string filePath)
    {
        var json = JsonUtility.ToJson(new TileDataListWrapper { TileDataList = tileDataList }, true);
        File.WriteAllText(filePath, json);
    }
    
    public static List<TileData> ReadTileDataListFromJson(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }

        var json = File.ReadAllText(filePath);
        var wrapper = JsonUtility.FromJson<TileDataListWrapper>(json);
        return wrapper.TileDataList;
    }
}
