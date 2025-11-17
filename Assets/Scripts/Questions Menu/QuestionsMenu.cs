// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System.Collections;
using CommandPattern;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the question presenter and timer. Presents a certain number of questions, each with their own timer and ends the game upon completion.
/// A CommandInvoker is used to display the questions. 
/// </summary>
public class QuestionsMenu : Menu
{
    [SerializeField] private QuestionPresenter _questionPresenter;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private int _numQuestions = 3;
    [SerializeField] private int _timeLimit = 10;
    private Coroutine _countdownCoroutine;
    private int _questionsAnswered;
    private int _correctQuestionCount;
    private int _timer;
    private QuestionGenerator _displayQuestionCommand;
    
    public CommandInvoker CommandInvoker { get; } = new CommandInvoker();

    private float _timeStarted;
    
    private void Awake()
    {
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

    private void SetupQuestionGeneratorForRoundProblemType()
    {
        switch (GameController.Instance.GameSettings.QuestionsType)
        {
            case AchievementEvents.RoundProblemType.Addition:
                _displayQuestionCommand = new QuestionGeneratorAddition(_questionPresenter);
                break;
            case AchievementEvents.RoundProblemType.Subtraction:
                _displayQuestionCommand = new QuestionGeneratorSubtraction(_questionPresenter);
                break;
            case AchievementEvents.RoundProblemType.Multiplication:
                _displayQuestionCommand = new QuestionGeneratorMultiplication(_questionPresenter);
                break;
            case AchievementEvents.RoundProblemType.Division:
                _displayQuestionCommand = new QuestionGeneratorDivision(_questionPresenter);
                break;
            case AchievementEvents.RoundProblemType.All:
                _displayQuestionCommand = new QuestionGeneratorAll(_questionPresenter);
                break;
            default:
                _displayQuestionCommand = new QuestionGeneratorMultiplication(_questionPresenter);
                break;
        }
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
        SetupQuestionGeneratorForRoundProblemType();
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
                RoundType = GameController.Instance.GameSettings.QuestionsType
            });
            GameController.Instance.EndGame(_correctQuestionCount, _questionsAnswered);
            return;
        }
        
        _countdownCoroutine = StartCoroutine(Countdown());
        
        CommandInvoker.ExecuteCommand(_displayQuestionCommand);
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
