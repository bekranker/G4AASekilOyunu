using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HomeButton : AbstractButton, ICommand
{
    [SerializeField] private ButtonEffect _buttonEffect;
    [SerializeField] private RectTransform _to;
    [SerializeField] private RectTransform _transform;
    private bool _toogle{get; set;}
    private Vector3 _startPos{get; set;}


    void Start()
    {
        _startPos = _transform.position;
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
           DOVirtual.DelayedCall(.2f, ()=>StaticTweenFunctions.MyMoveHandler(_transform, _startPos, 0.35f).SetEase(Ease.InBack));
        }
    }

    public override void EffectHandler()
    {
        StaticTweenFunctions.MyRotationPunchHandler(_transform, new Vector3(0,0,90), 0.25f);
    }
}
