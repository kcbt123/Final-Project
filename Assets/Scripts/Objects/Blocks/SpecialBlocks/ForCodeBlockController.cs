using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForCodeBlockController : BlockItemBase
{
    /** ======= MARK: - Fields and Properties ======= */

    public int loopCount;
    private TextMeshProUGUI loopCountText;

    private readonly int DEFAULT_COUNT = 1;
    private readonly int LOOP_UPPER_CAP = 9;
    private readonly int LOOP_LOWER_CAP = 1;

    [SerializeField]
    private GameObject[] childBlocks;

    /** ======= MARK: - MonoBehavior Functions ======= */

    public override void Awake()
    {
        loopCountText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        loopCount = DEFAULT_COUNT;
        loopCountText.text = DEFAULT_COUNT.ToString();
    }

    /** ======= MARK: - Actions ======= */

    public void OnIncreaseLoopCounter()
    {
        if (loopCount < LOOP_UPPER_CAP)
        {
            loopCount += 1;
        }
        loopCountText.text = loopCount.ToString();
    }

    public void OnDecreaseLoopCounter()
    {
        if (loopCount > LOOP_LOWER_CAP)
        {
            loopCount -= 1;
        }
        loopCountText.text = loopCount.ToString();
    }
}
