using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockItemType
{
    MOVEMENT,
    ACTION,
    SPECIAL,
}

public enum BlockItemIdentifier
{
    MOVEMENT_UP,
    MOVEMENT_DOWN,
    MOVEMENT_LEFT,
    MOVEMENT_RIGHT,

    ACTION_WATERING_YELLOW,
    ACTION_WATERING_WHITE,
    ACTION_WATERING_RED,

    SPECIAL_FOR,
    SPECIAL_IF,
    SPECIAL_FUNCTION,
}

