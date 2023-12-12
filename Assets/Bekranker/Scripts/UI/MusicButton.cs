using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicButton : AbstractButton, ICommand
{
    [SerializeField] private ButtonEffect _buttonEffect;
    [SerializeField] private RectTransform _to;
    [SerializeField] private RectTransform _transform;
    private bool _toogle;
    private Vector3 _startPos;

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
                StaticTweenFunctions.MyMoveHandler(_transform, _to.position, 0.35f);
            }
            else{
                StaticTweenFunctions.MyMoveHandler(_transform, _startPos, 0.35f);
            };
    }
    public override void EffectHandler()
    {
        
    }
}
