using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {
    public int width = 3;
    public int height = 5;
    public float side = 0.87f;
    public int dropDistance = 2;
    public List<GridElement> gridElementsList;
    public GridElement[] emtyGridElement;
    public GridElement[,] matrix;
    public Transform containerTransform;
    public bool mHor, mVer, mCub;


    public void Initialisation()
    {
        gridElementsList = new List<GridElement>();
        emtyGridElement = new GridElement[3];
        matrix = new GridElement[width, height];
       
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                GridElement item = Instantiate(GameController.instance.assetscController.gemElementPrefab, containerTransform);
                matrix[i, j] = item;
                item.positionVecor2 = new Vector2(i, j);
                item.swipeElement.Initialisation();
                item.Initialisation();
            }
        }
        for (int i = 0; i < 3; i++)
        {
           CreateEmptyCells();
        }
        Shuffle();
    }

    public Vector2 ChoosePos ()
    {
        Vector2 _tempPos = new Vector2(Random.Range(0, width), Random.Range(0, height));
        if (matrix[(int)_tempPos.x, (int)_tempPos.y].swipeElement.gemElement.index == 99)
        {
            _tempPos = ChoosePos();
        }
        else
        {
            return _tempPos;
        }
        return _tempPos;
    }

    public void Shuffle(bool isAnim = false, bool shuflleButton =false)
    {
        while (GameController.instance.matchController.CheckMatchHorizontal() || GameController.instance.matchController.CheckMatchVertical()  || GameController.instance.matchController.CheckMatchCube() || shuflleButton ==true)
        {
            shuflleButton = false;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    if (matrix[i, j].swipeElement.gemElement.index!=99)
                    {
                        SwipeElement item = matrix[i, j].swipeElement;
                        Vector2 _tempPos = ChoosePos();
                        matrix[i, j].swipeElement = matrix[(int)_tempPos.x, (int)_tempPos.y].swipeElement;
                        matrix[(int)_tempPos.x, (int)_tempPos.y].swipeElement = item;
                    }
                }
            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                matrix[i, j].GetGemToPosition(0.7f, isAnim);
            }
        }

    }

    public void CreateEmptyCells()
    {
        int xRange = Random.Range(0, width);
        int yRange = Random.Range(0, height);
        if (matrix[xRange, yRange].isActive != false)
        {
            matrix[xRange, yRange].isActive = false;
        }
        else
        {
            CreateEmptyCells();
        }
    }

    public void ClickCalc(GridElement item)
    {
        if(gridElementsList.Count==1)
        {
            if(gridElementsList[0]==item)
            {
                return;
            }
        }
        gridElementsList.Add(item);
        if (gridElementsList.Count == 2)
        {
            if (Vector2.Distance(gridElementsList[0].positionVecor2, gridElementsList[1].positionVecor2) == 1)
            {
                SwipeElement _temp = gridElementsList[0].swipeElement;
                gridElementsList[0].swipeElement = gridElementsList[1].swipeElement;
                gridElementsList[1].swipeElement= _temp;
                mHor = GameController.instance.matchController.CheckMatchHorizontal();
                mVer = GameController.instance.matchController.CheckMatchVertical();
                mCub = GameController.instance.matchController.CheckMatchCube();

                    if (!mHor && !mVer && !mCub)
                    {
                        CallGemBack(item);
                        gridElementsList = new List<GridElement>();
                    }
                    else if (mHor || mVer || mCub)
                    {
                        gridElementsList[0].GetGemToPosition(0.3f);
                        gridElementsList[1].GetGemToPosition(0.3f);
                        GameController.instance.conditionController.CheckMoves();
                        GameController.instance.matchController.CheckMatch();
                    }
            }
            else
            {
                gridElementsList = new List<GridElement>();
                gridElementsList.Add(item);
            }
        }
        if (gridElementsList.Count > 2)
        {
            gridElementsList = new List<GridElement>();
            gridElementsList.Add(item);
        }
    }

    public void CallGemBack(GridElement item)
    {
        gridElementsList[1].GetGemToPosition(0.9f, true, false);
        gridElementsList[0].GetGemToPosition(0.9f, true, false);
        SwipeElement _temp1 = gridElementsList[1].swipeElement;
        gridElementsList[1].swipeElement = gridElementsList[0].swipeElement;
        gridElementsList[0].swipeElement = _temp1;
    }

    public void DropElement(List<GridElement> matchlist)
    {
        SwipeElement _tempSwipe;
        for (int i = matchlist.Count-1; i >=0; i--)
        {
            _tempSwipe = matchlist[i].swipeElement;
            _tempSwipe.transform.position += Vector3.up * side * dropDistance * (height - matchlist[i].positionVecor2.y + 1);
            _tempSwipe.gemElement.index = Random.Range(0, GameController.instance.assetscController.gemsSprite.Length);
            _tempSwipe.bombElement.index = 99;
            for (int j = 0; j < (height - 1) - (int)matchlist[i].positionVecor2.y; j++)
            {
                if(matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement.gemElement.index == 99 && matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement.bombElement.index == 99)
                {
                    if (matchlist[i].positionVecor2.x - 1 < 0)
                    {
                        matrix[(int)matchlist[i].positionVecor2.x + 1, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement = matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement;
                        matrix[(int)matchlist[i].positionVecor2.x + 1, (int)(matchlist[i].positionVecor2.y + (float)j)].GetGemToPosition(0.7f);
                    }
                    else if (matchlist[i].positionVecor2.x + 1 >= width)
                    {
                        matrix[(int)matchlist[i].positionVecor2.x - 1, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement = matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement;
                        matrix[(int)matchlist[i].positionVecor2.x - 1, (int)(matchlist[i].positionVecor2.y + (float)j)].GetGemToPosition(0.7f);
                    }
                    else
                    {
                        matrix[(int)matchlist[i].positionVecor2.x + 1, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement = matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement;
                        matrix[(int)matchlist[i].positionVecor2.x + 1, (int)(matchlist[i].positionVecor2.y + (float)j)].GetGemToPosition(0.7f);
                    }
                }
                else if(matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement.gemElement.index == 99 && matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement.bombElement.index == 99)
                {
                    if (matchlist[i].positionVecor2.x - 1 < 0)
                    {
                        matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement = matrix[(int)matchlist[i].positionVecor2.x + 1, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement;
                    }
                    else if(matchlist[i].positionVecor2.x + 1 >= width)
                    {
                        matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement = matrix[(int)matchlist[i].positionVecor2.x - 1, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement;
                    }
                    else
                    {
                        matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement = matrix[(int)matchlist[i].positionVecor2.x + 1, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement;
                    }
                    matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j)].GetGemToPosition(0.7f);
                }
                else
                {
                    matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j)].swipeElement = matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j + 1)].swipeElement;
                    matrix[(int)matchlist[i].positionVecor2.x, (int)(matchlist[i].positionVecor2.y + (float)j)].GetGemToPosition(0.7f);
                }
            }
            if(matrix[(int)matchlist[i].positionVecor2.x, height - 1].swipeElement.gemElement.index == 99 && matrix[(int)matchlist[i].positionVecor2.x, height - 1].swipeElement.bombElement.index == 99)
            {
                if(matchlist[i].positionVecor2.x-1 < 0 )
                {
                    matrix[(int)matchlist[i].positionVecor2.x + 1, height - 1].swipeElement = _tempSwipe;
                    matrix[(int)matchlist[i].positionVecor2.x + 1, height - 1].GetGemToPosition(0.7f);
                }
                else if(matchlist[i].positionVecor2.x + 1 >= width)
                {
                    matrix[(int)matchlist[i].positionVecor2.x - 1, height - 1].swipeElement = _tempSwipe;
                    matrix[(int)matchlist[i].positionVecor2.x - 1, height - 1].GetGemToPosition(0.7f);
                }
                else
                {
                    matrix[(int)matchlist[i].positionVecor2.x + 1, height - 1].swipeElement = _tempSwipe;
                    matrix[(int)matchlist[i].positionVecor2.x + 1, height - 1].GetGemToPosition(0.7f);
                }
            }
            else
            {
                matrix[(int)matchlist[i].positionVecor2.x , height - 1].swipeElement = _tempSwipe;
                matrix[(int)matchlist[i].positionVecor2.x , height - 1].GetGemToPosition(0.7f);
            }
        }
        GameController.instance.gridController.gridElementsList = new List<GridElement>();

    }
}
