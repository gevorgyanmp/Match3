using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemElement : MonoBehaviour {

    public SpriteRenderer gemSpriteRenderer;


    private int _index;
    public int index
    {
        set
        {
            _index = value;
            if (value == 99)
            {
               // gemSpriteRenderer.sprite = null;
            }
            else
            {
                gemSpriteRenderer.sprite = GameController.instance.assetscController.gemsSprite[value];
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
