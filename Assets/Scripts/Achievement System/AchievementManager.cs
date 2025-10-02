using System;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public Achievement[] Achievements { get; private set; }

    public void Awake()
    {
        Achievements = Resources.LoadAll<Achievement>("Achievements");
        foreach (Achievement achievement in Achievements)
        {
            achievement.Load();
            achievement.Subscribe();
        }
    }

    [ContextMenu("Clear Save")]
    public void ClearSave()
    {
        #if UNITY_EDITOR

        if (!Application.isPlaying)
        {
            throw new Exception($"Cannot call {nameof(ClearSave)}() outside of playmode, as achievements are loaded in Awake().");
        }
        
        #endif
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        foreach (Achievement achievement in Achievements)
        {
            achievement.Load();
        }
    }
}
