using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _panelTransform;

    private Rect _currentdafeare = new();
    private ScreenOrientation _currentOrientation = ScreenOrientation.AutoRotation;



    void Start()
    {
        _currentOrientation = Screen.orientation;
        _currentdafeare = Screen.safeArea;
        ApplySafeAre();
    }
    private void ApplySafeAre(){
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= _canvas.pixelRect.width;
        anchorMin.y /= _canvas.pixelRect.height;

        anchorMax.x /= _canvas.pixelRect.width;
        anchorMax.y /= _canvas.pixelRect.height;
        _panelTransform.anchorMin = anchorMin;
        _panelTransform.anchorMax = anchorMax;

        _currentOrientation = Screen.orientation;
        _currentdafeare = Screen.safeArea;
    }
}
