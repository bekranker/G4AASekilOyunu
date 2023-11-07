using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    [Space(15)]
    [Header("-----Level Props")]
    [Space(15)]
    [SerializeField] private int _Level;
    [SerializeField] private int _TexturePixelPerUnit;
    [SerializeField] private Texture2D _Puzzle_Sprite;
    [SerializeField] private Vector2Int _Level_Size;

    [Space(15)]
    [Header("-----Level Objects")]
    [Space(15)]
    [SerializeField] private GameObject _Parent;

    private Color _firstColor, _secondColor;
    private string _firstHex, _secondHex;

    public void CreateLevel()
    {
        _Parent.name = "Level" + _Level.ToString();
        var spriteSizeX = _Puzzle_Sprite.width / _Level_Size.x;
        var spriteSizeY =  _Puzzle_Sprite.height / _Level_Size.y;
        var sideSize = 5.1145f / _Level_Size.x;
        var unitPer2 = sideSize / 2;
        FolderCreator.CreateEmptyFolder(_Level.ToString());
        AssetDatabase.Refresh();
        for (int y = 0; y < _Level_Size.y; y++)
        {
            for (int x = 0; x < _Level_Size.x; x++)
            {
                GameObject piece = new GameObject();
                piece.name = "Piece" + x + y;
                piece.transform.SetParent(_Parent.transform);
                piece.transform.tag = "Line";
                piece.transform.position = new Vector3((sideSize * x - unitPer2) - (unitPer2 / _Level_Size.x), sideSize * y - unitPer2);
                var rect = new Rect(x * spriteSizeX, y * spriteSizeY, spriteSizeX, spriteSizeY);
                var sprite = Sprite.Create(_Puzzle_Sprite, rect, Vector2.one * 0.5f, _TexturePixelPerUnit);
                piece.AddComponent<SpriteRenderer>().sprite = sprite;
                AssetDatabase.CreateAsset(sprite, $"Assets/Levels/Level{_Level}/Photo{x}{y}.asset");
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void ResetLevel(){
        List<GameObject> _pieces_ = GameObject.FindGameObjectsWithTag("Line").ToList();
        _pieces_?.ForEach((__piece__)=>{DestroyImmediate(__piece__);});
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
        if(GUILayout.Button("Reset"))
        {
            LevelGenerator levelGenerator = (LevelGenerator)target;
            levelGenerator.ResetLevel();
        }
    }
}