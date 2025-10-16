// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

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
