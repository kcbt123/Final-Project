using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeItemController : BlockBase, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;

    private EventListener[] _eventListeners;

    private Image _codeItemIcon;

    [SerializeField]
    private Sprite iconMovUp;
    [SerializeField]
    private Sprite iconMovDown;
    [SerializeField]
    private Sprite iconMovLeft;
    [SerializeField]
    private Sprite iconMovRight;
    [SerializeField]
    private Sprite iconActionPlant;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        _codeItemIcon = transform.GetComponent<Image>();
    }

    public override void Start()
    {
        //scrollPanelObject = transform.GetChild(2).GetChild(0).GetChild(0).gameObject;

        SetupIcon();
    }

    void SetupIcon()
    {
        if (base._data.blockType == BlockType.MOVEMENT)
        {
            if (base._data.blockIdentifier == MovementBlockIdentifier.UP)
            {
                _codeItemIcon.sprite = iconMovUp;
            }
            else if (base._data.blockIdentifier == MovementBlockIdentifier.DOWN)
            {
                _codeItemIcon.sprite = iconMovDown;
            }
            else if (base._data.blockIdentifier == MovementBlockIdentifier.LEFT)
            {
                _codeItemIcon.sprite = iconMovLeft;
            }
            else if (base._data.blockIdentifier == MovementBlockIdentifier.RIGHT)
            {
                _codeItemIcon.sprite = iconMovRight;
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
            _data.itemID,
            _data.blockType, 
            _data.blockIdentifier 
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
