/****************************************************
    文件：DrawLineControl.cs
    作者：Y
    邮箱: 916111418@qq.com
    日期：#CreateTime#
    功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using YFramework;
using YFramework.Extension;
using YFramework.Kit;
using Object = UnityEngine.Object;

public class DrawLineControl : MonoBehaviour
{
    [Tooltip("画线的密度.默认1为每秒六十点")]
    [Range(0.1f,2f)]
    public float Power = 1;
    [Tooltip("是否抬起时候重置线")] 
    public bool IsUpReset;
        
    
    public Camera TargetCamera { get; set; }
    public LineRenderer templatelineRenderer;

    public List<Vector2> curLinePoints = new List<Vector2>();

    public LayerMask layerMask;

    public LineRenderer curLine => _childLines.Peek();
    public Action OnCreateDraw;
    
    private Stack<LineRenderer> _childLines;
    
    private ActionFixedUpdate  _fixedUpdate;
    private const int DefaultFrequency = 60;

    private void Awake()
    {
        TargetCamera = Camera.main;
        _childLines = new Stack<LineRenderer>();
        _fixedUpdate = ActionKit.SecondsFixedUpdate(DefaultFrequency, DrawLine); 
        OnReset();
        
    }


    public void Clear()
    {
        OnReset();
        CreateLine();
    }

    private void Update()
    {
        _fixedUpdate.SetFrequency((int)(Power*DefaultFrequency));
        DrawUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clear();
        }
        
    }

    private void DrawUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        //DrawLine();
        StopDraw();
    }
    

    private void DrawLine()
    {
       
        if (Input.GetMouseButton(0))
        {
            var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Mathf.Abs(TargetCamera.transform.position.z));
            var position = TargetCamera.ScreenToWorldPoint(pos);


            //检测是否为不可画线的地方
            // if (pointList.Count <= 1 && Physics2D.Raycast(position, Vector3.forward, 100, layerMask))
            // {
            //     return;
            // }
            // if (pointList.Count > 1)
            // {
            //     RaycastHit2D raycast = Physics2D.Raycast(position, (pointList[_lineRenderer.positionCount - 1] - position).normalized,
            //     (position - pointList[_lineRenderer.positionCount - 1]).magnitude, layerMask);
            //     if (raycast)
            //     {
            //         Debug.Log("return");
            //         return;
            //     }
            // }


            //画线
            if (!curLinePoints.Contains(position))
            {
                _childLines.Peek().AddPoint(position);
                curLinePoints.Add(position);
            }
        }
    }


    private void StopDraw()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (IsUpReset)
            {
                OnReset();
            }
            

            // if (!_lineRenderer.gameObject.GetComponent<Rigidbody2D>())
            // {
            //     _lineRenderer.gameObject.AddComponent<Rigidbody2D>();
            //     _lineRenderer.gameObject.GetComponent<Rigidbody2D>().mass = 300;
            // }
        }
    }

    private void CreateLine()
    {
        curLinePoints.Clear();
        var go = Object.Instantiate(templatelineRenderer.gameObject,transform);
        go.transform.SetParent(transform);
        var child = go.GetComponent<LineRenderer>();
        go.SetActive(true);
        child.positionCount = 0;
        _childLines.Push(child);
        OnCreateDraw?.Invoke();
    }

    private void OnReset()
    {
        templatelineRenderer.positionCount = 0;
        curLinePoints.Clear();
        foreach (var chid in _childLines)
        {
            Destroy(chid.gameObject);
        }

        _childLines.Clear();
    }
}