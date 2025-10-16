using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(Mathematician), fileName = nameof(Mathematician))]
public class Mathematician : TieredAchievement
{
    public override void Subscribe()
    {
        AchievementEvents.OnMinutePassed += OnMinutePassed;
    }
    public override void Unsubscribe()
    {
        AchievementEvents.OnMinutePassed -= OnMinutePassed;
    }

    private void OnMinutePassed()
    {
        IncrementProgress();
    }
}
