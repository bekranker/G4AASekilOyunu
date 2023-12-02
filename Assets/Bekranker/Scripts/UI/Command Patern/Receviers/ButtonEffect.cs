using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static float _speed = 0.35f;
    private static Vector2 _downScale = new Vector2(0.8f, 0.8f);


    public delegate void UpAction();
    public event UpAction OnUp, OnUpFinished;


    private Transform _transform { get; set; }
    private Sequence _sequence { get; set; }
    public bool _canClick { get; set; }
    public ICommand StoredComman;

    void Awake()
    {
        _canClick = true;
    }

    private void Start()
    {
        _transform = transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_canClick) return;
        StaticTweenFunctions.MyRandomRotationHandler(_transform, new Vector3(_transform.rotation.x, _transform.rotation.y, _transform.rotation.z + 180), _speed);
        StaticTweenFunctions.MyScaleHandler(_transform, _downScale, _speed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!_canClick) return;
        _canClick = false;
        OnUp?.Invoke();
        StoredComman?.Execute();
    }
}