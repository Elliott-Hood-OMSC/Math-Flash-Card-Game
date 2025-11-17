// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

/// <summary>
/// An abstract base achievement class with virtual fields for
/// - descriptions
/// - completion
/// - subscription
///
/// Also contains:
/// - save keys
/// - saving/loading completion
/// </summary>
public abstract class Achievement : ScriptableObject
{
    public virtual string AchievementTitle => NicifyName(name);
    private static string NicifyName(string name)
    {
        name = name.TrimStart('_');
        name = System.Text.RegularExpressions.Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
        return char.ToUpper(name[0]) + name.Substring(1);
    }
    
    public virtual string AchievementDescription => _description;
    public virtual Sprite AchievementThumbnail => _thumbnail;
    public bool HasAchievement => _achievementGotten;
    public virtual bool IsMaxed => _achievementGotten;
    
    private bool _achievementGotten;
    
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
}
