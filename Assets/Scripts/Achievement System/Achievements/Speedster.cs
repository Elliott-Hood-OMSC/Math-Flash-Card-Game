using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(Speedster), fileName = nameof(Speedster))]
public class Speedster : Achievement
{
    [SerializeField] private int _requiredTime = 10;
    
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
        if (obj.TotalTimeTaken <= _requiredTime && obj.NumCorrectQuestions == obj.NumQuestionsAnswered)
        {
            GetAchievement();
        }
    }
}
