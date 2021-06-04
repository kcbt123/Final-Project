using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovementBlockController : BlockItemBase
{
    /** ======= MARK: - Fields and Properties ======= */

    private Image blockIcon;
    private TextMeshProUGUI blockText;

    [SerializeField]
    private Sprite iconUp;
    [SerializeField]
    private Sprite iconDown;
    [SerializeField]
    private Sprite iconLeft;
    [SerializeField]
    private Sprite iconRight;

    /** ======= MARK: - MonoBehavior Functions ======= */

    public override void Awake()
    {
        blockIcon = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        blockText = transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public override void Start()
    {
        base.Start();
        SetBlockData();
    }

    /** ======= MARK: - Setup ======= */

    public void SetBlockData()
    {
        if (data.blockType == BlockItemType.MOVEMENT)
        {
            switch (data.blockIdentifier)
            {
                case BlockItemIdentifier.MOVEMENT_UP:
                    blockIcon.sprite = iconUp;
                    blockText.text = "man.moveUp();";
                    break;
                case BlockItemIdentifier.MOVEMENT_DOWN:
                    blockIcon.sprite = iconDown;
                    blockText.text = "man.moveDown();";
                    break;
                case BlockItemIdentifier.MOVEMENT_LEFT:
                    blockIcon.sprite = iconLeft;
                    blockText.text = "man.moveLeft();";
                    break;
                case BlockItemIdentifier.MOVEMENT_RIGHT:
                    blockIcon.sprite = iconRight;
                    blockText.text = "man.moveRight();";
                    break;
            }
        }
    }
}
