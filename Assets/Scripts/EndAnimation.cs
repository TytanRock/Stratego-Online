using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour {
    public bool flag;
    private BaseFighter baseFighter;

    private void Start()
    {
        baseFighter = this.gameObject.GetComponent<BaseFighter>();
        flag = false;
    }

    private void Update()
    {
        if (flag) baseFighter.endAnimation = true;
    }
}
