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
            bangListHor.Add(GameController.instance.gridController.matrix[i, cordY]);
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
            bangListVer.Add(GameController.instance.gridController.matrix[cordX, i]);
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
        list[1].swipeElement.gemElement.index = 99;
        list[1].swipeElement.bombElement.index = 1;
        list[0].swipeElement = list[1].swipeElement;
        list[1].swipeElement = _tempswipe;
        list[0].GetGemToPosition(0.7f);
        list.Remove(list[0]);

    }

    public void Initialisation()
    {

    }


}
