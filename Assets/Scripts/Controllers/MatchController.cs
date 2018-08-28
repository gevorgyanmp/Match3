using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour {

    public List<GridElement> matchListVer;
    public List<GridElement> matchListHor;
    public List<GridElement> matchListCube;
    public bool isAuto = false;

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
                else if(GameController.instance.gridController.matrix[i, j].swipeElement.gemElement.index == GameController.instance.gridController.matrix[i, j-1].swipeElement.gemElement.index)
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
                else if (GameController.instance.gridController.matrix[i, j].swipeElement.gemElement.index == GameController.instance.gridController.matrix[i-1, j].swipeElement.gemElement.index)
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

    public bool CheckMatchCube()
    {
        for (int j = 0; j < GameController.instance.gridController.height; j++)
        {
            matchListCube = new List<GridElement>();

            for (int i = 0; i < GameController.instance.gridController.width; i++)
            {
                if (i == 0)
                {
                    matchListCube.Add(GameController.instance.gridController.matrix[i, j]);
                }
                else if (GameController.instance.gridController.matrix[i, j].swipeElement.gemElement.index == GameController.instance.gridController.matrix[i - 1, j].swipeElement.gemElement.index && j+1 < GameController.instance.gridController.height)
                {
                    matchListCube.Add(GameController.instance.gridController.matrix[i, j]);
                    j++;
                    i--;
                    if(GameController.instance.gridController.matrix[i, j].swipeElement.gemElement.index == GameController.instance.gridController.matrix[i , j-1].swipeElement.gemElement.index)
                    {
                        matchListCube.Add(GameController.instance.gridController.matrix[i, j]);
                        i++;
                        if(GameController.instance.gridController.matrix[i, j].swipeElement.gemElement.index == GameController.instance.gridController.matrix[i - 1, j].swipeElement.gemElement.index)
                        {
                            matchListCube.Add(GameController.instance.gridController.matrix[i, j]);
                            return true;
                        }
                        else
                        {
                            j--;
                            matchListCube = new List<GridElement>();
                            matchListCube.Add(GameController.instance.gridController.matrix[i, j]);
                        }
                    }
                    else
                    {
                        j--;
                        i++;
                        matchListCube = new List<GridElement>();
                        matchListCube.Add(GameController.instance.gridController.matrix[i, j]);
                    }
                }
                else
                {
                    matchListCube = new List<GridElement>();
                    matchListCube.Add(GameController.instance.gridController.matrix[i, j]);
                }
            }
        }
        return false;
    }

    public bool MatchHor()
    {
        Sequence seq = DOTween.Sequence();
        bool matchHor = CheckMatchHorizontal();
        if (matchHor)
        {
            seq.InsertCallback(0.4f, () =>
            {
                if(matchListHor.Count>3)
                {
                    if(GameController.instance.gridController.gridElementsList.Count!=0)
                    {
                        GridElement gemToBomb = CheckTransformBomb(GameController.instance.gridController.gridElementsList, matchListHor);
                        GameController.instance.bombController.CreateBombHorizontal(gemToBomb.swipeElement);
                        matchListHor.Remove(gemToBomb);
                    }
                    else
                    {
                        GridElement gemToBomb = matchListHor[Random.Range(0, matchListHor.Count)];
                        GameController.instance.bombController.CreateBombHorizontal(gemToBomb.swipeElement);
                        matchListHor.Remove(gemToBomb);
                    }
                    
                }
                GameController.instance.conditionController.CheckCondition(matchListHor[0].swipeElement.gemElement);
                GameController.instance.gridController.DropElement(matchListHor);
            });
            return true;
        }
        return false;
    }

    public bool MatchVer()
    {
        Sequence seq = DOTween.Sequence();
        bool matchVer = CheckMatchVertical();
        if(matchVer)
        {
            seq.InsertCallback(0.4f, () =>
            {
                if (matchListVer.Count>3)
                {
                    GameController.instance.bombController.CreateBombVertical(matchListVer);
                }
                GameController.instance.conditionController.CheckCondition(matchListVer[0].swipeElement.gemElement);
                GameController.instance.gridController.DropElement(matchListVer);
            });
            return true;
        }
        return false;
    }

    public bool MatchCub()
    {
        Sequence seq = DOTween.Sequence();
        bool matchCub = CheckMatchCube();
        if (matchCub)
        {
            seq.InsertCallback(0.4f, () =>
            {
                GameController.instance.conditionController.CheckCondition(matchListCube[0].swipeElement.gemElement);
                GameController.instance.bombController.CreateBombCube(matchListCube);
                GameController.instance.gridController.DropElement(matchListCube);
            });
            return true;
        }
        return false;
    }

    public void EliMatch()
    {
        Sequence seq = DOTween.Sequence();
        seq.InsertCallback(1.4f,  () =>
        {
            CheckMatch();
        });
        seq.Play();
    }

    public void CheckMatch()
    {
        bool MHor = MatchHor();
        bool MVer = MatchVer();
        bool MCub = MatchCub();
        if ( MHor || MVer || MCub)
        {
            isAuto = true;
            EliMatch();
        }
        else
        {
            isAuto = false;
        }
    }

    public GridElement CheckTransformBomb(List<GridElement> calcList, List<GridElement> matchList)
    {
        GridElement curGrid = null;
        for (int i = 0; i < calcList.Count; i++)
        {
            for (int j = 0; j < matchList.Count; j++)
            {
                if (calcList[i].positionVecor2 == matchList[j].positionVecor2)
                {
                    curGrid = calcList[i];
                }
            }
        }
        return curGrid;
    }

    public void Initialisation()
    {
        
    }

}
