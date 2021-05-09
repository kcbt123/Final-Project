using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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
            MovementBlockIdentifier.UP,
            MovementBlockIdentifier.LEFT,
            MovementBlockIdentifier.RIGHT,
            MovementBlockIdentifier.UP
        };

        for (int i = 0; i < 10; i++)
        {
            GameObject newCodeItem = Instantiate(codeItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCodeItem.name = "Test array generated item";
            newCodeItem.transform.SetParent(scrollPanelObject.transform);
            BlockJSONData data = newCodeItem.transform.GetComponent<CodeItemController>()._data;
            data.itemID = i + 1;
            newCodeItem.SetActive(false);
        }

        for (int i = 0; i < idents.Length; i++)
        {
            GameObject item = scrollPanelObject.transform.GetChild(i).gameObject;
            BlockJSONData data = item.transform.GetComponent<CodeItemController>()._data;
            data.blockType = BlockType.MOVEMENT;
            data.blockIdentifier = idents[i];
            item.SetActive(true);
        }
    }

    void refreshIDs()
    {
        int itemCount = scrollPanelObject.transform.childCount;
        for (int i = 0; i < itemCount; i++)
        {
            GameObject obj = scrollPanelObject.transform.GetChild(i).gameObject;
            int trueIndex = obj.transform.GetSiblingIndex() + 1;
            obj.GetComponent<CodeItemController>()._data.itemID = trueIndex;
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
        int _index = (int)eventParam[0];
        BlockType _type = (BlockType)eventParam[1];
        MovementBlockIdentifier _identifier = (MovementBlockIdentifier)eventParam[2];

        // Can them dung code item tuong ung

        //Lay index active moi nhat
        int latestActiveIndex = -1;
        for (int i = 0; i < scrollPanelObject.transform.childCount; i++)
        {
            if (scrollPanelObject.transform.GetChild(i).gameObject.activeSelf == false)
            {
                latestActiveIndex = i - 1;
                break;
            }
        }

        GameObject currentCodeItem = scrollPanelObject.transform.GetChild(latestActiveIndex + 1).gameObject;
        currentCodeItem.SetActive(true);
        currentCodeItem.SetActive(true);
        BlockJSONData data = currentCodeItem.transform.GetComponent<CodeItemController>()._data;
        data.blockType = _type;
        data.blockIdentifier = _identifier;

        refreshIDs();
    }

    private void OnRemoveBottomCodeItem(object[] eventParam)
    {
        int _index = (int)eventParam[0];
        BlockType _type = (BlockType)eventParam[1];
        MovementBlockIdentifier _identifier = (MovementBlockIdentifier)eventParam[2];

        // Can bo dung block code tuong ung
        Transform trans = scrollPanelObject.transform;
        if (trans.childCount > 0)
        {
            GameObject lastCodeBlock = trans.GetChild(_index - 1).gameObject;
            lastCodeBlock.SetActive(false);

            // Get all children
            GameObject[] _array = new GameObject[trans.childCount];

            for (int i = 0; i < trans.childCount; i++)
            {
                _array[i] = trans.GetChild(i).gameObject;
            }

            // Sort all remaining block
            GameObject[] _sortedArray = _array.OrderByDescending(w => w.activeSelf).ToArray();

            for (int i = 0; i < trans.childCount; i++)
            {
                _sortedArray[i].transform.SetSiblingIndex(i);
            }

            //Destroy(lastCodeBlock);
        }
        else
        {
            Debug.Log("No child left");
        }

        refreshIDs();
    }
}
