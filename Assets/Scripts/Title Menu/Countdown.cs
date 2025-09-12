using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;
    private Coroutine _currentCountdown;
    
    public void StartCountdown()
    {
        if (_currentCountdown != null)
            StopCoroutine(_currentCountdown);
        
        _currentCountdown = StartCoroutine(CountdownCoroutine());
    }
    
    private IEnumerator CountdownCoroutine()
    {
        for (int i = 5; i > 0; i--)
        {
            _countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        _countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        GameController.Instance.StartGame();
    }

    public void ResetCountdownText()
    {
        _countdownText.text = "";
    }
}
