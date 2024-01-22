using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelEnterance : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    public bool AngleX, AngleY, AngleZ;





    void OnEnable()
    {
        LevelManager.OnEnterance += SetPieceToRandom;
    }
    void OnDisable()
    {
        LevelManager.OnEnterance -= SetPieceToRandom;
    }

    private void SetPieceToRandom()
    {
        print("girdi");
        _levelManager.Pieces.ForEach((piece) =>
        {
            int rand = Random.Range(1, 3);
            if(AngleX)
            {
                for (int i = 0; i < rand; i++)
                {
                    piece.TurnMeX();
                }
            }
            if(AngleY)
            {
                for (int i = 0; i < rand; i++)
                {
                    piece.TurnMeY();
                }
            }
            if(AngleZ)
            {
                for (int i = 0; i < rand; i++)
                {
                    piece.TurnMeZ();
                }
            }
        });
        _levelManager.GridScrollPieces?.ForEach((gridScrollPiece)=>
        {
            int index = _levelManager.GridScrollPieces.FindIndex(x => x == gridScrollPiece);
            if(index != -1)
            {
                gridScrollPiece.gameObject.transform.DOMove(_levelManager.GridScrollPieces[index].transform.position, .2f);
                _levelManager.GridScrollPieces[index].transform.DOMove(gridScrollPiece.transform.position, .2f);
            }
        });
        LevelManager.CanClick = true;
    }
}