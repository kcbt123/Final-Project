using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForCodeBlockController : BlockBase
{
    public int loopCount;

    [SerializeField]
    private GameObject childBlock1;
    [SerializeField]
    private GameObject childBlock2;

    private TextMeshProUGUI loopCountText;

    private int LOOP_UPPER_CAP = 9;
    private int LOOP_LOWER_CAP = 1;

    private void Awake()
    {
        loopCountText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseLoopCounter()
    {
        if (loopCount < LOOP_UPPER_CAP)
        {
            loopCount += 1;
        }
        loopCountText.text = loopCount.ToString();
    }

    public void DecreaseLoopCounter()
    {
        if (loopCount > LOOP_LOWER_CAP)
        {
            loopCount -= 1;
        }
        loopCountText.text = loopCount.ToString();
    }
}
