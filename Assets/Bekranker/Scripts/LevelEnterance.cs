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
    private int _rand;




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
        _rand = Random.Range(1, 3);
        for (int i = 0; i < _levelManager.Pieces.Count; i++)
        {
            _rand = Random.Range(1, 3);
            if(AngleX)
            {
                for (int a = 0; i <= _rand; a++)
                {
                    _levelManager.Pieces[i].TurnMeX();
                }        
            }
            if(AngleY)
            {
                for (int a = 0; a <= _rand; a++)
                {
                    _levelManager.Pieces[i].TurnMeY();
                }
            }
            if(AngleZ)
            {
                for (int a = 0; a <= _rand; a++)
                {
                    print("girdi");
                    _levelManager.Pieces[i].TurnMeZ();
                }
            }    
        }
        
        for (int i = 0; i < _levelManager.GridScrollPieces.Count; i++)
        {
            if(i != _levelManager.GridScrollPieces.Count - 1)
            {
                _levelManager.GridScrollPieces[i].gameObject.transform.DOMove(_levelManager.GridScrollPieces[i + 1].transform.position, .3f);
                _levelManager.GridScrollPieces[i + 1].transform.DOMove(_levelManager.GridScrollPieces[i].transform.position, .3f).OnComplete(()=>{LevelManager.CanClick = true;});
            }
        }
    }
}