// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// A button wrapper that automatically selects the button when the pointer enters it, and broadcasts selection events.
/// Used so that players seamlessly switch to keyboard/controller after hovering over an item with their mouse.
/// </summary>
public class PointerSelectableButton : Button
{
    public bool AutoSelectWhenPointerHovered { get; set; } = true;
    public bool UsedMouseAndKeyboardLast { get; private set; } = true;
    public bool DeSelectWhenPointerLeaves { get; set; } = true;

    public new virtual bool IsInteractable
    {
        get => interactable;
        set
        {
            if (interactable == value) return;
            
            interactable = value;
            OnInteractableChanged?.Invoke(value);
        }
    }

    public readonly UnityEvent<bool> OnInteractableChanged = new();
    public readonly UnityEvent<BaseEventData> OnSelectEvent = new();
    public readonly UnityEvent<BaseEventData> OnDeselectEvent = new();
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        
        if (AutoSelectWhenPointerHovered)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }
    
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        
        if (DeSelectWhenPointerLeaves)
            EventSystem.current.SetSelectedGameObject(null);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        OnSelectEvent.Invoke(eventData);
    }
    
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        OnDeselectEvent.Invoke(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        UsedMouseAndKeyboardLast = true;
        base.OnPointerClick(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        UsedMouseAndKeyboardLast = false;
        base.OnSubmit(eventData);
    }
}
