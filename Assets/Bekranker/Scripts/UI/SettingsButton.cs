using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SettingsButton : MonoBehaviour, ICommand
{
    [SerializeField] private ButtonEffect _buttonEffect;
    private Transform _transform;
    private Sequence _sequence { get; set; }

    void OnEnable()
    {

    }
    void OnDisable()
    {

    }

    
    public void Execute()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(PointUpTweenHandler(Vector2.one).OnComplete(()=> 
        {
            _buttonEffect._canClick = true;
            _sequence.Kill();
        }));
    }
    private Tween PointUpTweenHandler(Vector3 targetScale)
    {
        StaticTweenFunctions.MyRotationHandler(_transform, new Vector3(_transform.rotation.x, _transform.rotation.y, _transform.rotation.z + 180), .35f);
        return StaticTweenFunctions.MyScaleHandler(_transform, targetScale, 0.35f);
    }
    public void Start()
    {
        _transform = transform;
        _buttonEffect.StoredComman = this;
    }
}

