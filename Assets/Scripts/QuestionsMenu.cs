using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionsMenu : Menu
{
    [SerializeField] private QuestionPresenter _questionPresenter;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private int _numQuestions = 3;
    [SerializeField] private int _timeLimit = 30;
    private Coroutine _countdownCoroutine;
    private int _questionsPresented;
    private int _correctQuestionCount;

    private void Awake()
    {
        _questionPresenter.OnAnsweredCorrectly.AddListener(() =>
        {
            _correctQuestionCount++;
            TryPresentNextQuestion();
        });
        _questionPresenter.OnAnsweredIncorrectly.AddListener(() =>
        {
            TryPresentNextQuestion();
        });
    }

    public override void SetVisible(bool visible)
    {
        base.SetVisible(visible);

        if (visible)
        {
            PresentAllQuestions();
        }
        else
        {
            if (_countdownCoroutine != null)
                StopCoroutine(_countdownCoroutine);
        }
    }

    private void PresentAllQuestions()
    {
        _questionsPresented = 0;
        _correctQuestionCount = 0;
        TryPresentNextQuestion();
    }

    private void TryPresentNextQuestion()
    {
        if (_questionsPresented >= _numQuestions)
        {
            GameController.Instance.EndGame(_correctQuestionCount, _questionsPresented);
            return;
        }
        
        _countdownCoroutine = StartCoroutine(Countdown());
        _questionPresenter.PresentQuestion();

        _questionsPresented++;
    }

    private IEnumerator Countdown()
    {
        for (int i = _timeLimit; i > 0; i--)
        {
            _timerText.text = $"TimeRemaining: {i}";
            yield return new WaitForSeconds(1);
        }
        TryPresentNextQuestion();
    }
}
