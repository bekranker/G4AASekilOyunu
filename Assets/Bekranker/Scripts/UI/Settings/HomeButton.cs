using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HomeButton : AbstractButton, ICommand
{
    [SerializeField] private ButtonEffect _buttonEffect;
    [SerializeField] private RectTransform _to;
    [SerializeField] private RectTransform _transform;
    [SerializeField] private RectTransform _startPos;
    private bool _toogle{get; set;}


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
            DOVirtual.DelayedCall(.2f, ()=>StaticTweenFunctions.MyMoveHandler(_transform, _to.position, 0.35f).SetEase(Ease.OutBack));
        }
        else{
           DOVirtual.DelayedCall(.2f, ()=>StaticTweenFunctions.MyMoveHandler(_transform, _startPos.position, 0.35f).SetEase(Ease.InBack));
        }
    }

    public override void EffectHandler()
    {
        StaticTweenFunctions.MyRotationPunchHandler(_transform, new Vector3(0,0,90), 0.25f);
    }
}
