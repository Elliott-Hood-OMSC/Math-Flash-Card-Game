using UnityEngine;

[CreateAssetMenu(
    menuName = "Achievements/" + nameof(PerfectRoundAchievement),
    fileName = nameof(PerfectRoundAchievement))]
public class PerfectRoundAchievement : Achievement
{
    [Header("Perfect Round Settings")]
    public AchievementEvents.RoundProblemType targetRoundType;

    public override void Subscribe()
    {
        // listen for end-of-round
        AchievementEvents.OnRoundEnded += OnRoundEnded;
    }

    public override void Unsubscribe()
    {
        AchievementEvents.OnRoundEnded -= OnRoundEnded;
    }

    private void OnRoundEnded(AchievementEvents.OnRoundEndedArgs args)
    {
        if (HasAchievement) return;                              // already got it
        if (args.RoundType != targetRoundType) return;           // wrong mode
        if (args.NumQuestionsAnswered <= 0) return;              // ignore empty rounds
        if (args.NumCorrectQuestions != args.NumQuestionsAnswered) return; // not perfect

        GetAchievement();                                        // unlock!
    }
}