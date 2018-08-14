﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour {

    public GemElement gemElement;
    public BombElement bombElement;
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
                gemElement.index = 99;
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
                GameController.instance.gridController.ClickCalc(this);
            }
    }

    public void GetGemToPosition (float speed,bool isAnim = true, bool isMatch = true)
    {
        gemElement.transform.SetParent(this.transform);
        if(isAnim)
        {
            Sequence seq = DOTween.Sequence();
            seq.Insert(0, gemElement.transform.DOLocalMove(Vector3.zero, speed).SetEase(Ease.InBack));
            if(!isMatch)
                {
                seq.SetLoops(2, LoopType.Yoyo);
            }
            seq.Play();
        }
        else
        {
            gemElement.transform.localPosition = Vector3.zero;
        }
    }


}
