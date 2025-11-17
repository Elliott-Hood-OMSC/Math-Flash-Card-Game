using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonQuestionType : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _typeLabel;
    [SerializeField] private GameObject _selectedHighlight;
    public Button Button => _button;
    public AchievementEvents.RoundProblemType Type { get; private set; }

    public void SetupForType(AchievementEvents.RoundProblemType roundProblemType)
    {
        Type = roundProblemType;
        _typeLabel.text = roundProblemType.ToString();
    }

    public void SetSelected(bool selected)
    {
        _selectedHighlight.SetActive(selected);
    }
}