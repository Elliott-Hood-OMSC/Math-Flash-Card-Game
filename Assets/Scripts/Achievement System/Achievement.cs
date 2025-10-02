using UnityEngine;

public abstract class Achievement : ScriptableObject
{
    [TextArea]
    public string Description;
    public Sprite Thumbnail;
    
    protected abstract string AchievementSaveKey { get; }
    
    public abstract void Initialize();
    
    protected virtual void GetAchievement()
    {
        Debug.Log($"Achievement Got: {GetType()}!");
        AchievementEvents.OnAchievementGet?.Invoke(new AchievementEvents.OnAchievementGetArgs
        {
            AchievementObtained = this
        });
    }
    public abstract void ClearSave();

    public abstract void Save();

    public abstract void Load();
}
