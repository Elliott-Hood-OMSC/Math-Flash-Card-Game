using UnityEngine;

public abstract class TieredAchievement : Achievement
{
    [System.Serializable]
    public class Tier
    {
        public int Requirement;
        public bool Achieved;
    }

    [SerializeField] private Tier[] _tiers;
    private int _progress;

    protected void IncrementProgress()
    {
        _progress++;

        foreach (Tier tier in _tiers)
        {
            if (!tier.Achieved && _progress >= tier.Requirement)
            {
                tier.Achieved = true;
                GetAchievement();
            }
        }

        Save();
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(AchievementSaveKey, _progress);

        string[] tierStates = new string[_tiers.Length];
        for (int i = 0; i < _tiers.Length; i++)
        {
            tierStates[i] = _tiers[i].Achieved ? "1" : "0";
        }
        string joined = string.Join(",", tierStates);
        PlayerPrefs.SetString(AchievementSaveKey + "_tiers", joined);

        PlayerPrefs.Save();
    }

    public override void Load()
    {
        _progress = PlayerPrefs.GetInt(AchievementSaveKey, 0);

        string tierString = PlayerPrefs.GetString(AchievementSaveKey + "_tiers", "");
        string[] states = tierString.Split(',');

        for (int i = 0; i < _tiers.Length; i++)
        {
            if (i < states.Length)
            {
                _tiers[i].Achieved = states[i] == "1";
            }
            else
            {
                _tiers[i].Achieved = false;
            }
        }
    }
}
