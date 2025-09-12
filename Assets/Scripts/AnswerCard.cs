using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnswerCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private Button _button;
    public readonly UnityEvent OnClick = new UnityEvent();
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnClick?.Invoke();
    }

    public void SetValue(int value)
    {
        _text.text = value.ToString();
    }
}
