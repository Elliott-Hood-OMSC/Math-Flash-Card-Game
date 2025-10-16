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

    public static Action<OnTieredAchievementArgs> OnTieredAchievementTierIncrease;
    public static Action<OnTieredAchievementArgs> OnTieredAchievementProgressUpdated;
    public struct OnTieredAchievementArgs
    {
        public TieredAchievement TieredAchievement;
    }
    
    #endregion
    
    #region Per-Project Events

    public static Action OnQuestionClicked;
    public static Action OnMinutePassed;
    
    public static Action<OnRoundEndedArgs> OnRoundEnded;
    public struct OnRoundEndedArgs
    {
        public int NumQuestionsAnswered;
        public int NumCorrectQuestions;
        public float TotalTimeTaken;
    }
    
    public static Action<OnQuestionAnsweredArgs> OnQuestionAnswered;
    public struct OnQuestionAnsweredArgs
    {
        public bool AnsweredCorrectly;
        public float TimeRemaining;
    }
    
    #endregion
}