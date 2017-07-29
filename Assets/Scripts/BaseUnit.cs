using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public int index { get; set; }
    public uint strength { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public bool isRed { get; set; }


    private void Start()
    {
        index = Manager.getCurrentIndex(isRed);
        Manager.addToArray(this);
        baseStart();
    }

    private void Update()
    {
        baseUpdate();
    }

    public virtual void baseStart()
    {
        //Do nothing if not overloaded
    }

    public virtual void baseUpdate()
    {
        //Do nothing if not overloaded
    }

    public virtual void highlightOff()
    {
        //Do nothing if it's not a Fighter
    }
}
