using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    MOVEMENT,
    ACTION,
}

public enum MovementBlockIdentifier
{
    UP,
    DOWN,
    LEFT,
    RIGHT,

    WATERING,
}

public enum ActionBlockIdentifier
{
    WATERING,
}


[System.Serializable]
public class BlockJSONData
{
    public int itemID;
    public BlockType blockType;
    public MovementBlockIdentifier blockIdentifier;
}
