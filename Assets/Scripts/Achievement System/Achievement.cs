using UnityEngine;

public abstract class Achievement : ScriptableObject
{
    [TextArea]
    public string Description;
    public Sprite Thumbnail;

    protected string AchievementSaveKey => GetType().Name;
    
    public abstract void Subscribe();
    
    protected void GetAchievement()
    {
        AchievementEvents.OnAchievementGet?.Invoke(new AchievementEvents.OnAchievementGetArgs
        {
            AchievementObtained = this
        });
    }

    public abstract void Save();

    public abstract void Load();
}
