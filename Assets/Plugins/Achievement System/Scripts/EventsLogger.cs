// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

/// <summary>
/// A logger class for debugging achievementEvents
/// </summary>
public class AchievementEventsLogger : MonoBehaviour
{
    [Header("Logging Options")]
    [SerializeField] private bool _logAchievementGet = true;
    [SerializeField] private bool _logTieredAchievementProgressed = true;
    [SerializeField] private bool _logQuestionAnswered = true;
    [SerializeField] private bool _logRoundEnded = true;
    [SerializeField] private bool _logHuhButton = true;

    private void Awake()
    {
        AchievementEvents.OnAchievementGet += (AchievementEvents.OnAchievementGetArgs args) =>
        {
            if (!_logAchievementGet) return;
            Debug.Log($"{args.AchievementObtained.AchievementTitle} Achieved!");
        };

        AchievementEvents.OnTieredAchievementProgressUpdated += (AchievementEvents.OnTieredAchievementArgs args) =>
        {
            if (!_logTieredAchievementProgressed) return;
            string progress = $"{args.TieredAchievement.GetProgressValue()} / {args.TieredAchievement.GetTierRequirement()}";
            Debug.Log($"{args.TieredAchievement.AchievementTitle} was progressed! Now it is {progress}");
        };

        AchievementEvents.OnQuestionAnswered += (AchievementEvents.OnQuestionAnsweredArgs args) =>
        {
            if (!_logQuestionAnswered) return;
            string correctText = args.AnsweredCorrectly ? "correctly" : "incorrectly";
            Debug.Log($"Question Answered! Answered {correctText} with {args.TimeRemaining} seconds remaining!");
        };

        AchievementEvents.OnRoundEnded += (AchievementEvents.OnRoundEndedArgs args) =>
        {
            if (!_logRoundEnded) return;
            string questionsCorrectRatio = $"{args.NumCorrectQuestions} / {args.NumQuestionsAnswered}";
            Debug.Log($"Round Ended! Answered {questionsCorrectRatio} questions correctly in a total of {args.TotalTimeTaken} seconds!");
        };

        AchievementEvents.OnQuestionClicked += () =>
        {
            if (!_logHuhButton) return;
            Debug.Log("Huh?");
        };
    }
}