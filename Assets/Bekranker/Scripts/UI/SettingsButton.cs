using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class SettingsButton :  AbstractButton, ICommand
{
    [SerializeField] private ButtonEffect _buttonEffect;
    [SerializeField] private List<AbstractButton> _buttons;
    private List<Vector3> _rotations = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 180)
    };
    private int _index;
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
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].SlideHandler();
        }
    }
    public override void EffectHandler()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(PointUpTweenHandler(Vector2.one));
        _buttonEffect._canClick = true;
        _sequence.Kill();
    }
    private Tween PointUpTweenHandler(Vector3 targetScale)
    {
        _buttonEffect.TurnAction();
        return StaticTweenFunctions.MyScaleHandler(_transform, targetScale, 0.35f);
    }
}