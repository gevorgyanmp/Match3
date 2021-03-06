﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour {

    public SwipeElement swipeElement;
    public SpriteRenderer spriteRenderer;
    public bool mHor, mVer;

    private Vector2 _positionVecor2;
    public Vector2 positionVecor2
    {
        get
        {
            return _positionVecor2;
        }
        set
        {
            _positionVecor2 = value;
            transform.position = new Vector3(_positionVecor2.x, _positionVecor2.y, 0) * GameController.instance.gridController.side;
        }
    }

    private bool _isActive;
    public bool isActive
    {
        set
        {
            _isActive = value;
            if(value)
            {
                if(positionVecor2.x%2==0 && positionVecor2.y%2==0)
                {
                    spriteRenderer.sprite = GameController.instance.assetscController.gridsSprite[1];
                }
                else if (positionVecor2.x % 2 == 1 && positionVecor2.y % 2 == 1)
                {
                    spriteRenderer.sprite = GameController.instance.assetscController.gridsSprite[1];
                }
                else
                {
                    spriteRenderer.sprite = GameController.instance.assetscController.gridsSprite[0];
                }
            }
            else
            {
                spriteRenderer.sprite = null;
                swipeElement.gemElement.index = 99;
                swipeElement.bombElement.index = 99;
            }
        }
        get
        {
            return _isActive;
        }
    }
    

    public void Initialisation()
    {
        isActive = true;
    }

    private void OnMouseDown()
    {
            if (isActive)
            {
             if (GameController.instance.gridController.gridElementsList.Count<2 && GameController.instance.matchController.isAuto == false)
             {
                 GameController.instance.gridController.ClickCalc(this);
             }
             if (swipeElement.bombElement.index == 0)
             {
                GameController.instance.bombController.BangCube(this);
                GameController.instance.conditionController.CheckMoves();
            }
            else if (swipeElement.bombElement.index == 1)
             {
                GameController.instance.bombController.BangHorizontal(this);
                GameController.instance.conditionController.CheckMoves();
            }
            else if(swipeElement.bombElement.index == 2)
             {
                GameController.instance.bombController.BangVertical(this);
                GameController.instance.conditionController.CheckMoves();
            }
        }
    }

    public void GetGemToPosition (float speed,bool isAnim = true, bool isMatch = true)
    {
        swipeElement.transform.SetParent(this.transform);
        if(isAnim)
        {
            Sequence seq = DOTween.Sequence();
            seq.Insert(0, swipeElement.transform.DOLocalMove(Vector3.zero, speed).SetEase(Ease.InBack));
            if(!isMatch)
                {
                seq.SetLoops(2, LoopType.Yoyo);
            }
           
            seq.Play();

        }
        else
        {
            swipeElement.transform.localPosition = Vector3.zero;
        }
    }


}
