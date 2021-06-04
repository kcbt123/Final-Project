using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeItemController : BlockItemBase, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    /** ======= MARK: - Fields and Properties ======= */

    private RectTransform rectTransform;

    private EventListener[] _eventListeners;

    private Image itemIcon;

    [SerializeField]
    private Sprite iconMovUp;
    [SerializeField]
    private Sprite iconMovDown;
    [SerializeField]
    private Sprite iconMovLeft;
    [SerializeField]
    private Sprite iconMovRight;

    [SerializeField]
    private Sprite iconWateringYellow;
    [SerializeField]
    private Sprite iconWateringWhite;
    [SerializeField]
    private Sprite iconWateringRed;

    [SerializeField]
    private Sprite iconFor;
    [SerializeField]
    private Sprite iconIf;

    [SerializeField]
    private Sprite iconFunction1;
    [SerializeField]
    private Sprite iconFunction2;

    /** ======= MARK: - MonoBehavior Functions ======= */

    public override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = Stage1GUIManager.instance.fullCanvas.GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = transform.GetComponent<Image>();
    }

    public override void Start()
    {
        //scrollPanelObject = transform.GetChild(2).GetChild(0).GetChild(0).gameObject;

        SetupIcon();
    }

    /** ======= MARK: - Setup ======= */

    void SetupIcon()
    {
        if (data.blockType == BlockItemType.MOVEMENT)
        {
            switch (data.blockIdentifier)
            {
                case BlockItemIdentifier.MOVEMENT_UP:
                    itemIcon.sprite = iconMovUp;
                    break;
                case BlockItemIdentifier.MOVEMENT_DOWN:
                    itemIcon.sprite = iconMovDown;
                    break;
                case BlockItemIdentifier.MOVEMENT_LEFT:
                    itemIcon.sprite = iconMovLeft;
                    break;
                case BlockItemIdentifier.MOVEMENT_RIGHT:
                    itemIcon.sprite = iconMovRight;
                    break;
            }
        } else if (data.blockType == BlockItemType.ACTION)
        {
            switch (data.blockIdentifier)
            {
                case BlockItemIdentifier.ACTION_WATERING_YELLOW:
                    itemIcon.sprite = iconWateringYellow;
                    break;
                case BlockItemIdentifier.ACTION_WATERING_WHITE:
                    itemIcon.sprite = iconWateringWhite;
                    break;
                case BlockItemIdentifier.ACTION_WATERING_RED:
                    itemIcon.sprite = iconWateringRed;
                    break;
            }
        } else if (data.blockType == BlockItemType.SPECIAL)
        {
            switch (data.blockIdentifier)
            {
                case BlockItemIdentifier.SPECIAL_FOR:
                    itemIcon.sprite = iconFor;
                    break;
                case BlockItemIdentifier.SPECIAL_IF:
                    itemIcon.sprite = iconIf;
                    break;


                case BlockItemIdentifier.SPECIAL_FUNCTION:
                    if (data.functionBlockNumber == 1)
                        itemIcon.sprite = iconFunction1;
                    else if (data.functionBlockNumber == 2)
                        itemIcon.sprite = iconFunction2;
                    break;
            }
        } 
    }

    /** ======= MARK: - Handle Drag Events ======= */

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

        CustomEventSystem.instance.DispatchEvent(EventCode.ON_ADD_CODEBLOCK_MAIN, new object[] { 
            data.itemCurrentIndex,
            data.blockType, 
            data.blockIdentifier 
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    //void AddNewCodeItem()
    //{
    //    Debug.Log("Adding new bottom code item");
    //    GameObject newCodeItem = Instantiate(codeItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    //    int blockIndex = 9;
    //    newCodeItem.name = "Code Item" + blockIndex.ToString();
    //    //newCodeItem.transform.SetParent(scrollPanelObject.transform);
    //}
}
