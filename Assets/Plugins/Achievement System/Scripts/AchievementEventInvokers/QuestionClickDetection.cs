// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Invokes the OnQuestionClicked event used by Huh.
/// </summary>
[RequireComponent(typeof(Button))]
public class QuestionClickDetection : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            AchievementEvents.OnQuestionClicked?.Invoke();
        });
    }
}
