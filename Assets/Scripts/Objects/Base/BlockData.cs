using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData
{
    public int itemCurrentIndex;
    public int blockCurrentIndex;
    public int functionBlockNumber = 0;
    public BlockItemType blockType;
    public BlockItemIdentifier blockIdentifier;
}
