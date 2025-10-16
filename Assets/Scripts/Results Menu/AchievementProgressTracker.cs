// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Keeps track of achievements earned during gameplay.
/// </summary>
public class AchievementRoundProgressTracker : MonoBehaviour
{
    public static AchievementRoundProgressTracker Instance { get; private set; }

    private readonly HashSet<Achievement> _progressedThisRound = new(); // keeps track of achievements that changed this round

    // runs once when the tracker first loads
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // start listening for achievement events
        AchievementEvents.OnTieredAchievementTierIncrease += OnProgressUpdated;
        AchievementEvents.OnAchievementGet += OnAchievementGet;
        AchievementEvents.OnRoundEnded += OnRoundEnded;
    }

    // unsubscribes from events when the tracker is destroyed
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;

        AchievementEvents.OnTieredAchievementTierIncrease -= OnProgressUpdated;
        AchievementEvents.OnAchievementGet -= OnAchievementGet;
        AchievementEvents.OnRoundEnded -= OnRoundEnded;
    }

    // clears the list at the start of a round
    public void BeginRound() => _progressedThisRound.Clear();

    // adds tiered achievements that gained progress
    private void OnProgressUpdated(AchievementEvents.OnTieredAchievementArgs args)
    {
        if (args.TieredAchievement != null)
            _progressedThisRound.Add(args.TieredAchievement);
    }

    // adds normal achievements that were unlocked
    private void OnAchievementGet(AchievementEvents.OnAchievementGetArgs args)
    {
        if (args.AchievementObtained != null)
            _progressedThisRound.Add(args.AchievementObtained);
    }

    // called at the end of each round, no UI work here—just confirms data is ready
    private void OnRoundEnded(AchievementEvents.OnRoundEndedArgs args)
    {
        // intentionally empty: the UI will read the buffered progress later
    }

    // returns the achievements that progressed this round, then clears the buffer
    public Achievement[] GetAndClearProgressThisRound()
    {
        var list = _progressedThisRound
            .Where(a => a != null)
            .OrderByDescending(a => a.HasAchievement)
            .ThenBy<Achievement, string>(a =>
            {
                // Use AchievementTitleNoTier if this is a TieredAchievement
                if (a is TieredAchievement tiered)
                    return tiered.AchievementTitleNoTier;
                return a.AchievementTitle;
            })
            .ToArray();

        _progressedThisRound.Clear();
        return list;
    }
}
