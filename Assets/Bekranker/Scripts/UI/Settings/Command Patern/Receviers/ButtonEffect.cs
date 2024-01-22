using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static float _speed = 0.1f;
    private static Vector2 _downScale = new Vector2(0.8f, 0.8f);
    private List<Vector3> _rotations = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 180)
    };
    private int _index;

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
        TurnAction();
        StaticTweenFunctions.MyScaleHandler(_transform, _downScale, _speed);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(!_canClick) return;
        _canClick = false;
        ExecuteEffects();
    }
    private void ExecuteEffects(){

        OnUp?.Invoke();
        StoredComman?.Execute();
    }
    public void TurnAction()
    {
        _index = (_index == 1) ? 0 : _index + 1;
        //StaticTweenFunctions.MyRotationHandler(_transform, _rotations[_index], _speed);
    }
}