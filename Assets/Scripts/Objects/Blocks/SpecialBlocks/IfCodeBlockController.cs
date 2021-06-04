using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IfCodeBlockController : BlockItemBase
{
    /** ======= MARK: - Fields and Properties ======= */

    public int targetedCondition;
    private Image conditionImage;
    private readonly int DEFAULT_INDEX = 0;

    [SerializeField]
    private Sprite yellowFlower;
    [SerializeField]
    private Sprite whiteFlower;
    [SerializeField]
    private Sprite redFlower;

    [SerializeField]
    private GameObject[] childBlocks;

    /** ======= MARK: - MonoBehavior Functions ======= */

    public override void Awake()
    {
        conditionImage = transform.GetChild(3).GetChild(1).GetComponent<Image>();

        OnChangeCondition(DEFAULT_INDEX);
    }

    /** ======= MARK: - Actions ======= */

    public void OnChangeCondition(int target)
    {
        if (target == 0)
        {
            conditionImage.sprite = yellowFlower;
        }
        else if (target == 1)
        {
            conditionImage.sprite = whiteFlower;
        }
        else if (target == 2)
        {
            conditionImage.sprite = redFlower;
        }
    }
}
