using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : AbstractClick
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private ClickManager _clickManager;
    private int _counter {get; set;}


    void Start()
    {
    }
    public override void ClickHandler()
    {
        DecreaseCounter();
    }
    void DecreaseCounter()
    {
        _counter = (_counter - 1 >= 0) ? _counter - 1 : 0;
    }
}
