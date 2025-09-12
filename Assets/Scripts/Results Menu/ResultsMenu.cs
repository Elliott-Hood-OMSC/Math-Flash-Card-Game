using TMPro;
using UnityEngine;

public class ResultsMenu : Menu
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    public void SetScore(int score, int numQuestions)
    {
        _scoreText.text = $"{score}/{numQuestions} correct!";
    }
}
