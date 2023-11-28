using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour, ITurnable
{
    public struct IndexValues
    {
        public int Z{get; set;}
        public int X{get; set;}
        public int Y{get; set;}
    }
    public delegate void TurnAction();
    public TurnAction turnAction;
    public bool CorrectAngle{get; set;}
    public bool CanTurn{get; set;}
    public IndexValues Index;
    private Transform _transform;
    public readonly static List<int> AnglesZ = new List<int>
    {
        0,
        90,
        180,
        270
    };
    public readonly static List<int> AnglesY = new List<int>
    {
        0,
        180,
    };
    public readonly static List<int> AnglesX = new List<int>
    {
        0,
        180,
    };

    private void Start(){
        _transform = transform;
        CanTurn = true;
    }
    
    public void TurnMeZ(){
        if(!CanTurn) return;

        Index.Z = (Index.Z + 1 < AnglesZ.Count) ? Index.Z + 1 : 0; 
        TurnMe();
    }
    public void TurnMeY()
    {
        if(!CanTurn) return;

        Index.Y = (Index.Y + 1 < AnglesY.Count) ? Index.Y + 1 : 0;
        TurnMe();
    }
    public void TurnMeX()
    {
        if(!CanTurn) return;

        Index.X = (Index.X + 1 < AnglesX.Count) ? Index.X + 1 : 0;
        TurnMe();
    }
    private void TurnMe()
    {
        _transform.DORotate(new Vector3(_transform.rotation.x, AnglesY[Index.Y], AnglesZ[Index.Z]), 0.5f).SetUpdate(true);
        turnAction?.Invoke();
    } 
}