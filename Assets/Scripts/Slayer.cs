using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slayer : BaseFighter
{
    private uint _SlayerStrength = 0;
    public int _x;
    public int _y;
    public bool _isRed;

    public override void baseFighterStart()
    {
        strength = _SlayerStrength;
        x = _x;
        y = _y;
        isRed = _isRed;
    }
}
