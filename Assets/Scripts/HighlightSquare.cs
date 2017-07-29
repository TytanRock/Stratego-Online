using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSquare : MonoBehaviour {
    GameObject baseObject;
	// Use this for initialization
	void Start () {
        baseObject = this.transform.parent.GetChild(0).gameObject;
	}

    private void OnMouseDown()
    {
        baseObject.GetComponent<BaseFighter>().move(this.gameObject);
        baseObject.GetComponent<BaseFighter>().highlightOff();
    }
}
