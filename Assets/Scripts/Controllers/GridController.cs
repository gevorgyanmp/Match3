﻿using System.Collections;
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
                item.gemElement.index = Random.Range(0, GameController.instance.assetscController.gemsSprite.Length);
                item.Initialisation();
            }
        }
        for (int i = 0; i < 3; i++)
        {
           // CreateEmptyCells();
        }
        Shuffle();
       

        

    }
    public void Shuffle(bool isAnim = false, bool shuflleButton =false)
    {
        while (GameController.instance.matchController.CheckMatchHorizontal() || GameController.instance.matchController.CheckMatchVertical()  || shuflleButton==true)
        {
            shuflleButton = false;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    GemElement item = matrix[i, j].gemElement;
                    item.index = Random.Range(0, GameController.instance.assetscController.gemsSprite.Length);
                    Vector2 _tempPos = new Vector2(Random.Range(0, width), Random.Range(0, height));
                    matrix[i, j].gemElement = matrix[(int)_tempPos.x, (int)_tempPos.y].gemElement;
                    matrix[(int)_tempPos.x, (int)_tempPos.y].gemElement = item;
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
        if(gridElementsList.Count == 2)
        {
            if(Vector2.Distance(gridElementsList[0].positionVecor2,gridElementsList[1].positionVecor2)==1)
            {
                GemElement _temp = gridElementsList[0].gemElement;
                gridElementsList[0].gemElement = gridElementsList[1].gemElement;
                gridElementsList[1].gemElement = _temp;

                gridElementsList[0].GetGemToPosition(0.3f);
                gridElementsList[1].GetGemToPosition(0.3f);
            }
            else
            {
                Debug.Log("too far");
            }
            gridElementsList = new List<GridElement>();
        }
    }
	
    public void DropElement (GridElement dropGem)
    {
        GemElement tempGem = dropGem.gemElement;

            tempGem.transform.position += Vector3.up * side * dropDistance*(height- dropGem.positionVecor2.y+1);
            tempGem.index = Random.Range(0, GameController.instance.assetscController.gemsSprite.Length);
            dropGem.GetGemToPosition(0.7f);
            Debug.Log(dropGem.positionVecor2.y + " vs " + (height - 1));
    }

    public void DropElementVertical(List<GridElement> matchlist)
    {
        List<GemElement> _tempGem = new List<GemElement>();
         for (int i = 0; i < matchlist.Count; i++)
         {
             _tempGem.Add(matchlist[i].gemElement);
             _tempGem[i].transform.position += Vector3.up * side * dropDistance * (height - matchlist[i].positionVecor2.y + 1);
             _tempGem[i].index = Random.Range(0, GameController.instance.assetscController.gemsSprite.Length);
         }

         int externalMove=0;
         for (int i = 0; i < (height - 1) - (int)matchlist[matchlist.Count-1].positionVecor2.y; i++)
         {
             if(matrix[(int)matchlist[0].positionVecor2.x, (int)(matchlist[0].positionVecor2.y + (float)i + _tempGem.Count)].gemElement.index == 99)
             {
                /* if(matrix[(int)matchlist[0].positionVecor2.x, (int)(matchlist[0].positionVecor2.y + (float)i + _tempGem.Count)].positionVecor2.y >= height-1)
                 {

                 }
                 else
                 {
                     externalMove++;
                 }*/
             }
             if(matrix[(int)matchlist[0].positionVecor2.x, (int)(matchlist[0].positionVecor2.y + (float)i)].gemElement.index == 99)
             {

             }
             matrix[(int)matchlist[0].positionVecor2.x, (int)(matchlist[0].positionVecor2.y + (float)i )].gemElement = matrix[(int)matchlist[0].positionVecor2.x, (int)(matchlist[0].positionVecor2.y + (float)i + _tempGem.Count+ externalMove)].gemElement;
             matrix[(int)matchlist[0].positionVecor2.x, (int)(matchlist[0].positionVecor2.y + (float)i)].GetGemToPosition(0.7f);
             /*if (externalMove>0)
             {
                 //externalMove--;
             }*/
            // Debug.Log((int)(matchlist[0].positionVecor2.y + (float)i + 1f) + " " + (int)(matchlist[0].positionVecor2.y + (float)i + _tempGem.Count)) ;
         }
         for (int i = 0; i < _tempGem.Count; i++)
         {
             matrix[(int)matchlist[0].positionVecor2.x, height - _tempGem.Count + i].gemElement = _tempGem[i];
             matrix[(int)matchlist[0].positionVecor2.x, height - _tempGem.Count + i].GetGemToPosition(0.7f);
         }

    }

    public void DropElementHorizontal(List<GridElement> matchlist)
    {
        Debug.Log((int)matchlist[matchlist.Count - 1].positionVecor2.x);
        List<GemElement> _tempGem = new List<GemElement>();
        for (int i = 0; i < matchlist.Count; i++)
        {
            _tempGem.Add(matchlist[i].gemElement);
            _tempGem[i].transform.position += Vector3.up * side * dropDistance * (height - matchlist[i].positionVecor2.y + 1);
            _tempGem[i].index = Random.Range(0, GameController.instance.assetscController.gemsSprite.Length);
        }
        for (int i = 0; i < matchlist.Count; i++)
        {
            for (int j = 0; j < height-(matchlist[i].positionVecor2.y+1); j++)
            {
                matrix[(int)matchlist[i].positionVecor2.x, (int)matchlist[i].positionVecor2.y + j].gemElement = matrix[(int)matchlist[i].positionVecor2.x, (int)matchlist[i].positionVecor2.y +(j + 1)].gemElement;
                matrix[(int)matchlist[i].positionVecor2.x, (int)matchlist[i].positionVecor2.y + j].GetGemToPosition(0.7f);
                Debug.Log("Loop " + i + " - " + j);
            }
            matrix[(int)matchlist[i].positionVecor2.x, height - 1].gemElement =  _tempGem[i];
            matrix[(int)matchlist[i].positionVecor2.x, height - 1].GetGemToPosition(0.7f);
        }
    }
}
