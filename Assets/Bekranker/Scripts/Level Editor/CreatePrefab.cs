using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public static class CreatePrefab
{
    public static void ToPrefab(GameObject SaveTheObject)
    {
        string cleanSaveName = string.Concat(SaveTheObject.name.Split(Path.GetInvalidFileNameChars()));
        FolderCreator.CreateEmptyFolder(cleanSaveName, "Assets/Levels/LevelsPrefabs");

        string localPath = $"Assets/Levels/LevelsPrefabs/{cleanSaveName}" + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        
        PrefabUtility.SaveAsPrefabAsset(SaveTheObject, localPath);
    }
}
