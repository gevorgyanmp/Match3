using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public AssetsController assetscController;
    public GridController gridController;
    public MatchController matchController;
    public UIController uIController;
    public BombController bombController;


    public static GameController instance;

	void Awake () {
		
        if(!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Initialisation(); 
	}

    public void Initialisation()
    {
        assetscController.Initialisation();
        gridController.Initialisation();
        matchController.Initialisation();
        uIController.Initialisation();
        bombController.Initialisation();
    }

}
