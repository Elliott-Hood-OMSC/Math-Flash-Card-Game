using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Card : MonoBehaviour
{
    private Image _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
