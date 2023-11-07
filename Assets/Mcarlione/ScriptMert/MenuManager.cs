using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using JetBrains.Annotations;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform _Title;
    [SerializeField] private RectTransform _Settings;
    [SerializeField] public Vector2 _Duration;
    [SerializeField] public Vector2 _EntryLocation;
    [SerializeField] public Vector2 _ExitLocation;
    [SerializeField] private Animator _ClickToStartAnimation;
    void Start()
    {
        _Title.DOLocalMove(new Vector2(_Title.position.x,_EntryLocation.x),_Duration.x);
        _Settings.DOLocalMove(new Vector2(_Title.position.x,_EntryLocation.y),_Duration.x);
    }
    public void SwitchScene()
    {
        _ClickToStartAnimation.SetTrigger("SwitchScene");
        _Title.DOLocalMove(new Vector2(_Title.position.x,_ExitLocation.x),_Duration.y);
        _Settings.DOLocalMove(new Vector2(_Title.position.x,_ExitLocation.y),_Duration.y);

    }
        
    

}
