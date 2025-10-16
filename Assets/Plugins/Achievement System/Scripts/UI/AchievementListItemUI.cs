using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementListItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;
    [SerializeField] private Image panel;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Slider progressSlider;

    [Header("Visuals")]
    [SerializeField] private Color NotUnlockedColor;
    [SerializeField] private Color NormalColor;
    [SerializeField] private Color MaxedColor;

    private CanvasGroup _canvasGroup;
    private Achievement _trackedAchievement;

    private void Awake()
    {
        AchievementEvents.OnRefreshAllAchievements += OnRefreshAllAchievements;
        AchievementEvents.OnAchievementGet += OnAchievementGet;
        AchievementEvents.OnTieredAchievementProgressUpdated += OnTieredAchievementProgressed;
    }

    private void OnDestroy()
    {
        AchievementEvents.OnRefreshAllAchievements -= OnRefreshAllAchievements;
        AchievementEvents.OnAchievementGet -= OnAchievementGet;
        AchievementEvents.OnTieredAchievementProgressUpdated -= OnTieredAchievementProgressed;
    }

    private void OnTieredAchievementProgressed(AchievementEvents.OnTieredAchievementArgs obj)
    {
        UpdateUI();
    }

    private void OnAchievementGet(AchievementEvents.OnAchievementGetArgs obj)
    {
        UpdateUI();
    }

    private void OnRefreshAllAchievements()
    {
        UpdateUI();
    }

    public void TrackAchievement(Achievement achievement)
    {
        _trackedAchievement = achievement;
        UpdateUI();
    }

    private void UpdateUI()
    {
        titleText.text = _trackedAchievement.AchievementTitle;
        descriptionText.text = _trackedAchievement.AchievementDescription;
        icon.sprite = _trackedAchievement.AchievementThumbnail;

        panel.color = _trackedAchievement.HasAchievement ? NormalColor : NotUnlockedColor;

        if (_trackedAchievement is TieredAchievement)
        {
            TieredAchievement tieredAchievement = _trackedAchievement as TieredAchievement;
            progressSlider.value = tieredAchievement.GetProgressPercentage();
            progressText.text = $"{tieredAchievement.GetProgressValue()} / {tieredAchievement.GetTierRequirement()}";
            if (tieredAchievement.IsMaxed) { panel.color = MaxedColor; }
        }
        else
        {
            progressSlider.value = _trackedAchievement.HasAchievement ? 1.0f : 0.0f;
            int progress = _trackedAchievement.HasAchievement ? 1 : 0;
            progressText.text = $"{progress} / {1}";
            if (_trackedAchievement.HasAchievement) { panel.color = MaxedColor; }
        }
    }
}
