// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System;

/// <summary>
/// A static class with events that achievements subscribe to.
/// Contains internal and external events (internal are within the system, external are game-specific)
/// </summary>
public static class AchievementEvents
{
    #region Internal Achievement Events
    
    public static Action OnRefreshAllAchievements;
    
    public static Action<OnAchievementGetArgs> OnAchievementGet;
    public struct OnAchievementGetArgs
    {
        public Achievement AchievementObtained;
    }

    public static Action<OnTieredAchievementArgs> OnTieredAchievementTierIncrease;
    public static Action<OnTieredAchievementArgs> OnTieredAchievementProgressUpdated;
    public struct OnTieredAchievementArgs
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