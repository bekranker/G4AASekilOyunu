using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Navigations{
    
    UP,
    DOWN,
    LEFT,
    RIGHT
}
public class Piece_GridScrollManager : MonoBehaviour
{
    public Navigations CurrentTargetMoveType;
    public List<Piece> PiecesCanGo;


    void OnEnable(){

    }

    void OnDisable(){
        
    }
}