using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;



public class EffeckHandler : MonoBehaviour
{
    [SerializeField] private ClickManager _clickManager;
    private GameObject _clickedPiece{get; set;}

    void OnEnable(){
        LeanTouch.OnFingerDown += ClickEffectDown;
        LeanTouch.OnFingerUp += ClickEffectUp;
    }
    void OnDisable(){
        LeanTouch.OnFingerDown -= ClickEffectDown;
        LeanTouch.OnFingerUp -= ClickEffectUp;
    }
    void ClickEffectDown(LeanFinger finger){
        // _clickedPiece = _clickManager._hitGameObject;
        // if(!_clickedPiece) return;
        // StaticTweenFunctions.MyScaleHandler(_clickedPiece.transform, Vector3.one * .8f, .2f);
    }
    void ClickEffectUp(LeanFinger finger){
        // if(!_clickedPiece) return;
        // StaticTweenFunctions.MyScaleHandler(_clickedPiece.transform, Vector3.one, .2f);
        // _clickedPiece = null;
        // _clickManager._hitGameObject = null;
    }
}
