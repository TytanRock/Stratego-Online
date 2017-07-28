using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slayer : MonoBehaviour, IBaseFighter {
    int _index;
    uint _strength = 0;
    public int _x;
    public int _y;
    bool _isRed;

    GameObject northPad;
    GameObject southPad;
    GameObject eastPad;
    GameObject westPad;

    private void Start()
    {
        northPad = transform.parent.GetChild(1).gameObject;
        southPad = transform.parent.GetChild(2).gameObject;
        eastPad = transform.parent.GetChild(3).gameObject;
        westPad = transform.parent.GetChild(4).gameObject;

        _index = Manager.getCurrentIndex(_isRed);
        Manager.addToArray(this);
    }

    private void OnMouseDown()
    {
        //Take highlight from other character
        Manager.switchHighlight(_index, _isRed);
        bool north, south, east, west;

        //Check for teammates in space
        north = !Manager.checkForFighter(x, y - 1, _isRed);
        south = !Manager.checkForFighter(x, y + 1, _isRed);
        east = !Manager.checkForFighter(x + 1, y, _isRed);
        west = !Manager.checkForFighter(x - 1, y, _isRed);

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

        if (south) southPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = true;
        else southPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = false;

        if (east) eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = true;
        else eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = false;

        if (west) westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = true;
        else westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = false;
    }

    public void moveHere(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void highlightOff()
    {
        //Turn off highlights
        westPad.GetComponent<MeshRenderer>().enabled = westPad.GetComponent<MeshCollider>().enabled = false;
        eastPad.GetComponent<MeshRenderer>().enabled = eastPad.GetComponent<MeshCollider>().enabled = false;
        southPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = false;
        northPad.GetComponent<MeshRenderer>().enabled = northPad.GetComponent<MeshCollider>().enabled = false;
    }

    private void Update()
    {
        this.transform.parent.position = BoardTracker.GetPosition(_x, _y);
    }

    //Implement getters and setters of IBaseFighter
    public uint strength { get { return _strength; } }
    public bool isRed { get { return _isRed; } set { _isRed = value; } }
    public int x { get { return _x; } }
    public int y { get { return _y; } }
    public int index { get { return _index; } }
}
