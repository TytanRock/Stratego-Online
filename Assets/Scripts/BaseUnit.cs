using UnityEngine;
using System.Collections;

public class BaseUnit : MonoBehaviour
{
    public int index { get; set; }
    public uint strength { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public bool isRed { get; set; }


    private void Start()
    {
        baseStart();
        index = Manager.getCurrentIndex(isRed);
        Manager.addToArray(this);
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

    public void destroyThis()
    {
        Destroy(this.transform.parent.gameObject);
    }

    public virtual void highlightForFight(BaseFighter person)
    {
        //Must do something in derived class for fighting
    }

    public virtual void highlightForFightOff()
    {
        //Must do something in derived class for fighting
    }

    public virtual void callBack(int id)
    {
        //Must do something in derived class for fighting
    }
}
