using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _downScale;
    [SerializeField] private Vector2 _upScale;


    public delegate void UpAction();
    public event UpAction OnUp, OnUpFinished;


    private Transform _transform { get; set; }
    private Sequence _sequence { get; set; }
    private bool _canClick { get; set; }


    void Awake()
    {
        _canClick = true;
    }

    private void Start()
    {
        _transform = transform;
        _sequence = DOTween.Sequence();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_canClick) return;
        RotationHandler(new Vector3(_transform.rotation.x, _transform.rotation.y, _transform.rotation.z + 180));
        ScaleHandler(_downScale);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!_canClick) return;
        _canClick = false;
        OnUp?.Invoke();
        _sequence.Append(PointUpTweenHandler(_upScale).OnComplete(()=> 
        {
            _canClick = true;
            OnUpFinished?.Invoke();
            _sequence.Kill();
        }));
        _sequence.Play();
    }

    private Tween PointUpTweenHandler(Vector3 targetScale)
    {
        RandomRotationHandler(new Vector3(_transform.rotation.x, _transform.rotation.y, _transform.rotation.z + 125));
        return ScaleHandler(targetScale);
    }

    private Tween ScaleHandler(Vector3 targetScale)
    {
        return _transform.DOScale(targetScale, _speed).SetUpdate(true);
    }

    private Tween RandomRotationHandler(Vector3 targetRotation)
    {
        return _transform.DORotate(targetRotation, _speed, RotateMode.LocalAxisAdd).SetUpdate(true);
    }

    private Tween RotationHandler(Vector3 targetRotation)
    {
        return _transform.DORotate(targetRotation, _speed).SetUpdate(true);
    }
}