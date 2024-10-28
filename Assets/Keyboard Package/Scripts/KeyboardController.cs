using System;
using TMPro;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private GameObject smallAlphaRow1;
    [SerializeField] private GameObject smallAlphaRow2;
    [SerializeField] private GameObject smallAlphaRow3;

    [SerializeField] private GameObject capitalAlphaRow1;
    [SerializeField] private GameObject capitalAlphaRow2;
    [SerializeField] private GameObject capitalAlphaRow3;

    [SerializeField] private GameObject numbers;
    [SerializeField] private GameObject splCharsNum1;
    [SerializeField] private GameObject splCharsNum2;
    [SerializeField] private GameObject splChars1;
    [SerializeField] private GameObject splChars2;

    [SerializeField] private GameObject actionNumbers;
    [SerializeField] private GameObject actionCapitalLetters;
    [SerializeField] private GameObject actionSmallLetters;

    [SerializeField] private bool isSmallLettersShown = true;

    [SerializeField] private RectTransform keyboard;
    
    private CustomInputField _currentText;

    private static KeyboardController instance;

    public static KeyboardController Instance
    {
        get
        {
            if (instance)
                return instance;

            instance = FindFirstObjectByType<KeyboardController>(FindObjectsInactive.Include);
            
            if (!instance)
                instance = Instantiate(Resources.Load<KeyboardController>("Prefabs/FullKeyboard"));
            
            DontDestroyOnLoad(instance);
            
            return instance;
        }   
    }
    
    private void Awake()
    {
        Close(0);
    }

    public void Open(CustomInputField currentText)
    {
#if !UNITY_EDITOR
        if (DeviceChecker.DeviceType != DeviceType.Mobile)
            return;
#endif
        
        gameObject.SetActive(true);
        
        LeanTween.moveY(keyboard, 0, 0.2f).setEase(LeanTweenType.easeInOutCubic);
        
        ShowSmallLetters();

        if (_currentText)
            _currentText.HideCaret();
        
        _currentText = currentText;
    }

    public void Close(float time = 0.2f)
    {
        _currentText?.HideCaret();
        
        LeanTween.moveY(keyboard, -keyboard.sizeDelta.y, time).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    
    public void DeleteLetter()
    {
        if (_currentText.text.Length == 0) 
            return;
            
        if (_currentText.caretPosition == 0)
            return;
        
        int caretMove = _currentText.caretPosition == _currentText.text.Length ? 0 : 1;
        
        _currentText.text = _currentText.text.Remove(_currentText.caretPosition - 1, 1);
        _currentText.caretPosition -= caretMove;
    }

    public void AddLetter(string letter)
    {
        _currentText.text = _currentText.text.Insert(_currentText.caretPosition, letter);
        _currentText.caretPosition++;
    }

    public void SubmitWord()
    {
        Close();
    }

    public void ShowCapitalLetters()
    {
        isSmallLettersShown = false;

        actionNumbers.SetActive(true);
        actionSmallLetters.SetActive(false);
        actionCapitalLetters.SetActive(false);

        smallAlphaRow1.SetActive(false);
        smallAlphaRow2.SetActive(false);
        smallAlphaRow3.SetActive(false);

        capitalAlphaRow1.SetActive(true);
        capitalAlphaRow2.SetActive(true);
        capitalAlphaRow3.SetActive(true);

        numbers.SetActive(false);
        splCharsNum1.SetActive(false);
        splCharsNum2.SetActive(false);
        splChars1.SetActive(false);
        splChars2.SetActive(false);
    }

    public void ShowSmallLetters()
    {
        isSmallLettersShown = true;

        actionNumbers.SetActive(true);
        actionSmallLetters.SetActive(false);
        actionCapitalLetters.SetActive(false);

        capitalAlphaRow1.SetActive(false);
        capitalAlphaRow2.SetActive(false);
        capitalAlphaRow3.SetActive(false);

        smallAlphaRow1.SetActive(true);
        smallAlphaRow2.SetActive(true);
        smallAlphaRow3.SetActive(true);

        numbers.SetActive(false);
        splCharsNum1.SetActive(false);
        splCharsNum2.SetActive(false);
        splChars1.SetActive(false);
        splChars2.SetActive(false);
    }

    public void ShowSpecialCharsNum()
    {
        actionNumbers.SetActive(false);

        if (isSmallLettersShown)
        {
            actionSmallLetters.SetActive(true);
            actionCapitalLetters.SetActive(false);
        }
        else
        {
            actionSmallLetters.SetActive(false);
            actionCapitalLetters.SetActive(true);
        }

        smallAlphaRow1.SetActive(false);
        smallAlphaRow2.SetActive(false);
        smallAlphaRow3.SetActive(false);

        capitalAlphaRow1.SetActive(false);
        capitalAlphaRow2.SetActive(false);
        capitalAlphaRow3.SetActive(false);

        numbers.SetActive(true);
        splCharsNum1.SetActive(true);
        splCharsNum2.SetActive(true);

        splChars1.SetActive(false);
        splChars2.SetActive(false);
    }

    public void ShowSpecialChars()
    {
        actionNumbers.SetActive(false);

        if (isSmallLettersShown)
        {
            actionSmallLetters.SetActive(true);
            actionCapitalLetters.SetActive(false);
        }
        else
        {
            actionSmallLetters.SetActive(false);
            actionCapitalLetters.SetActive(true);
        }

        smallAlphaRow1.SetActive(false);
        smallAlphaRow2.SetActive(false);
        smallAlphaRow3.SetActive(false);

        capitalAlphaRow1.SetActive(false);
        capitalAlphaRow2.SetActive(false);
        capitalAlphaRow3.SetActive(false);

        numbers.SetActive(true);
        splCharsNum1.SetActive(false);
        splCharsNum2.SetActive(false);

        splChars1.SetActive(true);
        splChars2.SetActive(true);
    }
}