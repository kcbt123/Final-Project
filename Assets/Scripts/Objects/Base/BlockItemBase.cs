using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockItemBase : MonoBehaviour
{
    /** ======= MARK: - Fields and Properties ======= */

    /** 
     * [BlockJSONData] Used to draw public info about the code item / block
     *  Contains:
     *  - Base Item ID in the Bottom Section
     *  - Code Item / Block type
     *  - Code Item / Block identifier
     */
    public BlockData data;

    /** [Canvas] which the block is currently in */
    public Canvas canvas;

    /** [CanvasBlock] used to handle UI collision and alpha when dragging the block */
    public CanvasGroup canvasGroup;

    /** ======= MARK: - MonoBehavior Functions ======= */
    public virtual void Awake()
    {
        
    }
    public virtual void Start()
    {
        
    }

    /** ======= MARK: - Action ======= */

    public void OnDeleteBlock()
    {
        CustomEventSystem.instance.DispatchEvent(EventCode.ON_REMOVE_CODEBLOCK_MAIN, new object[] {
            data.blockCurrentIndex,
            data.blockType,
            data.blockIdentifier
        });
    }
}
