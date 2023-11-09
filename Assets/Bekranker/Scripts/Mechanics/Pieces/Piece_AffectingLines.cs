using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_AffectingLines : MonoBehaviour
{
    public Piece AffectedLine;
    public Piece ParentPiece;

    public void AffetedTurn(){
        AffectedLine.TurnMe();
    }


}
