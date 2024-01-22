using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;



public class StarEffectHandler : MonoBehaviour, IStarEffect
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private StarSliderHandler _starSliderHandler;
    [SerializeField] private ShockWave _shockWave;
    [SerializeField] private Transform _settingsButtonTransform;
    [SerializeField] private Image _settingsButtonImage;
    [SerializeField] private Sprite _settingsButtonSprite, _starSprite;
    private Camera _cam;
    private Sequence _sequence;
    private int _index;



    public void OnClick(Transform starT)
    {
        starT.DOPunchScale(Vector3.one * .5f, .1f);
    }
    void OnEnable()
    {
        StarSliderHandler.OnClick += OnClick;
        LevelManager.OnWin += StarFillEffect;
    }

    void OnDisable()
    {
        StarSliderHandler.OnClick -= OnClick;
        LevelManager.OnWin -= StarFillEffect;
    }
    void Start()
    {
        _cam = Camera.main;
    }
    private void StarFillEffect(){
        if(_starSliderHandler._filledStars.Count == 0) return;
        if(_index == 3) return;

        PointUpTweenHandler();

        var item = FilledStar();
        UnFilledStar()?.ForEach((starParent)=>
        {
            starParent.transform.parent.GetChild(0).DOLocalMoveY(starParent.transform.parent.GetChild(0).position.y + 1, .5f);
            starParent.transform.parent.GetChild(0).GetComponent<Image>().DOFade(0, .5f);
            starParent.transform.DOLocalMoveY(starParent.transform.position.y + 1, .5f);
            starParent.DOFade(0, .5f);
        });

        _sequence = DOTween.Sequence();
        _sequence.Append(PhaseOne(item.transform.parent));
        _sequence.Append(item.DOFillAmount(1, .25f));
        _sequence.Append(DOVirtual.DelayedCall(.1f, ()=>PhaseTwo(item.transform.parent)));
        _sequence.Append(DOVirtual.DelayedCall(.1f, StarFillEffect));
        _index++;
    }

    private Tween PhaseOne(Transform _star){
        _star.DOLocalRotate(Vector3.forward * 360, .5f, RotateMode.FastBeyond360);
        return _star.DOScale(Vector2.one * 1.3f, .25f);
    }
    private Tween PhaseTwo(Transform _star){
        //particle
        Vector2 screenPosition = _cam.WorldToViewportPoint(_star.position);
        _shockWave.CallShockWave(screenPosition);
        Instantiate(_particleSystem, _star.position, Quaternion.identity);
        _star.gameObject.SetActive(false);
        return _cam.DOShakeRotation(.1f, 2, 2, 15, fadeOut: true, randomnessMode: ShakeRandomnessMode.Harmonic);
    }
    public Tween PointUpTweenHandler()
    {
        return StaticTweenFunctions.MyRotationHandler(_settingsButtonTransform, Vector3.forward * 360, .3f).SetEase(Ease.OutBack);
    }
    private Image FilledStar() => _starSliderHandler._filledStars[_index];
    private List<Image> UnFilledStar() => _starSliderHandler._unFilledStars;
}