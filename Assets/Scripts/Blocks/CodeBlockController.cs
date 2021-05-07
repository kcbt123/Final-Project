using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBlockController : MonoBehaviour
{
    /** ======= MARK: - Fields and Properties ======= */

    private Button deleteButton;

    private string _codeBlockName;
    private string _codeBlockType;

    /** ======= MARK: - MonoBehaviour Methods ======= */

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /** ======= MARK: - Actions ======= */

    public void OnDeleteBlock()
    {
        Debug.Log("TEST REMOVE BLOCK BY BUTTON");
        CustomEventSystem.instance.DispatchEvent(EventCode.ON_REMOVE_CODEBLOCK_MAIN, new object[] { _codeBlockName, _codeBlockType });
    }
}
