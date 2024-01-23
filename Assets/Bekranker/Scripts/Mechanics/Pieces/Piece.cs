using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour, ITurnable
{
    [SerializeField] public LevelManager _LevelManager;
    public struct IndexValues
    {
        public int Z{get; set;}
        public int X{get; set;}
        public int Y{get; set;}
    }
    public delegate void TurnAction();
    public TurnAction turnAction;
    public bool CanTurn{get; set;}
    public IndexValues Index;
    public readonly List<int> AnglesZ = new List<int>
    {
        0,
        90,
        180,
        270
    };
    public readonly List<int> AnglesY = new List<int>
    {
        0,
        180,
    };
    public readonly List<int> AnglesX = new List<int>
    {
        0,
        180,
    };
    public Vector3 StartPosition;

    private void Awake()
    {
        StartPosition = transform.position;
    }
    private void Start(){
        CanTurn = true;
    }
    
    //Turn Direction Functions
    #region ITurnable
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
    #endregion
    
    //Turn Action
    private void TurnMe()
    {
        transform.DORotate(new Vector3(transform.rotation.x, AnglesY[Index.Y], AnglesZ[Index.Z]), 0.5f).SetUpdate(true);
        SetCorrectPiece();
        turnAction?.Invoke();
    }
    public void SetCorrectPiece()
    {
        if(CorrectState())
        {
            _LevelManager.AddPiece(this);
        }
        else
        {
            _LevelManager.RemovePiece(this);
        }
    }
    public bool CorrectState()
    {
        return AnglesZ[Index.Z] == 0 && AnglesY[Index.Y] == 0 && AnglesX[Index.X] == 0 && transform.position == StartPosition;
    }
}