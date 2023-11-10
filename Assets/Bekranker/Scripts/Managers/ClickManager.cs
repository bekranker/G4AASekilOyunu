using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
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



    [Space(15)]
    [Header("---Managers")]
    [Space(15)]
    private RaycastHit2D _hit;
    private Piece _piece;
    public Vector3 FingerDownPosition;
    private Camera _camera;

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
    

    public void Raycasting(LeanFinger finger){
        FingerDownPosition = _camera.ScreenToWorldPoint(finger.ScreenPosition);
        FingerDownPosition.z = 0;
        RaycastHit2D hit = Physics2D.Raycast(FingerDownPosition, Vector3.forward, Mathf.Infinity);
        var hitColldier = hit.collider;
        if(hitColldier == null) return;
        var hitGameObject = hitColldier.gameObject;
        print("Turned" );
        if(hitGameObject.TryGetComponent(out _piece) && !hitGameObject.GetComponent<Piece_FlipHorizontal>())
        {
            //Turn On Z
            _piece.TurnMe();
        }
        if(hitGameObject.TryGetComponent(out Piece_FlipHorizontal piece_Horiztonal)){
            //Turn On Y
        }
    }
}
