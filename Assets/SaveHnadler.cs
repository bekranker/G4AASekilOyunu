using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveHnadler : MonoBehaviour
{
    [SerializeField] private StarSliderHandler _starSliderHandler;
    void Start()
    {
        PlayerPrefs.SetInt("LastPlayedLevel", SceneManager.GetActiveScene().buildIndex);
    }
    void OnEnable()
    {
        StarSliderHandler.OnWin += StarCount;
    }
    void OnDisable()
    {
        StarSliderHandler.OnWin -= StarCount;
    }
    private void StarCount()
    {
        PlayerPrefs.SetInt($"Level{SceneManager.GetActiveScene().buildIndex}StarEarned", _starSliderHandler._filledStars.Count);
    }
}
