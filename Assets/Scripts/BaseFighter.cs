using UnityEngine;
using System.Collections;

public class BaseFighter : BaseUnit
{
    GameObject northPad;
    GameObject southPad;
    GameObject eastPad;
    GameObject westPad;

    BaseUnit northAttack, southAttack, eastAttack, westAttack, caller;

    Animator thisAnimator;

    private bool _isHighlighted, _isHighlightedForFight;

    bool movingNorth, movingSouth, movingEast, movingWest;
    public bool endAnimation;

    //Function to call instead of Start()
    public override void baseStart()
    {
        baseFighterStart();
        _isHighlighted = _isHighlightedForFight = false;
        northPad = transform.parent.GetChild(1).gameObject;
        southPad = transform.parent.GetChild(2).gameObject;
        eastPad = transform.parent.GetChild(3).gameObject;
        westPad = transform.parent.GetChild(4).gameObject;
        thisAnimator = GetComponent<Animator>();

        movingNorth = movingSouth = movingEast = movingWest = endAnimation = false;
        northAttack = southAttack = eastAttack = westAttack = caller = null;
    }

    //Function to call instead of Update()
    public override void baseUpdate()
    {
        if(movingNorth && endAnimation) { movingNorth = false; y--; }
        if(movingSouth && endAnimation) { movingSouth = false; y++; }
        if(movingEast && endAnimation) { movingEast = false; x++; }
        if(movingWest && endAnimation) { movingWest = false; x--; }
        this.transform.parent.position = BoardTracker.GetPosition(x, y);
        endAnimation = false;
    }

    //Virtual method for use in derived class
    public virtual void baseFighterStart()
    {
        //Do nothing if not overridden
    }

    //Highlight object if clicked
    private void OnMouseDown()
    {
        if (!_isHighlighted && !_isHighlightedForFight)
        {
            //Keep track of if it's highlighted or not
            Manager.switchHighlight(index, isRed);

            //Take highlight from other character
            bool north, south, east, west;

            //Check for teammates in space
            north = !Manager.checkForFighter(x, y - 1, isRed);
            south = !Manager.checkForFighter(x, y + 1, isRed);
            east = !Manager.checkForFighter(x + 1, y, isRed);
            west = !Manager.checkForFighter(x - 1, y, isRed);

            //Check for opponents in space
            northAttack = Manager.findFighter(x, y - 1, !isRed);
            southAttack = Manager.findFighter(x, y + 1, !isRed);
            eastAttack = Manager.findFighter(x + 1, y, !isRed);
            westAttack = Manager.findFighter(x - 1, y, !isRed);

            //Turn off highlight if there's an opponent in the square
            north &= !northAttack;
            south &= !southAttack;
            east &= !eastAttack;
            west &= !westAttack;

            //Check if at edge of map
            if (x == 0) west = false;
            if (x == 9) east = false;
            if (y == 0) north = false;
            if (y == 7) south = false;

            //Check for crater and volcano
            if (y == 2 && (x == 2 || x == 3 || x == 6 || x == 7)) south = false;
            if ((y == 3 || y == 4) && (x == 1 || x == 5)) east = false;
            if ((y == 3 || y == 4) && (x == 4 || x == 8)) west = false;
            if (y == 5 && (x == 2 || x == 3 || x == 6 || x == 7)) north = false;

            //Apply to highlight squares
            if (north) northPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = true;
            else northPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = false;

            if (south) southPad.GetComponent<MeshRenderer>().enabled = southPad.GetComponent<MeshCollider>().enabled = true;
            else southPad.GetComponent<MeshRenderer>().enabled = southPad.GetComponent<MeshCollider>().enabled = false;

            if (east) eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = true;
            else eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = false;

            if (west) westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = true;
            else westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = false;

            if (northAttack) northAttack.highlightForFight(this);
            if (southAttack) southAttack.highlightForFight(this);
            if (eastAttack) eastAttack.highlightForFight(this);
            if (westAttack) westAttack.highlightForFight(this);
            _isHighlighted = true;
        }
        else if(_isHighlightedForFight)
        {
            caller.callBack(index);
        }
        else
        {
            Manager.switchHighlight(-1, isRed);
            northAttack = southAttack = eastAttack = westAttack = null;
        }
    }

