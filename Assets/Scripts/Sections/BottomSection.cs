using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottomSection : MonoBehaviour
{

    /** ======= MARK: - Field and Properties ======= */

    private GameObject scrollPanelObject;
    private EventListener[] _eventListeners;

    /** ======= MARK: - MonoBehaviour Methods ======= */



    void Awake()
    {
        scrollPanelObject = transform.GetChild(0).GetChild(0).gameObject;
    }

    /** ======= MARK: - Handle Events ======= */

    private void AddListeners()
    {
        _eventListeners = new EventListener[2];
        _eventListeners[0] = CustomEventSystem.instance.AddListener(EventCode.ON_ADD_CODEBLOCK_BOTTOM, this, OnAddCodeItem);
        _eventListeners[1] = CustomEventSystem.instance.AddListener(EventCode.ON_REMOVE_CODEBLOCK_BOTTOM, this, OnRemoveCodeItem);
    }

    private void RemoveListeners()
    {
        if (_eventListeners.Length != 0)
        {
            foreach (EventListener listener in _eventListeners)
                CustomEventSystem.instance.RemoveListener(listener.eventCode, listener);
        }
    }

    private void OnAddCodeItem(object[] eventParam)
    {
        string codeBlockName = (string)eventParam[0];
        string codeBlockType = (string)eventParam[1];

        //AddNewCodeItem();
    }

    private void OnRemoveCodeItem(object[] eventParam)
    {

    }
}
