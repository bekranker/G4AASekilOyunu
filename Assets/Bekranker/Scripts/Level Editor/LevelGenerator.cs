using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Mathematics;


public class LevelGenerator : MonoBehaviour
{
    [Space(15)]
    [Header("-----Level Props")]
    [Space(15)]
    [SerializeField] private int _Level;
    [SerializeField] private int _TexturePixelPerUnit;
    [SerializeField] private Texture2D _Puzzle_Sprite;
    public List<Sprite> Puzzle_Sprites = new();
    [SerializeField] private Vector2Int _Level_Size;


    [Space(15)]
    [Header("-----Level Props")]
    [Space(15)]
    public GameObject Parent;
    [Space(15)]
    [Header("-----Level Objects")]
    [Space(15)]
    [SerializeField] private GameObject _Parent;

    public void CreateLevel()
    {
        print("Level Creating");
        Parent.name = "Level" + _Level.ToString();
        var spriteSizeX = _Puzzle_Sprite.width / _Level_Size.x;
        var spriteSizeY =  _Puzzle_Sprite.height / _Level_Size.y;
        FolderCreator.CreateEmptyFolder(_Level.ToString());
        AssetDatabase.Refresh();
        for (int y = 0; y < _Level_Size.y; y++)
        {
            for (int x = 0; x < _Level_Size.x; x++)
            {
                //GameObject piece = new GameObject();
                //piece.name = "Piece" + x + y;
                var rect = new Rect(x * spriteSizeX, y * spriteSizeY, spriteSizeX, spriteSizeY);
                var sprite = Sprite.Create(_Puzzle_Sprite, rect, Vector2.one * 0.5f, _TexturePixelPerUnit);
                //piece.AddComponent<SpriteRenderer>().sprite = sprite;
                AssetDatabase.CreateAsset(sprite, $"Assets/Levels/Level{_Level}/Photo{x}{y}.asset");
                Puzzle_Sprites.Add(sprite);

                //piece.transform.position = new Vector3(x *_Level_Size.x,y*_Level_Size.x);
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