using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager {
    public static BaseUnit[] RedFighters = new BaseUnit[30];
    public static BaseUnit[] BlueFighters = new BaseUnit[30];

    static int indexOfHighlightedRedObject = -1;
    static int indexOfHighlightedBlueObject = -1;

    static int redFighterIndex = 0;
    static int blueFighterIndex = 0;

    public static int getCurrentIndex(bool isRed)
    {
        if (isRed) return redFighterIndex++;
        else return blueFighterIndex++;
    }

    public static void addToArray(BaseUnit fighter)
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
                if(RedFighters[i])
                    if (RedFighters[i].x == x && RedFighters[i].y == y) return true;
            }
        }
        else
        {
            for(int i = 0; i < blueFighterIndex; i++)
            {
                if (BlueFighters[i])
                    if (BlueFighters[i].x == x && BlueFighters[i].y == y) return true;
            }
        }
        return false;
    }

    public static BaseUnit findFighter(int x, int y, bool isRed)
    {
        if (isRed)
        {
            for (int i = 0; i < redFighterIndex; i++)
            {
                if (RedFighters[i].x == x && RedFighters[i].y == y) return RedFighters[i];
            }
        }
        else
        {
            for (int i = 0; i < blueFighterIndex; i++)
            {
                if (BlueFighters[i].x == x && BlueFighters[i].y == y) return BlueFighters[i];
            }
        }
        return null;
    }

    public static void endTurn()
    {
        //Do something here
    }

}
