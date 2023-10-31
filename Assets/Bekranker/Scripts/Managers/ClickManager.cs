using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [Space(15)]
    [Header("---Dotween Props")]
    [Space(15)]
    public float Speed;

    [Space(15)]
    [Header("---Piece Props")]
    [Space(15)]
    [SerializeField] private LayerMask _LayerMask;

    private RaycastHit2D _hit;



    void Update()
    {
        Raycasting();
    }

    private void Raycasting(){
        
    }
}
