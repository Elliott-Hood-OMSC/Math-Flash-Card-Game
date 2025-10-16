// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine;

/// <summary>
/// A simple class for showing/hiding a gameobject.
/// Exists to be overridden with animations and other functionality at a later point.
/// </summary>
public class Menu : MonoBehaviour
{
    public virtual void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
