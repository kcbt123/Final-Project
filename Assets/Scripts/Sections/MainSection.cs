using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSection : MonoBehaviour
{
    [SerializeField]
    private GameObject codeBlockPrefab;

    private List<GameObject> listCodeBlock;

    private GameObject scrollPanelObject;

    private VerticalLayoutGroup verticalLayoutGroup;

    //private List<CodeBlock> listCodeBlocks = new List<CodeBlock>(5);    // Start is called before the first frame update
    void Start()
    {
        verticalLayoutGroup = transform.GetComponent<VerticalLayoutGroup>();
        scrollPanelObject = transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
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

    public void OnDeleteAllClick() {
        if (scrollPanelObject.transform.childCount > 0) {
            foreach (Transform child in scrollPanelObject.transform) {
                Destroy(child.gameObject);
            }
        }
    }
}
