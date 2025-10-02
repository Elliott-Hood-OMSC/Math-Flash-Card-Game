using UnityEngine;

public abstract class Achievement : ScriptableObject
{
    [TextArea]
    public string Description;
    public Sprite Thumbnail;
    
    public abstract void InitializeListener();

    protected void GetAchievement()
    {
        AchievementEvents.OnAchievementGet?.Invoke(new AchievementEvents.OnAchievementGetArgs
        {
            AchievementObtained = this
        });
    }
    
    public void Save()
    {
        
    }

    public void Load()
    {
        
    }
}
