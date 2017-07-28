using UnityEngine;

public interface IBaseFighter {

    void moveHere(int x, int y);

    void highlightOff();

    //Get characteristics of piece
    uint strength { get; }
    bool isRed { get; set; }
    int x { get; }
    int y { get; }
    int index { get; }
}
