// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains the button to start the countdown and prevents opening the achievements menu while the countdown is active.
/// </summary>
public class TitleScreen : Menu
{
    [SerializeField] private Countdown _countdown;
    [SerializeField] private Button _startButton;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private Button _achievementsButton;
    
    public override void SetVisible(bool visible)
    {
        base.SetVisible(visible);
        _startButton.interactable = visible;
        _achievementsButton.interactable = visible;
        _countdown.ResetCountdownText();
    }
    
    private void Awake()
    {
        _startButton.onClick.AddListener(OnClickStartButton);
    }

    private void OnClickStartButton()
    {
        _settingsMenu.SetVisible(true);
        _settingsMenu.OnSubmitSettingsAction = StartCountdown;
    }

    public void StopCountdown()
    {
        _startButton.interactable = true;
        _achievementsButton.interactable = true;
        _countdown.StopCountdown();
    }

    private void StartCountdown()
    {
        _startButton.interactable = false;
        _achievementsButton.interactable = false;
        _countdown.StartCountdown();
    }
}
