using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringBlockController : CodeBlockController
{
    [SerializeField]
    private Sprite iconWatering;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetBlockData();
    }

    // Update is called once per frame
    public void SetBlockData()
    {
        if (base._data.blockType == BlockType.ACTION)
        {
            if (base._data.blockIdentifier == MovementBlockIdentifier.WATERING)
            {
                _codeBlockIcon.sprite = iconWatering;
                _textCodeBlock.text = "man.water()";
            }
        }
    }
}
