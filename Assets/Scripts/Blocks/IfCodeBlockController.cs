using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IfCodeBlockController : BlockBase
{
    // Start is called before the first frame update

    // Can be 0, 1, 2
    public int targetedCondition;

    [SerializeField]
    private Sprite yellowFlower;
    [SerializeField]
    private Sprite whiteFlower;
    [SerializeField]
    private Sprite redFlower;

    [SerializeField]
    private GameObject childBlock1;
    [SerializeField]
    private GameObject childBlock2;

    private void Awake()
    {
        if (targetedCondition == 0)
        {
            transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = yellowFlower;
        } else if (targetedCondition == 1)
        {
            transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = whiteFlower;
        } else if (targetedCondition == 2)
        {
            transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = redFlower;
        }
    }
}
