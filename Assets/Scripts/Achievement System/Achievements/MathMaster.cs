using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(MathMaster), fileName = nameof(MathMaster))]
public class MathMaster : Achievement
{
    [SerializeField] private int _requiredCorrectQuestions = 3;
    protected override string AchievementSaveKey => "MathMaster";
    private int _numCorrectQuestions;
    
    public override void Initialize()
    {
        Load();
        AchievementEvents.OnQuestionAnswered += OnQuestionAnswered;
    }

    private void OnQuestionAnswered(AchievementEvents.OnQuestionAnsweredArgs obj)
    {
        if (!obj.AnsweredCorrectly)
            return;
        
        _numCorrectQuestions++;
        Debug.Log(_numCorrectQuestions);
        
        if (_numCorrectQuestions == _requiredCorrectQuestions)
        {
            GetAchievement();
        }
        Save();
    }

    public override void ClearSave()
    {
        PlayerPrefs.SetInt(AchievementSaveKey, 0);
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(AchievementSaveKey, _numCorrectQuestions);
    }

    public override void Load()
    {
        _numCorrectQuestions = PlayerPrefs.GetInt(AchievementSaveKey);
    }
}
