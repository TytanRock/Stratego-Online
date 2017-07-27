using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
    public int strength = 0;
    public bool isRed = false;
    public int x = 0;
    public int y = 0;

    private void Update()
    {
        this.transform.position = BoardTracker.GetPosition(x, y);
    }
}
