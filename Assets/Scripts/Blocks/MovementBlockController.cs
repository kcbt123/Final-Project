using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBlockController : CodeBlockController
{
    /** ======= MARK: - Fields and Properties ======= */

    [SerializeField]
    private Sprite iconUp;
    [SerializeField]
    private Sprite iconDown;
    [SerializeField]
    private Sprite iconLeft;
    [SerializeField]
    private Sprite iconRight;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetBlockData();
    }

    public void SetBlockData()
    {
        if (base._data.blockType == BlockType.MOVEMENT)
        {
            if (base._data.blockIdentifier == MovementBlockIdentifier.UP)
            {
                _codeBlockIcon.sprite = iconUp;
                _textCodeBlock.text = "man.moveUp()";
            }
            else if (base._data.blockIdentifier == MovementBlockIdentifier.DOWN)
            {
                _codeBlockIcon.sprite = iconDown;
                _textCodeBlock.text = "man.moveDown()";
            }
            else if (base._data.blockIdentifier == MovementBlockIdentifier.LEFT)
            {
                _codeBlockIcon.sprite = iconLeft;
                _textCodeBlock.text = "man.moveLeft()";
            }
            else if (base._data.blockIdentifier == MovementBlockIdentifier.RIGHT)
            {
                _codeBlockIcon.sprite = iconRight;
                _textCodeBlock.text = "man.moveRight()";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
