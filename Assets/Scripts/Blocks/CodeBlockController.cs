using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeBlockController : BlockBase
{
    /** ======= MARK: - Fields and Properties ======= */

    private Button deleteButton;

    public Image _codeBlockIcon;
    public TextMeshProUGUI _textCodeBlock;

    private int codeBlockID;

    private string _codeBlockName;
    private string _codeBlockType;

    /** ======= MARK: - MonoBehaviour Methods ======= */

    private void Awake()
    {
        _codeBlockIcon = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _textCodeBlock = transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /** ======= MARK: - Actions ======= */

    public void OnDeleteBlock()
    {
        CustomEventSystem.instance.DispatchEvent(EventCode.ON_REMOVE_CODEBLOCK_MAIN, new object[] { 
            _data.itemID,
            _data.blockType, 
            _data.blockIdentifier 
        });
    }
}
