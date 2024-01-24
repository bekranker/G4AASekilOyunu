using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StarSliderHandler : MonoBehaviour
{
    public static event Action<Transform> OnClick, OnDead;
    public static event Action OnWin;

    [SerializeField] private List<Image> _stars = new List<Image>();
    public List<Transform> _starsParents = new List<Transform>();
    public List<Image> _filledStars = new List<Image>();
    public List<Image> _unFilledStars = new List<Image>();
    [SerializeField] private LevelManager _levelManager;
    

    private float _decreaser;
    public int SelectedIndex;

    void Start()
    {
        SelectedIndex = 0;
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
        _stars[SelectedIndex].fillAmount = (_stars[SelectedIndex].fillAmount - _decreaser >= 0) ? _stars[SelectedIndex].fillAmount - _decreaser : 0;
        StarSelector();
    }
    private void StarSelector()
    {
        if (_stars[SelectedIndex].fillAmount - _decreaser < 0)
        {
            OnDead?.Invoke(_stars[SelectedIndex].transform);
            _filledStars.Remove(_stars[SelectedIndex]);
            _unFilledStars.Add(_stars[SelectedIndex]);
            SelectedIndex = (SelectedIndex + 1 >= _stars.Count) ? 0 : SelectedIndex + 1;
            OnWin?.Invoke();
            return;
        }
        OnClick?.Invoke(_stars[SelectedIndex].transform);
    }
}