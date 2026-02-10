/****************************************************
    文件：HotPointGrid.cs
    作者：Y
    邮箱: 916111418@qq.com
    日期：#CreateTime#
    功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using YFramework;

public partial class HotPointGrid : MonoBehaviour, IHotPoint
{
    #region 成员变量

    [SerializeField] private HotPointGrid _parentGrid;
    private IHotPoint _hotPointParent;
    [SerializeField] private GameObject _point;
    [SerializeField] private HotPointSystem.HotPointType _hotPointType;

    public HotPointSystem.HotPointType HotPointType
    {
        get => _hotPointType;
    }

    public bool IsActivate { get; protected set; }

    public IHotPoint HotPointParent
    {
        get => _hotPointParent;
        protected set => _hotPointParent = value;
    }

    public List<IHotPoint> ChildNodes { get; protected set; }

    #endregion

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        ChildNodes = new List<IHotPoint>();
        HotPointSystem.RegisterHotPoint(this, _parentGrid);
    }


    public void SetParent(IHotPoint parent)
    {
        if (ReferenceEquals(parent, this))
        {
            Debug.LogError("禁止设置父亲对象为自己！");
            return;
        }

        HotPointParent = parent;
        if (!HotPointParent.ChildNodes.Contains(this))
        {
            HotPointParent.ChildNodes.Add(this);
        }
    }

    public void RemoveChild(IHotPoint child)
    {
        ChildNodes.Remove(child);
    }

    public void Activate()
    {
        if (_point)
        {
            _point.transform.gameObject.SetActive(true);
        }

        IsActivate = true;
        if (HotPointParent != null)
        {
            HotPointParent.Activate();
        }
    }

    public void Deactivate()
    {
        if (_point)
        {
            _point.transform.gameObject.SetActive(false);
        }

        IsActivate = false;
        if (HotPointParent != null)
        {
            bool active = false;
            foreach (var child in HotPointParent.ChildNodes)
            {
                if (child.IsActivate)
                {
                    active = true;
                    break;
                }
            }

            if (!active)
            {
                if (HotPointParent != null)
                {
                    HotPointParent.Deactivate();
                }
            }
        }
    }

    public void Dispose()
    {
        ChildNodes.Clear();
        ChildNodes = null;
        HotPointParent = null;
    }


    private void OnDestroy()
    {
        HotPointSystem.UnRegister(this);
    }
}