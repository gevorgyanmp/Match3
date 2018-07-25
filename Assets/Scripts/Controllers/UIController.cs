using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public bool onGoingAnimation;


    public void Initialisation()
    {

    }

    public void ShuffleButtonOn()
    {
        if(onGoingAnimation)
        {
            return;
        }
        onGoingAnimation = true;
        Sequence seq = DOTween.Sequence();
        seq.InsertCallback(1f, () =>
        {
            onGoingAnimation = false;
        });

        GameController.instance.gridController.Shuffle(true,true);
    }

}
