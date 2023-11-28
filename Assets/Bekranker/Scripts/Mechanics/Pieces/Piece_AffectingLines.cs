using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_AffectingLines : MonoBehaviour
{
    public Piece AffectedLine;
    private Piece _mainPiece;

    void Awake(){
        _mainPiece = GetComponent<Piece>();
    }
    void OnEnable(){
        _mainPiece.turnAction += AffectedTurn; 
    }
    void OnDisable(){
        _mainPiece.turnAction -= AffectedTurn;
    }

    public void AffectedTurn(){
        AffectedLine.TurnMeZ();
    }
}