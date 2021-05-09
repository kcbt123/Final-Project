using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockBase : MonoBehaviour
{

    /** ======= MARK: - Fields and Properties ======= */

    /** [BlockJSONData] Used to draw public info about the code item / block
     *  Contains:
     *  - Base Item ID in the Bottom Section
     *  - Code Item / Block type
     *  - Code Item / Block identifier
     */
    public BlockJSONData _data;

    /** [Canvas] which the block is currently in */
    [SerializeField]
    private Canvas canvas;

    /** [CanvasBlock] used to handle UI collision and alpha when dragging the block */
    private CanvasGroup canvasGroup;

    /** ======= MARK: - MonoBehavior Functions ======= */

    private void Awake()
    {
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /** ======= MARK: - Listeners ======= */

}
