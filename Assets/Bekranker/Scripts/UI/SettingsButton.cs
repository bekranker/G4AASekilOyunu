using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SettingsButton :  AbstractButton, ICommand
{
    [SerializeField] private ButtonEffect _buttonEffect;
    [SerializeField] private List<AbstractButton> _buttons;
    private RectTransform _transform;
    private Sequence _sequence { get; set; }




    void OnEnable()
    {
        _buttonEffect.OnUp += SlideHandler;
    }
    void OnDisable()
    {
        _buttonEffect.OnUp -= SlideHandler;
    }
    public void Start()
    {
        _transform = GetComponent<RectTransform>();
        _buttonEffect.StoredComman = this;
    }
    public void Execute()
    {
        EffectHandler();
    }
    public override void SlideHandler()
    {
        _buttons.ForEach((button)=>{button.SlideHandler();});
    }
    public override void EffectHandler()
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
        StaticTweenFunctions.MyRotationHandler(_transform, Vector3.one * 180, .35f);
        return StaticTweenFunctions.MyScaleHandler(_transform, targetScale, 0.35f);
    }
}

