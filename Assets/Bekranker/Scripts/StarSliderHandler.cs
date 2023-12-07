using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSliderHandler : MonoBehaviour
{
    [SerializeField] private List<Image> _stars = new List<Image>();
    [SerializeField] private LevelManager _levelManager;
    

    private float _decreaser;
    private int _selectedIndex;

    void Start()
    {
        _selectedIndex = 0;
        _decreaser = (1f / _levelManager.TurnCount) * 3f;
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
        _stars[_selectedIndex].fillAmount = (_stars[_selectedIndex].fillAmount - _decreaser > 0) ? _stars[_selectedIndex].fillAmount - _decreaser : 0;
        StarSelector();
    }
    private void StarSelector()
    {
        if (_stars[_selectedIndex].fillAmount - _decreaser <= 0)
        {
            _selectedIndex = (_selectedIndex + 1 >= _stars.Count) ? 0 : _selectedIndex + 1;
        }
    }
}