using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] private TMP_Text _levelText;





    void OnEnable()
    {
        _buttonEffect.OnUp += SetLevelIndex;
    }
    void OnDisable()
    {
        _buttonEffect.OnUp -= SetLevelIndex; 
    }


    void Start()
    {
        _levelText.text = StarIndex.ToString();
        SetButtonImage();
    }
    private void SetStars()
    {
        int earnedStarCount = PlayerPrefs.GetInt($"Level{StarIndex}StarEarned");
        print(earnedStarCount);
        for (int i = 0; i < earnedStarCount; i++)
        {
            _blacks[i].SetActive(false);
        }
    }
    private void SetButtonImage()
    {
        if(StarIndex == 1)
        {
            _buttonImage.sprite = _playedLevel;
            return;
        }
        if (IsMe("LastPlayedLevel"))
        {
            _buttonImage.sprite = _playedLevel;
            SetStars();
        }
        else if(IsMe($"PassedLevel{StarIndex}"))
        {
            _buttonImage.sprite = _passedLevel;
            SetStars();
        }
        else
        {
            _levelText.enabled = false;
            _buttonImage.sprite = _lockedImage;
            _starsParent.gameObject.SetActive(false);
            _buttonEffect.enabled = false;
        }
    }
    private void SetLevelIndex() => SceneManager.LoadScene(StarIndex);
    private bool IsMe(string saveName)
    {
        return PlayerPrefs.GetInt(saveName, -1) == StarIndex;
    }
}