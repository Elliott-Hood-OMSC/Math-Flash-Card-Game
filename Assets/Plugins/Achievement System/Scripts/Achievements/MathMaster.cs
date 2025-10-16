// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(MathMaster), fileName = nameof(MathMaster))]
public class MathMaster : TieredAchievement
{
    public override void Subscribe()
    {
        AchievementEvents.OnQuestionAnswered += OnQuestionAnswered;
    }
    public override void Unsubscribe()
    {
        AchievementEvents.OnQuestionAnswered -= OnQuestionAnswered;
    }

    private void OnQuestionAnswered(AchievementEvents.OnQuestionAnsweredArgs obj)
    {
        if (obj.AnsweredCorrectly)
            IncrementProgress();
    }
}
