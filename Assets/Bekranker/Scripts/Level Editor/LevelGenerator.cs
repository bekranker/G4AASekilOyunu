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
    [SerializeField] private int _level;
    [SerializeField] private int _clickCount;
    [SerializeField] private int _texturePixelPerUnit;
    [SerializeField] private Texture2D _puzzle_Sprite;
    [SerializeField] private Vector2Int _levelSize;


    [Space(15)]
    [Header("-----Level Objects")]
    [Space(15)]
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private GameObject _parent;
    [SerializeField] private List<string> _hexs;
    [SerializeField] private Material _backgroundMaterial;
    [SerializeField] private LevelManager _levelManager;

    private Color _firstColor, _secondColor;
    private string _firstHex, _secondHex;
    private static int _backgroundColorOne = Shader.PropertyToID("_Color_1");
    private static int _backgroundColorTwo = Shader.PropertyToID("_Color_2");

    public void CreateLevel()
    {
        _parent.name = "Level " + _level.ToString();
        _levelPrefab.name = "LevelPrefab " + _level.ToString();
        var spriteSizeX = _puzzle_Sprite.width / _levelSize.x;
        var spriteSizeY =  _puzzle_Sprite.height / _levelSize.y;
        var sideSize = 5.1145f / _levelSize.x;
        FolderCreator.CreateEmptyFolder(_level.ToString());
        AssetDatabase.Refresh();
        for (int y = 0; y < _levelSize.y; y++)
        {
            for (int x = 0; x < _levelSize.x; x++)
            {
                GameObject piece = new GameObject();
                piece.name = "Piece" + x + y;
                piece.transform.SetParent(_parent.transform);
                piece.layer = 6;
                piece.transform.tag = "Line";
                piece.transform.position = new Vector3((sideSize * x) - sideSize, (sideSize * y)- sideSize);
                var rect = new Rect(x * spriteSizeX, y * spriteSizeY, spriteSizeX, spriteSizeY);
                var sprite = Sprite.Create(_puzzle_Sprite, rect, Vector2.one * 0.5f, _texturePixelPerUnit);
                piece.AddComponent<SpriteRenderer>().sprite = sprite;
                piece.AddComponent<BoxCollider2D>();
                piece.AddComponent<Piece>();
                AssetDatabase.CreateAsset(sprite, $"Assets/Levels/Level{_level}/Photo{x}{y}.asset");
            }
        }
        SettingLevelColor();
        string hexOne = "#";
        string hexTwo = "#";
        for (int i = 0; i < 6; i++)
        {
            hexOne += _hexs[_level - 1][i];
        }
        for (int i = 7; i < 13; i++)
        {
            hexTwo += _hexs[_level - 1][i];
        }
        ParselingFirstColor(hexOne,  _backgroundColorOne);
        ParselingSecondColor(hexTwo,  _backgroundColorTwo);
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private void ParselingFirstColor(string hexString,  int staticValue){
        if (ColorUtility.TryParseHtmlString(hexString, out Color a))
        {
            _levelManager.ColorOne = a;
            _backgroundMaterial.SetColor(staticValue, a);
        }
    }
    private void ParselingSecondColor(string hexString, int staticValue){
        if (ColorUtility.TryParseHtmlString(hexString, out Color a))
        {
            _levelManager.ColorTwo = a;
            _backgroundMaterial.SetColor(staticValue, a);
        }
    }
    public void Save() => CreatePrefab.ToPrefab(_levelPrefab);
    public void SettingLevelColor()
    {
        string colorPath = "Assets/COLORS.txt";
        _hexs = GetTXT.GetTexts(colorPath);
    }
    public void ResetLevel(){
        List<GameObject> _pieces_ = GameObject.FindGameObjectsWithTag("Line").ToList();
        _pieces_?.ForEach((__piece__)=>{DestroyImmediate(__piece__);});
        _hexs.Clear();
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