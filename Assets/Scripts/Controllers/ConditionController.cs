using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionController : MonoBehaviour {

    public int steps = 50;

    public List<int> CondGemsIndex;
    public List<int> Totalmoves;

    public void CreateConditions()
    {
        CondGemsIndex = new List<int>();
        Totalmoves = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            CondGemsIndex.Add(Random.Range(0, GameController.instance.assetscController.gemsSprite.Length));
            Totalmoves.Add(Random.Range(6, 13));
        }
        if (CondGemsIndex[0] == CondGemsIndex[1] || CondGemsIndex[0] == CondGemsIndex[2] || CondGemsIndex[1] == CondGemsIndex[2])
        {
            CreateConditions();
        }

    }

    public void CheckMoves()
    {
        steps--;
        GameController.instance.uIController.steps.text = GameController.instance.conditionController.steps.ToString();
    }

    public void CheckCondition(GemElement gem)
    {
        for (int i = 0; i < 3; i++)
        {
            if(gem.index == CondGemsIndex[i])
            {
                Totalmoves[i]--;
                if(i==0)
                {
                    GameController.instance.uIController.firstCount.text = Totalmoves[i].ToString();
                }
                else if (i == 1)
                {
                    GameController.instance.uIController.secCount.text = Totalmoves[i].ToString();
                }
                else if (i == 2)
                {
                    GameController.instance.uIController.thirdCount.text = Totalmoves[i].ToString();
                }
            }
        }
    }

    public void Initialisation()
    {
        CreateConditions();
    }

}
