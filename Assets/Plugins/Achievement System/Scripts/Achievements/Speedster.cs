// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(Speedster), fileName = nameof(Speedster))]
public class Speedster : Achievement
{
    [SerializeField] private int _timeRequirement = 10;
    public override void Subscribe()
    {
        AchievementEvents.OnRoundEnded += OnRoundEnded;
    }
    public override void Unsubscribe()
    {
        AchievementEvents.OnRoundEnded -= OnRoundEnded;
    }

    private void OnRoundEnded(AchievementEvents.OnRoundEndedArgs obj)
    {
        if (obj.TotalTimeTaken <= _timeRequirement && obj.NumCorrectQuestions == obj.NumQuestionsAnswered) 
            GetAchievement();
    }
}
