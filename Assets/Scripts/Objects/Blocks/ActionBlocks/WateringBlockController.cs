using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WateringBlockController : BlockItemBase
{
    /** ======= MARK: - Fields and Properties ======= */

    private Image blockIcon;
    private TextMeshProUGUI blockText;

    [SerializeField]
    private Sprite iconWaterYellow;
    [SerializeField]
    private Sprite iconWaterWhite;
    [SerializeField]
    private Sprite iconWaterRed;

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

    // Update is called once per frame
    private void SetBlockData()
    {
        if (data.blockType == BlockItemType.ACTION)
        {
            switch (data.blockIdentifier)
            {
                case BlockItemIdentifier.ACTION_WATERING_YELLOW:
                    blockIcon.sprite = iconWaterYellow;
                    blockText.text = "man.waterYellow();";
                    break;
                case BlockItemIdentifier.ACTION_WATERING_WHITE:
                    blockIcon.sprite = iconWaterWhite;
                    blockText.text = "man.waterWhite();";
                    break;
                case BlockItemIdentifier.ACTION_WATERING_RED:
                    blockIcon.sprite = iconWaterRed;
                    blockText.text = "man.waterRed();";
                    break;
            }
        }
    }
}
