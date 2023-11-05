using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Piece : MonoBehaviour
{
    public bool CorrectAngle;
    public List<Vector3> Angles = new();
    public int Index;
    private ClickManager _clickManager;
    private Transform _t;

    private void Start(){
        _clickManager = GetComponent<ClickManager>();
        _t = transform;
    }

    public void TurnMe(){
        Index = (Index + 1 < Angles.Count) ? Index + 1 : 0; 
        _t.DORotate(Angles[Index], _clickManager.Speed).SetUpdate(true);
    }
}