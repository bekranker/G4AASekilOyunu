using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using DG.Tweening;

public class SwipeManager : MonoBehaviour
{
    [Space(15)]
    [Header("---Props")]
    [Space(15)]
    [SerializeField] private LayerMask _RayLayer;

    /// <summary>
    /// https://github.com/modesttree/Zenject
    /// </summary>

    private RaycastHit2D _hit;
    private Camera _camera;
    private Vector3 FingerPosition;
    private Collider2D _hitColliderDown, _hitColliderUp;
    private GameObject _hitGameObjectDown, _hitGameObjectUp;
    private Piece_GridScrollManager _piece_GridScrollManagerDown, _piece_GridScrollManagerUp;
    private Piece _piece;
    private Vector3 _downPiece_Position, _upPiece_Position;
    private Transform _downPiece_T, _upPiece_T;
    private Sequence _sequence;



    void Start()
    {
        _camera = Camera.main;
        _sequence = DOTween.Sequence();
    }
    void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleOnDown;
        LeanTouch.OnFingerSwipe += HandleSwipe;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleOnDown;
        LeanTouch.OnFingerSwipe -= HandleSwipe;
    }

    private void HandleOnDown(LeanFinger finger)
    {
        if(!Raycast(finger)) return;
        VariableHandler(ref _hitGameObjectDown, ref _hitColliderDown, ref _downPiece_T, ref _downPiece_Position);
        _piece_GridScrollManagerDown = _hitGameObjectDown.GetComponent<Piece_GridScrollManager>();
        _downPiece_Position = _downPiece_T.position;
    }
    public void HandleSwipe(LeanFinger swipe)
    {
        if (swipe.Up)
        {
            if(!_piece_GridScrollManagerDown) return;

            if(!Raycast(swipe)) return;
            
            VariableHandler(ref _hitGameObjectUp, ref _hitColliderUp, ref _upPiece_T, ref _upPiece_Position);
            if(_hitGameObjectUp == _hitGameObjectDown) return;
            
            _piece_GridScrollManagerUp = _hitGameObjectUp.GetComponent<Piece_GridScrollManager>();
            if(!_piece_GridScrollManagerUp) return;
            
            if(_piece_GridScrollManagerDown.ChangingSide || _piece_GridScrollManagerUp.ChangingSide) return;
            
            _piece_GridScrollManagerUp.SwipeHandlerEnter();

            SwipeThePieces();
        }
    }
    private void SwipeThePieces()
    {
        _sequence.Join(_downPiece_T.DOMove(_upPiece_Position, .5f).SetUpdate(true).OnComplete(()=>_piece_GridScrollManagerDown.SwipeHandlerExit()));
        _sequence.Join(_upPiece_T.DOMove(_downPiece_Position, .5f).SetUpdate(true).OnComplete(()=> _piece_GridScrollManagerUp.SwipeHandlerExit()));
        _sequence.Play();
    }
    private void VariableHandler(ref GameObject gameObject, ref Collider2D collider2D, ref Transform t, ref Vector3 position)
    {
        collider2D = _hit.collider;
        gameObject = collider2D.gameObject;
        t = gameObject.transform;
        position = t.position;
    }
    private bool Raycast(LeanFinger finger)
    {
        FingerPosition = _camera.ScreenToWorldPoint(finger.ScreenPosition);
        FingerPosition.z = 0;
        _hit = Physics2D.Raycast(FingerPosition, Vector3.forward, Mathf.Infinity, _RayLayer);
        return _hit;
    }
}