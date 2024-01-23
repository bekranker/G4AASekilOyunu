using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private SettingsButton _settingsButton;
    [SerializeField] private ShockWave _shockWave;
    private bool _win;

    void OnEnable()
    {
        LevelManager.OnWin += PieceHandler;
    }
    void OnDisable()
    {
        LevelManager.OnWin -= PieceHandler;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!_win) return;
            _shockWave.CallShockWave(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            StartCoroutine(WaitForEffect2());
        }
    }
    public void PieceHandler()
    {
        LevelManager.CanClick = false;
        _settingsButton.EffectHandler();
        _settingsButton.GoTo();
        StartCoroutine(WaitForEffect());
    }
    private IEnumerator WaitForEffect()
    {
        yield return new WaitForSeconds(1);
        _levelManager.PiecesTrue?.ForEach((piece) =>
        {
            piece.GetComponent<SpriteRenderer>().DOFade(0, .25f);
        });
        _win = true;
    }
    private IEnumerator WaitForEffect2()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
