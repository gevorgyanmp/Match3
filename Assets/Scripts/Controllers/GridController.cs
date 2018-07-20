using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {
    public int width = 7;
    public int height = 8;
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
            CreateEmptyCells();
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

}
