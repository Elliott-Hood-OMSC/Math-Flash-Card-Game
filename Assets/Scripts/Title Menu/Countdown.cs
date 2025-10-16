// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private float _secondTime = 0.7f;
    [SerializeField] private TextMeshProUGUI _countdownText;
    private Coroutine _currentCountdown;
    private Vector2 _originalTextAnchorPos;

    private void Awake()
    {
        _originalTextAnchorPos = _countdownText.rectTransform.anchoredPosition;
    }

    public void StartCountdown()
    {
        StopCountdown();

        _currentCountdown = StartCoroutine(CountdownCoroutine());
    }

    public void StopCountdown()
    {
        if (_currentCountdown != null)
            StopCoroutine(_currentCountdown);
    }

    private IEnumerator CountdownCoroutine()
    {
        void Shake()
        {
            DOTween.Sequence()
                .Append(_countdownText.rectTransform.DOShakeAnchorPos(
                    0.2f,
                    10,
                    vibrato: 30))
                .Append(_countdownText.rectTransform.DOAnchorPos(_originalTextAnchorPos, 0.05f)); // smooth reset
        }
        
        for (int i = 5; i > 0; i--)
        {
            _countdownText.text = i.ToString();
            Shake();
            yield return new WaitForSeconds(_secondTime);
        }
        _countdownText.text = "Go!";
        Shake();
        yield return new WaitForSeconds(_secondTime);
        GameController.Instance.StartGame();
    }

    public void ResetCountdownText()
    {
        _countdownText.text = "";
    }
}
