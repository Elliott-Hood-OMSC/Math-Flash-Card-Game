// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

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
