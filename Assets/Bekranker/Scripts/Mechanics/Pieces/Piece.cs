using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour, ITurn
{
    public delegate void _turnAction();
    public _turnAction TurnAction;
    public bool CorrectAngle;
    public int IndexZ, IndexY, IndexX;
    private Transform _t;
    public List<int> AnglesZ = new List<int>
    {
        0,
        90,
        180,
        270
    };
    public List<int> AnglesY = new List<int>
    {
        0,
        180,
    };
    public List<int> AnglesX = new List<int>
    {
        0,
        180,
    };

    private void Start(){
        _t = transform;
    }
    
    public void TurnMeZ(){
        IndexZ = (IndexZ + 1 < AnglesZ.Count) ? IndexZ + 1 : 0; 
        TurnMe();
    }
    public void TurnMeY()
    {
        IndexY = (IndexY + 1 < AnglesY.Count) ? IndexY + 1 : 0;
        TurnMe();
    }
    public void TurnMeX()
    {
        IndexX = (IndexX + 1 < AnglesX.Count) ? IndexX + 1 : 0;
        TurnMe();
    }
    private void TurnMe()
    {
        _t.DORotate(new Vector3(_t.rotation.x, AnglesY[IndexY], AnglesZ[IndexZ]), 0.5f).SetUpdate(true);
        TurnAction?.Invoke();
    } 
}