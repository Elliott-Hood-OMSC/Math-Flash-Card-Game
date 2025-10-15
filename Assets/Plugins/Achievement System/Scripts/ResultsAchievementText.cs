using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsAchievementsTextTMP : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI achievementsText; // text box that shows achievements after each round

    // called automatically when the results panel becomes active
    private void OnEnable()
    {
        RefreshFromTracker();
    }

    // pulls progress from the tracker and fills the text box
    public void RefreshFromTracker()
    {
        if (achievementsText == null)
        {
            return;
        }

        // make sure the tracker exists
        if (AchievementRoundProgressTracker.Instance == null)
        {
            achievementsText.text = "No achievements progressed this round.";
            return;
        }

        // get all achievements that progressed this round
        Achievement[] progressed = AchievementRoundProgressTracker.Instance.GetAndClearProgressThisRound();
        

        // build a simple bullet list
        List<string> lines = new();
        lines.Add("<b>Achievements progress:</b>");
        foreach (var a in progressed)
        {
            if (a == null) continue;
            lines.Add($"- {a.AchievementTitle}");
        }

        achievementsText.text = string.Join("\n", lines);
    }
}