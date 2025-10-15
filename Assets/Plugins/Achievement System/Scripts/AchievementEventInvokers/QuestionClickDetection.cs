using UnityEngine;
using UnityEngine.UI;

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
