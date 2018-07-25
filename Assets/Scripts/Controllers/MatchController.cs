using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour {

    public List<GridElement> matchListVer;
    public List<GridElement> matchListHor;

    public bool CheckMatchVertical ()
    {
        for (int i = 0; i < GameController.instance.gridController.width; i++)
        {
            matchListVer = new List<GridElement>();
            
            for (int j = 0; j < GameController.instance.gridController.height; j++)
            {
                if(j==0)
                {
                    matchListVer.Add(GameController.instance.gridController.matrix[i, j]);
                }
                else if(GameController.instance.gridController.matrix[i, j].gemElement.index == GameController.instance.gridController.matrix[i, j-1].gemElement.index)
                {
                    matchListVer.Add(GameController.instance.gridController.matrix[i, j]);
                }
                else
                {
                    if (matchListVer.Count >= 3)
                    {
                        return true;
                    }
                    else
                    {
                        matchListVer = new List<GridElement>();
                        matchListVer.Add(GameController.instance.gridController.matrix[i, j]);
                    }
                }
            }
            if (matchListVer.Count >=3)
            {
                return true;
            }
            else
            {
                matchListVer = new List<GridElement>();
            }
        }
        return false;
    }

    public bool CheckMatchHorizontal()
    {
        for (int j = 0; j < GameController.instance.gridController.height; j++)
        {
            matchListHor = new List<GridElement>();

            for (int i = 0; i < GameController.instance.gridController.width; i++)
            {
                if (i == 0)
                {
                    matchListHor.Add(GameController.instance.gridController.matrix[i, j]);
                }
                else if (GameController.instance.gridController.matrix[i, j].gemElement.index == GameController.instance.gridController.matrix[i-1, j].gemElement.index)
                {
                    matchListHor.Add(GameController.instance.gridController.matrix[i, j]);
                }
                else
                {
                    if (matchListHor.Count >= 3)
                    {
                        return true;
                    }
                    else
                    {
                        matchListHor = new List<GridElement>();
                        matchListHor.Add(GameController.instance.gridController.matrix[i, j]);

                    }
                }
            }
            if (matchListHor.Count >= 3)
            {
                return true;
            }
            else
            {
                matchListHor = new List<GridElement>();
            }
        }
        return false;
    }

    public void Initialisation()
    {
        
    }

}
