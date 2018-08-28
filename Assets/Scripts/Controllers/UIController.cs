using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour {

    public Text steps, firstCount, secCount, thirdCount;
    public SpriteRenderer firstCond, SecCond, thirdCond;



    public bool onGoingAnimation;

    public void Initialisation()
    {
        steps.text = GameController.instance.conditionController.steps.ToString();
        firstCond.sprite = GameController.instance.assetscController.gemsSprite[GameController.instance.conditionController.CondGemsIndex[0]];
        SecCond.sprite = GameController.instance.assetscController.gemsSprite[GameController.instance.conditionController.CondGemsIndex[1]];
        thirdCond.sprite = GameController.instance.assetscController.gemsSprite[GameController.instance.conditionController.CondGemsIndex[2]];
        firstCount.text = GameController.instance.conditionController.Totalmoves[0].ToString();
        secCount.text = GameController.instance.conditionController.Totalmoves[1].ToString();
        thirdCount.text = GameController.instance.conditionController.Totalmoves[2].ToString();
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
        GameController.instance.conditionController.steps--;
        steps.text = GameController.instance.conditionController.steps.ToString();
        GameController.instance.gridController.Shuffle(true,true);
    }


}
