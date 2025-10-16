// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(Mathematician), fileName = nameof(Mathematician))]
public class Mathematician : TieredAchievement
{
    public override void Subscribe()
    {
        AchievementEvents.OnSecondPassed += OnSecondPassed;
    }
    public override void Unsubscribe()
    {
        AchievementEvents.OnSecondPassed -= OnSecondPassed;
    }

    public override int GetProgressValue()
    {
        return base.GetProgressValue() / 60;
    }

    public override int GetTierRequirement()
    {
        return base.GetTierRequirement() / 60;
    }

    private void OnSecondPassed()
    {
        IncrementProgress();
    }
}
