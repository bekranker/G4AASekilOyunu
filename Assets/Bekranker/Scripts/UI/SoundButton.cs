using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundButton : AbstractButton, ICommand
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
        DOVirtual.DelayedCall(.1f, ()=> StaticTweenFunctions.MyMoveHandler(_transform, _to.position, 0.35f));
    }

    public override void EffectHandler()
    {
        
    }
}
