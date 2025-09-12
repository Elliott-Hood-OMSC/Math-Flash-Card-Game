using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestionPresenter : MonoBehaviour
{
    private const int NUM_QUESTIONS = 3;

    [SerializeField] private Sprite[] _cardSprites;
    [SerializeField] private Card _leftCard;
    [SerializeField] private Card _rightCard;
    [SerializeField] private List<AnswerCard> _answerCards;
    private AnswerCard _correctCard;

    public readonly UnityEvent OnAnsweredCorrectly = new UnityEvent();
    public readonly UnityEvent OnAnsweredIncorrectly = new UnityEvent();
    
    private void Awake()
    {
        foreach (AnswerCard card in _answerCards)
        {
            card.OnClick.AddListener(() => OnAnswerCardClicked(card));
        }
    }

    private void OnAnswerCardClicked(AnswerCard card)
    {
        if (card == _correctCard)
        {
            OnAnsweredCorrectly.Invoke();
        }
        else
        {
            OnAnsweredIncorrectly.Invoke();
        }
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

        _leftCard.SetSprite(GetSpriteForInt(finalNum1));
        _rightCard.SetSprite(GetSpriteForInt(finalNum2));
    }

    private Sprite GetSpriteForInt(int value)
    {
        int numSuites = 4;
        
        int suitOffset = Random.Range(0, numSuites);
        int rankIndex = (value - 1) * numSuites;
        return _cardSprites[rankIndex + suitOffset];
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
