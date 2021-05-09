using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BottomSection : MonoBehaviour
{
    /** ======= MARK: - Field and Properties ======= */

    private GameObject scrollPanelObject;
    private EventListener[] _eventListeners;

    [SerializeField]
    private GameObject codeItemPrefab;

    /** ======= MARK: - MonoBehaviour Methods ======= */
    void Awake()
    {
        AddListeners();
        scrollPanelObject = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
    }

    private void Start()
    {
        AddMockCodeItems();
    }

    void AddMockCodeItems()
    {
        MovementBlockIdentifier[] idents = {
            MovementBlockIdentifier.UP,
            MovementBlockIdentifier.DOWN,
            MovementBlockIdentifier.LEFT,
            MovementBlockIdentifier.LEFT,
            MovementBlockIdentifier.RIGHT,
            MovementBlockIdentifier.RIGHT
        };

        for (int i = 0; i < idents.Length; i++)
        {
            GameObject newCodeItem = Instantiate(codeItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCodeItem.name = "Test array generated item";
            newCodeItem.transform.SetParent(scrollPanelObject.transform);
            BlockJSONData data = newCodeItem.transform.GetComponent<CodeItemController>()._data;
            data.blockType = BlockType.MOVEMENT;
            data.blockIdentifier = idents[i];
        }
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    /** ======= MARK: - Handle Events ======= */

    private void AddListeners()
    {
        _eventListeners = new EventListener[2];
        _eventListeners[0] = CustomEventSystem.instance.AddListener(EventCode.ON_ADD_CODEBLOCK_MAIN, this, OnRemoveBottomCodeItem);
        _eventListeners[1] = CustomEventSystem.instance.AddListener(EventCode.ON_REMOVE_CODEBLOCK_MAIN, this, OnAddBottomCodeItem);
    }

    private void RemoveListeners()
    {
        if (_eventListeners.Length != 0)
        {
            foreach (EventListener listener in _eventListeners)
                CustomEventSystem.instance.RemoveListener(listener.eventCode, listener);
        }
    }

    private void OnAddBottomCodeItem(object[] eventParam)
    {
        string codeBlockName = (string)eventParam[0];
        string codeBlockType = (string)eventParam[1];

        // Can them dung code item tuong ung
        Debug.Log("Adding new code item to bottom bar");
        GameObject newCodeItem = Instantiate(codeItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        int blockIndex = 1;
        newCodeItem.name = "Code Item" + blockIndex.ToString();
        newCodeItem.transform.SetParent(scrollPanelObject.transform);
    }

    private void OnRemoveBottomCodeItem(object[] eventParam)
    {
        string codeBlockName = (string)eventParam[0];
        string codeBlockType = (string)eventParam[1];

        // Can bo dung block code tuong ung
        Transform trans = scrollPanelObject.transform;
        if (trans.childCount > 0)
        {
            Debug.Log("Removing a code item from bottom bar");
            GameObject lastCodeBlock = trans.GetChild(trans.childCount - 1).gameObject;
            Destroy(lastCodeBlock);
        }
        else
        {
            Debug.Log("No child left");
        }
    }
}
