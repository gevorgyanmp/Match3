using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeElement : MonoBehaviour {

    public GemElement gemElement;
    public BombElement bombElement;

    public void Initialisation()
    {
        gemElement.index = Random.Range(0, GameController.instance.assetscController.gemsSprite.Length);
        bombElement.index = 99;

    }
}
