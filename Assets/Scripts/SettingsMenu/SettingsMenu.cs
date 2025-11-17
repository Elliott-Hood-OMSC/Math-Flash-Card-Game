using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : Menu
{
    [SerializeField] private TextMeshProUGUI _numQuestionsLabel;
    [SerializeField] private Slider _numQuestions;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _submitSettingsButton;
    [SerializeField] private SettingsButtonQuestionType[] _buttons;
    private SettingsButtonQuestionType _selectedButton;
    public Action OnSubmitSettingsAction { get; set; }

    private void Awake()
    {
        _numQuestions.minValue = 3;
        _numQuestions.maxValue = 5;
        _numQuestions.wholeNumbers = true;
        _numQuestions.onValueChanged.AddListener((float value) =>
        {
            _numQuestionsLabel.text = $"Num Questions: {value}";
        });
        _backButton.onClick.AddListener(OnClickBack);
        _submitSettingsButton.onClick.AddListener(OnClickStartGame);
        
        for (int i = 0; i < _buttons.Length; i++)
        {
            SettingsButtonQuestionType button = _buttons[i];
            AchievementEvents.RoundProblemType type = (AchievementEvents.RoundProblemType)i;

            button.SetupForType(type);
            button.Button.onClick.AddListener(() => OnButtonTypeClick(button));
        }
        SelectButton(_buttons[0]);
    }

    private void OnClickBack()
    {
        SetVisible(false);
    }

    private void OnClickStartGame()
    {
        GameController.Instance.GameSettings.QuestionsType = _selectedButton.Type;
        GameController.Instance.GameSettings.NumQuestions = (int)_numQuestions.value;
        OnSubmitSettingsAction?.Invoke();
        SetVisible(false);
    }

    private void OnButtonTypeClick(SettingsButtonQuestionType button)
    {
        if (_selectedButton != null && _selectedButton != button) _selectedButton.SetSelected(false);

        SelectButton(button);
    }

    private void SelectButton(SettingsButtonQuestionType button)
    {
        _selectedButton = button;
        _selectedButton.SetSelected(true);
    }

    private void OnDestroy()
    {
        foreach (SettingsButtonQuestionType button in _buttons)
        {
            button?.Button?.onClick.RemoveAllListeners();
        }
    }
}