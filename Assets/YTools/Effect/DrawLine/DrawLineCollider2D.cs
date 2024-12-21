/****************************************************
    文件：DrawLineColidder.cs
    作者：Y
    邮箱: 916111418@qq.com
    日期：#CreateTime#
    功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
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
        _drawLine.OnCreateDraw += OnCreateLine;
        InitState();
    }

    private void Update()
    { 
        if (_drawLine.curLinePoints.Count > 0)
        {
            Debug.Log(_drawLine.curLinePoints.Count);
            //_points.Add(_drawLine.curLinePoints[0]);
            //简单根据距离优化一下点数，实际画线速度和弧度都有较大的影响
            while (_writeIndex < _drawLine.curLinePoints.Count - 1)
            {
                if (Vector2.Distance(_drawLine.curLinePoints[_readIndex], _drawLine.curLinePoints[_writeIndex + 1]) >=
                    _interval)
                {
                    _readIndex = _writeIndex;
                    //生成碰撞体
                    _points.Add(_drawLine.curLinePoints[_readIndex]);
                    _collider2D.points = _points.ToArray();
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
        _readIndex = 0;
        _writeIndex = 0;
        _collider2D = _drawLine.curLine.gameObject.AddComponent<EdgeCollider2D>();
        _collider2D.edgeRadius = _interval;
       _points.Clear();
    }
}