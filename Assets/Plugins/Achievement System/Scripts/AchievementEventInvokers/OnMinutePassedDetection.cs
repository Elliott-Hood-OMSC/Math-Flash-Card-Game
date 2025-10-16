using System.Collections;
using UnityEngine;

public class OnMinutePassedDetection : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(60);
            AchievementEvents.OnMinutePassed?.Invoke();
        }
    }
}
