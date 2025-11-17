using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _submitSettingsButton;
    [SerializeField] private SettingsButtonQuestionType[] _buttons;
    private SettingsButtonQuestionType _selectedButton;
    public Action OnSubmitSettingsAction { get; set; }

    private void Awake()
    {
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