using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsController : MonoBehaviour {

    public Sprite[] gridsSprite;
    public Sprite[] gemsSprite;
    public GridElement gemElementPrefab;


    public void Initialisation()
    {
        gridsSprite = new Sprite[2];
        gemsSprite = new Sprite[5];

        gridsSprite = Resources.LoadAll<Sprite>("Sprites/Grid");
        gemsSprite = Resources.LoadAll<Sprite>("Sprites/Gem");


    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
