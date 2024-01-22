using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private SettingsButton _settingsButton;

    void OnEnable()
    {
        LevelManager.OnWin += PieceHandler;
    }
    void OnDisable()
    {
        LevelManager.OnWin -= PieceHandler;
    }   
    public void PieceHandler()
    {
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
    }
}
