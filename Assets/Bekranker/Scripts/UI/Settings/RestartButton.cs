using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RestartButton : AbstractButton, ICommand
{
    [SerializeField] private ButtonEffect _buttonEffect;
    [SerializeField] private RectTransform _to;
    [SerializeField] private RectTransform _transform;
    [SerializeField] private RectTransform _startPos;

    private bool _toogle;

    void Start()
    {
        _buttonEffect.StoredComman = this;
    }
    public void Execute()
    {
        EffectHandler();
    }
    public override void SlideHandler()
    {
        _toogle = !_toogle;
            if(_toogle){
                DOVirtual.DelayedCall(.3f, ()=>StaticTweenFunctions.MyMoveHandler(_transform, _to.position, 0.35f).SetEase(Ease.OutBack));
            }
            else{
                DOVirtual.DelayedCall(.3f, ()=>StaticTweenFunctions.MyMoveHandler(_transform, _startPos.position, 0.35f).SetEase(Ease.InBack));
            };
    }
    public override void EffectHandler()
    {
        
    }
}
