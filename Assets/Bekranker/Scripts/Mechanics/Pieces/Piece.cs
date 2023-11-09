using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{
    public delegate void _turnAction();
    public _turnAction TurnAction; 
    public bool CorrectAngle;
    public int Index;
    private Transform _t;
    public List<int> Angles = new List<int>
    {
        0,
        90,
        180,
        270
    };

    private void Start(){
        _t = transform;
    }

    public void TurnMe(){
        Index = (Index + 1 < Angles.Count) ? Index + 1 : 0; 
        _t.DORotate(new Vector3(_t.rotation.x, _t.rotation.y, Angles[Index]), 0.5f).SetUpdate(true).OnComplete(()=>{
           TurnAction?.Invoke();
        });
    }
}