using UnityEditor;
using UnityEngine;
using System.IO;
public static class FolderCreator
{
    public static void CreateEmptyFolder(string name, string path = "Assets/Levels/")
        {
            string _path = $"{path}/Level{name}";

            if (!AssetDatabase.IsValidFolder(_path))
            {
                Directory.CreateDirectory(_path);
                AssetDatabase.Refresh();
                return;
            }
            Debug.LogWarning("Bu isimde bir dosya var");
        }
}
