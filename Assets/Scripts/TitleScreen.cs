using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : Menu
{
    [SerializeField] private Countdown _countdown;
    [SerializeField] private Button _startButton;
    
    public override void SetVisible(bool visible)
    {
        base.SetVisible(visible);
        _startButton.interactable = visible;
        _countdown.ResetCountdownText();
    }
    
    private void Awake()
    {
        _startButton.onClick.AddListener(StartCountdown);
    }

    public void StartCountdown()
    {
        _startButton.interactable = false;
        _countdown.StartCountdown();
    }
}
