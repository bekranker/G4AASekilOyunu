using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StarSliderHandler : MonoBehaviour
{
    public static event Action<Transform> OnClick, OnDead;


    [SerializeField] private List<Image> _stars = new List<Image>();
    public List<Transform> _starsParents = new List<Transform>();
    public List<Image> _filledStars = new List<Image>();
    public List<Image> _unFilledStars = new List<Image>();
    [SerializeField] private LevelManager _levelManager;
    

    private float _decreaser;
    private int _selectedIndex;

    void Start()
    {
        _selectedIndex = 0;
        float allStarDecreaser = _levelManager.TurnCount / 3;
        _decreaser = 1 / allStarDecreaser;
    }
    void OnEnable()
    {
        ClickManager.OnClick += StarHandler;
    }
    void OnDisable()
    {
        ClickManager.OnClick -= StarHandler;
    }
    public void StarHandler()
    {
        _stars[_selectedIndex].fillAmount = (_stars[_selectedIndex].fillAmount - _decreaser >= 0) ? _stars[_selectedIndex].fillAmount - _decreaser : 0;
        StarSelector();
    }
    private void StarSelector()
    {
        if (_stars[_selectedIndex].fillAmount - _decreaser < 0)
        {
            OnDead?.Invoke(_stars[_selectedIndex].transform);
            _filledStars.Remove(_stars[_selectedIndex]);
            _unFilledStars.Add(_stars[_selectedIndex]);
            _selectedIndex = (_selectedIndex + 1 >= _stars.Count) ? 0 : _selectedIndex + 1;
            return;
        }
        OnClick?.Invoke(_stars[_selectedIndex].transform);
    }
}