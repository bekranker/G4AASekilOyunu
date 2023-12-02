using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RestartButton :  AbstractButton, ICommand
{
    [SerializeField] private ButtonEffect _buttonEffect;
    [SerializeField] private RectTransform _to;
    [SerializeField] private RectTransform _transform;
    void Start()
    {
        _buttonEffect.StoredComman = this;
    }
    public void Execute()
    {
    }
    public override void SlideHandler()
    {
        DOVirtual.DelayedCall(.3f, ()=> StaticTweenFunctions.MyMoveHandler(_transform, _to.position, 0.35f).SetEase(Ease.OutBack));
    }

    public override void EffectHandler()
    {
        //effekt kodları buraya
    }
}
