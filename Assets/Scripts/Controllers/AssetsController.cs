using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsController : MonoBehaviour {

    public Sprite[] gridsSprite;
    public Sprite[] gemsSprite;
    public Sprite[] bombSprite;
    public GridElement gemElementPrefab;

    public void Initialisation()
    {
        gridsSprite = new Sprite[2];
        gemsSprite = new Sprite[5];
        bombSprite = new Sprite[3];

        gridsSprite = Resources.LoadAll<Sprite>("Sprites/Grid");
        gemsSprite = Resources.LoadAll<Sprite>("Sprites/Gem");
        bombSprite = Resources.LoadAll<Sprite>("Sprites/Bomb");
    }
}
