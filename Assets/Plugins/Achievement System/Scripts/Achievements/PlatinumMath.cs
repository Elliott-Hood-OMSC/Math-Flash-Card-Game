using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(PlatinumMath), fileName = nameof(PlatinumMath))]
public class PlatinumMath : Achievement
{
    public override void Subscribe()
    {
        AchievementEvents.OnAchievementGet += OnAchievementGet;
        AchievementEvents.OnTieredAchievementProgressUpdated += OnTieredAchievementProgressed;
    }

    public override void Unsubscribe()
    {
        AchievementEvents.OnAchievementGet -= OnAchievementGet;
        AchievementEvents.OnTieredAchievementProgressUpdated -= OnTieredAchievementProgressed;
    }

    private void OnTieredAchievementProgressed(AchievementEvents.OnTieredAchievementArgs obj)
    {
        CheckIfCompleted();
    }

    private void OnAchievementGet(AchievementEvents.OnAchievementGetArgs obj)
    {
        CheckIfCompleted();
    }

    private void CheckIfCompleted()
    {
        foreach (Achievement achievement in AchievementManager.Instance.Achievements)
        {
            if (achievement == this) 
                continue;
            
            if (!achievement.IsMaxed)
                return;
        }

        GetAchievement();
    }
}
