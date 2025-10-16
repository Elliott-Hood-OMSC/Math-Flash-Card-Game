// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System.Collections;
using UnityEngine;

/// <summary>
/// Invokes the OnSecondPassed event used by Mathematician.
/// </summary>
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
