using System;

public static class AchievementEvents
{
    // On Achievement Get
    public static Action<OnAchievementGetArgs> OnAchievementGet;
    public class OnAchievementGetArgs : AchievementArgs
    {
        public Achievement AchievementObtained;
    }
    
    // On Round Ended 
    public static Action<OnRoundEndedArgs> OnRoundEnded;
    public class OnRoundEndedArgs : AchievementArgs
    {
        public int NumQuestionsAnswered;
        public int NumQuestionsAnsweredCorrectly;
        public float Time;
    }
    
    // On Question Answered
    public static Action<OnQuestionAnsweredArgs> OnQuestionAnswered;
    public class OnQuestionAnsweredArgs : AchievementArgs
    {
        public bool AnsweredCorrectly;
        public float TimeRemaining;
    }
}

public abstract class AchievementArgs
{
}
