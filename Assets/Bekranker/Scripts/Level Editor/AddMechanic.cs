using UnityEditor;
using UnityEngine;

public enum Mechanics
{
    AffectingLines,
    Swide,
}

public class AddMechanic : EditorWindow
{
    private GameObject _selectedObject, _selectedObject1, _wasSelectedObject, _wasSelectedObject1;
    private Mechanics _selectedEnum;
    private GameObject _selectedObjectOne, _selectedObjectTwo;

    [MenuItem("Mert Buraya Tikla/Simdi Buraya Tikla")]
    public static void ShowWindow()
    {
        GetWindow<AddMechanic>("Evet Mert Burasi");
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        Selection.selectionChanged += OnSelectionChanged;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
        Selection.selectionChanged -= OnSelectionChanged;
    }

    private void OnGUI()
    {
        if(_selectedObjectOne != null){
            GUILayout.Label("Sol Tiklanan Nesne: " + _selectedObjectOne.name);
        }
        if(_selectedObjectTwo != null){
            GUILayout.Label("Sag Tiklanan Nesne: " + _selectedObjectTwo.name);
        }

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Mekanik secimi:");

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
                    default:
                        break;
                }
                GUILayout.Label("AFERIM MERT");
            }
        }
    }
    private void OnSceneGUI(SceneView sceneView)
    {
        Event currentEvent = Event.current;
        if(currentEvent.type == EventType.MouseDown)
        {
            if(currentEvent.button == 1)
            {
                GameObject clickedObject = HandleUtility.PickGameObject(Event.current.mousePosition, true);
                if(clickedObject != null){
                    _selectedObjectTwo = clickedObject;
                    
                }
                Repaint();
            }
            if(currentEvent.button == 0)
            {
                GameObject clickedObject = Selection.activeGameObject;
                if(clickedObject != null){
                    _selectedObjectOne = clickedObject;
                    
                }
                Repaint();
            }
        }
    }
    private void OnSelectionChanged()
    {
        Repaint();
    }
}