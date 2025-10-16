using UnityEngine;

public abstract class Achievement : ScriptableObject
{
    public virtual string AchievementTitle => NicifyName(GetType().ToString());
    public virtual string AchievementDescription => _description;
    public virtual Sprite AchievementThumbnail => _thumbnail;
    public bool HasAchievement => _achievementGotten;
    private bool _achievementGotten = false;
    
    public virtual bool IsMaxed => _achievementGotten;
    
    [TextArea]
    [SerializeField] private string _description;
    [SerializeField] private Sprite _thumbnail;

    protected string AchievementSaveKey => GetType().Name;
    
    public abstract void Subscribe();
    public abstract void Unsubscribe();
    
    protected void GetAchievement()
    {
        if (IsMaxed) return;
        
        _achievementGotten = true;
        Save();
        AchievementEvents.OnAchievementGet?.Invoke(new AchievementEvents.OnAchievementGetArgs
        {
            AchievementObtained = this
        });
    }

    public virtual void Save()
    {
        PlayerPrefs.SetInt(AchievementSaveKey + "_gotten", _achievementGotten ? 1 : 0);
    }

    public virtual void Load()
    {
        _achievementGotten = PlayerPrefs.GetInt(AchievementSaveKey + "_gotten") == 1 ? true : false;
    }
    
    public static string NicifyName(string name)
    {
        name = name.TrimStart('_');
        name = System.Text.RegularExpressions.Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
        return char.ToUpper(name[0]) + name.Substring(1);
    }
}