using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIEndStage : MonoBehaviour
{

    public AudioClip victoryClip;
    public AudioSource audioSource;
    public string GetClassName()
    {
        return this.GetType().Name;
    }

    private EventListener[] _eventListeners;

    /** ======= MARK: - MonoBehaviour Methods ======= */

    private void Awake()
    {
        gameObject.SetActive(false);
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    /** ======= MARK: - Handle Events ======= */

    protected void AddListeners()
    {
        _eventListeners = new EventListener[1];
        _eventListeners[0] = CustomEventSystem.instance.AddListener(EventCode.ON_STAGE_FINISHED, this, OnObjectiveCleared);
    }

    protected void RemoveListeners()
    {
        foreach (EventListener listener in _eventListeners)
        {
            CustomEventSystem.instance.RemoveListener(listener.eventCode, listener);
        }
    }

    /** ======= MARK: - UI Activation ======= */

    protected void OnObjectiveCleared(object[] eventParam)
    {
        gameObject.SetActive(true);
        audioSource.PlayOneShot(victoryClip);
    }
}
