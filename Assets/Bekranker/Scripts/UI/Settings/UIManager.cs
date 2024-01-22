using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour, IClick
{
    [SerializeField] private LevelManager _levelManager;
    public int _counter {get; private set;}

    void OnEnable()
    {
        ClickManager.OnClick += ClickHandler;
    }
    void OnDisable()
    {
        ClickManager.OnClick -= ClickHandler;
    }
    void Start()
    {
        _counter = _levelManager.TurnCount;
    }
    public void ClickHandler()
    {
        DecreaseCounter();
    }
    void DecreaseCounter()
    {
        _counter = (_counter - 1 >= 0) ? _counter - 1 : 0;
    }
}
