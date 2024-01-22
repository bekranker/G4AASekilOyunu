using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShockWave : MonoBehaviour
{

    [SerializeField] private float _shockWaveTime = .75f;
    [SerializeField] private Image _shockWaveImage;
    public Vector2 ShockWaveStartPoint;
    private Coroutine _shockWaveCoroutine;
    private Material _material;
    private static int _ringSize = Shader.PropertyToID("_WaveDistanceFromCenter");
    private static int _mainTexture = Shader.PropertyToID("_MainTex");
    private static int _shockWavePosition = Shader.PropertyToID("_SquarePosition");

    void Awake()
    {
        _material = _shockWaveImage.material;
    }
    
    public void CallShockWave(Vector2 shockWavePos)
    {
        ShockWaveStartPoint = shockWavePos;
        _shockWaveCoroutine = StartCoroutine(ScreenShockWaveAction(-.1f, 1f));
    }
    
    private IEnumerator ScreenShockWaveAction(float startValue, float endValue)
    {
        _material.SetVector(_shockWavePosition, ShockWaveStartPoint);
        _material.SetFloat(_ringSize, startValue);

        float lerpedAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < _shockWaveTime)
        {
            elapsedTime += Time.unscaledDeltaTime;

            lerpedAmount = Mathf.Lerp(startValue, endValue, (elapsedTime / _shockWaveTime));
            _material.SetFloat(_ringSize, lerpedAmount);

            yield return null;
        }
    }
}
