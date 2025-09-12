using UnityEngine;

public class Menu : MonoBehaviour
{
    public virtual void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
