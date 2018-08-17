using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombElement : MonoBehaviour {

    public SpriteRenderer bombSpriteRenderer;


    private int _index;
    public int index
    {
        set
        {
            _index = value;
            if (value == 99)
            {
                bombSpriteRenderer.sprite = null;
            }
            else
            {
                bombSpriteRenderer.sprite = GameController.instance.assetscController.bombSprite[value];
            }
        }
        get
        {
            return _index;
        }
    }

    public void Initialisation()
    {

    }

}
