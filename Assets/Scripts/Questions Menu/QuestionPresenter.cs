// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPresenter : MonoBehaviour
{
    [SerializeField] private Card _leftCard;
    [SerializeField] private Card _rightCard;
    [SerializeField] private List<AnswerCard> _answerCards;
    public int NumAnswerCards => _answerCards.Count;
    private AnswerCard _correctCard;

    /// <summary>
    /// Passes true if answered correctly
    /// </summary>
    public event Action<bool> OnAnswered;
    
    private void Awake()
    {
        foreach (AnswerCard card in _answerCards)
        {
            card.OnClick.AddListener(() => OnAnswerCardClicked(card));
        }
    }

    private void OnAnswerCardClicked(AnswerCard card)
    {
        OnAnswered?.Invoke(card == _correctCard);
    }

    public void PresentQuestion(QuestionInfo questionInfo)
    {
        ShuffleList(_answerCards);

        for (int i = 0; i < _answerCards.Count; i++)
        {
            // There are n - 1 wrong answers, and 1 correct answer. Assign them accordingly
            if (i < _answerCards.Count - 1)
            {
                _answerCards[i].SetValue(questionInfo.WrongAnswers[i]);
            }
            else
            {
                _answerCards[i].SetValue(questionInfo.CorrectAnswer);
                _correctCard = _answerCards[i];
            }
        }

        _leftCard.SetCardValue(questionInfo.QuestionDisplay.Numbers[0]);
        _rightCard.SetCardValue(questionInfo.QuestionDisplay.Numbers[1]);
    }
    
    public static void ShuffleList<T>(IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}
