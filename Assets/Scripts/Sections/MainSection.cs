using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    /** ======= MARK: - MonoBehaviour Methods ======= */

    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        scrollPanelObject = transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
        verticalLayoutGroup = scrollPanelObject.transform.GetComponent<VerticalLayoutGroup>();
        // Debug.Log("Object type 1" + transform.GetChild(2).gameObject.name);
        // Debug.Log("Object type 2" + transform.GetChild(2).GetChild(0).gameObject.name);
        // Debug.Log("Object type 3" + transform.GetChild(2).GetChild(0).GetChild(0).gameObject.name);
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
        string codeBlockName = (string)eventParam[0];
        string codeBlockType = (string)eventParam[1];

        AddNewCodeBlock();
    }

    private void OnRemoveCodeBlock(object[] eventParam)
    {

    }

    void AddNewCodeBlock() {
        Debug.Log("Adding new main code block");
        GameObject newCodeBlock = Instantiate(codeBlockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        int blockIndex = 9;
        newCodeBlock.name = "Code Block" + blockIndex.ToString();
        newCodeBlock.transform.SetParent(scrollPanelObject.transform);
    }

    public void OnDeleteAllClick() {
        if (scrollPanelObject.transform.childCount > 0) {
            foreach (Transform child in scrollPanelObject.transform) {
                Destroy(child.gameObject);
            }
        }
    }

    // ===== MARK: - On Drop Handler

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropMainSection");

        if (eventData.pointerDrag != null) 
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            //AddNewCodeBlock();
            Debug.Log("Nhan drop nhung minh se ko lay o day");
        }
    }
}
