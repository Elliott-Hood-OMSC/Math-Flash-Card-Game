using System.Collections;
using UnityEngine;

public class OnSecondPassedDetection : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            AchievementEvents.OnSecondPassed?.Invoke();
        }
    }
}
