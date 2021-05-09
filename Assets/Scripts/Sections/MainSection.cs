using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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

        //AddTestCodeBlocks();
    }

    void AddTestCodeBlocks()
    {

        MovementBlockIdentifier[] idents = { 
            MovementBlockIdentifier.UP,
            MovementBlockIdentifier.DOWN,
            MovementBlockIdentifier.UP,
            MovementBlockIdentifier.LEFT,
            MovementBlockIdentifier.RIGHT,
            MovementBlockIdentifier.RIGHT
        };

        for (int i = 0; i < idents.Length; i++)
        {
            GameObject newCodeBlock = Instantiate(codeBlockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCodeBlock.name = "Test array generated block";
            newCodeBlock.transform.SetParent(scrollPanelObject.transform);
            BlockJSONData data = newCodeBlock.transform.GetComponent<MovementBlockController>()._data;
            data.blockType = BlockType.MOVEMENT;
            data.blockIdentifier = idents[i];
            newCodeBlock.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
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
        string codeBlockName = (string)eventParam[0];
        string codeBlockType = (string)eventParam[1];

        // Can them dung block code
        Debug.Log("Adding new main code block to Main Section");
        GameObject newCodeBlock = Instantiate(codeBlockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        int blockIndex = 9;
        newCodeBlock.name = "Code Block" + blockIndex.ToString();
        newCodeBlock.transform.SetParent(scrollPanelObject.transform);
    }

    private void OnRemoveCodeBlock(object[] eventParam)
    {
        string codeBlockName = (string)eventParam[0];
        string codeBlockType = (string)eventParam[1];

        // Can remove dung block code o cho do
        Transform trans = scrollPanelObject.transform;
        if (trans.childCount > 0)
        {
            Debug.Log("Removing a code block from Main Section");
            GameObject lastCodeBlock = trans.GetChild(trans.childCount - 1).gameObject;
            Destroy(lastCodeBlock);
        }
        else
        {
            Debug.Log("No child left");
        }
    }

    public void OnDeleteAllClick() {
        if (scrollPanelObject.transform.childCount > 0) {
            foreach (Transform child in scrollPanelObject.transform) {
                Destroy(child.gameObject);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropMainSection");

        if (eventData.pointerDrag != null)
        {
            Debug.Log("Nhan drop nhung minh se ko lay o day");
        }
    }
}
