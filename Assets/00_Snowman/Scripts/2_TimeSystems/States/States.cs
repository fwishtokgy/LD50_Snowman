using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    START, MAIN, END
}

public enum Season
{
    WINTER, SPRING, SUMMER, APOCALYPSE, SUPERNOVA
}

public enum HitType { TEMPERATURE_EFFECT, BAG_COLLISION, INSTADEATH }

[System.Flags]
public enum PropPlacement { BACKGROUND, MIDBACK, MIDFRONT, FRONT }

public enum PlayerRank { POO, DECENT, BRONZE, SILVER, GOLD, PLATINUM }