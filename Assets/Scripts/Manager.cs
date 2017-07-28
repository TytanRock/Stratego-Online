using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager {
    public static IBaseFighter[] RedFighters = new IBaseFighter[30];
    public static IBaseFighter[] BlueFighters = new IBaseFighter[30];

    static int indexOfHighlightedRedObject = -1;
    static int indexOfHighlightedBlueObject = -1;

    static int redFighterIndex = 0;
    static int blueFighterIndex = 0;

    public static int getCurrentIndex(bool isRed)
    {
        if (isRed) return redFighterIndex++;
        else return blueFighterIndex++;
    }

    public static void addToArray(IBaseFighter fighter)
    {
        if(fighter.isRed)
        {
            RedFighters[fighter.index] = fighter;
        }
        else
        {
            BlueFighters[fighter.index] = fighter;
        }
    }

    public static void switchHighlight(int index, bool isRed)
    {
        if(isRed)
        {
            if (indexOfHighlightedRedObject >= 0) RedFighters[indexOfHighlightedRedObject].highlightOff();
            indexOfHighlightedRedObject = index;
        }
        else
        {
            if (indexOfHighlightedBlueObject >= 0) BlueFighters[indexOfHighlightedBlueObject].highlightOff();
            indexOfHighlightedBlueObject = index;
        }
    }

    public static bool checkForFighter(int x, int y, bool isRed)
    {
        if (isRed)
        {
            for(int i = 0; i < redFighterIndex; i++)
            {
                if (RedFighters[i].x == x && RedFighters[i].y == y) return true;
                Debug.Log(RedFighters[i].x.ToString());
                Debug.Log(RedFighters[i].y.ToString());
            }
        }
        else
        {
            for(int i = 0; i < blueFighterIndex; i++)
            {
                if (BlueFighters[i].x == x && BlueFighters[i].y == y) return true;
            }
        }
        return false;
    }

}
