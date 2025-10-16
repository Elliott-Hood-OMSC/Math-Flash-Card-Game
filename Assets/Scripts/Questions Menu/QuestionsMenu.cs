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
    private int _questionsAnswered;
    private int _correctQuestionCount;
    private int _timer;
    private QuestionGenerator _displayQuestionCommand;

    private float _timeStarted;
    
    private void Awake()
    {
        _displayQuestionCommand = new QuestionGeneratorMultiplication(_questionPresenter);
        _questionPresenter.OnAnswered += (bool answeredCorrectly) =>
        {
            _questionsAnswered++;
            if (answeredCorrectly)
            {
                _correctQuestionCount++;
            }
            
            AchievementEvents.OnQuestionAnswered?.Invoke(new AchievementEvents.OnQuestionAnsweredArgs
            {
                AnsweredCorrectly = answeredCorrectly,
                TimeRemaining = _timer
            });
            
            TryPresentNextQuestion();
        };
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
            if (_countdownCoroutine != null) StopCoroutine(_countdownCoroutine);
        }
    }

    private void PresentAllQuestions()
    {
        _questionsAnswered = 0;
        _correctQuestionCount = 0;
        _timeStarted = Time.time;
        TryPresentNextQuestion();
    }

    private void TryPresentNextQuestion()
    {
        if (_countdownCoroutine != null) StopCoroutine(_countdownCoroutine);
        
        if (GameHasEnded())
        {
            AchievementEvents.OnRoundEnded?.Invoke(new AchievementEvents.OnRoundEndedArgs
            {
                NumQuestionsAnswered = _questionsAnswered,
                NumCorrectQuestions = _correctQuestionCount,
                TotalTimeTaken = Time.time - _timeStarted,
            });
            GameController.Instance.EndGame(_correctQuestionCount, _questionsAnswered);
            return;
        }
        
        _countdownCoroutine = StartCoroutine(Countdown());
        _displayQuestionCommand.Execute();
    }

    private bool GameHasEnded()
    {
        return _questionsAnswered >= _numQuestions;
    }

    private IEnumerator Countdown()
    {
        for (int i = _timeLimit; i > 0; i--)
        {
            _timer = i;
            _timerText.text = $"TimeRemaining: {i}";
            yield return new WaitForSeconds(1);
        }
        TryPresentNextQuestion();
    }
}
