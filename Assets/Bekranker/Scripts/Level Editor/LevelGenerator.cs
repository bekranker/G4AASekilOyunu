using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

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
    [SerializeField] private GameObject _LevelPrefab;
    [SerializeField] private GameObject _Parent;
    [SerializeField] private List<string> _Hexs;
    [SerializeField] private Material _BackgroundMaterial;
    [SerializeField] private LevelManager _LevelManager;

    private Color _firstColor, _secondColor;
    private string _firstHex, _secondHex;
    private static int _backgroundColorOne = Shader.PropertyToID("_Color_1");
    private static int _backgroundColorTwo = Shader.PropertyToID("_Color_2");

    public void CreateLevel()
    {
        _Parent.name = "Level " + _Level.ToString();
        _LevelPrefab.name = "LevelPrefab " + _Level.ToString();
        var spriteSizeX = _Puzzle_Sprite.width / _Level_Size.x;
        var spriteSizeY =  _Puzzle_Sprite.height / _Level_Size.y;
        var sideSize = 5.1145f / _Level_Size.x;
        FolderCreator.CreateEmptyFolder(_Level.ToString());
        AssetDatabase.Refresh();
        for (int y = 0; y < _Level_Size.y; y++)
        {
            for (int x = 0; x < _Level_Size.x; x++)
            {
                GameObject piece = new GameObject();
                piece.name = "Piece" + x + y;
                piece.transform.SetParent(_Parent.transform);
                piece.layer = 6;
                piece.transform.tag = "Line";
                piece.transform.position = new Vector3((sideSize * x) - sideSize, (sideSize * y)- sideSize);
                var rect = new Rect(x * spriteSizeX, y * spriteSizeY, spriteSizeX, spriteSizeY);
                var sprite = Sprite.Create(_Puzzle_Sprite, rect, Vector2.one * 0.5f, _TexturePixelPerUnit);
                piece.AddComponent<SpriteRenderer>().sprite = sprite;
                piece.AddComponent<BoxCollider2D>();
                piece.AddComponent<Piece>();
                AssetDatabase.CreateAsset(sprite, $"Assets/Levels/Level{_Level}/Photo{x}{y}.asset");
            }
        }
        SettingLevelColor();
        string hexOne = "#";
        string hexTwo = "#";
        for (int i = 0; i < 6; i++)
        {
            hexOne += _Hexs[_Level - 1][i];
        }
        for (int i = 7; i < 13; i++)
        {
            hexTwo += _Hexs[_Level - 1][i];
        }
        if (ColorUtility.TryParseHtmlString(hexOne, out _firstColor))
        {
            _BackgroundMaterial.SetColor(_backgroundColorOne, _firstColor);
        }
        if (ColorUtility.TryParseHtmlString(hexOne, out _secondColor))
        {
            _BackgroundMaterial.SetColor(_backgroundColorTwo, _secondColor);
        }
        _LevelManager.ColorOne = _firstColor;
        _LevelManager.ColorTwo = _secondColor;

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    public void Save() => CreatePrefab.ToPrefab(_LevelPrefab);
    public void SettingLevelColor()
    {
        string colorPath = "Assets/COLORS.txt";
        _Hexs = GetTXT.GetTexts(colorPath);
    }
    public void ResetLevel(){
        List<GameObject> _pieces_ = GameObject.FindGameObjectsWithTag("Line").ToList();
        _pieces_?.ForEach((__piece__)=>{DestroyImmediate(__piece__);});
        _Hexs.Clear();
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
            levelGenerator().CreateLevel();
        }
        if(GUILayout.Button("Reset"))
        {
            levelGenerator().ResetLevel();
        }
        if(GUILayout.Button("Save")){
            levelGenerator().Save();
        }
    }

    private LevelGenerator levelGenerator()=> (LevelGenerator)target;
}