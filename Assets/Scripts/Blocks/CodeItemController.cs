using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeItemController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;

    [SerializeField]
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameObject codeItemPrefab;

    private GameObject scrollPanelObject;

    private EventListener[] _eventListeners;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();   
    }

    private void Start()
    {
        //scrollPanelObject = transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
    }

    /** ======= MARK: - Handle Events ======= */

    private void AddListeners()
    {
        _eventListeners = new EventListener[2];
        _eventListeners[0] = CustomEventSystem.instance.AddListener(EventCode.ON_ADD_CODEBLOCK_BOTTOM, this, OnAddCodeBlock);
        _eventListeners[1] = CustomEventSystem.instance.AddListener(EventCode.ON_REMOVE_CODEBLOCK_BOTTOM, this, OnRemoveCodeBlock);
    }

    private void RemoveListeners()
    {
        if (_eventListeners.Length != 0)
        {
            foreach (EventListener listener in _eventListeners)
                CustomEventSystem.instance.RemoveListener(listener.eventCode, listener);
        }
    }

    private void OnAddCodeBlock(object[] eventParam)
    {
        string codeBlockName = (string)eventParam[0];
        string codeBlockType = (string)eventParam[1];

        AddNewCodeItem();
    }

    private void OnRemoveCodeBlock(object[] eventParam)
    {

    }


    private string _codeBlockName;
    private string _codeBlockType;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;


        _codeBlockName = "MoveDownBlock";
        _codeBlockType = "MovementBlock";

        CustomEventSystem.instance.DispatchEvent(EventCode.ON_ADD_CODEBLOCK_MAIN, new object[] { _codeBlockName, _codeBlockType });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    void AddNewCodeItem()
    {
        Debug.Log("Adding new bottom code item");
        GameObject newCodeItem = Instantiate(codeItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        int blockIndex = 9;
        newCodeItem.name = "Code Item" + blockIndex.ToString();
        //newCodeItem.transform.SetParent(scrollPanelObject.transform);
    }
}
