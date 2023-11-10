using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Material _Material;
    public Color ColorOne, ColorTwo;

    private static int _backgroundColorOne = Shader.PropertyToID("_Color_1");
    private static int _backgroundColorTwo = Shader.PropertyToID("_Color_2");

    void Start()
    {
        SetBackgroundColors();
    }
    private void SetBackgroundColors(){
        _Material.SetColor(_backgroundColorOne, ColorOne);
        _Material.SetColor(_backgroundColorTwo, ColorTwo);
    }
    
}