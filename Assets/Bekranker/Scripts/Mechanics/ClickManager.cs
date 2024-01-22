using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lean.Touch;
using TMPro;
using UnityEngine;


public class ClickManager : MonoBehaviour
{
    public static event Action OnClick;
    
    [Space(15)]
    [Header("---Dotween Props")]
    [Space(15)]
    public float Speed;

    [Space(15)]
    [Header("---Props")]
    [Space(15)]
    [SerializeField] private LayerMask _rayLayer;
    
    private RaycastHit2D _hit;
    private Piece _piece;
    private Vector3 _fingerPosition;
    private Camera _camera;
    private Collider2D _hitCollider;
    [HideInInspector] public GameObject _hitGameObject;
    private Directions _eDirections = new();
    [SerializeField] private TMP_Text _buttonText; 


    void Start(){
        _camera = Camera.main;
    }
    void OnEnable()
    {
        LeanTouch.OnFingerDown += Raycasting;
    }
    void OnDisable()
    {
        LeanTouch.OnFingerDown -= Raycasting;
    }
    public void Raycasting(LeanFinger finger)
    {
        if(!Raycast(finger)) return;
        OnClick?.Invoke();
        _hitGameObject = _hitCollider.gameObject;
        HitedPiece();
        
        switch (_eDirections)
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
        _fingerPosition = _camera.ScreenToWorldPoint(finger.ScreenPosition);
        _fingerPosition.z = 0;
        _hit = Physics2D.Raycast(_fingerPosition, Vector3.forward, Mathf.Infinity, _rayLayer);
        _hitCollider = _hit.collider;
        return _hitCollider;
    }
    private void HitedPiece() 
    {
        _piece = _hitGameObject.GetComponent<Piece>();
    }
    public void ChangeState()
    {
        switch (_eDirections)
        {
            case Directions.Z:
                _buttonText.text = "Flip X";
                _eDirections = Directions.Y;
                break; 
            case Directions.X:
                _buttonText.text = "Flip Y";
                _eDirections = Directions.Z;
                break;
            case Directions.Y:
                _buttonText.text = "Flip Z";
                _eDirections = Directions.X;
                break;
            default:
                break;
        }
    }
}
