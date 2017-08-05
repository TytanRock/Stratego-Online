using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : BaseFighter
{
    private uint _ScoutStrength = 2;
    public int _x;
    public int _y;
    public bool _isRed;

    public override void baseFighterStart()
    {
        strength = _ScoutStrength;
        x = _x;
        y = _y;
        isRed = _isRed;
    }
}
