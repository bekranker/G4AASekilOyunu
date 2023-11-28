using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    private ButtonEffect _ButtonEffect;
    [SerializeField]
    private Image _Image;
    [SerializeField]
    private Sprite _SpriteNormal;
    [SerializeField]
    private Sprite _SpritePressed;




    void OnEnable()
    {
        _ButtonEffect.OnUp += OnClick;
    }
    void OnDisable()
    {
        _ButtonEffect.OnUp -= OnClick;
    }

    void OnClick()
    {
        _Image.sprite = _SpritePressed;
    }
}