    public void move(GameObject caller)
    {
        if (caller.Equals(northPad))
        {
            thisAnimator.SetTrigger("MoveNorthF");
            movingNorth = true;
        }
        if (caller.Equals(southPad))
        {
            thisAnimator.SetTrigger("MoveSouthF");
            movingSouth = true;
        }
        if (caller.Equals(eastPad))
        {
            thisAnimator.SetTrigger("MoveEastF");
            movingEast = true;
        }
        if (caller.Equals(westPad))
        {
            thisAnimator.SetTrigger("MoveWestF");
            movingWest = true;
        }
    }

    //Turn off highlight from Manager.cs
    public override void highlightOff()
    {
        //Turn off highlights
        westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = false;
        eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = false;
        southPad.GetComponent<MeshRenderer>().enabled = southPad.GetComponent<MeshCollider>().enabled = false;
        northPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = false;

        if (northAttack) northAttack.highlightForFightOff();
        if (southAttack) southAttack.highlightForFightOff();
        if (eastAttack) eastAttack.highlightForFightOff();
        if (westAttack) westAttack.highlightForFightOff();
        
        _isHighlighted = false;
    }

    public override void callBack(int id)
    {
        if(_isHighlighted)
        {
            checkFight(northAttack, id, 0);
            checkFight(eastAttack, id, 1);
            checkFight(southAttack, id, 2);
            checkFight(westAttack, id, 3);
        }
    }

    public override void highlightForFight(BaseFighter person)
    {
        Debug.Log("Highlighted");
        _isHighlightedForFight = true;
        Vector3 temp = new Vector3(1.2f, 1.2f, 1.2f);
        gameObject.transform.localScale = temp;
        caller = person;
    }

    public override void highlightForFightOff()
    {
        Debug.Log("UnHighlighted");
        Vector3 temp = new Vector3(1f, 1f, 1f);
        gameObject.transform.localScale = temp;
        _isHighlightedForFight = false;
        caller = null;
    }

    private void checkFight(BaseUnit baseUnit, int id, int direction)
    {
        if (baseUnit)
        {
            if (baseUnit.index == id)
            {
                if(baseUnit.strength == 10 && strength == 0)
                {
                    //Dragon V Slayer, dragon loses
                    Destroy(baseUnit.transform.parent.gameObject);
                    switch(direction)
                    {
                        default: break;
                        case 0: movingNorth = true; thisAnimator.SetTrigger("MoveNorthF"); break;
                        case 1: movingEast = true; thisAnimator.SetTrigger("MoveEastF"); break;
                        case 2: movingWest = true; thisAnimator.SetTrigger("MoveWestF"); break;
                        case 3: movingSouth = true; thisAnimator.SetTrigger("MoveSouthF"); break;
                    }
                    Manager.endTurn();
                }
                else if(baseUnit.strength == 11 && strength == 3)
                {
                    //Trap V Dwarf, Trap loses
                    Destroy(baseUnit.transform.parent.gameObject);
                    switch (direction)
                    {
                        default: break;
                        case 0: movingNorth = true; thisAnimator.SetTrigger("MoveNorthF"); break;
                        case 1: movingEast = true; thisAnimator.SetTrigger("MoveEastF"); break;
                        case 2: movingWest = true; thisAnimator.SetTrigger("MoveWestF"); break;
                        case 3: movingSouth = true; thisAnimator.SetTrigger("MoveSouthF"); break;
                    }
                    Manager.endTurn();
                }
                else if (baseUnit.strength > strength)
                {
                    //Destroy this, end turn
                    Manager.endTurn();
                    Destroy(this.transform.parent.gameObject);
                }
                else if (strength > baseUnit.strength)
                {
                    //Destroy other, move into other, end turn
                    Destroy(baseUnit.transform.parent.gameObject);
                    switch (direction)
                    {
                        default: break;
                        case 0: movingNorth = true; thisAnimator.SetTrigger("MoveNorthF"); break;
                        case 1: movingEast = true; thisAnimator.SetTrigger("MoveEastF"); break;
                        case 2: movingWest = true; thisAnimator.SetTrigger("MoveWestF"); break;
                        case 3: movingSouth = true; thisAnimator.SetTrigger("MoveSouthF"); break;
                    }
                    Manager.endTurn();
                }
                else
                {
                    Destroy(baseUnit.transform.parent.gameObject);
                    Manager.endTurn();
                    Destroy(this.transform.parent.gameObject);
                }
            }
        }
    }
}
