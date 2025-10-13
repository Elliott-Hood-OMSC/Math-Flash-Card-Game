using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : Menu
{
    [SerializeField] private Countdown _countdown;
    [SerializeField] private Button _startButton;
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
        _startButton.onClick.AddListener(StartCountdown);
    }

    public void StartCountdown()
    {
        _startButton.interactable = false;
        _achievementsButton.interactable = false;
        _countdown.StartCountdown();
    }
}
