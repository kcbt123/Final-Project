using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;
using DG.Tweening;

public class MainSection : MonoBehaviour, IDropHandler
{
    public string GetClassName()
    {
        return this.GetType().Name;
    }

    /** ======= MARK: - Field and Properties ======= */

    [SerializeField]
    private GameObject codeBlockPrefab;

    private List<GameObject> listCodeBlock;

    private GameObject scrollPanelObject;

    private VerticalLayoutGroup verticalLayoutGroup;

    //private List<CodeBlock> listCodeBlocks = new List<CodeBlock>(5);

    private EventListener[] _eventListeners;

    BlockItemIdentifier[] idents = {
            BlockItemIdentifier.MOVEMENT_UP,
            BlockItemIdentifier.MOVEMENT_RIGHT,
            BlockItemIdentifier.ACTION_WATERING_YELLOW
    };

    private int originalTotalBlockCount = 3;

    /** ======= MARK: - MonoBehaviour Methods ======= */

    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        //RemoveListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        scrollPanelObject = transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
        verticalLayoutGroup = scrollPanelObject.transform.GetComponent<VerticalLayoutGroup>();
        // Debug.Log("Object type 1" + transform.GetChild(2).gameObject.name);
        // Debug.Log("Object type 2" + transform.GetChild(2).GetChild(0).gameObject.name);
        // Debug.Log("Object type 3" + transform.GetChild(2).GetChild(0).GetChild(0).gameObject.name);

        //AddTestCodeBlocks();
        AddPlaceholderCodeBlocks();
    }

    void AddTestCodeBlocks()
    {
        for (int i = 0; i < idents.Length; i++)
        {
            GameObject newCodeBlock = Instantiate(codeBlockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCodeBlock.name = "Test array generated block";
            newCodeBlock.transform.SetParent(scrollPanelObject.transform);
            BlockData data = newCodeBlock.transform.GetComponent<MovementBlockController>().data;
            data.blockType = BlockItemType.MOVEMENT;
            data.blockIdentifier = idents[i];
            newCodeBlock.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }
    }

    void AddPlaceholderCodeBlocks()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newCodeBlock = Instantiate(codeBlockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCodeBlock.name = "Test array generated block";
            newCodeBlock.transform.SetParent(scrollPanelObject.transform);
            newCodeBlock.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Adding new code block");
            GameObject newCodeBlock = Instantiate(codeBlockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            int blockIndex = 9;
            newCodeBlock.name = "Code Block" + blockIndex.ToString();
            newCodeBlock.transform.SetParent(scrollPanelObject.transform);
        } else if (Input.GetKeyDown(KeyCode.H))
        {
            Transform trans = scrollPanelObject.transform;
            if (trans.childCount > 0) {
                Debug.Log("Deleting a code block");
                GameObject lastCodeBlock = trans.GetChild(trans.childCount - 1).gameObject;
                Destroy(lastCodeBlock);
            } else {
                Debug.Log("No child left");
            }
        }
    }

    /** ======= MARK: - Handle Events ======= */

    private void AddListeners()
    {
        _eventListeners = new EventListener[2];
        _eventListeners[0] = CustomEventSystem.instance.AddListener(EventCode.ON_ADD_CODEBLOCK_MAIN, this, OnAddCodeBlock);
        _eventListeners[1] = CustomEventSystem.instance.AddListener(EventCode.ON_REMOVE_CODEBLOCK_MAIN, this, OnRemoveCodeBlock);
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
        int _index = (int)eventParam[0];
        BlockItemType _type = (BlockItemType)eventParam[1];
        BlockItemIdentifier _identifier = (BlockItemIdentifier)eventParam[2];

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

        GameObject currentCodeBlock = scrollPanelObject.transform.GetChild(latestActiveIndex + 1).gameObject;
        currentCodeBlock.SetActive(true);
        BlockData data = currentCodeBlock.transform.GetComponent<MovementBlockController>().data;
        data.blockType = _type;
        data.blockIdentifier = _identifier;

        currentCodeBlock.transform.GetComponent<MovementBlockController>().SetBlockData();

        refreshIDs();
    }

    private void OnRemoveCodeBlock(object[] eventParam)
    {
        int _index = (int)eventParam[0];
        BlockItemType _type = (BlockItemType)eventParam[1];
        BlockItemIdentifier _identifier = (BlockItemIdentifier)eventParam[2];

        // Can remove dung block code o cho do
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
        }
        else
        {
            Debug.Log("No child left");
        }

        refreshIDs();
    }

    public void OnDeleteAllClick() {
        if (scrollPanelObject.transform.childCount > 0) {
            foreach (Transform child in scrollPanelObject.transform) {
                if (child.gameObject.activeSelf == true)
                {
                    child.gameObject.SetActive(false);
                }        
            }

            refreshIDs();
        }

        CustomEventSystem.instance.DispatchEvent(EventCode.ON_REMOVE_ALL_BLOCKS_MAIN, new object[] {
            originalTotalBlockCount,
        });
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropMainSection");

        if (eventData.pointerDrag != null)
        {
            Debug.Log("Nhan drop nhung minh se ko lay o day");
        }
    }

    void refreshIDs()
    {
        int itemCount = scrollPanelObject.transform.childCount;
        for (int i = 0; i < itemCount; i++)
        {
            GameObject obj = scrollPanelObject.transform.GetChild(i).gameObject;
            int trueIndex = obj.transform.GetSiblingIndex() + 1;
            obj.GetComponent<MovementBlockController>().data.blockCurrentIndex = trueIndex;
            obj.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = trueIndex.ToString();
        }
    }

    // MARK: - Test DOTWEEN HIDE MENU

    [SerializeField]
    private RectTransform _mainSection;
    [SerializeField]
    private RectTransform _backgroundGrid;
    [SerializeField]
    private RectTransform _playerAvatar;
    bool testFlag = true;

    public void OnTapMainSectionHeader()
    {
        if (testFlag == true)
        {
            _mainSection.DOAnchorPos(new Vector2(180, 0), 0.25f);
            _backgroundGrid.DOAnchorPos(new Vector2(5.03f, 5.14f), 0.4f);
            _playerAvatar.DOAnchorPos(new Vector2(-0.512f, -2.227f), 0.25f);
            testFlag = false;
        } else if (testFlag == false)
        {
            _mainSection.DOAnchorPos(new Vector2(0, 0), 0.25f);
            _backgroundGrid.DOAnchorPos(new Vector2(3.03f, 5.14f), 0.4f);
            _playerAvatar.DOAnchorPos(new Vector2(-2.512f, -2.227f), 0.25f);
            testFlag = true;
        }
    }
}
