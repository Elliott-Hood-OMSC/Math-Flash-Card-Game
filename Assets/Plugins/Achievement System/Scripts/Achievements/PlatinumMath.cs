// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(PlatinumMath), fileName = nameof(PlatinumMath))]
public class PlatinumMath : Achievement
{
    public override void Subscribe()
    {
        AchievementEvents.OnAchievementGet += OnAchievementGet;
        AchievementEvents.OnTieredAchievementProgressUpdated += OnTieredAchievementUpdated;
        AchievementEvents.OnTieredAchievementTierIncrease += OnTieredAchievementUpdated;
    }

    public override void Unsubscribe()
    {
        AchievementEvents.OnAchievementGet -= OnAchievementGet;
        AchievementEvents.OnTieredAchievementProgressUpdated -= OnTieredAchievementUpdated;
        AchievementEvents.OnTieredAchievementTierIncrease -= OnTieredAchievementUpdated;
    }

    private void OnTieredAchievementUpdated(AchievementEvents.OnTieredAchievementArgs obj)
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
