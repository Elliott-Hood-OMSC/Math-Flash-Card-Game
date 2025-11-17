// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(Huh), fileName = nameof(Huh))]
public class Huh : Achievement
{
    public override void Subscribe()
    {
        AchievementEvents.OnQuestionClicked += OnQuestionClicked;
    }

    public override void Unsubscribe()
    {
        AchievementEvents.OnQuestionClicked -= OnQuestionClicked;
    }

    private void OnQuestionClicked()
    {
        GetAchievement();
    }
}
