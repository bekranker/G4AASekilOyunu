using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockWave : MonoBehaviour
{

    [SerializeField] private float _shockWaveTime = .75f;
    [SerializeField] private Image _shockWaveImage;
    public Vector2 ShockWaveStartPoint;
    private Coroutine _shockWaveCoroutine;
    private Material _material;
    private static int _ringSize = Shader.PropertyToID("_RingSize");
    private static int _mainTexture = Shader.PropertyToID("_MainTex");
    private static int _shockWavePosition = Shader.PropertyToID("_RingSpawnPosition");

    void Awake()
    {
        _material = _shockWaveImage.material;
    }
    private void Start()
    {
        _shockWaveImage.enabled = false;
    }

    public void CallShockWave(Vector3 to, Vector2 shockWavePos)
    {
        _shockWaveImage.enabled = true;
        CreateAudio.PlayAudio($"OptionsOpeningSoundEffect", .05f, "General", "Sound");
        ShockWaveStartPoint = shockWavePos;
        transform.position = to;
        _shockWaveCoroutine = StartCoroutine(ScreenShockWaveAction(-.1f, 2f));
    }
    
    private IEnumerator ScreenShockWaveAction(float startPos, float endPos)
    {
        _material.SetVector(_shockWavePosition, ShockWaveStartPoint);
        _material.SetFloat(_ringSize, startPos);

        float lerpedAmount = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < _shockWaveTime)
        {
            elapsedTime += Time.unscaledDeltaTime;

            lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / _shockWaveTime));
            _material.SetFloat(_ringSize, lerpedAmount);

            yield return null;
        }
        if (elapsedTime >= _shockWaveTime)
        {
            _shockWaveImage.enabled = false;
        }
    }
}
