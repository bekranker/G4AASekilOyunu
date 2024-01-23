using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> _blacks = new();
    [SerializeField] private Transform _starsParent;
    public int StarIndex;
    [SerializeField] private Sprite _playedLevel;
    [SerializeField] private Sprite _lockedImage;
    [SerializeField] private Sprite _passedLevel;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private ButtonEffect _buttonEffect;

    void Start()
    {
        SetStars();
        SetButtonImage();
    }
    private void SetStars()
    {
        if(PlayerPrefs.HasKey($"Level{StarIndex}"))
        {
            if(PlayerPrefs.HasKey($"Level{StarIndex}StarEarned"))
            {
                int earnedStarCount = PlayerPrefs.GetInt("Level{StarIndex}StarEarned");
                for (int i = 0; i < earnedStarCount; i++)
                {
                    _blacks[i].SetActive(false);
                }
            }
        }
    }
    private void SetButtonImage()
    {
        if (IsMe("LastPlayedLevel"))
        {
            _buttonImage.sprite = _playedLevel;
        }
        else if(IsMe($"PassedLevel{StarIndex}"))
        {
            _buttonImage.sprite = _passedLevel;
        }
        else
        {
            _buttonImage.sprite = _lockedImage;
            _starsParent.gameObject.SetActive(false);
            _buttonEffect.enabled = false;
        }
    }
    private bool IsMe(string saveName)
    {
        return PlayerPrefs.GetInt(saveName, -1) == StarIndex;
    }
}
