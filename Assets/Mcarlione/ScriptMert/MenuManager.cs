using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform _Title;
    [SerializeField] private RectTransform _Settings;
    [SerializeField] private int _YValue;
    [SerializeField] private int _YValueSettings;
    [SerializeField,Range(0,10)] private float _Lenght=2;
    void Start()
    {
      _Title.DOLocalMove(new Vector2(_Title.position.x,_YValue),_Lenght);
      _Settings.DOLocalMove(new Vector2(_Title.position.x,_YValueSettings),_Lenght);
    }
}
