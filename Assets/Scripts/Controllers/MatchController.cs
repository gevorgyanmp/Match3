using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour {

    public List<GridElement> matchListTemp;
    public List<List<GridElement>> matchListFull;

    public void CheckMatchVertical ()
    {
        for (int i = 0; i < GameController.instance.gridController.width; i++)
        {
            matchListTemp = new List<GridElement>();
            for (int j = 0; j < GameController.instance.gridController.height; j++)
            {
                if(j==0)
                {
                    matchListTemp.Add(GameController.instance.gridController.matrix[i, j]);
                }
                else if(GameController.instance.gridController.matrix[i, j].gemElement.index == GameController.instance.gridController.matrix[i, j-1].gemElement.index)
                {
                    matchListTemp.Add(GameController.instance.gridController.matrix[i, j]);
                }
                else
                {
                    if (matchListTemp.Count == 4)
                    {
                      //  Debug.Log("Count 4 from " + matchListTemp[0].positionVecor2 + " to " + matchListTemp[matchListTemp.Count - 1].positionVecor2);
                        for (int p = 0; p < matchListTemp.Count; p++)
                        {
                            GameController.instance.gridController.DropElement(matchListTemp[p]);
                        }
                        matchListTemp = new List<GridElement>();
                    }
                    else if (matchListTemp.Count == 3)
                    {
                        for (int p = 0; p < matchListTemp.Count; p++)
                        {
                            GameController.instance.gridController.DropElement(matchListTemp[p]);
                        }
                        matchListTemp = new List<GridElement>();
                    }
                    else
                    {
                        matchListTemp = new List<GridElement>();
                    }
                    matchListTemp.Add(GameController.instance.gridController.matrix[i, j]);
                }

               
            }

        }

    }

    public void Initialisation()
    {
        
    }

}
