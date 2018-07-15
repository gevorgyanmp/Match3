using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {
    [Header("Board Settings")]
    public int length = 7;
    public int width = 7;
    [Space]
    public float side = 0.87f;
    public List<GridElement> gridElementsList;

    public GridElement[,] matrix;
    public Transform containerTransform;

    public void Initialisation()
    {
        gridElementsList = new List<GridElement>();
        matrix = new GridElement[length, width];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GridElement item = Instantiate(GameController.instance.assetscController.gemElementPrefab, containerTransform);
                matrix[i, j] = item;
                item.positionVecor2 = new Vector2(i, j);
                item.gemElement.index = Random.Range(0, GameController.instance.assetscController.gemsSprite.Length);
                item.Initialisation();
            }
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
                Debug.Log("Near");
                GemElement _temp = gridElementsList[0].gemElement;
                gridElementsList[0].gemElement = gridElementsList[1].gemElement;
                gridElementsList[1].gemElement = _temp;

                gridElementsList[0].GetGemToPosition();
                gridElementsList[1].GetGemToPosition();

            }
            else
            {
                Debug.Log("too far");
            }
            gridElementsList = new List<GridElement>();
        }
    }
	

}
