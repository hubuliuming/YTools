/****************************************************
    文件：DrawLineColidder.cs
    作者：Y
    邮箱: 916111418@qq.com
    日期：#CreateTime#
    功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawLineCollider2D : MonoBehaviour
{
    private DrawLineControl _drawLine;
    public Action OnEnter;

    private EdgeCollider2D _collider2D;
    private  List<Vector2> _points = new List<Vector2>();
    
    //生成碰撞体的间隔
    private float _interval;
    private int _readIndex;
    private int _writeIndex;

    private void Start()
    {
        _drawLine = GetComponent<DrawLineControl>();
        _drawLine.OnCreateDraw.OnTrigger = OnCreateLine;
        InitState();
    }

    private void Update()
    { 
        if (_drawLine.curLinePoints.Count > 1)
        {
          
            _points.Add(_drawLine.curLinePoints[1]);
           
            while (_writeIndex < _drawLine.curLinePoints.Count - 1)
            {
                if (Vector2.Distance(_drawLine.curLinePoints[_readIndex], _drawLine.curLinePoints[_writeIndex + 1]) >=
                    _interval)
                {
                    _readIndex = _writeIndex;
                    //生成碰撞体
                    _points.Add(_drawLine.curLinePoints[_readIndex]);
                    
                }
               
                _writeIndex++;
            }

        }
    }

    private void InitState()
    {
        var width = _drawLine.templatelineRenderer.widthMultiplier;
        _interval = width / 2f;
    }
    

    private void OnCreateLine()
    {
        Debug.Log("新建了一个Line");
        _readIndex = 1;
        _writeIndex = 1;
        _collider2D = _drawLine.curLine.gameObject.AddComponent<EdgeCollider2D>();
        _collider2D.edgeRadius = _interval;
        _collider2D.points = _points.ToArray();
    }
}