using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(BuzzerBeater), fileName = nameof(BuzzerBeater))]
public class BuzzerBeater : Achievement
{
    public override void Subscribe()
    {
        AchievementEvents.OnQuestionAnswered += OnQuestionAnswered;
    }
    public override void Unsubscribe()
    {
        AchievementEvents.OnQuestionAnswered -= OnQuestionAnswered;
    }

    private void OnQuestionAnswered(AchievementEvents.OnQuestionAnsweredArgs args)
    {
        if (args.AnsweredCorrectly && args.TimeRemaining <= 1) 
            GetAchievement();
    }
}
