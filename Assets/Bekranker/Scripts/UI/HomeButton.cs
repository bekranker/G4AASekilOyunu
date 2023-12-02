using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HomeButton : AbstractButton, ICommand
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
        DOVirtual.DelayedCall(.2f, ()=> StaticTweenFunctions.MyMoveHandler(_transform, _to.position, 0.35f));
    }

    public override void EffectHandler()
    {
        //effekt kodları buraya
    }
}
