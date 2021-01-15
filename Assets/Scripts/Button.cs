using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class Button : MonoBehaviour
{
    private EventTrigger eventTrigger;

    private void Awake()
    {
        CreateParent();
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(HandleMultipleClicks);

        CreatePointerEvents();
    }

    private void CreatePointerEvents()
    {
        eventTrigger = gameObject.AddComponent<EventTrigger>();

        AddEventTrigger(OnPointerEnter, EventTriggerType.PointerEnter);
        AddEventTrigger(OnPointerExit, EventTriggerType.PointerExit);
    }

    private void AddEventTrigger(UnityAction action, EventTriggerType triggerType)
    {
        EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
        trigger.AddListener((eventData) => action());

        EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

        eventTrigger.triggers.Add(entry);
    }
    public void OnPointerEnter()
    {
        gameObject.GetComponentInParent<Animator>().SetBool("Active", true);
    }
    public void OnPointerExit()
    {
        gameObject.GetComponentInParent<Animator>().SetBool("Active", false);
    }

    private void CreateParent()
    {
        gameObject.transform.SetParent(Instantiate(Resources.Load<GameObject>("Prefabs/AnimatedButton"), gameObject.transform.parent).transform);
    }

    protected virtual void HandleMultipleClicks()
    {
        AudioManager.Instance.Play(Sounds.Click);
        OnClick();
    }
    protected abstract void OnClick();
}
