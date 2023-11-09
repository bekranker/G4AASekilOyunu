using UnityEditor;
using UnityEngine;

public enum Mechanics
{
    AffectingLines,
    Swide,
    FlipHorizontal
}

[CustomEditor(typeof(GameObject))]
public class AddMechanic : EditorWindow
{
    private Mechanics _selectedEnum;
    private string _chosenOneName;
    private string _chooseTwoName;
    private GameObject _selectedObjectOne, _selectedObjectTwo;

    [MenuItem("Window/My Custom Window")]
    public static void ShowWindow()
    {
        GetWindow<AddMechanic>("Custom Window");
    }

    private void OnGUI()
    {
        
        GUILayout.Label("Sag Tiklanan Nesne: " + _chooseTwoName);

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Enum Secimi:");

        _selectedEnum = (Mechanics)EditorGUILayout.EnumPopup(_selectedEnum);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Add Mechanic"))
        {
            if (_selectedObjectOne != null && _selectedObjectTwo != null)
            {
                switch (_selectedEnum)
                {
                    case Mechanics.AffectingLines:
                        _selectedObjectOne.AddComponent<Piece_AffectingLines>().AffectedLine = _selectedObjectTwo.GetComponent<Piece>();
                        break;
                    case Mechanics.Swide:
                        _selectedObjectOne.AddComponent<Piece_GridScrollManager>();
                        _selectedObjectTwo.AddComponent<Piece_GridScrollManager>();
                        break;
                    case Mechanics.FlipHorizontal:
                        _selectedObjectOne.AddComponent<Piece_FlipHorizontal>();
                        _selectedObjectTwo.AddComponent<Piece_FlipHorizontal>();
                        break;
                    default:
                        break;
                }
                Debug.Log(_chosenOneName + " " + _chooseTwoName);
            }
            else
            {
                Debug.LogError("Secilen nesneler eksik. Lutfen sol ve sag tiklamayla iki nesneyi secin.");
            }
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.MouseDown)
        {
            if (currentEvent.button == 0)
            {
                _selectedObjectOne = Selection.activeGameObject;
                if (_selectedObjectOne != null)
                {
                    _chosenOneName = _selectedObjectOne.name;
                    GUILayout.Label("Sol Tiklanan Nesne: " + _chosenOneName);
                }
            }
            else if (currentEvent.button == 1)
            {
                _selectedObjectTwo = HandleUtility.PickGameObject(Event.current.mousePosition, true);
                GetWindow<AddMechanic>("Custom Window");
            }
        }
        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 1)
    {
        currentEvent.Use();
    }
        Repaint();
    }
}