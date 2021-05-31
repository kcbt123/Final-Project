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
    private Sprite normalFlowerImage;
    [SerializeField]
    private Sprite redFlowerImage;
    [SerializeField]
    private Sprite yellowFlowerImage;

    [SerializeField]
    private GameObject childBlock1;
    [SerializeField]
    private GameObject childBlock2;

    private void Awake()
    {
        if (targetedCondition == 0)
        {
            transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = normalFlowerImage;
        } else if (targetedCondition == 1)
        {
            transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = redFlowerImage;
        } else if (targetedCondition == 2)
        {
            transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = yellowFlowerImage;
        }
    }
}
