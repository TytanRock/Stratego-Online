using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardTracker {
    private static float[] boardX = { -6.3f, -4.9f, -3.5f, -2.05f, -0.65f, 0.65f, 2.05f, 3.5f, 4.9f, 6.3f };
    private static float[] boardZ = { 4.65f, 3.2f, 1.85f, 0.45f, -0.95f, -2.3f, -3.7f, -5.05f };
    private static float boardY = 0.51f;

    public static Vector3 GetPosition(int x, int y)
    {
        return new Vector3(boardX[x], boardY, boardZ[y]);
    }
        
}
