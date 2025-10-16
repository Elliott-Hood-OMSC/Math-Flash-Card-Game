// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

/// <summary>
/// Initializes and keeps a reference to all achievements on load as a persistent singleton.
/// Has the functionality to reset PlayerPrefs.
/// </summary>
public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public Achievement[] Achievements { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            InitializeAchievements();
        }
    }

    private void InitializeAchievements()
    {
        Achievements = Resources.LoadAll<Achievement>("Achievements");
        foreach (Achievement achievement in Achievements)
        {
            achievement.Load();
            achievement.Subscribe();
        }
    }

    public void OnDestroy()
    {
        foreach (Achievement achievement in Achievements)
        {
            achievement.Save();
            achievement.Unsubscribe();
        }
    }

    [ContextMenu("Clear Save")]
    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        if (!Application.isPlaying) return;
        
        foreach (Achievement achievement in Achievements)
        {
            achievement.Load();
        }
        
        AchievementEvents.OnRefreshAllAchievements?.Invoke();
    }
}