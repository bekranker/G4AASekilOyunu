using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public int PieceCount;
    public List<Piece> PiecesTrue = new();
    public List<Piece> Pieces = new();
    public List<Piece> GridScrollPieces = new();
    public int TurnCount;
    public Material _Material;
    public Color ColorOne, ColorTwo;
    private static int _backgroundColorOne = Shader.PropertyToID("_Color_1");
    private static int _backgroundColorTwo = Shader.PropertyToID("_Color_2");
    public static event Action OnWin, OnEnterance;
    public static bool CanClick;
    void Start()
    {
        CanClick = false;
        OnEnterance?.Invoke();
        SetBackgroundColors();
    }
    private void SetBackgroundColors(){
        _Material.SetColor(_backgroundColorOne, ColorOne);
        _Material.SetColor(_backgroundColorTwo, ColorTwo);
    }

    public void AddPiece(Piece piece){
        if(PiecesTrue.Contains(piece)) return;
        PiecesTrue.Add(piece);;
        WinCondition();
    }
    public void RemovePiece(Piece piece)
    {
        if(!PiecesTrue.Contains(piece)) return;
        PiecesTrue.Remove(piece);
    }
    private void WinCondition(){
        if(PiecesTrue.Count == 0) return;
        if(PiecesTrue.Count != PieceCount) return;
        OnWin?.Invoke();
    }
}