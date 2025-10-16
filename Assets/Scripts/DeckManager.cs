// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;
using UnityEngine.Events;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Deck _defaultDeck;
    [SerializeField] private Deck _numberedDeck;
    public Deck CurrentDeck { get; private set; }
    public readonly UnityEvent OnDeckChange = new UnityEvent();

    private void Awake()
    {
        ChangeDeck(_defaultDeck);
    }

    public void ToggleDeck()
    {
        if (CurrentDeck == _defaultDeck)
        {
            ChangeDeck(_numberedDeck);
        }
        else
        {
            ChangeDeck(_defaultDeck);
        }
    }
    
    private void ChangeDeck(Deck newDeck)
    {
        if (CurrentDeck == newDeck)
            return;
        
        CurrentDeck = newDeck;
        OnDeckChange.Invoke();
    }
}

[System.Serializable]
public class Deck
{
    public string DeckName;
    public Sprite[] CardSprites;
    
    const int NUM_SUITES = 4;

    public Sprite GetCard(int rank, int suit)
    {
        int rankIndex = (rank - 1) * NUM_SUITES;

        return CardSprites[rankIndex + suit];
    }
}
