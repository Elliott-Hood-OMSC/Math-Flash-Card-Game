// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// A simple card view that caches rank and suit, and listens to the DeckManager for changes in the deck to update visuals properly.
/// </summary>
[RequireComponent(typeof(Image))]
public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _jokerNumberText;
    private Image _spriteRenderer;
    private int _rank;
    private int _suit;

    private void Awake()
    {
        _spriteRenderer = GetComponent<Image>();
    }

    private void Start()
    {
        GameController.Instance.DeckManager.OnDeckChange.AddListener(UpdateVisuals);
    }

    public void SetCardValue(int cardRank)
    {
        _rank = cardRank;
        _suit = Random.Range(0, 4);
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        Deck deck = GameController.Instance.DeckManager.CurrentDeck;
        Sprite randomCardSprite = deck.GetCard(_rank, _suit);
        if (randomCardSprite == null)
        {
            _spriteRenderer.sprite = deck.JokerSprite;
            _jokerNumberText.text = _rank.ToString();
        }
        else
        {
            _jokerNumberText.text = String.Empty;
            _spriteRenderer.sprite = randomCardSprite;
        }
    }
}
