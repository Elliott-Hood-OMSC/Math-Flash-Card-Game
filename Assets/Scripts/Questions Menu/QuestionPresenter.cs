using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionPresenter : MonoBehaviour
{
    private const int NUM_QUESTIONS = 3;

    [SerializeField] private Card _leftCard;
    [SerializeField] private Card _rightCard;
    [SerializeField] private List<AnswerCard> _answerCards;
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

    public void PresentQuestion()
    {
        ShuffleList(_answerCards);

        HashSet<int> usedValues = new HashSet<int>();

        int finalNum1 = 0;
        int finalNum2 = 0;

        for (int index = 0; index < _answerCards.Count; index++)
        {
            int num1, num2, product;

            // Keep generating until we find a unique product
            do
            {
                num1 = Random.Range(1, 13);
                num2 = Random.Range(1, 13);
                product = num1 * num2;
            }
            while (usedValues.Contains(product));

            usedValues.Add(product);

            _answerCards[index].SetValue(product);

            // Assign the last one as the correct card
            if (index == _answerCards.Count - 1)
            {
                _correctCard = _answerCards[index];
                finalNum1 = num1;
                finalNum2 = num2;
            }
        }

        _leftCard.SetCardValue(finalNum1);
        _rightCard.SetCardValue(finalNum2);
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
