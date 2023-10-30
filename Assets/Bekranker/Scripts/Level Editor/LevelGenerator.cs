using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelGenerator : MonoBehaviour
{
    [Space(15)]
    [Header("-----Level Props")]
    [Space(15)]
    [SerializeField] private int Level;
    [SerializeField] private int TexturePixelPerUnit;
    [SerializeField] private Texture2D Puzzle_Sprite;
    public List<Sprite> Puzzle_Sprites = new();
    [SerializeField] private Vector2Int Level_Size;
    public void CreateLevel()
    {
        print("Level Creating");
        var spriteSizeX = Puzzle_Sprite.width / Level_Size.x;
        var spriteSizeY =  Puzzle_Sprite.height / Level_Size.y;
        FolderCreator.CreateEmptyFolder(Level.ToString());
        AssetDatabase.Refresh();
        for (int y = 0; y < Level_Size.y; y++)
        {
            for (int x = 0; x < Level_Size.x; x++)
            {
                var rect = new Rect(x * spriteSizeX, y * spriteSizeY, spriteSizeX, spriteSizeY);
                var sprite = Sprite.Create(Puzzle_Sprite, rect, Vector2.one * 0.5f, TexturePixelPerUnit);
                AssetDatabase.CreateAsset(sprite, $"Assets/Levels/Level{Level}/Photo{x}{y}.asset");
                Puzzle_Sprites.Add(sprite);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Do Level"))
        {
            LevelGenerator levelGenerator = (LevelGenerator)target;
            levelGenerator.CreateLevel();
        }
        
    }
}
