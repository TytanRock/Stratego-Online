using UnityEngine;

public class BaseFighter : BaseUnit
{
    GameObject northPad;
    GameObject southPad;
    GameObject eastPad;
    GameObject westPad;

    //Function to call instead of Start()
    public override void baseStart()
    {
        northPad = transform.parent.GetChild(1).gameObject;
        southPad = transform.parent.GetChild(2).gameObject;
        eastPad = transform.parent.GetChild(3).gameObject;
        westPad = transform.parent.GetChild(4).gameObject;
        baseFighterStart();
    }

    //Function to call instead of Update()
    public override void baseUpdate()
    {
        this.transform.parent.position = BoardTracker.GetPosition(x, y);
    }

    //Virtual method for use in derived class
    public virtual void baseFighterStart()
    {
        //Do nothing if not overridden
    }

    //Highlight object if clicked
    private void OnMouseDown()
    {
        //Take highlight from other character
        Manager.switchHighlight( base.index, base.isRed);
        bool north, south, east, west;

        //Check for teammates in space
        north = !Manager.checkForFighter(x, y - 1, base.isRed);
        south = !Manager.checkForFighter(x, y + 1, base.isRed);
        east = !Manager.checkForFighter(x + 1, y, base.isRed);
        west = !Manager.checkForFighter(x - 1, y, base.isRed);

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

        //Apply to gameobject
        if (north) northPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = true;
        else northPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = false;

        if (south) southPad.GetComponent<MeshRenderer>().enabled = southPad.GetComponent<MeshCollider>().enabled = true;
        else southPad.GetComponent<MeshRenderer>().enabled = southPad.GetComponent<MeshCollider>().enabled = false;

        if (east) eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = true;
        else eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = false;

        if (west) westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = true;
        else westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = false;
    }

    public void move(GameObject caller)
    {
        if (caller.Equals(northPad)) y--;
        if (caller.Equals(southPad)) y++;
        if (caller.Equals(eastPad)) x++;
        if (caller.Equals(westPad)) x--;
    }

    //Turn off highlight from Manager.cs
    public override void highlightOff()
    {
        //Turn off highlights
        westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = false;
        eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = false;
        southPad.GetComponent<MeshRenderer>().enabled = southPad.GetComponent<MeshCollider>().enabled = false;
        northPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = false;
    }
}
