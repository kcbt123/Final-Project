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
        LoadDataJSON();
        AddMockCodeItems();
    }

    void AddMockCodeItems()
    {
        List<BlockItemIdentifier> bottomBlockList = stageDataIdents;

        for (int i = 0; i < 10; i++)
        {
            GameObject newCodeItem = Instantiate(codeItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCodeItem.name = "Test array generated item";
            newCodeItem.transform.SetParent(scrollPanelObject.transform);
            BlockData data = newCodeItem.transform.GetComponent<CodeItemController>().data;
            data.itemCurrentIndex = i + 1;
            newCodeItem.SetActive(false);
        }

        for (int i = 0; i < bottomBlockList.Count; i++)
        {
            GameObject item = scrollPanelObject.transform.GetChild(i).gameObject;
            BlockData data = item.transform.GetComponent<CodeItemController>().data;

            if (bottomBlockList[i] == BlockItemIdentifier.MOVEMENT_UP
                || bottomBlockList[i] == BlockItemIdentifier.MOVEMENT_DOWN
                || bottomBlockList[i] == BlockItemIdentifier.MOVEMENT_LEFT
                || bottomBlockList[i] == BlockItemIdentifier.MOVEMENT_RIGHT)
            {
                data.blockType = BlockItemType.MOVEMENT;
            }
            else if (bottomBlockList[i] == BlockItemIdentifier.ACTION_WATERING_YELLOW
              || bottomBlockList[i] == BlockItemIdentifier.ACTION_WATERING_WHITE
              || bottomBlockList[i] == BlockItemIdentifier.ACTION_WATERING_RED)
            {
                data.blockType = BlockItemType.ACTION;
            }
            else if (bottomBlockList[i] == BlockItemIdentifier.SPECIAL_FOR
              || bottomBlockList[i] == BlockItemIdentifier.SPECIAL_IF)
            {
                data.blockType = BlockItemType.SPECIAL;
            }

            data.blockIdentifier = bottomBlockList[i];
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
            obj.GetComponent<CodeItemController>().data.itemCurrentIndex = trueIndex;
        }
    }

    private void OnDestroy()
    {
        //RemoveListeners();
    }

    /** ======= MARK: - Handle Events ======= */

    private void AddListeners()
    {
        _eventListeners = new EventListener[4];
        _eventListeners[0] = CustomEventSystem.instance.AddListener(EventCode.ON_ADD_CODEBLOCK_MAIN, this, OnRemoveBottomCodeItem);
        _eventListeners[1] = CustomEventSystem.instance.AddListener(EventCode.ON_REMOVE_CODEBLOCK_MAIN, this, OnAddBottomCodeItem);
        _eventListeners[2] = CustomEventSystem.instance.AddListener(EventCode.ON_REMOVE_ALL_BLOCKS_MAIN, this, OnAddAllBottomItems);
        //_eventListeners[3] = CustomEventSystem.instance.AddListener(EventCode.ON_LOAD_STAGE_DATA_DONE, this, OnLoadStageDataDone);
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
        BlockItemType _type = (BlockItemType)eventParam[1];
        BlockItemIdentifier _identifier = (BlockItemIdentifier)eventParam[2];

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
        BlockData data = currentCodeItem.transform.GetComponent<CodeItemController>().data;
        data.blockType = _type;
        data.blockIdentifier = _identifier;

        refreshIDs();
    }

    private void OnRemoveBottomCodeItem(object[] eventParam)
    {
        int _index = (int)eventParam[0];
        BlockItemType _type = (BlockItemType)eventParam[1];
        BlockItemIdentifier _identifier = (BlockItemIdentifier)eventParam[2];

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

    private void OnAddAllBottomItems(object[] eventParam)
    {
        int _originalBlockCount = (int)eventParam[0];

        Transform trans = scrollPanelObject.transform;
        for (int i = 0; i < _originalBlockCount; i++)
        {
            trans.GetChild(i).gameObject.SetActive(true);
        }
    }

    // Temp = Load Data JSON

    private StageData stageData;
    public List<BlockItemIdentifier> stageDataIdents;

    void LoadDataJSON()
    {
        stageData = JSONUtil.LoadDataFromJson<StageData>("Stage1Data");

        for (int i = 0; i < stageData.itemData.Count; i++)
        {
            if (stageData.itemData[i] == "MOVEMENT_UP") { stageDataIdents.Add(BlockItemIdentifier.MOVEMENT_UP); }
            else if (stageData.itemData[i] == "MOVEMENT_DOWN") { stageDataIdents.Add(BlockItemIdentifier.MOVEMENT_DOWN); }
            else if (stageData.itemData[i] == "MOVEMENT_LEFT") { stageDataIdents.Add(BlockItemIdentifier.MOVEMENT_LEFT); }
            else if (stageData.itemData[i] == "MOVEMENT_RIGHT") { stageDataIdents.Add(BlockItemIdentifier.MOVEMENT_RIGHT); }

            else if (stageData.itemData[i] == "ACTION_WATERING_YELLOW") { stageDataIdents.Add(BlockItemIdentifier.ACTION_WATERING_YELLOW); }
            else if (stageData.itemData[i] == "ACTION_WATERING_WHITE") { stageDataIdents.Add(BlockItemIdentifier.ACTION_WATERING_WHITE); }
            else if (stageData.itemData[i] == "ACTION_WATERING_RED") { stageDataIdents.Add(BlockItemIdentifier.ACTION_WATERING_RED); }

            else if (stageData.itemData[i] == "SPECIAL_FOR") { stageDataIdents.Add(BlockItemIdentifier.SPECIAL_FOR); }
            else if (stageData.itemData[i] == "SPECIAL_IF") { stageDataIdents.Add(BlockItemIdentifier.SPECIAL_IF); }
            else if (stageData.itemData[i] == "SPECIAL_FUNCTION") { stageDataIdents.Add(BlockItemIdentifier.SPECIAL_FUNCTION); }
        }
    }
}
