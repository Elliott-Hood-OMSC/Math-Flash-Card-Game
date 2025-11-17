using System;

public static class AchievementEvents
{
    #region Internal Achievement Events
    
    public static Action OnRefreshAllAchievements;
    
    public static Action<OnAchievementGetArgs> OnAchievementGet;
    public struct OnAchievementGetArgs
    {
        public Achievement AchievementObtained;
    }

    public static Action<OnTieredAchievementProgressedArgs> OnProgressUpdated;
    public struct OnTieredAchievementProgressedArgs
    {
        public TieredAchievement TieredAchievement;
    }
    
    #endregion
    
    #region Per-Project Events
    
    // NEW: ROUND TYPES
    public enum RoundProblemType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        All
    }

    public static Action OnQuestionClicked;
    public static Action OnSecondPassed;
    
    public static Action<OnRoundEndedArgs> OnRoundEnded;
    public struct OnRoundEndedArgs
    {
        public int NumQuestionsAnswered;
        public int NumCorrectQuestions;
        public float TotalTimeTaken;
        
        // NEW: what kind of round just ended
        public RoundProblemType RoundType;
    }
    
    public static Action<OnQuestionAnsweredArgs> OnQuestionAnswered;
    public struct OnQuestionAnsweredArgs
    {
        public bool AnsweredCorrectly;
        public float TimeRemaining;
    }
    
    #endregion
}