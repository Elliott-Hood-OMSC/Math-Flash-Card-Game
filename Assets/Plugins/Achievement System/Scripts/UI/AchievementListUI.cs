using UnityEngine;

public class AchievementListUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AchievementListItemUI achievementListItemUIPrefab;
    [SerializeField] private Transform achievementUiParent;
    [SerializeField] private RectTransform scrollRectViewport;

    private void Start()
    {
        ClearAchievementObjects(); // Clear achievements manually placed in editor
        PopulateAchievementsList();
    }

    private void ClearAchievementObjects()
    {
        foreach (Transform child in achievementUiParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void PopulateAchievementsList()
    {
        foreach (Achievement achievement in AchievementManager.Instance.Achievements)
        {
            AchievementListItemUI achievementUI = Instantiate(achievementListItemUIPrefab, achievementUiParent);
            achievementUI.TrackAchievement(achievement);
        }
    }
}
