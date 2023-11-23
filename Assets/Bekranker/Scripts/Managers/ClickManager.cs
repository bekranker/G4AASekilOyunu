using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lean.Touch;
using TMPro;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [Space(15)]
    [Header("---Dotween Props")]
    [Space(15)]
    public float Speed;

    [Space(15)]
    [Header("---Props")]
    [Space(15)]
    [SerializeField] private LayerMask _RayLayer;
    private RaycastHit2D _hit;
    private Piece _piece;
    private Vector3 FingerDownPosition;
    private Camera _camera;
    private Collider2D _hitCollider;
    private GameObject _hitGameObject;
    private Directions EDirections = new();
    [SerializeField] private TMP_Text _ButtonText; 


    void Start(){
        _camera = Camera.main;
    }
    void OnEnable()
    {
        LeanTouch.OnFingerUp += Raycasting;
    }
    void OnDisable()
    {
        LeanTouch.OnFingerUp -= Raycasting;
    }
    public void Raycasting(LeanFinger finger)
    {
        if(!Raycast(finger)) return;
        _hitGameObject = _hitCollider.gameObject;
        HitedPiece();
        
        switch (EDirections)
        {
            case Directions.Z:
                TurnZ();
                break; 
            case Directions.X:
                TrunX();
                break;
            case Directions.Y:
                TurnY();
                break;
            default:
                break;
        }
    }

    private void TurnZ()
    {
        _piece.TurnMeZ();
    }
    private void TurnY()
    {
        _piece.TurnMeY();
    }
    private void TrunX()
    {
        _piece.TurnMeX();
    }
    private bool Raycast(LeanFinger finger)
    {
        FingerDownPosition = _camera.ScreenToWorldPoint(finger.ScreenPosition);
        FingerDownPosition.z = 0;
        _hit = Physics2D.Raycast(FingerDownPosition, Vector3.forward, Mathf.Infinity, _RayLayer);
        _hitCollider = _hit.collider;
        return _hitCollider;
    }
    private void HitedPiece() 
    {
        _piece = _hitGameObject.GetComponent<Piece>();
    }
    public void ChangeState()
    {
        switch (EDirections)
        {
            case Directions.Z:
                _ButtonText.text = "Flip X";
                EDirections = Directions.Y;
                break; 
            case Directions.X:
                _ButtonText.text = "Flip Y";
                EDirections = Directions.Z;
                break;
            case Directions.Y:
                _ButtonText.text = "Flip Z";
                EDirections = Directions.X;
                break;
            default:
                break;
        }
    }
}
