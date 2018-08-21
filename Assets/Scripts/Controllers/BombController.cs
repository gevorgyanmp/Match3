using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    public void BangHorizontal(GridElement bomb)
    {
        List<GridElement> bangListHor = new List<GridElement>();
        int cordY = (int)bomb.positionVecor2.y;

        for (int i = 0; i < GameController.instance.gridController.width; i++)
        {
            if(GameController.instance.gridController.matrix[i, cordY].swipeElement.gemElement.index == 99 && GameController.instance.gridController.matrix[i, cordY].swipeElement.bombElement.index == 2)
            {
                BangVertical(GameController.instance.gridController.matrix[i, cordY]);
            }
            else
            {
                bangListHor.Add(GameController.instance.gridController.matrix[i, cordY]);
            }
        }
        GameController.instance.gridController.DropElementHorizontal(bangListHor);
        bomb.swipeElement.bombElement.index = 99;
        GameController.instance.matchController.EliMatch();
    }

    public void BangVertical(GridElement bomb)
    {
        List<GridElement> bangListVer = new List<GridElement>();
        int cordX = (int)bomb.positionVecor2.x;
        for (int i = 0; i < GameController.instance.gridController.height; i++)
        {
            if(GameController.instance.gridController.matrix[cordX, i].swipeElement.gemElement.index == 99 && GameController.instance.gridController.matrix[cordX, i].swipeElement.bombElement.index == 1)
            {
                BangHorizontal(GameController.instance.gridController.matrix[cordX, i]);
            }
            else
            {
                bangListVer.Add(GameController.instance.gridController.matrix[cordX, i]);
            }
        }
        GameController.instance.gridController.DropElementVertical(bangListVer);
        bomb.swipeElement.bombElement.index = 99;
        GameController.instance.matchController.EliMatch();
    }

    public void CreateBombHorizontal(SwipeElement swiper)
    {
        swiper.gemElement.index = 99;
        swiper.bombElement.index = 2;
    }

    public void CreateBombVertical(List<GridElement> list)
    {
        SwipeElement _tempswipe = list[0].swipeElement;
        GridElement _turnToBomb;
        if (GameController.instance.gridController.gridElementsList.Count != 0)
        {
           _turnToBomb = GameController.instance.matchController.CheckTransformBomb(GameController.instance.gridController.gridElementsList, list);
        }
        else
        {
            _turnToBomb = list[Random.Range(0, list.Count)];
        }
        _turnToBomb.swipeElement.gemElement.index = 99;
        _turnToBomb.swipeElement.bombElement.index = 1;
        list[0].swipeElement = _turnToBomb.swipeElement;
        _turnToBomb.swipeElement = _tempswipe;
        list[0].GetGemToPosition(0.7f);
        list.Remove(list[0]);
    }

    public void Initialisation()
    {

    }


}
