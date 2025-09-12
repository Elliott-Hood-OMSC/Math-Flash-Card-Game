using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        ReturnToTitleScreen();
        
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
            _titleScreenPanel.StartCountdown();
        });
        _deckTypeButton.onClick.AddListener(() =>
        {
            DeckManager.ToggleDeck();
            _deckTypeText.text = DeckManager.CurrentDeck.DeckName + " Deck";
        });
    }

    public void StartGame()
    {
        _titleScreenPanel.SetVisible(false);
        _questionsPanel.SetVisible(true);
        _resultsPanel.SetVisible(false);
    }

    public void EndGame(int score, int numQuestions)
    {
        _resultsPanel.SetScore(score, numQuestions);
        
        _titleScreenPanel.SetVisible(false);
        _questionsPanel.SetVisible(false);
        _resultsPanel.SetVisible(true);
    }

    public void ReturnToTitleScreen()
    {
        _titleScreenPanel.SetVisible(true);
        _questionsPanel.SetVisible(false);
        _resultsPanel.SetVisible(false);
    }
}
