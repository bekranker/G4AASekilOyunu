using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Piece_GridScrollManager : MonoBehaviour, ISwipe
{
    [HideInInspector] public bool ChangingSide;
    [HideInInspector] public Piece Piece;
    
    void OnStart()
    {
        Piece = GetComponent<Piece>();
    }

    public void SwipeHandlerEnter()
    {
        Piece.CanTurn = false;
        ChangingSide = true;
    }
    public void SwipeHandlerExit()
    {
        Piece.CanTurn = true;
        ChangingSide = false;
    }
}