// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The single entry-point with high-level game functions.
/// - Handles opening and closing menus mainly
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public DeckManager DeckManager;
    
    [SerializeField] private TitleScreen _titleScreenPanel;
    [SerializeField] private QuestionsMenu _questionsPanel;
    [SerializeField] private ResultsMenu _resultsPanel;
    
    [Header("Options Buttons")]
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _deckTypeButton;
    [SerializeField] private TextMeshProUGUI _deckTypeText;

    public GameSettings GameSettings { get; private set; } = new GameSettings();
    private readonly StateMachine _stateMachine = new StateMachine();
    private MainMenuState _mainMenuState;
    private GameState _gameState;
    private ResultsState _resultsState;

    private void Awake()
    {
        #region Singleton
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        #endregion

        InitializeButtonListeners();
        InitializeStateMachine();
        
        ReturnToTitleScreen();
    }

    private void InitializeButtonListeners()
    {
        _quitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        _restartButton.onClick.AddListener(() =>
        {
            ReturnToTitleScreen();
            _titleScreenPanel.StopCountdown();
        });
        _deckTypeButton.onClick.AddListener(() =>
        {
            DeckManager.ToggleDeck();
            _deckTypeText.text = DeckManager.CurrentDeck.DeckName + " Deck";
        });
    }

    private void InitializeStateMachine()
    {
        _mainMenuState = new MainMenuState
        {
            TitleScreenPanel = _titleScreenPanel
        };
        _gameState = new GameState
        {
            QuestionsPanel = _questionsPanel
        };
        _resultsState = new ResultsState
        {
            ResultsPanel = _resultsPanel
        };
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void StartGame()
    {
        _stateMachine.ChangeState(_gameState);
    }

    public void EndGame(int score, int numQuestions)
    {
        _resultsPanel.SetScore(score, numQuestions);
        _stateMachine.ChangeState(_resultsState);
    }

    private void ReturnToTitleScreen()
    {
        _stateMachine.ChangeState(_mainMenuState);
    }

    private class MainMenuState : IState
    {
        public Menu TitleScreenPanel { get; set; }

        public void Enter()
        {
            TitleScreenPanel.SetVisible(true);
        }

        public void Exit()
        {
            TitleScreenPanel.SetVisible(false);
        }
    }

    private class GameState : IState
    {
        public Menu QuestionsPanel { get; set; }

        public void Enter()
        {
            QuestionsPanel.SetVisible(true);
            AchievementRoundProgressTracker.Instance.BeginRound();
        }

        public void Exit()
        {
            QuestionsPanel.SetVisible(false);
        }
    }

    private class ResultsState : IState
    {
        public Menu ResultsPanel { get; set; }

        public void Enter()
        {
            ResultsPanel.SetVisible(true);
        }

        public void Exit()
        {
            ResultsPanel.SetVisible(false);
        }
    }
}
